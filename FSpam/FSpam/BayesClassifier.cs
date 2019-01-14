using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FSpam
{
    public class BayesClassifier
    {
        private List<string> spamMails;
        private List<string> notSpamMails;
        private int countSpamMails;
        private int countNotSpamMails;

        public BayesClassifier()
        {
            SpamHamReader reader = new SpamHamReader();
            spamMails = reader.Read("spam");
            notSpamMails = reader.Read("ham");
            countSpamMails = spamMails.Count();
            countNotSpamMails = notSpamMails.Count();
        }

        public Tuple<string, double> CheckEmail(string text)
        {
            Trainer trainer = new Trainer();
            foreach (var spamMail in spamMails)
            {
                trainer.Train(spamMail);
            }
            var spamWords = trainer.WordCounter;

            trainer = new Trainer();
            foreach (var notSpamMail in notSpamMails)
            {
                trainer.Train(notSpamMail);
            }

            var notSpamWords = trainer.WordCounter;

            return CheckIfSpam(text, countSpamMails, spamWords, countNotSpamMails, notSpamWords);
        }

        private Tuple<string, double> CheckIfSpam(string text,
            int countSpamMails, Dictionary<string, int> spamWordList,
            int countNotSpamMails, Dictionary<string, int> notSpamWordList)
        {

            string[] textOriginal = text.Split(' ');
            List<string> textWithoutNumbers = new List<string>();

            foreach (var word in textOriginal)
            {
                var removedNumberWord = Regex.Replace(word, @"\d", "");
                textWithoutNumbers.Add(removedNumberWord.ToLower());
            }

            List<string> wordsList = new List<string>();   
            
            foreach(var word in textWithoutNumbers)
            {
                var removedPunctuationWord = Regex.Replace(word, @"[^\w\s]", "");
                wordsList.Add(removedPunctuationWord.ToLower());
            }

            var wordsToRemove = new CommonWords();
            wordsList.RemoveAll(x => wordsToRemove.commonWords.Contains(x));
            wordsList.RemoveAll(x => x == String.Empty);
            wordsList = wordsList.Distinct().ToList();

            List<double> PvaluesSpam = new List<double>();
            List<double> PvaluesHam = new List<double>();

            foreach (var word in wordsList)
            {
                if(notSpamWordList.ContainsKey(word) && spamWordList.ContainsKey(word))
                {
                    var p = CalculateProbability(word.ToLower(),
                        countSpamMails, spamWordList,
                        countNotSpamMails, notSpamWordList);

                    PvaluesSpam.Add(p);//P(S|W)
                    PvaluesHam.Add(1 - p);//P(H|W)

                }
            }

            //konacni izracun
            double probabilityOfSpamMail;
            if (PvaluesSpam.Count == 0 || PvaluesHam.Count == 0)
                probabilityOfSpamMail = 0.5;// u slučaju da su nepoznate sve riječi emaila
            else
            {
                double PSpam = PvaluesSpam.Aggregate((a, x) => a * x);
                double PHam = PvaluesHam.Aggregate((a, x) => a * x);

                probabilityOfSpamMail = PSpam / (PSpam + PHam);
            }

            if (probabilityOfSpamMail > 0.5)
            {
                return Tuple.Create("SPAM", probabilityOfSpamMail);
            }

            else
            {
                return Tuple.Create("HAM", probabilityOfSpamMail);

            }
        }

        private double CalculateProbability(string word,
            int countSpamMails, Dictionary<string, int> spamWordList,
            int countNotSpamMails, Dictionary<string, int> notSpamWordList)
        {

            double ProbMailIsSpam = 0.5;//P(S)      //konstanta
            double ProbMailIsNotSpam = 0.5;//P(H)  //konstanta

            //vjerojatnost da riječ pripada spam mailu
            double wordCountInSpam = spamWordList[word];
            double ProbWordInSpam = wordCountInSpam / (double)countSpamMails; //P(W|S);

            // vjerojatnost da riječ pripada ne spam mailu
            double wordCountInNotSpam = notSpamWordList[word];
            double ProbWordInNotSpam = wordCountInNotSpam / (double)countNotSpamMails; //P(W|H)


            return (double)(ProbWordInSpam * ProbMailIsSpam) / (double)((ProbWordInSpam * ProbMailIsSpam) + (ProbWordInNotSpam * ProbMailIsNotSpam));//P(S|W)
        }
    }
}

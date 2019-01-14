using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace FSpam
{
    public class Trainer
    {
        private Dictionary<string, int> wordCounter = new Dictionary<string, int>();

        public Dictionary<string, int> WordCounter
        {
            get { return wordCounter; }
            set { wordCounter = value; }
        }

        public void Train(string textToParse)
        {
            string[] textOriginal = textToParse.Split(' ');
            List<string> textWithoutNumbers = new List<string>();
            
            foreach (var word in textOriginal)
            {
                var removedNumberWord = Regex.Replace(word, @"\d", "");
                textWithoutNumbers.Add(removedNumberWord.ToLower());
            }
            
            List<string> wordsList = new List<string>();

            foreach (var word in textOriginal)
            {
                var removedPunctuationWord = Regex.Replace(word, @"[^\w\s]", ""); //\s is whitespace characters, namely spacebar and tab. \w is alphanumeric characters.
                wordsList.Add(removedPunctuationWord.ToLower());
            }

            var commonWords = new CommonWords();
            wordsList.RemoveAll(x => commonWords.commonWords.Contains(x));
            wordsList.RemoveAll(x => x==String.Empty);
            wordsList = wordsList.Distinct().ToList();

            foreach (var str in wordsList)
            {
                if (wordCounter.ContainsKey(str.ToLower()))
                {
                    wordCounter[str.ToLower()] += 1;
                }
                else
                {
                    wordCounter.Add(str.ToLower(), 1);
                }
            }
        }
    }
}

using FSpam.mail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSpam
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {        
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            MailLoader mail = new MailLoader();
            mail.readMail(txtUsername.Text, txtPassword.Text);

            List<Tuple<string, double>> results = new List<Tuple<string, double>>();

            if (mail.zastavica != 0)
            {
                for(int i=0; i<mail.titles.Count;i++)
                {
                    BayesClassifier classifier = new BayesClassifier();
                    var result = classifier.CheckEmail(mail.titles[i] +" "+ mail.summaries[i]);
                    results.Add(result);
                }         

                FrmMails formMails = new FrmMails(mail.titles, mail.summaries, mail.senders, results);
                formMails.ShowDialog();
            }
        }
    }
}

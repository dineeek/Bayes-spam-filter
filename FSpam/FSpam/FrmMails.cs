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
    public partial class FrmMails : Form
    {
        private List<string> titles;
        private List<string> summaries;
        private List<string> senders;
        private List<Tuple<string, double>> results;

        DataTable dt = new DataTable();

        public FrmMails(List<string> titl, List<string> summary, List<string> authors, List<Tuple<string, double>> result)
        {
            InitializeComponent();

            titles = titl;
            summaries = summary;
            senders = authors;
            results = result;
        }

        private void FrmMails_Load(object sender, EventArgs e)
        {
            dt.Columns.Add(new DataColumn("Pošiljatelj", typeof(string)));
            dt.Columns.Add(new DataColumn("Naslov", typeof(string)));
            dt.Columns.Add(new DataColumn("Sažetak poruke", typeof(string)));
            dt.Columns.Add(new DataColumn("Bayes vrijednost", typeof(double)));
            dt.Columns.Add(new DataColumn("Rezultat", typeof(string))); //- DA ILI NE
         
            for(int i=0; i<titles.Count; i++)
            {
                DataRow row = dt.NewRow();

                row["Pošiljatelj"] = senders[i];
                row["Naslov"] = titles[i];
                row["Sažetak poruke"] = summaries[i];
                row["Bayes vrijednost"] = Math.Round(results[i].Item2, 5);
                row["Rezultat"] = results[i].Item1;

                dt.Rows.Add(row);
            }
            
            dgvMails.DataSource = dt;

            dgvMails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvMails.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dgvMails.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvMails.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvMails.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            //bojanje spamova
            foreach (DataGridViewRow row in dgvMails.Rows)
            {
                if (row.Cells[4].Value.ToString() == "SPAM")
                    row.DefaultCellStyle.BackColor = Color.LightPink;
            }
        }

        private void dgvMails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRowIndex = dgvMails.CurrentCell.RowIndex;

            MessageBox.Show(summaries[selectedRowIndex], "Dodatne informacije");
        }
    }
}

namespace FSpam
{
    partial class FrmMails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvMails = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMails
            // 
            this.dgvMails.AllowUserToAddRows = false;
            this.dgvMails.AllowUserToDeleteRows = false;
            this.dgvMails.AllowUserToOrderColumns = true;
            this.dgvMails.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvMails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMails.Location = new System.Drawing.Point(1, 31);
            this.dgvMails.MultiSelect = false;
            this.dgvMails.Name = "dgvMails";
            this.dgvMails.Size = new System.Drawing.Size(1264, 657);
            this.dgvMails.TabIndex = 0;
            this.dgvMails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMails_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(1, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nepročitani mailovi:";
            // 
            // FrmMails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 689);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvMails);
            this.MaximizeBox = false;
            this.Name = "FrmMails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtered mails";
            this.Load += new System.EventHandler(this.FrmMails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMails;
        private System.Windows.Forms.Label label1;
    }
}
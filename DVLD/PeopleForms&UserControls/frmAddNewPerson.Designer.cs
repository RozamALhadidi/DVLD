namespace DVLD.People
{
    partial class frmAddNewPerson
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
            this.uctrlAddNewPerson1 = new DVLD.People.uctrlAddNewPerson();
            this.SuspendLayout();
            // 
            // uctrlAddNewPerson1
            // 
            this.uctrlAddNewPerson1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.uctrlAddNewPerson1.Location = new System.Drawing.Point(7, 16);
            this.uctrlAddNewPerson1.Name = "uctrlAddNewPerson1";
            this.uctrlAddNewPerson1.Size = new System.Drawing.Size(1656, 965);
            this.uctrlAddNewPerson1.TabIndex = 0;
            // 
            // frmAddNewPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(1919, 1005);
            this.Controls.Add(this.uctrlAddNewPerson1);
            this.Name = "frmAddNewPerson";
            this.Text = "frmAddNewPerson";
            this.Load += new System.EventHandler(this.frmAddNewPerson_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private uctrlAddNewPerson uctrlAddNewPerson1;
    }
}
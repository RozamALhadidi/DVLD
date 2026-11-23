namespace DVLD.People
{
    partial class frmUpdatePersonDetails
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
            this.uctrlUpdatePerson1 = new DVLD.uctrlUpdatePerson();
            this.SuspendLayout();
            // 
            // uctrlUpdatePerson1
            // 
            this.uctrlUpdatePerson1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.uctrlUpdatePerson1.Location = new System.Drawing.Point(85, 51);
            this.uctrlUpdatePerson1.Name = "uctrlUpdatePerson1";
            this.uctrlUpdatePerson1.Size = new System.Drawing.Size(1659, 966);
            this.uctrlUpdatePerson1.TabIndex = 0;
            // 
            // frmUpdatePersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(1801, 1066);
            this.Controls.Add(this.uctrlUpdatePerson1);
            this.Name = "frmUpdatePersonDetails";
            this.Text = "frmEditPersonDetails";
            this.ResumeLayout(false);

        }

        #endregion

        private uctrlUpdatePerson uctrlUpdatePerson1;
    }
}
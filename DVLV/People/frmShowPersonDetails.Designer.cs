namespace DVLV.People
{
    partial class frmShowPersonDetails
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
            this.uctrlPersonDetails1 = new DVLV.People.uctrlPersonDetails();
            this.SuspendLayout();
            // 
            // uctrlPersonDetails1
            // 
            this.uctrlPersonDetails1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uctrlPersonDetails1.Location = new System.Drawing.Point(6, 2);
            this.uctrlPersonDetails1.Name = "uctrlPersonDetails1";
            this.uctrlPersonDetails1.Size = new System.Drawing.Size(1684, 781);
            this.uctrlPersonDetails1.TabIndex = 0;
            // 
            // frmShowPersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1688, 783);
            this.Controls.Add(this.uctrlPersonDetails1);
            this.Name = "frmShowPersonDetails";
            this.Text = "frmShowPersonDetails";
            this.ResumeLayout(false);

        }

        #endregion

        private uctrlPersonDetails uctrlPersonDetails1;
    }
}
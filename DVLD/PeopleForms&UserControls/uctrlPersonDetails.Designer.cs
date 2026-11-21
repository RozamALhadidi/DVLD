namespace DVLD.People
{
    partial class uctrlPersonDetails
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crownLabel19 = new ReaLTaiizor.Controls.CrownLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // crownLabel19
            // 
            this.crownLabel19.AutoSize = true;
            this.crownLabel19.Font = new System.Drawing.Font("Alexandria ExtraBold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crownLabel19.ForeColor = System.Drawing.Color.Black;
            this.crownLabel19.Location = new System.Drawing.Point(581, 54);
            this.crownLabel19.Name = "crownLabel19";
            this.crownLabel19.Size = new System.Drawing.Size(475, 97);
            this.crownLabel19.TabIndex = 46;
            this.crownLabel19.Text = "Person Details:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 2.142857F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(578, 141);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 10);
            this.panel1.TabIndex = 47;
            // 
            // uctrlPersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.crownLabel19);
            this.Name = "uctrlPersonDetails";
            this.Size = new System.Drawing.Size(1660, 892);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ReaLTaiizor.Controls.CrownLabel crownLabel19;
        private System.Windows.Forms.Panel panel1;
    }
}

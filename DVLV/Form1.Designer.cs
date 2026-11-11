namespace DVLD
{
    partial class Form1
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
            this.materialButton1 = new ReaLTaiizor.Controls.MaterialButton();
            this.tb1 = new ReaLTaiizor.Controls.MaterialMaskedTextBox();
            this.tb2 = new ReaLTaiizor.Controls.MaterialMaskedTextBox();
            this.crownGroupBox1 = new ReaLTaiizor.Controls.CrownGroupBox();
            this.darkButton1 = new DVLV.DarkButton();
            this.darkButton2 = new DVLV.DarkButton();
            this.crownGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialButton1
            // 
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton1.Depth = 0;
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = null;
            this.materialButton1.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.materialButton1.Location = new System.Drawing.Point(90, 35);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton1.Size = new System.Drawing.Size(264, 63);
            this.materialButton1.TabIndex = 3;
            this.materialButton1.Text = "materialButton1";
            this.materialButton1.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = false;
            this.materialButton1.UseVisualStyleBackColor = true;
            this.materialButton1.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // tb1
            // 
            this.tb1.AllowPromptAsInput = true;
            this.tb1.AnimateReadOnly = false;
            this.tb1.AsciiOnly = false;
            this.tb1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tb1.BeepOnError = false;
            this.tb1.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.tb1.Depth = 0;
            this.tb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tb1.HidePromptOnLeave = false;
            this.tb1.HideSelection = true;
            this.tb1.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.tb1.LeadingIcon = null;
            this.tb1.Location = new System.Drawing.Point(47, 162);
            this.tb1.Mask = "";
            this.tb1.MaxLength = 32767;
            this.tb1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.tb1.Name = "tb1";
            this.tb1.PasswordChar = '\0';
            this.tb1.PrefixSuffixText = null;
            this.tb1.PromptChar = '_';
            this.tb1.ReadOnly = false;
            this.tb1.RejectInputOnFirstFailure = false;
            this.tb1.ResetOnPrompt = true;
            this.tb1.ResetOnSpace = true;
            this.tb1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb1.SelectedText = "";
            this.tb1.SelectionLength = 0;
            this.tb1.SelectionStart = 0;
            this.tb1.ShortcutsEnabled = true;
            this.tb1.Size = new System.Drawing.Size(379, 84);
            this.tb1.SkipLiterals = true;
            this.tb1.TabIndex = 4;
            this.tb1.TabStop = false;
            this.tb1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tb1.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.tb1.TrailingIcon = null;
            this.tb1.UseSystemPasswordChar = false;
            this.tb1.ValidatingType = null;
            // 
            // tb2
            // 
            this.tb2.AllowPromptAsInput = true;
            this.tb2.AnimateReadOnly = false;
            this.tb2.AsciiOnly = false;
            this.tb2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tb2.BeepOnError = false;
            this.tb2.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.tb2.Depth = 0;
            this.tb2.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tb2.HidePromptOnLeave = false;
            this.tb2.HideSelection = true;
            this.tb2.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.tb2.LeadingIcon = null;
            this.tb2.Location = new System.Drawing.Point(47, 291);
            this.tb2.Mask = "";
            this.tb2.MaxLength = 32767;
            this.tb2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.tb2.Name = "tb2";
            this.tb2.PasswordChar = '\0';
            this.tb2.PrefixSuffixText = null;
            this.tb2.PromptChar = '_';
            this.tb2.ReadOnly = false;
            this.tb2.RejectInputOnFirstFailure = false;
            this.tb2.ResetOnPrompt = true;
            this.tb2.ResetOnSpace = true;
            this.tb2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb2.SelectedText = "";
            this.tb2.SelectionLength = 0;
            this.tb2.SelectionStart = 0;
            this.tb2.ShortcutsEnabled = true;
            this.tb2.Size = new System.Drawing.Size(379, 84);
            this.tb2.SkipLiterals = true;
            this.tb2.TabIndex = 5;
            this.tb2.TabStop = false;
            this.tb2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tb2.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.tb2.TrailingIcon = null;
            this.tb2.UseSystemPasswordChar = false;
            this.tb2.ValidatingType = null;
            // 
            // crownGroupBox1
            // 
            this.crownGroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.crownGroupBox1.Controls.Add(this.tb2);
            this.crownGroupBox1.Controls.Add(this.tb1);
            this.crownGroupBox1.Controls.Add(this.materialButton1);
            this.crownGroupBox1.Location = new System.Drawing.Point(23, 27);
            this.crownGroupBox1.Name = "crownGroupBox1";
            this.crownGroupBox1.Size = new System.Drawing.Size(465, 400);
            this.crownGroupBox1.TabIndex = 6;
            this.crownGroupBox1.TabStop = false;
            this.crownGroupBox1.Text = "crownGroupBox1";
            // 
            // darkButton1
            // 
            this.darkButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.darkButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.darkButton1.FlatAppearance.BorderSize = 0;
            this.darkButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darkButton1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.darkButton1.ForeColor = System.Drawing.Color.White;
            this.darkButton1.Location = new System.Drawing.Point(556, 52);
            this.darkButton1.Name = "darkButton1";
            this.darkButton1.Size = new System.Drawing.Size(337, 113);
            this.darkButton1.TabIndex = 7;
            this.darkButton1.Text = "darkButton1";
            this.darkButton1.UseVisualStyleBackColor = false;
            this.darkButton1.Click += new System.EventHandler(this.darkButton1_Click);
            // 
            // darkButton2
            // 
            this.darkButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.darkButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.darkButton2.FlatAppearance.BorderSize = 0;
            this.darkButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darkButton2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.darkButton2.ForeColor = System.Drawing.Color.White;
            this.darkButton2.Location = new System.Drawing.Point(953, 52);
            this.darkButton2.Name = "darkButton2";
            this.darkButton2.Size = new System.Drawing.Size(337, 113);
            this.darkButton2.TabIndex = 8;
            this.darkButton2.Text = "darkButton2";
            this.darkButton2.UseVisualStyleBackColor = false;
            this.darkButton2.Click += new System.EventHandler(this.darkButton2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(2017, 845);
            this.Controls.Add(this.darkButton2);
            this.Controls.Add(this.darkButton1);
            this.Controls.Add(this.crownGroupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.crownGroupBox1.ResumeLayout(false);
            this.crownGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ReaLTaiizor.Controls.MaterialButton materialButton1;
        private ReaLTaiizor.Controls.MaterialMaskedTextBox tb1;
        private ReaLTaiizor.Controls.MaterialMaskedTextBox tb2;
        private ReaLTaiizor.Controls.CrownGroupBox crownGroupBox1;
        private DVLV.DarkButton darkButton1;
        private DVLV.DarkButton darkButton2;
    }
}


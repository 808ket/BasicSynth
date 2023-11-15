namespace BasicSynthesizer
{
    partial class BasicSynthesizer
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.oscillator1 = new BasicSynthesizer.Oscillator();
            this.oscillator2 = new BasicSynthesizer.Oscillator();
            this.oscillator3 = new BasicSynthesizer.Oscillator();
            this.SuspendLayout();
            // 
            // oscillator1
            // 
            this.oscillator1.Location = new System.Drawing.Point(12, 12);
            this.oscillator1.Name = "oscillator1";
            this.oscillator1.Size = new System.Drawing.Size(200, 100);
            this.oscillator1.TabIndex = 0;
            this.oscillator1.TabStop = false;
            this.oscillator1.Text = "Oscillator 1";
            this.oscillator1.Enter += new System.EventHandler(this.oscillator1_Enter);
            // 
            // oscillator2
            // 
            this.oscillator2.Location = new System.Drawing.Point(12, 118);
            this.oscillator2.Name = "oscillator2";
            this.oscillator2.Size = new System.Drawing.Size(200, 100);
            this.oscillator2.TabIndex = 1;
            this.oscillator2.TabStop = false;
            this.oscillator2.Text = "Oscillator 2";
            // 
            // oscillator3
            // 
            this.oscillator3.Location = new System.Drawing.Point(12, 224);
            this.oscillator3.Name = "oscillator3";
            this.oscillator3.Size = new System.Drawing.Size(200, 100);
            this.oscillator3.TabIndex = 2;
            this.oscillator3.TabStop = false;
            this.oscillator3.Text = "Oscillator 3";
            // 
            // BasicSynthesizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 408);
            this.Controls.Add(this.oscillator3);
            this.Controls.Add(this.oscillator2);
            this.Controls.Add(this.oscillator1);
            this.KeyPreview = true;
            this.Name = "BasicSynthesizer";
            this.Text = "BasicSynthesizer";
            this.Load += new System.EventHandler(this.BasicSynthesizer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BasicSynthesizer_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private BasicSynthesizer.Oscillator oscillator1;
        private BasicSynthesizer.Oscillator oscillator2;
        private BasicSynthesizer.Oscillator oscillator3;
    }
}


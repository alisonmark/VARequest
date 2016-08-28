namespace Auto_VAR
{
    partial class FrmCaptcharBox
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
            this.txtCaptcha = new System.Windows.Forms.MaskedTextBox();
            this.ptbCaptcha = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ptbCaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCaptcha
            // 
            this.txtCaptcha.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtCaptcha.Location = new System.Drawing.Point(62, 7);
            this.txtCaptcha.Mask = "L L L L L L";
            this.txtCaptcha.Name = "txtCaptcha";
            this.txtCaptcha.Size = new System.Drawing.Size(103, 26);
            this.txtCaptcha.TabIndex = 10;
            this.txtCaptcha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCaptcha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCaptcha_KeyDown);
            this.txtCaptcha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCaptcha_KeyPress);
            // 
            // ptbCaptcha
            // 
            this.ptbCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ptbCaptcha.Location = new System.Drawing.Point(1, 40);
            this.ptbCaptcha.Name = "ptbCaptcha";
            this.ptbCaptcha.Size = new System.Drawing.Size(252, 52);
            this.ptbCaptcha.TabIndex = 11;
            this.ptbCaptcha.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Captcha";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(171, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 26);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK (20)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmCaptcharBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 95);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtCaptcha);
            this.Controls.Add(this.ptbCaptcha);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmCaptcharBox";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Box";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCaptcharBox_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ptbCaptcha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox txtCaptcha;
        private System.Windows.Forms.PictureBox ptbCaptcha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
    }
}
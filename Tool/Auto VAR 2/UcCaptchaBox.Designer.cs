namespace Auto_VAR
{
    partial class UcCaptchaBox
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAcceptCaptcha = new System.Windows.Forms.Button();
            this.txtCaptcha = new System.Windows.Forms.MaskedTextBox();
            this.ptbCaptcha = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbCaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAcceptCaptcha);
            this.groupBox1.Controls.Add(this.txtCaptcha);
            this.groupBox1.Controls.Add(this.ptbCaptcha);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnAcceptCaptcha
            // 
            this.btnAcceptCaptcha.Location = new System.Drawing.Point(179, 18);
            this.btnAcceptCaptcha.Name = "btnAcceptCaptcha";
            this.btnAcceptCaptcha.Size = new System.Drawing.Size(75, 26);
            this.btnAcceptCaptcha.TabIndex = 1;
            this.btnAcceptCaptcha.Text = "OK";
            this.btnAcceptCaptcha.UseVisualStyleBackColor = true;
            this.btnAcceptCaptcha.Click += new System.EventHandler(this.btnAcceptCaptcha_Click);
            // 
            // txtCaptcha
            // 
            this.txtCaptcha.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtCaptcha.Location = new System.Drawing.Point(70, 18);
            this.txtCaptcha.Mask = "L L L L L L";
            this.txtCaptcha.Name = "txtCaptcha";
            this.txtCaptcha.Size = new System.Drawing.Size(103, 26);
            this.txtCaptcha.TabIndex = 0;
            this.txtCaptcha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCaptcha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCaptcha_KeyDown);
            this.txtCaptcha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCaptcha_KeyPress);
            // 
            // ptbCaptcha
            // 
            this.ptbCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ptbCaptcha.Location = new System.Drawing.Point(3, 52);
            this.ptbCaptcha.Name = "ptbCaptcha";
            this.ptbCaptcha.Size = new System.Drawing.Size(252, 52);
            this.ptbCaptcha.TabIndex = 15;
            this.ptbCaptcha.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Captcha";
            // 
            // UcCaptchaBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "UcCaptchaBox";
            this.Size = new System.Drawing.Size(258, 112);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbCaptcha)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button btnAcceptCaptcha;
        public System.Windows.Forms.MaskedTextBox txtCaptcha;
        public System.Windows.Forms.PictureBox ptbCaptcha;
        private System.Windows.Forms.Label label4;
    }
}

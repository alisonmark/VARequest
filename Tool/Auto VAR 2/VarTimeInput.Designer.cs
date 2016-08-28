namespace Auto_VAR
{
    partial class VarTimeInput
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
            this.txtStartTime = new System.Windows.Forms.MaskedTextBox();
            this.chbUse = new System.Windows.Forms.CheckBox();
            this.txtNumUser = new System.Windows.Forms.NumericUpDown();
            this.txtFrequency = new System.Windows.Forms.NumericUpDown();
            this.txtRepeatInterval = new System.Windows.Forms.NumericUpDown();
            this.txtRepeatCount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepeatInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepeatCount)).BeginInit();
            this.SuspendLayout();
            // 
            // txtStartTime
            // 
            this.txtStartTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtStartTime.Location = new System.Drawing.Point(72, 3);
            this.txtStartTime.Mask = "00:00:00.000";
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(101, 26);
            this.txtStartTime.TabIndex = 2;
            this.txtStartTime.Leave += new System.EventHandler(this.txtStartTime_Leave);
            // 
            // chbUse
            // 
            this.chbUse.AutoSize = true;
            this.chbUse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chbUse.Location = new System.Drawing.Point(3, 9);
            this.chbUse.Name = "chbUse";
            this.chbUse.Size = new System.Drawing.Size(15, 14);
            this.chbUse.TabIndex = 0;
            this.chbUse.UseVisualStyleBackColor = true;
            this.chbUse.CheckedChanged += new System.EventHandler(this.chbUse_CheckedChanged);
            // 
            // txtNumUser
            // 
            this.txtNumUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtNumUser.Location = new System.Drawing.Point(24, 3);
            this.txtNumUser.Name = "txtNumUser";
            this.txtNumUser.Size = new System.Drawing.Size(42, 26);
            this.txtNumUser.TabIndex = 1;
            // 
            // txtFrequency
            // 
            this.txtFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtFrequency.Location = new System.Drawing.Point(179, 3);
            this.txtFrequency.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.txtFrequency.Name = "txtFrequency";
            this.txtFrequency.Size = new System.Drawing.Size(46, 26);
            this.txtFrequency.TabIndex = 3;
            // 
            // txtRepeatInterval
            // 
            this.txtRepeatInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtRepeatInterval.Location = new System.Drawing.Point(231, 3);
            this.txtRepeatInterval.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.txtRepeatInterval.Name = "txtRepeatInterval";
            this.txtRepeatInterval.Size = new System.Drawing.Size(43, 26);
            this.txtRepeatInterval.TabIndex = 4;
            // 
            // txtRepeatCount
            // 
            this.txtRepeatCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtRepeatCount.Location = new System.Drawing.Point(280, 3);
            this.txtRepeatCount.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.txtRepeatCount.Name = "txtRepeatCount";
            this.txtRepeatCount.Size = new System.Drawing.Size(43, 26);
            this.txtRepeatCount.TabIndex = 5;
            // 
            // VarTimeInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtRepeatCount);
            this.Controls.Add(this.txtRepeatInterval);
            this.Controls.Add(this.txtFrequency);
            this.Controls.Add(this.txtNumUser);
            this.Controls.Add(this.chbUse);
            this.Controls.Add(this.txtStartTime);
            this.Name = "VarTimeInput";
            this.Size = new System.Drawing.Size(326, 31);
            ((System.ComponentModel.ISupportInitialize)(this.txtNumUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepeatInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepeatCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox txtStartTime;
        private System.Windows.Forms.CheckBox chbUse;
        private System.Windows.Forms.NumericUpDown txtNumUser;
        private System.Windows.Forms.NumericUpDown txtFrequency;
        private System.Windows.Forms.NumericUpDown txtRepeatInterval;
        private System.Windows.Forms.NumericUpDown txtRepeatCount;
    }
}

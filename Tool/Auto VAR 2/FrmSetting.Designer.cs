namespace Auto_VAR
{
    partial class FrmSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsername0 = new System.Windows.Forms.TextBox();
            this.txtPrepareSeconds = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword0 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDelayEachStep = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDelaySubmit = new System.Windows.Forms.NumericUpDown();
            this.txtRetrySubmitCount = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDecaptchaTimeout = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtForceSubmitDelay = new System.Windows.Forms.NumericUpDown();
            this.txtForceSubmitCount = new System.Windows.Forms.NumericUpDown();
            this.txtMaxCaptchaManual = new System.Windows.Forms.NumericUpDown();
            this.txtSessionTime = new System.Windows.Forms.NumericUpDown();
            this.txtSessionTimeout = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPassword1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUsername1 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPassword2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtUsername2 = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPassword3 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtUsername3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrepareSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelayEachStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelaySubmit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRetrySubmitCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecaptchaTimeout)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtForceSubmitDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtForceSubmitCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxCaptchaManual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSessionTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSessionTimeout)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(8, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "DelayEachStep (ms)";
            // 
            // txtUsername0
            // 
            this.txtUsername0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtUsername0.Location = new System.Drawing.Point(99, 25);
            this.txtUsername0.Name = "txtUsername0";
            this.txtUsername0.Size = new System.Drawing.Size(138, 26);
            this.txtUsername0.TabIndex = 0;
            // 
            // txtPrepareSeconds
            // 
            this.txtPrepareSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtPrepareSeconds.Location = new System.Drawing.Point(190, 26);
            this.txtPrepareSeconds.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtPrepareSeconds.Name = "txtPrepareSeconds";
            this.txtPrepareSeconds.Size = new System.Drawing.Size(83, 26);
            this.txtPrepareSeconds.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnCancel.Location = new System.Drawing.Point(299, 405);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Location = new System.Drawing.Point(176, 405);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 35);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(6, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Password";
            // 
            // txtPassword0
            // 
            this.txtPassword0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtPassword0.Location = new System.Drawing.Point(99, 57);
            this.txtPassword0.Name = "txtPassword0";
            this.txtPassword0.Size = new System.Drawing.Size(138, 26);
            this.txtPassword0.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(8, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "DelaySubmit (ms)";
            // 
            // txtDelayEachStep
            // 
            this.txtDelayEachStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDelayEachStep.Location = new System.Drawing.Point(191, 63);
            this.txtDelayEachStep.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtDelayEachStep.Name = "txtDelayEachStep";
            this.txtDelayEachStep.Size = new System.Drawing.Size(83, 26);
            this.txtDelayEachStep.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(8, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "RetrySubmitCount";
            // 
            // txtDelaySubmit
            // 
            this.txtDelaySubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDelaySubmit.Location = new System.Drawing.Point(191, 100);
            this.txtDelaySubmit.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtDelaySubmit.Name = "txtDelaySubmit";
            this.txtDelaySubmit.Size = new System.Drawing.Size(83, 26);
            this.txtDelaySubmit.TabIndex = 2;
            // 
            // txtRetrySubmitCount
            // 
            this.txtRetrySubmitCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtRetrySubmitCount.Location = new System.Drawing.Point(191, 137);
            this.txtRetrySubmitCount.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtRetrySubmitCount.Name = "txtRetrySubmitCount";
            this.txtRetrySubmitCount.Size = new System.Drawing.Size(83, 26);
            this.txtRetrySubmitCount.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(8, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "PrepareTime (ms)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(8, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(175, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "DecaptchaTimeout(ms)";
            // 
            // txtDecaptchaTimeout
            // 
            this.txtDecaptchaTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDecaptchaTimeout.Location = new System.Drawing.Point(191, 174);
            this.txtDecaptchaTimeout.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtDecaptchaTimeout.Name = "txtDecaptchaTimeout";
            this.txtDecaptchaTimeout.Size = new System.Drawing.Size(83, 26);
            this.txtDecaptchaTimeout.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDelayEachStep);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtForceSubmitDelay);
            this.groupBox1.Controls.Add(this.txtForceSubmitCount);
            this.groupBox1.Controls.Add(this.txtMaxCaptchaManual);
            this.groupBox1.Controls.Add(this.txtSessionTime);
            this.groupBox1.Controls.Add(this.txtSessionTimeout);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txtDecaptchaTimeout);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtPrepareSeconds);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtRetrySubmitCount);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtDelaySubmit);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.Location = new System.Drawing.Point(2, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 382);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // txtForceSubmitDelay
            // 
            this.txtForceSubmitDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtForceSubmitDelay.Location = new System.Drawing.Point(191, 348);
            this.txtForceSubmitDelay.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtForceSubmitDelay.Name = "txtForceSubmitDelay";
            this.txtForceSubmitDelay.Size = new System.Drawing.Size(84, 26);
            this.txtForceSubmitDelay.TabIndex = 6;
            // 
            // txtForceSubmitCount
            // 
            this.txtForceSubmitCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtForceSubmitCount.Location = new System.Drawing.Point(191, 316);
            this.txtForceSubmitCount.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtForceSubmitCount.Name = "txtForceSubmitCount";
            this.txtForceSubmitCount.Size = new System.Drawing.Size(84, 26);
            this.txtForceSubmitCount.TabIndex = 6;
            // 
            // txtMaxCaptchaManual
            // 
            this.txtMaxCaptchaManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaxCaptchaManual.Location = new System.Drawing.Point(190, 284);
            this.txtMaxCaptchaManual.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtMaxCaptchaManual.Name = "txtMaxCaptchaManual";
            this.txtMaxCaptchaManual.Size = new System.Drawing.Size(84, 26);
            this.txtMaxCaptchaManual.TabIndex = 6;
            // 
            // txtSessionTime
            // 
            this.txtSessionTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSessionTime.Location = new System.Drawing.Point(191, 248);
            this.txtSessionTime.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtSessionTime.Name = "txtSessionTime";
            this.txtSessionTime.Size = new System.Drawing.Size(84, 26);
            this.txtSessionTime.TabIndex = 6;
            // 
            // txtSessionTimeout
            // 
            this.txtSessionTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSessionTimeout.Location = new System.Drawing.Point(190, 211);
            this.txtSessionTimeout.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtSessionTimeout.Name = "txtSessionTimeout";
            this.txtSessionTimeout.Size = new System.Drawing.Size(84, 26);
            this.txtSessionTimeout.TabIndex = 5;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label18.Location = new System.Drawing.Point(7, 350);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(148, 20);
            this.label18.TabIndex = 0;
            this.label18.Text = "Force Submit Delay";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label17.Location = new System.Drawing.Point(7, 318);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(186, 20);
            this.label17.TabIndex = 0;
            this.label17.Text = "Force Submit Count (ms)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label16.Location = new System.Drawing.Point(7, 286);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(150, 20);
            this.label16.TabIndex = 0;
            this.label16.Text = "MaxManualCaptcha";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label15.Location = new System.Drawing.Point(8, 250);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(127, 20);
            this.label15.TabIndex = 0;
            this.label15.Text = "SessionTime (m)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label14.Location = new System.Drawing.Point(8, 213);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(150, 20);
            this.label14.TabIndex = 0;
            this.label14.Text = "SessionTimeout (m)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtPassword0);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtUsername0);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox2.Location = new System.Drawing.Point(297, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 91);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "www.deathbycaptcha.com";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtPassword1);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtUsername1);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox3.Location = new System.Drawing.Point(297, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(243, 91);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "de-captcher.com";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(6, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Username";
            // 
            // txtPassword1
            // 
            this.txtPassword1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtPassword1.Location = new System.Drawing.Point(99, 59);
            this.txtPassword1.Name = "txtPassword1";
            this.txtPassword1.Size = new System.Drawing.Size(138, 26);
            this.txtPassword1.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label9.Location = new System.Drawing.Point(6, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "Password";
            // 
            // txtUsername1
            // 
            this.txtUsername1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtUsername1.Location = new System.Drawing.Point(99, 25);
            this.txtUsername1.Name = "txtUsername1";
            this.txtUsername1.Size = new System.Drawing.Size(138, 26);
            this.txtUsername1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txtPassword2);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.txtUsername2);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox4.Location = new System.Drawing.Point(297, 200);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(243, 91);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "www.imagetyperz.com";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label10.Location = new System.Drawing.Point(6, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "Username";
            // 
            // txtPassword2
            // 
            this.txtPassword2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtPassword2.Location = new System.Drawing.Point(99, 59);
            this.txtPassword2.Name = "txtPassword2";
            this.txtPassword2.Size = new System.Drawing.Size(138, 26);
            this.txtPassword2.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label11.Location = new System.Drawing.Point(6, 62);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "Password";
            // 
            // txtUsername2
            // 
            this.txtUsername2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtUsername2.Location = new System.Drawing.Point(99, 25);
            this.txtUsername2.Name = "txtUsername2";
            this.txtUsername2.Size = new System.Drawing.Size(138, 26);
            this.txtUsername2.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.txtPassword3);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.txtUsername3);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox5.Location = new System.Drawing.Point(297, 297);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(243, 91);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "www.shanibpo.com";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label12.Location = new System.Drawing.Point(6, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "Username";
            // 
            // txtPassword3
            // 
            this.txtPassword3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtPassword3.Location = new System.Drawing.Point(99, 53);
            this.txtPassword3.Name = "txtPassword3";
            this.txtPassword3.Size = new System.Drawing.Size(138, 26);
            this.txtPassword3.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label13.Location = new System.Drawing.Point(6, 56);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "Password";
            // 
            // txtUsername3
            // 
            this.txtUsername3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtUsername3.Location = new System.Drawing.Point(99, 21);
            this.txtUsername3.Name = "txtUsername3";
            this.txtUsername3.Size = new System.Drawing.Size(138, 26);
            this.txtUsername3.TabIndex = 0;
            // 
            // FrmSetting
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(548, 451);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.FrmSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPrepareSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelayEachStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelaySubmit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRetrySubmitCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecaptchaTimeout)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtForceSubmitDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtForceSubmitCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxCaptchaManual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSessionTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSessionTimeout)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsername0;
        private System.Windows.Forms.NumericUpDown txtPrepareSeconds;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword0;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtDelayEachStep;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown txtDelaySubmit;
        private System.Windows.Forms.NumericUpDown txtRetrySubmitCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown txtDecaptchaTimeout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPassword1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUsername1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPassword2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtUsername2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPassword3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtUsername3;
        private System.Windows.Forms.NumericUpDown txtSessionTimeout;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown txtSessionTime;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown txtMaxCaptchaManual;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown txtForceSubmitDelay;
        private System.Windows.Forms.NumericUpDown txtForceSubmitCount;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
    }
}
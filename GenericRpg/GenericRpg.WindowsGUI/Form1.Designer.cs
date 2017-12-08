namespace GenericRpg.WindowsGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnGenerateUniverse = new System.Windows.Forms.Button();
            this.txtEntitiesNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDraw = new System.Windows.Forms.Button();
            this.txtTimerInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStopTimer = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAlvie = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDead = new System.Windows.Forms.Label();
            this.lblGenratingLabelOnly = new System.Windows.Forms.Label();
            this.lblGenerating = new System.Windows.Forms.Label();
            this.cbShowAttacks = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblUniversalTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblUTUSecond = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGenerateUniverse
            // 
            this.btnGenerateUniverse.Location = new System.Drawing.Point(1067, 264);
            this.btnGenerateUniverse.Name = "btnGenerateUniverse";
            this.btnGenerateUniverse.Size = new System.Drawing.Size(123, 37);
            this.btnGenerateUniverse.TabIndex = 1;
            this.btnGenerateUniverse.Text = "Generate Universe";
            this.btnGenerateUniverse.UseVisualStyleBackColor = true;
            this.btnGenerateUniverse.Click += new System.EventHandler(this.btnGenerateUniverse_Click);
            // 
            // txtEntitiesNumber
            // 
            this.txtEntitiesNumber.Location = new System.Drawing.Point(1067, 228);
            this.txtEntitiesNumber.Name = "txtEntitiesNumber";
            this.txtEntitiesNumber.Size = new System.Drawing.Size(100, 20);
            this.txtEntitiesNumber.TabIndex = 2;
            this.txtEntitiesNumber.Text = "100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1064, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Number of entities to create (1600 is a meaningful limit)";
            this.label1.Click += new System.EventHandler(this.uselessmethod1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(936, 581);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Simple world representation";
            this.label2.Click += new System.EventHandler(this.uslessMethod2);
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(968, 479);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(123, 37);
            this.btnDraw.TabIndex = 6;
            this.btnDraw.Text = "Action!";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // txtTimerInterval
            // 
            this.txtTimerInterval.Location = new System.Drawing.Point(968, 441);
            this.txtTimerInterval.Name = "txtTimerInterval";
            this.txtTimerInterval.Size = new System.Drawing.Size(100, 20);
            this.txtTimerInterval.TabIndex = 7;
            this.txtTimerInterval.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(965, 425);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Timer Interval";
            // 
            // btnStopTimer
            // 
            this.btnStopTimer.Location = new System.Drawing.Point(1097, 479);
            this.btnStopTimer.Name = "btnStopTimer";
            this.btnStopTimer.Size = new System.Drawing.Size(123, 37);
            this.btnStopTimer.TabIndex = 9;
            this.btnStopTimer.Text = "Stop!";
            this.btnStopTimer.UseVisualStyleBackColor = true;
            this.btnStopTimer.Click += new System.EventHandler(this.btnStopTimer_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(965, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Alive";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // lblAlvie
            // 
            this.lblAlvie.AutoSize = true;
            this.lblAlvie.Location = new System.Drawing.Point(973, 59);
            this.lblAlvie.Name = "lblAlvie";
            this.lblAlvie.Size = new System.Drawing.Size(22, 13);
            this.lblAlvie.TabIndex = 11;
            this.lblAlvie.Text = "- - -";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(965, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Dead";
            // 
            // lblDead
            // 
            this.lblDead.AutoSize = true;
            this.lblDead.Location = new System.Drawing.Point(973, 111);
            this.lblDead.Name = "lblDead";
            this.lblDead.Size = new System.Drawing.Size(22, 13);
            this.lblDead.TabIndex = 13;
            this.lblDead.Text = "- - -";
            // 
            // lblGenratingLabelOnly
            // 
            this.lblGenratingLabelOnly.AutoSize = true;
            this.lblGenratingLabelOnly.Location = new System.Drawing.Point(1234, 228);
            this.lblGenratingLabelOnly.Name = "lblGenratingLabelOnly";
            this.lblGenratingLabelOnly.Size = new System.Drawing.Size(71, 13);
            this.lblGenratingLabelOnly.TabIndex = 14;
            this.lblGenratingLabelOnly.Text = "Generating!!!!";
            // 
            // lblGenerating
            // 
            this.lblGenerating.AutoSize = true;
            this.lblGenerating.Location = new System.Drawing.Point(1245, 251);
            this.lblGenerating.Name = "lblGenerating";
            this.lblGenerating.Size = new System.Drawing.Size(24, 13);
            this.lblGenerating.TabIndex = 15;
            this.lblGenerating.Text = "--/--";
            // 
            // cbShowAttacks
            // 
            this.cbShowAttacks.AutoSize = true;
            this.cbShowAttacks.Location = new System.Drawing.Point(1109, 361);
            this.cbShowAttacks.Name = "cbShowAttacks";
            this.cbShowAttacks.Size = new System.Drawing.Size(92, 17);
            this.cbShowAttacks.TabIndex = 16;
            this.cbShowAttacks.Text = "Show Attacks";
            this.cbShowAttacks.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1064, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Universal Time Units";
            // 
            // lblUniversalTime
            // 
            this.lblUniversalTime.AutoSize = true;
            this.lblUniversalTime.Location = new System.Drawing.Point(1067, 59);
            this.lblUniversalTime.Name = "lblUniversalTime";
            this.lblUniversalTime.Size = new System.Drawing.Size(13, 13);
            this.lblUniversalTime.TabIndex = 18;
            this.lblUniversalTime.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1064, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "UTU / Second";
            // 
            // lblUTUSecond
            // 
            this.lblUTUSecond.AutoSize = true;
            this.lblUTUSecond.Location = new System.Drawing.Point(1067, 111);
            this.lblUTUSecond.Name = "lblUTUSecond";
            this.lblUTUSecond.Size = new System.Drawing.Size(23, 13);
            this.lblUTUSecond.TabIndex = 20;
            this.lblUTUSecond.Text = "0/s";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1406, 581);
            this.Controls.Add(this.lblUTUSecond);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblUniversalTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbShowAttacks);
            this.Controls.Add(this.lblGenerating);
            this.Controls.Add(this.lblGenratingLabelOnly);
            this.Controls.Add(this.lblDead);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblAlvie);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnStopTimer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTimerInterval);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEntitiesNumber);
            this.Controls.Add(this.btnGenerateUniverse);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGenerateUniverse;
        private System.Windows.Forms.TextBox txtEntitiesNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.TextBox txtTimerInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStopTimer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAlvie;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDead;
        private System.Windows.Forms.Label lblGenratingLabelOnly;
        private System.Windows.Forms.Label lblGenerating;
        private System.Windows.Forms.CheckBox cbShowAttacks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblUniversalTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblUTUSecond;
    }
}


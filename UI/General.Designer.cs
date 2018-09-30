namespace UI
{
    partial class General
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
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnCreater = new System.Windows.Forms.Button();
            this.grCreate = new System.Windows.Forms.GroupBox();
            this.nmCreater = new System.Windows.Forms.NumericUpDown();
            this.lblTime = new System.Windows.Forms.Label();
            this.grImport = new System.Windows.Forms.GroupBox();
            this.grCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmCreater)).BeginInit();
            this.grImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(234, 12);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(852, 497);
            this.txtResult.TabIndex = 12;
            this.txtResult.WordWrap = false;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(6, 19);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(163, 23);
            this.btnRun.TabIndex = 13;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(23, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(177, 23);
            this.btnLoad.TabIndex = 14;
            this.btnLoad.Text = "Load Classes and Rules";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnCreater
            // 
            this.btnCreater.Location = new System.Drawing.Point(96, 17);
            this.btnCreater.Name = "btnCreater";
            this.btnCreater.Size = new System.Drawing.Size(73, 23);
            this.btnCreater.TabIndex = 15;
            this.btnCreater.Text = "Create Classes";
            this.btnCreater.UseVisualStyleBackColor = true;
            this.btnCreater.Click += new System.EventHandler(this.btnCreater_Click);
            // 
            // grCreate
            // 
            this.grCreate.Controls.Add(this.nmCreater);
            this.grCreate.Controls.Add(this.btnCreater);
            this.grCreate.Enabled = false;
            this.grCreate.Location = new System.Drawing.Point(23, 53);
            this.grCreate.Name = "grCreate";
            this.grCreate.Size = new System.Drawing.Size(177, 46);
            this.grCreate.TabIndex = 16;
            this.grCreate.TabStop = false;
            this.grCreate.Text = "Generator";
            // 
            // nmCreater
            // 
            this.nmCreater.Location = new System.Drawing.Point(6, 17);
            this.nmCreater.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nmCreater.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmCreater.Name = "nmCreater";
            this.nmCreater.Size = new System.Drawing.Size(73, 20);
            this.nmCreater.TabIndex = 16;
            this.nmCreater.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(20, 195);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(10, 13);
            this.lblTime.TabIndex = 17;
            this.lblTime.Text = " ";
            // 
            // grImport
            // 
            this.grImport.Controls.Add(this.btnRun);
            this.grImport.Enabled = false;
            this.grImport.Location = new System.Drawing.Point(23, 128);
            this.grImport.Name = "grImport";
            this.grImport.Size = new System.Drawing.Size(177, 46);
            this.grImport.TabIndex = 19;
            this.grImport.TabStop = false;
            this.grImport.Text = "Import";
            // 
            // General
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 521);
            this.Controls.Add(this.grImport);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.grCreate);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtResult);
            this.Name = "General";
            this.Text = "Class Engine";
            this.Load += new System.EventHandler(this.General_Load);
            this.grCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmCreater)).EndInit();
            this.grImport.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnCreater;
        private System.Windows.Forms.GroupBox grCreate;
        private System.Windows.Forms.NumericUpDown nmCreater;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.GroupBox grImport;
    }
}


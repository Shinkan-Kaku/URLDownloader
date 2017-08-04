namespace URLDownloader
{
    partial class URLSDownloader
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(URLSDownloader));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Address = new System.Windows.Forms.Button();
            this.FPUrlTtBox = new System.Windows.Forms.TextBox();
            this.TitlettBox = new System.Windows.Forms.TextBox();
            this.FpageLabel = new System.Windows.Forms.Label();
            this.SNameLabel = new System.Windows.Forms.Label();
            this.dlRuleCBox = new System.Windows.Forms.ComboBox();
            this.ModeCB = new System.Windows.Forms.ComboBox();
            this.rTBox = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DldataView = new System.Windows.Forms.DataGridView();
            this.Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BnAddUrlsLog = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.initButton = new System.Windows.Forms.Button();
            this.FBDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.TitleCdtCB = new System.Windows.Forms.CheckBox();
            this.FPUrlCdtCB = new System.Windows.Forms.CheckBox();
            this.DlPathCdtCB = new System.Windows.Forms.CheckBox();
            this.ListvcdtCB = new System.Windows.Forms.CheckBox();
            this.Vlabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DldataView)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Address);
            this.panel1.Controls.Add(this.FPUrlTtBox);
            this.panel1.Controls.Add(this.TitlettBox);
            this.panel1.Controls.Add(this.FpageLabel);
            this.panel1.Controls.Add(this.SNameLabel);
            this.panel1.Controls.Add(this.dlRuleCBox);
            this.panel1.Controls.Add(this.ModeCB);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // Address
            // 
            resources.ApplyResources(this.Address, "Address");
            this.Address.Name = "Address";
            this.Address.UseVisualStyleBackColor = true;
            this.Address.Click += new System.EventHandler(this.Address_Click);
            // 
            // FPUrlTtBox
            // 
            resources.ApplyResources(this.FPUrlTtBox, "FPUrlTtBox");
            this.FPUrlTtBox.Name = "FPUrlTtBox";
            this.FPUrlTtBox.TextChanged += new System.EventHandler(this.FPUrlTtBox_TextChanged);
            // 
            // TitlettBox
            // 
            resources.ApplyResources(this.TitlettBox, "TitlettBox");
            this.TitlettBox.Name = "TitlettBox";
            this.TitlettBox.TextChanged += new System.EventHandler(this.TitlettBox_TextChanged);
            // 
            // FpageLabel
            // 
            resources.ApplyResources(this.FpageLabel, "FpageLabel");
            this.FpageLabel.Name = "FpageLabel";
            // 
            // SNameLabel
            // 
            resources.ApplyResources(this.SNameLabel, "SNameLabel");
            this.SNameLabel.Name = "SNameLabel";
            // 
            // dlRuleCBox
            // 
            resources.ApplyResources(this.dlRuleCBox, "dlRuleCBox");
            this.dlRuleCBox.FormattingEnabled = true;
            this.dlRuleCBox.Items.AddRange(new object[] {
            resources.GetString("dlRuleCBox.Items"),
            resources.GetString("dlRuleCBox.Items1")});
            this.dlRuleCBox.Name = "dlRuleCBox";
            this.dlRuleCBox.SelectedIndexChanged += new System.EventHandler(this.dlRuleCBox_SelectedIndexChanged);
            // 
            // ModeCB
            // 
            this.ModeCB.FormattingEnabled = true;
            this.ModeCB.Items.AddRange(new object[] {
            resources.GetString("ModeCB.Items"),
            resources.GetString("ModeCB.Items1")});
            resources.ApplyResources(this.ModeCB, "ModeCB");
            this.ModeCB.Name = "ModeCB";
            this.ModeCB.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // rTBox
            // 
            resources.ApplyResources(this.rTBox, "rTBox");
            this.rTBox.Name = "rTBox";
            this.rTBox.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DldataView);
            this.panel2.Controls.Add(this.panel3);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // DldataView
            // 
            this.DldataView.AllowUserToAddRows = false;
            this.DldataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DldataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Num,
            this.Url});
            resources.ApplyResources(this.DldataView, "DldataView");
            this.DldataView.Name = "DldataView";
            this.DldataView.RowTemplate.Height = 24;
            // 
            // Num
            // 
            resources.ApplyResources(this.Num, "Num");
            this.Num.Name = "Num";
            this.Num.ReadOnly = true;
            this.Num.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Url
            // 
            resources.ApplyResources(this.Url, "Url");
            this.Url.Name = "Url";
            this.Url.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BnAddUrlsLog);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // BnAddUrlsLog
            // 
            resources.ApplyResources(this.BnAddUrlsLog, "BnAddUrlsLog");
            this.BnAddUrlsLog.Name = "BnAddUrlsLog";
            this.BnAddUrlsLog.UseVisualStyleBackColor = true;
            this.BnAddUrlsLog.Click += new System.EventHandler(this.addingURLsLog_Click);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Step = 1;
            // 
            // initButton
            // 
            resources.ApplyResources(this.initButton, "initButton");
            this.initButton.Name = "initButton";
            this.initButton.UseVisualStyleBackColor = true;
            this.initButton.Click += new System.EventHandler(this.initButton_Click);
            // 
            // TitleCdtCB
            // 
            resources.ApplyResources(this.TitleCdtCB, "TitleCdtCB");
            this.TitleCdtCB.Name = "TitleCdtCB";
            this.TitleCdtCB.UseVisualStyleBackColor = true;
            // 
            // FPUrlCdtCB
            // 
            resources.ApplyResources(this.FPUrlCdtCB, "FPUrlCdtCB");
            this.FPUrlCdtCB.Name = "FPUrlCdtCB";
            this.FPUrlCdtCB.UseVisualStyleBackColor = true;
            // 
            // DlPathCdtCB
            // 
            resources.ApplyResources(this.DlPathCdtCB, "DlPathCdtCB");
            this.DlPathCdtCB.Name = "DlPathCdtCB";
            this.DlPathCdtCB.UseVisualStyleBackColor = true;
            // 
            // ListvcdtCB
            // 
            resources.ApplyResources(this.ListvcdtCB, "ListvcdtCB");
            this.ListvcdtCB.Name = "ListvcdtCB";
            this.ListvcdtCB.UseVisualStyleBackColor = true;
            // 
            // Vlabel
            // 
            resources.ApplyResources(this.Vlabel, "Vlabel");
            this.Vlabel.Name = "Vlabel";
            // 
            // URLSDownloader
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Vlabel);
            this.Controls.Add(this.ListvcdtCB);
            this.Controls.Add(this.DlPathCdtCB);
            this.Controls.Add(this.FPUrlCdtCB);
            this.Controls.Add(this.TitleCdtCB);
            this.Controls.Add(this.initButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.rTBox);
            this.Controls.Add(this.panel1);
            this.Name = "URLSDownloader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.URLSDownloader_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DldataView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rTBox;
        private System.Windows.Forms.Button Address;
        private System.Windows.Forms.TextBox FPUrlTtBox;
        private System.Windows.Forms.TextBox TitlettBox;
        private System.Windows.Forms.Label FpageLabel;
        private System.Windows.Forms.Label SNameLabel;
        private System.Windows.Forms.ComboBox dlRuleCBox;
        private System.Windows.Forms.ComboBox ModeCB;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button initButton;
        private System.Windows.Forms.FolderBrowserDialog FBDialog1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BnAddUrlsLog;
        private System.Windows.Forms.CheckBox DlPathCdtCB;
        private System.Windows.Forms.CheckBox FPUrlCdtCB;
        private System.Windows.Forms.CheckBox TitleCdtCB;
        private System.Windows.Forms.DataGridView DldataView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn Url;
        private System.Windows.Forms.CheckBox ListvcdtCB;
        private System.Windows.Forms.Label Vlabel;
    }
}


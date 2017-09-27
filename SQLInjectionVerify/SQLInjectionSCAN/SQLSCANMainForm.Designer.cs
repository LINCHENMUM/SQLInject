namespace SQLInjectionSCAN
{
    partial class SQLSCANMainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("");
            this.lblSelectFile = new System.Windows.Forms.Label();
            this.ScanSubmit = new System.Windows.Forms.Button();
            this.lblScanResult = new System.Windows.Forms.Label();
            this.txtBrowseFiles = new System.Windows.Forms.TextBox();
            this.btnAST = new System.Windows.Forms.Button();
            this.listViewScanResult = new System.Windows.Forms.ListView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTimeSpend = new System.Windows.Forms.Label();
            this.lblProgressStatus = new System.Windows.Forms.Label();
            this.lblTimeAmount = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblFileAmount = new System.Windows.Forms.Label();
            this.lblDefect = new System.Windows.Forms.Label();
            this.lblDefectAmount = new System.Windows.Forms.Label();
            this.btnFile = new System.Windows.Forms.Button();
            this.btnRule = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSelectFile
            // 
            this.lblSelectFile.AutoSize = true;
            this.lblSelectFile.Location = new System.Drawing.Point(28, 57);
            this.lblSelectFile.Name = "lblSelectFile";
            this.lblSelectFile.Size = new System.Drawing.Size(135, 15);
            this.lblSelectFile.TabIndex = 0;
            this.lblSelectFile.Text = "请选择文件或路径:";
            // 
            // ScanSubmit
            // 
            this.ScanSubmit.Location = new System.Drawing.Point(1160, 92);
            this.ScanSubmit.Name = "ScanSubmit";
            this.ScanSubmit.Size = new System.Drawing.Size(92, 23);
            this.ScanSubmit.TabIndex = 1;
            this.ScanSubmit.Text = "开始检测";
            this.ScanSubmit.UseVisualStyleBackColor = true;
            this.ScanSubmit.Click += new System.EventHandler(this.ScanSubmit_Click);
            // 
            // lblScanResult
            // 
            this.lblScanResult.AutoSize = true;
            this.lblScanResult.Location = new System.Drawing.Point(34, 183);
            this.lblScanResult.Name = "lblScanResult";
            this.lblScanResult.Size = new System.Drawing.Size(82, 15);
            this.lblScanResult.TabIndex = 5;
            this.lblScanResult.Text = "检测结果：";
            // 
            // txtBrowseFiles
            // 
            this.txtBrowseFiles.Location = new System.Drawing.Point(161, 47);
            this.txtBrowseFiles.Name = "txtBrowseFiles";
            this.txtBrowseFiles.Size = new System.Drawing.Size(806, 25);
            this.txtBrowseFiles.TabIndex = 6;
            // 
            // btnAST
            // 
            this.btnAST.Location = new System.Drawing.Point(1160, 53);
            this.btnAST.Name = "btnAST";
            this.btnAST.Size = new System.Drawing.Size(92, 23);
            this.btnAST.TabIndex = 7;
            this.btnAST.Text = "抽象语法树";
            this.btnAST.UseVisualStyleBackColor = true;
            this.btnAST.Click += new System.EventHandler(this.btnAST_Click);
            // 
            // listViewScanResult
            // 
            this.listViewScanResult.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10});
            this.listViewScanResult.Location = new System.Drawing.Point(31, 201);
            this.listViewScanResult.Name = "listViewScanResult";
            this.listViewScanResult.Size = new System.Drawing.Size(1221, 473);
            this.listViewScanResult.TabIndex = 8;
            this.listViewScanResult.UseCompatibleStateImageBehavior = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(915, 183);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 15);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "状态：";
            // 
            // lblTimeSpend
            // 
            this.lblTimeSpend.AutoSize = true;
            this.lblTimeSpend.Location = new System.Drawing.Point(1097, 183);
            this.lblTimeSpend.Name = "lblTimeSpend";
            this.lblTimeSpend.Size = new System.Drawing.Size(52, 15);
            this.lblTimeSpend.TabIndex = 11;
            this.lblTimeSpend.Text = "耗时：";
            // 
            // lblProgressStatus
            // 
            this.lblProgressStatus.AutoSize = true;
            this.lblProgressStatus.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblProgressStatus.Location = new System.Drawing.Point(962, 183);
            this.lblProgressStatus.Name = "lblProgressStatus";
            this.lblProgressStatus.Size = new System.Drawing.Size(87, 15);
            this.lblProgressStatus.TabIndex = 12;
            this.lblProgressStatus.Text = "ScanStatus";
            // 
            // lblTimeAmount
            // 
            this.lblTimeAmount.AutoSize = true;
            this.lblTimeAmount.Location = new System.Drawing.Point(1143, 182);
            this.lblTimeAmount.Name = "lblTimeAmount";
            this.lblTimeAmount.Size = new System.Drawing.Size(79, 15);
            this.lblTimeAmount.TabIndex = 13;
            this.lblTimeAmount.Text = "TimeSpend";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1160, 131);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(92, 23);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "退 出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(492, 182);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(97, 15);
            this.lblFile.TabIndex = 15;
            this.lblFile.Text = "检测文件数：";
            // 
            // lblFileAmount
            // 
            this.lblFileAmount.AutoSize = true;
            this.lblFileAmount.Location = new System.Drawing.Point(582, 183);
            this.lblFileAmount.Name = "lblFileAmount";
            this.lblFileAmount.Size = new System.Drawing.Size(52, 15);
            this.lblFileAmount.TabIndex = 16;
            this.lblFileAmount.Text = "文件数";
            // 
            // lblDefect
            // 
            this.lblDefect.AutoSize = true;
            this.lblDefect.Location = new System.Drawing.Point(681, 183);
            this.lblDefect.Name = "lblDefect";
            this.lblDefect.Size = new System.Drawing.Size(136, 15);
            this.lblDefect.TabIndex = 17;
            this.lblDefect.Text = "SQL注入漏洞个数：";
            // 
            // lblDefectAmount
            // 
            this.lblDefectAmount.AutoSize = true;
            this.lblDefectAmount.Location = new System.Drawing.Point(809, 183);
            this.lblDefectAmount.Name = "lblDefectAmount";
            this.lblDefectAmount.Size = new System.Drawing.Size(67, 15);
            this.lblDefectAmount.TabIndex = 18;
            this.lblDefectAmount.Text = "漏洞个数";
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(986, 47);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(75, 25);
            this.btnFile.TabIndex = 20;
            this.btnFile.Text = "浏 览";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // btnRule
            // 
            this.btnRule.Location = new System.Drawing.Point(1160, 12);
            this.btnRule.Name = "btnRule";
            this.btnRule.Size = new System.Drawing.Size(92, 23);
            this.btnRule.TabIndex = 21;
            this.btnRule.Text = "规 则";
            this.btnRule.UseVisualStyleBackColor = true;
            this.btnRule.Click += new System.EventHandler(this.btnRule_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(986, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "考 试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SQLSCANMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 698);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRule);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.lblDefectAmount);
            this.Controls.Add(this.lblDefect);
            this.Controls.Add(this.lblFileAmount);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblTimeAmount);
            this.Controls.Add(this.lblProgressStatus);
            this.Controls.Add(this.lblTimeSpend);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.listViewScanResult);
            this.Controls.Add(this.btnAST);
            this.Controls.Add(this.txtBrowseFiles);
            this.Controls.Add(this.lblScanResult);
            this.Controls.Add(this.ScanSubmit);
            this.Controls.Add(this.lblSelectFile);
            this.Name = "SQLSCANMainForm";
            this.Text = "C# SQL注入漏洞静态扫描窗口";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelectFile;
        private System.Windows.Forms.Button ScanSubmit;
        private System.Windows.Forms.Label lblScanResult;
        private System.Windows.Forms.TextBox txtBrowseFiles;
        private System.Windows.Forms.Button btnAST;
        private System.Windows.Forms.ListView listViewScanResult;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTimeSpend;
        private System.Windows.Forms.Label lblProgressStatus;
        private System.Windows.Forms.Label lblTimeAmount;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lblFileAmount;
        private System.Windows.Forms.Label lblDefect;
        private System.Windows.Forms.Label lblDefectAmount;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Button btnRule;
        private System.Windows.Forms.Button button1;

        //FolderDialog openFolder = new FolderDialog();
        //if (openFolder.DisplayDialog()==DialogResult.OK) 
        //txtBrowseFiles.Text=openFolder.Path.ToString(); 
        //else txtBrowseFiles.Text="你没有选择目录";

       

    }
}


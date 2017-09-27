namespace SQLInjectionSCAN
{
    partial class DataFlow
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
            this.btnExit = new System.Windows.Forms.Button();
            this.listViewDataFlow = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "析取抽象语法树的结果";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1059, 565);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "退 出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // listViewDataFlow
            // 
            this.listViewDataFlow.Location = new System.Drawing.Point(16, 43);
            this.listViewDataFlow.Name = "listViewDataFlow";
            this.listViewDataFlow.Size = new System.Drawing.Size(1118, 504);
            this.listViewDataFlow.TabIndex = 3;
            this.listViewDataFlow.UseCompatibleStateImageBehavior = false;
            // 
            // DataFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 600);
            this.Controls.Add(this.listViewDataFlow);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label1);
            this.Name = "DataFlow";
            this.Text = "数据流树";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListView listViewDataFlow;
    }
}
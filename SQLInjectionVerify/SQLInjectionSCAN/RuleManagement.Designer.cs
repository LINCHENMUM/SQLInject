namespace SQLInjectionSCAN
{
    partial class RuleManagement
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
            this.listViewRule = new System.Windows.Forms.ListView();
            this.lblRuleManagement = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewRule
            // 
            this.listViewRule.LabelEdit = true;
            this.listViewRule.Location = new System.Drawing.Point(16, 41);
            this.listViewRule.Name = "listViewRule";
            this.listViewRule.Size = new System.Drawing.Size(827, 400);
            this.listViewRule.TabIndex = 0;
            this.listViewRule.UseCompatibleStateImageBehavior = false;
            // 
            // lblRuleManagement
            // 
            this.lblRuleManagement.AutoSize = true;
            this.lblRuleManagement.Location = new System.Drawing.Point(13, 18);
            this.lblRuleManagement.Name = "lblRuleManagement";
            this.lblRuleManagement.Size = new System.Drawing.Size(67, 15);
            this.lblRuleManagement.TabIndex = 1;
            this.lblRuleManagement.Text = "检测规则";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(657, 456);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存修改";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(768, 456);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "退 出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // RuleManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 491);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblRuleManagement);
            this.Controls.Add(this.listViewRule);
            this.Name = "RuleManagement";
            this.Text = "规则管理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewRule;
        private System.Windows.Forms.Label lblRuleManagement;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
    }
}
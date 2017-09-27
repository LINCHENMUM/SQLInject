using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLInjectionSCAN
{
    public partial class RuleManagement : Form
    {
        public RuleManagement()
        {
            InitializeComponent();

            #region 显示规则
            //定义listView
            listViewRule.Clear();
            listViewRule.GridLines = true;//显示各个记录的分隔线 
            listViewRule.View = View.Details;//定义列表显示的方式 
            listViewRule.Scrollable = true; //需要时候显示滚动条
            if (listViewRule.Columns.Count == 0)
            {
                listViewRule.Columns.Add("规则", 350);
                listViewRule.Columns.Add("规则类型", 250);
                listViewRule.Columns.Add("规则安全类型", 230);
            }

            //获取规则数据

            Service.BLL.SQL_RULE_STATEMENTBo ruleBo = new Service.BLL.SQL_RULE_STATEMENTBo();
            DataSet ruleDS = ruleBo.SelectAllRule();

            if (ruleDS.Tables[0].Rows.Count > 0)
            {

                for (int r = 0; r < ruleDS.Tables[0].Rows.Count; r++)
                {
                    ListViewItem ltResult = new ListViewItem();
                    ltResult.ImageIndex = r;
                    ltResult.SubItems[0].Text = ruleDS.Tables[0].Rows[r]["STATEMENT"].ToString();
                    ltResult.SubItems.Add(ruleDS.Tables[0].Rows[r]["STATEMENTTYPE"].ToString());
                    ltResult.SubItems.Add(ruleDS.Tables[0].Rows[r]["SECURITYTYPE"].ToString());
                    listViewRule.Items.Add(ltResult);

                }// for (int r = 0; r < ruleDS.Tables[0].Rows.Count; r++)
            }// if (ruleDS.Tables[0].Rows.Count > 0)
            #endregion

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}

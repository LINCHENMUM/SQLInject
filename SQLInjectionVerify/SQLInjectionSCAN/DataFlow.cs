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
    public partial class DataFlow : Form
    {
        public DataFlow()
        {
            InitializeComponent();
            #region 显示规则
            //定义listView
            listViewDataFlow.Clear();
            listViewDataFlow.GridLines = true;//显示各个记录的分隔线 
            listViewDataFlow.View = View.Details;//定义列表显示的方式 
            listViewDataFlow.Scrollable = true; //需要时候显示滚动条
            if (listViewDataFlow.Columns.Count == 0)
            {
                listViewDataFlow.Columns.Add("目标操作数", 350);
                listViewDataFlow.Columns.Add("操作符", 150);
                listViewDataFlow.Columns.Add("源操作数", 230);
                listViewDataFlow.Columns.Add("行号", 50);
                listViewDataFlow.Columns.Add("文件名", 230);
            }

            //获取规则数据

            Service.BLL.SQL_DATAFLOWBo dataFlowBo = new Service.BLL.SQL_DATAFLOWBo();
            DataSet dataFlowDS = dataFlowBo.SelectAllAST();

            if (dataFlowDS.Tables[0].Rows.Count > 0)
            {

                for (int r = 0; r < dataFlowDS.Tables[0].Rows.Count; r++)
                {
                    ListViewItem ltResult = new ListViewItem();
                    ltResult.ImageIndex = r;
                    ltResult.SubItems[0].Text = dataFlowDS.Tables[0].Rows[r]["DESTINATIONOPERAND"].ToString();
                    ltResult.SubItems.Add(dataFlowDS.Tables[0].Rows[r]["OPCODE"].ToString());
                    ltResult.SubItems.Add(dataFlowDS.Tables[0].Rows[r]["SOURCEOPERAND"].ToString());
                    ltResult.SubItems.Add(dataFlowDS.Tables[0].Rows[r]["CODELINENO"].ToString());
                    ltResult.SubItems.Add(dataFlowDS.Tables[0].Rows[r]["FILENAME"].ToString());
                    listViewDataFlow.Items.Add(ltResult);

                }// for (int r = 0; r < ruleDS.Tables[0].Rows.Count; r++)
            }// if (ruleDS.Tables[0].Rows.Count > 0)
            #endregion

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}

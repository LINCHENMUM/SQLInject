using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;


using Mono.Cecil;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Demo;
using System.Reflection;

namespace SQLInjectionSCAN
{
    public partial class SQLSCANMainForm : Form
    {
        public SQLSCANMainForm()
        {
            InitializeComponent();
            lblProgressStatus.Text = "";
            lblTimeAmount.Text = "";
            lblFileAmount.Text = "";
            lblDefectAmount.Text = "";
        }

        public string rtScanResult = string.Empty;
        public string rtDataFlow = string.Empty;
        public string rtFileName = string.Empty;
        public string rtSQLinjectSource = string.Empty;
        public int rtCodelineNo;
        public int intNewMaxBatch;
        public int intMaxBatch;
        public int intFileAmount;
        public int intDefectAmount;

        public string filenamedb = string.Empty;
        public string filenameAST = string.Empty;
        
        private void ScanSubmit_Click(object sender, EventArgs e)
        {
            #region 开始准备
            //开始计时
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            lblProgressStatus.Text="检测中...";
            lblTimeAmount.Text = "";
            lblFileAmount.Text = "";
            lblDefectAmount.Text = "";
            intFileAmount = 0;
            intDefectAmount = 0;
            listViewScanResult.Clear();
            Application.DoEvents();
            #endregion
            try
            {
            #region 遍历所有的文件
              
            #region 获取本次检测的批量号
            Service.BLL.SQL_SCANRESULTBo newMaxBatchBo = new Service.BLL.SQL_SCANRESULTBo();
            DataSet newMaxBatchDS = newMaxBatchBo.SelectMaxBatch();

            if (newMaxBatchDS.Tables[0].Rows.Count > 0)
             {
                 for (int y = 0; y < newMaxBatchDS.Tables[0].Rows.Count; y++)
                  {
                      intNewMaxBatch = int.Parse(newMaxBatchDS.Tables[0].Rows[y]["BATCH"].ToString());

                  }//  for (int y = 0; y < newMaxBatchDS.Tables[0].Rows.Count; y++)
             }//  if (newMaxBatchDS.Tables[0].Rows.Count > 0)

            #endregion
            Service.BLL.SQL_DATAFLOWBo fileBo=new Service.BLL.SQL_DATAFLOWBo();
            DataSet FileDS=fileBo.SelectScanFileName();

            if (FileDS.Tables[0].Rows.Count > 0)
            {
                #region 循环遍历检测每一个文件
                for (int f = 0; f < FileDS.Tables[0].Rows.Count; f++)
                {
                    string strFileName = FileDS.Tables[0].Rows[f]["FILENAME"].ToString();
                    rtFileName = strFileName;
                    rtScanResult = "UnSafe";
                    intFileAmount += 1;
                    #region 遍历同一个文件所有的用户输入
                    //查找同一个文件里所有的用户输入
                    Service.BLL.SQL_DATAFLOWBo SourceBo = new Service.BLL.SQL_DATAFLOWBo();
                    DataSet SourceDS = SourceBo.SelectScanSource(strFileName);
                    if (SourceDS.Tables[0].Rows.Count>0)
                    {
                        //遍历用户输入的数据流树
                        for ( int k=0; k<SourceDS.Tables[0].Rows.Count;k++)
                        {
                            string strTreeName=SourceDS.Tables[0].Rows[k]["TREENAME"].ToString();
                            string strInjectSource = strTreeName;
                            string strSourceSecurity = "UnSafe";
                            string strOpcodeSecurity=string.Empty;
                            rtSQLinjectSource = strInjectSource;

                            #region 确定每个用户输入是否安全
                            Service.BLL.SQL_DATAFLOWBo treeBo = new Service.BLL.SQL_DATAFLOWBo();
                            DataSet treeDS=treeBo.SelectAllTree(strFileName,strTreeName);
                            if (treeDS.Tables[0].Rows.Count>0)
                                {
                                    
                                    #region 遍历每一个用户输入的数据传播过程
                                    for (int m=0;m<treeDS.Tables[0].Rows.Count;m++)
                                    {
                                        string strSourceOperand = treeDS.Tables[0].Rows[m]["SOURCEOPERAND"].ToString();
                                        int intCodeLineNo=int.Parse(treeDS.Tables[0].Rows[m]["CODELINENO"].ToString());
                                        string strOpcode = treeDS.Tables[0].Rows[m]["OPCODE"].ToString();
                                        string strDestinationOperand = treeDS.Tables[0].Rows[m]["DESTINATIONOPERAND"].ToString();
                                        rtCodelineNo = int.Parse(treeDS.Tables[0].Rows[0]["CODELINENO"].ToString()); ;
                                        rtDataFlow = rtDataFlow + "-->" + strDestinationOperand + "(" + "行号:" + intCodeLineNo + ")";

                                        #region 输出检测信息
                                        //lboxScanProgress.Text = "";
                                        //lboxScanProgress.Text = strDestinationOperand + strOpcode + strSourceOperand + strOpcode + strTreeName;
                                        //Application.DoEvents();
                                        ////等待一秒
                                        //System.Threading.Thread.Sleep(1000); 
                                        #endregion

                                        //检查指令是否安全
                                        #region 获取操作符的安全性

                                        #region 调试点
                                        //if (strOpcode == "Parameters.Add")
                                        //{

                                        //    string a="1";
                                        //}
                                        #endregion
                                        Service.BLL.SQL_RULE_STATEMENTBo RuleBo = new Service.BLL.SQL_RULE_STATEMENTBo();
                                        DataSet ruleDS = RuleBo.SelectOpcodeSecurity(strOpcode);
                                        if (ruleDS.Tables[0].Rows.Count > 0)
                                            {
                                                for (int j = 0; j < ruleDS.Tables[0].Rows.Count; j++)
                                                {
                                                    strOpcodeSecurity = ruleDS.Tables[0].Rows[j]["SECURITYTYPE"].ToString();

                                                }// for (int j=0;j<IRDS.Tables[0].Rows.Count;j++)
                                            }// if (IRDS.Tables[0].Rows.Count>0)
                                        #endregion
                                        #region 比较操作符和源操作数，确定输出代码是否安全
                                        if (strSourceSecurity == "UnSafe"  && strOpcodeSecurity == "UnSafe")
                                        {
                                            strSourceSecurity = "UnSafe";
                                            rtScanResult = "UnSafe";
                                        }
                                        else if (strSourceSecurity == "UnSafe" && strOpcodeSecurity == "Safe")
                                        {
                                            strSourceSecurity = "Safe";
                                            rtScanResult = "Safe";
                                        }
                                        else if (strSourceSecurity == "Unsafe" && strOpcodeSecurity == "MaybeUnSafe")
                                        {
                                            strSourceSecurity = "MaybeUnSafe";
                                            rtScanResult = "MaybeUnSafe";
                                        }
                                        else if (strSourceSecurity == "Safe" && strOpcodeSecurity == "UnSafe")
                                        {
                                            strSourceSecurity = "Safe";
                                            rtScanResult = "Safe";
                                        }
                                        else if (strSourceSecurity == "Safe" && strOpcodeSecurity == "Safe")
                                        {
                                            strSourceSecurity = "Safe";
                                            rtScanResult = "Safe";
                                        }
                                        else if (strSourceSecurity == "Safe" && strOpcodeSecurity == "MaybeUnSafe")
                                        {
                                            strSourceSecurity = "Safe";
                                            rtScanResult = "Safe";
                                        }
                                        else if (strSourceSecurity == "MaybeUnSafe" && strOpcodeSecurity == "UnSafe")
                                        {
                                            strSourceSecurity = "UnSafe";
                                            rtScanResult = "UnSafe";
                                        }
                                        else if (strSourceSecurity == "MaybeUnSafe" && strOpcodeSecurity == "Safe")
                                        {
                                            strSourceSecurity = "Safe";
                                            rtScanResult = "Safe";
                                        }
                                        else if (strSourceSecurity == "MaybeUnSafe" && strOpcodeSecurity == "MaybeUnSafe")
                                        {
                                            strSourceSecurity = "MaybeUnSafe";
                                            rtScanResult = "MaybeUnSafe";
                                        }
                                        #endregion
                                    }//for (int m=0;m<treeDS.Tables[0].Rows.Count;m++)
                                    #endregion
                                    #region 保存一个用户输入的检测结果
                                    Service.BLL.SQL_SCANRESULTBo ScanResultBO = new Service.BLL.SQL_SCANRESULTBo();
                                    Model.SQL_SCANRESULT result = new Model.SQL_SCANRESULT();
                                    result.Sqlinjectsource = rtSQLinjectSource;
                                    result.Codelineno = rtCodelineNo;
                                    result.Filename = rtFileName;
                                    result.Scanresult = rtScanResult;
                                    if (rtScanResult=="UnSafe")
                                    {
                                        intDefectAmount += 1;
                                    }
                                    result.Dataflow = rtDataFlow.Remove(0,3);
                                    result.Batch = intNewMaxBatch + 1;

                                    ScanResultBO.InsertScanResult(result);
                                    #endregion
                          }// if (treeDS.Tables[0].Rows.Count>0)
                            rtDataFlow = string.Empty;
                            rtCodelineNo = -1;
                            rtScanResult = string.Empty;
                            #endregion
                        }//for ( int k=0; k<SourceDS.Tables[0].Rows.Count;k++)
                    }// if (SourceDS.Tables[0].Rows.Count>0)
                    #endregion
                }//for (int f = 0; f < FileDS.Tables[0].Rows.Count; f++)
                #endregion
            }//  if (FileDS.Tables[0].Rows.Count > 0)
            #endregion 
            #region 输出报告
            //定义listView
            listViewScanResult.Clear();
            listViewScanResult.GridLines = true;//显示各个记录的分隔线 
            listViewScanResult.View = View.Details;//定义列表显示的方式 
            listViewScanResult.Scrollable = true; //需要时候显示滚动条
            if (listViewScanResult.Columns.Count == 0)
            {
                listViewScanResult.Columns.Add("注入源", 220);
                listViewScanResult.Columns.Add("行号", 50);
                listViewScanResult.Columns.Add("SQL注入检测结果", 130);
                listViewScanResult.Columns.Add("注入源传播路径", 480);
                listViewScanResult.Columns.Add("文件名", 180);
                listViewScanResult.Columns.Add("检测时间", 180);
            }

            //获取扫描数据

            Service.BLL.SQL_SCANRESULTBo MaxBatchBo = new Service.BLL.SQL_SCANRESULTBo();
            DataSet MaxBatchDS = MaxBatchBo.SelectMaxBatch();
            
            if (MaxBatchDS.Tables[0].Rows.Count > 0)
            {
                for (int x = 0; x < MaxBatchDS.Tables[0].Rows.Count; x++)
                {
                    intMaxBatch =int.Parse (MaxBatchDS.Tables[0].Rows[x]["BATCH"].ToString());

                }// for (int x = 0; x < MaxBatchDS.Tables[0].Rows.Count; x++)
            }//  if (MaxBatchDS.Tables[0].Rows.Count > 0)

            Service.BLL.SQL_SCANRESULTBo resultBo=new Service.BLL.SQL_SCANRESULTBo();
            DataSet resultDS = resultBo.SelectScanResultAll(intMaxBatch);

            if (resultDS.Tables[0].Rows.Count > 0)
            {

                for (int r = 0; r < resultDS.Tables[0].Rows.Count; r++)
                {
                    ListViewItem ltResult = new ListViewItem();
                    ltResult.ImageIndex = r; 
                    ltResult.SubItems[0].Text = resultDS.Tables[0].Rows[r]["SQLINJECTSOURCE"].ToString();
                    ltResult.SubItems.Add( resultDS.Tables[0].Rows[r]["CODELINENO"].ToString());
                    ltResult.SubItems.Add( resultDS.Tables[0].Rows[r]["SCANRESULT"].ToString());
                    ltResult.SubItems.Add( resultDS.Tables[0].Rows[r]["DATAFLOW"].ToString());
                    ltResult.SubItems.Add(resultDS.Tables[0].Rows[r]["FILENAME"].ToString());
                    ltResult.SubItems.Add( resultDS.Tables[0].Rows[r]["SCANDATE"].ToString());
                    listViewScanResult.Items.Add(ltResult);

                }//for (int r = 0; r < resultDS.Tables[0].Rows.Count; r++)
            }//if (resultDS.Tables[0].Rows.Count > 0)
                #endregion
            #region 检测完成
            lblFileAmount.Text = intFileAmount.ToString();
            lblDefectAmount.Text = intDefectAmount.ToString();
            lblProgressStatus.Text = "检测完成";
            stopwatch.Stop();
            lblTimeAmount.Text = (Math.Round((decimal)stopwatch.ElapsedMilliseconds / 1000, 2)).ToString() + "秒";
            #endregion

            }//try
            catch (SqlException ae)
            {
                MessageBox.Show(ae.Message.ToString());
            }
        }

        private void btnAST_Click(object sender, EventArgs e)
        {
            ICSharpCode.NRefactory.Demo.ASTree ast = new ICSharpCode.NRefactory.Demo.ASTree();
            ast.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            Stream openFileStream;
            OpenFileDialog findFileDialog = new OpenFileDialog();
            findFileDialog.Title = "Open File Dialog";
            findFileDialog.Multiselect = true;
            findFileDialog.InitialDirectory = Path.GetFullPath("./");//@"c:\";
            findFileDialog.Filter = "C# 源文件 (*.cs)|*.cs";
            findFileDialog.FilterIndex = 2;
            findFileDialog.RestoreDirectory = true;
            if (findFileDialog.ShowDialog() == DialogResult.OK)
            {
                //txtBrowseFiles.Text = findFileDialog.FileName;
                //txtBrowseFiles.Text = findFileDialog.SafeFileName;

                    if ((openFileStream = findFileDialog.OpenFile()) != null)
                    {
                        this.txtBrowseFiles.Text = "";
                        for (int fi = 0; fi < findFileDialog.FileNames.Length; fi++)
                        {
                            //this.txtBrowseFiles.Text += findFileDialog.FileNames[fi].ToString();
                            this.txtBrowseFiles.Text += findFileDialog.SafeFileName+";";
                            filenamedb = findFileDialog.SafeFileName;
                            filenameAST = findFileDialog.FileNames[fi].ToString();
                        }
                        openFileStream.Close();
                    }
            } 
        }

        private void btnRule_Click(object sender, EventArgs e)
        {
            RuleManagement rule = new RuleManagement();
            rule.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MUMExam Exam = new MUMExam();
            Exam.Show();
        }

    }
}

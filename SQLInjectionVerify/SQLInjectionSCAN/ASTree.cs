// Copyright (c) 2010-2013 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;

using Model;
using Library;
using Service;
using SQLInjectionSCAN;
using System.Data;
using System.Text.RegularExpressions;


namespace ICSharpCode.NRefactory.Demo
{
    /// <summary>
    /// Description of CSDemo.
    /// </summary>
    public partial class ASTree : Form
    {
        public string filenamedb = string.Empty;
        public string filenameAST = string.Empty;
        public string filecontent = string.Empty;
        public string ASTtext=string.Empty;

        public string secondDestinationoperand = string.Empty;
        public string firstOpcode = string.Empty;
        public string firstDestinationoperand = string.Empty;
        public string firstSourceoperand = string.Empty;
        public string traceSourceoperand=string.Empty;
        public int intLineNO;
        public string treeName = string.Empty;
        public int intTreeLevel;
        
        SyntaxTree syntaxTree;

        public ASTree()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            ASTtext = System.IO.File.ReadAllText(@"C:\\D\\SchoolTask\\2014学年第二学期\\第2版\\新思路\\SQLInjectionVerify\\Admin\\Login.aspx.cs");
            filecontent = ASTtext;
            csharpCodeTextBox.Text = filecontent;


            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                csharpCodeTextBox.SelectAll();
                CSharpParseButtonClick(null, null);
                resolveButton.UseWaitCursor = true;
                ThreadPool.QueueUserWorkItem(
                    delegate
                    {
                        builtInLibs.Value.ToString();
                        BeginInvoke(new Action(delegate { resolveButton.UseWaitCursor = false; }));
                    });
            }
        }

        // StreamReader objReader = new StreamReader("C:\\D\\SchoolTask\\2014学年第二学期\\第2版\\新思路\\SQLInjectionVerify\\Admin\\Login.aspx.cs");
        
        void CSharpParseButtonClick(object sender, EventArgs e)
        {
            
            var parser = new CSharpParser();
            //生成语法树
            syntaxTree = parser.Parse(csharpCodeTextBox.Text, filenamedb);

            if (parser.HasErrors)
            {
                MessageBox.Show(string.Join(Environment.NewLine, parser.Errors.Select(err => err.Message)));
            }
            csharpTreeView.Nodes.Clear();
            foreach (var element in syntaxTree.Children)
            {
                //呈现语法树
                csharpTreeView.Nodes.Add(MakeTreeNode(element));
            }
            SelectCurrentNode(csharpTreeView.Nodes);
            resolveButton.Enabled = true;
            findReferencesButton.Enabled = true;
        }


        TreeNode MakeTreeNode(AstNode node)
        {
            TreeNode t = new TreeNode(GetNodeTitle(node));
            t.Tag = node;
            foreach (AstNode child in node.Children)
            {
                t.Nodes.Add(MakeTreeNode(child));
            }
            return t;
        }

        string GetNodeTitle(AstNode node)
        {
            StringBuilder b = new StringBuilder();
            b.Append(node.Role.ToString());
            b.Append(": ");
            b.Append(node.GetType().Name);
            bool hasProperties = false;
            foreach (PropertyInfo p in node.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (p.Name == "NodeType" || p.Name == "IsNull" || p.Name == "IsFrozen" || p.Name == "HasChildren")
                    continue;
                if (p.PropertyType == typeof(string) || p.PropertyType.IsEnum || p.PropertyType == typeof(bool))
                {
                    if (!hasProperties)
                    {
                        hasProperties = true;
                        b.Append(" (");
                    }
                    else
                    {
                        b.Append(", ");
                    }
                    b.Append(p.Name);
                    b.Append(" = ");
                    try
                    {
                        object val = p.GetValue(node, null);
                        b.Append(val != null ? val.ToString() : "**null**");
                    }
                    catch (TargetInvocationException ex)
                    {
                        b.Append("**" + ex.InnerException.GetType().Name + "**");
                    }
                }
            }
            if (hasProperties)
                b.Append(")");
            return b.ToString();
        }

        bool SelectCurrentNode(TreeNodeCollection c)
        {
            int selectionStart = csharpCodeTextBox.SelectionStart;
            int selectionEnd = selectionStart + csharpCodeTextBox.SelectionLength;
            foreach (TreeNode t in c)
            {
                AstNode node = t.Tag as AstNode;
                if (node != null && !node.StartLocation.IsEmpty && !node.EndLocation.IsEmpty
                    && selectionStart >= GetOffset(csharpCodeTextBox, node.StartLocation)
                    && selectionEnd <= GetOffset(csharpCodeTextBox, node.EndLocation))
                {
                    if (selectionStart == selectionEnd
                        && (selectionStart == GetOffset(csharpCodeTextBox, node.StartLocation)
                            || selectionStart == GetOffset(csharpCodeTextBox, node.EndLocation)))
                    {
                        // caret is on border of this node; don't expand
                        csharpTreeView.SelectedNode = t;
                    }
                    else
                    {
                        t.Expand();
                        if (!SelectCurrentNode(t.Nodes))
                            csharpTreeView.SelectedNode = t;
                    }
                    return true;
                }
            }
            return false;
        }

        void CSharpGenerateCodeButtonClick(object sender, EventArgs e)
        {
            csharpCodeTextBox.Text = syntaxTree.ToString();
        }

        int GetOffset(TextBox textBox, TextLocation location)
        {
            // TextBox uses 0-based coordinates, TextLocation is 1-based
            return textBox.GetFirstCharIndexFromLine(location.Line - 1) + location.Column - 1;
        }

        TextLocation GetTextLocation(TextBox textBox, int offset)
        {
            int line = textBox.GetLineFromCharIndex(offset);
            int col = offset - textBox.GetFirstCharIndexFromLine(line);
            return new TextLocation(line + 1, col + 1);
        }

        void CSharpTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            AstNode node = e.Node.Tag as AstNode;
            if (node != null)
            {
                if (node.StartLocation.IsEmpty || node.EndLocation.IsEmpty)
                {
                    csharpCodeTextBox.DeselectAll();
                }
                else
                {
                    int startOffset = GetOffset(csharpCodeTextBox, node.StartLocation);
                    int endOffset = GetOffset(csharpCodeTextBox, node.EndLocation);
                    csharpCodeTextBox.Select(startOffset, endOffset - startOffset);
                }
            }
        }

        Lazy<IList<IUnresolvedAssembly>> builtInLibs = new Lazy<IList<IUnresolvedAssembly>>(
            delegate
            {
                Assembly[] assemblies = {
					typeof(object).Assembly, // mscorlib
					typeof(Uri).Assembly, // System.dll
					typeof(System.Linq.Enumerable).Assembly, // System.Core.dll
//					typeof(System.Xml.XmlDocument).Assembly, // System.Xml.dll
//					typeof(System.Drawing.Bitmap).Assembly, // System.Drawing.dll
//					typeof(Form).Assembly, // System.Windows.Forms.dll
					typeof(ICSharpCode.NRefactory.TypeSystem.IProjectContent).Assembly,
				};
                IUnresolvedAssembly[] projectContents = new IUnresolvedAssembly[assemblies.Length];
                Stopwatch total = Stopwatch.StartNew();
                Parallel.For(
                    0, assemblies.Length,
                    delegate(int i)
                    {
                        Stopwatch w = Stopwatch.StartNew();
                        AssemblyLoader loader = AssemblyLoader.Create();
                        projectContents[i] = loader.LoadAssemblyFile(assemblies[i].Location);
                        Debug.WriteLine(Path.GetFileName(assemblies[i].Location) + ": " + w.Elapsed);
                    });
                Debug.WriteLine("Total: " + total.Elapsed);
                return projectContents;
            });

        void ResolveButtonClick(object sender, EventArgs e)
        {
            IProjectContent project = new CSharpProjectContent();
            var unresolvedFile = syntaxTree.ToTypeSystem();
            project = project.AddOrUpdateFiles(unresolvedFile);
            project = project.AddAssemblyReferences(builtInLibs.Value);

            ICompilation compilation = project.CreateCompilation();

            ResolveResult result;
            IType expectedType = null;
            Conversion conversion = null;
            if (csharpTreeView.SelectedNode != null)
            {
                var selectedNode = (AstNode)csharpTreeView.SelectedNode.Tag;
                CSharpAstResolver resolver = new CSharpAstResolver(compilation, syntaxTree, unresolvedFile);
                result = resolver.Resolve(selectedNode);
                // CSharpAstResolver.Resolve() never returns null
                Expression expr = selectedNode as Expression;
                if (expr != null)
                {
                    expectedType = resolver.GetExpectedType(expr);
                    conversion = resolver.GetConversion(expr);
                }
            }
            else
            {
                TextLocation location = GetTextLocation(csharpCodeTextBox, csharpCodeTextBox.SelectionStart);
                result = ResolveAtLocation.Resolve(compilation, unresolvedFile, syntaxTree, location);
                if (result == null)
                {
                    MessageBox.Show("Could not find a resolvable node at the caret location.");
                    return;
                }
            }
            using (var dlg = new SemanticTreeDialog())
            {
                dlg.AddRoot("Resolve() = ", result);
                if (expectedType != null)
                    dlg.AddRoot("GetExpectedType() = ", expectedType);
                if (conversion != null)
                    dlg.AddRoot("GetConversion() = ", conversion);
                dlg.ShowDialog();
            }
        }

        void CSharpCodeTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                e.Handled = true;
                csharpCodeTextBox.SelectAll();
            }
        }

        void CsharpCodeTextBoxTextChanged(object sender, EventArgs e)
        {
            resolveButton.Enabled = false;
            findReferencesButton.Enabled = false;
        }

        void FindReferencesButtonClick(object sender, EventArgs e)
        {
            if (csharpTreeView.SelectedNode == null)
                return;

            IProjectContent project = new CSharpProjectContent();
            var unresolvedFile = syntaxTree.ToTypeSystem();
            project = project.AddOrUpdateFiles(unresolvedFile);
            project = project.AddAssemblyReferences(builtInLibs.Value);

            ICompilation compilation = project.CreateCompilation();
            CSharpAstResolver resolver = new CSharpAstResolver(compilation, syntaxTree, unresolvedFile);

            AstNode node = (AstNode)csharpTreeView.SelectedNode.Tag;
            IEntity entity;
            MemberResolveResult mrr = resolver.Resolve(node) as MemberResolveResult;
            TypeResolveResult trr = resolver.Resolve(node) as TypeResolveResult;
            if (mrr != null)
            {
                entity = mrr.Member;
            }
            else if (trr != null)
            {
                entity = trr.Type.GetDefinition();
            }
            else
            {
                return;
            }

            FindReferences fr = new FindReferences();
            int referenceCount = 0;
            FoundReferenceCallback callback = delegate(AstNode matchNode, ResolveResult result)
            {
                Debug.WriteLine(matchNode.StartLocation + " - " + matchNode + " - " + result);
                referenceCount++;
            };

            var searchScopes = fr.GetSearchScopes(entity);
            Debug.WriteLine("Find references to " + entity.ReflectionName);
            fr.FindReferencesInFile(searchScopes, unresolvedFile, syntaxTree, compilation, callback, CancellationToken.None);

            MessageBox.Show("Found " + referenceCount + " references to " + entity.FullName);
        }

        private void btnAnalysisAST_Click(object sender, EventArgs e)
        {
            var parser = new CSharpParser();
            //生成语法树
            syntaxTree = parser.Parse(csharpCodeTextBox.Text, filenamedb);

            if (parser.HasErrors)
            {
                MessageBox.Show(string.Join(Environment.NewLine, parser.Errors.Select(err => err.Message)));
            }

           foreach (var element in syntaxTree.Children)
            {
                //遍历并析取语法树
                AnalysisTreeNode(element);
            }

            //显示语法树

           DataFlow dataflowAST = new DataFlow();
           dataflowAST.Show();
        }

        #region 析取语法树
        
        Service.BLL.SQL_DATAFLOW_SOURCEBo DataFlowSourceBO = new Service.BLL.SQL_DATAFLOW_SOURCEBo();
        Model.SQL_DATAFLOW_SOURCE dataflow = new Model.SQL_DATAFLOW_SOURCE();
        TreeNode AnalysisTreeNode(AstNode node)
        {//1
            TreeNode t = new TreeNode(GetNodeTitle(node));
            t.Tag = node;

            Type nodeType = node.GetType();
            bool isEqual = nodeType.Equals(typeof(ICSharpCode.NRefactory.CSharp.CSharpTokenNode));

            //获取用户输入类型的规则
            Service.BLL.SQL_RULE_STATEMENTBo userInputRuleBO = new Service.BLL.SQL_RULE_STATEMENTBo();
            DataSet userInputRuleDS = userInputRuleBO.SelectUserInputRule();


            if (isEqual)
            {//2
                string strToken = node.GetText();
                if (strToken == "=")
                {//3
                    string strExpresion = node.NextSibling.GetText();
                    if (userInputRuleDS.Tables[0].Rows.Count > 0)
                    {//4

                        for (int i = 0; i < userInputRuleDS.Tables[0].Rows.Count; i++)
                        {//5
                           
                        if (strExpresion.Contains(userInputRuleDS.Tables[0].Rows[i]["STATEMENT"].ToString()))
                        {//6
                            string segment = node.Parent.Parent.GetText();

                            string segment1 = node.Parent.GetText();
                            string[] strArray = segment.Split('=');


                            if (strArray.Length > 1)
                            {//7

                                string strLeft = strArray[0].Trim();
                                string strRight = strArray[1].Trim();
                                if (strRight.Contains(userInputRuleDS.Tables[0].Rows[i]["STATEMENT"].ToString()))
                                {
                                    //string firstDestinationoperand = strRight;
                                    intTreeLevel = 0;
                                    secondDestinationoperand = node.PrevSibling.ToString();
                                    firstOpcode = node.NextSibling.FirstChild.ToString();
                                    firstDestinationoperand= Regex.Replace(node.NextSibling.ToString(), @"\s", "");
                                    firstSourceoperand = node.NextSibling.FirstChild.NextSibling.NextSibling.ToString().Replace("\"","");
                                    intLineNO = node.PrevSibling.EndLocation.Line;
                                    treeName = firstDestinationoperand;

                                    #region 保存每一个节点的遍历结果
                                    dataflow.Destinationoperand = firstDestinationoperand;
                                    dataflow.Opcode = firstOpcode;
                                    dataflow.Sourceoperand = firstSourceoperand;
                                    dataflow.Treelevel = intTreeLevel;
                                    dataflow.Codelineno = intLineNO;
                                    dataflow.Filename = filenamedb;
                                    dataflow.Treename = treeName;
                                    DataFlowSourceBO.insertDATAFLOW(dataflow);
                                    intTreeLevel += 1;
                                    #endregion
                                   
                                }


                                #region 保存每一个节点的遍历结果
                                dataflow.Destinationoperand = secondDestinationoperand;
                                dataflow.Opcode = strToken;
                                dataflow.Sourceoperand = firstDestinationoperand;
                                dataflow.Treelevel = intTreeLevel;
                                dataflow.Codelineno = intLineNO;
                                dataflow.Filename = filenamedb;
                                dataflow.Treename = treeName;
                                DataFlowSourceBO.insertDATAFLOW(dataflow);
                                
                                traceSourceoperand=secondDestinationoperand;
                                intTreeLevel += 1;
                                #endregion
                            }// 7 if (strArray.Length > 1)
                        }// 6 if (strExpresion.Contains(ruleDS.Tables[0].Rows[r]["STATEMENT"].ToString()))
                    }// 5 for (int i = 0; i < userInputRuleDS.Tables[0].Rows.Count; ++)
                }//4  if (userInputRuleDS.Tables[0].Rows.Count > 0)
            }//3 if (strToken == "=")
          }//2 if (isEqual)
            
            foreach (AstNode child in node.Children)
            {
                AnalysisTreeNode(child);
                traceTreeNode(child,traceSourceoperand);
                t.Nodes.Add(MakeTreeNode(child));
            }
            return t;
            
        }// 1 {//1

        #endregion

        #region 析取数据传播路径
        TreeNode traceTreeNode(AstNode node,string sourceOperand)
        {//1
            TreeNode t = new TreeNode(GetNodeTitle(node));
            t.Tag = node;

            Type nodeType = node.GetType();
            bool isEqual = nodeType.Equals(typeof(ICSharpCode.NRefactory.CSharp.CSharpTokenNode));
            if (isEqual)
            {//2
                string strToken = node.GetText();
                if (strToken == "=")
                {//3
                    string strExpresion = node.NextSibling.GetText();
                    if (strExpresion.Contains(sourceOperand))
                       {//4
                                string segment = node.Parent.Parent.GetText();

                                string segment1 = node.Parent.GetText();
                                string[] strArray = segment.Split('=');


                                if (strArray.Length > 1)
                                {//5

                                    string strLeft = strArray[0].Trim();
                                    string strRight = strArray[1].Trim();

                                    firstDestinationoperand = node.NextSibling.ToString();
                                    firstOpcode = node.NextSibling.FirstChild.ToString();
                                    firstSourceoperand = node.NextSibling.FirstChild.ToString().Replace("\"", "");
                                    intLineNO = node.PrevSibling.EndLocation.Line;

                                    if ((firstDestinationoperand.Contains("select") && firstDestinationoperand.Contains("from")) || (firstDestinationoperand.Contains("update") && firstDestinationoperand.Contains("set")) || (firstDestinationoperand.Contains("delete") && firstDestinationoperand.Contains("from")))
                                    {
                                        firstOpcode = "Concatenate";
                                        firstSourceoperand = sourceOperand;

                                        #region 保存字符串等式的左边关系
                                        dataflow.Destinationoperand = firstDestinationoperand;
                                        dataflow.Opcode = firstOpcode;
                                        dataflow.Sourceoperand = firstSourceoperand;
                                        dataflow.Treelevel = intTreeLevel;
                                        dataflow.Codelineno = intLineNO;
                                        dataflow.Filename = filenamedb;
                                        dataflow.Treename = treeName;
                                        DataFlowSourceBO.insertDATAFLOW(dataflow);

                                        intTreeLevel += 1;
                                        #endregion

                                        secondDestinationoperand = node.Parent.FirstChild.ToString();
                                        firstOpcode ="=";
                                        firstDestinationoperand = node.Parent.LastChild.ToString();
                                        firstSourceoperand = firstDestinationoperand;

                                        #region 保存字符串等式的右边关系
                                        dataflow.Destinationoperand = secondDestinationoperand;
                                        dataflow.Opcode = firstOpcode;
                                        dataflow.Sourceoperand = firstSourceoperand;
                                        dataflow.Treelevel = intTreeLevel;
                                        dataflow.Codelineno = intLineNO;
                                        dataflow.Filename = filenamedb;
                                        dataflow.Treename = treeName;
                                        DataFlowSourceBO.insertDATAFLOW(dataflow);
                                        intTreeLevel += 1;
                                        #endregion

                                    }
                                    if ((firstDestinationoperand.ToUpper().Contains("SqlCommand".ToUpper()) && firstDestinationoperand.ToUpper().Contains(sourceOperand.ToUpper())) || (firstDestinationoperand.ToUpper().Contains("OleDbCommand".ToUpper()) && firstDestinationoperand.ToUpper().Contains(sourceOperand.ToUpper())) || (firstDestinationoperand.ToUpper().Contains("ODBCCommand".ToUpper()) && firstDestinationoperand.ToUpper().Contains(sourceOperand.ToUpper())))
                                    {
                                        string tempfirstOpcode=string.Empty;
                                        if (firstDestinationoperand.ToUpper().Contains("SqlCommand".ToUpper()))
                                        {
                                            tempfirstOpcode = "SqlCommand()";
                                        }
                                        if (firstDestinationoperand.ToUpper().Contains("OleDbCommand".ToUpper()))
                                        {
                                            tempfirstOpcode = "OleDbCommand()";
                                        }
                                        if (firstDestinationoperand.ToUpper().Contains("ODBCCommand".ToUpper()))
                                        {
                                            tempfirstOpcode = "ODBCCommand()";
                                        }

                                        firstDestinationoperand = node.NextSibling.ToString();
                                        firstOpcode = tempfirstOpcode;
                                        firstSourceoperand = sourceOperand;

                                        #region 保存字符串等式的左边关系
                                        dataflow.Destinationoperand = firstDestinationoperand.Remove(0,4);
                                        dataflow.Opcode = firstOpcode;
                                        dataflow.Sourceoperand = firstSourceoperand;
                                        dataflow.Treelevel = intTreeLevel;
                                        dataflow.Codelineno = intLineNO;
                                        dataflow.Filename = filenamedb;
                                        dataflow.Treename = treeName;
                                        DataFlowSourceBO.insertDATAFLOW(dataflow);

                                        intTreeLevel += 1;
                                        #endregion

                                        secondDestinationoperand = node.Parent.FirstChild.ToString();
                                        firstOpcode =node.Parent.FirstChild.NextSibling.NextSibling.FirstChild.ToString();
                                        firstDestinationoperand = node.Parent.LastChild.ToString();
                                        firstSourceoperand = firstDestinationoperand;

                                        #region 保存字符串等式的右边关系
                                        dataflow.Destinationoperand = secondDestinationoperand;
                                        dataflow.Opcode = firstOpcode;
                                        dataflow.Sourceoperand = firstSourceoperand;
                                        dataflow.Treelevel = intTreeLevel;
                                        dataflow.Codelineno = intLineNO;
                                        dataflow.Filename = filenamedb;
                                        dataflow.Treename = treeName;
                                        DataFlowSourceBO.insertDATAFLOW(dataflow);
                                        intTreeLevel += 1;
                                        #endregion

                                        secondDestinationoperand = node.Parent.FirstChild.ToString();
                                        firstOpcode = node.Parent.FirstChild.NextSibling.ToString();
                                        firstDestinationoperand = node.Parent.ToString();
                                        firstSourceoperand = firstDestinationoperand;

                                        #region 保存字符串等式的右边关系
                                        dataflow.Destinationoperand = secondDestinationoperand;
                                        dataflow.Opcode = firstOpcode;
                                        dataflow.Sourceoperand = firstSourceoperand;
                                        dataflow.Treelevel = intTreeLevel;
                                        dataflow.Codelineno = intLineNO;
                                        dataflow.Filename = filenamedb;
                                        dataflow.Treename = treeName;
                                        DataFlowSourceBO.insertDATAFLOW(dataflow);
                                        intTreeLevel += 1;
                                        #endregion
                                    }

                                   
                                    
                                    secondDestinationoperand = node.Parent.FirstChild.ToString();
                                    firstOpcode = "=";
                                    firstDestinationoperand = node.Parent.LastChild.ToString();
                                    firstSourceoperand = firstDestinationoperand;

                                    #region 保存字符串等式的右边关系
                                    dataflow.Destinationoperand = secondDestinationoperand;
                                    dataflow.Opcode = firstOpcode;
                                    dataflow.Sourceoperand = firstSourceoperand;
                                    dataflow.Treelevel = intTreeLevel;
                                    dataflow.Codelineno = intLineNO;
                                    dataflow.Filename = filenamedb;
                                    dataflow.Treename = treeName;
                                    DataFlowSourceBO.insertDATAFLOW(dataflow);
                                    #endregion
                                    
                                    traceSourceoperand = secondDestinationoperand;

                                   

                                }// 5  if (strArray.Length > 1)
                       }//4  if (strExpresion.Contains(sourceOperand))
                }//3 if (strToken == "=")
            }//2 if (isEqual)

            //foreach (AstNode child in node.Children)
            //{
            //    traceTreeNode(child, traceSourceoperand);
               
            //}
            return t;

        }// 1 {//1

      #endregion


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

    }
}

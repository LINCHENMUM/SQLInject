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

namespace ICSharpCode.NRefactory.Demo
{
    /// <summary>
    /// Description of CSDemo.
    /// </summary>
    public partial class CSDemo : UserControl
    {
        public string filenamedb = string.Empty;
        public string filenameAST = string.Empty;
        public string filecontent = string.Empty;

       // filenameAST=

        public CSDemo()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            

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

        SyntaxTree syntaxTree;

        void CSharpParseButtonClick(object sender, EventArgs e)
        {
           // StreamReader objReader = new StreamReader("C:\\D\\SchoolTask\\2014学年第二学期\\第2版\\新思路\\SQLInjectionVerify\\Admin\\Login.aspx.cs");
            string text = System.IO.File.ReadAllText(@"C:\\D\\SchoolTask\\2014学年第二学期\\第2版\\新思路\\SQLInjectionVerify\\Admin\\Login.aspx.cs");
            var parser = new CSharpParser();
            //生成语法树
            filecontent = text;
            csharpCodeTextBox.Text = filecontent;
            syntaxTree = parser.Parse(filecontent, filenamedb);

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
            foreach (AstNode child in node.Children) {
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
            StreamReader objReader = new StreamReader(filenameAST);
            //syntaxTree = parser.Parse(csharpCodeTextBox.Text, filenameAST);
            syntaxTree = parser.Parse(objReader, filenameAST);
            if (parser.HasErrors)
            {
                MessageBox.Show(string.Join(Environment.NewLine, parser.Errors.Select(err => err.Message)));
            }
            foreach (var element in syntaxTree.Children)
            {
                //呈现语法树
                //csharpTreeView.Nodes.Add(MakeTreeNode(element));
                AnalysisTreeNode(element);
            }

            //显示语法树

            DataFlow dataflowAST = new DataFlow();
            dataflowAST.Show();
        }

        #region 析取语法树
        List<string> keyList = new List<string>() { "Request.Form", "Request.QueryString" };

        Service.BLL.SQL_DATAFLOW_SOURCEBo DataFlowSourceBO = new Service.BLL.SQL_DATAFLOW_SOURCEBo();
        Model.SQL_DATAFLOW_SOURCE dataflow = new Model.SQL_DATAFLOW_SOURCE();

        TreeNode AnalysisTreeNode(AstNode node)
        {
            TreeNode t = new TreeNode(GetNodeTitle(node));
            t.Tag = node;

            Type nodeType = node.GetType();
            bool isEqual = nodeType.Equals(typeof(ICSharpCode.NRefactory.CSharp.CSharpTokenNode));

            if (isEqual)
            {
                string strToken = node.GetText();
                if (strToken == "=")
                {
                    string strExpresion = node.NextSibling.GetText();
                    foreach (string strKey in keyList)
                    {
                        if (strExpresion.Contains(strKey))
                        {
                            string segment = node.Parent.Parent.GetText();
                            string[] strArray = segment.Split('=');


                            if (strArray.Length > 1)
                            {

                                string strLeft = strArray[0];
                                string strRight = strArray[1];

                                #region 保存每一个节点的遍历结果
                                dataflow.Destinationoperand = strLeft;
                                dataflow.Opcode = strToken;
                                dataflow.Sourceoperand = strRight;
                                dataflow.Treelevel = 0;
                                dataflow.Codelineno = 99;
                                dataflow.Filename = filenamedb;
                                dataflow.Treename = "treename";
                                DataFlowSourceBO.insertDATAFLOW(dataflow);
                                #endregion

                            }
                        }
                    }
                }
            }

            foreach (AstNode child in node.Children)
            {
                t.Nodes.Add(MakeTreeNode(child));
            }

            return t;
        }

        #endregion

    }
}

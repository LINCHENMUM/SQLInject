<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SQL注入验证系统</title>
    <style type="text/css">
        body
        {
            position: relative;
            width: 180px;
            margin: 0 auto;
            text-align: center;
            color: #000000;
            font-size: 12px;
            border: solid 1px #808080;
            margin-left: 120px;
        }
        .style1
        {
            
            height: 25px;
        }
        .titleSystemName
        {
            float: left;
            padding-left: 20px;
            font-size: 16px;
            font-weight: bolder;
            font-family: Arial, Helvetica,sans-serif;
            color: #000000;
        }
    </style>
</head>
<body style="margin-top: 20px; width: 150px;  margin-left :120px">
    <form id="form1" runat="server">
    <div>
        <table style="width: 180px;" cellspacing="0" cellpadding="0">
            <tr>
                <td class="style1 ">
                    <div class="titleSystemName">
                        导航栏</div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                        Width="155px">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                            <asp:TreeNode Text="SQL注入研究" Value="SQL注入研究">
                                <asp:TreeNode NavigateUrl="AddNews.aspx?id=1" Target="right" Text="添加信息" Value="添加信息">
                                </asp:TreeNode>
                                <asp:TreeNode NavigateUrl="ArticleManage.aspx?id=1" Target="right" Text="管理信息" Value="管理信息">
                                </asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Text="参数化编程" Value="参数化编程">
                                <asp:TreeNode NavigateUrl="AddNews.aspx?id=2" Target="right" Text="添加信息" Value="添加信息">
                                </asp:TreeNode>
                                <asp:TreeNode NavigateUrl="ArticleManage.aspx?id=2" Target="right" Text="管理信息" Value="管理信息">
                                </asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Text="对象化编程" Value="对象化编程">
                                <asp:TreeNode NavigateUrl="AddNews.aspx?id=3" Target="right" Text="添加信息" Value="添加信息">
                                </asp:TreeNode>
                                <asp:TreeNode NavigateUrl="ArticleManage.aspx?id=3" Target="right" Text="管理信息" Value="管理信息">
                                </asp:TreeNode>
                             </asp:TreeNode>
                            <asp:TreeNode Text="存储过程防御" Value="存储过程防御">
                                <asp:TreeNode NavigateUrl="AddNews.aspx?id=4" Target="right" Text="添加信息" Value="添加信息">
                                </asp:TreeNode>
                                <asp:TreeNode NavigateUrl="ArticleManage.aspx?id=4" Target="right" Text="管理信息" Value="管理信息">
                            </asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Text="全局防御" Value="全局防御">
                                <asp:TreeNode NavigateUrl="AddNews.aspx?id=5" Target="right" Text="添加信息" Value="添加信息">
                                </asp:TreeNode>
                                <asp:TreeNode NavigateUrl="ArticleManage.aspx?id=5" Target="right" Text="管理信息" Value="管理信息">
                                </asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Text="用户管理" Value="用户管理">
                                <asp:TreeNode NavigateUrl="~/Admin/AddUser.aspx" Target="right" Text="添加用户" Value="添加用户">
                                </asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/Admin/UserManage.aspx" Target="right" Text="管理用户"
                                    Value="管理用户"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode NavigateUrl="../Default.aspx" Text="退出后台" Value="退出后台" Target="_blank">
                            </asp:TreeNode>
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                            NodeSpacing="0px" VerticalPadding="2px" />
                    </asp:TreeView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

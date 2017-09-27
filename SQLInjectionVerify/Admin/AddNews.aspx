<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNews.aspx.cs" Inherits="AddNews" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .txtwid
        {
            width: 384px;
        }
        body
        {
            width: 810px;
            color: #000000;
            font-size: 12px;
            border: solid 1px #000000;
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
        .style1
        {
           
            height: 25px;
            text-align: left;
        }
    </style>
</head>
<body style="margin-top: 20px; width: 850px;">
    <form id="form1" runat="server" method="post">
    <div>
        <table width="810px">
            <tr>
                <td class="style1 " colspan="3">
                    <div class="titleSystemName">
                       添加信息
                    </div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            </tr>  <tr><td style ="height :18px"></td></tr>
            <tr>
                <td colspan="3" align="left">
                    
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 120px; height: 28px; text-align: right;">
                    信息类别：
                </td>
                <td>
                    <asp:DropDownList ID="dlstNewsType" runat="server" AutoPostBack="True" Width="387px" >
                        <asp:ListItem>SQL注入研究</asp:ListItem>
                        <asp:ListItem>参数化编程</asp:ListItem>
                        <asp:ListItem>对象化编程</asp:ListItem>
                        <asp:ListItem>存储过程防御</asp:ListItem>
                        <asp:ListItem>全局防御</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="labTitle" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px; height: 28px;">
                    信息标题：
                </td>
                <td>
                    <asp:TextBox ID="txtNewsTitle" runat="server" CssClass="txtwid" MaxLength="200"></asp:TextBox>
                    <p style="text-align: left;">
                        *字数在30字以内</p>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewsTitle"
                        ErrorMessage="*标题必填" Width="118px"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px; height: 28px;">
                    信息内容：
                </td>
                <td>
                    <asp:TextBox ID="txtNewsContent" runat="server" Height="211px" TextMode="MultiLine"
                        CssClass="txtwid"></asp:TextBox>
                </td>
                <td style="width: 85px; text-align: left;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewsContent"
                        ErrorMessage="*内容必填" Width="128px"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center" style="width: 383px; height: 26px;">
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加" Width="74px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" runat="server" CausesValidation="False" OnClick="btnReset_Click"
                        Text="取消" Width="74px" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="color: Red; text-align: center;">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

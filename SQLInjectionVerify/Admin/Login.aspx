﻿<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            position: relative;
            width: 43%;
            text-align: center;
            margin-top: 105px;
            color: #808080;
            font-size: 12px;
            height :100%;
            top: 0px;
            left: -7px;
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post">
    
    <table width="500" height="220"  style="align-content" border="1">
        <tr>
            <td height="54">
            </td>
            <td height="54">
                 <b>&nbsp;&nbsp;后台管理系统</b>
            </td>
            <td height="54"></td>
        </tr>

        <tr>
            <td align="right" width="120" height="30">
                用户名:
            </td>
            <td style="height: 5px">
                <asp:TextBox ID="username" runat="server" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*姓名必填"
                    ControlToValidate="username"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" height="30px">
                密&nbsp;码:
            </td>
            <td>
                <asp:TextBox ID="pwd" runat="server" TextMode="Password" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*密码必填"
                    ControlToValidate="pwd"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" height="30px">
                验证码:
            </td>
            <td>
                <asp:TextBox ID="checkCode" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td style="color: Blue; text-align: left;">
                <div style="background-color: #ececec; width: 35px; height: 20px; border: solid 1px #6AC0FF; text-align: center;">
                    <asp:Label ID="verifyCode" runat="server"></asp:Label></div>
            </td>
        </tr>
    </table>
    <br />
    <p style="width: 385px;align-content:center" >&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="submit" runat="server" Text="登录" OnClick="submit_Click" Width="74px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
        <asp:Button ID="cancel" runat="server" Text="取消" OnClick="cancel_Click" Width="74px" />
    </p>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="AddUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
  <style type="text/css">
       
        body
        {
            width: 810px;
            color: #000000;
            font-size: 12px;
            border: solid 1px #000000;
        }
        .titleSystemName
{
	
	font-size: 25px;
	font-weight: bolder;
	font-family: Arial, Helvetica,sans-serif;
	color: #009966;
}
td
{ line-height:21px}
      .style1
      {
          width: 185px;
      }
    </style>
</head>
<body style="margin-top: 20px; width: 450px;">
    <form id="form1" runat="server">
   
        <table   style="width:250px" >
            <tr><td colspan="2" align="left">
                  添加用户
            <tr>
                <td style ="height :50px;"><span >用户姓名</span>:</td>
                <td class="style1"><asp:TextBox ID="username" runat="server" Width="150px"></asp:TextBox></td>
            </tr>
            <tr>
                <td  style ="height :40px;"><span >用户密码</span>:</td>
                <td class="style1"><asp:TextBox ID="pwd" runat="server" Width="150px" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
          
                <td   style ="height :60px;" colspan="2"><asp:Button ID="add" runat="server" Text="添加" OnClick="add_Click" 
                        Width="74px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="reset" runat="server" Text="取消" OnClick="reset_Click" 
                        Width="74px" /></td>
            </tr>
            <tr><td colspan="2" style="color:Red;"><asp:Label ID="lblMessage" runat="server" ></asp:Label></td></tr>
        </table>
  
    </form>
</body>
</html>

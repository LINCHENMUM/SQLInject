<%@ Control Language="C#" AutoEventWireup="true" CodeFile="head.ascx.cs" Inherits="UserControl_head" %>
<style type="text/css">
    .td1
    {
        height: 35px;
        width: 19%;
        font-size: 12px;
      
    }
    .td2
    {
        width: 9%;
        height: 35px;
        font-size :15px;
        background-color:#808080
    }
</style>
<table style ="width :100%" border="0">
    <tr>
        <td colspan="10">
            <div class="wishTop">
                <div class="titleSystemName">
                    SQL 注入防御验证系统</div>
                <div class="bglogo">
                   <!-- <asp:Image ID="Image1" runat="server" />-->
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td class="td1 ">
            <asp:Label ID="dateLabel" runat="server" Style="color: #000000" Width="180"></asp:Label>
        </td>
        <td class="td2 ">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" Font-Underline="false"
                ForeColor="black" Style="font-weight: 800; color: #000000;">首页</asp:HyperLink>
        </td>
        <td class="td2 ">
            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/ShowInformation.aspx?id=1" Font-Underline="false"
                ForeColor="black" Style="font-weight: 800; color: #000000;">SQL注入研究</asp:HyperLink>
        </td>
         <td class="td2 ">
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/ShowInformation.aspx?id=2" Font-Underline="false"
                ForeColor="black" Style="font-weight: 800; color: #000000;">参数化编程</asp:HyperLink>
        </td>
        <td class="td2 ">
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/ShowInformation.aspx?id=3" Font-Underline="false"
                ForeColor="black" Style="font-weight: 800; color: #000000;">对象化编程</asp:HyperLink>
        </td>
        <td class="td2 ">
            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/ShowInformation.aspx?id=4" Font-Underline="false"
                ForeColor="black" Style="font-weight: 800; color: #000000;">存储过程防御</asp:HyperLink>
        </td>
        <td class="td2 ">
            <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/ShowInformation.aspx?id=5" Font-Underline="false"
                ForeColor="black" Style="font-weight: 800; color: #000000;">全局防御</asp:HyperLink>
        </td>
         <td class="td2 ">
            <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/ShowInformation.aspx?id=6" Font-Underline="false"
                ForeColor="black" Style="font-weight: 800; color: #000000;">存储过程化编程</asp:HyperLink>
        </td>
        <td></td>
    </tr>
</table>

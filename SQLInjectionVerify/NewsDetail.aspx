<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="NewsDetail.aspx.cs" Inherits="NewsDetail" Title="SQL注入" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Label ID="labTitle" runat="server"></asp:Label></h1>
    <br /></br>
    <p style="width: 100%;">
        <span style="width: 70%; padding-left: 55px; font-size: 14px; line-height: 23.0px;
            word-break: break-all;">
            <%=GetContent()%>
        </span>
    </p>
    </br>
</asp:Content>

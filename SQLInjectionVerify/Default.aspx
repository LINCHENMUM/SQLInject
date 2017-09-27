<%@ Page Language="C#" MasterPageFile="~/mainMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" Title="SQL Injection Verify System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td class="dtd1">
                <table class="dtab1">
                    <tr class="dtr1">
                        <td colspan="2">
                            <b>&nbsp;&nbsp;SQL注入研究</b>
                            <div class="more ">
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" NavigateUrl="~/ShowInformation.aspx?id=1">更多</asp:HyperLink></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataList ID="dlstShiShi" runat="server" OnItemCommand="dlstShiShi_ItemCommand"
                                Height="136" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" GridLines="Both">
                                <ItemTemplate>
                                    <table border="0" style="font-size: 9pt; width: 100%; height: 100%;" cellspacing="0"
                                        cellpadding="0">
                                        <tr>
                                            <td class="dtd2">
                                                <asp:LinkButton ID="lbtnTitle" runat="server" CommandName="select" CausesValidation="False"><%# DataBinder.Eval(Container.DataItem, "title")%></asp:LinkButton>
                                            </td>
                                            <td style="width: 150px">
                                                <%#DataBinder.Eval(Container.DataItem, "IssueDate")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle ForeColor="Blue" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
             <td class="dtd1">
                <table class="dtab1">
                    <tr class="dtr1">
                        <td colspan="2">
                            <b>&nbsp;&nbsp;参数化编程</b>
                            <div class="more ">
                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="False" NavigateUrl="~/ShowInformation.aspx?id=2">更多</asp:HyperLink></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataList ID="dlstEconomic" runat="server" OnItemCommand="dlstShiShi_ItemCommand"
                                Height="136" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" GridLines="Both">
                                <ItemTemplate>
                                    <table border="0" style="font-size: 9pt; width: 100%; height: 100%;" cellspacing="0"
                                        cellpadding="0">
                                        <tr>
                                            <td class="dtd2">
                                                <asp:LinkButton ID="lbtnTitle" runat="server" CommandName="select" CausesValidation="False"><%# DataBinder.Eval(Container.DataItem, "title")%></asp:LinkButton>
                                            </td>
                                            <td style="width: 150px">
                                                <%#DataBinder.Eval(Container.DataItem, "IssueDate")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle ForeColor="Blue" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="dtd1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="dtd1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
             <td class="dtd1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="dtd1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
             <td class="dtd1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
    <br />
</asp:Content>

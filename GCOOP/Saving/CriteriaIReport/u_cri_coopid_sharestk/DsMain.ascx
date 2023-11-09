<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_sharestk.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span>สหกรณ์:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>หุ้น:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="s_share" runat="server" ToolTip="#,##0.00" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>ถึง:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="e_share" runat="server" ToolTip="#,##0.00"  Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

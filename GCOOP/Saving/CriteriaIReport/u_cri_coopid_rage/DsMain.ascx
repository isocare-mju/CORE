<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_rage.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td width="25%">
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td colspan="3" width="75%">
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <div>
                        <span>อายุ:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="s_age" runat="server">
                    </asp:TextBox>
                </td>
                <td width="25%">
                    <div>
                        <span>ถึง:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="e_age" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

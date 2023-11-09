<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_sex_age_salaly.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td width="25%">
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
                <td width="25%">
                    <div>
                        <span>เพศ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="sex" runat="server">
                        <asp:ListItem Value="%%">ทั้งหมด</asp:ListItem>
                        <asp:ListItem Value="M">ชาย</asp:ListItem>
                        <asp:ListItem Value="F">หญิง</asp:ListItem>
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
                    <asp:TextBox ID="s_age" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="25%">
                    <div>
                        <span>ถึง:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="e_age" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <div>
                        <span>เงินเดือน:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="s_salaly" ToolTip="#,##0.00" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="25%">
                    <div>
                        <span>ถึง:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="e_salaly" ToolTip="#,##0.00" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

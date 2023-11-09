<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_rgroup_age.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>ตามสังกัด:</span>
                    </div>
                </td>
                <td width="35%">
                    <asp:DropDownList ID="membgroup_start" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>ถึงสังกัด:</span>
                    </div>
                </td>
                <td width="35%">
                    <asp:DropDownList ID="membgroup_end" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อายุสมาชิกตั้งแต่(ปี):</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="age" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

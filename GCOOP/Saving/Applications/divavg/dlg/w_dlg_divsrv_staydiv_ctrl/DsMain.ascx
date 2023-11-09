<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.divavg.dlg.w_dlg_divsrv_staydiv_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 560px;">
            <tr>
                <td width="12%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="div_year" runat="server"></asp:TextBox>
                    </div>
                </td>
                </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
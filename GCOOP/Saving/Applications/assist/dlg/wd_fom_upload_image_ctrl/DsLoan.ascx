<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsLoan.ascx.cs" Inherits="Saving.Applications.assist.dlg.wd_fom_upload_image_ctrl.DsLoan" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 400px">
            <tr>
                <td width="35%" align="right">
                    <div>
                        <span>สัญญา :</span>
                    </div>
                </td>
                <td width="50%">
                    <div>
                        <asp:TextBox ID="loancontract_no" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

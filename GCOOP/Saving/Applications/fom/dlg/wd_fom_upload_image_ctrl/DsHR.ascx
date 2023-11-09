<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsHR.ascx.cs" Inherits="Saving.Applications.fom.dlg.wd_fom_upload_image_ctrl.DsHR" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 400px">
            <tr>
                <td width="35%" align="right">
                    <div>
                        <span>รหัสพนักงาน :</span>
                    </div>
                </td>
                <td width="50%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" style="text-align:center"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="35%" align="right">
                    <div>
                        <span>ชื่อ-สกุล :</span>
                    </div>
                </td>
                <td width="50%">
                    <div>
                        <asp:TextBox ID="full_name" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsRdc.ascx.cs" Inherits="Saving.Applications.fom.dlg.wd_fom_upload_image_ctrl.DsRdc" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 400px">
            <tr>
                <td width="35%" align="right">
                    <div>
                        <span> เลขที่ลงรับ :</span>
                    </div>
                </td>
                <td width="50%">
                    <div>
                        <asp:TextBox ID="document_no" runat="server" style="text-align:center"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="35%" align="right">
                    <div>
                        <span>ชื่อเอกสาร/เรื่อง :</span>
                    </div>
                </td>
                <td width="50%">
                    <div>
                        <asp:TextBox ID="document_title" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

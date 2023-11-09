<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsImg.ascx.cs" Inherits="Saving.Applications.assist.dlg.wd_fom_upload_image_ctrl.DsImg" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 300px">
            <tr>
                <td width="40%">
                    <span>ชื่อรูปภาพ :</span>
                </td>
                <td width="60%">
                    <asp:TextBox ID="img_name" runat="server" Width="" ReadOnly="true"></asp:TextBox>
                </td>
            <tr>
            </tr>
                <td width="40%">
                    <span>วันที่หมดอายุ :</span>
                </td>
                <td width="60%">
                    <asp:TextBox ID="expire_date" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

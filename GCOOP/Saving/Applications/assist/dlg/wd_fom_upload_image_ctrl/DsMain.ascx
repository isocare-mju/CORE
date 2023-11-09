<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.dlg.wd_fom_upload_image_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 400px">
            <tr>
                <td width="30%" align="right">
                    <div>
                        <span>เลือกกลุ่มงาน :</span>
                    </div>
                </td>
                <td width="60%">
                    <div>
                        <asp:DropDownList ID="application" runat="server" Width="100%">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width="10%">
                </td>
            </tr>
            <tr>
                <td width="30%" align="right">
                    <div>
                        <span>เลือกรูปแบบข้อมูล :</span>
                    </div>
                </td>
                <td width="60%">
                    <div>
                        <asp:DropDownList ID="img_type_code" runat="server" Width="100%">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width="10%">
                    <asp:Button ID="B_1" runat="server" Text="ดึง" Width="100%" style="display:none" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

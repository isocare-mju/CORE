<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_approve_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 350px;">
            <tr>
                <td width="50%">
                    <div>
                        <span>สถานะรายการ :</span>
                    </div>
                </td>
                <td width="40%">
                    <div>
                        <asp:DropDownList ID="apv_status" runat="server" Enabled="false">
                            <asp:ListItem Value="8">รออนุมัติ</asp:ListItem>                  
                            <asp:ListItem Value="1">อนุมัติ</asp:ListItem>                  
                            <asp:ListItem Value="-1">ยกเลิก</asp:ListItem>                  
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

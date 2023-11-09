<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_approve_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Height="550px" ScrollBars="Auto">
    <table class="DataSourceRepeater"
        <tr>
            <th width="3%">
                ลำดับ
            </th>
            <th width="10%">
                เลขที่คำขอ
            </th>
            <th width="15%">
                ผู้ร้องขอ
            </th>
            <th width="13%">
                เลขที่บัญชี
            </th>
            <th width="20%">
               รายการ
            </th>
            <th width="10%">
                จำนวนเงิน
            </th>
            <th width="20%">
                สถานะ
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                       <asp:TextBox ID="running_number" runat="server" style="text-align:center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="apv_docno" runat="server"  style="text-align:center" ReadOnly="true"></asp:TextBox>
                    </td>
                     <td>
                        <asp:TextBox ID="user_id" runat="server"  style="text-align:center" ReadOnly="true"></asp:TextBox>
                    </td>
                     <td>
                        <asp:TextBox ID="deptaccount_no" runat="server"  style="text-align:center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="remark" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                     <td>
                        <asp:TextBox ID="dept_amt" runat="server" ToolTip="#,##0.00"  style="text-align:right" ReadOnly="true"></asp:TextBox>
                    </td>
                     <td>
                        <asp:DropDownList ID="apv_status" runat="server">
                            <asp:ListItem Value="8">รออนุมัติ</asp:ListItem>   
                            <asp:ListItem Value="1">อนุมัติ</asp:ListItem>   
                            <asp:ListItem Value="-1">ยกเลิก</asp:ListItem>  
                        </asp:DropDownList>
                    </td>                   
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>

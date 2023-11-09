<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.deposit_const.w_sheet_dp_const_dpucfrecppaytype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater">
        <tr>
            <th width="8%">
                รหัส
            </th>
            <th width="30%">
                ประเภทการทำรายการ
            </th>
            <th width="8%">
                ฝั่งรายการ
            </th>
            <th width="8%">
                รหัส Stm
            </th>
            <th width="8%">
                ประเภทเงิน
            </th>
            <th width="8%">
                คู่บัญชี
            </th>
            <th width="8%">
                รหัสกลุ่ม
            </th>
            <th width="8%">
                ฝั่งบ/ช
            </th>
            <th width="8%">
                ทำ VC
            </th>
            <th width="8%">
                กลุ่ม VC
            </th>
            <th width="5%">
                ลบ!
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="750px" Height="435px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="8%">
                        <asp:TextBox ID="RECPPAYTYPE_CODE" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="RECPPAYTYPE_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td width="8%">
                          <asp:DropDownList ID="recppaytype_flag" runat="server" >
                                <asp:ListItem Text="ฝาก" Value="1"></asp:ListItem>
                                <asp:ListItem Text="ถอน" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="stm_itemtype" runat="server"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:DropDownList ID="moneytype_support" runat="server">
                            </asp:DropDownList>
                    </td>
                    <td width="8%">
                        <asp:DropDownList ID="default_accid" runat="server"></asp:DropDownList>
                    </td>
                    <td width="8%">
                        <asp:DropDownList ID="group_itemtpe" runat="server" Style="text-align: center;">
                                <asp:ListItem Text="--กรุณาเลือก--" Value="0" ></asp:ListItem>
                                <asp:ListItem Text="ADJ - ปรังปรุง" Value="ADJ" ></asp:ListItem>
                                <asp:ListItem Text="CLS - ปิดบัญชี" Value="CLS" ></asp:ListItem>
                                <asp:ListItem Text="DEP - ฝาก" Value="DEP" ></asp:ListItem>
                                <asp:ListItem Text="ERR - ปรังปรุง" Value="ERR" ></asp:ListItem>
                                <asp:ListItem Text="INT - ดอกเบี้ย" Value="INT" ></asp:ListItem>
                                <asp:ListItem Text="OPN - เปิดบัญชี" Value="OPN" ></asp:ListItem>
                                <asp:ListItem Text="TAX - ภาษี" Value="TAX" ></asp:ListItem>
                                <asp:ListItem Text="WID - ถอน" Value="WID" ></asp:ListItem>
                                <asp:ListItem Text="RCL - ปรังปรุง" Value="RCL" ></asp:ListItem>
                                <asp:ListItem Text="NOR - ฝาก" Value="NOR" ></asp:ListItem>
                                <asp:ListItem Text="FEE - ค่าธรรมเนียม" Value="FEE" ></asp:ListItem>
                                <asp:ListItem Text="DIV - ปันผล" Value="DIV" ></asp:ListItem>
                            </asp:DropDownList>
                    </td>
                    <td width="8%">
                        <%--<asp:TextBox ID="accnature_flag" runat="server"></asp:TextBox>--%>
                        <asp:DropDownList ID="accnature_flag" runat="server">
                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                            <asp:ListItem Text="DR" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="CR" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="center" width="8%">
                        <%--<asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>--%>
                        <asp:CheckBox ID="genvc_flag" runat="server" />
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="accmap_code" runat="server"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>

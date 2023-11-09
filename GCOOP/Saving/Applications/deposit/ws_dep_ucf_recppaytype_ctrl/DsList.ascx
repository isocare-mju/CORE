<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.deposit.ws_dep_ucf_recppaytype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
    <asp:Panel ID="Panel1" runat="server" Height="550px" ScrollBars="Auto" Width="830px">
        <table class="DataSourceRepeater" style="width:1200px;">
            <tr>
                <th width="4%">
                    รหัส
                </th>
                <th width="15%">
                    ประเภทการทำรายการ
                </th>
                <th width="5%">
                    ฝั่งรายการ
                </th>
                <th width="6%">
                    ประเภทเงิน
                </th>
                <th width="6%">
                    กลุ่มรายการ
                </th>              
                <th width="3%">
                    สถานะ
                </th>
                <th width="3%">
                    ลำดับ
                </th>
                <th width="8%">
                    คู่บัญชี
                </th>
                <th width="4%">
                    Showtran Status
                </th>
                <th width="3%">
                    Showimport Status
                </th>
                <th width="5%">
                    โอนภายในแบ่งยอด
                </th>
                <th width="1.8%">
                    ลบ!
                </th>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td style="width:40px">
                            <asp:TextBox ID="RECPPAYTYPE_CODE" runat="server" Style="text-align: center;" MaxLength="3"></asp:TextBox>
                        </td>
                        <td style="width:153px">
                            <asp:TextBox ID="RECPPAYTYPE_DESC" runat="server" Style="text-align: left;"></asp:TextBox>
                        </td>
                        <td style="width:47px">
                             <asp:DropDownList ID="recppaytype_flag" runat="server" >
                                <asp:ListItem Text="ฝาก" Value="1"></asp:ListItem>
                                <asp:ListItem Text="ถอน" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width:48px">
                            <asp:DropDownList ID="moneytype_support" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="width:38px">
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
                        <td style="width:50px">
                            <asp:DropDownList ID="active_flag" runat="server" Style="text-align: center;">
                                <asp:ListItem Text="--กรุณาเลือก--"  Value=""></asp:ListItem>
                                <asp:ListItem Text="เปิด" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="ปิด" Value="0" ></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width:37px">
                            <asp:TextBox ID="order_sort" runat="server" Style="text-align: center;" onkeypress="check_number();" ></asp:TextBox>
                        </td>
                        <td  style="width:60px">
                            <asp:DropDownList ID="default_accid" runat="server" Style="text-align: center;"></asp:DropDownList>
                        </td>
                        <td align="center" style="width:62px">
                            <asp:CheckBox ID="showtran_status" runat="server" Style="text-align: center;" />
                        </td>
                        <td align="center" style="width:75px">
                            <asp:CheckBox ID="showimport_status" runat="server" Style="text-align: center;" />
                        </td>
                        <td align="center" style="width:75px">
                            <asp:CheckBox ID="chaecktran_status" runat="server" Style="text-align: center;" />
                        </td>
                        <td>
                            <asp:Button ID="b_del" runat="server" Text="ลบ" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>

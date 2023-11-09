<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_rloantype_principal_rperiod.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span>สหกรณ์:</span>
                    </div>
                </td>
                <td colspan="3" >
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>   
            <tr>
                <td width="25%">
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="S_MEMBGROUP" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถึงสังกัด:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="E_MEMBGROUP" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>                  
            <tr>
                <td width="25%">
                    <div>
                        <span>ประเภทเงินกู้:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="S_LOANTYPE" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถึงประเภทเงินกู้:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="E_LOANTYPE" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>         
            <tr>
                <td width="25%">
                    <div>
                        <span>วงเงินกู้:</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="S_CREDIT" runat="server" ToolTip="#,##0.00" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="25%">
                    <div>
                        <span>ถึงวงเงินกู้:</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="E_CREDIT" ToolTip="#,##0.00" Style="text-align: center" runat="server">                        
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <div>
                        <span>เงินกู้คงเหลือ:</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="S_PRINCIPAL" ToolTip="#,##0.00" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="25%">
                    <div>
                        <span>ถึงเงินกู้คงเหลือ:</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="E_PRINCIPAL" ToolTip="#,##0.00" Style="text-align: center" runat="server">                        
                    </asp:TextBox>
                </td>
            </tr>

             <tr>
                <td width="25%">
                    <div>
                        <span>ยื่นกู้:</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="S_LOANREQUEST" ToolTip="#,##0.00" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="25%">
                    <div>
                        <span>ถึงยื่นกู้:</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="E_LOANREQUEST" ToolTip="#,##0.00" Style="text-align: center" runat="server">                        
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <div>
                        <span>ผ่อนชำระ:</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="S_PAYMENT" ToolTip="#,##0.00" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="25%">
                    <div>
                        <span>ถึงผ่อนชำระ:</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="E_PAYMENT" ToolTip="#,##0.00" Style="text-align: center" runat="server">                        
                    </asp:TextBox>
                </td>
            </tr>

            <tr>
                <td width="25%">
                    <div>
                        <span>งวดคงเหลือ:</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="S_PERIOD" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="25%">
                    <div>
                        <span>ถึงงวดคงเหลือ:</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="E_PERIOD" runat="server" Style="text-align: center">                        
                    </asp:TextBox>
                </td>
            </tr>

            <tr>
                <td width="25%">
                    <div>
                        <span>มูลค่าหุ้น:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="s_share" runat="server" ToolTip="#,##0.00" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="25%">
                    <div>
                        <span>ถึง:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="e_share" runat="server" ToolTip="#,##0.00"  Style="text-align: center"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td width="25%">
                    <div>
                        <span>เพศ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="sex" runat="server">
                        <asp:ListItem Value="%">ทั้งหมด</asp:ListItem>
                        <asp:ListItem Value="M">ชาย</asp:ListItem>
                        <asp:ListItem Value="F">หญิง</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td width="25%">
                    <div>
                        <span>อายุ:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="s_age" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="25%">
                    <div>
                        <span>ถึง:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="e_age" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <div>
                        <span>เงินเดือน:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="s_salaly" ToolTip="#,##0.00" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="25%">
                    <div>
                        <span>ถึง:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="e_salaly" ToolTip="#,##0.00" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
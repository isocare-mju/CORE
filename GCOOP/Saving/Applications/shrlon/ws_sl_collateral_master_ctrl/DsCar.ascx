<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCar.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_ctrl.DsCar" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="FormStyle">
            <tr>
                <td>
                    <div>
                        <span>ประเภทรถยนต์ :</span></div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="car_type" runat="server">
                    <asp:ListItem Value="" Selected="">กรุณาเลือก</asp:ListItem>
                    <asp:ListItem Value="รถยนต์นั่งส่วนบุคคลไม่เกิน 7 คน"></asp:ListItem>
                    <asp:ListItem Value="รถยนต์นั่งส่วนบุคคลเกิน 7 คน" ></asp:ListItem>
                    <asp:ListItem Value="รถยนต์บรรทุกส่วนบุคคล" ></asp:ListItem>
                    <asp:ListItem Value="รถยนต์สามล้อส่วนบุคคล" ></asp:ListItem>
                    <asp:ListItem Value="รถยนต์รับจ้างระหว่างจังหวัด" ></asp:ListItem>
                    <asp:ListItem Value="รถยนต์รับจ้างบรรทุกคนโดยสารไม่เกิน 7 คน" ></asp:ListItem>
                    <asp:ListItem Value="รถยนต์สี่ล้อเล็กรับจ้าง" ></asp:ListItem>
                    <asp:ListItem Value="รถยนต์รับจ้างสามล้อ" ></asp:ListItem>
                    <asp:ListItem Value="รถยนต์บริการธุรกิจ" ></asp:ListItem>
                    <asp:ListItem Value="รถยนต์บริการทัศนาจร" ></asp:ListItem>
                    <asp:ListItem Value="รถยนต์บริการให้เช่า" ></asp:ListItem>
                    <asp:ListItem Value="รถจักรยานยนต์" ></asp:ListItem>
                    <asp:ListItem Value="รถแทรกเตอร์" ></asp:ListItem>
                    <asp:ListItem Value="รถบดถนน" ></asp:ListItem>
                    <asp:ListItem Value="รถใช้งานเกษตรกรรม" ></asp:ListItem>
                    <asp:ListItem Value="รถพ่วง" ></asp:ListItem>
                    <asp:ListItem Value="รถจักรยานยนต์สาธารณะ" ></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ยี่ห้อ :</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="car_brand" runat="server">
                    <asp:ListItem Value="" Selected="">กรุณาเลือก</asp:ListItem>
                    <asp:ListItem Value="Toyota"></asp:ListItem>
                    <asp:ListItem Value="Isuzu" ></asp:ListItem>
                    <asp:ListItem Value="Honda" ></asp:ListItem>
                    <asp:ListItem Value="Mitsubishi" ></asp:ListItem>
                    <asp:ListItem Value="Ford" ></asp:ListItem>
                    <asp:ListItem Value="Nissan" ></asp:ListItem>
                    <asp:ListItem Value="Mazda" ></asp:ListItem>
                    <asp:ListItem Value="Suzuki" ></asp:ListItem>
                    <asp:ListItem Value="MG" ></asp:ListItem>
                    <asp:ListItem Value="Chevrolet" ></asp:ListItem>
                    <asp:ListItem Value="Tata" ></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>รุ่นรถ :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="car_model" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สี :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="car_color" runat="server">
                    </asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>หมายเลขรถ :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="car_serialno" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขตัวเครื่อง :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="car_chassisno" runat="server">
                    </asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ขนาดรถยนต์ :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="car_enginesize" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

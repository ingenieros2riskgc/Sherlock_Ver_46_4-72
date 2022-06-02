<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ObjetivoEstrategico.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Gestion.ObjetivoEstrategico" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
#Background
    {
        position: fixed;
        top: 0px;
        bottom: 0px;
        left: 0px;
        right: 0px;
        overflow: hidden;
        padding: 0;
        margin: 0;
        background: #EEEEEE;
        filter: alpha(opacity=80);
        opacity: 0.8;
        z-index: 100000;
    }
    
    #Progress
    {
        position: fixed;
        top: 40%;
        left: 40%;
        height: 10%;
        width: 20%;
        z-index: 100001;
        background-color: #FFFFFF;
        border: 1px solid Gray;
        background-image: url('./Imagenes/Icons/loading.gif');
        background-repeat: no-repeat;
        background-position: center;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr>
                <td align="center">
                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                        DisplayAfter="0">
                        <ProgressTemplate>
                            <div id="Background">
                            </div>
                            <div id="Progress">
                                <asp:Label ID="Lbl11" runat="server" Text="Procesando, por favor espere..." Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                <br />
                                <asp:Image ID="Img11" runat="server" ImageUrl="~/Imagenes/Icons/loading.gif" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label7" runat="server" ForeColor="White" Text="Objetivos Estratégicos"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <table id="FiltroPE" runat="server" align="center" visible="true">
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewPlanEstratagico" runat="server" AutoGenerateColumns="False"
                                    BorderStyle="Solid" CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333"
                                    GridLines="Vertical" HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center"
                                    OnRowCommand="GridViewPlanEstratagico_RowCommand" ShowHeaderWhenEmpty="True"
                                    OnSelectedIndexChanged="GridViewPlanEstratagico_SelectedIndexChanged">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdPlan" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="CodigoPlan" HeaderText="Codigo" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Plan Estratégico" />
                                        <asp:ButtonField ButtonType="Image" CommandName="Filtrar" HeaderText="Acción" ImageUrl="~/Imagenes/Icons/select.png"
                                            Text="Filtrar">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        </caption>
                    </table>
                    <table id="FiltroAplicado" runat="server" align="center" visible="false">
                        <tr align="center" bgcolor="#5D7B9D">
                            <td colspan="4" bgcolor="#5D7B9D">
                                <asp:Label ID="Label24" runat="server" Text="Plan Estratégico" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="Id:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelIdPlan" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label19" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelCodigoPlan" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label22" runat="server" Text="Nombre:" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF; background-color: #5D7B9D"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelNombrePlan" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label21" runat="server" Text="Fecha inicio:" Font-Names="Calibri"
                                    Font-Size="Small" Visible="true" Style="color: #FFFFFF; background-color: #5D7B9D"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox12" runat="server" Enabled="false" Font-Names="Calibri" Font-Size="Small"
                                    Width="89px" Visible="true"></asp:TextBox>
                            </td>
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label26" runat="server" Text="Fecha fin:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="true" Style="color: #FFFFFF; background-color: #5D7B9D"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox11" runat="server" Enabled="false" Font-Names="Calibri" Font-Size="Small"
                                    Width="89px" Visible="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="VerPlanEstrategico" runat="server" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Text="Planes Estratégicos" OnClick="VerPlanEstrategico_Click"
                                    Width="126px" ToolTip="Ver Planes" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table id="TablePEstrategico" runat="server" align="center" visible="false">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnRowCommand="GridView1_RowCommand"
                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="CodigoObjetivo" HeaderText="Código" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="NombreDetalle" HeaderText="Perspectiva" />
                                        <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio" />
                                        <asp:BoundField DataField="FechaFin" HeaderText="Fecha Fin" />
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" Visible="False" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" Visible="False" />
                                        <asp:ButtonField ButtonType="Image" CommandName="Modificar" HeaderText="Modificar"
                                            ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="Eliminar" HeaderText="Eliminar"
                                            ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:ImageButton ID="BtnAdicionaPlan" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                    OnClick="BtnAdicionaPlan_Click" ToolTip="Agregar" ValidationGroup="addLista" />
                            </td>
                        </tr>
                        </caption>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TbModificarVision" runat="server" visible="false" align="center">
                        <tr align="center" bgcolor="#333399">
                            <td colspan="2">
                                <asp:Label ID="Label15" runat="server" Text="Modificar Objetivo Estratégico" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="medium" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label12" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox14" runat="server" Enabled="False" Font-Names="Calibri" Font-Size="Small"
                                    Width="97px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Descipción:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Width="404px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="TextBox2" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label20" runat="server" Text="Perspectiva:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="226px" Font-Names="Calibri"
                                    Font-Size="Small" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                    Height="19px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="DropDownList1" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Fecha inicio:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox5" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="96px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true" TargetControlID="TextBox5"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator6" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox5" ControlToCompare="TextBox12" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha inicio no puede ser menor a Fecha inicio del Plan Estratégico"
                                    Type="Date" Operator="GreaterThanEqual" ValidationGroup="updateLista">
                                </asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator7" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox5" ControlToCompare="TextBox11" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha inicio no puede ser mayor a Fecha fin del Plan Estratégico"
                                    Type="Date" Operator="LessThanEqual" ValidationGroup="updateLista">
                                </asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="TextBox5" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label17" runat="server" Text="Fecha fin:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox10" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="96px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true" TargetControlID="TextBox10"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox10" ControlToCompare="TextBox5" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha fin no puede ser menor a Fecha inicio" Type="Date"
                                    Operator="GreaterThanEqual" ValidationGroup="updateLista">
                                </asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator5" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox10" ControlToCompare="TextBox11" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha fin no puede ser mayor a Fecha fin del Plan Estratégico"
                                    Type="Date" Operator="LessThanEqual" ValidationGroup="updateLista">
                                </asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="TextBox10" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label10" runat="server" Text="Usuario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server" Width="156px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="TextBox2" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label9" runat="server" Text="Fecha Registro:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox6" runat="server" Width="155px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="TextBox6" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="BtnModificaPlan" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" OnClick="BtnModificaPlan_Click" ValidationGroup="updateLista"
                                                Style="height: 20px; width: 20px;" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="BtnCancelaModPlan" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="BtnCancelaModPlan_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TbAdicionarVision" runat="server" visible="false" align="center">
                        <tr align="center" bgcolor="#333399">
                            <td colspan="2">
                                <asp:Label ID="Label1" runat="server" Text="Adicionar Objetivo Estratégico" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="medium" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox13" runat="server" Enabled="False" Font-Names="Calibri" Font-Size="Small"
                                    Width="97px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Descipción:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Width="404px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="addObjetivos"
                                    ControlToValidate="TextBox1" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Perspectiva:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="226px" Font-Names="Calibri"
                                    Font-Size="Small" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator8" runat="server" ForeColor="Red" ControlToValidate="DropDownList2"
                                    ValidationGroup="addObjetivos" ValueToCompare="---" Type="String" Operator="NotEqual">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label11" runat="server" Text="Fecha inicio:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="TextBox3" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="96px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="TextBox3"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                                <asp:CompareValidator ID="ValidaFechaObjPlan" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox3" ControlToCompare="TextBox12" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha inicio no puede ser menor a Fecha inicio del Plan Estratégico"
                                    Type="Date" Operator="GreaterThanEqual" ValidationGroup="addObjetivos">
                                </asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox3" ControlToCompare="TextBox11" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha inicio no puede ser mayor a Fecha fin del Plan Estratégico"
                                    Type="Date" Operator="LessThanEqual" ValidationGroup="addObjetivos">
                                </asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="addObjetivos"
                                    ControlToValidate="TextBox3" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label14" runat="server" Text="Fecha fin:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox4" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="96px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" TargetControlID="TextBox4"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator3" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox4" ControlToCompare="TextBox3" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha fin no puede ser menor a Fecha inicio" Type="Date"
                                    Operator="GreaterThanEqual" ValidationGroup="addObjetivos">
                                </asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator4" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox4" ControlToCompare="TextBox11" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha fin no puede ser mayor a Fecha fin del Plan Estratégico"
                                    Type="Date" Operator="LessThanEqual" ValidationGroup="addObjetivos">
                                </asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="addObjetivos"
                                    ControlToValidate="TextBox4" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label16" runat="server" Text="Usuario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox8" runat="server" Width="156px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label18" runat="server" Text="Fecha Registro:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox9" runat="server" Width="155px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="BtnGuardaPlan" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" OnClick="BtnGuardaPlan_Click" ValidationGroup="addObjetivos"
                                                Style="height: 20px; width: 20px;" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="BtnCancelaPlan" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="BtnCancelaPlan_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" TargetControlID="btndummy"
            PopupControlID="pnlMsgBox" OkControlID="btnAceptar" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BorderWidth="1px" BackColor="White" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #333399">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        &nbsp;
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" ForeColor="White" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-ok.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox1" runat="server" TargetControlID="btndummy1"
            PopupControlID="pnlMsgBox1" OkControlID="btnAceptar1" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy1" runat="server" Text="Button1" Style="display: none" />
        <asp:Panel ID="pnlMsgBox1" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BorderWidth="1px" BackColor="White" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #333399">
                    <td colspan="2" align="center" runat="server" id="td1">
                        &nbsp;
                        <asp:Label ID="Label25" runat="server" Text="Atención" ForeColor="White" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo1" runat="server" ImageUrl="~/Imagenes/Icons/Alerta.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox1" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar1" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AsocCaracterizacion.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.AsocCaracterizacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<style type="text/css">
    .ajax__html_editor_extender_texteditor
    {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }

    .gridViewHeader a:link
    {
        text-decoration: none;
    }

    .style1
    {
        width: 100%;
    }

    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .Apariencia
    {
        width:100%;
    }

    .centerTable
    {
        margin-left: auto;
        margin-right: auto;
    }

    .centertdtr
    {
        text-align: center;
    }


    .righttdtr
    {
        text-align: right;
    }

    .right5D7B9D
    {
        text-align: right;
        background: #5D7B9D;
    }

    .lefttdtr
    {
        text-align: left;
    }

    .leftBBBBBB
    {
        text-align: left;
        background: #BBBBBB;
    }

    .Tablewidth
    {
        width: 100%;
    }

    .TablaEspecial
    {
        width: 100%;
        border: hidden;
        border: 0;
        vertical-align: middle;
    }

    .centerMiddle
    {
        text-align: center;
        vertical-align: middle;
    }

    .LeftMiddle
    {
        text-align: left;
        vertical-align: middle;
    }

    .Toptdtr
    {
        vertical-align: top;
    }
</style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <table align="center" width="80%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Asociación Caracterización" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr visible="false" runat="server">
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxId" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="CadenaValor">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label19" runat="server" Text="Cadena de Valor:" CssClass="Apariencia"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlCadenaValor" runat="server" Width="300px" CssClass="Apariencia" AutoPostBack="True"
                                    DataTextField="NombreCadenaValor" DataValueField="IdCadenaValor"
                                    OnSelectedIndexChanged="ddlCadenaValor_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCadenaValor" runat="server" ControlToValidate="ddlCadenaValor"
                                    ErrorMessage="Debe ingresar la cadena de valor." ToolTip="Debe ingresar la cadena de valor."
                                    ValidationGroup="iCaract" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Macroproceso">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                    CssClass="Apariencia" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                    >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvMacroProceso" runat="server" ControlToValidate="ddlMacroproceso"
                                    ErrorMessage="Debe ingresar el Macroproceso." ToolTip="Debe ingresar el Macroproceso."
                                    ValidationGroup="iCaract" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Proceso" runat="server" visible="false">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label7" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                    CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvProceso" runat="server" ControlToValidate="ddlProceso"
                                    ErrorMessage="Debe ingresar el Proceso." ToolTip="Debe ingresar el Proceso." Enabled="False"
                                    ValidationGroup="iCaract" ForeColor="Red" >*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Subproceso" runat="server" visible="false">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlSubproceso_SelectedIndexChanged"
                                    CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSubproceso" runat="server" ControlToValidate="ddlSubproceso"
                                    ErrorMessage="Debe ingresar el Subproceso." ToolTip="Debe ingresar el Subproceso." Enabled="False"
                                    ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="botonBuscar" runat="server">
                            <td class="centertdtr" colspan="2">
                                <asp:ImageButton ID="btnBuscar" runat="server" CausesValidation="true" CommandName="Insert" ValidationGroup="iCaract"
                                    ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png" Text="Insert" OnClick="btnBuscar_Click" ToolTip="Buscar"
                                     />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr id="filaDetalle" runat="server" visible="false">
                            <td style="background: #EEEEEE" align="center">
                                <asp:TabContainer ID="TabDetalles" runat="server" ActiveTabIndex="0" Font-Names="Calibri" Font-Size="Small" Width="800px">

                                    <asp:TabPanel ID="tpEntradas" Visible="false" runat="server" HeaderText="Entradas" Font-Names="Calibri" Font-Size="Small">
                                        <HeaderTemplate>
                                            Entradas
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tabla" style="width: 100%">
                                                <tr>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:Panel ID="checkBoxPanel1" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList">
                                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                                    <asp:TabPanel ID="tpActividades"  runat="server" HeaderText="Actividades" Font-Names="Calibri" Font-Size="Small">
                                        <HeaderTemplate>
                                            Actividades
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tabla" style="width: 100%">
                                                <tr>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:Panel ID="checkBoxPanel2" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList">
                                                            <asp:CheckBoxList ID="CheckBoxList2" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                                    <asp:TabPanel ID="tpSalidas" Visible="false" runat="server" HeaderText="Salidas" Font-Names="Calibri" Font-Size="Small">
                                        <HeaderTemplate>
                                            Salidas
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tabla" style="width: 100%">
                                                <tr>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:Panel ID="checkBoxPanel3" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList">
                                                            <asp:CheckBoxList ID="CheckBoxList3" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                                    <asp:TabPanel ID="tpRecursos" runat="server">
                                        <HeaderTemplate>Recursos</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tabla" style="width:100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtRecursos" CssClass="Apariencia" placeholder="Recursos necesarios para el proceso"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                                    <asp:TabPanel ID="tpNumerales" runat="server">
                                        <HeaderTemplate>Numerales</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tabla" style="width:100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtNumerales" CssClass="Apariencia" placeholder="Numerales de la norma ISO"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                                    <asp:TabPanel ID="tpResponsables" runat="server">
                                        <HeaderTemplate>Responsables</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tabla" style="width:100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtResponsables" CssClass="Apariencia" placeholder="Responsables de proceso"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                                    <asp:TabPanel ID="tpCodigo" runat="server">
                                        <HeaderTemplate>Código</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tabla" style="width:100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtCodigo" CssClass="Apariencia" placeholder="Entidad para la caracterización"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                                </asp:TabContainer>
                            </td>
                        </tr>
                        <tr id="filaBotones" align="center" visible="false" runat="server">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnImgInsertar_Click" ToolTip="Guardar" ValidationGroup="iProceso" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnImgActualizar_Click" ToolTip="Guardar" ValidationGroup="iProceso" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteModificaciones.ascx.cs" Inherits="ListasSarlaft.UserControls.Perfilamiento.ReporteModificaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .gridViewHeader a:link
    {
        text-decoration: none;
    }
    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }
    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }
    .scrollingCheckBoxList
    {
        border: 1px #808080 solid;
        margin: 10px 10px 10px 10px;
        height: 200px;
    }
    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Reporte de Modificaciones Perfilamiento"
                        Font-Bold="False" Font-Names="Calibri" width="500px" Font-Size="Large"></asp:Label>
                </td>
            </tr>  
        </table>
        <table id="tbGridSelccion" runat="server" align="center">
                        <tr align="center">

                            <td>
                                <table>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Lopcion" runat="server" Text="Tipo de reporte" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DDLopciones" runat="server" Width="250px" Font-Names="Calibri"
                                                Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DDLopciones_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--Seleccione una opción--</asp:ListItem>
                                                <asp:ListItem Value="1">Reporte de Variables</asp:ListItem>
                                                <asp:ListItem Value="2">Reporte de Categorías</asp:ListItem>
                                                <asp:ListItem Value="3">Reporte de Perfiles</asp:ListItem>
                                                <asp:ListItem Value="4">Reporte de Estructura de archivos</asp:ListItem>
                                                <asp:ListItem Value="5">Reporte de Señales de alerta</asp:ListItem>
                                                <asp:ListItem Value="6">Reporte de Concidencias de Señales de alerta</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                    </table>
                                     <%--Inicio del GRID de Variables--%>
        <div id="Div0" runat="server" visible="false" class="ColumnStyle">
            <table id="Tbuttons" class="tabla" align="center" width="25%">
                <tr align="center">
                    <td>Exportar a Excel:
                    </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExportVariables" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExportVariables_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <table align="center">
            
                        <tr align="center">
                            <td>
                                
                                <asp:Panel ID="Panel3" runat="server">
                                    <asp:GridView ID="gvTipoParametro" runat="server" CellPadding="4" EnableModelValidation="True"
                                        ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False"
                                        OnPageIndexChanging="gvTipoParametro_PageIndexChanging" OnRowCommand="gvTipoParametro_RowCommand"
                                        ShowHeaderWhenEmpty="True" BorderStyle="Solid" HeaderStyle-CssClass="gridViewHeader" 
                                        HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="StrIdVariable" HeaderText="IdVariable" Visible="False" />
                                            <asp:BoundField DataField="StrNombreVariable" HeaderText="Nombre Variable"  />
                                            <asp:BoundField DataField="StrCalificacion" HeaderText="Calificación" />
                                            <asp:TemplateField HeaderText="Activo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chbEsActivo" runat="server" Checked='<% # Bind("BooActivo") %>'
                                                    Enabled="false" />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                            <asp:BoundField DataField="StrIdUsuario" HeaderText="Id de Usuario " Visible="false"/>
                                            <asp:BoundField DataField="StrUsuario" HeaderText="Usuario " />
                                            <asp:BoundField DataField="StrFechaModificacion" HeaderText="Fecha de Modificación " />
                                        </Columns>

                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
        
                                     <%--Fin del GRID de Variables--%>
                                     <%--Inicio del GRID de categorías--%>
        <div id="Div1" runat="server" visible="false" class="ColumnStyle">
            <table id="Tbuttons" class="tabla" align="center" width="25%">
                <tr align="center">
                    <td>Exportar a Excel:
                    </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExportCategorias" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExportCategorias_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <table align="center">

            <tr align="center">
                            <td>
                                            
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:GridView ID="gvParametrizacion" runat="server" CellPadding="4" EnableModelValidation="True"
                                        ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="false"
                                        OnRowCommand="gvParametrizacion_RowCommand" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                        BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="StrIdCategoria" HeaderText="IdParametros" Visible="False" />
                                            <asp:BoundField DataField="StrNombreCategoria" HeaderText="Nombre Categoría" />
                                            <asp:BoundField DataField="StrCodigoCategoria" HeaderText="Código Categoría" />
                                            <asp:BoundField DataField="StrIdVariable" HeaderText="IdVariable" Visible="False" />
                                            <asp:BoundField DataField="StrNombreVariable" HeaderText="Nombre Variable" />
                                            <asp:BoundField DataField="StrCalificacionCategoria" HeaderText="Calificación Categoría" />
                                            <asp:BoundField DataField="BooEsFormula" HeaderText="Condición" Visible="False" />
                                            <asp:BoundField DataField="StrIdUsuario" HeaderText="Id de Usuario " Visible="false"/>
                                            <asp:BoundField DataField="StrUsuario" HeaderText="Usuario " />
                                            <asp:BoundField DataField="StrFechaModificacion" HeaderText="Fecha de Modificación " />
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
        
                                     <%--Fin del GRID de categorías--%>
                                     <%--Inicio del GRID de perfiles--%>
        <div id="Div2" runat="server" visible="false" class="ColumnStyle">
            <table id="Tbuttons" class="tabla" align="center" width="25%">
                <tr align="center">
                    <td>Exportar a Excel:
                    </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExportPerfiles" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExportPerfiles_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <table align="center">

            <tr align="center">
                <td>
                    <table>
                        <tr>
                            <td>
                                            
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:GridView ID="gvPerfiles" runat="server" CellPadding="4" EnableModelValidation="True"
                                        ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False"
                                        OnPageIndexChanging="gvPerfiles_PageIndexChanging" OnRowCommand="gvPerfiles_RowCommand"
                                        ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid"
                                        HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="StrIdPerfil" HeaderText="IdPerfil" Visible="False" />
                                            <asp:BoundField DataField="StrNombrePerfil" HeaderText="Nombre Perfil" />
                                            <asp:BoundField DataField="StrValorMinimo" HeaderText="Rango Mínimo >=" />
                                            <asp:BoundField DataField="StrValorMaximo" HeaderText="Rango Máximo <" />
                                            <asp:BoundField DataField="StrIdUsuario" HeaderText="Id de Usuario " Visible="false"/>
                                            <asp:BoundField DataField="StrUsuario" HeaderText="Usuario " />
                                            <asp:BoundField DataField="StrFechaModificacion" HeaderText="Fecha de Modificación " />
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
                                    <%--Fin del GRID de perfiles--%>
                                    <%--Inicio del GRID de Estructura de Archivos--%>
        <div id="Div3" runat="server" visible="false" class="ColumnStyle">
            <table id="Tbuttons" class="tabla" align="center" width="25%">
                <tr align="center">
                    <td>Exportar a Excel:
                    </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExportEstructuraArchivos" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExportEstructuraArchivos_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <table align="center">

                        <tr align="center">
                            <td>
                                          
                                <asp:Panel ID="Panel4" runat="server">
                                    <asp:GridView ID="gvEstructura" runat="server" CellPadding="4" EnableModelValidation="True"
                                        ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False"
                                        OnRowCommand="gvEstructura_RowCommand" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                        BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="StrIdEstructCampo" HeaderText="IdParametros" HeaderStyle-CssClass="no-visible" ItemStyle-CssClass="no-visible"  />
                                            <asp:BoundField DataField="StrNombreCampo" HeaderText="Nombre Campo" />
                                            <asp:BoundField DataField="StrLongitud" HeaderText="Longitud" Visible="False" />
                                            <asp:TemplateField HeaderText="Parametrizable">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chbParametrizable" runat="server" Checked='<% # Bind("BooEsParametrico") %>'
                                                        Enabled="false" />
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StrIdVariable" HeaderText="IdVariable" Visible="False" />
                                            <asp:BoundField DataField="StrNombreParametro" HeaderText="Nombre Parametro" />
                                            <asp:BoundField DataField="StrIdTipoDato" HeaderText="IdTipoDato" Visible="False" />
                                            <asp:BoundField DataField="StrPosicion" HeaderText="Posición" />
                                            <asp:TemplateField HeaderText="Numerico">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkNumerico" runat="server" 
                                                        Enabled="false"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StrIdUsuario" HeaderText="Id de Usuario " Visible="false"/>
                                            <asp:BoundField DataField="StrUsuario" HeaderText="Usuario " />
                                        <asp:BoundField DataField="StrFechaModificacion" HeaderText="Fecha de Modificación" />
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
        
                                    <%--Fin del GRID de Estructura de Archivos--%>
                                    <%--Inicio del GRID de Señales de alerta--%>
        <div id="Div4" runat="server" visible="false" class="ColumnStyle">
            <table id="Tbuttons" class="tabla" align="center" width="25%">
                <tr align="center">
                    <td>Exportar a Excel:
                    </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExportSenales" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExportSenales_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <table id="TblMostrarSenales" runat="server" align="center">
                        <tr>
                            <td>
                                            
                                <asp:GridView ID="gvSenales" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnPageIndexChanging="gvSenales_PageIndexChanging"
                                    OnRowCommand="gvSenales_RowCommand" ShowHeaderWhenEmpty="True" AllowPaging="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="StrIdSenal" HeaderText="IdSenal" Visible="false" />
                                        <asp:BoundField DataField="StrCodigoSenal" HeaderText="Código Señal" />
                                        <asp:BoundField DataField="StrDescripcionSenal" HeaderText="Descripción Señal" />
                                        <asp:BoundField DataField="StrIdUsuario" HeaderText="Id de Usuario " Visible="false"/>
                                            <asp:BoundField DataField="StrUsuario" HeaderText="Usuario " />
                                        <asp:BoundField DataField="StrFechaModificacion" HeaderText="Fecha de Modificación" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                
                    </table>
        
        
                                    <%--Fin del GRID de Señales de alerta--%>
                                    <%--Inicio del GRID de Coincidencias de Señales de alerta--%>
        <div id="Div5" runat="server" visible="false" class="ColumnStyle">
            <table id="Tbuttons" class="tabla" align="center" width="25%">
                <tr align="center">
                    <td>Exportar a Excel:
                    </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExportConsulSenales" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExportConsulSenales_Click" />
                    </td>
                </tr>
            </table>
        </div>
   
        <table id="ConsulSenales" runat="server" align="center">
                        <tr>
                            <td>
                                            
                                <asp:GridView ID="gvConsulSenales" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" align="center" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnPageIndexChanging="gvConsulSenales_PageIndexChanging"
                                    OnRowCommand="gvConsulSenales_RowCommand" ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="StrIdSenal" HeaderText="IdSenal" Visible="false" />
                                        <asp:BoundField DataField="StrCodigoSenal" HeaderText="Código Señal" />
                                        <asp:BoundField DataField="StrDescripcionSenal" HeaderText="Descripción Señal" />
                                        <asp:BoundField DataField="StrFechaInicial" HeaderText="Fecha Inicial" />
                                        <asp:BoundField DataField="StrFechaFinal" HeaderText="Fecha Final" />
                                        <asp:BoundField DataField="StrNumeroCoincidencias" HeaderText="Cantidad de Coincidencias" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="StrIdUsuario" HeaderText="Id de Usuario " Visible="false"/>
                                            <asp:BoundField DataField="StrUsuario" HeaderText="Usuario " />
                                        <asp:BoundField DataField="StrFechaConsulta" HeaderText="Fecha de Consulta" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                
                    </table>
                                    <%--Fin del GRID de Coincidencias de Señales de alerta--%>


        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" TargetControlID="btndummy"
            PopupControlID="pnlMsgBox" OkControlID="btnAceptar" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BackColor="#FFFFFF" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #5D7B9D">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        &nbsp;
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Names="Calibri" Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
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
    </ContentTemplate>
</asp:UpdatePanel>

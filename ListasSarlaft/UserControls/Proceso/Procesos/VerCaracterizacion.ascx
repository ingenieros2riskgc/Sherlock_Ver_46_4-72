<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VerCaracterizacion.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Procesos.VerCaracterizacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />

<style>
    .ellipsis {
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
    }

    .table-center {
        margin-left: auto;
        margin-right: auto;
        width: 90%;
    }

    td {
        font-size: 12px;
        font-family: Calibri;
    }
</style>
<asp:UpdatePanel ID="VCbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadVC" runat="server">
            <asp:Label ID="LtituloVC" runat="server" ForeColor="White" Text="Visualización de la Caracterización" Font-Bold="False"
                Font-Names="Calibri" Font-Size="Large"></asp:Label>
            <asp:Label ID="LidProcesoT" runat="server" ForeColor="White" Text="" Font-Bold="False"
                Font-Names="Calibri" Font-Size="Large" Visible="false"></asp:Label>
        </div>
        <div id="BodyFormGEC" class="ColumnStyle" runat="server" visible="true">
            <div id="form" class="TableContains">
                <table class="tabla" align="center" width="80%">
                    <tr id="CadenaValor">
                        <td class="RowsText">
                            <asp:Label ID="Label19" runat="server" Text="Cadena de Valor:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlCadenaValor" runat="server" Width="300px"
                                CssClass="Apariencia" AutoPostBack="True"
                                DataTextField="NombreCadenaValor" DataValueField="IdCadenaValor"
                                OnSelectedIndexChanged="ddlCadenaValor_SelectedIndexChanged">
                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvCadenaValor" runat="server" ControlToValidate="ddlCadenaValor"
                                ErrorMessage="Debe ingresar la cadena de valor." ToolTip="Debe ingresar la cadena de valor."
                                ValidationGroup="Caracterizacion" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="tbxProcIndica" runat="server" Width="90px" MaxLength="20" CssClass="Apariencia" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="Macroproceso">
                        <td class="RowsText">
                            <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                OnSelectedIndexChanged="ddlMacroproceso_SelectedIndexChanged">
                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvMacroProceso" runat="server" ControlToValidate="ddlMacroproceso"
                                ErrorMessage="Debe ingresar el Macroproceso." ToolTip="Debe ingresar el Macroproceso."
                                ValidationGroup="Caracterizacion" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ImageButton ID="btnSearchCarac" runat="server" CausesValidation="true" ValidationGroup="Caracterizacion" CommandName="Search"
                                ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png" Text="Search" OnClick="btnSearchCarac_Click" ToolTip="Buscar" Visible="false" />
                            <asp:ImageButton ID="btnImgCancelar" Visible="false" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div id="Dbutton" runat="server" visible="false" class="ColumnStyle">
            <table id="Tbuttons" class="tabla" align="center" width="25%">
                <tr align="center">
                    <td>Para exportar a PDF:
                    </td>
                    <td>
                        <asp:ImageButton ID="ImButtonPDFexport" runat="server" ImageUrl="~/Imagenes/Icons/pdfImg.jpg" OnClick="ImButtonPDFexport_Click" />
                    </td>
                    <td>Para exportar a Excel:
                    </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExport" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExport_Click" />
                    </td>
                </tr>
            </table>
        </div>

        <div id="GridVC" runat="server" visible="false" class="ColumnStyle">

            <asp:GridView ID="GVheader" runat="server" CellPadding="4"
                ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                CssClass="table-center" Font-Bold="False" PageSize="4">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>

                    <asp:TemplateField HeaderText="Código Aplicativo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lCodigoAplicativo" runat="server" Text='<% # Bind("Codigo")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Código Caracterización" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="intId" runat="server" Text='<% # Bind("intId")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nombre Proceso" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="strnombreProceso" runat="server" Width="200px" CssClass="ellipsis" Text='<% # Bind("strnombreProceso")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Objetivo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="strObjetivo" runat="server" Width="600px" CssClass="ellipsis" Text='<% # Bind("strObjetivo")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre Responsable" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible">
                        <ItemTemplate>
                            <asp:Label ID="strNombreResponsable" runat="server" Width="200px" CssClass="ellipsis" Text='<% # Bind("NombreResponsable")%>' ToolTip="Para funciones y responsabilidades consultar perfil en submódulo parametrización."></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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

        </div>

        <div id="GridVCdetalle" runat="server" visible="false" class="ColumnStyle">
           <%--  <asp:GridView ID="GVVCdetalle" runat="server" CellPadding="4"
                ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Both"
                CssClass="table-center" Font-Bold="False" PageSize="4" OnPreRender="GVVCdetalle_PreRender">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>

                    <asp:TemplateField HeaderText="Descripción Entrada" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="strDescripcionEntrada" runat="server" CssClass="ellipsis" Text='<% # Bind("strDescripcionEntrada")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" Width="150" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Proveedor" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="strProveedor" runat="server" CssClass="ellipsis" Text='<% # Bind("strProveedor")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" Width="150" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción Actividad" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="strDescripcionActividad" runat="server" CssClass="ellipsis" Text='<% # Bind("strDescripcionActividad")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" Width="150" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cargo Responsable" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible">
                        <ItemTemplate>
                            <asp:Label ID="strCargoResponsable" runat="server" CssClass="ellipsis" Text='<% # Bind("strCargoResponsable")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" Width="150" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Descripción Salida" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="strDescripcionSalida" runat="server" CssClass="ellipsis" Text='<% # Bind("strDescripcionSalida")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" Width="150" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cliente" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                <asp:Label ID="strCliente" runat="server" Text='<% # Bind("strCliente")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" Width="150" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Descripción Procedimiento" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" >
                        <ItemTemplate>
                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                <asp:Label ID="strDescripcionProcedimiento" runat="server" Text='<% # Bind("strDescripcionProcedimiento")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" Width="150" />
                    </asp:TemplateField>
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
            </asp:GridView>--%>
            
                        <div class="ColumnStyle">
                    <asp:GridView ID="GVVCentradas" runat="server" CellPadding="4"
                ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Both"
                CssClass="table-center" Font-Bold="False" PageSize="4" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>

                    <asp:TemplateField HeaderText="Descripción Entrada" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="strDescripcionEntrada" Style="word-wrap: normal; word-break: break-all;" runat="server" Width="150px"
                                CssClass="ellipsis" Text='<% # Bind("strDescripcionEntrada")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" Width="150" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Proveedor" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="strProveedor" runat="server" CssClass="ellipsis" Text='<% # Bind("strProveedor")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" Width="150" />
                    </asp:TemplateField>
                    
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
                </div>
                    
                         <div class="ColumnStyle">
                    <asp:GridView ID="GVVCactividades" runat="server" CellPadding="4"
                ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Both"
                CssClass="table-center" Font-Bold="False" PageSize="4" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>

                    <asp:TemplateField HeaderText="Descripción Actividad" ItemStyle-HorizontalAlign="Left" 
                        ItemStyle-Wrap="true" >
                        <ItemTemplate>
                            <asp:Label ID="strDescripcionActividad" runat="server" 
                                Width="350px"
                                Text='<% # Bind("strDescripcionActividad")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="true" Width="150"  />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción PHVA" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                <asp:Label ID="strDescripcionPHVA" Style="word-wrap: normal; word-break: break-all;" runat="server" Text='<% # Bind("strDescripcionPHVA")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" Width="150" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cargo Responsable" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="strCargoResponsable" runat="server" CssClass="ellipsis" Text='<% # Bind("strCargoResponsable")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" Width="150" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción Procedimiento" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            
                                <asp:Label ID="strDescripcionProcedimiento" Style="word-wrap: normal; word-break: break-all;"
                                    Width="250px"
                                    runat="server" Text='<% # Bind("strDescripcionProcedimiento")%>'></asp:Label>
                            
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="true" Width="150" />
                    </asp:TemplateField>
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
                </div>
                   
<div class="ColumnStyle">
                    <asp:GridView ID="GVVCsalidas" runat="server" CellPadding="4"
                ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Both"
                CssClass="table-center" Font-Bold="False" PageSize="4">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="Descripción Salida" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="strDescripcionSalida" runat="server" CssClass="ellipsis" Text='<% # Bind("strDescripcionSalida")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="true" Width="150" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cliente" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <div style="text-overflow: ellipsis;  width: 150px">
                                <asp:Label ID="strCliente" Style="word-wrap: normal; word-break: break-all;"
                                    runat="server" Text='<% # Bind("strCliente")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                        <ItemStyle Wrap="true" Width="150" />
                    </asp:TemplateField>
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
                </div>
                    
         
            
        </div>

        <div id="divCamposCaracterizacion" runat="server" visible="true" class="ColumnStyle">
            <asp:GridView ID="gvCamposCaracterizacion" runat="server" CellPadding="4" AutoGenerateColumns="false"
                ForeColor="#333333" AllowSorting="false" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Both"
                CssClass="table-center" Font-Bold="false" AllowPaging="false">
                <EditRowStyle BackColor="#999999" />
                <Columns>
                    <asp:BoundField DataField="Recursos" HeaderText="Recursos" />
                    <asp:BoundField DataField="Numerales" HeaderText="Numerales" />
                    <asp:BoundField DataField="Responsables" HeaderText="Responsables" />
                </Columns>
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
        </div>

        <div id="divDocumentos" class="ColumnStyle" runat="server" visible="true">
            <asp:GridView ID="gvDocumentos" runat="server" CellPadding="4" AutoGenerateColumns="false"
                ForeColor="#333333" AllowSorting="false" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Both"
                CssClass="table-center" Font-Bold="false">
                <EditRowStyle BackColor="#999999" />
                <Columns>
                    <asp:BoundField DataField="CodigoDocumento" HeaderText="Código Documento" />
                    <asp:BoundField DataField="NombreDocumento" HeaderText="Nombre Documento" />
                    <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Documento" />
                    <asp:BoundField DataField="FechaImplementacion" HeaderText="Fecha Implementación" />
                    <asp:BoundField DataField="NombreResponsable" HeaderText="Nombre Responsable" />
                    <asp:BoundField DataField="CargoResponsable" HeaderText="Cargo Responsable" />
                </Columns>
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
        </div>
        <div id="DindicadorRiesgo" runat="server" visible="false" class="ColumnStyle">
            <asp:GridView ID="GVindicadorRiesgo" runat="server" CellPadding="4"
                ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="None"
                CssClass="table-center" Font-Bold="False" PageSize="4" OnPreRender="GVindicadorRiesgo_PreRender" OnRowCommand="GVindicadorRiesgo_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>

                    <asp:TemplateField HeaderText="Código Indicador" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="IdIndicador" runat="server"  Text='<% # Bind("intIdIndicador")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nombre Indicador" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="NombreIndicador" runat="server" Width="75px"  Text='<% # Bind("strNombreIndicador")%>' ToolTip='<% # Bind("strNombreIndicador")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>

                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar"
                        HeaderText="Seleccionar" CommandName="SeleccionarIndicador" ItemStyle-HorizontalAlign="Center" />
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
        </div>
        <div id="Driesgos" runat="server" visible="false" class="ColumnStyle">
            <asp:GridView ID="GVRiesgos" runat="server" CellPadding="4"
                ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="None"
                CssClass="table-center" Font-Bold="False" PageSize="4" OnPreRender="GVRiesgos_PreRender" OnRowCommand="GVRiesgos_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="Código Riesgo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="CodigoRiesgo" runat="server" Text='<% # Bind("strCodigoRiesgo")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="center" Wrap="false" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre Riesgo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="NombreRiesgo" runat="server" Text='<% # Bind("strNombreRiesgo")%>' ToolTip='<% # Bind("strNombreRiesgo")%>' Width="300px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="center" Wrap="false" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>
                    <asp:ButtonField ButtonType="Image" CommandName="SeleccionarRiesgo" HeaderText="Seleccionar" ImageUrl="~/Imagenes/Icons/select.png" ItemStyle-HorizontalAlign="Center" Text="Seleccionar" />
                    <asp:TemplateField HeaderText="Código Control" ItemStyle-HorizontalAlign="center" Visible="false">
                        <ItemTemplate>
                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                <asp:Label ID="CodigoControl" runat="server" Text='<% # Bind("strCodigoControl")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="center" Wrap="false" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>
                    <asp:ButtonField ButtonType="Image" CommandName="SeleccionarControl" HeaderText="Seleccionar" ImageUrl="~/Imagenes/Icons/select.png" ItemStyle-HorizontalAlign="Center" Text="Seleccionar" Visible="false" />
                    <asp:TemplateField HeaderText="Nombre Control" ItemStyle-HorizontalAlign="center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="NombreControl" runat="server" CssClass="ellipsis" Text='<% # Bind("strNombreControl")%>' ToolTip='<% # Bind("strNombreControl")%>' Width="300px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="center" Wrap="false" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>
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
        </div>

        <div id="hidden" runat="server" visible="false">
            <asp:GridView ID="GVindicadorRiesgoPrint" runat="server" CellPadding="4"
                ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                CssClass="Apariencia" Font-Bold="False" PageSize="4" OnPreRender="GVindicadorRiesgo_PreRender" OnRowCommand="GVindicadorRiesgo_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="Nombre Indicador" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                <asp:Label ID="strNombreIndicador" runat="server" Text='<% # Bind("strNombreIndicador")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="Nombre Riesgo" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                          <asp:Label ID="strNombreRiesgo" runat="server" Text='<% # Bind("strNombreRiesgo")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>--%>

                  <%--   <asp:TemplateField HeaderText="Código Control" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                <asp:Label ID="strCodigoControl" runat="server" Text='<% # Bind("strCodigoControl")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre Control" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                <asp:Label ID="strNombreControl" runat="server" Text='<% # Bind("strNombreControl")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>--%>
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
        </div>

        <div id="hidden2" runat="server" visible="false">
            <asp:GridView ID="GVRiesgosPrint" runat="server" CellPadding="4"
                ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                CssClass="Apariencia" Font-Bold="False" PageSize="4" OnPreRender="GVRiesgos_PreRender" OnRowCommand="GVRiesgos_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="Nombre Riesgo" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <div style="overflow: hidden2; text-overflow: ellipsis; white-space: nowrap;">
                                <asp:Label ID="strNombreRiesgo" runat="server" Text='<% # Bind("strNombreRiesgo")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                        <ItemStyle Wrap="false" />
                    </asp:TemplateField>
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
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in 
the Page_Load method of the user control.--%>
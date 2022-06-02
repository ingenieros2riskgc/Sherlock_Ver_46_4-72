<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RPTreporteDocumentosVigentes.ascx.cs" Inherits="ListasSarlaft.UserControls.Reportes.RPTreporteDocumentosVigentes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="CIbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadCI" runat="server">
            <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Reportes Documentos y Registros Vigentes" Font-Bold="False"
                Font-Names="Calibri" Font-Size="Large"></asp:Label>
        </div>
        <div id="form" class="TableContains" runat="server">
            <table class="tabla" align="center" width="80%">
                <tr>
                    <td class="RowsText">
                        <asp:Label ID="LTipoDocumento" runat="server" Text="Tipo Documento:" CssClass="Apariencia"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:DropDownList ID="DDLtipo" runat="server" Width="300px"
                            CssClass="Apariencia">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvTipoDocumento" runat="server" ControlToValidate="DDLtipo"
                            ErrorMessage="Debe seleccionar un tipo de documento." ToolTip="Debe seleccionar un tipo de documento."
                            ValidationGroup="ControlDocument" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:ImageButton ID="IBsearch" runat="server" CausesValidation="true" CommandName="Buscar"
                            ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png" Text="Insert" ValidationGroup="ControlDocument" ToolTip="Buscar" OnClick="IBsearch_Click" />

                        <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                            ToolTip="Cancelar" OnClick="btnImgCancelar_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="BodyGridRD" class="ColumnStyle" runat="server">
            <table class="tabla" align="center" width="100%">
                <tr align="center">
                    <td>
                        <asp:GridView ID="GVcontrolDocumento" runat="server" CellPadding="4"
                            ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True"
                            HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                            CssClass="Apariencia" Font-Bold="False" OnPageIndexChanging="GVcontrolDocumento_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="intId" runat="server" Text='<% # Bind("intId")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strNombreDocumento" runat="server" Text='<% # Bind("strNombreDocumento")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Archivo" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strNombreArchivo" runat="server" Text='<% # Bind("strNombreArchivo")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código Documento" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="CodigoDocumento" runat="server" Text='<% # Bind("strCodigoDocumento")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Implementación" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strFechaImplementacion" runat="server" Text='<% # Bind("strFechaImplementacion")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Versión" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strVersion" runat="server" Text='<% # Bind("strVersion")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Proceso" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strNombreProceso" runat="server" Text='<% # Bind("strNombreProceso")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Responsable" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strNombreResponsable" runat="server" Text='<% # Bind("strNombreResponsable")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Modificación" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strFechaModificacion" runat="server" Text='<% # Bind("strFechaModificacion")%>'></asp:Label>
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



                        <asp:GridView ID="GVprint" runat="server" CellPadding="4"
                            ForeColor="#333333" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True"
                            HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                            CssClass="Apariencia" Font-Bold="False" Visible="false">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="intId" runat="server" Text='<% # Bind("intId")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strNombreDocumento" runat="server" Text='<% # Bind("strNombreDocumento")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Archivo" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strNombreArchivo" runat="server" Text='<% # Bind("strNombreArchivo")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código Documento" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="CodigoDocumento" runat="server" Text='<% # Bind("strCodigoDocumento")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Implementación" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strFechaImplementacion" runat="server" Text='<% # Bind("strFechaImplementacion")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Versión" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strVersion" runat="server" Text='<% # Bind("strVersion")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Proceso" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strNombreProceso" runat="server" Text='<% # Bind("strNombreProceso")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Responsable" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strNombreResponsable" runat="server" Text='<% # Bind("strNombreResponsable")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Modificación" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strFechaModificacion" runat="server" Text='<% # Bind("strFechaModificacion")%>'></asp:Label>
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
                    </td>
                </tr>
            </table>
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
                        <asp:ImageButton ID="ImButtonExcelExport" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExport_Click" Style="width: 26px" />
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Encuestas.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Param.Encuestas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<script type="text/javascript">
    function recibe(valor) {
        document.getElementById('guarda').innerHTML = valor;

    }
</script>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .auto-style1 {
        width: 14px;
    }
</style>
<asp:UpdatePanel ID="Encbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div id="guarda" style="display: none;"></div>
        <div>
            
        <div class="TituloLabel" id="HeadEnc" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Gestión de Encuestas" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridEnc" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVencuestas" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="strUsuario,dtFechaRegistro"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVencuestas_RowCommand" OnPageIndexChanging="GVencuestas_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intIdEncuesta" HeaderText="Código" SortExpression="intIdEncuesta" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:TemplateField HeaderText="Nombre Encuesta" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreEncuesta" runat="server" Text='<% # Bind("strNombreEncuesta")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Descripción Empresa" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="DescripcionEmpresa" runat="server" Text='<% # Bind("strDescripcionEmpresa")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intCantPreguntas" HeaderText="Cantidad de Preguntas" SortExpression="intCantPreguntas" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="true" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha Registro" SortExpression="dtFechaRegistro" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="Id usuario" SortExpression="intIdUsuario" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar" HeaderText="Modificar" CommandName="Modificar" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Preguntas" HeaderText="Preguntas" CommandName="Preguntas" ItemStyle-HorizontalAlign="Center" />
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
                <tr>
                    <td>
                        <asp:ImageButton ID="btnInsertarNuevo" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnInsertarNuevo_Click" ToolTip="Insertar" />
                    </td>
                </tr>
            </Table>
        </div>
        <div id="BodyFormEnc" class="ColumnStyle" runat="server" visible="false">
            <div id="form" class="TableContains">
                <Table class="tabla" align="center" width="80%">
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lcodigo" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtId" runat="server" Enabled="False"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td class="auto-style1"></td>
                        </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lnombre" runat="server" Text="Nombre de la Encuesta:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                
                               <asp:TextBox ID="TXnombre" runat="server" Width="300px"></asp:TextBox> 
                            </td>
                            <td class="auto-style1">
                                <asp:RequiredFieldValidator ID="RFVciudad" runat="server" ControlToValidate="TXnombre"
                                    ErrorMessage="Debe ingresar el nombre de la encuesta." ToolTip="Debe ingresar el nombre de la encuesta."
                                    ValidationGroup="Encuesta" ForeColor="Red">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="Ldescripcion" runat="server" Text="Descripcion de la empresa:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                
                               <asp:TextBox ID="TXdescripcion" runat="server" Width="300px" Height="75px" TextMode="MultiLine"></asp:TextBox> 
                            </td>
                            <td class="auto-style1">
                                <asp:RequiredFieldValidator ID="RFVdescripcion" runat="server" ControlToValidate="TXdescripcion"
                                    ErrorMessage="Debe ingresar la descripcion de la empresa." ToolTip="Debe ingresar la descripcion de la empresa."
                                    ValidationGroup="Encuesta" ForeColor="Red">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="LcantPregunta" runat="server" Text="Cantidad Preguntas:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXcantPregunta" runat="server" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td class="auto-style1">
                                <asp:RequiredFieldValidator ID="RFVcantPregunta" runat="server" ControlToValidate="TXcantPregunta"
                                    ErrorMessage="Debe ingresar la cantidad de Preguntas." ToolTip="Debe ingresar la cantidad de Preguntas."
                                    ValidationGroup="Encuesta" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revCantPre" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="TXcantPregunta" ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ValidationGroup="Encuesta"
                                    ErrorMessage="Ingresar solamente números enteros" ToolTip="Ingresar solamente números enteros">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lusuario" runat="server" Text="Usuario Creación:" CssClass="Apariencia" Width="300px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioCreacion" runat="server" Width="300px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="LfechaCreacion" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" Width="300px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                    
                    </table>
            </div>
        </div>
        <div id="DVpreguntas" class="ColumnStyle" runat="server" visible="false">
            <div class="TituloLabel" id="DVtituloQuestions" runat="server">
                    <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Preguntas de Encuesta" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
            <div id="Dquestions" class="TableContains" runat="server">
           
                <table runat="server" id="GridQuestions" align="center">
                    <tr>
                        <td align="center">
                             <asp:GridView ID="GVpreguntas" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVpreguntas_RowCommand" OnPageIndexChanging="GVpreguntas_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intIdPregunta" HeaderText="Código" SortExpression="intIdPregunta" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Texto Pregunta" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strTextoPregunta" runat="server" Text='<% # Bind("strTextoPregunta")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intConsecutivo" HeaderText="Consecutivo" SortExpression="intConsecutivo" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="true" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar" HeaderText="Modificar" CommandName="Modificar" ItemStyle-HorizontalAlign="Center" />
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
            <div id="preguntas" class="TableContains" runat="server">
                
                <table runat="server" id="tbuttonsQUestion" align="center">
                    <tr>
                        <td align="center">
<asp:ImageButton ID="IBnewQuestion" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" ToolTip="Insertar" OnClick="IBnewQuestion_Click" BorderStyle="None" />
                        </td>
                        <td>
                            <asp:ImageButton ID="IBcancelarEncuesta" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                        </td>
                    </tr>
                </table>
                <Table class="tabla" align="center" width="80%" runat="server" id="FormQuestion" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="TXidQuestion" runat="server" Visible="false"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Label2" runat="server" Text="Consecutivo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXconsecutivo" runat="server" Enabled="False"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="Label3" runat="server" Text="Pregunta de la Encuesta:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                
                               <asp:TextBox ID="TXpregunta" runat="server" Width="300px"></asp:TextBox> 
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RFVpregunta" runat="server" ControlToValidate="TXpregunta"
                                    ErrorMessage="Debe ingresar la pregunta de la encuesta." ToolTip="Debe ingresar la pregunta de la encuesta."
                                    ValidationGroup="EncuestaQuestion" ForeColor="Red">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                    </table>
                </div>
            <div id="DVbutonQuestion" class="ColumnStyle" runat="server" visible="false">
            <div id="ButtonsContentsQ" class="TableContains">
                <Table class="tabla" align="center" width="80%" id="Table1" runat="server">
                <tr>
                        <td>
                            
                            <asp:ImageButton ID="IBinsertQuestions" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="EncuestaQuestion" ToolTip="Insertar Pregunta" Visible="false" OnClick="IBinsertQuestions_Click" />

            <asp:ImageButton ID="IBupdateQuestions" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="EncuestaQuestion" ToolTip="Actualizar Pregunta" Visible="false" OnClick="IBupdateQuestions_Click"/>
            <asp:ImageButton ID="IBcancelQuestions" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                     ToolTip="Cancelar Pregunta" OnClick="IBcancelQuestions_Click" />
                        </td>
                    </tr>
            </table>
        </div>
            </div>
            </div>
        <div id="DVbuttons" class="ColumnStyle" runat="server" visible="false">
            <div id="ButtonsContents" class="TableContains">
                <Table class="tabla" align="center" width="80%" id="questionContents" runat="server">
                <tr>
                        <td>
                            <asp:ImageButton ID="IBinsertGVC" runat="server" CausesValidation="true" CommandName="Insert Encuesta"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="Encuesta" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click"/>
            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="GEvaProveedor" ToolTip="Actualizar Encuesta" Visible="false" OnClick="IBupdateGVC_Click" Width="20px"/>
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar Encuesta" />
                        </td>
                    </tr>
            </table>
        </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

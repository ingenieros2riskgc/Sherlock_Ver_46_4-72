<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultExcepciones.aspx.cs"
    Inherits="ListasSarlaft.Formularios.ManejoExcepciones.DefaultExcepciones" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Excepción</title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div align="center">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Aplicacion/underConstruction.jpg" />
                </div>
                <br />
                <div align="center">
                    <asp:Label ID="Label1" runat="server" Text="LA PAGINA SOLICITADA NO SE ENCUENTRA DISPONIBLE"
                        ForeColor="#D44242" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                </div>
                <br />
                <div align="center">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/backhome.png"
                        PostBackUrl="~/Formularios/Sitio/Login.aspx" ToolTip="Go Home" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

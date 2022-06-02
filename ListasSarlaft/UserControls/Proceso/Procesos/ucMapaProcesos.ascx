<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMapaProcesos.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.ucMapaProcesos" %>
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

    .center333399
    {
        text-align: center;
        background: #333399;
    }

    .centerEEEEEE
    {
        text-align: center;
        background: #EEEEEE;
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
    .btnMapaProcesos {
    background: #3498db;
  background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
  background-image: -moz-linear-gradient(top, #3498db, #2980b9);
  background-image: -ms-linear-gradient(top, #3498db, #2980b9);
  background-image: -o-linear-gradient(top, #3498db, #2980b9);
  background-image: linear-gradient(to bottom, #3498db, #2980b9);
  -webkit-border-radius: 28;
  -moz-border-radius: 28;
  border-radius: 28px;
  font-family: Arial;
  color: #ffffff;
  font-size: 12px;
  padding: 10px 20px 10px 20px;
  text-decoration: none;
  line-height: 30px;
  text-align: center;
  word-wrap: break-word;
  width: 150px;
}
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <table align="center" width="80%">
            
            <tr id="trTitulo" class="center333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Mapa de Procesos" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    
            </tr>
            <tr>
                <td align="center">
                    <table>
                        
                        <tr>
                            
                            
                            <td>
                                <asp:Image ID="imgNecesidades" runat="server" ImageUrl="~/Imagenes/Aplicacion/Necesidades.png" /></td>
                            <td id="colProcesos" class="centertdtr" runat="server">
                                <table id="tblProcesos" runat="server">
                                    <tr id="trUno" runat="server" visible="false">
                                        <td></td>
                                    </tr>
                                </table>

                            </td>
                            <td>
                                <asp:Image ID="imgSatisfaccion" runat="server" ImageUrl="~/Imagenes/Aplicacion/Satisfaccion.png" />
                            </td>
                        </tr>
                    </table>
                </td>

            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

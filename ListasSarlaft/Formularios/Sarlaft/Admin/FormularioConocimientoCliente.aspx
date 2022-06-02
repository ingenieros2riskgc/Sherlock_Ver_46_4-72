<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MastersPages/Admin.Master" Title="Sherlock" MaintainScrollPositionOnPostback="true"
    CodeBehind="FormularioConocimientoCliente.aspx.cs" Inherits="ListasSarlaft.Formularios.Sarlaft.Admin.FormularioConocimientoCliente" %>

<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="FCC" TagName="FormularioConocimientoCliente" Src="~/UserControls/Sarlaft/FormularioConocimientoCliente.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder5" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../../../Calendario/jsDatePick_ltr.min.css" />
    <script type="text/javascript" src="../../../Calendario/jsDatePick.min.1.3.js"></script>    
    <FCC:FormularioConocimientoCliente ID="FormularioConocimientoCliente1" runat="server" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder6" runat="server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolder7" runat="server">
</asp:Content>

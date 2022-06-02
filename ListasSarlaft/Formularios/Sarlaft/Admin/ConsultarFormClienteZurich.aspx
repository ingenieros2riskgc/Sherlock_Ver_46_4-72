<%@ Page Title="Sherlock" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="ConsultarFormClienteZurich.aspx.cs" Inherits="ListasSarlaft.Formularios.Admin.ConsultarFormClienteZurich" %>

<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCCFC" TagName="ConsultarFormCliente1" Src="~/UserControls/Sarlaft/ConsultarFormClienteZurich.ascx" %>
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
    <CCCFC:ConsultarFormCliente1 ID="ConsultarFormCliente1" runat="server" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder6" runat="server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolder7" runat="server">
</asp:Content>

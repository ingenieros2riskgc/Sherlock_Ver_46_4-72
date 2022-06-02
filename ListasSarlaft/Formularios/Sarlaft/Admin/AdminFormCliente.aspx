<%@ Page Title="Sherlock" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="AdminFormCliente.aspx.cs" Inherits="ListasSarlaft.Formularios.Admin.AdminFormCliente" %>

<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCFC" TagName="FormCliente1" Src="~/UserControls/Sarlaft/FormCliente.ascx" %>
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
    <CCFC:FormCliente1 ID="FormCliente1" runat="server" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder6" runat="server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolder7" runat="server">
</asp:Content>

<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="Servicios.aspx.cs" Inherits="ListasSarlaft.Formularios.Eventos.Servicios" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCR" TagName="Servicioss" Src="~/UserControls/Eventos/Servicios.ascx" %>


    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCR:Servicioss ID="Servicioss" runat="server" />
</asp:Content>

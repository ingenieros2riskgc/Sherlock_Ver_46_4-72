<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="Canales.aspx.cs" Inherits="ListasSarlaft.Formularios.Eventos.Canales" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCR" TagName="Canaless" Src="~/UserControls/Eventos/Canales.ascx" %>


    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCR:Canaless ID="Canaless" runat="server" />
</asp:Content>

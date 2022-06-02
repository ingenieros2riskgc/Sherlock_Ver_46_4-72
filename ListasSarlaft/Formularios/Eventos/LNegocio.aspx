<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="LNegocio.aspx.cs" Inherits="ListasSarlaft.Formularios.Eventos.LNegocio" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCR" TagName="LNegocioo" Src="~/UserControls/Eventos/LNegocio.ascx" %>


    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCR:LNegocioo ID="LNegocioo" runat="server" />
</asp:Content>

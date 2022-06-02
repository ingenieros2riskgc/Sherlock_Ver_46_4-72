<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="Estado.aspx.cs" Inherits="ListasSarlaft.Formularios.Eventos.Estado" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCR" TagName="Estadoo" Src="~/UserControls/Eventos/Estado.ascx" %>


    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCR:Estadoo ID="Estadoo" runat="server" />
</asp:Content>

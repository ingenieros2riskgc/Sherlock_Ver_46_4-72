<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="Clases.aspx.cs" Inherits="ListasSarlaft.Formularios.Eventos.Clases" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCR" TagName="Clasess" Src="~/UserControls/Eventos/Clases.ascx" %>


    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCR:Clasess ID="Clasess" runat="server" />
</asp:Content>

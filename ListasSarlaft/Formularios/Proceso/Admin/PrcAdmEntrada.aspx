<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmEntrada.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmEntrada" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PE" TagName="Entrada" Src="~/UserControls/Proceso/Param/Entrada.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PE:Entrada ID="PE" runat="server" />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcVerCaracterizacion.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcVerCaracterizacion" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="VC" TagName="VerCaracterizacion" Src="~/UserControls/Proceso/Procesos/VerCaracterizacion.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <VC:VerCaracterizacion ID="VC" runat="server" />
</asp:Content>

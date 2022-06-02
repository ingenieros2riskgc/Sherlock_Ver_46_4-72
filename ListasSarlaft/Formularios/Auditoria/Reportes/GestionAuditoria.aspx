<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="GestionAuditoria.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Reportes.GestionAuditoria" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCRA2" TagName="GestionAuditoriaa" Src="~/UserControls/MAuditoria/Reportes/GestionAuditoria.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCRA2:GestionAuditoriaa ID="GestionAuditoriaa" runat="server" />
</asp:Content>


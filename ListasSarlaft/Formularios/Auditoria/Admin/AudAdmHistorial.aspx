<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmHistorial.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmHistorial" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCHIS" TagName="Historial" Src="~/UserControls/MAuditoria/Historial.ascx" %>
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCHIS:Historial ID="Historial" runat="server" />
</asp:Content>

<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmPlaneacion.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmPlaneacion" Culture="es-CO" UICulture="es-CO"%>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCAP" TagName="Planeacion" Src="~/UserControls/MAuditoria/Planeacion.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCAP:Planeacion ID="Planeacion" runat="server" />
</asp:Content>



<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmPlanesAccion.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmPlanesAccion" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCPA" TagName="PlanesAccion" Src="~/UserControls/MAuditoria/PlanesAccion.ascx" %>
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCPA:PlanesAccion ID="PlanesAccion" runat="server" />
</asp:Content>

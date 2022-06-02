<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmRecomendaciones.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmRecomendaciones" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCREC" TagName="Recomendaciones" Src="~/UserControls/MAuditoria/Recomendaciones.ascx" %>
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCREC:Recomendaciones ID="Recomendaciones" runat="server" />
</asp:Content>

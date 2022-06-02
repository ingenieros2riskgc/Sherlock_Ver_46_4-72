<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmSeguimiento.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmSeguimiento" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="UCS" TagName="Seguimiento" Src="~/UserControls/MAuditoria/Seguimiento.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <UCS:Seguimiento ID="Seguimiento" runat="server" />
</asp:Content>

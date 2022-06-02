<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmRiesgoInherente.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmRiesgoInherente" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CRI" TagName="RiesgoInherente" Src="~/UserControls/MAuditoria/RiesgoInherente.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CRI:RiesgoInherente ID="RiesgoInherente" runat="server" />
</asp:Content>

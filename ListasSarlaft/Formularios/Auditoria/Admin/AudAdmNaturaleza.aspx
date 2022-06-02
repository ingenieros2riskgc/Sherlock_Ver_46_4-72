<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmNaturaleza.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmNaturaleza" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CIC" TagName="Naturaleza" Src="~/UserControls/MAuditoria/Naturaleza.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CIC:Naturaleza ID="Naturaleza" runat="server" />
</asp:Content>

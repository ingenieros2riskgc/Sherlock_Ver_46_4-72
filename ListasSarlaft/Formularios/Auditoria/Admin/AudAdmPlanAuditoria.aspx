<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmPlanAuditoria.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmPlanAuditoria" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCPA" TagName="PlanAuditoria" Src="~/UserControls/MAuditoria/PlanAuditoria.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCPA:PlanAuditoria ID="PlanAuditoria" runat="server" />
</asp:Content>

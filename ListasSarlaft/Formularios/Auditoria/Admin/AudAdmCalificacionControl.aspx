<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmCalificacionControl.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmCalificacionControl" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCC" TagName="CalificacionControl" Src="~/UserControls/MAuditoria/CalificacionControl.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCC:CalificacionControl ID="CalificacionControl" runat="server" />
</asp:Content>

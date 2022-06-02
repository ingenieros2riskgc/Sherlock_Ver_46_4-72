<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MastersPages/Admin.Master" CodeBehind="RPTreporteDocumentosVigentes.aspx.cs" Inherits="ListasSarlaft.Formularios.Reportes.RPTreporteDocumentosVigentes" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RDV" TagName="ReporteDocumentosVigentes" Src="~/UserControls/Reportes/RPTreporteDocumentosVigentes.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <RDV:ReporteDocumentosVigentes ID="RDV" runat="server" />
</asp:Content>
<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="RPTreporteNoConformidades.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.RPTreporteNoConformidades" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RNC" TagName="ReporteNoConformidades" Src="~/UserControls/Proceso/Reportes/ReporteNoConformidades.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <RNC:ReporteNoConformidades ID="RNC" runat="server" />
</asp:Content>


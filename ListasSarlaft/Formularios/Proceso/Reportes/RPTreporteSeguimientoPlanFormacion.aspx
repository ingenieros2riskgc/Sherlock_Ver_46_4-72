<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="RPTreporteSeguimientoPlanFormacion.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Reportes.RPTreporteSeguimientoPlanFormacion" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RSPF" TagName="ReporteSeguimientoPlanFormacion" Src="~/UserControls/Proceso/Reportes/ReporteSeguimientoPlanFormacion.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <RSPF:ReporteSeguimientoPlanFormacion ID="RSPF" runat="server" />
</asp:Content>
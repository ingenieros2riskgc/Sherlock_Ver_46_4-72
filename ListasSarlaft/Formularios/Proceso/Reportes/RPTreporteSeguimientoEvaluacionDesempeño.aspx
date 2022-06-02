<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="RPTreporteSeguimientoEvaluacionDesempeño.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Reportes.RPTreporteSeguimientoEvaluacionDesempeño" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RSED" TagName="ReporteSeguimientoEvaluacionDesempeño" Src="~/UserControls/Proceso/Reportes/ReporteSeguimientoEvaluacionDesempeño.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <RSED:ReporteSeguimientoEvaluacionDesempeño ID="RSED" runat="server" />
</asp:Content>
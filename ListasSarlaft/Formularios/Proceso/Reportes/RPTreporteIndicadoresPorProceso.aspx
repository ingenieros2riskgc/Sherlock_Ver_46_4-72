<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="RPTreporteIndicadoresPorProceso.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Reportes.RPTreporteIndicadoresPorProceso" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RIPP" TagName="ReporteIndicadoresPorProceso" Src="~/UserControls/Proceso/Reportes/ReporteIndicadoresPorProceso.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <RIPP:ReporteIndicadoresPorProceso ID="RIPP" runat="server" />
</asp:Content>
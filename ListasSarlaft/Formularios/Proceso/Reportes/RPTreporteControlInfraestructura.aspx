<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="RPTreporteControlInfraestructura.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Reportes.RPTreporteControlInfraestructura" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RCI" TagName="ReporteControlInfraestructura" Src="~/UserControls/Proceso/Reportes/ReporteControlInfraestructura.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <RCI:ReporteControlInfraestructura ID="RCI" runat="server" />
</asp:Content>
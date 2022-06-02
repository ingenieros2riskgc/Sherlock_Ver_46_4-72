<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="RPTreporteIndicadoresMetasIncumplidas.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Reportes.RPTreporteIndicadoresMetasIncumplidas" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RIMI" TagName="ReporteIndicadoresMetasIncumplidas" Src="~/UserControls/Proceso/Reportes/ReporteIndicadoresMetasIncumplidas.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <RIMI:ReporteIndicadoresMetasIncumplidas ID="RIMI" runat="server" />
</asp:Content>
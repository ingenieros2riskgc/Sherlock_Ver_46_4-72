<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="RPTreporteInformacionGeneral.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Reportes.RPTreporteInformacionGeneral" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RIG" TagName="ReporteInformacionGeneral" Src="~/UserControls/Proceso/Reportes/ReporteInformacionGeneral.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <RIG:ReporteInformacionGeneral ID="RIG" runat="server" />
</asp:Content>
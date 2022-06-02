<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmObjetivoCalidad.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmObjetivoCalidad" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PO" TagName="Objetivo" Src="~/UserControls/Proceso/Param/Objetivo.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PO:Objetivo ID="PO" runat="server" />
</asp:Content>

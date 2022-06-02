<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmCriterioProveedor.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmCriterioProveedor" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PCP" TagName="CriterioProveedor" Src="~/UserControls/Proceso/Param/CriterioProveedor.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PCP:CriterioProveedor ID="PCP" runat="server" />
</asp:Content>

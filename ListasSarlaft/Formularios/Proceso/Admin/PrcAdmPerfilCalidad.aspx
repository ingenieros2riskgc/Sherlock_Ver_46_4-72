<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmPerfilCalidad.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmPerfilCalidad" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PPC" TagName="PerfilCalidad" Src="~/UserControls/Proceso/Param/PerfilCalidad.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PPC:PerfilCalidad ID="PPC" runat="server" />
</asp:Content>

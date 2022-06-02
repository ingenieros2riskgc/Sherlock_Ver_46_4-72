<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmPoliticaCalidad.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmPoliticaCalidad" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PPC" TagName="PoliticaCalidad" Src="~/UserControls/Proceso/Param/PoliticaCalidad.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PPC:PoliticaCalidad ID="PPC" runat="server" />
</asp:Content>

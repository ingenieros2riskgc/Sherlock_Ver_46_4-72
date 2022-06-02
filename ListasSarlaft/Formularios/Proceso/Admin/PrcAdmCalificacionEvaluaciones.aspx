<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmCalificacionEvaluaciones.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmCalificacionEvaluaciones" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PC" TagName="Calificacion" Src="~/UserControls/Proceso/Param/CalificacionEvaluaciones.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PC:Calificacion ID="PC" runat="server" />
</asp:Content>

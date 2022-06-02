<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmSeguimientoIndicador.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmSeguimientoIndicador" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="SE" TagName="Seguimiento" Src="~/UserControls/Proceso/Procesos/SeguimientoIndicadores.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <SE:Seguimiento ID="SE" runat="server" />
</asp:Content>

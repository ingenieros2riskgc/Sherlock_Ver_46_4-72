<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmCriterioServicio.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmCriterioServicio" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CS" TagName="CriteriosServicios" Src="~/UserControls/Proceso/Param/CriteriosServicios.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CS:CriteriosServicios ID="CS" runat="server" />
</asp:Content>
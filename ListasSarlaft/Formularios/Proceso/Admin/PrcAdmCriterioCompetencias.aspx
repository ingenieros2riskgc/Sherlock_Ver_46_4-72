<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmCriterioCompetencias.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmCriterioCompetencias" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PCC" TagName="CriterioCompetencia" Src="~/UserControls/Proceso/Param/CriterioCompetencias.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PCC:CriterioCompetencia ID="PCC" runat="server" />
</asp:Content>

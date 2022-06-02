<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmGestionEvaluacionCompetencia.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmValorVariable" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="GEC" TagName="GestionEvaluacionCompetencia" Src="~/UserControls/Proceso/Procesos/GestionEvaluacionCompetencia.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <GEC:GestionEvaluacionCompetencia ID="GEC" runat="server" />
</asp:Content>

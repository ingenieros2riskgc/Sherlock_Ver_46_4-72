<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmGestionEvaluacionServicio.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmGestionEvaluacionServicio" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="GES" TagName="GestionEvaluacionServicio" Src="~/UserControls/Proceso/Procesos/GestionEvaluacionServicio.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <GES:GestionEvaluacionServicio ID="GES" runat="server" />
</asp:Content>
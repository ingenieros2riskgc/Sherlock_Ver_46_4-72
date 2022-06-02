<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmGestionEvaluacionProveedor.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmGestionEvaluacionProveedor" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="GEP" TagName="GestionEvaluacionProveedor" Src="~/UserControls/Proceso/Procesos/GestionEvaluacionProveedor.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <GEP:GestionEvaluacionProveedor ID="GEP" runat="server" />
</asp:Content>
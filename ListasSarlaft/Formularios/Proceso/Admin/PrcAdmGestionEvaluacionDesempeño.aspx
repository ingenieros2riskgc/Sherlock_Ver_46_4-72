<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmGestionEvaluacionDesempeño.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmGestionEvaluacionDesempeño" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="GED" TagName="GestionEvaluacionDesempeño" Src="~/UserControls/Proceso/Procesos/GestionEvaluacionDesempeño.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <GED:GestionEvaluacionDesempeño ID="GED" runat="server" />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmValorVariable.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmValorVariable" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="VV" TagName="ValorVariable" Src="~/UserControls/Proceso/Param/ValorVariableIndicador.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <VV:ValorVariable ID="VV" runat="server" />
</asp:Content>

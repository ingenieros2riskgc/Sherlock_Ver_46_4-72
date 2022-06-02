<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MastersPages/Admin.Master" CodeBehind="PrcAdmMatrizIndicadores.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmMatrizIndicadores" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="MI" TagName="MatrizIndicadores" Src="~/UserControls/Proceso/Procesos/MatrizIndicadores.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <MI:MatrizIndicadores ID="MI" runat="server" />
</asp:Content>
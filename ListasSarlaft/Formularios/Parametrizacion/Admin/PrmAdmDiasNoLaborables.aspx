<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrmAdmDiasNoLaborables.aspx.cs" Inherits="ListasSarlaft.Formularios.Parametrizacion.Admin.PrmAdmDiasNoLaborables" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="DNL" TagName="DiasNoLaborables" Src="~/UserControls/Parametrizacion/DiasNoLaborables.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <DNL:DiasNoLaborables ID="DiasNoLaborables" runat="server" />
</asp:Content>

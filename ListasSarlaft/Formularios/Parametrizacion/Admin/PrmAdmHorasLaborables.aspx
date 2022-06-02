<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrmAdmHorasLaborables.aspx.cs" Inherits="ListasSarlaft.Formularios.Parametrizacion.Admin.PrmAdmHorasLaborables" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PHL" TagName="HorasLaborables" Src="~/UserControls/Parametrizacion/HorasLaborables.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PHL:HorasLaborables ID="HorasLaborables" runat="server" />
</asp:Content>

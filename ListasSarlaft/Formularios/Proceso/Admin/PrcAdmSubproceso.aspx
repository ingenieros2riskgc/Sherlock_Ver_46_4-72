<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmSubproceso.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmSubproceso" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PPS" TagName="Subproceso" Src="~/UserControls/Proceso/Param/Subproceso.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PPS:Subproceso ID="PPS" runat="server" />
</asp:Content>

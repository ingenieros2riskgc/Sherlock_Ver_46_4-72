<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcCrlProductoNoConforme.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcCrlProductoNoConforme" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CPNC" TagName="ControlProductoNoConforme" Src="~/UserControls/Proceso/Procesos/ControlProductoNoConforme.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CPNC:ControlProductoNoConforme ID="CPNC" runat="server" />
</asp:Content>

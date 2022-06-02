<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrmAdmDepartamentos.aspx.cs" Inherits="ListasSarlaft.Formularios.Parametrizacion.Admin.PrmAdmDepartamentos" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCD" TagName="Departamentos" Src="~/UserControls/Parametrizacion/Departamentos.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <CCD:Departamentos ID="Departamentos" runat="server" />
</asp:Content>

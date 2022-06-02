<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrmAdmParamColores.aspx.cs" Inherits="ListasSarlaft.Formularios.Parametrizacion.Admin.PrmAdmParamColores" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PHL" TagName="PrmAdmParamColoress" Src="~/UserControls/Parametrizacion/ParamColores.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PHL:PrmAdmParamColoress ID="PrmAdmParamColoress" runat="server" />
</asp:Content>

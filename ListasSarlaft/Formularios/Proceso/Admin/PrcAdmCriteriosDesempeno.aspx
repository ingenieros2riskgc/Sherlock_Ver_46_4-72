<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmCriteriosDesempeno.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmCriteriosDesempeno" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CD" TagName="CriteriosDesempeno" Src="~/UserControls/Proceso/Param/CriteriosDesempeno.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CD:CriteriosDesempeno ID="CD" runat="server" />
</asp:Content>

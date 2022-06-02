<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmOtrosFactores.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmOtrosFactores" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="COF" TagName="OtrosFactores" Src="~/UserControls/MAuditoria/OtrosFactores.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <COF:OtrosFactores ID="OtrosFactores" runat="server" />
</asp:Content>

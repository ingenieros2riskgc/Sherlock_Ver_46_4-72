<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmAuditoriaGestion.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmAuditoriaGestion" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCAG" TagName="MAuditoriaGestion" Src="~/UserControls/MAuditoria/MAuditoriaGestion.ascx" %>
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCAG:MAuditoriaGestion ID="MAuditoriaGestion" runat="server" />
</asp:Content>

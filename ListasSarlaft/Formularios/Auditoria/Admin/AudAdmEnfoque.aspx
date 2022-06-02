<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmEnfoque.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmEnfoque" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CAE" TagName="Enfoque" Src="~/UserControls/MAuditoria/Enfoque.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <CAE:Enfoque ID="Enfoque" runat="server" />
</asp:Content>

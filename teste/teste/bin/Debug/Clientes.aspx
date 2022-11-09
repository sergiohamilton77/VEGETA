<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="teste.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p>
        CONTROLE DE CLIENTES</p>
    <p class="style1">
        <asp:TextBox ID="txtnome" runat="server" Width="318px"></asp:TextBox>
&nbsp; Nome</p>
    <p class="style1">
        <asp:TextBox ID="txttel" runat="server"></asp:TextBox>
&nbsp;Celular&nbsp;&nbsp;&nbsp;
    </p>
    <p class="style1">
        <asp:TextBox ID="txtemail" runat="server" Width="315px"></asp:TextBox>
&nbsp;Email&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Cadastrar" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Limpar" 
            onclick="Button2_Click" />
&nbsp;
        <asp:CheckBox ID="CheckBox1" runat="server" Text="Exibir inativos" />
    </p>
</asp:Content>

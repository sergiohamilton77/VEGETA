﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="teste.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        CONTROLE DE CLIENTES</p>
    <p class="style1">
    &nbsp; Nome:&nbsp; <asp:TextBox ID="txtnome" runat="server" Width="256px" height="23px"></asp:TextBox>
     &nbsp;CPF: <asp:TextBox ID="txtcpf" runat="server" Height="23px" Width="256px"></asp:TextBox>
   </p>
    <p class="style1">
        &nbsp;</p>
    <p class="style1">
        &nbsp;Celular:&nbsp;&nbsp;<asp:TextBox ID="txttel" runat="server" height="23px" width="264px"></asp:TextBox>

    </p>
    <p class="style1">
        &nbsp;</p>
    <p class="style1">
     &nbsp;Email :&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtemail" runat="server" 
            Width="264px" height="23px"></asp:TextBox>

        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Cadastrar" />
            &nbsp;
        <asp:Button ID="Button2" runat="server" Text="Limpar" 
            OnClientClick="return confirm('Deseja mesmo limpar todas as informações ?');" onclick="Button3_Click" />
&nbsp;
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
            Text="Excluir" Visible="False" />
    </p>
    <p class="style1">
        &nbsp;</p>
<p class="style1">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            AutoGenerateSelectButton="True" AutoGenerateDeleteButton="True" 
            DataSourceID="MySQLData" Width="577px" 
            onselectedindexchanging="GridView1_SelectedIndexChanging" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            onrowdeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="Nome_Cliente" HeaderText="Nome_Cliente" 
                    SortExpression="Nome_Cliente" />
                <asp:BoundField DataField="Telefone" HeaderText="Telefone" 
                    SortExpression="Telefone" />
                <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                <asp:BoundField DataField="CPF" HeaderText="CPF" SortExpression="CPF" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="Sqinvests" runat="server" 
            ConnectionString="<%$ ConnectionStrings:INVESTSCOPConnectionString %>" 
            SelectCommand="SELECT * FROM [Clientes]"></asp:SqlDataSource>
          
            <asp:SqlDataSource runat="server" ID="MySQLData"
            ConnectionString="<%$ ConnectionStrings:teste %>"
            ProviderName="<%$ ConnectionStrings:teste.ProviderName %>"            
            SelectCommand="SELECT Nome_Cliente, Telefone, email, CPF FROM Clientes"></asp:SqlDataSource>
    </p>
<p class="style1">
        &nbsp;</p>
</asp:Content>

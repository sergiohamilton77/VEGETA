<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="teste._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            font-size: x-large;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2 class="style1">
        BEM VINDO A INVESTSCORP
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </h2>
    <p style="font-size: x-large">
        Selecione no menu acima a opção desejada
    </p>
    <p>
        
    </p>
</asp:Content>

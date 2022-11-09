<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="AreaCliente.aspx.cs" Inherits="teste.AreaCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Panel ID="Panel1" runat="server" Height="32px">
        BEM VINDO
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </asp:Panel>
     <div style="float:left;width:316px; height: 320px;">
        ATIVOS EM ABERTO:<br />
        <br />
        <asp:GridView ID="GridView2" runat="server" Visible="False">
        </asp:GridView>
         <asp:GridView ID="gridmovim" runat="server" 
             onrowdatabound="gridmovim_RowDataBound">
         </asp:GridView>
         <br />
         <br />
         <asp:Button ID="btnrelatorio" runat="server" Text="Gerar Relatorio" 
             Width="126px" onclick="btnrelatorio_Click" />
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="aspnetForm.target ='_blank';"
            onclick="LinkButton1_Click" Visible="False">Último Relatório</asp:LinkButton>
            &nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server" ><a href="temp.pdf" target="_blank">Último Relatório</a></asp:HyperLink>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
     </div>
     <div style="float:left;width:508px; height: 295px;">
         <asp:Panel ID="Panel2" runat="server" BorderStyle="Groove" Height="90px">
             <br />
             USUARIO :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:TextBox ID="txtusuario" runat="server"></asp:TextBox>
             <asp:Button ID="btnaltera" runat="server" Text="Alterar Senha" Width="116px" 
                 onclick="btnaltera_Click" />
             <br />
             <br />
             NOVA SENHA:
             <asp:TextBox ID="TextBox1" runat="server" ontextchanged="TextBox1_TextChanged"></asp:TextBox>
             &nbsp;&nbsp;
             <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
             <br />
             <br />
             <br />
         </asp:Panel>
          <asp:Panel ID="Panel3" runat="server" Widht="420px" BorderStyle="Groove" Height="212px" 
             Width="603px">
              &nbsp;<br /> CONTRATOS:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <br />
              <br />
              &nbsp;<asp:Button ID="btnrelcontrato" runat="server" Text="GERAR RELATORIO" />
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:DropDownList ID="drptipo" runat="server">
                  <asp:ListItem Value="F">Finalizados</asp:ListItem>
                  <asp:ListItem Value="P">Faturados</asp:ListItem>
              </asp:DropDownList>
              <br />
              <br />
              <asp:GridView ID="gridcontrato" runat="server" Height="16px" 
                  style="margin-bottom: 0px" Width="350px" AutoGenerateColumns="False" 
                  onselectedindexchanged="gridcontrato_SelectedIndexChanged" 
                  Font-Size="X-Small">
                  <RowStyle Font-Size="Small" HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:GridView>
         </asp:Panel>

     </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Callvenda.aspx.cs" Inherits="teste.Callvenda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Selecione os Clientes para envio do CALL DE VENDA:</p>
    <p>
        <asp:ListBox ID="listenvio" runat="server" Width="227px" AutoPostBack="True" 
            onselectedindexchanged="listenvio_SelectedIndexChanged"></asp:ListBox>
        <asp:Button ID="Button1" runat="server" Text="Enviar" Width="73px" 
            onclick="Button1_Click" />
        <asp:Button ID="Button5" runat="server" Text="Remover Selec." Width="107px" 
            OnClientClick="return confirm('Deseja mesmo remover as informações selecionadas?');"  onclick="Button5_Click" />
        <asp:Button ID="Button4" runat="server" Text="Selec. Todos" Width="95px" 
            onclick="Button4_Click" />
        <asp:Button ID="Button3" runat="server" Text="Limpar Lista" Width="80px" 
           OnClientClick="return confirm('Deseja mesmo limpar todas as informações ?');"  onclick="Button3_Click" />

        <asp:Button ID="Button2" runat="server" Text="Sair" Width="65px" 
            onclick="Button2_Click" />
          
        <asp:Button ID="Button6" runat="server" Text="Verificar TXT" Width="16px" 
            onclick="Button6_Click" Height="19px" Visible="False" 
             />
            

      

   <p>
        &nbsp;</p>
  
    <p>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        &nbsp;<asp:TextBox ID="respostaEnvioLabel" runat="server" Visible="False" 
            Width="235px" Height="25px" ontextchanged="respostaEnvioLabel_TextChanged"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    <p>
        &nbsp;</p>
   <div style="float:left;  overflow-y:scroll; width:454px; height: 525px;">
        <asp:GridView ID="gridenvio" runat="server" Width="415px" onrowediting="gridenvio_RowEditing" 
            onrowupdating="gridenvio_RowUpdating" 
            onrowcancelingedit="gridenvio_RowCancelingEdit" 
            AutoGenerateSelectButton="True" 
            onselectedindexchanged="gridenvio_SelectedIndexChanged" >

        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server" Height="105px" Visible="False" 
            Width="16px">
        </asp:GridView>
        <asp:ListBox ID="lstvenda" runat="server" Visible="False"></asp:ListBox>
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="gridmovim" runat="server" 
            onrowdatabound="gridmovim_RowDataBound" Width="437px">
        </asp:GridView>
        </div>
         <div style="float:left;width:448px; height: 272px;">
            <asp:TextBox ID="txtarq" runat="server" Height="227px" TextMode="MultiLine" 
                Width="429px"></asp:TextBox>
            <br />
            <asp:Button ID="Button7" runat="server" onclick="Button7_Click" 
                Text="Atualizar" Width="109px" Visible="False" />
        </div>
</asp:Content>

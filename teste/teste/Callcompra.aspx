<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Callcompra.aspx.cs" Inherits="teste.Callcompra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Selecione os Clientes para envio do CALL DE COMPRA:</p>
    <p>
        <asp:ListBox ID="listenvio" runat="server" Width="227px"></asp:ListBox>
        <asp:Button ID="Button1" runat="server" Text="Enviar" Width="80px" 
            onclick="Button1_Click" />
        <asp:Button ID="Button5" runat="server" Text="Remover Selec." Width="107px" 
            OnClientClick="return confirm('Deseja mesmo remover as informações selecionadas?');" onclick="Button5_Click" />
        <asp:Button ID="Button4" runat="server" Text="Selec. Todos" Width="95px" 
            onclick="Button4_Click" />
        <asp:Button ID="Button3" runat="server" Text="Limpar Lista" Width="80px" 
             OnClientClick="return confirm('Deseja mesmo limpar todas as informações ?');" onclick="Button3_Click" />

        <asp:Button ID="Button2" runat="server" Text="Sair" Width="80px" 
            onclick="Button2_Click" />
            

        <asp:Button ID="Button6" runat="server" Text="Verificar TXT" Width="122px" 
            onclick="Button6_Click" Visible="False" />
            

    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:TextBox ID="respostaEnvioLabel" runat="server" Visible="False" 
            Width="354px" Height="25px"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ListBox ID="list" runat="server" Height="64px" Visible="False">
        </asp:ListBox>
    </p>
   
        <div style="float:left;  overflow-y:scroll; width:402px">
        <asp:GridView ID="gridenvio" runat="server" Width="385px" onrowediting="gridenvio_RowEditing" 
            onrowupdating="gridenvio_RowUpdating" 
            onrowcancelingedit="gridenvio_RowCancelingEdit" 
            AutoGenerateSelectButton="True" onrowdatabound="gridenvio_RowDataBound" 
            onselectedindexchanged="gridenvio_SelectedIndexChanged" Height="136px" >

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

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Movimentacoes.aspx.cs" Inherits="teste.Movimentacoes" EnableEventValidation="False" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 301px;
            height: 5px;
        }
        .style2
        {
            height: 5px;
        }
        .style3
        {
            height: 5px;
            width: 214px;
        }
        .style4
        {
            height: 5px;
            width: 192px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    
    <p Visible= false>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:FileUpload 
            ID="FileUpload1" runat="server" Width="221px" 
        onload="FileUpload1_Load" Visible="False" />
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
    
        <asp:Button ID="Button5" runat="server" onclick="Button5_Click" Text="Run" 
            Visible="False" />
    <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
    
        <asp:Button ID="Button9" runat="server" onclick="Button9_Click" 
            Text="Gerar PDF" Visible="true" />
    
        
    
    </p>
    
        <asp:Panel ID="Panel2" runat="server" BorderStyle="Groove" Height="105px">
        &nbsp;CLIENTE :&nbsp; 
        <asp:ListBox ID="ListBox1" runat="server" DataSourceID="MYSQLCliente" 
            DataTextField="Nome_cliente" DataValueField="Id_cliente" 
            onselectedindexchanged="ListBox1_SelectedIndexChanged" 
            style="margin-top: 13px" AutoPostBack="True"></asp:ListBox>
        <asp:SqlDataSource ID="clientefinal" runat="server" 
            ConnectionString="<%$ ConnectionStrings:INVESTSCOPConnectionString %>" 
            SelectCommand="SELECT [Id_cliente], [Nome_cliente] FROM [Clientes]">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="MYSQLCliente" runat="server" 
            ConnectionString="<%$ ConnectionStrings:teste %>"
            ProviderName="<%$ ConnectionStrings:teste.ProviderName %>"     
            SelectCommand="SELECT Id_cliente, Nome_cliente FROM Clientes">
        </asp:SqlDataSource>
    &nbsp;&nbsp;<asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
            Text="Filtrar Mov." Visible="False" />
        &nbsp;<asp:Button ID="Button10" runat="server" onclick="Button10_Click1" 
            Text="Gerar Relatorio" Width="108px" />
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="aspnetForm.target ='_blank';"
            onclick="LinkButton1_Click" Visible="False">Último Relatório</asp:LinkButton>
            &nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server" Visible="false"><a href="teeee.pdf" target="_blank">Último Relatório</a></asp:HyperLink>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
     </asp:Panel>
        <asp:SqlDataSource ID="clientes" runat="server" 
            ConnectionString="<%$ ConnectionStrings:INVESTSCOPConnectionString %>" 
            SelectCommand="SELECT [Nome_cliente] FROM [Clientes]"></asp:SqlDataSource>
  
     <div style="height: 146px; width: 898px; margin-top: 27px">    
   <table style="height: 17px; width: 891px">  
    <tr>  
    <td class="style2">   
      
        AÇÃO 
        </td>  
        <td class="style2">  
        <asp:TextBox ID="TextBox1" runat="server" Height="18px" style="margin-top: 0px" 
                AutoPostBack="True" ontextchanged="TextBox1_TextChanged" TabIndex="2"></asp:TextBox>  
        </td>  
        <td class="style2"style="text-align: center; vertical-align:middle !important">   
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
                ID="Button1" runat="server" Text="IR" onclick="Button1_Click" 
                Height="24px" Width="61px" Visible="False" />  
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
        </td>  
        <td class="style3">

VALOR :
            <asp:TextBox ID="txtvalor" runat="server" height="18px" width="128px" 
                ontextchanged="txtvalor_TextChanged" TabIndex="3"></asp:TextBox>

        </td>
         <td class="style4">

            QTDE :
            <asp:TextBox ID="TextBox3" runat="server" Height="18px" Width="128px" 
                 ontextchanged="TextBox3_TextChanged" TabIndex="4"></asp:TextBox>

        </td>
         <td class="style1">
        <asp:Button ID="Button2" runat="server" Text="Salvar" Width="73px" 
            onclick="Button2_Click" />
&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Limpar" Width="73px"  
                 OnClientClick="return confirm('Deseja mesmo limpar todas as informações ?');" 
                 onclick="Button3_Click" />
             <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
             <asp:Label ID="Label4" runat="server" Text="Label" Visible="False"></asp:Label>
        </td>
        </tr>  
</table>  
         <br />
         DATA :
         <asp:TextBox ID="TextBox4" runat="server" ontextchanged="TextBox4_TextChanged" 
             Height="18px" TabIndex="5"></asp:TextBox>
         <br />
         <br />
         
         <asp:ListBox ID="ListBox3" runat="server" Width="402px" 
             onselectedindexchanged="ListBox3_SelectedIndexChanged" 
             ondatabound="ListBox3_DataBound"></asp:ListBox>
    &nbsp;<asp:Button ID="Button6" runat="server" onclick="Button6_Click" 
             Text="Excluir" height="26px" width="101px" OnClientClick="return confirm('Deseja mesmo excluir ?');"/>
              &nbsp;<asp:Button ID="Button8" runat="server" onclick="Button8_Click" 
             Text="Emit. CALL" Height="26px" Width="101px" />
         <asp:Button ID="Button7" runat="server" Height="26px" Text="Reg. Venda" 
             Width="101px" onclick="Button7_Click" />
         
    </div> 
      <div style="float:left;  width:335px">
        
          <p>&nbsp;<asp:Calendar ID="Calendar1" runat="server" Height="70px" Width="214px" 
                  onselectionchanged="Calendar1_SelectionChanged" 
                  onvisiblemonthchanged="Calendar1_VisibleMonthChanged"></asp:Calendar>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            Height="16px" style="margin-right: 126px" Width="16px" AllowSorting="True" Visible="False">
        </asp:GridView>
              <asp:GridView ID="GridView3" runat="server">
              </asp:GridView>
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
             AutoGenerateEditButton="True" Height="16px" PageSize="5" Visible="False" 
             Width="16px" >  
    </asp:GridView>   
    </p>
    </div>
    <div style="float:left;width:568px; height: 266px;">
       
       <br />
        <br />
       <asp:Label ID="respostaEnvioLabel" runat="server" Text="Label" Visible="False"></asp:Label>
        
        <br />
       
        ULTIMAS MOVIMENTAÇÕES:<br />
        <br />
        <div style="overflow-y:scroll; width: 550px; height: 134px;">
        <asp:GridView ID="gridmovim" runat="server" Width="532px" 
             onrowediting="gridmovim_RowEditing" 
            AutoGenerateDeleteButton="True" AutoGenerateSelectButton="True" 
            onrowdeleted="gridmovim_RowDeleted" onrowdeleting="gridmovim_RowDeleting" 
                onsorting="gridmovim_Sorting" onrowdatabound="gridmovim_RowDataBound" 
                onselectedindexchanged="gridmovim_SelectedIndexChanged" >
        </asp:GridView>
        </div>
        <br />
        <asp:SqlDataSource ID="movim" runat="server" 
            ConnectionString="<%$ ConnectionStrings:INVESTSCOPConnectionString %>" 
            SelectCommand="SELECT [data], [nome], [ativo], [valor], [tipo] FROM [movim]">
        </asp:SqlDataSource>
          <asp:SqlDataSource ID="MySQL_movim" runat="server" 
             ConnectionString="<%$ ConnectionStrings:teste %>"
                     ProviderName="<%$ ConnectionStrings:teste.ProviderName %>"     
            SelectCommand="SELECT data, nome, ativo, valor, tipo FROM movim">
        </asp:SqlDataSource>
       
    </div>
</asp:Content>

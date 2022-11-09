<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manutencao.aspx.cs" Inherits="teste.Manutencao" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
     <div style="float:left;  height: 428px; width: 326px;">
    <asp:FileUpload ID="FileUpload1" runat="server" Width="221px" 
        onload="FileUpload1_Load" />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    
    <br />
    &nbsp;<br />
    TIPO DE ATIVO
    <br />
    <br />   
    <asp:DropDownList ID="droptipo" runat="server" Height="16px" Width="215px" 
        DataSourceID="tipoativo" DataTextField="nome" DataValueField="nome" 
        onselectedindexchanged="droptipo_SelectedIndexChanged" 
        ontextchanged="droptipo_TextChanged">
    </asp:DropDownList>
   
    <asp:SqlDataSource ID="tipoativo" runat="server" 
        ConnectionString="<%$ ConnectionStrings:INVESTSCOPConnectionString %>" 
        SelectCommand="SELECT [nome], [id_tipoativo] FROM [Tipos_ativo]"></asp:SqlDataSource>
          <asp:SqlDataSource ID="MYSQLtipoativo" runat="server" 
            ConnectionString="<%$ ConnectionStrings:teste %>"
            ProviderName="<%$ ConnectionStrings:teste.ProviderName %>"     
            SelectCommand="SELECT nome, id_tipoativo FROM Clientes">
        </asp:SqlDataSource>
    <br />
   
    <br />
       <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
    Text="exportar" />
    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
        Text="TESTE SUM" />
    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
        Text="% contratos-calc" Width="119px" />
    <br />
  <br />
    <asp:Button ID="Button2" runat="server" Text="Eliminar calls  temporarias" 
        Height="22px" Width="211px" onclick="Button2_Click" />
    <br />
   
         <br />
         NEGOCIAÇÕES:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; MERCADO 
         :<br />
         <br />
         <asp:ListBox ID="lsttipos" runat="server" AutoPostBack="True" Height="92px" 
             onselectedindexchanged="lsttipos_SelectedIndexChanged" Width="105px">
         </asp:ListBox>
&nbsp;
         <asp:Button ID="btnatual" runat="server" Text="Atualizar" 
             onclick="btnatual_Click" />
         <asp:DropDownList ID="drpmercado" runat="server">
             <asp:ListItem></asp:ListItem>
             <asp:ListItem Value="F">Fracionário</asp:ListItem>
             <asp:ListItem>Cota</asp:ListItem>
         </asp:DropDownList>
         <br />
         <br />
         FRACIONADO: <asp:TextBox ID="txtfra" runat="server" Height="22px" Width="46px"></asp:TextBox>
        LOTE PADRÃO: <asp:TextBox ID="txtcotas" runat="server" Height="23px" 
             Width="52px"></asp:TextBox>
&nbsp;&nbsp;
         <br />
         <br />
   
    </div>

      <div style="float:left;width:569px; height: 698px;">
          <asp:Panel ID="Panel1" runat="server" Height="252px">
              USUÁRIOS SISTEMA:&nbsp;&nbsp;&nbsp;&nbsp;
              <br />
              <asp:TextBox ID="txtuser" runat="server" ontextchanged="TextBox1_TextChanged"></asp:TextBox>
              &nbsp;&nbsp;<asp:DropDownList ID="DropDownList1" runat="server">
                  <asp:ListItem Value="A">Admin</asp:ListItem>
                  <asp:ListItem Value="C">Cliente</asp:ListItem>
              </asp:DropDownList>
              &nbsp;&nbsp;
              <asp:Button ID="Button5" runat="server" onclick="Button5_Click" 
                  Text="Alterar" />
              <asp:Button ID="btnredefinir" runat="server" Text="Redefinir Senha" 
                  onclick="btnredefinir_Click" />
              <br />

              <br />
              <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" 
                  DataSourceID="MYSQLuser" DataTextField="email" DataValueField="fun_codigo" 
                  Height="89px" onselectedindexchanged="ListBox1_SelectedIndexChanged" 
                  Width="276px"></asp:ListBox>
              <asp:SqlDataSource ID="user" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:INVESTSCOPConnectionString %>" 
                  SelectCommand="SELECT [fun_codigo], [fun_login], [email] FROM [Usuario]">
              </asp:SqlDataSource>
               <asp:SqlDataSource ID="MYSQLuser" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:teste %>"
                    ProviderName="<%$ ConnectionStrings:teste.ProviderName %>"     
                    SelectCommand="SELECT fun_codigo, fun_login, email FROM Usuario">
                </asp:SqlDataSource>
              <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                  Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                  WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="102px" 
                  Visible="False" Width="135px">
                  <LocalReport ReportPath="Report2.rdlc">
                      <DataSources>
                          <rsweb:ReportDataSource DataSourceId="datamovim" Name="DataSet1" />
                      </DataSources>
                  </LocalReport>
              </rsweb:ReportViewer>
              <asp:SqlDataSource ID="datamovim" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:INVESTSCOPConnectionString %>" 
                  SelectCommand="SELECT * FROM [movim]"></asp:SqlDataSource>
              <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                  SelectMethod="GetData" 
                  TypeName="teste.INVESTSCOPreportTableAdapters.movimTableAdapter">
              </asp:ObjectDataSource>
              <br />
              <br />
              NOVO USUÁRIO :
              <asp:TextBox ID="TextBox1" runat="server" Width="221px" AutoPostBack="True" 
                  ontextchanged="TextBox1_TextChanged1" 
                  ToolTip="Insira o E-mail do Cliente e pressione ENTER"></asp:TextBox>
              &nbsp;
              <asp:Button ID="Button6" runat="server" Text="Habilitar" 
                  onclick="Button6_Click" />
              &nbsp;<asp:Label ID="lblaviso" runat="server" Text="USUARIO HABILITADO" 
                  Visible="False"></asp:Label>
              <br />
              ---------------------------------------------------------------------------------------------------------------<br /> 
              DADOS DA EMPRESA :<br />
              <br />
              Nome :&nbsp;&nbsp;&nbsp;
              <asp:TextBox ID="txtnomeempr" runat="server" Height="19px" Width="270px"></asp:TextBox>
              <br />
              <br />
              CNPJ/CPF:
              <asp:TextBox ID="txtcnpj" runat="server" height="19px" Width="268px"></asp:TextBox>
              &nbsp;&nbsp;
              <br />
              <br />
              TEL:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:TextBox ID="txttel" runat="server" height="19px" Width="190px"></asp:TextBox>
              <br />
              <br />
              LOUGRADOURO:
              <asp:TextBox ID="txtrua" runat="server" height="19px" Width="260px"></asp:TextBox>
              &nbsp;&nbsp; NR:
              <asp:TextBox ID="txtnr" runat="server" height="19px" Width="81px"></asp:TextBox>
              <br />
              <br />
              CEP:
              <asp:TextBox ID="txtcep" runat="server" height="20px" Width="93px"></asp:TextBox>
              &nbsp; CIDADE :
              <asp:TextBox ID="txtcidade" runat="server" height="19px"></asp:TextBox>
              &nbsp;&nbsp; ESTADO(SIGLA) :
              <asp:TextBox ID="txtuf" runat="server" height="19px" Width="28px"></asp:TextBox>
              <br />
              <br />
              <asp:Button ID="btnsalvaemp" runat="server" onclick="btnsalvaemp_Click" 
                  Text="Alterar/Salvar" Width="120px" />
              &nbsp;</asp:Panel>
      </div>
</asp:Content>

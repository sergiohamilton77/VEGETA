<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ativos.aspx.cs" Inherits="teste.Ativos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 TIPO&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server" 
        DataSourceID="MySQLDatatipo"  TabIndex="2" 
                        DataTextField="nome" DataValueField="nome" 
        AutoPostBack="True" Height="16px" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        <asp:ListItem Value="-1" Text="Selecione" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                   
                     <asp:SqlDataSource runat="server" ID="MySQLDatatipo"
                     ConnectionString="<%$ ConnectionStrings:teste %>"
                     ProviderName="<%$ ConnectionStrings:teste.ProviderName %>"            
                      SelectCommand="SELECT nome,id_tipoativo FROM Tipos_ativo"></asp:SqlDataSource>

    <br />

    <asp:Button ID="Button1" runat="server" onclick="Button1_Click"  TabIndex="3" 
        Text="Filtrar" Visible="False" />
    <br />
    
 NOME &nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtnome" runat="server" Width="200px" AutoPostBack="True"  TabIndex="1" 
        ontextchanged="txtnome_TextChanged" > </asp:TextBox>
   
&nbsp;<asp:Button ID="Button2" runat="server" Text="Filtrar" 
        CausesValidation="false" TabIndex="4" 
        onclick="Button2_Click" Visible="False" />

    <asp:TextBox ID="txtidtipo" runat="server" TabIndex="5"  Visible="False"></asp:TextBox>

    <br />
    <br />
    CALL:
    <asp:DropDownList ID="DropDownList3" runat="server"  TabIndex="6" >
        <asp:ListItem>Compra</asp:ListItem>
        <asp:ListItem>Venda</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:CheckBox ID="CheckBox1" runat="server" Visible="False" />
    &nbsp;PERIODO :<asp:DropDownList ID="DropDownList4" runat="server">
        <asp:ListItem Value="M">Manhã</asp:ListItem>
        <asp:ListItem Value="T">Tarde</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <div style="float:left;  width:500px">
        <asp:GridView ID="gridcheck" runat="server" Width="440px"   TabIndex="7" 
            onpageindexchanging="gridcheck_PageIndexChanging" 
             AllowSorting="True" 
            onrowdatabound="gridcheck_RowDataBound" 
            onrowediting="gridcheck_RowEditing" ViewStateMode="Enabled" 
            
            AutoGenerateSelectButton="True" 
            AllowPaging="True" PageSize="30" onrowupdating="gridcheck_RowUpdating" 
            onselectedindexchanging="gridcheck_SelectedIndexChanging" 
            onrowcancelingedit="gridcheck_RowCancelingEdit" 
            onselectedindexchanged="gridcheck_SelectedIndexChanged">
        </asp:GridView>
    </div>
    <div style="float:left;width:394px; height: 266px;">
        <asp:ListBox ID="lstcompra" runat="server" Height="209px" Width="123px"  TabIndex="8" 
            onselectedindexchanged="lstcompra_SelectedIndexChanged"></asp:ListBox>
        <asp:Button ID="Button5" runat="server"  TabIndex="9" Text="-" onclick="Button5_Click" />
        <asp:ListBox ID="lstvenda" runat="server" TabIndex="10"  Height="209px" Width="137px" 
            style="margin-right: 21px"></asp:ListBox>
        <asp:Button ID="Button6" runat="server"  TabIndex="11" Text="-" Width="19px" 
            onclick="Button6_Click" />
            <br />
            <br />
        <asp:Button ID="Button4" runat="server"  TabIndex="12" Text="Call Compra" Width="100px" 
            onclick="Button4_Click" />
        <asp:Button ID="Button7" runat="server"  TabIndex="13" Text="Limpar tudo" Width="79px" 
            OnClientClick="return confirm('Deseja mesmo limpar todas as informações ?');" onclick="Button7_Click"  />
        <asp:Button ID="Button3" runat="server"  TabIndex="14" Text="Call Venda" Width="106px" 
            onclick="Button3_Click" />
    </div>
  
    INSERIR ATIVO :<br />
    <br />
    <asp:TextBox ID="txtnovo" runat="server"  TabIndex="15" ></asp:TextBox>
        <asp:Button ID="Button8" runat="server"  TabIndex="16" Text="+" Width="19px" 
            onclick="Button8_Click" />
    TIPO : 

    <asp:DropDownList ID="DropDownList2" runat="server" TabIndex="17" DataSourceID="MYSQLDatatipo" 
                         DataTextField="nome" DataValueField="nome">
                        <asp:ListItem Value="-1"    Text="Selecione" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"  
                        ConnectionString="<%$ ConnectionStrings:ativos %>" 
                        SelectCommand="SELECT [nome], [id_tipoativo] FROM [Tipos_ativo]">
                    </asp:SqlDataSource>
  
    <asp:Panel ID="Panel1" runat="server" Height="206px" style="margin-left: 506px" 
        Visible="False">
        <br />
        ALTERAR ATIVO :<br />
        <br />
        NOME :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtativo" runat="server"></asp:TextBox>
        <br />
        <br />
        DESCRIÇÃO :
        <asp:TextBox ID="txtdesc" runat="server" Height="40px" Width="313px" 
            TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
        <br />
        <asp:Button ID="btnatual" runat="server" Height="26px" onclick="btnatual_Click" 
            Text="Atualizar" Width="130px" />
        <asp:Button ID="Button9" runat="server" Height="26px" onclick="Button9_Click" 
            Text="Fechar" Width="130px" />
        <asp:Button ID="Button10" runat="server" Text="Excluir" Width="130px" 
            OnClientClick="return confirm('Deseja mesmo excluir ?');" Height="26px" 
            onclick="Button10_Click" />
    </asp:Panel>
  
</asp:Content>

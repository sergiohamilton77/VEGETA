<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="Loginuser.aspx.cs" Inherits="teste.Loginuser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel2" runat="server" Height="54px" HorizontalAlign="Center" 
         Font-Bold="True" Font-Size="12pt" ForeColor="#3366CC">
         BEM VINDO A INVESTSCORP!&nbsp;&nbsp;
         <asp:Label ID="lblusuario" runat="server" Text=" "></asp:Label>
     </asp:Panel>
    <asp:Panel ID="Panel1" runat="server" Height="467px" HorizontalAlign="Center" 
         ViewStateMode="Disabled" Width="931px">
        <br />
       <asp:Label ID="lbluser" runat="server" Text=" USUARIO :"></asp:Label>
        &nbsp;&nbsp;  <asp:TextBox ID="txtuser" runat="server"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpass" runat="server" 
            Text="SENHA :"></asp:Label>
&nbsp;<asp:TextBox ID="txtpass" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Lblnovouser" runat="server" 
            Text="CRIE UM
             USUÁRIO :" Visible="False"></asp:Label>
        &nbsp;<asp:TextBox ID="txtnovouser" runat="server" 
            ontextchanged="TextBox1_TextChanged" 
            ToolTip="Escolha um usuário e Pressione &quot;Enter&quot;" Visible="False"></asp:TextBox>
        &nbsp;<br />
        <br />
        <asp:Label ID="lblnovasenha" runat="server" Text="DIGITE UMA SENHA : " 
            Visible="False"></asp:Label>
        <asp:TextBox ID="txtnovasenha" runat="server" Visible="False" 
            ontextchanged="txtnovasenha_TextChanged" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblnovasenhac" runat="server" Text="CONFIRME A SENHA : " 
            Visible="False"></asp:Label>
        <asp:TextBox ID="txtnovasenhac" runat="server" Visible="False" 
            ontextchanged="txtnovasenhac_TextChanged" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Button 
            ID="btnregistro" runat="server"  
            Text="REGISTRAR" Width="88px" BorderStyle="Inset" Visible="False" 
            onclick="btnregistro_Click" />
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Button ID="ENTRAR" runat="server" onclick="ENTRAR_Click" 
            Text="ENTRAR" Width="88px" BorderStyle="Inset" />
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" Enabled="False" 
            Font-Size="Small" ForeColor="#3366CC" Height="109px" ReadOnly="True" 
            style="font-family: 'Bahnschrift Light'; text-align: left; margin-left: 3px" 
            TextMode="MultiLine" Width="766px">Na INVESTSCORP, privacidade e segurança são prioridades e nos comprometemos com a transparência do tratamento de dados pessoais dos nossos usuários/clientes. Ao utilizar nossos serviços, você entende que coletaremos e usaremos suas informações pessoais nas formas descritas nesta Política, sob as normas da Constituição Federal de 1988 (art. 5º, LXXIX; e o art. 22º, XXX – incluídos pela EC 115/2022), das normas de Proteção de Dados (LGPD, Lei Federal 13.709/2018), das disposições consumeristas da Lei Federal 8078/1990 e as demais normas do ordenamento jurídico brasileiro aplicáveis.</asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Esqueci a senha</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:Label ID="lblenvio" runat="server" Text="EMAIL DE REGISTO: " 
            Visible="False"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="txtemail" runat="server" Height="22px" Visible="False" 
            Width="411px"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnenvio" runat="server" onclick="btnenvio_Click" Text="ENVIAR" 
            Visible="False" Width="77px" />
    </asp:Panel>
</asp:Content>

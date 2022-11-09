<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="auto.aspx.cs" Inherits="teste.auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>  
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">  
<html xmlns="http://www.w3.org/1999/xhtml">  
<head id="Head1" runat="server">  
    <title></title>  
</head>  
<body style="background-color:lightgray">  
    <form id="form1" runat="server">  
    <div>  
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">  
        </asp:ScriptManager>  
        <asp:AutoCompleteExtender ServiceMethod="GetSearch" MinimumPrefixLength="1" CompletionInterval="10"  
            EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox1" ID="AutoCompleteExtender1"  
            runat="server" FirstRowSelected="false">  
        </asp:AutoCompleteExtender>  
        <asp:Label ID="Label1" runat="server" Text="Search Name"></asp:Label>  
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>  
    </div>  
    </form>  
</body>  
</html>  
 

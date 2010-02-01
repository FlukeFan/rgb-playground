<%@ Page AutoEventWireup="true" Language="C#"%>
<script runat="server" language="C#">
    private void b1_OnClick(object sender, EventArgs e)
    {
       l1.Text = "Clicked";
    }
</script>
<html>
    <head><title>Test page</title></head>
    <body>
        <form runat="server">
            <asp:Label ID="l1" runat="server" Text="Hello" />
            
            <asp:Button ID="b1" runat="server" Text="Click1" OnClick="b1_OnClick" />
        </form>
    </body>
</html>
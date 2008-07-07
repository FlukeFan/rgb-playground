<%@ Page Language="C#"
         Inherits="Samples.GridViewTest"
         AutoEventWireup="false"
         %>
<html xmlns="http://www.w3.org/1999/xhtml">

    <head>
        <title>demo</title>
        <meta http-equiv="Page-Enter" content="progid:DXImageTransform.Microsoft.RandomDissolve(duration=0)" />
        <meta http-equiv="Page-Exit" content="progid:DXImageTransform.Microsoft.RandomDissolve(duration=0)" />
    </head>

    <body>

        <form id="_form" runat="server">
        
            <div style="position:absolute; background:lightgreen; top:10px; left:10px; width:700px; height:200px;">
                <asp:GridView ID="_gridView" runat="server" />
                <asp:Button ID="Button1" runat="server" Text="Postback" />
            </div>
            
        </form>
        
    </body>

</html>

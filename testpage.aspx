<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testpage.aspx.cs" Inherits="FalaBricks.LegoSystem.UI.testpage" %>

<form runat="server">
    <asp:table runat="server" id="UI_Table_Test">
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" ID="UI_LBL_Output1" Text="1"></asp:Label><br />
            <asp:Label runat="server" ID="UI_LBL_Output2" Text="2"></asp:Label><br />
            <asp:Label runat="server" ID="UI_LBL_Output3" Text="3"></asp:Label><br />
            <asp:Label runat="server" ID="UI_LBL_Output4" Text="4"></asp:Label><br />
            <asp:Label runat="server" ID="UI_LBL_Output5" Text="5"></asp:Label><br />
            <asp:Label runat="server" ID="UI_LBL_Output6" Text="6"></asp:Label><br />
        </asp:TableCell>
    </asp:TableRow>
</asp:table>

    <%-- SAVING AN IMAGE TO A FOLDER LOCALLY --%>

    <asp:table runat="server" id="UI_Table_ImageTest">
    <asp:TableRow>
        <asp:TableCell>
            <asp:FileUpload runat="server" ID="UI_FUL_Image" Accept=".png,.jpg,.jpeg,.gif" Text="Choose an image" /> <br/>
            <asp:Button runat="server" OnClick="Test_Click" Text="CLICK TO SAVE IMAGE"></asp:Button>
            <asp:ImageButton runat="server" ImageUrl="~/image/FALALogo.png" Height="150" Width="150"></asp:ImageButton> <br />
        </asp:TableCell>
        </asp:TableRow>

        <%-- AREA TO RETRIEVE AN IMAGE FROM THE DATABASE AND DISPLAY IN A BUTTON --%>
        <asp:TableRow>
            <asp:TableCell>
                    <asp:ImageButton ID="UI_ImgBtn_Test" Height="150" Width="150" OnClick="GetImageTest_Click" runat="server"></asp:ImageButton>
            </asp:TableCell>
        
    </asp:TableRow>
    
</asp:table>
</form>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SubmitPost.aspx.cs" Inherits="SubmitPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">'
    <br />
    <br />
    <br />
    <div class="SubmitBG">
        <div class="btn-group-lg ButtonRow">
            <asp:Button ID="TextSelectBtn" CssClass="btn-group-lg btn btn-secondary ThirdOfButton"  runat="server" Text="Text" OnClick="TextSelectBtn_Click" />
            <asp:Button ID="ImageSelectBtn" CssClass="btn-group-lg btn btn-secondary ThirdOfButton"  runat="server" Text="Image" OnClick="ImageSelectBtn_Click" />
            <asp:Button ID="LinkSelectBtn" CssClass="btn-group-lg btn btn-secondary ThirdOfButton"  runat="server" Text="Link" OnClick="LinkSelectBtn_Click" />
        </div>
        <hr  style="color:whitesmoke"/>
        <div class="SubmitDiv">
            <asp:TextBox ID="TitleTB" runat="server" Placeholder="Title" ForeColor="WhiteSmoke" BackColor="#787d87" CssClass="SubmitTitle"></asp:TextBox>
        </div>
        <hr  style="color:whitesmoke"/>
        <div class="SubmitDiv">
            <asp:MultiView ID="SubmitMV" runat="server" ActiveViewIndex="0">
                <asp:View ID="TextView" runat="server">
                    <asp:TextBox ID="ContentTextTB" runat="server" Placeholder="Text(Optional)" ForeColor="WhiteSmoke" BackColor="#787d87" CssClass="SubmitContent" TextMode="MultiLine" Rows="5">
                    </asp:TextBox>
                </asp:View>
                <asp:View ID="ImageView" runat="server">
                    <asp:FileUpload ID="ImageFU" Accept=".png,.jpg,.jpeg,.gif" Text="Choose an image" runat="server" CssClass="SubmitContent" />
                </asp:View>
                <asp:View ID="LinkView" runat="server">
                    <asp:TextBox ID="LinkTextTB" runat="server" ForeColor="WhiteSmoke" BackColor="#787d87" CssClass="SubmitContent" TextMode="Url">
                    </asp:TextBox>
                </asp:View>
            </asp:MultiView>
        </div>
        <hr  style="color:whitesmoke"/>
        <asp:Button ID="SubmitBtn" runat="server" Text="Post" CssClass="btn-dark btn ThirdOfButton" OnClick="SubmitBtn_Click" />

     </div>
</asp:Content>


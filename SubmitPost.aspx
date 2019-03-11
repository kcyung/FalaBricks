<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SubmitPost.aspx.cs" Inherits="SubmitPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">'
    <br />
    <br />
    <br />
    <div class="SubmitContainer">
        <asp:Button ID="TextSelectionBtn" runat="server" Text="Text" CssClass="btn-group-lg btn SubmitButtonLeft btn-secondary"/> 
        <asp:Button ID="ImageSelectionBtn" runat="server" Text="Image" CssClass="btn-group-lg btn SubmitButtonMiddle btn-secondary" />
        <asp:Button ID="LinkSelectionBtn" runat="server" Text="Link" CssClass="btn-group-lg btn SubmitButtonRight btn-secondary"/>
        
        <asp:TextBox ID="TitleTB" runat="server" CssClass="SubmitTitleTB RoundTB" BackColor="#81878e" ForeColor="White" Width="100%" placeholder="Title"></asp:TextBox>
        <asp:MultiView ID="SubmitMV" runat="server" ActiveViewIndex="0">
            <asp:View ID="TextView" runat="server">
                <asp:TextBox ID="TextTB" runat="server" CssClass="RoundTB SubmitContent" BackColor="#81878e" ForeColor="White" Width="100%" TextMode="MultiLine" placeholder="Text"></asp:TextBox>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>


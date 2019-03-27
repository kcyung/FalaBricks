using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FalaBricks.LegoSystem.Domain;
using System.Web.UI.HtmlControls;

public partial class Comments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LS Controller = new LS();
        if(Session["PostID"] !=  null && Session["PostID"].ToString() != string.Empty)
        {
            Post post = Controller.FindThreadPostByMainPostReference(Convert.ToInt32(Session["PostID"].ToString()))[0];
            Main.Controls.Add(CreateContent(post));
        }

    }

    private HtmlGenericControl CreateContent(Post post)
    {
        //Create the controller;
        LS Controller = new LS();

        //User Data
        int voteCount = 0;
        string userName = null;

        //Create the Div
        HtmlGenericControl ContentDiv = new HtmlGenericControl("div");
        ContentDiv.ID = post.PostID.ToString();
        ContentDiv.Attributes["class"] = post.ContainsImage ? "CommentPostContainerwImage" : "CommentPostContainerwoImage";
        ContentDiv.Attributes["style"] = "margin: 0 auto;  margin-bottom:15px; background-color:#464749; border-radius: 10px;  ";

        if (Session["User"] != null)
        {
            userName = Session["User"].ToString();
            voteCount = Controller.FindUserVoteForPost(post.PostID, userName);
        }

        //Create div to hold the upvote, downvote, vote count and comment count
        HtmlGenericControl ToolLine = new HtmlGenericControl("div");
        ToolLine.Attributes["class"] = "CommentNavLine";

        //Create the upvote control
        ImageButton Upvote = new ImageButton();
        Upvote.ID = post.PostID.ToString() + "uv";
        Upvote.CssClass = "CommentPostUpvote";

        if (voteCount > 0)
            Upvote.ImageUrl = "image/UpVoteOn.png";
        else
            Upvote.ImageUrl = "image/UpVoteOff.png";

        Upvote.CommandArgument = post.PostID.ToString();
        Upvote.Click += new ImageClickEventHandler(Upvote_Click);
        ToolLine.Controls.Add(Upvote);

        //Create the downvote control
        ImageButton Downvote = new ImageButton();
        Downvote.ID = post.PostID.ToString() + "dv";
        Downvote.CssClass = "CommentPostDownvote";
        ToolLine.Controls.Add(Downvote);

        if (voteCount < 0)
            Downvote.ImageUrl = "image/DownVoteOn.png";
        else
            Downvote.ImageUrl = "image/DownVoteOff.png";


        //Create the comment counter lbl(Will be hyperlink in the future)
        Label CommentCountLbl = new Label();
        CommentCountLbl.ID = post.PostID.ToString() + "cc";
        CommentCountLbl.CssClass = "CommentCommentCount";
        CommentCountLbl.ForeColor = System.Drawing.Color.WhiteSmoke;
        CommentCountLbl.Text = "Comments: " + Controller.GetThreadCount((int)post.MainPostReferenceID) + "";
        ToolLine.Controls.Add(CommentCountLbl);

        //Create the Vote count Label
        Label VotecountLbl = new Label();
        VotecountLbl.ID = post.PostID.ToString() + "vc";
        VotecountLbl.CssClass = "CommentPostVoteCount";
        VotecountLbl.ForeColor = System.Drawing.Color.WhiteSmoke;
        VotecountLbl.Text = (post.UpCount - post.DownCount) + "";
        ToolLine.Controls.Add(VotecountLbl);

        //Add the tooline div to the contentDiv
        ContentDiv.Controls.Add(ToolLine);

        //Create the Post user Lbl
        Label PostUserLbl = new Label();
        PostUserLbl.ID = post.PostID.ToString() + "pu";
        PostUserLbl.CssClass = "CommentPostUser";
        PostUserLbl.ForeColor = System.Drawing.Color.WhiteSmoke;
        PostUserLbl.Text = string.Format("Posted by: {0} on {1}", post.UserName, post.PostDate.ToShortDateString());
        ContentDiv.Controls.Add(PostUserLbl);

        //Create the Title Label
        Label TitleLbl = new Label();
        TitleLbl.ID = post.PostID.ToString() + "tl";
        TitleLbl.Font.Size = 20;
        TitleLbl.ForeColor = System.Drawing.Color.WhiteSmoke;
        TitleLbl.CssClass = "CommentPostTitle";
        TitleLbl.Text = post.Title;
        ContentDiv.Controls.Add(TitleLbl);
        
        
        //Content whether image or text
        if(post.ContainsImage)
        {
            //Create the div that holds the anchor
            HtmlGenericControl ImageDiv = new HtmlGenericControl("div");
            ImageDiv.ID = post.PostID.ToString() + "ic";
            ImageDiv.Attributes["class"] = "ImageDivContent";
            ImageDiv.Attributes["style"] = "margin: 0 auto;";

            //Create the anchor that holds the img acts as a hyperlink
            
            //HtmlAnchor anchor = new HtmlAnchor();
            //anchor.HRef = "default.aspx"; //This can be changed later to be dynamic;
            

            //Create the image
            ImageButton img = new ImageButton();
            foreach (ImagePic ip in Controller.FindImagesByPostID(post.PostID))
            {
                img.CommandArgument = post.PostID.ToString();
                img.Attributes["width"] = "auto";
                img.Attributes["height"] = "auto";
                img.Attributes["max-height"] = "400px";
                post.PostImages = Controller.FindImagesByPostID(post.PostID);
                img.ImageUrl = ip.ImagePath;//"image/img150.png";
                ImageDiv.Controls.Add(img);
            }
  
            ContentDiv.Controls.Add(ImageDiv);
        }
        else
        {
            Label ContentLbl = new Label();
            ContentLbl.ID = post.PostID.ToString() + "tl";
            ContentLbl.Font.Size = 16;
            ContentLbl.ForeColor = System.Drawing.Color.WhiteSmoke;
            ContentLbl.Text = post.PostText;
            ContentDiv.Controls.Add(ContentLbl);
        }
        


        return ContentDiv;
    }

    protected void Upvote_Click(object sender, EventArgs e)
    {
        if (!(sender is ImageButton))
            return;

        // Must be logged in to vote
        if (Session["User"] == null)
            return;

        ImageButton upVoteButton = (ImageButton)sender;

        // Find the control for the downVoteButton with the same PostID
        string postIDstring = upVoteButton.CommandArgument;
        int postID = Convert.ToInt32(upVoteButton.CommandArgument);

        ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");

        string downVoteID = postIDstring + "dv";
        ImageButton downVoteButton = (ImageButton)cph.FindControl(downVoteID);

        LS Controller = new LS();
        bool Confirmation;
        //string userName = "Bob";

        // Check the state of upvote before event 
        if (upVoteButton.ImageUrl == "image/UpVoteOff.png")
        {
            // Check if downVote button state was on -> turn it off
            if (downVoteButton.ImageUrl == "image/DownVoteOn.png")
                downVoteButton.ImageUrl = "image/DownVoteOff.png";

            // Turn on the upVote button
            upVoteButton.ImageUrl = "image/UpVoteOn.png";

            // Send the vote to sql db (parameters:user, postID, and value (+1 = upvoted) 0 (-1 =downvoted))
            //  Controller.ModifyVotingSystem(postID, userName, 1);
        }
        else  // upVote button was previously On
        {
            upVoteButton.ImageUrl = "image/UpVoteOff.png";
            // Confirmation = Controller.ModifyVotingSystem(postID, userName, 0);
        }
    }

    protected void Downvote_Click(object sender, EventArgs e)
    {
        if (!(sender is ImageButton))
            return;

        if (Session["User"] == null)
            return;

        ImageButton downVoteButton = (ImageButton)sender;

        // Find the control for the downVoteButton with the same PostID
        string postIDstring = downVoteButton.CommandArgument;
        int postID = Convert.ToInt32(downVoteButton.CommandArgument);

        ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");

        string upVoteID = postIDstring + "uv";
        ImageButton upVoteButton = (ImageButton)cph.FindControl(upVoteID);

        LS Controller = new LS();
        bool Confirmation;
        //string userName = "Bob";

        // Check the state of downvote before event 
        if (downVoteButton.ImageUrl == "image/DownVoteOff.png")
        {
            // Check if upVote button state was on -> turn it off
            if (upVoteButton.ImageUrl == "image/UpVoteOn.png")
                upVoteButton.ImageUrl = "image/UpVoteOff.png";

            // Turn on the downVote button
            downVoteButton.ImageUrl = "image/DownVoteOn.png";

            // Send the vote to sql db (parameters:user, postID, and value (+1 = upvoted) 0 (-1 =downvoted))

            //  Confirmation = Controller.ModifyVotingSystem(postID, userName, -1);
        }
        else  // downVote button was previously On
        {
            downVoteButton.ImageUrl = "image/DownVoteOff.png";
            //Confirmation = Controller.ModifyVotingSystem(postID, userName, 0);
        }
    }
}
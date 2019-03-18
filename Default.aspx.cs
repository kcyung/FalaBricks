using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FalaBricks.LegoSystem.Domain;
using System.Web.UI.HtmlControls;

namespace FalaBricks.LegoSystem.UI
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // DISPLAYING ALL POSTS ON MAIN PAGE
            LS Controller = new LS();
            List<Post> mainPosts = Controller.FindMainForumPost();

            foreach (Post mp in mainPosts)
            {
                Main.Controls.Add(CreatePost(mp));
            }

            // DISPLAYING ALL THREADPOST
            int mainPostID = 1;
            /*
            List<Post> threadPosts = Controller.FindThreadPostByMainPostReference(mainPostID);
            
            foreach(Post tp in threadPosts)
            {
                // Dispaly UserName + Display DateTime
                // Display Title
                // Check if there's postText - and display it if true
                // Check if there's images and display all images 
                // Display UpVote CB, Counter, DownVote CB, Add Reply Button
            }      
            */
            //            Post post = new Post(1, "henry", DateTime.Now, "title", "blah blah blah", 0, 0, true, 1, true);


            //Main.Controls.Add(CreatePost(post));
        }

        /// <summary>
        /// Creates the div that holds all of the post content
        /// </summary>
        /// <param name="post">Post object that contains all the data</param>
        /// <returns></returns>
        private HtmlGenericControl CreatePost(Post post)
        {
            //Create the controller;
            LS Controller = new LS();
            //Create the Div
            HtmlGenericControl PostDiv = new HtmlGenericControl("div");
            PostDiv.ID = post.PostID.ToString();
            PostDiv.Attributes["class"] = "PostContainer";
            PostDiv.Attributes["style"] = "margin: 0 auto;  margin-bottom:15px; background-color:#464749; border-radius: 10px;  ";

            // Check if a user is logged in; in not - voteCount = 0;
            int voteCount = 0;
            string userName = null;

            if (Session["User"] != null)
            {
                userName = Session["User"].ToString();
                voteCount = Controller.FindUserVoteForPost(post.PostID, userName);
            }

            // Check if the user has voted on this post before and assign appropriate upvote/downvote image
           // int voteCount = Controller.FindUserVoteForPost(post.PostID, "Bob");

            //Create the upvote control
            ImageButton Upvote = new ImageButton();
            Upvote.ID = post.PostID.ToString() + "uv";
            Upvote.CssClass = "PostUpvote";

            if (voteCount > 0)
                Upvote.ImageUrl = "image/UpVoteOn.png";
            else
                Upvote.ImageUrl = "image/UpVoteOff.png";

            Upvote.CommandArgument = post.PostID.ToString();
            Upvote.Click += new ImageClickEventHandler(Upvote_Click);
            PostDiv.Controls.Add(Upvote);

            //Create the downvote control
            ImageButton Downvote = new ImageButton();
            Downvote.ID = post.PostID.ToString() + "dv";
            Downvote.CssClass = "PostDownvote";

            if (voteCount < 0)
                Downvote.ImageUrl = "image/DownVoteOn.png";
            else
                Downvote.ImageUrl = "image/DownVoteOff.png";

            Downvote.CommandArgument = post.PostID.ToString();
            Downvote.Click += new ImageClickEventHandler(Downvote_Click);
            PostDiv.Controls.Add(Downvote);

            //Create the Vote count Label
            Label VotecountLbl = new Label();
            VotecountLbl.ID = post.PostID.ToString() + "vc";
            VotecountLbl.CssClass = "PostVoteCount";
            VotecountLbl.ForeColor = System.Drawing.Color.WhiteSmoke;
            VotecountLbl.Text = (post.UpCount - post.DownCount) + "";
            PostDiv.Controls.Add(VotecountLbl);

            //Create the Title Label(Will be hyperlink in the future)
            Label TitleLbl = new Label();
            TitleLbl.ID = post.PostID.ToString() + "tl";
            TitleLbl.Font.Size = 20;
            TitleLbl.ForeColor = System.Drawing.Color.WhiteSmoke;
            TitleLbl.Text = post.Title;
            if (post.ContainsImage)
            {
                TitleLbl.CssClass = "PostTitlewImage";
            }
            else
            {
                TitleLbl.CssClass = "PostTitlewoImage";
            }

            PostDiv.Controls.Add(TitleLbl);

            //Create the comment counter lbl(Will be hyperlink in the future)
            Label CommentCountLbl = new Label();
            CommentCountLbl.ID = post.PostID.ToString() + "cc";
            CommentCountLbl.CssClass = "PostCommentCount";
            CommentCountLbl.ForeColor = System.Drawing.Color.WhiteSmoke;
            CommentCountLbl.Text = "Comments: " + Controller.GetThreadCount((int)post.MainPostReferenceID) + "";
            PostDiv.Controls.Add(CommentCountLbl);

            //Create the Post date Lbl
            Label PostDateLbl = new Label();
            PostDateLbl.ID = post.PostID.ToString() + "pd";
            PostDateLbl.CssClass = "PostDate";
            PostDateLbl.ForeColor = System.Drawing.Color.WhiteSmoke;
            PostDateLbl.Text = post.PostDate.ToLongDateString();
            PostDiv.Controls.Add(PostDateLbl);

            //Create the PostBy Label
            Label PostByLbl = new Label();
            PostByLbl.ID = post.PostID.ToString() + "pb";
            PostByLbl.CssClass = "PostUser";
            PostByLbl.ForeColor = System.Drawing.Color.WhiteSmoke;
            PostByLbl.Text = "Submitted By: " + post.UserName;
            PostDiv.Controls.Add(PostByLbl);

            //Create Image if it contains image
            if (post.ContainsImage)
            {
                //Create the div that holds the anchor
                HtmlGenericControl ImageDiv = new HtmlGenericControl("div");
                ImageDiv.ID = post.PostID.ToString() + "ic";
                ImageDiv.Attributes["class"] = "ImageDivPreview";
                ImageDiv.Attributes["style"] = "margin: 0 auto;";

                //Create the anchor that holds the img acts as a hyperlink
                /*
                HtmlAnchor anchor = new HtmlAnchor();
                anchor.HRef = "default.aspx"; //This can be changed later to be dynamic;
                */

                //Create the image
                ImageButton img = new ImageButton();
                img.CommandArgument = post.PostID.ToString();
                img.Attributes["width"] = "150px";
                img.Attributes["height"] = "150px";
                post.PostImages = Controller.FindImagesByPostID(post.PostID);
                img.ImageUrl = post.PostImages[0].ImagePath;//"image/img150.png";
                /*
                System.Web.UI.WebControls.Image PostImage = new System.Web.UI.WebControls.Image();
                PostImage.ImageUrl = "image/img150.png"; //Link the image to file
                */
                //Add the image to the anchor which is added to the div
                //anchor.Controls.Add(PostImage);
                ImageDiv.Controls.Add(img);
                PostDiv.Controls.Add(ImageDiv);
            }

            return PostDiv;
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

        protected void TestButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/testpage.aspx");
        }
    }
}

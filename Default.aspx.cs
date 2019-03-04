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
            
            foreach(Post mp in mainPosts)
            {
                // Display Username + DateTime of Post
                // Display Title
                // Check if there's an image + post it
                // Display UpVote CheckBox, Counter, DownVote Check Box
                Main.Controls.Add(CreatePost(mp));

                // Comment counter for the thread 
                //int threadCount = mp.GetThreadCount();
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
            Post post = new Post(1, "henry", DateTime.Now, "title", "blah blah blah", 0, 0, true, 1, true);


            //Main.Controls.Add(CreatePost(post));
        }

        /// <summary>
        /// Creates the div that holds all of the post content
        /// </summary>
        /// <param name="post">Post object that contains all the data</param>
        /// <returns></returns>
        private HtmlGenericControl CreatePost(Post post)
        {
            //Create the Div
            HtmlGenericControl PostDiv = new HtmlGenericControl("div");
            PostDiv.ID = post.PostID.ToString();
            PostDiv.Attributes["class"] = "PostContainer";
            PostDiv.Attributes["style"] = "margin: 0 auto;  margin-bottom:15px; border-style: solid; border-width: 1px;";

            //Create the upvote control
            CheckBox Upvote = new CheckBox();
            Upvote.ID = post.PostID.ToString() + "uv";
            Upvote.CssClass = "PostUpvote";
            Upvote.Text = "upvote";
            PostDiv.Controls.Add(Upvote);

            //Create the downvote control
            CheckBox Downvote = new CheckBox();
            Downvote.ID = post.PostID.ToString() + "uv";
            Downvote.CssClass = "PostDownvote";
            Downvote.Text = "downvote";
            PostDiv.Controls.Add(Downvote);

            //Create the Vote count Label
            Label VotecountLbl = new Label();
            VotecountLbl.ID = post.PostID.ToString() + "vc";
            VotecountLbl.CssClass = "PostVoteCount";
            PostDiv.Controls.Add(VotecountLbl);

            //Create the Title Label(Will be hyperlink in the future)
            Label TitleLbl = new Label();
            TitleLbl.ID = post.PostID.ToString() + "tl";
            TitleLbl.Font.Size = 20;
            TitleLbl.Text = post.PostText;
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
            PostDiv.Controls.Add(CommentCountLbl);

            //Create the Post date Lbl
            Label PostDateLbl = new Label();
            PostDateLbl.ID = post.PostID.ToString() + "pd";
            PostDateLbl.CssClass = "PostDate";
            PostDateLbl.Text = post.PostDate.ToLongDateString();
            PostDiv.Controls.Add(PostDateLbl);

            //Create the PostBy Label
            Label PostByLbl = new Label();
            PostByLbl.ID = post.PostID.ToString() + "pb";
            PostByLbl.CssClass = "PostUser";
            PostByLbl.Text = "Submitted By: " + post.UserName;
            PostDiv.Controls.Add(PostByLbl);

            //Create Image if it contains image
            if (post.ContainsImage)
            {
                System.Web.UI.WebControls.Image PostImage = new System.Web.UI.WebControls.Image();
                PostImage.ImageUrl = "image/img150.png";
                PostImage.CssClass = "PostImage";
                PostDiv.Controls.Add(PostImage);
            }

            return PostDiv;
        }
    }
}

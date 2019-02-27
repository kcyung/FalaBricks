using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FalaBricks.LegoSystem.Domain;

namespace FalaBricks.LegoSystem.UI
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Post post = new Post(1, "henry", DateTime.Now, "title", "blah blah blah", 0, 0, true, 1, true);

            System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            createDiv.ID = "blah";
            Button upvote = new Button();
            upvote.Text = "Upvote";
            Button downvote = new Button();
            downvote.Text = "Downvote";
            upvote.CssClass = "PostUpvote";
            downvote.CssClass = "PostDownvote";
            createDiv.Attributes["class"] = "PostContainer";
            createDiv.Attributes["style"] = "margin: 0 auto;";
            createDiv.Controls.Add(upvote);
            createDiv.Controls.Add(downvote);
            Label titlelabel = new Label();
            titlelabel.Text = post.Title;
            Label textlabel = new Label();
            textlabel.Text = post.PostText;
            titlelabel.CssClass = "PostUser";
            textlabel.CssClass = "PostTitle";
            createDiv.Controls.Add(textlabel);
            createDiv.Controls.Add(titlelabel);

            Main.Controls.Add(createDiv);
        }
    }
}

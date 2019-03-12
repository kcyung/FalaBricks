using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FalaBricks.LegoSystem.Domain;

public partial class SubmitPost : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void TextSelectBtn_Click(object sender, EventArgs e)
    {
        SubmitMV.ActiveViewIndex = 0;
    }

    protected void ImageSelectBtn_Click(object sender, EventArgs e)
    {
        SubmitMV.ActiveViewIndex = 1;
    }

    protected void LinkSelectBtn_Click(object sender, EventArgs e)
    {
        SubmitMV.ActiveViewIndex = 2;
    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        if (TitleTB.Text != null || TitleTB.Text != string.Empty)
        {
            switch (SubmitMV.ActiveViewIndex)
            {
                case 0:
                    SubmitNewTextPost();
                    break;
                case 1:
                    SubmitNewImagePost();
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }

    }

    private void SubmitNewTextPost()
    {
        LS Controller = new LS();
        if (ContentTextTB.Text == string.Empty || ContentTextTB.Text == null)
        {
            Controller.CreatePost("testuser", DateTime.Now, TitleTB.Text, "", true, null, false);
        }
        else
        {
            Controller.CreatePost("testuser", DateTime.Now, TitleTB.Text, ContentTextTB.Text, true, null, false);
        }
        Response.Redirect("~/Default.aspx");
    }

    private void SubmitNewImagePost()
    {
        LS Controller = new LS();
        int PostID = 0;
  
        if(ImageFU.HasFiles)
        {
            // Add the post to the db and retrieve the PostID
            PostID = Controller.CreatePost("testuser", DateTime.Now, TitleTB.Text, "", true, null, true);

            // Create folder to store image for new main post
            string path = @"~/Images/PostID" + PostID.ToString() + @"/";

            if (!System.IO.Directory.Exists(Server.MapPath(@"~/Images/PostID" + PostID.ToString() + @"/")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(path));
            }

            // Save image locally and store file path for each image
            foreach (HttpPostedFile file in ImageFU.PostedFiles)
            {
                file.SaveAs(Server.MapPath(path + file.FileName));
                Controller.AddImage(PostID, path + file.FileName);
            }

            Response.Redirect("~/Default.aspx");  
        }
    }
}
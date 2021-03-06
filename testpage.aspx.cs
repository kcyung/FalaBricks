﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FalaBricks.LegoSystem.Domain;

namespace FalaBricks.LegoSystem.UI
{
    public partial class testpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Test 
            LS Controller = new LS();
            //bool Confirmation = Controller.AddImage(7, "test from ui");
            //if (Confirmation)
            //{
            //    UI_LBL_Output1.Text = "Success";
            //}
            //else
            //    UI_LBL_Output1.Text = "failed";

            //bool Confirmation2 = Controller.CreatePost("kyung4Test", DateTime.Now, "last post on page 1", "This is just a random test", true, null, false);
            //Controller.CreatePost("kyung4Test", DateTime.Now, "last post on page 1", "first post on page 2???", true, null, false);
            //if (Confirmation2)
            //    UI_LBL_Output2.Text = "Success";
            //else
            //    UI_LBL_Output2.Text = "Fail";

            //bool Confirmation3 = Controller.CreatePost("kyung4Test", DateTime.Now, "TestingFromUI", "This is just a random test", false, 17, false);
            //if (Confirmation2)
            //    UI_LBL_Output3.Text = "Success";
            //else
            //    UI_LBL_Output3.Text = "Fail";
            //List<ImagePic> imageList = Controller.FindImagesByPostID(1);
            //foreach(ImagePic i in imageList)
            //{
            //    Response.Write(i.ImageID + ":" + i.ImagePath + "<br />");
            //}
            //List<Post> mainPosts = Controller.FindMainForumPost();
            //foreach (Post p in mainPosts)
            //{
            //    Response.Write("ThreadCount: " + Controller.GetThreadCount(p.MainPostReferenceID.Value)  + " PostID: " + p.PostID + ", UserName: " + p.UserName + ", PostDate: " + p.PostDate + ", Title: " + p.Title +
            //       ", PostText: " + p.PostText + " UpCount: " + p.UpCount + " Counter: " + (p.UpCount - p.DownCount).ToString() + " DownCount: " + p.DownCount + "<br />");
            //}
            //List<Post> page1 = Controller.FindMainForumPostByPage(1);
            //foreach (Post p in page1)
            //{
            //    Response.Write("PostID: " + p.PostID + ", UserName: " + p.UserName + ", PostDate: " + p.PostDate + ", Title: " + p.Title +
            //        ", PostText: " + p.PostText + " UpCount: " + p.UpCount + " Counter: " + (p.UpCount - p.DownCount).ToString() + " DownCount: " + p.DownCount + "<br />");
            //}
            //Response.Write("--------------------------------");
            //List<Post> page2 = Controller.FindMainForumPostByPage(2);
            //foreach (Post p in page2)
            //{
            //    Response.Write("PostID: " + p.PostID + ", UserName: " + p.UserName + ", PostDate: " + p.PostDate + ", Title: " + p.Title +
            //        ", PostText: " + p.PostText + " UpCount: " + p.UpCount + " Counter: " + (p.UpCount - p.DownCount).ToString() + " DownCount: " + p.DownCount + "<br />");
            //}

            //List<Post> threadPosts = Controller.FindThreadPostByMainPostReference(1);
            //foreach (Post p in threadPosts)
            //{
            //    Response.Write("PostID: " + p.PostID + ", UserName: " + p.UserName + ", PostDate: " + p.PostDate + ", Title: " + p.Title +
            //        ", PostText: " + p.PostText + " UpCount: " + p.UpCount + " Counter: " + (p.UpCount - p.DownCount).ToString() + " DownCount: " + p.DownCount + "<br />");
            //}

            //Controller.ModifyDownCount(1, 5);
            //Controller.ModifyUpCount(1, 8);
        }


        // This is how we store images locally
        protected void Test_Click(object sender, EventArgs e)
        {
            if (!UI_FUL_Image.HasFiles)
            {
                Response.Write("No file selected");
                return;
            }

            int PostID = 3;

            if (UI_FUL_Image.HasFiles)
            {
                string path = @"~/Images/PostID" + PostID.ToString() + @"/";
                if (!System.IO.Directory.Exists(Server.MapPath(@"~/Images/PostID" + PostID.ToString() + @"/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(path));
                }

                UI_FUL_Image.SaveAs(Server.MapPath(path + UI_FUL_Image.FileName));
                LS Controller = new LS();

                bool Confirmation = Controller.AddImage(PostID, path + UI_FUL_Image.FileName);
                Response.Write(Confirmation);
            }
        }

        // THis is how we retrieve an image and display it back into a button
        protected void GetImageTest_Click(object sender, EventArgs e)
        {
            LS Controller = new LS();
            List<ImagePic> imageList = Controller.FindImagesByPostID(3);
            UI_ImgBtn_Test.ImageUrl = imageList.Last().ImagePath;
        }
    }
}
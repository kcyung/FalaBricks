using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FalaBricks.LegoSystem.Domain
{
    public class ImagePic
    {
        public int ImageID { get; set; }
        public int PostID { get; set; }
        public string ImagePath { get; set; }

        public ImagePic(int imageID, int postID, string imagePath)
        {
            ImageID = imageID;
            PostID = postID;
            ImagePath = imagePath;
        }
    }
}
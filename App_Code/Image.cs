using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FalaBrick.LegoSystem.Domain
{
    public class Image
    {
        public int ImageID { get; set; }
        public int PostID { get; set; }
        public string ImagePath { get; set; }

        public Image(int imageID, int postID, string imagePath)
        {
            ImageID = imageID;
            PostID = postID;
            ImagePath = imagePath;
        }
    }
}
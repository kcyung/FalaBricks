using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FalaBrick.LegoSystem.Domain
{
    public class Post
    {
        public int PostID { get; set; }
        public string UserName { get; set; }
        public DateTime PostDate { get; set; }
        public string Title { get; set; }
        public string PostText { get; set; }
        public int UpCount { get; set; }
        public int DownCount { get; set; }
        public bool IsAMainPost { get; set; }
        public int MainPostReferenceID { get; set; }
        public bool ContainsImage { get; set; }

        public List<Image> PostImages { get; set; }

        public Post(int postID, string userName, DateTime postDate, string title,
            string postText, int upCount, int downCount, bool isAMainPost,
            int mainPostReferenceID, bool containsImage)
        {
            PostID = postID;
            UserName = userName;
            PostDate = postDate;
            Title = title;
            PostText = postText;
            UpCount = upCount;
            DownCount = downCount;
            IsAMainPost = isAMainPost;
            MainPostReferenceID = mainPostReferenceID;
            ContainsImage = containsImage;
            PostImages = null;
        }
    }
}
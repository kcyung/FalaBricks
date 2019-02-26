using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FalaBricks.LegoSystem.TechnicalServices;

namespace FalaBricks.LegoSystem.Domain
{
    public class LegoSystem
    {
        public bool CreatePost(string userName, DateTime postDate, string title, string postText,
            bool isMain, int mainReferencePostID, bool ContainsImage)
        {
            PostManager Manager = new PostManager();
            bool Success = Manager.AddPost(userName, postDate, title, postText,
                    isMain, mainReferencePostID, ContainsImage);
            return Success;
        }

        public bool AddImage(int postID, string imagePath)
        {
            PostManager Manager = new PostManager();
            bool Success = Manager.AddImage(postID, imagePath);
            return Success;
        }

        public List<Post> FindMainForumPost()
        {
            PostManager Manager = new PostManager();
            return Manager.GetMainPost();
        }

        public List<Post> FindMainForumPostByPage(int pageNumber)
        {
            PostManager Manager = new PostManager();
            return Manager.GetMainPostByPage(pageNumber);
        }

        public List<Image> FindImagesByPostID(int postID)
        {
            PostManager Manager = new PostManager();
            return Manager.GetImagesByPostID(postID);
        }

        public List<Post> FindThreadPostByMainPostReference(int mainPostReferenceID)
        {
            PostManager Manager = new PostManager();
            return Manager.GetThreadByMainPostReference(mainPostReferenceID);
        }
    }
}
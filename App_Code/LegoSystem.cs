using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FalaBricks.LegoSystem.TechnicalServices;

namespace FalaBricks.LegoSystem.Domain
{
    public class LegoSystem
    {
        // Adds a post to the database
        public bool CreatePost(string userName, DateTime postDate, string title, string postText,
            bool isMain, int mainReferencePostID, bool ContainsImage)
        {
            PostManager Manager = new PostManager();
            bool Success = Manager.AddPost(userName, postDate, title, postText,
                    isMain, mainReferencePostID, ContainsImage);
            return Success;
        }

        // Adds an image to the database
        public bool AddImage(int postID, string imagePath)
        {
            PostManager Manager = new PostManager();
            bool Success = Manager.AddImage(postID, imagePath);
            return Success;
        }

        // Pulls all the main forum posts from the database
        public List<Post> FindMainForumPost()
        {
            PostManager Manager = new PostManager();
            return Manager.GetMainPost();
        }

        // Pulls all main forums posts for a given page number from the database
        public List<Post> FindMainForumPostByPage(int pageNumber)
        {
            PostManager Manager = new PostManager();
            return Manager.GetMainPostByPage(pageNumber);
        }

        // Gets all the images associated with one post from the database
        public List<Image> FindImagesByPostID(int postID)
        {
            PostManager Manager = new PostManager();
            return Manager.GetImagesByPostID(postID);
        }

        // Gets all thread posts for one main post ID from the database
        public List<Post> FindThreadPostByMainPostReference(int mainPostReferenceID)
        {
            PostManager Manager = new PostManager();
            return Manager.GetThreadByMainPostReference(mainPostReferenceID);
        }

        // Changes the UpCounter for a post ID to the database
        public bool ModifyUpCount(int postID, int upCount)
        {
            PostManager Manager = new PostManager();
            bool Success = Manager.UpdateUpvoteCounter(postID, upCount);
            return Success;
        }

        // Changes the DownCounter for a postID to the database
        public bool ModifyDownCount(int postID, int upCount)
        {
            PostManager Manager = new PostManager();
            bool Success = Manager.UpdateDownvoteCounter(postID, upCount);
            return Success;
        }
    }
}
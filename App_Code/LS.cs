using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FalaBricks.LegoSystem.TechnicalServices;

namespace FalaBricks.LegoSystem.Domain
{
    public class LS
    {
        // Adds a post to the database
        public int CreatePost(string userName, DateTime postDate, string title, string postText,
            bool isMain, int? mainReferencePostID, bool ContainsImage)
        {
            PostManager Manager = new PostManager();
            int PostID = Manager.AddPost(userName, postDate, title, postText,
                    isMain, mainReferencePostID, ContainsImage);
            return PostID;
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
        public List<ImagePic> FindImagesByPostID(int postID)
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

        public int GetThreadCount(int mainPostReferenceID)
        {
            PostManager Manager = new PostManager();
            return Manager.GetThreadCount(mainPostReferenceID);
        }

        // A user has made an initial or changed their vote for a post
        public bool ModifyVotingSystem(int postID, string userName, int countValue)
        {
            PostManager Manager = new PostManager();
            return Manager.UpdatePostVote(postID, userName, countValue);
        }

        public int FindUserVoteForPost(int postID, string userName)
        {
            PostManager Manager = new PostManager();
            return Manager.GetUserVoteForPost(postID, userName);
        }
    }
}
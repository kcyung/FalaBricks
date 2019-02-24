using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using FalaBrick.LegoSystem.Domain;

namespace FalaBrick.LegoSystem.TechnicalServices
{
    public class PostManager
    {
        private string CONNECTION = "Data Source=DESKTOP-P612TBL; initial catalog=FalaBricksDB; integrated security=true";

        private SqlConnection Connection(string name)
        {
            SqlConnection newConnection = new SqlConnection();
            newConnection.ConnectionString = name;
            return newConnection;
        }

        private SqlCommand StoredProcedureCommand(string commandText, SqlConnection connection)
        {
            SqlCommand newCommand = new SqlCommand();
            newCommand.CommandType = CommandType.StoredProcedure;
            newCommand.CommandText = commandText;
            newCommand.Connection = connection;
            return newCommand;
        }

        private SqlParameter InputParameter(string parameterName, SqlDbType type)
        {
            SqlParameter newParameter = new SqlParameter();
            newParameter.ParameterName = parameterName;
            newParameter.Direction = ParameterDirection.Input;
            newParameter.SqlDbType = type;
            return newParameter;
        }

        /* STORED PROCEDURES */
        public bool AddPost(string userName, DateTime postDate, string title, string postText,
            bool isMain, int mainReferencePostID, bool ContainsImage)
        {
            SqlConnection connection = Connection(CONNECTION);
            SqlCommand AddPostCommand = StoredProcedureCommand("AddPost", connection);

            SqlParameter UserNameParameter = InputParameter("@UserName", SqlDbType.VarChar);
            UserNameParameter.Value = userName;

            SqlParameter PostDateParameter = InputParameter("@PostDate", SqlDbType.DateTime);
            UserNameParameter.Value = postDate;

            SqlParameter TitleParameter = InputParameter("@Title", SqlDbType.VarChar);
            TitleParameter.Value = title;

            SqlParameter PostTextParameter = InputParameter("@PostText", SqlDbType.VarChar);
            PostTextParameter.Value = postText;

            SqlParameter IsAMainPostParameter = InputParameter("@MainPost", SqlDbType.Bit);
            IsAMainPostParameter.Value = isMain ? 1 : 0;

            SqlParameter MainPostReferenceIDParameter = InputParameter("MainPostReference", SqlDbType.Int);
            MainPostReferenceIDParameter.Value = mainReferencePostID;

            SqlParameter ContainsImageParameter = InputParameter("ContainsImage", SqlDbType.Bit);
            ContainsImageParameter.Value = ContainsImage ? 1 : 0;

            AddPostCommand.Parameters.Add(UserNameParameter);
            AddPostCommand.Parameters.Add(PostDateParameter);
            AddPostCommand.Parameters.Add(TitleParameter);
            AddPostCommand.Parameters.Add(PostTextParameter);
            AddPostCommand.Parameters.Add(IsAMainPostParameter);
            AddPostCommand.Parameters.Add(MainPostReferenceIDParameter);
            AddPostCommand.Parameters.Add(ContainsImageParameter);

            connection.Open();

            int rowsAffected = AddPostCommand.ExecuteNonQuery();

            connection.Close();

            if (rowsAffected == 1)
                return true;
            return false;
        }

        public bool AddImage(int postID, string imagePath)
        {
            SqlConnection connection = Connection(CONNECTION);
            SqlCommand AddImageCommand = StoredProcedureCommand("AddImage", connection);

            SqlParameter PostIDParameter = InputParameter("@PostID", SqlDbType.Int);
            PostIDParameter.Value = postID;

            SqlParameter ImagePathParameter = InputParameter("@ImagePath", SqlDbType.VarChar);
            ImagePathParameter.Value = imagePath;

            AddImageCommand.Parameters.Add(PostIDParameter);
            AddImageCommand.Parameters.Add(ImagePathParameter);

            connection.Open();

            int rowsAffected = AddImageCommand.ExecuteNonQuery();

            connection.Close();

            if (rowsAffected == 1)
                return true;
            return false;
        }

        public List<Post> GetMainPost()
        {
            SqlConnection connection = Connection(CONNECTION);
            SqlCommand GetMainPostCommand = StoredProcedureCommand("GetMainPosts", connection);

            SqlDataReader reader;
            List<Post> MainPostList = new List<Post>();
            int postID = 0;
            string UserName = null;
            DateTime PostDate;
            string Title = null;
            string PostText = null;
            int UpCount = 0;
            int DownCount = 0;
            bool IsAMainPost = false;
            int MainPostReferenceID = 0;
            bool ContainsImage = false;

            connection.Open();

            reader = GetMainPostCommand.ExecuteReader();

            while (reader.Read())
            {
                postID = reader.GetInt32(reader.GetOrdinal("postID"));
                UserName = reader["UserName"].ToString();
                PostDate = reader.GetDateTime(reader.GetOrdinal("PostDate"));
                Title = reader["Title"].ToString();
                PostText = reader["PostText"].ToString();
                UpCount = reader.GetInt32(reader.GetOrdinal("UpCount"));
                DownCount = reader.GetInt32(reader.GetOrdinal("DownCount"));
                IsAMainPost = reader.GetBoolean(reader.GetOrdinal("MainPost"));
                MainPostReferenceID = reader.GetInt32(reader.GetOrdinal("MainPostReference"));
                ContainsImage = reader.GetBoolean(reader.GetOrdinal("ContainsImage"));

                Post newPost = new Post(postID, UserName, PostDate, Title, PostText, UpCount,
                    DownCount, IsAMainPost, MainPostReferenceID, ContainsImage);
                MainPostList.Add(newPost);
            }
            connection.Close();

            return MainPostList;
        }

        public List<Post> GetMainPostByPage(int pageNumber)
        {
            SqlConnection connection = Connection(CONNECTION);
            SqlCommand GetMainPostByPageCommand = StoredProcedureCommand("GetMainPostByPage", connection);

            SqlParameter PageParameter = InputParameter("@Page", SqlDbType.VarChar);
            PageParameter.Value = pageNumber;

            GetMainPostByPageCommand.Parameters.Add(PageParameter);

            SqlDataReader reader;
            List<Post> MainPostList = new List<Post>();
            int postID = 0;
            string UserName = null;
            DateTime PostDate;
            string Title = null;
            string PostText = null;
            int UpCount = 0;
            int DownCount = 0;
            bool IsAMainPost = false;
            int MainPostReferenceID = 0;
            bool ContainsImage = false;

            connection.Open();

            reader = GetMainPostByPageCommand.ExecuteReader();

            while (reader.Read())
            {
                postID = reader.GetInt32(reader.GetOrdinal("postID"));
                UserName = reader["UserName"].ToString();
                PostDate = reader.GetDateTime(reader.GetOrdinal("PostDate"));
                Title = reader["Title"].ToString();
                PostText = reader["PostText"].ToString();
                UpCount = reader.GetInt32(reader.GetOrdinal("UpCount"));
                DownCount = reader.GetInt32(reader.GetOrdinal("DownCount"));
                IsAMainPost = reader.GetBoolean(reader.GetOrdinal("MainPost"));
                MainPostReferenceID = reader.GetInt32(reader.GetOrdinal("MainPostReference"));
                ContainsImage = reader.GetBoolean(reader.GetOrdinal("ContainsImage"));

                Post newPost = new Post(postID, UserName, PostDate, Title, PostText, UpCount,
                    DownCount, IsAMainPost, MainPostReferenceID, ContainsImage);
                MainPostList.Add(newPost);
            }
            connection.Close();

            return MainPostList;
        }

        public List<Image> GetImagesByPostID(int postID)
        {
            SqlConnection connection = Connection(CONNECTION);
            SqlCommand GetImagesByPostIDCommand = StoredProcedureCommand("GetImagesByPostID", connection);

            SqlParameter PostIDParameter = InputParameter("@PostID", SqlDbType.Int);
            PostIDParameter.Value = postID;

            GetImagesByPostIDCommand.Parameters.Add(PostIDParameter);

            SqlDataReader reader;
            List<Image> ImagesInPost = new List<Image>();
            int imageID = 0;
            int postIDForImage = 0;
            string imagePath = null;

            connection.Open();

            reader = GetImagesByPostIDCommand.ExecuteReader();

            while (reader.Read())
            {
                imageID = reader.GetInt32(reader.GetOrdinal("ImageID"));
                postIDForImage = reader.GetInt32(reader.GetOrdinal("PostID"));
                imagePath = reader["ImagePath"].ToString();

                ImagesInPost.Add(new Image(imageID, postIDForImage, imagePath));
            }

            connection.Close();

            return ImagesInPost;
        }
    }
}
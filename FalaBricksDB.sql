CREATE DATABASE FalaBricksDB

USE FalaBricksDB

CREATE TABLE Post
(
	PostID				INT		IDENTITY(1,1)	PRIMARY KEY,
	UserName			VARCHAR(25)	NOT NULL,
	PostDate			DATETIME	NOT NULL,
	Title				VARCHAR(100)	NOT NULL,
	PostText			VARCHAR(5000),
	UpCount				INT	DEFAULT 0,
	DownCount			INT	DEFAULT 0,
	MainPost			BIT NOT NULL,			-- 0 is a thread post, 1 is a post for the main page
	MainPostReference	INT	FOREIGN KEY REFERENCES Post(PostID), -- Used for thread posts to pull from same main forum idea
	ContainsImages		BIT	NOT NULL			-- 0 is False, 1 is True		
)

CREATE TABLE Images
(
	ImageID		INT	IDENTITY(1,1) PRIMARY KEY,
	PostID		INT	FOREIGN KEY REFERENCES Post(PostID) NOT NULL,
	ImagePath	VARCHAR(500) NOT NULL
)

GO
-- Add a new post to the database
CREATE PROCEDURE AddPost
(
	@UserName	VARCHAR(25),
	@PostDate	DATETIME,
	@Title		VARCHAR(100),
	@PostText	VARCHAR(5000) NULL,
	@MainPost	BIT,  -- 0 Is a Thread Post, 1 is a Main Post
	@MainPostReference INT NULL,
	@ContainsImage		BIT,
	@PostID as int output
)
AS
	IF @UserName IS NULL
		RAISERROR('StoredProcedure - AddPost - Missing Parameter: @UserName', 16, 1)
	ELSE IF @PostDate IS NULL
		RAISERROR('StoredProcedure - AddPost - Missing Parameter: @PostDate', 16, 1)
	ELSE IF @Title IS NULL
		RAISERROR('StoredProcedure - AddPost - Missing Parameter: @Title', 16, 1)
	ELSE IF @MainPost IS NULL
		RAISERROR('StoredProcedure - AddPost - Missing Parameter: @MainPost', 16, 1)
	ELSE IF @ContainsImage IS NULL
		RAISERROR('StoredProcedure - AddPost - Missing Parameter: @ContainsImage', 16, 1)
	ELSE
		BEGIN
			DECLARE @ReturnCode INT
			SET		@ReturnCode = 1
			
			IF @MainPost = 0  -- This is a thread post
				INSERT INTO Post (UserName, PostDate, Title, PostText, UpCount, DownCount, MainPost, MainPostReference, ContainsImages) VALUES 
								 (@UserName, @PostDate, @Title, @PostText, 0, 0, @MainPost, @MainPostReference, @ContainsImage)
			ELSE -- A initial main post
				BEGIN 
					INSERT INTO Post (UserName, PostDate, Title, PostText, UpCount, DownCount, MainPost, ContainsImages) VALUES 
								 (@UserName, @PostDate, @Title, @PostText, 0, 0, @MainPost, @ContainsImage)
					Set @PostID = SCOPE_IDENTITY()
					UPDATE POST SET MainPostReference = (@@IDENTITY) WHERE PostID = @@IDENTITY
				END

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE 
				RAISERROR('StoredProcedure - AddPost - INSERT Error', 16, 1)
		END
	RETURN @ReturnCode
GO				

-- Save the Image path into the table and associate it with a postID
CREATE PROCEDURE AddImage
(
	@PostID		INT,
	@ImagePath	VARCHAR(500)
)
AS
	IF @PostID IS NULL
		RAISERROR('StoredProcedure - AddImage - Missing Parameter: @PostID', 16, 1)
	ELSE IF @ImagePath IS NULL
		RAISERROR('StoredProcedure - AddImage - Missing Parameter: @ImagePath', 16, 1)
	ELSE
		BEGIN
			DECLARE @ReturnCode INT
			SET		@ReturnCode = 1
			
			INSERT INTO Images (PostID, ImagePath) VALUES (@PostID, @ImagePath)
		
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE 
				RAISERROR('StoredProcedure - AddImage - INSERT Error', 16, 1)
		END
	RETURN @ReturnCode
GO				


-- Returns all main posts order by the most recent comment in the thread
CREATE PROCEDURE GetMainPosts 
AS
	DECLARE @ReturnCode INT
	SET		@ReturnCode = 1

	-- Get all main posts order by the most recent comment in the main post thread
	SELECT * FROM Post A INNER JOIN (SELECT MainPostReference, MAX(PostDate) AS 'PostDate' FROM Post GROUP BY MainPostReference) B
	ON A.MainPostReference = B.MainPostReference
	WHERE MainPost = 1
	ORDER BY B.PostDate DESC 

	IF @@ERROR = 0
		SET @ReturnCode = 0
	ELSE
		RAISERROR('StoredProcedure - GetMainPosts - SELECT Error', 16, 1)
	
	RETURN @ReturnCode
GO

-- Get the thread count for each main post (for displaying in the main post forum)
CREATE PROCEDURE GetThreadCount
(
	@MainPostReference	INT
)
AS
	IF @MainPostReference IS NULL
		RAISERROR('StoredProcedure - GetThreadCount - Missing Required Parameter: @MainPostReference', 16, 1)
	ELSE
		BEGIN
			DECLARE @ReturnCode INT
			SET		@ReturnCode = 1
			
			SELECT MainPostReference, COUNT(MainPostReference) AS 'ThreadCount' 
			FROM Post 
			WHERE MainPostReference = @MainPostReference
			GROUP BY MainPostReference

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('StoredProcedure - GetThreadCount - SELECT Error', 16, 1)
		END
	RETURN @ReturnCode
GO

-- Gets all post for a given page number // Initially set for 10 posts per page
CREATE PROCEDURE GetMainPostByPage
(
	@Page	INT
)
AS
	IF @Page IS NULL
		RAISERROR('StoredProcedure - GetMainPostByPage - Missing Required Parameter: @Page', 16, 1)
	ELSE
		BEGIN
			DECLARE @ReturnCode INT
			SET		@ReturnCode = 1

			-- Where MainPost = 1 Clause -> indicates it's a main post and not a threaded post
			SELECT * FROM Post A INNER JOIN (SELECT MainPostReference, MAX(PostDate) AS 'PostDate' FROM Post GROUP BY MainPostReference) B
			ON A.MainPostReference = B.MainPostReference
			WHERE MainPost = 1
			ORDER BY B.PostDate DESC 
			OFFSET (@Page - 1)*10 ROWS
			FETCH NEXT 10 ROWS ONLY

			IF @@ERROR =  0
				SET @ReturnCode = 0
			ELSE 
				RAISERROR('StoredProcedure - GetMainPostByPage: SELECT ERROR', 16, 1)
		END
	RETURN @ReturnCode
GO

-- Gets all the images associated with one post
CREATE PROCEDURE GetImagesByPostID
(
	@PostID		INT
)
AS
	IF @PostID IS NULL
		RAISERROR('StoredProcedure - GetImagesByPostID - Missing Required Parameter: @PostID', 16, 1)
	ELSE
		BEGIN
			DECLARE @ReturnCode INT
			SET		@ReturnCode = 1

			SELECT * FROM Images WHERE PostID = @PostID

			IF @@ERROR =  0
				SET @ReturnCode = 0
			ELSE 
				RAISERROR('StoredProcedure - GetImagesByPostID: SELECT ERROR', 16, 1)
		END
	RETURN @ReturnCode
GO

CREATE PROCEDURE GetThreadByMainPostReference
(
	@MainPostReference	INT
)
AS
	IF @MainPostReference IS NULL
		RAISERROR('StoredProcedure - GetThreadByMainPostReference - Missing Required Parameter: @MainPostReference', 16, 1)
	ELSE
		BEGIN
			DECLARE	@ReturnCode	INT
			SET		@ReturnCode = 0

			SELECT * FROM Post WHERE MainPostReference = @MainPostReference
			ORDER BY PostID ASC

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('StoredProcedure - GetThreadByMainPostReference: SELECT ERROR', 16, 1)
		END
	RETURN @ReturnCode
GO

CREATE PROCEDURE UpdateUpCounter
(
	@PostID		INT,
	@UpCount	INT
)
AS
	IF @PostID IS NULL
		RAISERROR('StoredProcedure - UpdateUpCounter - Missing Required Parameter: @PostID', 16, 1)
	ELSE IF @UpCount IS NULL
		RAISERROR('StoredProcedure - UpdateUpCounter - Missing Required Parameter: @UpCount', 16, 1)
	ELSE
		BEGIN
			DECLARE	@ReturnCode	INT
			SET		@ReturnCode = 0

			UPDATE Post
			SET	UpCount = @UpCount
			WHERE PostID = @PostID

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('StoredProcedure - UpdateUpCounter: INSERT ERROR', 16, 1)
		END
	RETURN @ReturnCode
GO

CREATE PROCEDURE UpdateDownCounter
(
	@PostID		INT,
	@DownCount	INT
)
AS
	IF @PostID IS NULL
		RAISERROR('StoredProcedure - UpdateDownCounter - Missing Required Parameter: @PostID', 16, 1)
	ELSE IF @DownCount IS NULL
		RAISERROR('StoredProcedure - UpdateDownCounter - Missing Required Parameter: @DownCount', 16, 1)
	ELSE
		BEGIN
			DECLARE	@ReturnCode	INT
			SET		@ReturnCode = 0

			UPDATE Post
			SET	DownCount = @DownCount
			WHERE PostID = @PostID

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('StoredProcedure - UpdateDownCounter: INSERT ERROR', 16, 1)
		END
	RETURN @ReturnCode
GO	
SELECT * FROM Images
SELECT * FROM Post
EXECUTE AddPost 'kyung4', '2019-03-10 11:00:00', 'My first lego idea', 'What do you think of my idea?', 1, null, 1  
EXECUTE AddImage 1, '~/Images/PostID1/Concept.jpg'

EXECUTE AddPost 'kyung4', '2019-03-10 11:01:00', 'Bonsai Tree', 'Building this', 1, null, 1
EXECUTE AddImage 2, '~/Images/PostID2/Bonsai Tree.jpg'

EXECUTE AddPost 'kyung4', '2019-03-10 11:02', 'Good idea', 'Can a builder please make it?', 0, 1, 0

EXECUTE AddPost 'Bob', '2019-03-10 11:03', 'I''m designing this', 'Tell me what you think', 1, null, 1
EXECUTE AddImage 4, '~/Images/PostID4/Residential.png'

EXECUTE AddPost 'Alice', '2019-03-10 11:04', 'Would anyone ever want to buy this?', 'Yes?/No?', 1, null, 1
EXECUTE AddImage 5, '~/Images/PostID5/ResidentialHouse2.png'

EXECUTE AddPost 'Mary', '2019-03-10 11:05', 'My dream home', 'I''ll pay you millions to build it', 1, null, 1
EXECUTE AddImage 6, '~/Images/PostID6/ResidentialHouse3.png'


SELECT * FROM Post
SELECT * FROM Images

-- Get all the images for one post with provided PostID
SELECT * FROM Post INNER JOIN Images ON Post.PostID = Images.PostID WHERE Post.PostID = 1

-- Get all the posts for one thread (Missing the Main Post Itself)
SELECT * FROM Post WHERE MainPostReference = 1 ORDER BY PostID

EXECUTE GetThreadCount 1

SELECT * FROM POst

/** SAMPLE RUN 

A) GUY CREATES POST
B) UPLOADS IMAGES
C) WRITES TEXT
D) SUBMITS BUTTON CLICK

-- ON THE BUTTON CLICK
1) ADO THE STUFF INTO THE POST TABLE
2) GET THE POSTID FROM THE LAST STATEMENT
3) ITERATE THROUGH EACH IMAGE AND UPLOAD IT INTO THE IMAGES TABLE
*/
SELECT * FROM POST
SELECT * FROM Images


USE [master]
GO
/****** Object:  Database [ServerPaginationDB]    Script Date: 04-01-2024 18:09:44 ******/
CREATE DATABASE [ServerPaginationDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ServerPaginationDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ServerPaginationDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ServerPaginationDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ServerPaginationDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ServerPaginationDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ServerPaginationDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ServerPaginationDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ServerPaginationDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ServerPaginationDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ServerPaginationDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ServerPaginationDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ServerPaginationDB] SET  MULTI_USER 
GO
ALTER DATABASE [ServerPaginationDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ServerPaginationDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ServerPaginationDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ServerPaginationDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ServerPaginationDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ServerPaginationDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ServerPaginationDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [ServerPaginationDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ServerPaginationDB]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04-01-2024 18:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](max) NULL,
	[ProfileImage] [varchar](max) NULL,
	[FirstName] [varchar](max) NULL,
	[LastName] [varchar](max) NULL,
	[Gender] [varchar](max) NULL,
	[EmailAddress] [varchar](max) NULL,
	[MobileNo] [varchar](max) NULL,
	[Password] [varchar](max) NULL,
	[HashValue] [varchar](max) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[DeletedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_proc_AddUser]    Script Date: 04-01-2024 18:09:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_proc_AddUser]
(
@Role VARCHAR(255),
@ProfileImage VARCHAR(255),
@FirstName VARCHAR(255),
@LastName VARCHAR(255),
@Gender VARCHAR(255),
@Email VARCHAR(255),
@MobileNo VARCHAR(255),
@Password VARCHAR(255),
@IsActive BIT,
@IsDeleted BIT,	
@CreatedDate DATETIME
)
AS
BEGIN
	INSERT INTO Users(Role,ProfileImage, FirstName, LastName, Gender, EmailAddress, MobileNo,Password, IsActive, IsDeleted, CreatedDate)
	VALUES(@Role, @ProfileImage, @FirstName, @LastName, @Gender, @Email, @MobileNo, @Password, @IsActive, @IsDeleted, @CreatedDate)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_proc_CheckStatus]    Script Date: 04-01-2024 18:09:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATe pROCEDURE [dbo].[sp_proc_CheckStatus]
(
@UserId INT
)
AS
BEGIN
	select IsActive from Users where UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_proc_DeleteUser]    Script Date: 04-01-2024 18:09:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_proc_DeleteUser]
(
@UserId INT
)
AS
BEGIN
	UPDATE Users SET IsDeleted  = 1 WHERE UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_proc_EditUser]    Script Date: 04-01-2024 18:09:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_proc_EditUser]
(
@UserId INT,
@FirstName VARCHAR(255),
@LastName VARCHAR(255),
@Gender VARCHAR(255),
@Email VARCHAR(255),
@MobileNo VARCHAR(255),
@UpdatedDate DATETIME
)
AS
BEGIN
	Update Users set FirstName = @FirstName, LastName = @LastName, Gender = @Gender, EmailAddress = @Email, MobileNo =@MobileNo,UpdatedDate = @UpdatedDate where UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_proc_GetUser]    Script Date: 04-01-2024 18:09:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATe pROCEDURE [dbo].[sp_proc_GetUser]
(
@UserId INT
)
AS
BEGIN
select * from Users where UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_proc_PaginatedUsers]    Script Date: 04-01-2024 18:09:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_proc_PaginatedUsers]
    @PageSize INT,
    @CurrentPage INT,
    @SearchQuery NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TotalRecords INT, @TotalPages INT, @NextPage INT, @PrevPage INT, @StartIndex INT, @EndIndex INT;

    -- Calculate the offset to skip records for the current page
    DECLARE @Offset INT = (@CurrentPage - 1) * @PageSize;

    -- Get the total number of records (optionally filtered by search query)
    SELECT @TotalRecords = COUNT(*)
    FROM Users
    WHERE Isdeleted = 0 AND 
			(@SearchQuery IS NULL OR
             FirstName LIKE '%' + @SearchQuery + '%' OR
             LastName LIKE '%' + @SearchQuery + '%' OR
             EmailAddress LIKE '%' + @SearchQuery + '%' OR
			 Gender LIKE '%' + @SearchQuery + '%'
			);
			
	-- Calculate the total number of pages
    SET @TotalPages = CEILING(@TotalRecords * 1.0 / @PageSize);

    -- Calculate the next and previous page numbers
    SET @NextPage = CASE WHEN @CurrentPage < @TotalPages THEN @CurrentPage + 1 ELSE @TotalPages END;
    SET @PrevPage = CASE WHEN @CurrentPage > 1 THEN @CurrentPage - 1 ELSE 1 END;

    -- Calculate the start and end indices for the current page
    SET @StartIndex = CASE WHEN @TotalRecords = 0 THEN 0 ELSE @Offset + 1 END;
    SET @EndIndex = CASE
        WHEN @TotalRecords = 0 THEN 0
        WHEN @CurrentPage = @TotalPages THEN @TotalRecords
        ELSE @Offset + @PageSize
    END;

    -- Fetch the paginated user data
    SELECT *
    FROM Users
    WHERE Isdeleted = 0 AND 
			(@SearchQuery IS NULL OR
             FirstName LIKE '%' + @SearchQuery + '%' OR
             LastName LIKE '%' + @SearchQuery + '%' OR
             EmailAddress LIKE '%' + @SearchQuery + '%' OR
			 Gender LIKE '%' + @SearchQuery + '%'
			)
    ORDER BY UserID
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Return pagination metadata
    SELECT
        @PageSize AS PageSize,
        @CurrentPage AS CurrentPage,
        @NextPage AS NextPage,
        @PrevPage AS PrevPage,
        @TotalPages AS TotalPage,
        @StartIndex AS StartIndex,
        @EndIndex AS EndIndex,
        @TotalRecords AS TotalRecord,
        @SearchQuery AS SearchQuery;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_proc_UserActive]    Script Date: 04-01-2024 18:09:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATe pROCEDURE [dbo].[sp_proc_UserActive]
(
@UserId INT
)
AS
BEGIN
	Update Users SEt IsActive = 1 WHERE UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_proc_UserList]    Script Date: 04-01-2024 18:09:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_proc_UserList]
	@PageNumber INT,
    @PageSize INT,
    @SearchQuery NVARCHAR(255),
	@TotalCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

	-- Calculate the total count
	SELECT 	@TotalCount = COUNT(*) 	FROM Users    WHERE IsDeleted= 0 AND (@SearchQuery IS NULL OR           FirstName LIKE '%' + @SearchQuery + '%' OR           LastName LIKE '%' + @SearchQuery + '%' OR           EmailAddress LIKE '%' + @SearchQuery + '%' OR		   Gender LIKE '%' + @SearchQuery + '%' OR           MobileNo LIKE '%' + @SearchQuery + '%')

	-- Get the paginated result set
    SELECT
        *
    FROM (
        SELECT
            *,
            ROW_NUMBER() OVER (ORDER BY UserId Desc) AS RowNum
        FROM Users
        WHERE Isdeleted = 0 AND (@SearchQuery IS NULL OR
               FirstName LIKE '%' + @SearchQuery + '%' OR
               LastName LIKE '%' + @SearchQuery + '%' OR
               EmailAddress LIKE '%' + @SearchQuery + '%' OR
			   Gender LIKE '%' + @SearchQuery + '%' OR
			   MobileNo LIKE '%' + @SearchQuery + '%'
			   )
    ) AS Temp
    WHERE RowNum BETWEEN ((@PageNumber - 1) * @PageSize) + 1 AND @PageNumber * @PageSize ;

	SELECT @TotalCount AS TotalCount;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_proc_UserPagination]    Script Date: 04-01-2024 18:09:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_proc_UserPagination]
    @CurrentPage INT,
    @PageSize INT,
    @SearchQuery NVARCHAR(255),
	@TotalCount int OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Calculate the total count
    SELECT @TotalCount = COUNT(*)
 FROM Users
    WHERE (@SearchQuery IS NULL OR
           FirstName LIKE '%' + @SearchQuery + '%' OR
           LastName LIKE '%' + @SearchQuery + '%' OR
           EmailAddress LIKE '%' + @SearchQuery + '%' OR
           MobileNo LIKE '%' + @SearchQuery + '%') AND  IsDeleted = 'false';

    -- Get the paginated result set
    SELECT * FROM (
					SELECT *,ROW_NUMBER() OVER (ORDER BY CreatedDate DESC) AS RowNum
					FROM Users
					WHERE (@SearchQuery IS NULL OR
					FirstName LIKE '%' + @SearchQuery + '%' OR
					LastName LIKE '%' + @SearchQuery + '%' OR
					EmailAddress LIKE '%' + @SearchQuery + '%') AND IsDeleted = 'false'
				 )AS Temp
				 WHERE RowNum BETWEEN ((@CurrentPage - 1) * @PageSize) + 1 AND @CurrentPage * @PageSize;
    -- Return the total count as a result set
    SELECT @TotalCount AS TotalCount;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_proc_UserUnactive]    Script Date: 04-01-2024 18:09:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATe pROCEDURE [dbo].[sp_proc_UserUnactive]
(
@UserId INT
)
AS
BEGIN
	Update Users SEt IsActive = 0 WHERE UserId = @UserId
END
GO
USE [master]
GO
ALTER DATABASE [ServerPaginationDB] SET  READ_WRITE 
GO

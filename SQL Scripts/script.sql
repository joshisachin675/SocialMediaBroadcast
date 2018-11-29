/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2012 (11.0.3128)
    Source Database Engine Edition : Microsoft SQL Server Standard Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [smb]    Script Date: 11/29/2018 12:26:06 PM ******/
CREATE DATABASE [smb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'smb', FILENAME = N'C:\Data\socialmediabroadcast.mdf' , SIZE = 13312KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'smb_log', FILENAME = N'C:\Data\socialmediabroadcast_log.ldf' , SIZE = 6272KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [smb] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [smb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [smb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [smb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [smb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [smb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [smb] SET ARITHABORT OFF 
GO
ALTER DATABASE [smb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [smb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [smb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [smb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [smb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [smb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [smb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [smb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [smb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [smb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [smb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [smb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [smb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [smb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [smb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [smb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [smb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [smb] SET RECOVERY FULL 
GO
ALTER DATABASE [smb] SET  MULTI_USER 
GO
ALTER DATABASE [smb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [smb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [smb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [smb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [smb]
GO
/****** Object:  User [smbadmin]    Script Date: 11/29/2018 12:26:09 PM ******/
CREATE USER [smbadmin] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [crworkmaster]    Script Date: 11/29/2018 12:26:10 PM ******/
CREATE USER [crworkmaster] FOR LOGIN [crworkmaster] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [adminSMB]    Script Date: 11/29/2018 12:26:10 PM ******/
CREATE USER [adminSMB] FOR LOGIN [adminSMB] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [crworkmaster]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [crworkmaster]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [crworkmaster]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [crworkmaster]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [crworkmaster]
GO
ALTER ROLE [db_datareader] ADD MEMBER [crworkmaster]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [crworkmaster]
GO
ALTER ROLE [db_owner] ADD MEMBER [adminSMB]
GO
/****** Object:  Schema [smbadmin]    Script Date: 11/29/2018 12:26:12 PM ******/
CREATE SCHEMA [smbadmin]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryId] [int] NOT NULL,
	[ISO] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[NiceName] [nvarchar](100) NOT NULL,
	[ISO3] [nvarchar](100) NULL,
	[NumCode] [int] NULL,
	[PhoneCode] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](100) NULL,
	[TableRowID] [int] NULL,
	[ErrorMsg] [nvarchar](max) NULL,
	[ErrorPath] [nvarchar](max) NULL,
	[ErrorDate] [datetime] NULL,
	[ProcessName] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogTypes]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogTypes](
	[Key] [varchar](200) NULL,
	[Name] [varchar](200) NULL,
	[IdName] [varchar](200) NULL,
	[ClientIdName] [varchar](200) NULL,
	[ParentIdName] [varchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProvinceState ]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProvinceState ](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NOT NULL,
	[StateName] [varchar](50) NULL,
	[ShortName] [varchar](50) NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rcaPhotos]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rcaPhotos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_mls] [int] NOT NULL,
	[mlscode] [nvarchar](25) NOT NULL,
	[MainPhoto] [varchar](250) NULL,
	[Picture1] [varchar](250) NULL,
	[Picture2] [varchar](250) NULL,
	[Picture3] [varchar](250) NULL,
	[Picture4] [varchar](250) NULL,
	[Picture5] [varchar](250) NULL,
	[Picture6] [varchar](250) NULL,
	[Picture7] [varchar](250) NULL,
	[Picture8] [varchar](250) NULL,
	[Picture9] [varchar](250) NULL,
	[Picture10] [varchar](250) NULL,
	[Picture11] [varchar](250) NULL,
	[Picture12] [varchar](250) NULL,
	[Picture13] [varchar](250) NULL,
	[Picture14] [varchar](250) NULL,
	[Picture15] [varchar](250) NULL,
	[Picture16] [varchar](250) NULL,
	[Picture17] [varchar](250) NULL,
	[Picture18] [varchar](250) NULL,
	[Picture19] [varchar](250) NULL,
	[Picture20] [varchar](250) NULL,
	[Picture21] [varchar](250) NULL,
	[Picture22] [varchar](250) NULL,
	[Picture23] [varchar](250) NULL,
	[Picture24] [varchar](250) NULL,
	[Picture25] [varchar](250) NULL,
	[Picture26] [varchar](250) NULL,
	[Picture27] [varchar](250) NULL,
	[Picture28] [varchar](250) NULL,
	[Picture29] [varchar](250) NULL,
	[Picture30] [varchar](250) NULL,
	[DateEntered] [datetime] NOT NULL,
	[Complete] [bit] NOT NULL,
 CONSTRAINT [PK_rcaPhotos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rcaProps]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rcaProps](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_MLS] [int] NOT NULL,
	[id_Partner] [int] NOT NULL,
	[rcaid] [int] NOT NULL,
	[id_Partner2] [int] NOT NULL,
	[rcaid2] [int] NOT NULL,
	[MLSCode] [nvarchar](50) NULL,
	[FullAddress] [nvarchar](250) NULL,
	[Address] [nvarchar](50) NULL,
	[Street] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](7) NULL,
	[Price] [float] NULL,
	[CondoFees] [float] NULL,
	[Taxes] [float] NULL,
	[Active] [bit] NOT NULL,
	[DateEntered] [smalldatetime] NULL,
	[LastUpdate] [smalldatetime] NULL,
	[bToProcess] [bit] NOT NULL,
 CONSTRAINT [PK_rcaProps] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rcaRealtors]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rcaRealtors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Partner] [int] NOT NULL,
	[rcaid] [int] NOT NULL,
	[id_type] [int] NOT NULL,
	[disabled] [bit] NOT NULL,
	[lastUpdate] [datetime] NULL,
	[failed] [int] NULL,
 CONSTRAINT [PK_rcaRealtors] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smActivityLog]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smActivityLog](
	[ActivityId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[IpAddress] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
	[AreaAccessed] [varchar](250) NULL,
	[Event] [varchar](250) NULL,
	[Message] [varchar](250) NULL,
	[TimeStamp] [datetime] NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_ActivityLog] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smAutoPreference]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smAutoPreference](
	[AutoPreferenceId] [int] IDENTITY(1,1) NOT NULL,
	[SubindustryID] [int] NULL,
	[SubindustryName] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserId] [int] NULL,
	[Day] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AutoPreferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smAutoselectedLandPage]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smAutoselectedLandPage](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[LandingPageID] [int] NULL,
	[UserId] [int] NULL,
	[IsActive] [bit] NULL,
	[Isdeleted] [bit] NULL,
	[DayID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smContentLibrary]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smContentLibrary](
	[ContentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SocialMedia] [varchar](20) NULL,
	[Title] [varchar](max) NULL,
	[Heading] [varchar](max) NULL,
	[Description] [varchar](max) NULL,
	[Url] [varchar](max) NULL,
	[IsFacebook] [bit] NOT NULL,
	[IsLinkedIn] [bit] NOT NULL,
	[IsTwitter] [bit] NOT NULL,
	[TextDescription] [varchar](max) NULL,
	[ContentSource] [varchar](250) NULL,
	[ImageUrl] [varchar](max) NULL,
	[Tags] [varchar](max) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CategoryId] [int] NULL,
	[CategoryName] [varchar](250) NULL,
	[SubIndustryName] [varchar](250) NULL,
	[SubIndustryId] [int] NULL,
	[IsIgnored] [nchar](10) NULL,
	[GroupId] [varchar](25) NULL,
	[Archive] [bit] NOT NULL,
	[OriginalTitle] [varchar](max) NULL,
 CONSTRAINT [PK_smContentLibrary] PRIMARY KEY CLUSTERED 
(
	[ContentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smContentstatus]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smContentstatus](
	[ConId] [int] IDENTITY(1,1) NOT NULL,
	[ContentId] [int] NULL,
	[UserId] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_smContentstatus] PRIMARY KEY CLUSTERED 
(
	[ConId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smDays]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smDays](
	[DayId] [int] IDENTITY(1,1) NOT NULL,
	[DayName] [varchar](50) NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smDomainExcept]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smDomainExcept](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DomainName] [varchar](150) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smFacebookDefaultPreference]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smFacebookDefaultPreference](
	[PreferenceId] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[PageId] [bigint] NULL,
	[Type] [int] NULL,
	[CreatedDate] [datetime2](7) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[isActive] [bit] NULL,
	[isDeleted] [bit] NULL,
 CONSTRAINT [PK__smFacebo__E228496FC93121C1] PRIMARY KEY CLUSTERED 
(
	[PreferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smFacebookPageDetail]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smFacebookPageDetail](
	[pId] [int] IDENTITY(1,1) NOT NULL,
	[PageAccessToken] [nvarchar](max) NULL,
	[PageId] [bigint] NULL,
	[PageName] [nvarchar](100) NULL,
	[UserId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UserFaceBookID] [bigint] NULL,
	[category] [nvarchar](150) NULL,
 CONSTRAINT [PK__smFacebo__DD36D56293ED045C] PRIMARY KEY CLUSTERED 
(
	[pId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smFacebookPostDetails]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smFacebookPostDetails](
	[FId] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NULL,
	[UserId] [int] NULL,
	[FBId] [varchar](max) NULL,
	[FBPostId] [varchar](max) NULL,
	[Type] [varchar](20) NULL,
	[AddedDate] [datetime] NULL,
 CONSTRAINT [PK_FacebookPostDetails] PRIMARY KEY CLUSTERED 
(
	[FId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smHomeValue]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smHomeValue](
	[HomeValueId] [int] IDENTITY(1,1) NOT NULL,
	[Address] [varchar](max) NULL,
	[StreetAddress] [varchar](255) NULL,
	[Unit] [varchar](50) NULL,
	[City] [varchar](100) NULL,
	[Province] [varchar](100) NULL,
	[PostalCode] [varchar](50) NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[EmailAddress] [varchar](50) NULL,
	[PhoneNumber] [varchar](15) NULL,
	[TimePeriodId] [int] NOT NULL,
	[IsCompleted] [bit] NOT NULL,
	[PostId] [int] NOT NULL,
	[Notify] [bit] NOT NULL,
	[DateSubmit] [datetime] NULL,
	[userID] [int] NULL,
	[IPAddress] [nvarchar](200) NULL,
 CONSTRAINT [PK_smHomeValue] PRIMARY KEY CLUSTERED 
(
	[HomeValueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smIndustry]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smIndustry](
	[IndustryId] [int] IDENTITY(1,1) NOT NULL,
	[IndustryName] [varchar](250) NULL,
	[IndustryShortName] [varchar](100) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[IndustryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smLandingPagesCategoryForLeads]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smLandingPagesCategoryForLeads](
	[LeadCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[IndustryId] [int] NOT NULL,
	[Label] [varchar](120) NULL,
	[isActive] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smLandingPagesCollection]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smLandingPagesCollection](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_industry] [int] NULL,
	[LeadCategoryId] [int] NOT NULL,
	[label] [nvarchar](150) NULL,
	[url] [nvarchar](150) NULL,
	[sortorder] [int] NULL,
	[active] [bit] NULL,
 CONSTRAINT [PK__smLandin__3213E83FE8BF5D09] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smLandingPagesForUsers]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smLandingPagesForUsers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NULL,
	[id_landingpage] [int] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smLeadCategory]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smLeadCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](150) NOT NULL,
	[id_industry] [int] NOT NULL,
	[active] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smLeads]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smLeads](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[IndustryId] [int] NOT NULL,
	[LeadCategoryId] [int] NOT NULL,
	[PostId] [int] NOT NULL,
	[UniquePostId] [nvarchar](8) NULL,
	[Ref] [varchar](150) NULL,
	[FirstName] [varchar](75) NULL,
	[LastName] [varchar](75) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[Topic] [varchar](500) NULL,
	[Budget] [varchar](50) NULL,
	[HasMortgageArrangements] [varchar](50) NULL,
	[HasRealtor] [bit] NOT NULL,
	[HasTimeline] [varchar](50) NULL,
	[Comments] [varchar](1000) NULL,
	[DateEntered] [datetime] NOT NULL,
	[IPAddress] [varchar](15) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_smLeads] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smLinkedInPostDetails]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smLinkedInPostDetails](
	[LId] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NULL,
	[UserId] [int] NULL,
	[Type] [varchar](20) NULL,
	[LIinkedInId] [varchar](max) NULL,
	[LinkedInUrl] [varchar](max) NULL,
	[Partner_Id] [int] NULL,
	[AddedDate] [datetime] NULL,
 CONSTRAINT [PK_smLinkedInPostDetails] PRIMARY KEY CLUSTERED 
(
	[LId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smLoginAuthentication]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smLoginAuthentication](
	[AuthenticationId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ActivityTime] [datetime] NULL,
	[SessionTime] [datetime] NULL,
	[ExpireTime] [int] NULL,
	[TokenId] [uniqueidentifier] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_LoginAuthentication] PRIMARY KEY CLUSTERED 
(
	[AuthenticationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smPost]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smPost](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[SocialMediaProfileId] [int] NULL,
	[SocialMedia] [varchar](50) NULL,
	[Name] [varchar](max) NULL,
	[Caption] [varchar](max) NULL,
	[Description] [varchar](max) NULL,
	[EventId] [int] NULL,
	[Url] [varchar](max) NULL,
	[ImageUrl] [varchar](max) NULL,
	[IsPosted] [bit] NULL,
	[PostType] [int] NULL,
	[PostDate] [datetime] NULL,
	[LikesCount] [int] NULL,
	[CommentsCount] [int] NULL,
	[LikesNames] [varchar](max) NULL,
	[CommentsText] [varchar](max) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[ExpiryDate] [date] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[UniquePostId] [nvarchar](8) NULL,
	[ContentId] [int] NOT NULL,
	[ContentCreatedId] [int] NULL,
	[PostingStatus] [int] NOT NULL,
 CONSTRAINT [PK_smPost] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smPreference]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smPreference](
	[PreferenceId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Preference] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PreferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smProperties]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smProperties](
	[id_property] [int] NOT NULL,
	[mlscode] [varchar](50) NOT NULL,
	[Price] [float] NULL,
	[Address] [nvarchar](255) NULL,
	[LongDescription] [nvarchar](500) NULL,
	[VirtualTour] [nvarchar](255) NULL,
	[Sold] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smPublishingTime]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smPublishingTime](
	[PublishingTimeId] [int] IDENTITY(1,1) NOT NULL,
	[DayId] [int] NOT NULL,
	[TimeStampPosted] [time](7) NOT NULL,
	[Time] [varchar](50) NULL,
	[UserId] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smRssArtical]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smRssArtical](
	[RssID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[IsIgnored] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK__RssArtic__13C104DC02B3AB0C] PRIMARY KEY CLUSTERED 
(
	[RssID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smRssFeeds]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smRssFeeds](
	[FeedId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[FeedName] [varchar](250) NULL,
	[FeedUrl] [varchar](250) NULL,
	[IsActive] [bit] NULL,
	[Limit] [int] NULL,
	[UserType] [varchar](250) NULL,
	[IsApproved] [bit] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
	[DateProcess] [datetime] NULL,
 CONSTRAINT [PK_smRssFeeds] PRIMARY KEY CLUSTERED 
(
	[FeedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smScheduleEvents]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smScheduleEvents](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[ContentId] [int] NOT NULL,
	[UserId] [int] NULL,
	[Evnt_Id] [varchar](50) NULL,
	[Title] [varchar](max) NULL,
	[ScheduleTime] [datetime2](7) NULL,
	[LocalTime] [datetime2](7) NULL,
	[IsPosted] [bit] NULL,
	[IsFacebook] [bit] NULL,
	[IsLinkedIn] [bit] NULL,
	[IsTwitter] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime2](7) NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime2](7) NULL,
	[ContentCreatedId] [int] NULL,
 CONSTRAINT [PK_smScheduleEvents] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smSocialMedia]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smSocialMedia](
	[SocialMediaId] [int] IDENTITY(1,1) NOT NULL,
	[SocialMedia] [varchar](200) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_smSocialMedia] PRIMARY KEY CLUSTERED 
(
	[SocialMediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smSocialMediaProfile]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smSocialMediaProfile](
	[Fid] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Link] [varchar](200) NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Email] [varchar](200) NULL,
	[Gender] [varchar](20) NULL,
	[Photo] [varchar](500) NULL,
	[SocialMediaId] [varchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[Partner_Id] [int] NULL,
	[SocialMedia] [varchar](50) NULL,
	[AccessToken] [varchar](max) NULL,
	[TokenSecret] [varchar](max) NULL,
	[IsAccountActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
	[AccountActiveBy] [int] NULL,
	[AccountDeactiveBy] [int] NULL,
 CONSTRAINT [PK_SMB_FacebookProfile] PRIMARY KEY CLUSTERED 
(
	[Fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smSubIndustry]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smSubIndustry](
	[SubIndustryId] [int] IDENTITY(1,1) NOT NULL,
	[SubIndustryName] [varchar](250) NULL,
	[IndustryId] [int] NOT NULL,
	[IndustryName] [varchar](250) NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smTermsConditions]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smTermsConditions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TermsConditions] [varchar](max) NULL,
	[cratedDate] [datetime] NULL,
	[createdBy] [int] NULL,
	[isActive] [bit] NULL,
	[isDeleted] [bit] NULL,
	[id_Industry] [int] NULL,
	[labelandtittle] [nvarchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smTwitterPostDetails]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smTwitterPostDetails](
	[TId] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NULL,
	[UserId] [int] NULL,
	[Type] [nchar](10) NULL,
	[TwitterId] [varchar](max) NULL,
	[Partner_Id] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_smTwitterPostDetails] PRIMARY KEY CLUSTERED 
(
	[TId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smUserActiveSocialMedia]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smUserActiveSocialMedia](
	[SId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SocialMedia] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[Partner_Id] [int] NULL,
 CONSTRAINT [PK_smUserActiveSocialMedia] PRIMARY KEY CLUSTERED 
(
	[SId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smViews]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smViews](
	[UserId] [int] NOT NULL,
	[UniquePostId] [nvarchar](8) NOT NULL,
	[Postid] [int] NOT NULL,
	[DateEntered] [datetime] NOT NULL,
	[ViewId] [int] IDENTITY(1,1) NOT NULL,
	[IPAddress] [nvarchar](15) NOT NULL,
	[Ref] [varchar](120) NULL,
	[SessionID] [bigint] NULL,
 CONSTRAINT [PK_smViews] PRIMARY KEY CLUSTERED 
(
	[ViewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[smWatermark]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smWatermark](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[ImagePathLogo] [varchar](250) NULL,
	[ImageText] [varchar](250) NULL,
	[TextWidth] [int] NULL,
	[TextSiz] [int] NULL,
	[Textcolor] [varchar](50) NULL,
	[TextBg] [varchar](50) NULL,
	[Gravity] [varchar](50) NULL,
	[Fontfamily] [varchar](250) NULL,
	[Opacity] [float] NULL,
	[Margin] [int] NULL,
	[OutputWidth] [varchar](50) NULL,
	[OutputHeight] [varchar](50) NULL,
	[OutPutType] [varchar](50) NULL,
	[CreatedBy] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[UserID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[date] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Title] [varchar](50) NULL,
	[DisplayName] [varchar](250) NULL,
	[CompanyName] [varchar](250) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[Email] [varchar](100) NOT NULL,
	[LocalMarket] [varchar](200) NULL,
	[LocalArea] [varchar](200) NULL,
	[UserTypeId] [int] NULL,
	[AuthFacebookId] [varchar](100) NULL,
	[Active] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[IsSuperAdmin] [bit] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
	[Password] [varchar](max) NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[Photo] [varchar](100) NULL,
	[IndustryId] [int] NULL,
	[IndustryName] [varchar](250) NULL,
	[ShortName] [varchar](50) NULL,
	[AcceptTerms] [bit] NULL,
	[AcceptTermsDate] [timestamp] NULL,
	[AcceptTermsIP] [nvarchar](20) NULL,
	[Status] [nvarchar](250) NULL,
	[Country] [nvarchar](20) NULL,
	[ProvinceState] [nvarchar](20) NULL,
	[LastChangePasswordDate] [datetime] NULL,
	[FacebookProfile] [nvarchar](250) NULL,
	[TwitterProfile] [nvarchar](250) NULL,
	[LinkedInProfile] [nvarchar](250) NULL,
	[AdwordsScript] [nvarchar](500) NULL,
	[AnalyticsScript] [nvarchar](500) NULL,
	[RemarketingScript] [nvarchar](500) NULL,
	[ConversionScript] [nvarchar](500) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[UserTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[UserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Countries] ADD  CONSTRAINT [DF_Countries_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[rcaPhotos] ADD  CONSTRAINT [DF_rcaPhotos_id_mls]  DEFAULT ((0)) FOR [id_mls]
GO
ALTER TABLE [dbo].[rcaPhotos] ADD  CONSTRAINT [DF_rcaPhotos_DateEntered]  DEFAULT (getdate()) FOR [DateEntered]
GO
ALTER TABLE [dbo].[rcaPhotos] ADD  CONSTRAINT [DF_rcaPhotos_Complete]  DEFAULT ((1)) FOR [Complete]
GO
ALTER TABLE [dbo].[rcaProps] ADD  CONSTRAINT [DF_rcaProps_id_MLS]  DEFAULT ((0)) FOR [id_MLS]
GO
ALTER TABLE [dbo].[rcaProps] ADD  CONSTRAINT [DF_rcaProps_id_Partner]  DEFAULT ((0)) FOR [id_Partner]
GO
ALTER TABLE [dbo].[rcaProps] ADD  CONSTRAINT [DF_rcaProps_rcaid]  DEFAULT ((0)) FOR [rcaid]
GO
ALTER TABLE [dbo].[rcaProps] ADD  CONSTRAINT [DF_rcaProps_id_Partner2]  DEFAULT ((0)) FOR [id_Partner2]
GO
ALTER TABLE [dbo].[rcaProps] ADD  CONSTRAINT [DF_rcaProps_rcaid2]  DEFAULT ((0)) FOR [rcaid2]
GO
ALTER TABLE [dbo].[rcaProps] ADD  CONSTRAINT [DF_rcaProps_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[rcaProps] ADD  CONSTRAINT [DF_rcaProps_DateEntered]  DEFAULT (getdate()) FOR [DateEntered]
GO
ALTER TABLE [dbo].[rcaProps] ADD  CONSTRAINT [DF_rcaProps_bToProcess]  DEFAULT ((1)) FOR [bToProcess]
GO
ALTER TABLE [dbo].[rcaRealtors] ADD  CONSTRAINT [DF_rcaRealtors_id_Partner]  DEFAULT ((0)) FOR [id_Partner]
GO
ALTER TABLE [dbo].[rcaRealtors] ADD  CONSTRAINT [DF_rcaRealtors_rcaid]  DEFAULT ((0)) FOR [rcaid]
GO
ALTER TABLE [dbo].[rcaRealtors] ADD  CONSTRAINT [DF_rcaRealtors_id_type]  DEFAULT ((4)) FOR [id_type]
GO
ALTER TABLE [dbo].[rcaRealtors] ADD  CONSTRAINT [DF_rcaRealtors_disabled]  DEFAULT ((0)) FOR [disabled]
GO
ALTER TABLE [dbo].[rcaRealtors] ADD  CONSTRAINT [DF_rcaRealtors_failed]  DEFAULT ((0)) FOR [failed]
GO
ALTER TABLE [dbo].[smActivityLog] ADD  CONSTRAINT [DF_smActivityLog_CreatedBy_1]  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[smAutoPreference] ADD  CONSTRAINT [DF_smAutoPreference_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[smContentLibrary] ADD  CONSTRAINT [DF_smContentLibrary_IsFacebook_1]  DEFAULT ((0)) FOR [IsFacebook]
GO
ALTER TABLE [dbo].[smContentLibrary] ADD  CONSTRAINT [DF_smContentLibrary_IsLinkedIn_1]  DEFAULT ((0)) FOR [IsLinkedIn]
GO
ALTER TABLE [dbo].[smContentLibrary] ADD  CONSTRAINT [DF_smContentLibrary_IsTwitter_1]  DEFAULT ((0)) FOR [IsTwitter]
GO
ALTER TABLE [dbo].[smContentLibrary] ADD  CONSTRAINT [DF_smContentLibrary_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[smContentLibrary] ADD  CONSTRAINT [DF_smContentLibrary_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[smContentLibrary] ADD  CONSTRAINT [DF_smContentLibrary_CategoryId]  DEFAULT ((0)) FOR [CategoryId]
GO
ALTER TABLE [dbo].[smContentLibrary] ADD  CONSTRAINT [DF_smContentLibrary_SubIndustryId]  DEFAULT ((0)) FOR [SubIndustryId]
GO
ALTER TABLE [dbo].[smContentLibrary] ADD  CONSTRAINT [DF_smContentLibrary_Archive]  DEFAULT ((0)) FOR [Archive]
GO
ALTER TABLE [dbo].[smContentstatus] ADD  CONSTRAINT [DF_smContentstatus_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[smContentstatus] ADD  CONSTRAINT [DF_smContentstatus_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[smIndustry] ADD  CONSTRAINT [DF_Category_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[smIndustry] ADD  CONSTRAINT [DF_Category_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[smLandingPagesCategoryForLeads] ADD  CONSTRAINT [DF_smLandingPagesCategory_IndustryId]  DEFAULT ((0)) FOR [IndustryId]
GO
ALTER TABLE [dbo].[smLandingPagesCategoryForLeads] ADD  CONSTRAINT [DF_smLandingPagesCategory_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[smLandingPagesCollection] ADD  CONSTRAINT [DF_smLandingPagesCollection_LeadCategoryId]  DEFAULT ((0)) FOR [LeadCategoryId]
GO
ALTER TABLE [dbo].[smLeadCategory] ADD  CONSTRAINT [DF_smLeadCategory_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[smLeads] ADD  CONSTRAINT [DF_smLeads_LeadCategoryId]  DEFAULT ((0)) FOR [LeadCategoryId]
GO
ALTER TABLE [dbo].[smLeads] ADD  CONSTRAINT [DF_smLeads_HasRealtor]  DEFAULT ((0)) FOR [HasRealtor]
GO
ALTER TABLE [dbo].[smLeads] ADD  CONSTRAINT [DF_smLeads_DateEntered]  DEFAULT (getdate()) FOR [DateEntered]
GO
ALTER TABLE [dbo].[smLeads] ADD  CONSTRAINT [DF_smLeads_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[smLeads] ADD  CONSTRAINT [DF_smLeads_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[smLinkedInPostDetails] ADD  CONSTRAINT [DF_smLinkedInPostDetails_Partner_Id]  DEFAULT ((0)) FOR [Partner_Id]
GO
ALTER TABLE [dbo].[smPost] ADD  CONSTRAINT [DF_smPost_EventId_1]  DEFAULT ((0)) FOR [EventId]
GO
ALTER TABLE [dbo].[smPost] ADD  CONSTRAINT [DF_smPost_IsPosted]  DEFAULT ((0)) FOR [IsPosted]
GO
ALTER TABLE [dbo].[smPost] ADD  CONSTRAINT [DF_smPost_LikesCount_1]  DEFAULT ((0)) FOR [LikesCount]
GO
ALTER TABLE [dbo].[smPost] ADD  CONSTRAINT [DF_smPost_CommentsCount_1]  DEFAULT ((0)) FOR [CommentsCount]
GO
ALTER TABLE [dbo].[smPost] ADD  CONSTRAINT [DF_smPost_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[smPost] ADD  CONSTRAINT [DF_smPost_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[smPost] ADD  CONSTRAINT [DF_smPost_ExpiryDate]  DEFAULT ('9999-12-31') FOR [ExpiryDate]
GO
ALTER TABLE [dbo].[smPost] ADD  CONSTRAINT [DF_smPost_CreatedBy_1]  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[smPost] ADD  CONSTRAINT [DF_smPost_DeletedBy_1]  DEFAULT ((0)) FOR [DeletedBy]
GO
ALTER TABLE [dbo].[smPost] ADD  CONSTRAINT [DF_smPost_ModifiedBy_1]  DEFAULT ((0)) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[smPost] ADD  CONSTRAINT [DF_smPost_ContentId]  DEFAULT ((0)) FOR [ContentId]
GO
ALTER TABLE [dbo].[smPost] ADD  CONSTRAINT [DF_smPost_ContentCreatedId]  DEFAULT ((1)) FOR [ContentCreatedId]
GO
ALTER TABLE [dbo].[smPreference] ADD  CONSTRAINT [DF_smPreference_IsDeleted_1]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[smPreference] ADD  CONSTRAINT [DF_smPreference_CreatedBy_1]  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[smPreference] ADD  CONSTRAINT [DF_smPreference_DeletedBy_1]  DEFAULT ((0)) FOR [DeletedBy]
GO
ALTER TABLE [dbo].[smPreference] ADD  CONSTRAINT [DF_smPreference_ModifiedBy_1]  DEFAULT ((0)) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[smProperties] ADD  CONSTRAINT [DF_smProperties_Sold]  DEFAULT ((0)) FOR [Sold]
GO
ALTER TABLE [dbo].[smProperties] ADD  CONSTRAINT [DF_smProperties_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[smRssFeeds] ADD  CONSTRAINT [DF_smRssFeeds_UserId]  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[smRssFeeds] ADD  CONSTRAINT [DF_smRssFeeds_Limit]  DEFAULT ((0)) FOR [Limit]
GO
ALTER TABLE [dbo].[smRssFeeds] ADD  CONSTRAINT [DF_smRssFeeds_CreatedBy]  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[smScheduleEvents] ADD  CONSTRAINT [DF_smScheduleEvents_IsPosted_1]  DEFAULT ((0)) FOR [IsPosted]
GO
ALTER TABLE [dbo].[smScheduleEvents] ADD  CONSTRAINT [DF_smScheduleEvents_IsFacebook_1]  DEFAULT ((0)) FOR [IsFacebook]
GO
ALTER TABLE [dbo].[smScheduleEvents] ADD  CONSTRAINT [DF_smScheduleEvents_IsLinkedIn_1]  DEFAULT ((0)) FOR [IsLinkedIn]
GO
ALTER TABLE [dbo].[smScheduleEvents] ADD  CONSTRAINT [DF_smScheduleEvents_IsTwitter_1]  DEFAULT ((0)) FOR [IsTwitter]
GO
ALTER TABLE [dbo].[smScheduleEvents] ADD  CONSTRAINT [DF_smScheduleEvents_ContentCreatedId]  DEFAULT ((1)) FOR [ContentCreatedId]
GO
ALTER TABLE [dbo].[smSocialMedia] ADD  CONSTRAINT [DF_smSocialMedia_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[smSocialMedia] ADD  CONSTRAINT [DF_smSocialMedia_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[smSocialMediaProfile] ADD  CONSTRAINT [DF_SMB_FacebookProfile_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[smSocialMediaProfile] ADD  CONSTRAINT [DF_SMB_FacebookProfile_Link-id_partner]  DEFAULT ((0)) FOR [Partner_Id]
GO
ALTER TABLE [dbo].[smSocialMediaProfile] ADD  CONSTRAINT [DF_smSocialMediaProfile_IsAccountActive]  DEFAULT ((0)) FOR [IsAccountActive]
GO
ALTER TABLE [dbo].[smSocialMediaProfile] ADD  CONSTRAINT [DF_smSocialMediaProfile_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[smSocialMediaProfile] ADD  CONSTRAINT [DF_smSocialMediaProfile_CreatedBy_1]  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[smSocialMediaProfile] ADD  CONSTRAINT [DF_smSocialMediaProfile_DeletedBy_1]  DEFAULT ((0)) FOR [DeletedBy]
GO
ALTER TABLE [dbo].[smSocialMediaProfile] ADD  CONSTRAINT [DF_smSocialMediaProfile_AccountActiveBy_1]  DEFAULT ((0)) FOR [AccountActiveBy]
GO
ALTER TABLE [dbo].[smSocialMediaProfile] ADD  CONSTRAINT [DF_smSocialMediaProfile_AccountDeactiveBy_1]  DEFAULT ((0)) FOR [AccountDeactiveBy]
GO
ALTER TABLE [dbo].[smSubIndustry] ADD  CONSTRAINT [DF_smSubIndustry_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[smSubIndustry] ADD  CONSTRAINT [DF_smSubIndustry_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[smTwitterPostDetails] ADD  CONSTRAINT [DF_smTwitterPostDetails_Partner_Id]  DEFAULT ((0)) FOR [Partner_Id]
GO
ALTER TABLE [dbo].[smUserActiveSocialMedia] ADD  CONSTRAINT [DF_smUserActiveSocialMedia_Partner_Id]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[smViews] ADD  CONSTRAINT [DF_smViews_DateAdded]  DEFAULT (getdate()) FOR [DateEntered]
GO
ALTER TABLE [dbo].[smWatermark] ADD  CONSTRAINT [DF_smWatermark_UserID_1]  DEFAULT ((0)) FOR [UserID]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedBy]  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsSuperAdmin]  DEFAULT ((0)) FOR [IsSuperAdmin]
GO
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [IsConfirmed]
GO
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess]
GO
ALTER TABLE [dbo].[ProvinceState ]  WITH CHECK ADD  CONSTRAINT [FK_ProvinceState _Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([CountryId])
GO
ALTER TABLE [dbo].[ProvinceState ] CHECK CONSTRAINT [FK_ProvinceState _Countries]
GO
ALTER TABLE [dbo].[smLeads]  WITH CHECK ADD  CONSTRAINT [FK_smLeads_smLeads] FOREIGN KEY([id])
REFERENCES [dbo].[smLeads] ([id])
GO
ALTER TABLE [dbo].[smLeads] CHECK CONSTRAINT [FK_smLeads_smLeads]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserType] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserType] ([UserTypeId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserType]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[webpages_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_RoleId]
GO
/****** Object:  StoredProcedure [dbo].[smGetViewClick]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Rohit Grover>
-- Create date: <Create Date,,2017-10-03 15:33:33.983>
-- Description:	<Description,, To get view CLick>
-- =============================================    smGetViewClick 3  
Create PROCEDURE [dbo].[smGetViewClick]

	@UserId int
	AS

BEGIN
	select smv.[UserId]
      ,smv.[UniquePostId]
      ,smv.[Postid]
      ,smv.[DateEntered]
      ,smv.[ViewId] 
	  ,smp.[SocialMedia]
	  from smViews smv  join smPost smp on smv.UniquePostId=smp.UniquePostId
	where smv.UserId =@UserId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_smSaveHomeValue]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Rohit>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_smSaveHomeValue]
	


	@address  varchar(max)      ,
		        @StreetAddress  varchar(max),
		     	@Unit		    varchar(max),
			    @City		    varchar(max),
				@Province	    varchar(max),
				@PostalCode	    varchar(max),
				@FirstName	    varchar(max),
				@LastName	    varchar(max),
				@EmailAddress   varchar(max),
				@PhoneNumber    varchar(max),
				@UserID		   int,
				@IsCompleted   bit,
				@TimePeriodId  int,
				@Notify		    bit,
				@DateSubmit	  datetime,
				@IPAddress	    nvarchar(400),
				@PostId		    int
							   
AS
BEGIN
     declare  @provision varchar(200)
	set @provision = REPLACE(@Province, ',', ' ')
	Insert into  smHomevalue (
	            [Address],
				 StreetAddress,
				 Unit,
				 City,
				 Province,
				 PostalCode,
				 FirstName,
				 LastName,
				 EmailAddress ,
				 PhoneNumber,
				 UserID,
				 IsCompleted,
				 TimePeriodId,
				 Notify,
				 DateSubmit,
				 IPAddress,
				 PostId  )

				 values
				 (
	            @Address,
				@StreetAddress,
		     	@Unit,
			    @City,
				@provision,
				@PostalCode,
				@FirstName,
				@LastName,
				@EmailAddress,
				@PhoneNumber,
				@UserID,
				@IsCompleted,
				@TimePeriodId,
				@Notify,
				@DateSubmit,
				@IPAddress,
				@PostId)

				END
GO
/****** Object:  StoredProcedure [dbo].[sp_smUpdateHomeWizardValue]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Rohit>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_smUpdateHomeWizardValue]
	@Address          varchar(max)      ,           
	@StreetAddress	  varchar(max),
	@Unit			  varchar(max),
	@City			  varchar(max),
	@Province		  varchar(max),
	@PostalCode		  varchar(max),
	@FirstName		  varchar(max),
	@LastName		  varchar(max),
	@EmailAddress	  varchar(max),
	@PhoneNumber	  varchar(max),
	@UserID			 int,
	@IsCompleted	 bit,
	@TimePeriodId	 int,
	@Notify			  bit,
	@DateSubmit		 datetime,
	@IPAddress       varchar(max),
	@PostId          int
	
AS
BEGIN


declare   @provincec nvarchar(250) 
set @provincec = replace(@Province, ',', '')
IF  NULLIF(@Address, '') IS NOT NULL
---- STEP 1

	          UPDATE smHomeValue SET 
                  [Address]= @Address   ,                       
                  UserID  =    @UserID	,		
                  IsCompleted= @IsCompleted	     ,  
                  DateSubmit=  @DateSubmit	
			          
              WHERE IPAddress= @IPAddress     and postid  =  @PostId 

ELSE IF NULLIF(@Address, '') IS NULL AND  NULLIF(@StreetAddress, '') IS NOT NULL AND NULLIF(@City, '') IS NOT NULL
--- STEP 2 
UPDATE smHomeValue SET 
               
                  StreetAddress=@StreetAddress	        ,
                  Unit=        @Unit			          ,        
                  City=        @City			       ,
                  Province=    @provincec		       ,
                  PostalCode=  @PostalCode		   , 
                  IsCompleted= @IsCompleted	     , 
                  DateSubmit=  @DateSubmit	,	  
                       UserID  =    @UserID	                                                  
              WHERE IPAddress= @IPAddress     and postid  =  @PostId 
 
ELSE IF NULLIF(@Address, '') IS NULL AND  NULLIF(@StreetAddress, '') IS NULL AND   NULLIF(@City, '') IS NULL 
  
  --- STEP 3

	UPDATE smHomeValue SET 
                
                  FirstName=   @FirstName		  ,
                  LastName=    @LastName		      ,
                  EmailAddress=@EmailAddress	   ,
                  PhoneNumber= @PhoneNumber	     ,               
                  UserID  =    @UserID	,		
                  IsCompleted= 1	     ,      
                  TimePeriodId=@TimePeriodId	     ,      
                  Notify=      @Notify			        ,
                  DateSubmit=  @DateSubmit		  
                                                                     
              WHERE IPAddress= @IPAddress     and postid  =  @PostId 



			  --ELSE


			  --UPDATE smHomeValue SET 
     --             [Address]= @Address   ,
     --             StreetAddress=@StreetAddress	        ,
     --             Unit=        @Unit			          ,        
     --             City=        @City			       ,
     --             Province=    @Province		       ,
     --             PostalCode=  @PostalCode		   ,
     --             FirstName=   @FirstName		  ,
     --             LastName=    @LastName		      ,
     --             EmailAddress=@EmailAddress	   ,
     --             PhoneNumber= @PhoneNumber	     ,               
     --             UserID  =    @UserID	,		
     --             IsCompleted= @IsCompleted	     ,      
     --             TimePeriodId=@TimePeriodId	     ,      
     --             Notify=      @Notify			        ,
     --             DateSubmit=  @DateSubmit		  
                                                                     
     --         WHERE IPAddress= @IPAddress     and postid  =  @PostId 

END
GO
/****** Object:  StoredProcedure [dbo].[ssp_smGetAuthorizeToken]    Script Date: 11/29/2018 12:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--assign Token in a variable



CREATE proc [dbo].[ssp_smGetAuthorizeToken] --34

(
@UserId int

)
as

DECLARE @TokenID uniqueidentifier
Declare @ExpireTime datetime
declare @min int
Declare @SessionTime datetime
Declare @ActivityTime datetime
Declare @CurrentTime datetime
DECLARE @ResponseTokenID uniqueidentifier
--DATEADD(mi,10,GETDATE()) 



--User Not exists



if not exists(Select AuthenticationId from smLoginAuthentication where UserId=@UserId and Active=0)

begin

SET @TokenID = NEWID()
set @SessionTime= GETUTCDATE()--DATEADD(hour, -5, getdate())

insert into smLoginAuthentication(UserId,SessionTime,ExpireTime,TokenId,Active,ActivityTime)
values(@UserId,@SessionTime,10,@TokenID,0,GETUTCDATE())
select @ResponseTokenID = TokenId from smLoginAuthentication where UserId=@UserId and Active=0
end
else



--user exists



begin



select @ActivityTime = ActivityTime from smLoginAuthentication where UserId=@UserId and Active=0

select @min = ExpireTime from smLoginAuthentication where UserId=@UserId and Active=0

set @ExpireTime=DATEADD(mi,@min,@ActivityTime)



set @CurrentTime=GETUTCDATE()--DATEADD(hour, -5, getdate())



if(@CurrentTime<=@ExpireTime)



begin



select @ResponseTokenID = TokenId from smLoginAuthentication where UserId=@UserId and Active=0



end



else



begin



set @ResponseTokenID=null



update  smLoginAuthentication set Active=1 where UserId=@UserId and Active=0



end



end
GO
USE [master]
GO
ALTER DATABASE [smb] SET  READ_WRITE 
GO

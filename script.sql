USE [master]
GO
/****** Object:  Database [OnlineExamination1]    Script Date: 23-11-2021 10:11:37 ******/
CREATE DATABASE [OnlineExamination1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnlineExamination1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\OnlineExamination1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OnlineExamination1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\OnlineExamination1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OnlineExamination1] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineExamination1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineExamination1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlineExamination1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlineExamination1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlineExamination1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlineExamination1] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlineExamination1] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [OnlineExamination1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlineExamination1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineExamination1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlineExamination1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlineExamination1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlineExamination1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlineExamination1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlineExamination1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlineExamination1] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OnlineExamination1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlineExamination1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlineExamination1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlineExamination1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlineExamination1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlineExamination1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlineExamination1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlineExamination1] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OnlineExamination1] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineExamination1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineExamination1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlineExamination1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlineExamination1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OnlineExamination1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OnlineExamination1] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OnlineExamination1] SET QUERY_STORE = OFF
GO
USE [OnlineExamination1]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 23-11-2021 10:11:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[admin_id] [nvarchar](20) NOT NULL,
	[admin_name] [nvarchar](100) NOT NULL,
	[admin_password] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[admin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Candidate]    Script Date: 23-11-2021 10:11:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Candidate](
	[Userid] [nvarchar](20) NOT NULL,
	[Userfname] [nvarchar](100) NOT NULL,
	[Userlname] [nvarchar](100) NOT NULL,
	[phoneno] [nvarchar](30) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[confirmpassword] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 23-11-2021 10:11:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[category_id] [nvarchar](20) NOT NULL,
	[category_name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[exam]    Script Date: 23-11-2021 10:11:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[exam](
	[exam_id] [nvarchar](20) NOT NULL,
	[exam_description] [nvarchar](100) NULL,
	[category_id] [nvarchar](20) NULL,
	[levelid] [int] NULL,
	[duration] [datetime] NULL,
	[Noofquestion] [int] NULL,
	[marks] [int] NULL,
	[totalmarks] [int] NULL,
	[passmarks] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[exam_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 23-11-2021 10:11:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[Qid] [int] NOT NULL,
	[Q_text] [nvarchar](max) NOT NULL,
	[QA] [nvarchar](100) NOT NULL,
	[QB] [nvarchar](100) NOT NULL,
	[QC] [nvarchar](100) NOT NULL,
	[QD] [nvarchar](100) NOT NULL,
	[Qcorrectans] [nvarchar](100) NOT NULL,
	[category_id] [nvarchar](20) NOT NULL,
	[levelid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Qid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 23-11-2021 10:11:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[result_id] [int] IDENTITY(1,1) NOT NULL,
	[result_status] [nvarchar](50) NULL,
	[result_score] [int] NULL,
	[user_id] [nvarchar](20) NULL,
	[exam_id] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[result_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[admin] ([admin_id], [admin_name], [admin_password]) VALUES (N'A101', N'ravi', N'ravi123')
GO
INSERT [dbo].[Candidate] ([Userid], [Userfname], [Userlname], [phoneno], [email], [password], [confirmpassword]) VALUES (N'jo7', N'jothi', N'ravi', N'7890654321', N'jo4@gmail.com', N'123', N'123')
INSERT [dbo].[Candidate] ([Userid], [Userfname], [Userlname], [phoneno], [email], [password], [confirmpassword]) VALUES (N'jo78', N'jothi', N'ravi', N'7890654321', N'jo@gmail.co', N'jo', N'jo')
INSERT [dbo].[Candidate] ([Userid], [Userfname], [Userlname], [phoneno], [email], [password], [confirmpassword]) VALUES (N'jo789', N'jothi', N'ravi', N'7890654321', N'jo@gmail.com', N'jo789', N'jo789')
INSERT [dbo].[Candidate] ([Userid], [Userfname], [Userlname], [phoneno], [email], [password], [confirmpassword]) VALUES (N'jo7899', N'jothi', N'ravi', N'7890654321', N'jo1@gmail.com', N'123', N'123')
GO
INSERT [dbo].[Category] ([category_id], [category_name]) VALUES (N'C1', N'Java')
INSERT [dbo].[Category] ([category_id], [category_name]) VALUES (N'C2', N'SQL')
GO
INSERT [dbo].[exam] ([exam_id], [exam_description], [category_id], [levelid], [duration], [Noofquestion], [marks], [totalmarks], [passmarks]) VALUES (N'E1', N'This  is java exam', N'C1', 1, CAST(N'2021-11-18T12:00:00.000' AS DateTime), 5, 1, 5, 3)
INSERT [dbo].[exam] ([exam_id], [exam_description], [category_id], [levelid], [duration], [Noofquestion], [marks], [totalmarks], [passmarks]) VALUES (N'E2', N'This  is java exam LEVEL2', N'C1', 2, CAST(N'2021-11-18T12:00:00.000' AS DateTime), 5, 1, 5, 3)
INSERT [dbo].[exam] ([exam_id], [exam_description], [category_id], [levelid], [duration], [Noofquestion], [marks], [totalmarks], [passmarks]) VALUES (N'E3', N'This  is java exam LEVEL3', N'C1', 3, CAST(N'2021-11-18T12:00:00.000' AS DateTime), 5, 1, 5, 3)
GO
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (1, N' CLR is the .Net equivalent of _____.', N'Java Virtual machine', N'Common Language Runtime', N'Common Type System', N'Common Language Specification', N'A', N'C1', 1)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (2, N'Abstract class contains _____.', N'Abstract methods', N'Non Abstract methods', N'Both', N'None', N'C', N'C1', 1)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (3, N'The default scope for the members of an interface is _____.', N'private', N'public', N'protected', N'internal', N'B', N'C1', 1)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (4, N'The data members of a class by default are?', N'protected, public', N'private, public', N'private', N'public', N'C', N'C1', 1)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (5, N'Which of the following is incorrect about constructors?', N'Defining of constructors can be implicit or explicit.', N'The calling of constructors is explicit.', N'Implicit constructors can be parameterized or parameterless.', N'Explicit constructors can be parameterized or parameterless.', N'C', N'C1', 1)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (6, N'Reference is a ___.', N'Copy of class which leads to memory allocation.', N'Copy of class that is not initialized.', N'Pre-defined data type.', N'Copy of class creating by an existing instance.', N'D', N'C1', 2)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (7, N'Struct’s data members are ___ by default.', N'Protected', N'Public', N'Private', N'Default', N'C', N'C1', 2)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (8, N'The point at which an exception is thrown is called the _____.', N'Default point', N'Invoking point', N'Calling point', N'Throw point', N'D', N'C1', 2)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (9, N'Which of the following keywords is used to refer base class constructor to subclass constructor?', N'this', N'static', N'base', N'extend', N'C', N'C1', 2)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (10, N'Select the two types of threads mentioned in the concept of multithreading?', N'Foreground', N'Background', N'Only foreground', N'Both foreground and background', N'D', N'C1', 2)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (11, N' Which of the following converts a type to a signed byte type in C#?', N'ToInt64', N'ToSbyte', N'ToSingle', N'ToInt32', N'B', N'C1', 3)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (12, N'Which of the following converts a type to an unsigned long type in C#?', N'ToType', N'ToUInt16', N'ToUInt32', N'ToString', N'C', N'C1', 3)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (13, N'Which of the following statements is correct about access specifiers in C#?', N'Encapsulation is implemented by using access specifiers.', N' An access specifier defines the scope and visibility of a class member.', N'Both of the above.', N'None of the above.', N'C', N'C1', 3)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (14, N'C# has _______ operator, useful for making two way decisions.', N'Looping', N'Functional', N'Exponential', N'Conditional', N'D', N'C1', 3)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (15, N'A structure in C# provides a unique way of packing together data of ______ types.', N'Different', N'Same', N'Invoking', N'Calling', N'A', N'C1', 3)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (16, N'Who invented Java Programming?', N'Guido van Rossum', N'James Gosling', N'Dennis Ritchie', N'Bjarne Stroustrup', N'B', N'C2', 1)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (17, N'Which statement is true about Java?', N'Java is a sequence-dependent programming language', N' Java is a code dependent programming language', N'Java is a platform-dependent programming language', N'Java is a platform independent programming language', N'D', N'C2', 1)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (18, N' Which component is used to compile, debug and execute the java programs?', N' JRE', N'JIT', N'JDK', N'JVM', N'C', N'C2', 1)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (19, N'Which one of the following is not a Java feature?', N'Object-oriented', N' Use of pointers', N'Portable', N'Dynamic and Extensible', N'B', N'C2', 2)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (20, N' Which of these cannot be used for a variable name in Java?', N'identifier', N'keyword', N'identifier & keyword', N'none', N'B', N'C2', 2)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (21, N'What is the extension of java code files?', N'.js', N' .txt', N'.class', N'.java', N'D', N'C2', 2)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (22, N'Which environment variable is used to set the java path?', N'MAVEN_HOME', N'CLASSPATH', N'JAVA  ', N'JAVA_HOME', N'D', N'C2', 3)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (23, N'Which of the following is not an OOPS concept in Java?', N'Polymorphism', N'Inheritance', N'Compilation', N'Encapsulation', N'C', N'C2', 3)
INSERT [dbo].[Questions] ([Qid], [Q_text], [QA], [QB], [QC], [QD], [Qcorrectans], [category_id], [levelid]) VALUES (24, N' Which of the following is a type of polymorphism in Java Programming?', N'Multiple polymorphism', N'Compile time polymorphism', N'Multilevel polymorphism', N' Execution time polymorphism', N'B', N'C2', 3)
GO
SET IDENTITY_INSERT [dbo].[Reports] ON 

INSERT [dbo].[Reports] ([result_id], [result_status], [result_score], [user_id], [exam_id]) VALUES (1, N'excellent', 5, N'jo789', N'E1')
SET IDENTITY_INSERT [dbo].[Reports] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Candidat__AB6E616449829615]    Script Date: 23-11-2021 10:11:37 ******/
ALTER TABLE [dbo].[Candidate] ADD UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Category__5189E2556CFA2D44]    Script Date: 23-11-2021 10:11:37 ******/
ALTER TABLE [dbo].[Category] ADD UNIQUE NONCLUSTERED 
(
	[category_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[exam]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[Category] ([category_id])
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[Category] ([category_id])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD FOREIGN KEY([exam_id])
REFERENCES [dbo].[exam] ([exam_id])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[Candidate] ([Userid])
GO
USE [master]
GO
ALTER DATABASE [OnlineExamination1] SET  READ_WRITE 
GO

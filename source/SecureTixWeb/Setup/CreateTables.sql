USE [StealThisDatabase]


-- Create Tables
IF EXISTS (SELECT * FROM sysobjects WHERE name='Events' and xtype='U')
BEGIN
    DROP TABLE [Events]
END
CREATE TABLE [dbo].[Events](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[Description] [nvarchar](2048) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[OnSale] [bit] NOT NULL,
	[SoldOut] [bit] NOT NULL,
	[ImageUrl] [nvarchar](1024) NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


IF EXISTS (SELECT * FROM sysobjects WHERE name='OrderItems' and xtype='U')
BEGIN
    DROP TABLE [OrderItems]
END
CREATE TABLE [dbo].[OrderItems](
	[OrderItemId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[TicketCount] [int] NOT NULL,
	[PricePerTicket] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[OrderItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


IF EXISTS (SELECT * FROM sysobjects WHERE name='Orders' and xtype='U')
BEGIN
    DROP TABLE [Orders]
END
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ConfirmedAt] [datetime] NOT NULL,
	[RefundedAt] [datetime] NULL,
	[TotalValue] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


IF EXISTS (SELECT * FROM sysobjects WHERE name='UserBasketItems' and xtype='U')
BEGIN
    DROP TABLE [UserBasketItems]
END
CREATE TABLE [dbo].[UserBasketItems](
	[BasketItemId] [int] IDENTITY(1,1) NOT NULL,
	[BasketId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[TicketCount] [int] NOT NULL,
	[PricePerTicket] [int] NOT NULL,
 CONSTRAINT [PK_UserBasketItems] PRIMARY KEY CLUSTERED 
(
	[BasketItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


IF EXISTS (SELECT * FROM sysobjects WHERE name='UserBaskets' and xtype='U')
BEGIN
    DROP TABLE [UserBaskets]
END
CREATE TABLE [dbo].[UserBaskets](
	[BasketId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PaidAt] [datetime] NULL,
	[RefundedAt] [datetime] NULL,
 CONSTRAINT [PK_UserBaskets] PRIMARY KEY CLUSTERED 
(
	[BasketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


IF EXISTS (SELECT * FROM sysobjects WHERE name='Users' and xtype='U')
BEGIN
    DROP TABLE [Users]
END
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Role]  DEFAULT (N'Standard') FOR [Role]


IF EXISTS (SELECT * FROM sysobjects WHERE name='MailingList' and xtype='U')
BEGIN
    DROP TABLE [MailingList]
END
CREATE TABLE [dbo].[MailingList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmailAddress] [nvarchar](512) NOT NULL,
 CONSTRAINT [PK_MailingList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

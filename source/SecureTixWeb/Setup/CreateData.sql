USE [StealThisDatabase]

-- Users
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Role]) VALUES (1, N'testuser@securetix.com', N'5f4dcc3b5aa765d61d8327deb882cf99', N'Standard')
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Role]) VALUES (2, N'admin@securetix.com', N'9a618248b64db62d15b300a07b00580b', N'Admin')
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Role]) VALUES (3, N'ctiller2@hostgator.com', N'845c1614456edee040c5f23206ada390', N'Standard')
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Role]) VALUES (6, N'jpitson0@craigslist.org', N'0571749e2ac330a7455809c6b0e7af90', N'Standard')
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Role]) VALUES (7, N'tbasire3@bluehost.com', N'dbe3017ea50ccffeb211726514cff700', N'Standard')
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Role]) VALUES (9, N'dstallebrass4@springer.com', N'9a2e6e41e4e3a906c4c4b2e3cfdbb5da', N'Standard')
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Role]) VALUES (10, N'admin2@securetix.com', N'7fc54f2a830b2391e0fc8b5f3559dc59', N'Admin')
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Role]) VALUES (11, N'superuser@securetix.com', N'fb490005a09267d1d56049db31cfcc03', N'Standard')
SET IDENTITY_INSERT [dbo].[Users] OFF

-- Events
SET IDENTITY_INSERT [dbo].[Events] ON 
INSERT [dbo].[Events] ([Id], [Name], [Description], [Price], [OnSale], [SoldOut], [ImageUrl]) VALUES (1, N'A Midummer Night''s Dream', N'The course of true love never did run smooth...', CAST(10.00 AS Decimal(18, 2)), 1, 1, NULL)
INSERT [dbo].[Events] ([Id], [Name], [Description], [Price], [OnSale], [SoldOut], [ImageUrl]) VALUES (2, N'The Importance of Being Earnest', N'The truth is rarely pure and never simple...', CAST(20.00 AS Decimal(18, 2)), 1, 0, NULL)
INSERT [dbo].[Events] ([Id], [Name], [Description], [Price], [OnSale], [SoldOut], [ImageUrl]) VALUES (3, N'Twelfth Night', N'If music be the food of love, play on...', CAST(25.00 AS Decimal(18, 2)), 1, 0, NULL)
INSERT [dbo].[Events] ([Id], [Name], [Description], [Price], [OnSale], [SoldOut], [ImageUrl]) VALUES (4, N'Cats on Ice', N'Real cats, real ice, better than the play...', CAST(50.00 AS Decimal(18, 2)), 1, 1, NULL)
INSERT [dbo].[Events] ([Id], [Name], [Description], [Price], [OnSale], [SoldOut], [ImageUrl]) VALUES (521, N'Illuminati Summer BBQ', N'Members only', CAST(100.00 AS Decimal(18, 2)), 0, 0, NULL)
INSERT [dbo].[Events] ([Id], [Name], [Description], [Price], [OnSale], [SoldOut], [ImageUrl]) VALUES (863, N'Lockdown Drinks', N'Could be controversial, don''t let the press find out about this!', CAST(0.00 AS Decimal(18, 2)), 0, 1, NULL)
SET IDENTITY_INSERT [dbo].[Events] OFF

SET IDENTITY_INSERT [dbo].[OrderItems] ON 
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [EventId], [TicketCount], [PricePerTicket]) VALUES (1, 1, 2, 1, CAST(50.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [EventId], [TicketCount], [PricePerTicket]) VALUES (2, 2, 4, 2, CAST(20.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [EventId], [TicketCount], [PricePerTicket]) VALUES (3, 3, 521, 1, CAST(100.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [EventId], [TicketCount], [PricePerTicket]) VALUES (4, 4, 5, 2, CAST(25.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [EventId], [TicketCount], [PricePerTicket]) VALUES (5, 4, 3, 2, CAST(10.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [EventId], [TicketCount], [PricePerTicket]) VALUES (6, 5, 2, 99, CAST(50.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [EventId], [TicketCount], [PricePerTicket]) VALUES (7, 6, 521, 1, CAST(100.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [EventId], [TicketCount], [PricePerTicket]) VALUES (8, 6, 863, 2, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [EventId], [TicketCount], [PricePerTicket]) VALUES (9, 7, 521, 2, CAST(100.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [EventId], [TicketCount], [PricePerTicket]) VALUES (10, 8, 863, 1, CAST(0.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[OrderItems] OFF

SET IDENTITY_INSERT [dbo].[Orders] ON 
INSERT [dbo].[Orders] ([OrderId], [UserId], [ConfirmedAt], [RefundedAt], [TotalValue]) VALUES (1, 1, CAST(N'2022-06-17T10:24:39.593' AS DateTime), NULL, CAST(50.00 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([OrderId], [UserId], [ConfirmedAt], [RefundedAt], [TotalValue]) VALUES (2, 1, CAST(N'2022-06-17T10:24:55.700' AS DateTime), NULL, CAST(40.00 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([OrderId], [UserId], [ConfirmedAt], [RefundedAt], [TotalValue]) VALUES (3, 2, CAST(N'2022-06-17T10:26:05.307' AS DateTime), NULL, CAST(100.00 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([OrderId], [UserId], [ConfirmedAt], [RefundedAt], [TotalValue]) VALUES (4, 3, CAST(N'2022-06-17T10:27:04.173' AS DateTime), NULL, CAST(70.00 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([OrderId], [UserId], [ConfirmedAt], [RefundedAt], [TotalValue]) VALUES (5, 6, CAST(N'2022-06-17T10:27:55.077' AS DateTime), NULL, CAST(4950.00 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([OrderId], [UserId], [ConfirmedAt], [RefundedAt], [TotalValue]) VALUES (6, 7, CAST(N'2022-06-17T10:29:00.553' AS DateTime), NULL, CAST(100.00 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([OrderId], [UserId], [ConfirmedAt], [RefundedAt], [TotalValue]) VALUES (7, 9, CAST(N'2022-06-17T10:29:33.180' AS DateTime), NULL, CAST(200.00 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([OrderId], [UserId], [ConfirmedAt], [RefundedAt], [TotalValue]) VALUES (8, 10, CAST(N'2022-06-17T10:30:09.583' AS DateTime), NULL, CAST(0.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Orders] OFF

SET IDENTITY_INSERT [dbo].[UserBasketItems] ON 
INSERT [dbo].[UserBasketItems] ([BasketItemId], [BasketId], [EventId], [TicketCount], [PricePerTicket]) VALUES (1, 1, 2, 1, 50)
INSERT [dbo].[UserBasketItems] ([BasketItemId], [BasketId], [EventId], [TicketCount], [PricePerTicket]) VALUES (2, 2, 4, 2, 20)
INSERT [dbo].[UserBasketItems] ([BasketItemId], [BasketId], [EventId], [TicketCount], [PricePerTicket]) VALUES (3, 4, 521, 1, 100)
INSERT [dbo].[UserBasketItems] ([BasketItemId], [BasketId], [EventId], [TicketCount], [PricePerTicket]) VALUES (4, 6, 5, 2, 25)
INSERT [dbo].[UserBasketItems] ([BasketItemId], [BasketId], [EventId], [TicketCount], [PricePerTicket]) VALUES (5, 6, 3, 2, 10)
INSERT [dbo].[UserBasketItems] ([BasketItemId], [BasketId], [EventId], [TicketCount], [PricePerTicket]) VALUES (6, 8, 2, 99, 50)
INSERT [dbo].[UserBasketItems] ([BasketItemId], [BasketId], [EventId], [TicketCount], [PricePerTicket]) VALUES (7, 10, 521, 1, 100)
INSERT [dbo].[UserBasketItems] ([BasketItemId], [BasketId], [EventId], [TicketCount], [PricePerTicket]) VALUES (8, 10, 863, 2, 0)
INSERT [dbo].[UserBasketItems] ([BasketItemId], [BasketId], [EventId], [TicketCount], [PricePerTicket]) VALUES (9, 12, 521, 2, 100)
INSERT [dbo].[UserBasketItems] ([BasketItemId], [BasketId], [EventId], [TicketCount], [PricePerTicket]) VALUES (10, 14, 863, 1, 0)
INSERT [dbo].[UserBasketItems] ([BasketItemId], [BasketId], [EventId], [TicketCount], [PricePerTicket]) VALUES (11, 15, 4, 2, 20)
SET IDENTITY_INSERT [dbo].[UserBasketItems] OFF

SET IDENTITY_INSERT [dbo].[UserBaskets] ON 
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (1, 1, CAST(N'2022-06-17T10:24:39.593' AS DateTime), NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (2, 1, CAST(N'2022-06-17T10:24:55.700' AS DateTime), NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (3, 1, NULL, NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (4, 2, CAST(N'2022-06-17T10:26:05.307' AS DateTime), NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (5, 2, NULL, NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (6, 3, CAST(N'2022-06-17T10:27:04.173' AS DateTime), NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (7, 3, NULL, NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (8, 6, CAST(N'2022-06-17T10:27:55.077' AS DateTime), NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (9, 6, NULL, NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (10, 7, CAST(N'2022-06-17T10:29:00.553' AS DateTime), NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (11, 7, NULL, NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (12, 9, CAST(N'2022-06-17T10:29:33.180' AS DateTime), NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (13, 9, NULL, NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (14, 10, CAST(N'2022-06-17T10:30:09.583' AS DateTime), NULL)
INSERT [dbo].[UserBaskets] ([BasketId], [UserId], [PaidAt], [RefundedAt]) VALUES (15, 10, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserBaskets] OFF

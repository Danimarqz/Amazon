CREATE DATABASE IF NOT EXISTS AmazonMontecastelo
USE [AmazonMontecastelo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrito](
	[CarritoID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NOT NULL,
	[FechaCarrito] [datetime2](7) NOT NULL,
	[totalVenta] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Carrito] PRIMARY KEY CLUSTERED 
(
	[CarritoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetallesCarrito](
	[DetalleID] [int] IDENTITY(1,1) NOT NULL,
	[CarritoID] [int] NOT NULL,
	[ProductoID] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [decimal](10, 2) NOT NULL,
	[PrecioTotal] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_DetallesCarrito] PRIMARY KEY CLUSTERED 
(
	[DetalleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetallesVenta](
	[DetalleID] [int] IDENTITY(1,1) NOT NULL,
	[VentaID] [int] NOT NULL,
	[ProductoID] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [decimal](10, 2) NOT NULL,
	[PrecioTotal] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK__Detalles__6E19D6FAA398A39C] PRIMARY KEY CLUSTERED 
(
	[DetalleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[ProductoID] [int] IDENTITY(1,1) NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[Descripcion] [varchar](150) NOT NULL,
	[Nombre] [varchar](150) NOT NULL,
 CONSTRAINT [PK__Producto__A430AE8347166411] PRIMARY KEY CLUSTERED 
(
	[ProductoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [varchar](50) NOT NULL,
	[Contrasena] [varchar](255) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[userType] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Usuarios__2B3DE7983591A88A] PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ventas](
	[VentaID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NOT NULL,
	[FechaVenta] [datetime2](7) NOT NULL,
	[TotalVenta] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK__Ventas__5B41514C943E090F] PRIMARY KEY CLUSTERED 
(
	[VentaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DetallesVenta] ON 
GO
INSERT [dbo].[DetallesVenta] ([DetalleID], [VentaID], [ProductoID], [Cantidad], [PrecioUnitario], [PrecioTotal]) VALUES (68, 6, 16, 4, CAST(50.00 AS Decimal(10, 2)), CAST(200.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[DetallesVenta] ([DetalleID], [VentaID], [ProductoID], [Cantidad], [PrecioUnitario], [PrecioTotal]) VALUES (104, 5, 2, 7, CAST(85.55 AS Decimal(10, 2)), CAST(598.85 AS Decimal(10, 2)))
GO
INSERT [dbo].[DetallesVenta] ([DetalleID], [VentaID], [ProductoID], [Cantidad], [PrecioUnitario], [PrecioTotal]) VALUES (105, 5, 1, 1, CAST(55.00 AS Decimal(10, 2)), CAST(55.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[DetallesVenta] ([DetalleID], [VentaID], [ProductoID], [Cantidad], [PrecioUnitario], [PrecioTotal]) VALUES (106, 5, 16, 1, CAST(450.00 AS Decimal(10, 2)), CAST(450.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[DetallesVenta] ([DetalleID], [VentaID], [ProductoID], [Cantidad], [PrecioUnitario], [PrecioTotal]) VALUES (107, 5, 1, 5, CAST(220.00 AS Decimal(10, 2)), CAST(1100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[DetallesVenta] ([DetalleID], [VentaID], [ProductoID], [Cantidad], [PrecioUnitario], [PrecioTotal]) VALUES (108, 5, 16, 5, CAST(50.00 AS Decimal(10, 2)), CAST(250.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[DetallesVenta] ([DetalleID], [VentaID], [ProductoID], [Cantidad], [PrecioUnitario], [PrecioTotal]) VALUES (109, 5, 2, 1, CAST(85.55 AS Decimal(10, 2)), CAST(85.55 AS Decimal(10, 2)))
GO
INSERT [dbo].[DetallesVenta] ([DetalleID], [VentaID], [ProductoID], [Cantidad], [PrecioUnitario], [PrecioTotal]) VALUES (110, 5, 23, 4, CAST(35.00 AS Decimal(10, 2)), CAST(140.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[DetallesVenta] ([DetalleID], [VentaID], [ProductoID], [Cantidad], [PrecioUnitario], [PrecioTotal]) VALUES (111, 5, 23, 4, CAST(35.00 AS Decimal(10, 2)), CAST(140.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[DetallesVenta] ([DetalleID], [VentaID], [ProductoID], [Cantidad], [PrecioUnitario], [PrecioTotal]) VALUES (112, 5, 1, 5, CAST(220.00 AS Decimal(10, 2)), CAST(1100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[DetallesVenta] ([DetalleID], [VentaID], [ProductoID], [Cantidad], [PrecioUnitario], [PrecioTotal]) VALUES (113, 5, 35, 4, CAST(250.35 AS Decimal(10, 2)), CAST(1001.40 AS Decimal(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[DetallesVenta] OFF
GO
SET IDENTITY_INSERT [dbo].[Productos] ON 
GO
INSERT [dbo].[Productos] ([ProductoID], [Precio], [Descripcion], [Nombre]) VALUES (1, CAST(250.80 AS Decimal(10, 2)), N'Television HD', N'Samsung g')
GO
INSERT [dbo].[Productos] ([ProductoID], [Precio], [Descripcion], [Nombre]) VALUES (2, CAST(75.56 AS Decimal(10, 2)), N'Portactil 4G', N'Philips')
GO
INSERT [dbo].[Productos] ([ProductoID], [Precio], [Descripcion], [Nombre]) VALUES (16, CAST(50.00 AS Decimal(10, 2)), N'iphone12 128Gb', N'Apple')
GO
INSERT [dbo].[Productos] ([ProductoID], [Precio], [Descripcion], [Nombre]) VALUES (23, CAST(35.00 AS Decimal(10, 2)), N'caja d emadera', N'Cebox')
GO
INSERT [dbo].[Productos] ([ProductoID], [Precio], [Descripcion], [Nombre]) VALUES (35, CAST(250.35 AS Decimal(10, 2)), N'hermoso', N'gato')
GO
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 
GO
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Contrasena], [Email], [userType]) VALUES (13, N'a', N'clave1', N'a@gmail.com', N'usuario')
GO
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Contrasena], [Email], [userType]) VALUES (14, N'aa', N'clave2', N'aa@gmail.com', N'administrador')
GO
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Contrasena], [Email], [userType]) VALUES (15, N'jose', N'clave3', N'jose@gmail.com', N'usuario')
GO
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Contrasena], [Email], [userType]) VALUES (19, N'juan', N'clave3', N'juan@gmail.com', N'usuario')
GO
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Contrasena], [Email], [userType]) VALUES (20, N'carmen', N'clave6', N'carmen@gmail.com', N'administrador')
GO
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Contrasena], [Email], [userType]) VALUES (21, N'd', N'1234', N'd@gmail.com', N'usuario')
GO
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
SET IDENTITY_INSERT [dbo].[Ventas] ON 
GO
INSERT [dbo].[Ventas] ([VentaID], [UsuarioID], [FechaVenta], [TotalVenta]) VALUES (5, 13, CAST(N'2023-12-08T18:10:25.2166667' AS DateTime2), CAST(598.85 AS Decimal(10, 2)))
GO
INSERT [dbo].[Ventas] ([VentaID], [UsuarioID], [FechaVenta], [TotalVenta]) VALUES (6, 19, CAST(N'2023-12-09T21:00:23.0466667' AS DateTime2), CAST(200.00 AS Decimal(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[Ventas] OFF
GO
ALTER TABLE [dbo].[DetallesVenta] ADD  CONSTRAINT [DF_DetallesVenta_Cantidad]  DEFAULT ((0)) FOR [Cantidad]
GO
ALTER TABLE [dbo].[DetallesVenta] ADD  CONSTRAINT [DF_DetallesVenta_PrecioUnitario]  DEFAULT ((0)) FOR [PrecioUnitario]
GO
ALTER TABLE [dbo].[DetallesVenta] ADD  CONSTRAINT [DF_DetallesVenta_PrecioTotal]  DEFAULT ((0)) FOR [PrecioTotal]
GO
ALTER TABLE [dbo].[Productos] ADD  CONSTRAINT [DF_Productos_Precio]  DEFAULT ((0)) FOR [Precio]
GO
ALTER TABLE [dbo].[Ventas] ADD  CONSTRAINT [DF_Ventas_TotalVenta]  DEFAULT ((0)) FOR [TotalVenta]
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK_Carrito_Usuarios] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK_Carrito_Usuarios]
GO
ALTER TABLE [dbo].[DetallesCarrito]  WITH CHECK ADD  CONSTRAINT [FK_DetallesCarrito_Carrito] FOREIGN KEY([CarritoID])
REFERENCES [dbo].[Carrito] ([CarritoID])
GO
ALTER TABLE [dbo].[DetallesCarrito] CHECK CONSTRAINT [FK_DetallesCarrito_Carrito]
GO
ALTER TABLE [dbo].[DetallesCarrito]  WITH CHECK ADD  CONSTRAINT [FK_DetallesCarrito_Productos] FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Productos] ([ProductoID])
GO
ALTER TABLE [dbo].[DetallesCarrito] CHECK CONSTRAINT [FK_DetallesCarrito_Productos]
GO
ALTER TABLE [dbo].[DetallesVenta]  WITH CHECK ADD  CONSTRAINT [FK__DetallesV__Produ__3F466844] FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Productos] ([ProductoID])
GO
ALTER TABLE [dbo].[DetallesVenta] CHECK CONSTRAINT [FK__DetallesV__Produ__3F466844]
GO
ALTER TABLE [dbo].[DetallesVenta]  WITH CHECK ADD  CONSTRAINT [FK__DetallesV__Venta__3E52440B] FOREIGN KEY([VentaID])
REFERENCES [dbo].[Ventas] ([VentaID])
GO
ALTER TABLE [dbo].[DetallesVenta] CHECK CONSTRAINT [FK__DetallesV__Venta__3E52440B]
GO
ALTER TABLE [dbo].[Ventas]  WITH CHECK ADD  CONSTRAINT [FK_Ventas_Usuarios] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[Ventas] CHECK CONSTRAINT [FK_Ventas_Usuarios]
GO

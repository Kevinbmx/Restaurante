USE [master]
GO
/****** Object:  Database [FoodGood]    Script Date: 06/20/2017 00:17:53 ******/
CREATE DATABASE [FoodGood] ON  PRIMARY 
( NAME = N'FoodGood', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\FoodGood.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FoodGood_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\FoodGood_0.ldf' , SIZE = 4224KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FoodGood] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FoodGood].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FoodGood] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [FoodGood] SET ANSI_NULLS OFF
GO
ALTER DATABASE [FoodGood] SET ANSI_PADDING OFF
GO
ALTER DATABASE [FoodGood] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [FoodGood] SET ARITHABORT OFF
GO
ALTER DATABASE [FoodGood] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [FoodGood] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [FoodGood] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [FoodGood] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [FoodGood] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [FoodGood] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [FoodGood] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [FoodGood] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [FoodGood] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [FoodGood] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [FoodGood] SET  DISABLE_BROKER
GO
ALTER DATABASE [FoodGood] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [FoodGood] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [FoodGood] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [FoodGood] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [FoodGood] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [FoodGood] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [FoodGood] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [FoodGood] SET  READ_WRITE
GO
ALTER DATABASE [FoodGood] SET RECOVERY FULL
GO
ALTER DATABASE [FoodGood] SET  MULTI_USER
GO
ALTER DATABASE [FoodGood] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [FoodGood] SET DB_CHAINING OFF
GO
USE [FoodGood]
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetAccesoForSearch]    Script Date: 06/20/2017 00:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 24-04-2017
-- Description:	seleccionar por busqueda de acceso
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetAccesoForSearch]
	@whereSql		 AS VARCHAR(4000)
AS
BEGIN
DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'select	 ac.[usuarioId],
							ac.[moduloId]
					from [dbo].[tbl_SEG_accesos] ac
					join [dbo].[tbl_SEG_modulos] m on ac.moduloId = m.moduloId
					join [dbo].[tbl_SEG_areas] a on a.areaID = m.areaId	
				    WHERE ' + @whereSql + ' order by m.descripcion asc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_PRO_SearchProductos]    Script Date: 06/20/2017 00:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 04/06/2017
-- Description:	buscador de productos de acuerdo al titulo
-- =============================================
CREATE PROCEDURE [dbo].[usp_PRO_SearchProductos]
	@varWhere		NVARCHAR(MAX),
	@intPageSize	INT,
	@intFirstRow	INT,
	@TotalRows		INT OUTPUT,
	@ordenar		NVARCHAR(100)
AS
BEGIN
	IF @varWhere IS NULL OR @varWhere=''
		SET @varWhere = '1=1'
		
	declare @lastRow int

CREATE TABLE ##tablaProducto(
		[productoId] int NOT NULL,
		[nombre] nvarchar(150) null,
		[descripcion] nvarchar(max) null,
		[unidadMedidaId] nvarchar(50),
		[precio]numeric(18,2) null,
		[stock]int null,
		[familiaId] int null,
		[ImagenId] int null,
		[RowNumber] int NOT NULL IDENTITY(1, 1))
		

DECLARE @varquery NVARCHAR(MAX)

SELECT @varquery =' INSERT INTO ##tablaProducto([productoId], 
											[nombre],
											[descripcion],
											[unidadMedidaId], 
											[precio],
											[stock],
											[familiaId],
											[ImagenId])

		select				p.[productoId],
							p.[nombre],
							p.[descripcion],
							p.[unidadMedidaId],
							p.[precio],
							p.[stock],
							p.[familiaId],
							[dbo].[svf_GetImangePortadaProducto](p.[productoId]) [ImageId]
					FROM [dbo].[tbl_INV_producto] p
					join [dbo].[tbl_INV_unidadMedida]u on u.unidadMedidaId = p.unidadMedidaId
					join [dbo].[tbl_INV_familia] fa on fa.[familiaId]= p.[familiaId]
		WHERE '+ @varWhere +
		@ordenar

		print @varquery

		EXEC (@varquery)

		--select * from ##tablaProducto

		SELECT @TotalRows = COUNT(*) FROM ##tablaProducto

		SELECT @intFirstRow = @intFirstRow + 1
		SELECT @lastRow = @intFirstRow + @intPageSize
		
		if (@intFirstRow >0)
			SELECT @lastRow = @intFirstRow + @intPageSize-1
		--SELECT @intFirstRow, @intPageSize

		SELECT  [productoId], 
				[nombre],
				[descripcion],
				[unidadMedidaId], 
				[precio],
				[stock],
				[familiaId],
				[ImagenId]
		FROM ##tablaProducto
		WHERE [RowNumber] BETWEEN @intFirstRow AND @lastRow -- + @ordenar --(@+1) AND (@intFirstRow+ @intPageSize) 

		drop table ##tablaProducto
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetTipoUsuarioForSearch]    Script Date: 06/20/2017 00:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 26-04-2017
-- Description:	seleccionar por busqueda de tipoUsuario
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetTipoUsuarioForSearch]
	@whereSql		 AS VARCHAR(4000)
AS
BEGIN
DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'SELECT  tu.[tipoUsuarioId],
							tu.[descripcion]
					FROM [dbo].[tbl_SEG_tipoUsuario] tu
				    WHERE ' + @whereSql + ' order by  tu.[descripcion] asc'
	EXEC(@sqlQuery)





END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetModuloForSearch]    Script Date: 06/20/2017 00:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 24-04-2017
-- Description:	obtener la lista de modulos por una busqueda
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetModuloForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'select m.[moduloId],
							m.[areaId],
							m.[descripcion]
					from [dbo].[tbl_SEG_modulos] m
					join [dbo].[tbl_SEG_areas] a on a.[areaID] = m.[areaId]
				    WHERE ' + @whereSql + ' order by m.[descripcion] asc'
	EXEC(@sqlQuery)

END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetAreaJoinModuloForSearch]    Script Date: 06/20/2017 00:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgaidillo Salazar
-- Create date: 15-05-2017
-- Description:	buscar las areas que estan asignadas al modulo
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetAreaJoinModuloForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
	DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'select DISTINCT a.[areaID],
							a.[descripcion]
					from [dbo].[tbl_SEG_areas] a
					JOIN [dbo].[tbl_SEG_modulos] m ON a.areaID = m.areaId
					WHERE ' + @whereSql + ' order by a.[descripcion] asc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetAreaForSearch]    Script Date: 06/20/2017 00:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 24-04-2017
-- Description:	seleccionar por busqueda
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetAreaForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
	DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'select a.[areaID],
							a.[descripcion]
					from [dbo].[tbl_SEG_areas] a
					WHERE ' + @whereSql + ' order by a.[descripcion] asc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_PED_GetPedidoForSearch]    Script Date: 06/20/2017 00:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 17-06-2017
-- Description:	obtener pedido por query
-- =============================================
CREATE PROCEDURE [dbo].[usp_PED_GetPedidoForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
	DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'SELECT p.[pedidoId],
							p.[usuarioId],
							p.[departamentoId],
							p.[direccion],
							p.[nombreCliente],
							p.[apellidoCliente],
							p.[nit],
							p.[fechaPedido],
							p.[fechaEntrega],
							p.[observacionEntrega],
							p.[observacionEntrega],
							p.[carritoId],
							p.[tipoPagoId],
							p.[ventaId],
							p.[montoTotal],
							p.[latitud],
							p.[longitud]
					FROM tbl_PED_pedido p
				    WHERE ' + @whereSql + ' order by p.[fechaPedido] desc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_SearchProducto]    Script Date: 06/20/2017 00:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_SearchProducto]
	@varWhere		NVARCHAR(MAX),
	@intPageSize	INT,
	@intFirstRow	INT,
	@TotalRows		INT OUTPUT,
	@ordenar		NVARCHAR(100)
AS
BEGIN
	IF @varWhere IS NULL OR @varWhere=''
		SET @varWhere = '1=1'
		
	declare @lastRow int

CREATE TABLE #tablaProducto(
		[productoId] int NOT NULL,
		[nombre] nvarchar(250) NOT NULL,
		[descripcion] nvarchar(max) NOT NULL,
		[unidadMedidaId] nvarchar(50) NOT NULL,
		[precio] decimal(18,2) NOT NULL,
		[stock] int NOT NULL,
		[familiaId] int NOT NULL,
		[RowNumber] int NOT NULL IDENTITY(1, 1))
		

DECLARE @varquery NVARCHAR(MAX)

SELECT @varquery =' INSERT INTO #tablaProducto(productoId, 
											nombre,
											descripcion,
											unidadMedidaId,
											precio,
											stock, 
											familiaId)

		select p.[productoId],
							p.[nombre],
							p.[descripcion],
							p.[unidadMedidaId],
							p.[precio],
							p.[stock],
							p.[familiaId]
					FROM [dbo].[tbl_INV_producto] p
					join [dbo].[tbl_INV_unidadMedida]u on u.unidadMedidaId = p.unidadMedidaId
					join [dbo].[tbl_INV_familia] fa on fa.[familiaId]= p.[familiaId]
		WHERE '+ @varWhere +
		@ordenar

		print @varquery

		EXEC (@varquery)

		--select * from #tablaProducto

		SELECT @TotalRows = COUNT(*) FROM #tablaProducto

		SELECT @intFirstRow = @intFirstRow + 1
		SELECT @lastRow = @intFirstRow + @intPageSize
		
		if (@intFirstRow >0)
			SELECT @lastRow = @intFirstRow + @intPageSize-1
		--SELECT @intFirstRow, @intPageSize

		SELECT  productoId,
				nombre,
				descripcion,
				unidadMedidaId ,
				precio,
				stock ,
				familiaId 
		FROM #tablaProducto
		WHERE [RowNumber] BETWEEN @intFirstRow AND @lastRow -- + @ordenar --(@+1) AND (@intFirstRow+ @intPageSize) 

		drop table #tablaProducto
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_GetProductoForSearch]    Script Date: 06/20/2017 00:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 10-05-2017
-- Description:	seleccionar Productos por busqueda
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_GetProductoForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
	DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'select p.[productoId],
							p.[nombre],
							p.[descripcion],
							p.[unidadMedidaId],
							p.[precio],
							p.[stock],
							p.[familiaId],
							[dbo].[svf_GetImangePortadaProducto](p.[productoId]) [ImageId]
					FROM [dbo].[tbl_INV_producto] p
					join [dbo].[tbl_INV_unidadMedida]u on u.unidadMedidaId = p.unidadMedidaId
					join [dbo].[tbl_INV_familia] fa on fa.[familiaId]= p.[familiaId]
				    WHERE ' + @whereSql + ' order by p.[nombre] asc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_GetUnidadMedidaForSearch]    Script Date: 06/20/2017 00:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 26-04-2017
-- Description:	seleccionar unidad medida por busqueda
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_GetUnidadMedidaForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
	DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'select um.[unidadMedidaId],
							um.[descripcion]
					FROM [dbo].[tbl_INV_unidadMedida] um
				    WHERE ' + @whereSql + ' order by um.[descripcion] asc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  Table [dbo].[tbl_venta]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_venta](
	[ventaId] [int] IDENTITY(1,1) NOT NULL,
	[nombreCliente] [nvarchar](300) NULL,
	[apellidoCliente] [nvarchar](300) NULL,
	[nit] [int] NOT NULL,
	[montoTotal] [decimal](18, 18) NOT NULL,
	[pagoTotal] [decimal](18, 2) NULL,
	[montoCambio] [decimal](18, 2) NOT NULL,
	[montoDescuento] [decimal](18, 18) NULL,
	[fechaPedido] [datetime] NULL,
	[fechaEntrega] [datetime] NULL,
	[fechaAnulacion] [datetime] NULL,
	[estado] [nvarchar](50) NULL,
	[latitud] [decimal](18, 8) NULL,
	[longitud] [decimal](18, 8) NULL,
 CONSTRAINT [PK_tbl_venta] PRIMARY KEY CLUSTERED 
(
	[ventaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_tipoPago]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_tipoPago](
	[tipoPagoId] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_tbl_tipoPago] PRIMARY KEY CLUSTERED 
(
	[tipoPagoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_tipoPago] ON
INSERT [dbo].[tbl_tipoPago] ([tipoPagoId], [descripcion]) VALUES (1, N'contado')
INSERT [dbo].[tbl_tipoPago] ([tipoPagoId], [descripcion]) VALUES (2, N'contra entrega')
SET IDENTITY_INSERT [dbo].[tbl_tipoPago] OFF
/****** Object:  Table [dbo].[tbl_tipoCambio]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_tipoCambio](
	[tipoCambioId] [int] NOT NULL,
	[fechaTipoCambio] [datetime] NULL,
	[tipoCambio] [numeric](18, 2) NULL,
 CONSTRAINT [PK_tbl_tipoCambio] PRIMARY KEY CLUSTERED 
(
	[tipoCambioId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_CAR_GetCarritoForSearch]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 05-06-2017
-- Description:	obtener carrito por query
-- =============================================
CREATE PROCEDURE [dbo].[usp_CAR_GetCarritoForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
	DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'select c[carritoId],
							c[usuarioId],
							c.[contenido],
							c.[fecha]
							c.[estadoVenta]
					from  dbo.tbl_carrito c
				    WHERE ' + @whereSql + ' order by c.[fecha] asc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  Table [dbo].[tbl_accesoAreasJSON]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_accesoAreasJSON](
	[accesoAreaJsonId] [int] NOT NULL,
	[NombreArea] [nvarchar](150) NOT NULL,
	[nombreModulos] [nvarchar](150) NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_FILE_SearchImagenes]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 31/05/2017
-- Description:	buscador de imagenes de acuerdo al titulo
-- =============================================
CREATE PROCEDURE [dbo].[usp_FILE_SearchImagenes]
	@varWhere		NVARCHAR(MAX),
	@intPageSize	INT,
	@intFirstRow	INT,
	@TotalRows		INT OUTPUT,
	@ordenar		NVARCHAR(100)
AS
BEGIN
	IF @varWhere IS NULL OR @varWhere=''
		SET @varWhere = '1=1'
		
	declare @lastRow int

CREATE TABLE #tablaImagen(
		[imagenId] int NOT NULL,
		[titulo] nvarchar(250) NOT NULL,
		[size] bigint NOT NULL,
		[extension] varchar(50) NOT NULL,
		[directorio] nvarchar(max) NOT NULL,
		[fechaImagen] datetime NOT NULL,
		[RowNumber] int NOT NULL IDENTITY(1, 1))
		

DECLARE @varquery NVARCHAR(MAX)

SELECT @varquery =' INSERT INTO #tablaImagen(imagenId, 
											titulo,
											size,
											extension, 
											directorio,
											fechaImagen)

		SELECT i.[imagenId]
				,i.[titulo]
				,i.[size]
				,i.[extension]
				,i.[directorio]
				,i.[fechaImagen]
		FROM [FoodGood].[dbo].[tbl_INV_Imagen] i
		WHERE '+ @varWhere +
		@ordenar

		print @varquery

		EXEC (@varquery)

		--select * from #tablaImagen

		SELECT @TotalRows = COUNT(*) FROM #tablaImagen

		SELECT @intFirstRow = @intFirstRow + 1
		SELECT @lastRow = @intFirstRow + @intPageSize
		
		if (@intFirstRow >0)
			SELECT @lastRow = @intFirstRow + @intPageSize-1
		--SELECT @intFirstRow, @intPageSize

		SELECT  [imagenId]
				,[titulo]
				,[fechaImagen]
				,[size]
				,[extension]
				,[directorio]
		FROM #tablaImagen
		WHERE [RowNumber] BETWEEN @intFirstRow AND @lastRow -- + @ordenar --(@+1) AND (@intFirstRow+ @intPageSize) 

		drop table #tablaImagen
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_GetFamiliaForSearch]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 08-05-2017
-- Description:	seleccionar Familia por busqueda
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_GetFamiliaForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
	DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'select fa.[familiaId],
							fa.[descripcion],
							fa.[imagenId]
					from tbl_INV_familia fa
				    WHERE ' + @whereSql + ' order by fa.[descripcion] asc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_GetImagenForSearch]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 01-06-2017
-- Description:	buscador de imagenes
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_GetImagenForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
	DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'SELECT i.[ImagenId]
					  ,i.[titulo]
					  ,i.[size]
					  ,i.[extension]
					  ,i.[directorio]
					  ,i.[fechaImagen]
					FROM [FoodGood].[dbo].[tbl_INV_Imagen] i
				    WHERE ' + @whereSql + ' order by i.[titulo] asc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  Table [dbo].[tbl_dosificacion]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_dosificacion](
	[dosificacionId] [int] IDENTITY(1,1) NOT NULL,
	[desde] [int] NOT NULL,
	[hasta] [int] NOT NULL,
	[numeroAutorizacion] [nvarchar](1000) NOT NULL,
	[llaveDosificacion] [nvarchar](max) NOT NULL,
	[fechaInicio] [date] NOT NULL,
	[fechaFinal] [date] NOT NULL,
	[facturaActual] [int] NOT NULL,
	[nit] [int] NOT NULL,
	[glosa] [nvarchar](max) NOT NULL,
	[cboEstado] [int] NOT NULL,
 CONSTRAINT [PK_tbl_dosificacion] PRIMARY KEY CLUSTERED 
(
	[dosificacionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_dosificacion] ON
INSERT [dbo].[tbl_dosificacion] ([dosificacionId], [desde], [hasta], [numeroAutorizacion], [llaveDosificacion], [fechaInicio], [fechaFinal], [facturaActual], [nit], [glosa], [cboEstado]) VALUES (1, 0, 100, N'263401600021783', N'#%7s*ugvK@GFsAsa_yW2Dc4kF%xjVK*_@DSKJ8JVqQI}vdNIN=ahsTz3{+MF}RmK|38151|3004004520427#%7s40334530*ugvK@GFs746807401692As2007052955a_yW2D3382761c4|21302368|1HGnW|4E-62-66-62-65|', CAST(0xEB3C0B00 AS Date), CAST(0xF53C0B00 AS Date), 51, 3832311, N'venta al por mayor de una variedad de productos que no reflejen una especialidad(supermercados, distribuido).', 1)
INSERT [dbo].[tbl_dosificacion] ([dosificacionId], [desde], [hasta], [numeroAutorizacion], [llaveDosificacion], [fechaInicio], [fechaFinal], [facturaActual], [nit], [glosa], [cboEstado]) VALUES (2, 100, 200, N'154546778', N'sdfasdfasdfasdfasdfasdfsaf54as65df4s65f4as', CAST(0xEC3C0B00 AS Date), CAST(0xFD3C0B00 AS Date), 60, 5456412, N'asdfasdfasfsafsdf', 0)
INSERT [dbo].[tbl_dosificacion] ([dosificacionId], [desde], [hasta], [numeroAutorizacion], [llaveDosificacion], [fechaInicio], [fechaFinal], [facturaActual], [nit], [glosa], [cboEstado]) VALUES (3, 200, 300, N'564984654', N'a56d4f6a5sd4f98a7sdf65asf65a4sdf65as4fd654sf87af5v4', CAST(0xEF3C0B00 AS Date), CAST(0x033D0B00 AS Date), 100, 5421231, N'afasfsdafasf', 0)
INSERT [dbo].[tbl_dosificacion] ([dosificacionId], [desde], [hasta], [numeroAutorizacion], [llaveDosificacion], [fechaInicio], [fechaFinal], [facturaActual], [nit], [glosa], [cboEstado]) VALUES (4, 300, 400, N'54654654', N'adf4a6ds54f6a5sd4f65as1df32ad1fa3', CAST(0xE03C0B00 AS Date), CAST(0xFE3C0B00 AS Date), 200, 21651626, N'asdf5asdf5as4df65asd6f2s', 0)
SET IDENTITY_INSERT [dbo].[tbl_dosificacion] OFF
/****** Object:  Table [dbo].[tbl_departamento]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_departamento](
	[departamentoId] [int] IDENTITY(1,1) NOT NULL,
	[nombreDepartamento] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_tbl_departamento] PRIMARY KEY CLUSTERED 
(
	[departamentoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_DatabaseInfo]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_DatabaseInfo](
	[majorversion] [smallint] NOT NULL,
	[minorversion] [smallint] NOT NULL,
	[releaseversion] [smallint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_INV_unidadMedida]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_INV_unidadMedida](
	[unidadMedidaId] [nvarchar](50) NOT NULL,
	[descripcion] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_tbl_unidadMedida] PRIMARY KEY CLUSTERED 
(
	[unidadMedidaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_INV_unidadMedida] ([unidadMedidaId], [descripcion]) VALUES (N'plt', N'plato')
INSERT [dbo].[tbl_INV_unidadMedida] ([unidadMedidaId], [descripcion]) VALUES (N'pza', N'pieza')
INSERT [dbo].[tbl_INV_unidadMedida] ([unidadMedidaId], [descripcion]) VALUES (N'und', N'unidad')
/****** Object:  Table [dbo].[tbl_SEG_areas]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SEG_areas](
	[areaID] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](250) NULL,
 CONSTRAINT [PK_tbl_areas] PRIMARY KEY CLUSTERED 
(
	[areaID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_SEG_areas] ON
INSERT [dbo].[tbl_SEG_areas] ([areaID], [descripcion]) VALUES (2003, N'tipo usuario')
INSERT [dbo].[tbl_SEG_areas] ([areaID], [descripcion]) VALUES (2004, N'cliente')
INSERT [dbo].[tbl_SEG_areas] ([areaID], [descripcion]) VALUES (2005, N'usuario')
INSERT [dbo].[tbl_SEG_areas] ([areaID], [descripcion]) VALUES (2006, N'area')
INSERT [dbo].[tbl_SEG_areas] ([areaID], [descripcion]) VALUES (2007, N'modulo')
INSERT [dbo].[tbl_SEG_areas] ([areaID], [descripcion]) VALUES (2008, N'accesos')
INSERT [dbo].[tbl_SEG_areas] ([areaID], [descripcion]) VALUES (2009, N'unidad medida')
INSERT [dbo].[tbl_SEG_areas] ([areaID], [descripcion]) VALUES (2010, N'tipo caracteristica')
INSERT [dbo].[tbl_SEG_areas] ([areaID], [descripcion]) VALUES (2011, N'lista producto')
INSERT [dbo].[tbl_SEG_areas] ([areaID], [descripcion]) VALUES (2012, N'imagen')
SET IDENTITY_INSERT [dbo].[tbl_SEG_areas] OFF
/****** Object:  Table [dbo].[tbl_INV_Imagen]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_INV_Imagen](
	[imagenId] [int] IDENTITY(1,1) NOT NULL,
	[titulo] [nvarchar](250) NOT NULL,
	[size] [bigint] NOT NULL,
	[extension] [varchar](50) NOT NULL,
	[directorio] [nvarchar](max) NOT NULL,
	[fechaImagen] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_INV_Imagen] PRIMARY KEY CLUSTERED 
(
	[imagenId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_INV_Imagen] ON
INSERT [dbo].[tbl_INV_Imagen] ([imagenId], [titulo], [size], [extension], [directorio], [fechaImagen]) VALUES (17, N'Lasagna.jpg', 452297, N'.jpg', N'D:\Kevin\Universidad\taller\Restaurante\FoodGood\img\ImgRestaurante\Lasagna.jpg', CAST(0x0000A7880112153F AS DateTime))
INSERT [dbo].[tbl_INV_Imagen] ([imagenId], [titulo], [size], [extension], [directorio], [fechaImagen]) VALUES (18, N'pizza.jpg', 92039, N'.jpg', N'D:\Kevin\Universidad\taller\Restaurante\FoodGood\img\ImgRestaurante\pizza.jpg', CAST(0x0000A78801127736 AS DateTime))
INSERT [dbo].[tbl_INV_Imagen] ([imagenId], [titulo], [size], [extension], [directorio], [fechaImagen]) VALUES (19, N'bebidas.jpg', 228017, N'.jpg', N'D:\Kevin\Universidad\taller\Restaurante\FoodGood\img\ImgRestaurante\bebidas.jpg', CAST(0x0000A78A00EFAB99 AS DateTime))
INSERT [dbo].[tbl_INV_Imagen] ([imagenId], [titulo], [size], [extension], [directorio], [fechaImagen]) VALUES (20, N'adornos1.jpg', 317245, N'.jpg', N'D:\Kevin\Universidad\taller\Restaurante\FoodGood\img\ImgRestaurante\adornos1.jpg', CAST(0x0000A78A00EFF4F8 AS DateTime))
INSERT [dbo].[tbl_INV_Imagen] ([imagenId], [titulo], [size], [extension], [directorio], [fechaImagen]) VALUES (21, N'postre.jpg', 600637, N'.jpg', N'D:\Kevin\Universidad\taller\Restaurante\FoodGood\img\ImgRestaurante\postre.jpg', CAST(0x0000A78A00F02E3B AS DateTime))
INSERT [dbo].[tbl_INV_Imagen] ([imagenId], [titulo], [size], [extension], [directorio], [fechaImagen]) VALUES (22, N'parrillada.jpg', 87913, N'.jpg', N'D:\Kevin\Universidad\taller\Restaurante\FoodGood\img\ImgRestaurante\parrillada.jpg', CAST(0x0000A78B00C1F424 AS DateTime))
INSERT [dbo].[tbl_INV_Imagen] ([imagenId], [titulo], [size], [extension], [directorio], [fechaImagen]) VALUES (23, N'parrillada4persona.jpg', 2424652, N'.jpg', N'D:\Kevin\Universidad\taller\Restaurante\FoodGood\img\ImgRestaurante\parrillada4persona.jpg', CAST(0x0000A78B00C2AD69 AS DateTime))
INSERT [dbo].[tbl_INV_Imagen] ([imagenId], [titulo], [size], [extension], [directorio], [fechaImagen]) VALUES (24, N'BifeChorizo.jpg', 123047, N'.jpg', N'D:\Kevin\Universidad\taller\Restaurante\FoodGood\img\ImgRestaurante\BifeChorizo.jpg', CAST(0x0000A78B00C49F63 AS DateTime))
INSERT [dbo].[tbl_INV_Imagen] ([imagenId], [titulo], [size], [extension], [directorio], [fechaImagen]) VALUES (25, N'souffleChocolate.jpg', 26114, N'.jpg', N'D:\Kevin\Universidad\taller\Restaurante\FoodGood\img\ImgRestaurante\souffleChocolate.jpg', CAST(0x0000A78B00C7F77E AS DateTime))
INSERT [dbo].[tbl_INV_Imagen] ([imagenId], [titulo], [size], [extension], [directorio], [fechaImagen]) VALUES (26, N'VinoKohlberg.jpg', 50957, N'.jpg', N'D:\Kevin\Universidad\taller\Restaurante\FoodGood\img\ImgRestaurante\VinoKohlberg.jpg', CAST(0x0000A78B00CF0EEF AS DateTime))
SET IDENTITY_INSERT [dbo].[tbl_INV_Imagen] OFF
/****** Object:  Table [dbo].[tbl_INV_familia]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_INV_familia](
	[familiaId] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](250) NULL,
	[imagenId] [int] NULL,
 CONSTRAINT [PK_tbl_familia] PRIMARY KEY CLUSTERED 
(
	[familiaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_INV_familia] ON
INSERT [dbo].[tbl_INV_familia] ([familiaId], [descripcion], [imagenId]) VALUES (1, N'bebida', 19)
INSERT [dbo].[tbl_INV_familia] ([familiaId], [descripcion], [imagenId]) VALUES (4, N'comida', 20)
INSERT [dbo].[tbl_INV_familia] ([familiaId], [descripcion], [imagenId]) VALUES (5, N'postre', 21)
INSERT [dbo].[tbl_INV_familia] ([familiaId], [descripcion], [imagenId]) VALUES (8, N'parrillada', 22)
SET IDENTITY_INSERT [dbo].[tbl_INV_familia] OFF
/****** Object:  Table [dbo].[tbl_SEG_tipoUsuario]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SEG_tipoUsuario](
	[tipoUsuarioId] [int] NOT NULL,
	[descripcion] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_tbl_tipoUsuarioId] PRIMARY KEY CLUSTERED 
(
	[tipoUsuarioId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_SEG_tipoUsuario] ([tipoUsuarioId], [descripcion]) VALUES (1, N'administrador')
INSERT [dbo].[tbl_SEG_tipoUsuario] ([tipoUsuarioId], [descripcion]) VALUES (2, N'cliente')
INSERT [dbo].[tbl_SEG_tipoUsuario] ([tipoUsuarioId], [descripcion]) VALUES (3, N'visita')
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetUsersForSearch]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 19-04-2017
-- Description:	obtener la lista de usuarios
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetUsersForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
	
	DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'select u.[usuarioId],
		u.[Nombre],
		u.[apellido],
		u.[password],
		u.[tipoUsuarioId],
		u.[email],
		u.[celular1],
		u.[celular2],
		u.[nit],
		tu.[descripcion]
		from [dbo].[tbl_SEG_usuario]  u
		JOIN [dbo].[tbl_SEG_tipoUsuario] tu on u.[tipoUsuarioId] = tu.[tipoUsuarioId]
	  WHERE ' + @whereSql + ' order by u.[Nombre] asc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UBI_GetUbicacionEnvioForSearch]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 16-06-2017
-- Description:	Obtener ubicacion por busqueda
-- =============================================
CREATE PROCEDURE[dbo].[usp_UBI_GetUbicacionEnvioForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
	DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'select u.ubicacionId,
							u.usuarioId
							u.descripcion,
							u.latitud,
							u.longitud
					FROM tbl_ubicacionDeEnvio u
				    WHERE ' + @whereSql + ' order by u.[descripcion] desc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_VENTA_GetDosificacionForSearch]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 10-06-2017
-- Description:	obtener dosificacion por busquesa
-- =============================================
CREATE PROCEDURE [dbo].[usp_VENTA_GetDosificacionForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
	DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'select d.[dosificacionId] ,
						   d.[desde] ,
						   d.[hasta],
						   d.[numeroAutorizacion] ,
						   d.[llaveDosificacion] ,
						   d.[fechaInicio] ,
						   d.[fechaFinal],
						   d.[facturaActual],
						   d.[nit],
						   d.[glosa],
						   d.[cboEstado]
					FROM [dbo].[tbl_dosificacion] d
				    WHERE ' + @whereSql + ' order by d.[dosificacionId] asc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_VENTA_GetVentaForSearch]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 18-06-2017
-- Description: obtener venta por busqueda
-- =============================================
CREATE PROCEDURE [dbo].[usp_VENTA_GetVentaForSearch]
	@whereSql AS VARCHAR(4000)
AS
BEGIN
	DECLARE @sqlQuery nvarchar(4000)
	SET @sqlQuery = 'SELECT v.[ventaId],
							v.[nombreCliente] ,
							v.[apellidoCliente] ,
							v.[nit],
							v.[montoTotal] ,
							v.[pagoTotal] ,
							v.[montoCambio] ,
							v.[montoDescuento] ,
							v.[fechaPedido] ,
							v.[fechaEntrega] ,
							v.[fechaAnulacion] ,
							v.[estado] ,
							v.[latitud] ,
							v.[longitud]
					FROM tbl_venta  v
				    WHERE ' + @whereSql + ' order by v.[ventaId] asc'
	EXEC(@sqlQuery)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_VENTA_GetVentaById]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 17-06-2017
-- Description:	seleccionar venta por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_VENTA_GetVentaById]
	@intVentaId			int
AS
BEGIN
	SELECT [ventaId],
		[nombreCliente] ,
		[apellidoCliente] ,
		[nit],
		[montoTotal] ,
		[pagoTotal] ,
		[montoCambio] ,
		[montoDescuento] ,
		[fechaPedido] ,
		[fechaEntrega] ,
		[fechaAnulacion] ,
		[estado] ,
		[latitud] ,
		[longitud]
FROM tbl_venta 
  where [ventaId] = @intVentaId 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_VENTA_UpdateVenta]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 18-06-2017
-- Description:	actualizar venta
-- =============================================
CREATE PROCEDURE [dbo].[usp_VENTA_UpdateVenta]
	@varNombreCliente			nvarchar(300),
	@varApellidoCliente			nvarchar(300),
	@intNit						int,
	@intMontoTotal				decimal(18,2),
	@intPagoTotal				decimal(18,2),
	@intMontoCambio				decimal(18,2),
	@intMontoDescuento			decimal(18,2),
	@dateFechaPedido			date,
	@dateFechaEntrega			date,
	@dateFechaAnulacion 		date,
	@varEstado					nvarchar(50),
	@decimalLatitud				decimal(18,8),
	@decimalLongitud			decimal(18,8),
	@intVentaId					int
AS
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
UPDATE [dbo].[tbl_venta]  
	   SET  [nombreCliente] = @varNombreCliente  ,
				[apellidoCliente] = @varApellidoCliente ,
				[nit]=@intNit,
				[montoTotal]=@intMontoTotal ,
				[pagoTotal]= @intPagoTotal ,
				[montoCambio] = @intMontoCambio,
				[montoDescuento] = @intMontoDescuento,
				[fechaPedido] = @dateFechaPedido,
				[fechaEntrega] = @dateFechaEntrega ,
				[fechaAnulacion] = @dateFechaAnulacion,
				[estado] = @varEstado,
				[latitud] = @decimalLatitud,
				[longitud] = @decimalLongitud
		WHERE [ventaId] = @intVentaId  
END
GO
/****** Object:  StoredProcedure [dbo].[usp_VENTA_UpdateDosificacion]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 10-06-2017
-- Description:	update Dosificacion
-- =============================================
CREATE PROCEDURE [dbo].[usp_VENTA_UpdateDosificacion]
	@intDesde					int,
	@inthasta					int,
	@varNumeroAutorizacion		nvarchar(max),
	@varLlaveDosificacion		nvarchar(max),
	@dateFechaInicio			date,
	@dateFechaFinal				date,
	@intFacturaActual			int,
	@intNit						int,
	@varGlosa					nvarchar(max),
	@intCboEstado				int,
	@intDosificacinId			int
AS
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
UPDATE [dbo].[tbl_dosificacion] 
	   SET  [desde]  = @intDesde,
			[hasta]= @inthasta , 
				 [numeroAutorizacion] =@varNumeroAutorizacion ,
				 [llaveDosificacion] = @varLlaveDosificacion ,
				 [fechaInicio]=@dateFechaInicio , 
				 [fechaFinal] = @dateFechaFinal,
				 [facturaActual] = @intFacturaActual,
				 [nit]= @intNit ,
				 [glosa]= @varGlosa ,
				 [cboEstado]= @intCboEstado 
		WHERE [dosificacionId] = @intDosificacinId 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_VENTA_InsertVenta]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 18-06-2017
-- Description:	Insertar venta 
-- =============================================
CREATE PROCEDURE [dbo].[usp_VENTA_InsertVenta]
	@varNombreCliente			nvarchar(300),
	@varApellidoCliente			nvarchar(300),
	@intNit						int,
	@intMontoTotal				decimal(18,2),
	@intPagoTotal				decimal(18,2),
	@intMontoCambio				decimal(18,2),
	@intMontoDescuento			decimal(18,2),
	@dateFechaPedido			date,
	@dateFechaEntrega			date,
	@dateFechaAnulacion 		date,
	@varEstado					nvarchar(50),
	@decimalLatitud				decimal(18,8),
	@decimalLongitud			decimal(18,8)
	
AS
BEGIN
	insert into [dbo].[tbl_venta] 
				([nombreCliente] ,
				[apellidoCliente] ,
				[nit],
				[montoTotal] ,
				[pagoTotal] ,
				[montoCambio] ,
				[montoDescuento] ,
				[fechaPedido] ,
				[fechaEntrega] ,
				[fechaAnulacion] ,
				[estado] ,
				[latitud] ,
				[longitud]) 
	values(@varNombreCliente,
			@varApellidoCliente,
			@intNit,
			@intMontoTotal,
			@intPagoTotal,
			@intMontoCambio,
			@intMontoDescuento,
			@dateFechaPedido,
			@dateFechaEntrega,
			@dateFechaAnulacion,
			@varEstado,
			@decimalLatitud,
			@decimalLongitud)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_VENTA_InsertDosificacion]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 10-062017
-- Description:	insertar Dosificacion
-- =============================================
CREATE PROCEDURE [dbo].[usp_VENTA_InsertDosificacion]
	@intDesde					int,
	@inthasta					int,
	@varNumeroAutorizacion		nvarchar(max),
	@varLlaveDosificacion		nvarchar(max),
	@dateFechaInicio			date,
	@dateFechaFinal				date,
	@intFacturaActual			int,
	@intNit						int,
	@varGlosa					nvarchar(max),
	@intCboEstado				int
AS
BEGIN
	insert into [dbo].[tbl_dosificacion]
				([desde], 
				 [hasta] , 
				 [numeroAutorizacion] ,
				 [llaveDosificacion],
				 [fechaInicio], 
				 [fechaFinal],
				 [facturaActual],
				 [nit],
				 [glosa],
				 [cboEstado]) 
	values(@intDesde,
			@inthasta,
			@varNumeroAutorizacion,
			@varLlaveDosificacion,
			@dateFechaInicio,
			@dateFechaFinal,
			@intFacturaActual,
			@intNit,
			@varGlosa,
			@intCboEstado)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_VENTA_GetDosificacionById]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 10-06-2017
-- Description:	obtener dosificacion por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_VENTA_GetDosificacionById]
	@intDosificacionId		int
AS
BEGIN
	select [dosificacionId] ,
		   [desde] ,
		   [hasta],
		   [numeroAutorizacion] ,
		   [llaveDosificacion] ,
		   [fechaInicio] ,
		   [fechaFinal],
		   [facturaActual],
		   [nit],
		   [glosa],
		   [cboEstado]
	FROM [dbo].[tbl_dosificacion]
  where [dosificacionId]  = @intDosificacionId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_VENTA_DeleteVenta]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 17-06-2017
-- Description:	eliminar la venta
-- =============================================
CREATE PROCEDURE [dbo].[usp_VENTA_DeleteVenta]
	@intVentaId			int
AS
BEGIN
	DELETE FROM [dbo].[tbl_venta] 
   WHERE [ventaId]  = @intVentaId 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_VENTA_DeleteDosificaion]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 10-06-2017
-- Description:	eliminar dosificacion
-- =============================================
CREATE PROCEDURE [dbo].[usp_VENTA_DeleteDosificaion]
	@intDosificacionId			int
AS
BEGIN
	DELETE FROM [dbo].[tbl_dosificacion]
   WHERE [dosificacionId]  = @intDosificacionId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_InserTipoUsuairo]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 26-04-2017
-- Description:	insertar Tipo Usuario
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_InserTipoUsuairo]
	@varDescripcion		nvarchar(250)
AS
BEGIN
	insert into [dbo].[tbl_SEG_tipoUsuario]
				([descripcion])
	values(	@varDescripcion)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_InsertArea]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 23-04-2017
-- Description:	insertar Area
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_InsertArea]
	@varDescripcion		nvarchar(250)
AS
BEGIN
	insert into [dbo].[tbl_SEG_areas]
				([descripcion])
	values(	@varDescripcion)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_UpdateArea]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 24-04-2017
-- Description:	actualizar las areas
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_UpdateArea]
	@varDescripcion		nvarchar(250),
	@intAreaId			int
AS
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
UPDATE [dbo].[tbl_SEG_areas]
	   SET  [descripcion]= @varDescripcion
		WHERE[areaID] = @intAreaId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_UpdateTipoUsuario]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 24-04-2017
-- Description:	actualizar los tipos de Usuario
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_UpdateTipoUsuario]
	@varDescripcion		nvarchar(250),
	@intTipoUsiaroId			int
AS
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
UPDATE [dbo].[tbl_SEG_tipoUsuario]
	   SET  [descripcion]= @varDescripcion
		WHERE[tipoUsuarioId] = @intTipoUsiaroId
END
GO
/****** Object:  Table [dbo].[tbl_SEG_modulos]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SEG_modulos](
	[moduloId] [int] IDENTITY(1,1) NOT NULL,
	[areaId] [int] NOT NULL,
	[descripcion] [nvarchar](250) NULL,
 CONSTRAINT [PK_tbl_modulos] PRIMARY KEY CLUSTERED 
(
	[moduloId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_SEG_modulos] ON
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2010, 2004, N'crear cliente')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2011, 2004, N'editar cliente')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2012, 2004, N'eliminar cliente')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2013, 2005, N'crear usuario')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2014, 2007, N'crear modulo')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2016, 2004, N'ver cliente')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2018, 2006, N'crear area')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2020, 2008, N'crear acceso')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2021, 2008, N'eliminar acceso')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2022, 2008, N'ver acceso')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2023, 2006, N'eliminar area')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2024, 2006, N'editar area')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2025, 2006, N'ver area')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2026, 2007, N'editar modulo')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2027, 2007, N'eliminar modulo')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2028, 2007, N'ver modulo')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2032, 2005, N'editar usuario')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2033, 2005, N'eliminar usuario')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2034, 2005, N'ver usuario')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2035, 2010, N'crear tipo caracteristica')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2036, 2010, N'editar tipo caracteristica')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2037, 2010, N'eliminar tipo caracteristica')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2038, 2010, N'ver tipo caracteristica')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2039, 2011, N'crear lista producto')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2040, 2011, N'editar lista producto')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2041, 2011, N'eliminar lista producto')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2042, 2011, N'ver lista producto')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2043, 2009, N'ver unidad medida')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2044, 2009, N'editar unidad medida')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2045, 2009, N'eliminar unidad medida')
INSERT [dbo].[tbl_SEG_modulos] ([moduloId], [areaId], [descripcion]) VALUES (2046, 2009, N'crear unidad medida')
SET IDENTITY_INSERT [dbo].[tbl_SEG_modulos] OFF
/****** Object:  Table [dbo].[tbl_factura]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_factura](
	[facturaId] [int] IDENTITY(1,1) NOT NULL,
	[numero] [nvarchar](12) NOT NULL,
	[nombre] [nvarchar](250) NOT NULL,
	[nit] [nvarchar](50) NOT NULL,
	[fecha] [datetime] NOT NULL,
	[fechaLimiteEmision] [datetime] NOT NULL,
	[montoPalabra] [nvarchar](500) NOT NULL,
	[codigoAutorizacion] [nvarchar](15) NOT NULL,
	[codigoControl] [nvarchar](50) NOT NULL,
	[ventaId] [int] NOT NULL,
 CONSTRAINT [PK_tbl_factura] PRIMARY KEY CLUSTERED 
(
	[facturaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_INV_producto]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_INV_producto](
	[productoId] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](150) NULL,
	[descripcion] [nvarchar](max) NULL,
	[unidadMedidaId] [nvarchar](50) NULL,
	[precio] [numeric](18, 2) NULL,
	[stock] [int] NULL,
	[familiaId] [int] NULL,
 CONSTRAINT [PK_tbl_producto] PRIMARY KEY CLUSTERED 
(
	[productoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_INV_producto] ON
INSERT [dbo].[tbl_INV_producto] ([productoId], [nombre], [descripcion], [unidadMedidaId], [precio], [stock], [familiaId]) VALUES (3, N'pizza', N'tiene esto con esto', N'pza', CAST(50.25 AS Numeric(18, 2)), 47, 4)
INSERT [dbo].[tbl_INV_producto] ([productoId], [nombre], [descripcion], [unidadMedidaId], [precio], [stock], [familiaId]) VALUES (4, N'lazagnaq', N'tiene esto con eto', N'pza', CAST(45.00 AS Numeric(18, 2)), 5, 4)
INSERT [dbo].[tbl_INV_producto] ([productoId], [nombre], [descripcion], [unidadMedidaId], [precio], [stock], [familiaId]) VALUES (5, N'parrilada para 4 personas', N'parrillada especial o con interiores, ensaladas sustida, chilena,papas mayo,almendrado.', N'plt', CAST(70.00 AS Numeric(18, 2)), 14, 8)
INSERT [dbo].[tbl_INV_producto] ([productoId], [nombre], [descripcion], [unidadMedidaId], [precio], [stock], [familiaId]) VALUES (6, N'bife de chorizo', N'pedazo de carne, con papas,salsa, ensalada', N'plt', CAST(40.00 AS Numeric(18, 2)), 15, 8)
INSERT [dbo].[tbl_INV_producto] ([productoId], [nombre], [descripcion], [unidadMedidaId], [precio], [stock], [familiaId]) VALUES (7, N'soufflé de chocolate', N'este soufflé de chocolate de chocolate es ligero y de sabor delicioso', N'und', CAST(15.00 AS Numeric(18, 2)), 40, 5)
INSERT [dbo].[tbl_INV_producto] ([productoId], [nombre], [descripcion], [unidadMedidaId], [precio], [stock], [familiaId]) VALUES (8, N'vino kohlberg ', N'puesto nro.- 1 del vino fino tinto cosecha 2014', N'und', CAST(30.00 AS Numeric(18, 2)), 30, 1)
SET IDENTITY_INSERT [dbo].[tbl_INV_producto] OFF
/****** Object:  StoredProcedure [dbo].[usp_INV_GetImagenById]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 30-05-2017
-- Description:	seleccionar imagen por id
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_GetImagenById]
	@intImagenId		int
AS
BEGIN
	SELECT [imagenId] 
      ,[titulo]
      ,[size]
      ,[extension]
      ,[directorio]
      ,[fechaImagen]
  FROM [FoodGood].[dbo].[tbl_INV_Imagen]
  where [ImagenId] = @intImagenId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_DeleteImagen]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 31-05-2017
-- Description:	eliminar Imagen por id
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_DeleteImagen]
	@intImagenId			int
AS
BEGIN
	DELETE FROM [dbo].[tbl_INV_Imagen] 
   WHERE [ImagenId] = @intImagenId 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_DeleteFamilia]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 08-05-2017
-- Description:	eliminar familia por id
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_DeleteFamilia]
	@intFamiliaId			int
AS
BEGIN
	DELETE FROM [dbo].[tbl_INV_familia]
   WHERE [familiaId] = @intFamiliaId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_GetFamiliaById]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 08-05-2017
-- Description:	obtener la lista de tipo de la familia por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_GetFamiliaById]
	@intFamiliaId		int
AS
BEGIN
	select [familiaId],
		   [descripcion],
		   [imagenId]
	FROM [dbo].[tbl_INV_familia]
  where [familiaId] = @intFamiliaId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_DeleteUnidadMedida]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 08-05-2017
-- Description:	eliminar unidad de medida
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_DeleteUnidadMedida]
	@varUnidadMedidaId			nvarchar(50)
AS
BEGIN
	DELETE FROM [dbo].[tbl_INV_unidadMedida]
   WHERE [unidadMedidaId] = @varUnidadMedidaId
END
GO
/****** Object:  Table [dbo].[tbl_SEG_usuario]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SEG_usuario](
	[usuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](250) NOT NULL,
	[apellido] [nvarchar](350) NULL,
	[password] [nvarchar](250) NULL,
	[tipoUsuarioId] [int] NOT NULL,
	[email] [nvarchar](250) NULL,
	[celular1] [nvarchar](50) NULL,
	[celular2] [nvarchar](50) NULL,
	[nit] [bigint] NULL,
 CONSTRAINT [PK_tbl_usuario] PRIMARY KEY CLUSTERED 
(
	[usuarioId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_SEG_usuario] ON
INSERT [dbo].[tbl_SEG_usuario] ([usuarioId], [Nombre], [apellido], [password], [tipoUsuarioId], [email], [celular1], [celular2], [nit]) VALUES (1, N'admin', NULL, N'admin!123', 1, N'admin', NULL, NULL, NULL)
INSERT [dbo].[tbl_SEG_usuario] ([usuarioId], [Nombre], [apellido], [password], [tipoUsuarioId], [email], [celular1], [celular2], [nit]) VALUES (1002, N'juan', N'perez', N'admin!123', 1, N'juanperez@gmail.com', N'75632145', N'', 0)
INSERT [dbo].[tbl_SEG_usuario] ([usuarioId], [Nombre], [apellido], [password], [tipoUsuarioId], [email], [celular1], [celular2], [nit]) VALUES (1003, N'kevin', N'delgadillo', N'admin!123', 1, N'kevi3195@gmail.com', N'75057204', N'', 0)
INSERT [dbo].[tbl_SEG_usuario] ([usuarioId], [Nombre], [apellido], [password], [tipoUsuarioId], [email], [celular1], [celular2], [nit]) VALUES (1009, N'fsadfa', N'dsfa', N'admin!123', 1, N'fasdfad', N'156421', N'', 0)
INSERT [dbo].[tbl_SEG_usuario] ([usuarioId], [Nombre], [apellido], [password], [tipoUsuarioId], [email], [celular1], [celular2], [nit]) VALUES (1010, N'kevin', N'delgadillo', N'admin!123', 3, N'kevi3195+fg@gmail.com', N'75057204', N'', 6256298014)
INSERT [dbo].[tbl_SEG_usuario] ([usuarioId], [Nombre], [apellido], [password], [tipoUsuarioId], [email], [celular1], [celular2], [nit]) VALUES (1011, N'aaa', N'asdfasd', N'', 2, N'', N'5456148', N'154789', 5647657843)
INSERT [dbo].[tbl_SEG_usuario] ([usuarioId], [Nombre], [apellido], [password], [tipoUsuarioId], [email], [celular1], [celular2], [nit]) VALUES (1014, N'juanaaaa', N'delgadillo', N'', 3, N'juanaaaa@gmail.es', N'12345678', N'213456666', 123456789)
INSERT [dbo].[tbl_SEG_usuario] ([usuarioId], [Nombre], [apellido], [password], [tipoUsuarioId], [email], [celular1], [celular2], [nit]) VALUES (1015, N'ana', N'salazar', N'', 2, N'', N'123456', N'123456888', 4561237898)
INSERT [dbo].[tbl_SEG_usuario] ([usuarioId], [Nombre], [apellido], [password], [tipoUsuarioId], [email], [celular1], [celular2], [nit]) VALUES (1016, N'juan', N'mamani', N'admin!123', 3, N'jumamami@gmail.com', N'78797987', N'', 45614544)
INSERT [dbo].[tbl_SEG_usuario] ([usuarioId], [Nombre], [apellido], [password], [tipoUsuarioId], [email], [celular1], [celular2], [nit]) VALUES (1017, N'awwww', N'sdfsa', N'admin!123', 3, N'asdfghj@gmail.es', N'5464879', N'8979879', 54879465)
SET IDENTITY_INSERT [dbo].[tbl_SEG_usuario] OFF
/****** Object:  StoredProcedure [dbo].[usp_INV_GetUnidadMedidaById]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 26-04-2017
-- Description:	obtener la lista de tipo de unidad de medida por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_GetUnidadMedidaById]
	@intUnidadMediaId		nvarchar(50)
AS
BEGIN
	select [unidadMedidaId],
		   [descripcion]
	FROM [dbo].[tbl_INV_unidadMedida]
  where [unidadMedidaId] = @intUnidadMediaId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_InsertUnidadMedida]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 26-04-2017
-- Description:	Inserta los Unidad Medida
-- =============================================
CREATE PROCEDURE[dbo].[usp_INV_InsertUnidadMedida]
	@varUnidadMedida	nvarchar(50),
	@varDescripcion		nvarchar(150)
AS
BEGIN
	insert into [dbo].[tbl_INV_unidadMedida]
				([unidadMedidaId],
				[descripcion])
	values(@varUnidadMedida,
			@varDescripcion)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_UpdateImagen]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 31-05-2017
-- Description:	actualizar Imagen
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_UpdateImagen]
		@varTitulo			nvarchar(250),
	@intSize			bigint,
	@varExtencion		varchar(50),
	@varDirectiorio		nvarchar(max),
	@dateFechaImagen	datetime,
	@ImagenId			int
AS
BEGIN
	UPDATE [dbo].[tbl_INV_Imagen] 
	set			[titulo] =@varTitulo,
				size =@intSize,
				[extension] =@varExtencion,
				[directorio] =@varDirectiorio,
				[fechaImagen]=@dateFechaImagen
	where [ImagenId] = @ImagenId 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_UpdateFamilia]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 08-05-2017
-- Description:	actualizar familia
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_UpdateFamilia]
	@vardescipcion	nvarchar(250),
	@intImagenId	int,
	@intFamiliaId	int
AS
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
UPDATE [dbo].[tbl_INV_familia]
	   SET  [descripcion] = @vardescipcion,
			[imagenId] = @intImagenId
		WHERE [familiaId] = @intFamiliaId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_InsertImagen]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 30-05-2017
-- Description:	insertar Imagen
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_InsertImagen]
	@intFileID			int OUTPUT,
	@varTitulo			nvarchar(250),
	@intSize			bigint,
	@varExtension		varchar(50),
	@varDirectiorio		nvarchar(max),
	@dateFechaImagen	datetime
AS
BEGIN
SET NOCOUNT ON;
	insert into [dbo].[tbl_INV_Imagen]
				([titulo],
				[size],
				[extension] ,
				[directorio],
				[fechaImagen] )
	values(@varTitulo,
			@intSize,
			@varExtension,
			@varDirectiorio,
			@dateFechaImagen)
	SELECT @intFileID = @@IDENTITY
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_InsertFamilia]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 08-05-2017
-- Description:	Inserta la familia
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_InsertFamilia]
	@varDescripcion		nvarchar(250),
	@intImagenId		int
AS
BEGIN
	insert into [dbo].[tbl_INV_familia]
				([descripcion],
				 [imagenId])
	values(@varDescripcion,
			@intImagenId)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_UpdateUnidadMedida]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 07-05-2017
-- Description:	update de unidad de medida
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_UpdateUnidadMedida]
	@varUnidadMedidaId	nvarchar(50),
	@varDescripcion		nvarchar(150),
	@varUnidadMedidaComp	nvarchar(50)
AS
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
UPDATE [dbo].[tbl_INV_unidadMedida]
	   SET  [unidadMedidaId] = @varUnidadMedidaId,
			[descripcion]= @varDescripcion
		WHERE[unidadMedidaId] = @varUnidadMedidaComp
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetAreaById]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 24-04-2017
-- Description:	obtener el area por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetAreaById]
	@intArea AS INT
AS
BEGIN
	select 	[areaID],
			[descripcion]
	from [dbo].[tbl_SEG_areas]
	WHERE [areaID] = @intArea
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetArea]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 20-04-2017
-- Description:	obtener la lista de Areas
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetArea]
AS
BEGIN
	SELECT  [areaID],
			[descripcion]
  FROM [dbo].[tbl_SEG_areas]
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetTipoUsersById]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 25-04-2017
-- Description:	obtener la lista de tipo de usuarios por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetTipoUsersById]
	@intTipoUsuarioId		int
AS
BEGIN
	SELECT  [tipoUsuarioId],
			[descripcion]
  FROM [dbo].[tbl_SEG_tipoUsuario]
  where [tipoUsuarioId] = @intTipoUsuarioId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetTipoUsers]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 20-04-2017
-- Description:	obtener la lista de tipo de usuarios
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetTipoUsers]
	
AS
BEGIN
	SELECT  [tipoUsuarioId],
			[descripcion]
  FROM [dbo].[tbl_SEG_tipoUsuario]
 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_DeleteArea]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 23-04-2017
-- Description:	Eliminar Area Por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_DeleteArea]
	@intAreaId			int
AS
BEGIN
	DELETE FROM [dbo].[tbl_SEG_areas]
   WHERE [areaID] = @intAreaId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_DeleteTipoUsuario]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 26-04-2017
-- Description:	Eliminar TipoUsuario Por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_DeleteTipoUsuario]
	@intTipoUsuarioId			int
AS
BEGIN
	DELETE FROM [dbo].[tbl_SEG_tipoUsuario]
   WHERE [tipoUsuarioId] = @intTipoUsuarioId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_DeleteUsuario]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 23-04-2017
-- Description:	Eliminar usuario Por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_DeleteUsuario]
	@UsuarioId	int
AS
BEGIN
	DELETE FROM [dbo].[tbl_SEG_usuario] 
   WHERE [usuarioId] = @UsuarioId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_DeleteModulo]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 24-04-2017
-- Description:	elimacion de modulo por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_DeleteModulo]
	@intModuloId	int
AS
BEGIN
	DELETE FROM [dbo].[tbl_SEG_modulos] 
   WHERE [moduloId] = @intModuloId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetModuloId]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 24-04-2017
-- Description:	obtener la lista de moduloc por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetModuloId]
	@intModuloId AS int
AS
BEGIN
	select [moduloId],
			[areaId],
			[descripcion]
	from [dbo].[tbl_SEG_modulos]
	where [moduloId] = @intModuloId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_UpdateProducto]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 10-05-2017
-- Description:	actualizar Producto
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_UpdateProducto]
	@varNombre		nvarchar(150),
	@varDescripcion	nvarchar(max),
	@varUnidadMedida	nvarchar(50),
	@numPrecio		numeric(18,2),
	@intStock		int,
	@intFamilia		int,
	@intProductoId	int
AS
BEGIN
	UPDATE [dbo].[tbl_INV_producto]
	set			[nombre]=@varNombre,
				[descripcion]=@varDescripcion,
				[unidadMedidaId]=@varUnidadMedida,
				[precio]=@numPrecio,
				[stock]=@intStock,
				[familiaId]=@intFamilia	
	where [productoId] = @intProductoId
	
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_InsertProducto]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 10-05-2017
-- Description:	insert Producto
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_InsertProducto]
	@varNombre		nvarchar(150),
	@varDescripcion	nvarchar(max),
	@varUnidadMedida	nvarchar(50),
	@numPrecio		numeric(18,2),
	@intStock		int,
	@intFamilia		int
AS
BEGIN
	insert into [dbo].[tbl_INV_producto]
				([nombre],
				[descripcion],
				[unidadMedidaId],
				[precio],
				[stock],
				[familiaId]	)
		values(@varNombre,
				@varDescripcion,
				@varUnidadMedida,
				@numPrecio,
				@intStock,
				@intFamilia)
END
GO
/****** Object:  Table [dbo].[tbl_ubicacionDeEnvio]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_ubicacionDeEnvio](
	[ubicacionId] [int] IDENTITY(1,1) NOT NULL,
	[usuarioId] [int] NULL,
	[descripcion] [nvarchar](300) NULL,
	[latitud] [decimal](12, 8) NULL,
	[longitud] [decimal](12, 8) NULL,
 CONSTRAINT [PK_tbl_ubicacionDeEnvio] PRIMARY KEY CLUSTERED 
(
	[ubicacionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_DeleteProducto]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 10-05-2017
-- Description:	eliminar producto por id
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_DeleteProducto]
	@intProductoId			int
AS
BEGIN
	DELETE FROM [dbo].[tbl_INV_producto]
   WHERE [productoId] = @intProductoId
END
GO
/****** Object:  Table [dbo].[tbl_INV_imagenProducto]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_INV_imagenProducto](
	[imagenProductoId] [int] IDENTITY(1,1) NOT NULL,
	[productoId] [int] NOT NULL,
	[imagenId] [int] NOT NULL,
 CONSTRAINT [PK_tbl_INV_imagenProducto] PRIMARY KEY CLUSTERED 
(
	[imagenProductoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_INV_imagenProducto] ON
INSERT [dbo].[tbl_INV_imagenProducto] ([imagenProductoId], [productoId], [imagenId]) VALUES (14, 4, 17)
INSERT [dbo].[tbl_INV_imagenProducto] ([imagenProductoId], [productoId], [imagenId]) VALUES (15, 3, 18)
INSERT [dbo].[tbl_INV_imagenProducto] ([imagenProductoId], [productoId], [imagenId]) VALUES (16, 5, 23)
INSERT [dbo].[tbl_INV_imagenProducto] ([imagenProductoId], [productoId], [imagenId]) VALUES (17, 6, 24)
INSERT [dbo].[tbl_INV_imagenProducto] ([imagenProductoId], [productoId], [imagenId]) VALUES (18, 7, 25)
INSERT [dbo].[tbl_INV_imagenProducto] ([imagenProductoId], [productoId], [imagenId]) VALUES (19, 8, 26)
SET IDENTITY_INSERT [dbo].[tbl_INV_imagenProducto] OFF
/****** Object:  Table [dbo].[tbl_carrito]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_carrito](
	[carritoId] [nvarchar](50) NOT NULL,
	[usuarioId] [int] NULL,
	[contenido] [nvarchar](max) NOT NULL,
	[fecha] [datetime] NOT NULL,
	[estadoVenta] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbl_carrito] PRIMARY KEY CLUSTERED 
(
	[carritoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_carrito] ([carritoId], [usuarioId], [contenido], [fecha], [estadoVenta]) VALUES (N'3a506e97-1659-4abd-966f-148049a029ad', 1016, N'{"5":{"ProductoId":5,"Nombre":"parrilada para 4 personas","Descripcion":"parrillada especial o con interiores, ensaladas sustida, chilena,papas mayo,almendrado.","UnidadMedidaId":"plt","Cantidad":4,"Precio":70.00,"SubTotal":280,"Stock":14,"FamiliaId":8,"ImagenId":23,"PrecioForDisplay":"70.00","SubTotalForDisplay":"280.00"},"8":{"ProductoId":8,"Nombre":"vino kohlberg ","Descripcion":"puesto nro.- 1 del vino fino tinto cosecha 2014","UnidadMedidaId":"und","Cantidad":1,"Precio":30.00,"SubTotal":30.00,"Stock":30,"FamiliaId":1,"ImagenId":26,"PrecioForDisplay":"30.00","SubTotalForDisplay":"30.00"},"7":{"ProductoId":7,"Nombre":"soufflé de chocolate","Descripcion":"este soufflé de chocolate de chocolate es ligero y de sabor delicioso","UnidadMedidaId":"und","Cantidad":2,"Precio":15.00,"SubTotal":30.00,"Stock":40,"FamiliaId":5,"ImagenId":25,"PrecioForDisplay":"15.00","SubTotalForDisplay":"30.00"},"3":{"ProductoId":3,"Nombre":"pizza","Descripcion":"tiene esto con esto","UnidadMedidaId":"pza","Cantidad":2,"Precio":50.25,"SubTotal":100.5,"Stock":47,"FamiliaId":4,"ImagenId":18,"PrecioForDisplay":"50.25","SubTotalForDisplay":"100.50"}}', CAST(0x0000A798000404D5 AS DateTime), N'habilitado')
/****** Object:  Table [dbo].[tbl_cantidadProductoVendidoAlDia]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_cantidadProductoVendidoAlDia](
	[cantidadProductoId] [int] IDENTITY(1,1) NOT NULL,
	[productoId] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[fechaVenta] [date] NOT NULL,
 CONSTRAINT [PK_tbl_cantidadProductoVendidoAlDia] PRIMARY KEY CLUSTERED 
(
	[cantidadProductoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_bitacora]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_bitacora](
	[bitacoraId] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NOT NULL,
	[usuariorId] [int] NOT NULL,
	[programa] [nvarchar](250) NULL,
	[operacion] [nvarchar](250) NULL,
	[valAnterior] [nvarchar](250) NULL,
	[valNueva] [nvarchar](250) NULL,
 CONSTRAINT [PK_tbl_bitacora] PRIMARY KEY CLUSTERED 
(
	[bitacoraId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_SEG_accesos]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SEG_accesos](
	[usuarioId] [int] NOT NULL,
	[moduloId] [int] NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2010)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2011)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2012)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2016)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2013)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2046)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2032)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2033)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1009, 2010)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2034)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1009, 2016)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1009, 2011)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1009, 2012)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2044)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2045)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2043)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2018)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2025)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2023)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2024)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2014)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2028)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2026)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2027)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2035)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2020)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2021)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2022)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2036)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2037)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2038)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2010)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2013)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2011)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2032)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2012)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2033)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2016)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2034)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2020)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2018)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2014)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2024)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2026)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2021)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2023)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2027)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2022)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2025)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2028)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2039)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2042)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2040)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1, 2041)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2039)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2040)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2041)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2042)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2035)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2036)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2037)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2038)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2046)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2044)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2045)
INSERT [dbo].[tbl_SEG_accesos] ([usuarioId], [moduloId]) VALUES (1003, 2043)
/****** Object:  StoredProcedure [dbo].[usp_SEG_UpdateModelo]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[usp_SEG_UpdateModelo]
	@intAreaId			int,
	@varDescripcion		nvarchar(250),
	@intModuloId		int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
UPDATE [dbo].[tbl_SEG_modulos]
	   SET [areaId] = @intAreaId
		  ,[descripcion]= @varDescripcion
		  
		WHERE[moduloId] = @intModuloId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_InsertUsuario]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 23-04-2017
-- Description:	Insertar Usuario
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_InsertUsuario]
	@nombre				nvarchar(250),
	@apellido			nvarchar(250),
	@password			nvarchar(250),
	@tipoUsuario		int,
	@email				nvarchar(250),
	@celular1			nvarchar(50),
	@celular2			nvarchar(50),
	@nit				bigint


AS
BEGIN
	insert into [dbo].[tbl_SEG_usuario]
				([Nombre],
				[apellido],
				[password],
				[tipoUsuarioId],
				[email],
				[celular1],
				[celular2],
				[nit])
	values(	@nombre,
			@apellido,
			@password,
			@tipoUsuario,
			@email,
			@celular1,
			@celular2,
			@nit
			)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_InsertModulos]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 24-04-2017
-- Description:	Inserta los modulos
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_InsertModulos]
	@intAreaId			int,
	@varDescripcion		nvarchar(250)
AS
BEGIN
	insert into [dbo].[tbl_SEG_modulos]
				([areaId],
				[descripcion])
	values(	@intAreaId,
			@varDescripcion)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetUsersById]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 19-04-2017
-- Description:	obtener la lista de usuarios por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetUsersById]
	@intUserId AS int
AS
BEGIN
	
	SELECT  [usuarioId],
		[Nombre],
		[apellido],
		[password],
		[tipoUsuarioId],
		[email],
		[celular1],
		[celular2],
		[nit]
  FROM [dbo].[tbl_SEG_usuario]
  WHERE	[usuarioId] = @intUserId 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_UpdateUsuario]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 23-04-2017
-- Description:	actualizar Usuario
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_UpdateUsuario] 
	@varNombre			nvarchar(250),
	@varapellido		nvarchar(250),
	@varPassword			nvarchar(250),
	@inttipoUsuarioId	int,
	@varemail			nvarchar(250),
	@varcelular1		nvarchar(50),
	@varcelular2		nvarchar(50),
	@intNit				bigint,
	@intUsuaruiId		int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	UPDATE [dbo].[tbl_SEG_usuario]
	   SET [Nombre] = @varNombre
		  ,[apellido]= @varapellido
		  ,[password] = @varPassword
		  ,[tipoUsuarioId] = @inttipoUsuarioId
		  ,[email] = @varemail
		  ,[celular1] = @varcelular1
		  ,[celular2] = @varcelular2
		  ,[nit] = @intNit
		WHERE[usuarioId] = @intUsuaruiId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UBI_UpdateUbicacionEnvio]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 16-06-2017
-- Description:	actualizar la ubicacion de envio
-- =============================================
CREATE PROCEDURE[dbo].[usp_UBI_UpdateUbicacionEnvio]
	@intUsuarioId		int,
	@varDescripcion		nvarchar(300),
	@intLatitud			int,
	@intLongitud		int,
	@intUbicacionId		int

AS
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
UPDATE [dbo].[tbl_ubicacionDeEnvio]
	   SET  [usuarioId] = @intUsuarioId,
			[descripcion]   = @varDescripcion,
			[latitud]   = @intLatitud,
			[longitud] 	= @intLongitud
		WHERE [ubicacionId] = @intUbicacionId 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UBI_InsertUbicacionEnvio]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 05-06-2017
-- Description:	insertar ubicaion
-- =============================================
CREATE PROCEDURE [dbo].[usp_UBI_InsertUbicacionEnvio]
	@intUsuarioId		int,
	@varDescripcion		nvarchar(300),
	@decimalLatitud			decimal,
	@decimalLongitud		decimal
AS
BEGIN
	insert into [dbo].[tbl_ubicacionDeEnvio] 
				([usuarioId],
				[descripcion] , 
				 [latitud] ,
				 [longitud]  )
	values( @intUsuarioId,
			@varDescripcion,
			@decimalLatitud,
			@decimalLongitud)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UBI_GetUbicacionById]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 16-06-2017
-- Description:	obtener ubicacion por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_UBI_GetUbicacionById]
	@intUbicacion		int
AS
BEGIN
	select u.ubicacionId,
		u.usuarioId,
		u.descripcion,
		u.latitud,
		u.longitud
FROM tbl_ubicacionDeEnvio u
  where u.[ubicacionId] = @intUbicacion 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UBI_DeleteUbicacionEnvio]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 16-06-2017
-- Description:	eliminar ubicacion de envio
-- =============================================
CREATE PROCEDURE [dbo].[usp_UBI_DeleteUbicacionEnvio]
	@intUbicacionId			int
AS
BEGIN
	DELETE FROM [dbo].[tbl_ubicacionDeEnvio]
   WHERE [ubicacionId]   = @intUbicacionId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_GetImagenProductoById]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 01-06-2017
-- Description:	obtener imagen por id del producto
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_GetImagenProductoById]
	@intProductoId		int
AS
BEGIN
	SELECT ip.[imagenProductoId] 
		  ,ip.[imagenId] 
		  ,ip.[productoId]
		  ,i.[titulo] 
  FROM [FoodGood].[dbo].[tbl_INV_imagenProducto] ip
  join [FoodGood].[dbo].[tbl_INV_Imagen] i on i.[imagenId]  = ip.[imagenId] 
  where [productoId]  = @intProductoId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_InsertAcceso]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 25-04-2017
-- Description:	insertar Acceso
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_InsertAcceso]
	@intUsuarioId		int,
	@modeloId			int
AS
BEGIN
	insert into [dbo].[tbl_SEG_accesos]
				([usuarioId],
				[moduloId])
	values(	@intUsuarioId,
			@modeloId)
END
GO
/****** Object:  Table [dbo].[tbl_PED_pedido]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PED_pedido](
	[pedidoId] [int] IDENTITY(1,1) NOT NULL,
	[usuarioId] [int] NULL,
	[departamentoId] [int] NOT NULL,
	[direccion] [nvarchar](300) NULL,
	[nombreCliente] [nvarchar](250) NULL,
	[apellidoCliente] [nvarchar](250) NULL,
	[nit] [int] NOT NULL,
	[fechaPedido] [datetime] NOT NULL,
	[fechaEntrega] [datetime] NULL,
	[observacionEntrega] [nvarchar](700) NULL,
	[carritoId] [nvarchar](50) NOT NULL,
	[tipoPagoId] [int] NULL,
	[ventaId] [int] NOT NULL,
	[montoTotal] [decimal](18, 2) NULL,
	[latitud] [decimal](18, 8) NULL,
	[longitud] [decimal](18, 8) NULL,
 CONSTRAINT [PK_tbl_pedido] PRIMARY KEY CLUSTERED 
(
	[pedidoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_DeleteImagenProducto]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 01-06-2017
-- Description:	eliminar la Imagen del producto por id
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_DeleteImagenProducto]
	@intImagenProductoId			int
AS
BEGIN
	DELETE FROM [dbo].[tbl_INV_imagenProducto] 
   WHERE [imagenProductoId] = @intImagenProductoId 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_CAR_UpdateCarrito]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 05-06-2017
-- Description:	actalizar carrito
-- =============================================
CREATE PROCEDURE [dbo].[usp_CAR_UpdateCarrito]
	@intUsuarioId	int,
	@varContenido	nvarchar(max),
	@dateFecha		datetime,
	@varCarritoId	nvarchar(50)
	
AS
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
UPDATE [dbo].[tbl_carrito]
	   SET  [usuarioId]  = @intUsuarioId,
			[contenido]  = @varContenido,
			[fecha]	= @dateFecha,
			[estadoVenta] = 'habilitado'
		WHERE [carritoId] = @varCarritoId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_CAR_InsertCarrito]    Script Date: 06/20/2017 00:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 05-06-2017
-- Description:	insertar carrito
-- =============================================
CREATE PROCEDURE [dbo].[usp_CAR_InsertCarrito]
	@varCarritoId		nvarchar(50),
	@intUsuarioId		int,
	@varContenido		nvarchar(max),
	@dateFecha			datetime
AS
BEGIN
	insert into [dbo].[tbl_carrito] 
				([carritoId], 
				 [usuarioId], 
				 [contenido],
				 [fecha],
				 [estadoVenta])
	values(@varCarritoId,
			@intUsuarioId,
			@varContenido,
			@dateFecha,
			'habilitado')
END
GO
/****** Object:  UserDefinedFunction [dbo].[svf_GetImangePortadaProducto]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 30-05-2017
-- Description: funcion para obtener imagen por id
-- =============================================
CREATE FUNCTION [dbo].[svf_GetImangePortadaProducto]
(
	@intProductoId	INT
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @intFileId	INT

	SELECT TOP 1 @intFileId = [imagenId] 
	FROM [dbo].[tbl_INV_imagenProducto]   
	WHERE [productoId]   = @intProductoId

	-- Return the result of the function
	RETURN @intFileId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_CAR_GetCarritoByUserId]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 05-06-2017
-- Description:	buscar Carrito Por usuarioId
-- =============================================
CREATE PROCEDURE [dbo].[usp_CAR_GetCarritoByUserId]
	-- Add the parameters for the stored procedure here
	@userId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [carritoId]
      ,[usuarioId]
      ,[contenido]
      ,[fecha]
      ,[estadoVenta]
	FROM [dbo].[tbl_Carrito]
	WHERE [usuarioId] = @userId
	and [estadoVenta] = 'habilitado'
END
GO
/****** Object:  StoredProcedure [dbo].[usp_CAR_GetCarritoById]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 05-06-2017
-- Description:	obtener carrito por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_CAR_GetCarritoById]
	@varCarritoId			nvarchar(50)
AS
BEGIN
	select [carritoId] ,
		   [usuarioId] ,
		   [contenido] ,
		   [fecha],
		   [estadoVenta]
	FROM [dbo].[tbl_carrito] 
  where [carritoId] = @varCarritoId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_CAR_DeleteCarrito]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 05-06-2017
-- Description:	eliminar carrito por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_CAR_DeleteCarrito]
	@varCarritoId			nvarchar(50)
AS
BEGIN
	DELETE FROM [dbo].[tbl_carrito]  
   WHERE [carritoId] = @varCarritoId 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_CAR_actualizarAVentaCarrito]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 18-06-2017
-- Description:	actualizar carrito a deshabilitado o sea que ya este carrito esta en una venta
-- =============================================
CREATE PROCEDURE [dbo].[usp_CAR_actualizarAVentaCarrito]
	@varCarritoId	nvarchar(50)
	
AS
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
UPDATE [dbo].[tbl_carrito]
	   SET  [estadoVenta] = 'deshabilitado'
		WHERE [carritoId] = @varCarritoId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_InsertImagenProducto]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 01-06-2017
-- Description:	Inserta la Imagen DE Producto
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_InsertImagenProducto]
	@intProductoId			int,
	@intImagenId			int
AS
BEGIN
	insert into [dbo].[tbl_INV_imagenProducto] 
				([productoId],
				 [imagenId] )
	values(@intProductoId,
			@intImagenId)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_GetAccesosById]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 24-04-2017
-- Description:	obtener accesos por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_GetAccesosById]
	@intUsuarioId		int,
	@intModuloId		int
AS
BEGIN
	select [usuarioId],
			[moduloId]
	from [dbo].[tbl_SEG_accesos]
	where [usuarioId] = @intUsuarioId
		and [moduloId]= @intModuloId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SEG_DeleteAcceso]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 25-04-2017
-- Description:	Eliminar Area Por Id del usuario e Id modulo
-- =============================================
CREATE PROCEDURE [dbo].[usp_SEG_DeleteAcceso]
	@intUsuarioId			int,
	@intModuloId			int

AS
BEGIN
	DELETE FROM [dbo].[tbl_SEG_accesos]
   WHERE [usuarioId] = @intUsuarioId and [moduloId] = @intModuloId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_PED_UpdatePedido]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 17-06-2017
-- Description:	actualizar el pedido
-- =============================================
CREATE PROCEDURE [dbo].[usp_PED_UpdatePedido]
	@intUsuarioId		int,
	@intDepartamentoId	int,
	@varDireccion		nvarchar(300),
	@varNombreCliente	nvarchar(250),
	@varApellidoCliente	nvarchar(250),
	@intNit				int,
	@dateFechaPedido	datetime,
	@datefechaEntrega	datetime,
	@varObservacion		nvarchar(700),
	@varCarritoId		nvarchar(50),
	@intTipoPago		int,
	@intVentaId			int,
	@decimalMontoTotal	decimal(18,2),
	@decimalLatitud		decimal(18,8),
	@decimalLongitud	decimal(18,8),
	@intPedidoId		int
AS
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
UPDATE [dbo].[tbl_PED_pedido] 
	   SET  [usuarioId] = @intUsuarioId, 
			[departamentoId] = @intDepartamentoId,
			[direccion] = @varDireccion,
			[nombreCliente] = @varNombreCliente ,
			[apellidoCliente] = @varApellidoCliente ,
			[nit] = @intNit ,
			[fechaPedido]  = @dateFechaPedido,
			[fechaEntrega] = @datefechaEntrega ,
			[observacionEntrega]  = @varObservacion,
			[carritoId] = @varCarritoId,
			[tipoPagoId]  = @intTipoPago,
			[ventaId] = @intVentaId,
			[montoTotal] =@decimalMontoTotal ,
			[latitud] = @decimalLatitud ,
			[longitud] = @decimalLongitud			
		WHERE[pedidoId] = @intPedidoId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_PED_InsertPedido]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 17-06-2017
-- Description:	Insertar pedido
-- =============================================
CREATE PROCEDURE [dbo].[usp_PED_InsertPedido]
--	@intPedidoId		int,
	@intUsuarioId		int,
	@intDepartamentoId	int,
	@varDireccion		nvarchar(300),
	@varNombreCliente	nvarchar(250),
	@varApellidoCliente	nvarchar(250),
	@intNit				int,
	@dateFechaPedido	datetime,
	@datefechaEntrega	datetime,
	@varObservacion		nvarchar(700),
	@varCarritoId		nvarchar(50),
	@intTipoPago		int,
	@intVentaId			int,
	@decimalMontoTotal	decimal(18,2),
	@decimalLatitud		decimal(18,8),
	@decimalLongitud	decimal(18,8)
AS
BEGIN
	insert into [dbo].[tbl_PED_pedido]  
				([usuarioId], 
				 [departamentoId],
				 [direccion],
				 [nombreCliente] ,
				 [apellidoCliente],
				 [nit],
				 [fechaPedido],
				 [fechaEntrega],
				 [observacionEntrega] ,
				 [carritoId] ,
				 [tipoPagoId] ,
				 [ventaId] ,
				 [montoTotal],
				 [latitud],
				 [longitud] )      
	values( @intUsuarioId,
			@intDepartamentoId,
			@varDireccion,
			@varNombreCliente,
			@varApellidoCliente,
			@intNit,
			@dateFechaPedido,
			@datefechaEntrega,
			@varObservacion,
			@varCarritoId,
			@intTipoPago,
			@intVentaId,
			@decimalMontoTotal,
			@decimalLatitud,
			@decimalLongitud)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_PED_GetPedidoById]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 17-06-2017
-- Description:	obtener los pedido por id
-- =============================================
CREATE PROCEDURE [dbo].[usp_PED_GetPedidoById]
	@intPedidoId			nvarchar(50)
AS
BEGIN
	SELECT [pedidoId],
		[usuarioId],
		[departamentoId],
		[direccion],
		[nombreCliente],
		[apellidoCliente],
		[nit],
		[fechaPedido],
		[fechaEntrega],
		[observacionEntrega],
		[observacionEntrega],
		[carritoId],
		[tipoPagoId],
		[ventaId],
		[montoTotal],
		[latitud],
		[longitud]
FROM tbl_PED_pedido 
  where [pedidoId] = @intPedidoId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_PED_DeletePedido]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Delgadillo Salazar
-- Create date: 19-06-2017
-- Description:	eliminar pedido por Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_PED_DeletePedido]
	@intPedidoId			int
AS
BEGIN
	DELETE FROM [dbo].[tbl_PED_pedido] 
   WHERE [pedidoId]  = @intPedidoId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_INV_GetProductoById]    Script Date: 06/20/2017 00:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kevin delgadillo salazar
-- Create date: 10-05-2017
-- Description:	obtener la lista de Producto por ID
-- =============================================
CREATE PROCEDURE [dbo].[usp_INV_GetProductoById]
	@intProductoId		int
AS
BEGIN
	select [productoId],
			[nombre],
			[descripcion],
			[unidadMedidaId],
			[precio],
			[stock],
			[familiaId],
			[dbo].[svf_GetImangePortadaProducto]([productoId]) [ImagenId]
	FROM [dbo].[tbl_INV_producto]
  where [productoId] = @intProductoId
END
GO
/****** Object:  ForeignKey [FK_tbl_SEG_modulos_tbl_SEG_areas]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_SEG_modulos]  WITH CHECK ADD  CONSTRAINT [FK_tbl_SEG_modulos_tbl_SEG_areas] FOREIGN KEY([areaId])
REFERENCES [dbo].[tbl_SEG_areas] ([areaID])
GO
ALTER TABLE [dbo].[tbl_SEG_modulos] CHECK CONSTRAINT [FK_tbl_SEG_modulos_tbl_SEG_areas]
GO
/****** Object:  ForeignKey [FK_tbl_factura_tbl_venta]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_factura]  WITH CHECK ADD  CONSTRAINT [FK_tbl_factura_tbl_venta] FOREIGN KEY([ventaId])
REFERENCES [dbo].[tbl_venta] ([ventaId])
GO
ALTER TABLE [dbo].[tbl_factura] CHECK CONSTRAINT [FK_tbl_factura_tbl_venta]
GO
/****** Object:  ForeignKey [FK_tbl_producto_tbl_familia]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_INV_producto]  WITH CHECK ADD  CONSTRAINT [FK_tbl_producto_tbl_familia] FOREIGN KEY([familiaId])
REFERENCES [dbo].[tbl_INV_familia] ([familiaId])
GO
ALTER TABLE [dbo].[tbl_INV_producto] CHECK CONSTRAINT [FK_tbl_producto_tbl_familia]
GO
/****** Object:  ForeignKey [FK_tbl_producto_tbl_unidadMedida]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_INV_producto]  WITH CHECK ADD  CONSTRAINT [FK_tbl_producto_tbl_unidadMedida] FOREIGN KEY([unidadMedidaId])
REFERENCES [dbo].[tbl_INV_unidadMedida] ([unidadMedidaId])
GO
ALTER TABLE [dbo].[tbl_INV_producto] CHECK CONSTRAINT [FK_tbl_producto_tbl_unidadMedida]
GO
/****** Object:  ForeignKey [FK_tbl_usuario_tbl_tipoUsuarioId]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_SEG_usuario]  WITH CHECK ADD  CONSTRAINT [FK_tbl_usuario_tbl_tipoUsuarioId] FOREIGN KEY([tipoUsuarioId])
REFERENCES [dbo].[tbl_SEG_tipoUsuario] ([tipoUsuarioId])
GO
ALTER TABLE [dbo].[tbl_SEG_usuario] CHECK CONSTRAINT [FK_tbl_usuario_tbl_tipoUsuarioId]
GO
/****** Object:  ForeignKey [FK_tbl_ubicacionDeEnvio_tbl_SEG_usuario]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_ubicacionDeEnvio]  WITH CHECK ADD  CONSTRAINT [FK_tbl_ubicacionDeEnvio_tbl_SEG_usuario] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[tbl_SEG_usuario] ([usuarioId])
GO
ALTER TABLE [dbo].[tbl_ubicacionDeEnvio] CHECK CONSTRAINT [FK_tbl_ubicacionDeEnvio_tbl_SEG_usuario]
GO
/****** Object:  ForeignKey [FK_tbl_INV_imagenProducto_tbl_INV_Imagen]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_INV_imagenProducto]  WITH CHECK ADD  CONSTRAINT [FK_tbl_INV_imagenProducto_tbl_INV_Imagen] FOREIGN KEY([imagenId])
REFERENCES [dbo].[tbl_INV_Imagen] ([imagenId])
GO
ALTER TABLE [dbo].[tbl_INV_imagenProducto] CHECK CONSTRAINT [FK_tbl_INV_imagenProducto_tbl_INV_Imagen]
GO
/****** Object:  ForeignKey [FK_tbl_INV_imagenProducto_tbl_INV_producto]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_INV_imagenProducto]  WITH CHECK ADD  CONSTRAINT [FK_tbl_INV_imagenProducto_tbl_INV_producto] FOREIGN KEY([productoId])
REFERENCES [dbo].[tbl_INV_producto] ([productoId])
GO
ALTER TABLE [dbo].[tbl_INV_imagenProducto] CHECK CONSTRAINT [FK_tbl_INV_imagenProducto_tbl_INV_producto]
GO
/****** Object:  ForeignKey [FK_tbl_carrito_tbl_usuario]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_carrito]  WITH CHECK ADD  CONSTRAINT [FK_tbl_carrito_tbl_usuario] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[tbl_SEG_usuario] ([usuarioId])
GO
ALTER TABLE [dbo].[tbl_carrito] CHECK CONSTRAINT [FK_tbl_carrito_tbl_usuario]
GO
/****** Object:  ForeignKey [FK_tbl_cantidadProductoVendidoAlDia_tbl_producto]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_cantidadProductoVendidoAlDia]  WITH CHECK ADD  CONSTRAINT [FK_tbl_cantidadProductoVendidoAlDia_tbl_producto] FOREIGN KEY([productoId])
REFERENCES [dbo].[tbl_INV_producto] ([productoId])
GO
ALTER TABLE [dbo].[tbl_cantidadProductoVendidoAlDia] CHECK CONSTRAINT [FK_tbl_cantidadProductoVendidoAlDia_tbl_producto]
GO
/****** Object:  ForeignKey [FK_tbl_bitacora_tbl_usuario]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_bitacora]  WITH CHECK ADD  CONSTRAINT [FK_tbl_bitacora_tbl_usuario] FOREIGN KEY([usuariorId])
REFERENCES [dbo].[tbl_SEG_usuario] ([usuarioId])
GO
ALTER TABLE [dbo].[tbl_bitacora] CHECK CONSTRAINT [FK_tbl_bitacora_tbl_usuario]
GO
/****** Object:  ForeignKey [FK_tbl_SEG_accesos_tbl_SEG_modulos]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_SEG_accesos]  WITH CHECK ADD  CONSTRAINT [FK_tbl_SEG_accesos_tbl_SEG_modulos] FOREIGN KEY([moduloId])
REFERENCES [dbo].[tbl_SEG_modulos] ([moduloId])
GO
ALTER TABLE [dbo].[tbl_SEG_accesos] CHECK CONSTRAINT [FK_tbl_SEG_accesos_tbl_SEG_modulos]
GO
/****** Object:  ForeignKey [FK_tbl_SEG_accesos_tbl_SEG_usuario]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_SEG_accesos]  WITH CHECK ADD  CONSTRAINT [FK_tbl_SEG_accesos_tbl_SEG_usuario] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[tbl_SEG_usuario] ([usuarioId])
GO
ALTER TABLE [dbo].[tbl_SEG_accesos] CHECK CONSTRAINT [FK_tbl_SEG_accesos_tbl_SEG_usuario]
GO
/****** Object:  ForeignKey [FK_tbl_PED_pedido_tbl_carrito]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_PED_pedido]  WITH CHECK ADD  CONSTRAINT [FK_tbl_PED_pedido_tbl_carrito] FOREIGN KEY([carritoId])
REFERENCES [dbo].[tbl_carrito] ([carritoId])
GO
ALTER TABLE [dbo].[tbl_PED_pedido] CHECK CONSTRAINT [FK_tbl_PED_pedido_tbl_carrito]
GO
/****** Object:  ForeignKey [FK_tbl_pedido_tbl_departamento]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_PED_pedido]  WITH CHECK ADD  CONSTRAINT [FK_tbl_pedido_tbl_departamento] FOREIGN KEY([departamentoId])
REFERENCES [dbo].[tbl_departamento] ([departamentoId])
GO
ALTER TABLE [dbo].[tbl_PED_pedido] CHECK CONSTRAINT [FK_tbl_pedido_tbl_departamento]
GO
/****** Object:  ForeignKey [FK_tbl_pedido_tbl_tipoPago]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_PED_pedido]  WITH CHECK ADD  CONSTRAINT [FK_tbl_pedido_tbl_tipoPago] FOREIGN KEY([tipoPagoId])
REFERENCES [dbo].[tbl_tipoPago] ([tipoPagoId])
GO
ALTER TABLE [dbo].[tbl_PED_pedido] CHECK CONSTRAINT [FK_tbl_pedido_tbl_tipoPago]
GO
/****** Object:  ForeignKey [FK_tbl_pedido_tbl_usuario]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_PED_pedido]  WITH CHECK ADD  CONSTRAINT [FK_tbl_pedido_tbl_usuario] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[tbl_SEG_usuario] ([usuarioId])
GO
ALTER TABLE [dbo].[tbl_PED_pedido] CHECK CONSTRAINT [FK_tbl_pedido_tbl_usuario]
GO
/****** Object:  ForeignKey [FK_tbl_pedido_tbl_venta]    Script Date: 06/20/2017 00:17:56 ******/
ALTER TABLE [dbo].[tbl_PED_pedido]  WITH CHECK ADD  CONSTRAINT [FK_tbl_pedido_tbl_venta] FOREIGN KEY([ventaId])
REFERENCES [dbo].[tbl_venta] ([ventaId])
GO
ALTER TABLE [dbo].[tbl_PED_pedido] CHECK CONSTRAINT [FK_tbl_pedido_tbl_venta]
GO

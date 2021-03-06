USE [master]
GO
/****** Object:  Database [jctm]    Script Date: 4/17/2016 1:24:18 PM ******/
CREATE DATABASE [jctm]
USE [jctm]
GO
/****** Object:  UserDefinedFunction [dbo].[WEBAPI_getHighForCurrentDayFUNC]    Script Date: 4/26/2016 7:36:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[WEBAPI_getHighForCurrentDayFUNC]()
RETURNS FLOAT
AS
	BEGIN
		DECLARE @CurrentDay DATETIME
		DECLARE @MaxTemp FLOAT

		SET @CurrentDay = DATEADD(HOUR, -4, GETDATE())

		SELECT @MaxTemp = MAX(dbo.Temperatures.Temperature)
		FROM dbo.Temperatures
		WHERE DATEPART(DAY, DATEADD(HOUR, -4, dbo.Temperatures.Created)) = DATEPART(DAY, @CurrentDay)

		RETURN @MaxTemp
	END
GO
/****** Object:  UserDefinedFunction [dbo].[WEBAPI_getMinForCurrentDayFUNC]    Script Date: 4/26/2016 7:36:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[WEBAPI_getMinForCurrentDayFUNC]()
RETURNS FLOAT
AS
	BEGIN
		DECLARE @CurrentDay DATETIME
		DECLARE @MaxTemp FLOAT

		SET @CurrentDay = DATEADD(HOUR, -4, GETDATE())

		SELECT @MaxTemp = MIN(dbo.Temperatures.Temperature)
		FROM dbo.Temperatures
		WHERE DATEPART(DAY, DATEADD(HOUR, -4, dbo.Temperatures.Created)) = DATEPART(DAY, @CurrentDay)

		RETURN @MaxTemp
	END
GO
/****** Object:  Table [dbo].[DGT_DayOverviewDetail]    Script Date: 4/26/2016 7:36:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DGT_DayOverviewDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DayPart] [datetime] NOT NULL,
	[Hour] [int] NOT NULL,
	[AvgTemp] [int] NOT NULL,
 CONSTRAINT [PK_DGT_DayOverviewDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[DGT_DayOverviewListing]    Script Date: 4/26/2016 7:36:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DGT_DayOverviewListing](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Modified] [datetimeoffset](7) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Active] [bit] NOT NULL,
	[Day] [datetime] NOT NULL,
	[LowTemp] [float] NOT NULL,
	[HighTemp] [float] NOT NULL,
	[AverageTemp] [float] NOT NULL,
 CONSTRAINT [PK_DGT_DayOveriewListing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Humidity]    Script Date: 4/26/2016 7:36:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Humidity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Modified] [datetime] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[Humidity] [float] NOT NULL,
 CONSTRAINT [PK_Humidity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Temperatures]    Script Date: 4/26/2016 7:36:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Temperatures](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Modified] [datetime] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[Temperature] [float] NOT NULL,
 CONSTRAINT [PK_Temperatures] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  StoredProcedure [dbo].[DGT_GenerateDayOverviewListingSP]    Script Date: 4/26/2016 7:36:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DGT_GenerateDayOverviewListingSP]
AS
	DELETE FROm dbo.DGT_DayOverviewListing

	INSERT INTO dbo.DGT_DayOverviewListing
		SELECT
			GETUTCDATE(),
			GETUTCDATE(),
			1,		
			Result.DayPart,
			(SELECT MIN(minTemp.Temperature) FROM dbo.Temperatures minTemp WHERE CAST(convert(char(11), DATEADD(HOUR, -4, minTemp.Created), 113) as datetime) = Result.DayPart) AS MinTemp,
			(SELECT MAX(minTemp.Temperature) FROM dbo.Temperatures minTemp WHERE CAST(convert(char(11), DATEADD(HOUR, -4, minTemp.Created), 113) as datetime) = Result.DayPart) AS MaxTemp,
			(SELECT AVG(minTemp.Temperature) FROM dbo.Temperatures minTemp WHERE CAST(convert(char(11), DATEADD(HOUR, -4, minTemp.Created), 113) as datetime) = Result.DayPart) AS Average
		FROM (SELECT
				DISTINCT CAST(convert(char(11), DATEADD(HOUR, -4, dbo.Temperatures.Created), 113) as datetime) AS DayPart
			FROM dbo.Temperatures
			WHERE dbo.Temperatures.Active = 1) AS Result
		ORDER BY Result.DayPart ASC
GO
/****** Object:  StoredProcedure [dbo].[SQL_runNightlyJobsSP]    Script Date: 4/26/2016 7:36:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SQL_runNightlyJobsSP]
AS
	-- Generate the DGT_DayOverviewListing Table
	EXEC dbo.DGT_GenerateDayOverviewListingSP

	-- Generate the DGT_DayOverviewDetail Table
	EXEC dbo.WEBAPI_generateDayOverviewDetailSP
GO
/****** Object:  StoredProcedure [dbo].[WEBAPI_generateDayOverviewDetailSP]    Script Date: 4/26/2016 7:36:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WEBAPI_generateDayOverviewDetailSP]
AS
	BEGIN
		INSERT INTO dbo.DGT_DayOverviewDetail			
			SELECT
				Result.DayPart,
				Result.HourPart, 
				ROUND(AVG(Result.Temperature), 0) AS AvgTemp
			FROM (
				SELECT
					CAST(convert(char(11), DATEADD(HOUR, -4, dbo.Temperatures.Created), 113) as datetime) AS DayPart,
					DATEPART(HOUR, DATEADD(HOUR, -4, dbo.Temperatures.Created)) AS HourPart,
					dbo.Temperatures.Temperature
				FROM dbo.Temperatures
			) AS Result
			GROUP BY Result.DayPart, Result.HourPart
	END
GO
/****** Object:  StoredProcedure [dbo].[WEBAPI_getDayOverviewDetailSP]    Script Date: 4/26/2016 7:36:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WEBAPI_getDayOverviewDetailSP]
(@SelectedDayListingID INT)
AS
	BEGIN
		DECLARE @DayPart DATETIME

		SELECT @DayPart = dbo.DGT_DayOverviewListing.Day FROM dbo.DGT_DayOverviewListing WHERE dbo.DGT_DayOverviewListing.ID = @SelectedDayListingID

		SELECT
			dbo.DGT_DayOverviewDetail.[Hour] AS HourPart,
			dbo.DGT_DayOverviewDetail.AvgTemp
		FROM dbo.DGT_DayOverviewDetail
		WHERE dbo.DGT_DayOverviewDetail.DayPart = @DayPart
	END
GO
/****** Object:  StoredProcedure [dbo].[WEBAPI_getDayOverviewListingSP]    Script Date: 4/26/2016 7:36:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WEBAPI_getDayOverviewListingSP]
AS
	SELECT
		dbo.DGT_DayOverviewListing.ID,
		dbo.DGT_DayOverviewListing.Day,
		dbo.DGT_DayOverviewListing.LowTemp,
		dbo.DGT_DayOverviewListing.HighTemp,
		dbo.DGT_DayOverviewListing.AverageTemp
	FROM dbo.DGT_DayOverviewListing
	ORDER BY dbo.DGT_DayOverviewListing.Day ASC
GO
/****** Object:  StoredProcedure [dbo].[WEBAPI_getLatestTemperatureSP]    Script Date: 4/26/2016 7:36:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WEBAPI_getLatestTemperatureSP]
AS
	SELECT
		TOP 1
		dbo.Temperatures.Temperature,
		dbo.Temperatures.Modified,
		dbo.WEBAPI_getMinForCurrentDayFUNC() AS Min,
		dbo.WEBAPI_getHighForCurrentDayFUNC() AS Max		
	FROM dbo.Temperatures
	WHERE dbo.Temperatures.Active = 1
	ORDER BY dbo.Temperatures.Modified DESC
GO

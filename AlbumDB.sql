
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'AlbumDB' )
BEGIN 
	CREATE DATABASE AlbumDB
END
GO
USE AlbumDB
GO
--IF EXISTS (SELECT * FROM sys.tables UNION ALL SELECT * FROM sysobjects WHERE xtype='p' UNION ALL SELECT * FROM sysobjects WHERE xtype='v' UNION ALL SELECT * FROM sysobjects WHERE xtype = 'tr')
--BEGIN 

--END
IF NOT EXISTS (SELECT * FROM sys.filegroups WHERE name = 'FileStreamGroup' )
BEGIN
ALTER DATABASE AlbumDB ADD FILEGROUP FileStreamGroup CONTAINS FILESTREAM
   ALTER DATABASE AlbumDB 
   ADD FILE (NAME = 'FileStreamData', FILENAME = 'C:\Program Files\Databases\MSSQL_FileStream')
   TO FILEGROUP FileStreamGroup
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Роли')
BEGIN
	/*Tables*/
   CREATE TABLE Роли(
		КодРоли INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
		НазваниеРоли NVARCHAR(20)
   )
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Пользователи')
BEGIN
   CREATE TABLE Пользователи (
		КодПользователя INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		КодРоли INT,
		Логин NVARCHAR(15) UNIQUE,
		ХешированныйПароль NVARCHAR(100),
		НазваниеПочты NVARCHAR (40)
	)
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Гости')
BEGIN
	CREATE TABLE Гости (
		КодГостя INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
		КодРоли INT,
		Логин NVARCHAR (15) UNIQUE
	)
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Альбомы')
BEGIN
	CREATE TABLE Альбомы (
		КодАльбома INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
		КодПользователя INT,
		ДатаСоздания DATETIME,
		НазваниеАльбома NVARCHAR (25) UNIQUE,
		КраткоеОписание NVARCHAR (100)
	)
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Альбомы_Фотографии')
BEGIN
	CREATE TABLE Альбомы_Фотографии (
		КодАльбома_Фотографии INT PRIMARY KEY IDENTITY (1,1),
		КодАльбома INT,
		КодФотографии INT
	)
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Фотографии')
BEGIN
	CREATE TABLE Фотографии (
	    КодФотографии INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		КодСтроки UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL UNIQUE DEFAULT NEWID(),
		КодПользователя INT,
		КодОбъекта INT,
		КодСтиля INT,
		ДатаЗагрузки DATETIME,
		НазваниеФотографии NVARCHAR (20) UNIQUE,
		Описание NVARCHAR(100),
		Формат NVARCHAR (10),
		Разрешение NVARCHAR(10),
		Уникальность int,
		Путь VARBINARY(MAX) FILESTREAM DEFAULT(0x)
	)
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'АрхивФотографий')
BEGIN
	CREATE TABLE АрхивФотографий (
		КодФотографии INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		КодСтроки UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL UNIQUE DEFAULT NEWID(),
		ДатаЗагрузки DATETIME,
		НазваниеФотографии NVARCHAR (20) UNIQUE,
		Описание NVARCHAR(100),
		Формат NVARCHAR (10),
		Разрешение NVARCHAR(10),
		Уникальность int,
		Путь VARBINARY(MAX) FILESTREAM DEFAULT(0x)
	)
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Вопросы')
BEGIN
	CREATE TABLE Вопросы (
		КодВопроса INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		КодПользователя INT,
		НазваниеВопроса NVARCHAR(25) UNIQUE
	)
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Стили')
BEGIN
	CREATE TABLE Стили (
		КодСтиля INT PRIMARY KEY IDENTITY (1,1),
		НазваниеСтиля NVARCHAR (25) UNIQUE
	)
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Объекты')
BEGIN
	CREATE TABLE Объекты (
		КодОбъекта INT PRIMARY KEY IDENTITY (1,1),
		НазваниеОбъекта NVARCHAR(25) UNIQUE
	)
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Места')
BEGIN
	CREATE TABLE Места (
		КодМеста INT PRIMARY KEY IDENTITY (1,1),
		НазваниеМеста NVARCHAR(25) UNIQUE
	)
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Фотографии_Места')
BEGIN
	CREATE TABLE Фотографии_Места(
		КодФотографии_Места INT PRIMARY KEY IDENTITY(1,1),
		КодФотографии INT,
		КодМеста INT
	)
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Фотографии_Оборудование')
BEGIN
	CREATE TABLE Фотографии_Оборудование(
	    КодФотографии_Оборудование INT PRIMARY KEY IDENTITY (1,1),
		КодФотографии INT,
		КодОборудования INT
	)
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Оборудование')
BEGIN
	CREATE TABLE Оборудование(
		КодОборудования INT PRIMARY KEY IDENTITY(1,1),
		НазваниеОборудования NVARCHAR (25) UNIQUE
	)
END
GO
INSERT INTO Роли VALUES (N'Пользователь')
INSERT INTO Роли VALUES (N'Гость')
GO
/*Check*/
ALTER TABLE Пользователи ADD CONSTRAINT ch_login CHECK (LEN(LTRIM(RTRIM(Логин))) > 0 and Логин NOT LIKE '% %' and Логин NOT LIKE '%[^A-Za-z0-9]%') 
GO
ALTER TABLE Пользователи ADD CONSTRAINT ch_email CHECK (НазваниеПочты LIKE '%_@__%.__%')
GO
ALTER TABLE Гости ADD CONSTRAINT ch_login1 CHECK (LEN(LTRIM(RTRIM(Логин))) > 0 and Логин NOT LIKE '% %' and Логин NOT LIKE '%[^A-Za-z0-9]%') 
GO
ALTER TABLE Фотографии ADD CONSTRAINT ch_format CHECK (Формат NOT LIKE '.[^A-Za-z]%') 
GO
ALTER TABLE Фотографии ADD CONSTRAINT ch_dim CHECK (Разрешение  LIKE '[0-9]%x[0-9]%') 
GO
ALTER TABLE Фотографии ADD CONSTRAINT ch_photoname CHECK (НазваниеФотографии NOT LIKE '%[^A-Za-zА-Яа-я]%')
GO
ALTER TABLE Альбомы ADD CONSTRAINT ch_albumname CHECK (НазваниеАльбома NOT LIKE '%[^A-Za-zА-Яа-я]%')
GO
ALTER TABLE Фотографии ADD CONSTRAINT ch_uniquephoto CHECK (Уникальность>=0 and Уникальность<=10)
GO
ALTER TABLE Вопросы ADD CONSTRAINT ch_questionname CHECK (НазваниеВопроса NOT LIKE '%[^A-Za-zА-Яа-я0-9]%')
GO
ALTER TABLE Стили ADD CONSTRAINT ch_stylename CHECK (НазваниеСтиля NOT LIKE '%[^A-Za-zА-Яа-я]%')
GO
ALTER TABLE Объекты ADD CONSTRAINT ch_objectname CHECK (НазваниеОбъекта NOT LIKE '%[^A-Za-zА-Яа-я]%')
GO
ALTER TABLE Места ADD CONSTRAINT ch_placename CHECK (НазваниеМеста NOT LIKE '%[^A-Za-zА-Яа-я0-9]%')
GO
ALTER TABLE Оборудование ADD CONSTRAINT ch_accessoriesname CHECK (НазваниеОборудования NOT LIKE '%[^A-Za-zА-Яа-я]%')
GO
/*Foreign Keys*/
ALTER TABLE Фотографии_Оборудование ADD CONSTRAINT FK_PhotoCode FOREIGN KEY (КодФотографии) REFERENCES Фотографии(КодФотографии) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE Фотографии_Оборудование ADD CONSTRAINT FK_AccessoriesCode FOREIGN KEY (КодОборудования) REFERENCES Оборудование (КодОборудования) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE Фотографии_Места ADD CONSTRAINT FK_PhotoCode1 FOREIGN KEY (КодФотографии) REFERENCES Фотографии(КодФотографии) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE Фотографии_Места ADD CONSTRAINT FK_PlaceCode FOREIGN KEY (КодМеста) REFERENCES  Места(КодМеста) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE Вопросы ADD CONSTRAINT FK_UserCode FOREIGN KEY (КодПользователя) REFERENCES Пользователи(КодПользователя) ON DELETE SET NULL ON UPDATE NO ACTION
GO
ALTER TABLE Фотографии ADD CONSTRAINT FK_UserCode FOREIGN KEY (КодПользователя) REFERENCES Пользователи(КодПользователя) ON DELETE SET NULL ON UPDATE NO ACTION
GO
ALTER TABLE Фотографии ADD CONSTRAINT FK_ObjectCode FOREIGN KEY (КодОбъекта)  REFERENCES Объекты(КодОбъекта) ON DELETE SET NULL ON UPDATE CASCADE
GO
ALTER TABLE Фотографии ADD CONSTRAINT FK_styleCode FOREIGN KEY (КодСтиля)  REFERENCES Стили(КодСтиля) ON DELETE SET NULL ON UPDATE CASCADE 
GO
ALTER TABLE Альбомы_Фотографии ADD CONSTRAINT FK_AlbumCode FOREIGN KEY (КодАльбома) REFERENCES Альбомы(КодАльбома) ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE Альбомы_Фотографии ADD CONSTRAINT FK_PhotoCode FOREIGN KEY (КодФотографии) REFERENCES Фотографии(КодФотографии) ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE Альбомы ADD CONSTRAINT FK_UserCode FOREIGN KEY (КодПользователя) REFERENCES Пользователя(КодПользователя) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE Гости ADD CONSTRAINT FK_RoleCode FOREIGN KEY (КодРоли) REFERENCES Роли(КодРоли) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE Пользователи ADD CONSTRAINT FK_RoleCode1 FOREIGN KEY (КодРоли) REFERENCES Роли(КодРоли) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
/*Indexes*/
CREATE NONCLUSTERED INDEX IX_Photos_TitlePhotos ON Фотографии (НазваниеФотографии ASC)
GO
CREATE NONCLUSTERED INDEX IX_Users_Login ON Пользователи(Логин ASC)
GO
CREATE NONCLUSTERED INDEX IX_Styles_StyleTitle ON Стили(НазваниеСтиля ASC)
GO
CREATE NONCLUSTERED INDEX IX_Objects_ObjectName ON Объекты(НазваниеОбъекта ASC)
GO
CREATE NONCLUSTERED INDEX IX_Albums_AlbumName ON Альбомы(НазваниеАльбома ASC)
GO
CREATE NONCLUSTERED INDEX IX_Photos_Author ON Фотографии(КодПользователя)
/*Views*/
GO
CREATE VIEW Photos_Detailed
AS
SELECT ph.ДатаЗагрузки, ph.НазваниеФотографии,
ph.Описание, ph.Разрешение, ph.Уникальность, ph.Формат, ph.Путь, us.Логин, obj.НазваниеОбъекта, st.НазваниеСтиля
	FROM Фотографии ph
	INNER JOIN Пользователи us
		ON ph.КодПользователя = us.КодПользователя 
	LEFT OUTER JOIN Объекты obj
		ON ph.КодОбъекта = obj.КодОбъекта
	LEFT OUTER JOIN Стили st
		ON ph.КодСтиля = st.КодСтиля
	ORDER BY ph.ДатаЗагрузки ASC
GO
CREATE VIEW All_Albums_Photos
AS
SELECT  albums.НазваниеАльбома, ph.НазваниеФотографии
	FROM Альбомы_Фотографии alph
	LEFT OUTER JOIN Фотографии ph
		ON alph.КодФотографии = ph.КодФотографии  
	INNER JOIN Альбомы albums
		ON alph.КодАльбома = albums.КодАльбома
GO
CREATE VIEW Albums_Users
AS
SELECT  albums.ДатаСоздания, albums.НазваниеАльбома, albums.КраткоеОписание, us.Логин
	FROM Альбомы albums
	INNER JOIN Пользователи us
		ON albums.КодПользователя = us.КодПользователя
	ORDER BY albums.ДатаСоздания ASC
GO
CREATE VIEW All_Accessories_Photos
AS
SELECT  ph.НазваниеФотографии, ac.НазваниеОборудования
	FROM Фотографии_Оборудование phac
	INNER JOIN Фотографии ph
		ON phac.КодФотографии = ph.КодФотографии
	LEFT JOIN Оборудование ac
		ON phac.КодОборудования = ac.КодОборудования
GO
CREATE VIEW All_Places_Photos
AS 
SELECT  ph.НазваниеФотографии, pl.НазваниеМеста
	FROM Фотографии_Места phpl
	INNER JOIN Фотографии ph
		ON phpl.КодФотографии = ph.КодФотографии
	LEFT JOIN Места pl
		ON phpl.КодМеста = pl.КодМеста
GO
CREATE VIEW Questions
AS
SELECT qu.НазваниеВопроса, us.Логин
	FROM Вопросы qu
	INNER JOIN Пользователи us
		ON qu.КодПользователя = us.КодПользователя

/*Procedures*/
GO
CREATE PROCEDURE InsertPhoto
        @КодСтроки UNIQUEIDENTIFIER,
		@КодПользователя INT,
		@КодОбъекта INT,
		@КодСтиля INT,
		@ДатаЗагрузки DATETIME,
		@НазваниеФотографии NVARCHAR(20),
		@Описание NVARCHAR(100),
		@Формат NVARCHAR (10),
		@Разрешение NVARCHAR(10),
		@Уникальность INT,
		@Путь VARBINARY(MAX)
AS
BEGIN
		SET NOCOUNT ON;
		SET @КодСтроки = NEWID()
		INSERT INTO Фотографии (КодСтроки, КодПользователя, КодОбъекта,  КодСтиля, ДатаЗагрузки, НазваниеФотографии, Описание, Формат, Разрешение, Уникальность, Путь)
		VALUES (@КодСтроки, @КодПользователя, @КодОбъекта, @КодСтиля, @ДатаЗагрузки, @НазваниеФотографии, @Описание,
		@Формат, @Разрешение, @Уникальность, @Путь)
END
GO
CREATE PROCEDURE DeletePhoto
	@КодФотографии INT
AS
BEGIN
		SET NOCOUNT ON;
		DELETE FROM Фотографии
		WHERE КодФотографии = @КодФотографии
END
GO
CREATE PROCEDURE UpdatePhoto
		@КодФотографии INT,
		@КодПользователя INT,
		@КодОбъекта INT,
		@КодСтиля INT,
		@ДатаЗагрузки DATETIME,
		@НазваниеФотографии NVARCHAR(20),
		@Описание NVARCHAR(100),
		@Формат NVARCHAR (10),
		@Разрешение NVARCHAR(10),
		@Уникальность INT,
		@Путь VARBINARY(MAX)
AS
BEGIN
	SET NOCOUNT ON
	UPDATE Фотографии 
	SET КодПользователя = @КодПользователя,
		КодОбъекта = @КодОбъекта,
		КодСтиля = @КодСтиля,
		ДатаЗагрузки = @ДатаЗагрузки,
		НазваниеФотографии = @НазваниеФотографии,
		Описание = @Описание,
		Формат = @Формат,
		Разрешение = @Разрешение,
		Уникальность = @Уникальность,
		Путь = @Путь
	WHERE КодФотографии = @КодФотографии
END
GO
CREATE PROCEDURE InsertAccessories
	@НазваниеОборудования NVARCHAR(25)
AS
BEGIN
	INSERT INTO Оборудование(НазваниеОборудования)VALUES(@НазваниеОборудования)
END
GO
CREATE PROCEDURE UpdateAccessories
 @КодОборудования INT,
 @НазваниеОборудования NVARCHAR(25)
AS
BEGIN
	UPDATE Оборудование
	SET НазваниеОборудования = @НазваниеОборудования
	WHERE КодОборудования = @КодОборудования
END
GO
CREATE PROCEDURE InsertPlaces
	@НазваниеМеста NVARCHAR(25)
AS
BEGIN
	INSERT INTO Места(НазваниеМеста)VALUES(@НазваниеМеста)
END
GO
CREATE PROCEDURE UpdatePlaces
 @КодМеста INT,
 @НазваниеМеста NVARCHAR(25)
AS
BEGIN
	UPDATE Места
	SET НазваниеМеста = @НазваниеМеста
	WHERE КодМеста = @КодМеста
END
GO
CREATE PROCEDURE AlbumPhotos
	@КодАльбома INT,
	@КодФотографии INT
AS
BEGIN
	INSERT INTO Альбомы_Фотографии (КодАльбома, КодФотографии)
	VALUES (@КодАльбома, @КодФотографии)
END
GO
CREATE PROCEDURE PhotoAccessories
	@КодФотографии INT,
	@КодОборудования INT
AS
BEGIN
	INSERT INTO Фотографии_Оборудование(КодФотографии, КодОборудования)
	VALUES (@КодФотографии, @КодОборудования)
END
GO
CREATE PROCEDURE PhotoPlaces
	@КодФотографии INT,
	@КодМеста INT
AS
BEGIN
	INSERT INTO Фотографии_Места(КодФотографии, КодМеста)
	VALUES(@КодФотографии, @КодМеста)
END
GO
/*Triggers*/
CREATE TRIGGER ArchivePhotos
ON Фотографии
AFTER DELETE 
AS
BEGIN 
	SET NOCOUNT ON;
	INSERT INTO АрхивФотографий(КодСтроки, ДатаЗагрузки, НазваниеФотографии, Описание, Формат, Разрешение, Уникальность, Путь )
	SELECT NEWID(),  GETDATE(), d.НазваниеФотографии, d.Описание, d.Формат, d.Разрешение, d.Уникальность, d.Путь
	FROM deleted d
END;

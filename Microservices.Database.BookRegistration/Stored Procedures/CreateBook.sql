CREATE PROCEDURE [dbo].[CreateBook]
	@ID nvarchar(50),
	@Title nvarchar(50),
	@Author nvarchar(50)
AS
	INSERT INTO [dbo].[Books]
	VALUES (@ID,@Title,@Author)

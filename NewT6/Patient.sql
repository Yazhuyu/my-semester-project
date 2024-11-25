CREATE TABLE [dbo].[Patient]
(
	[PatIdno] NVARCHAR(20) NOT NULL PRIMARY KEY,
    [PatName] NVARCHAR(100) NULL DEFAULT '未提供',  -- 設定預設值為 '未提供'
    [PatId] INT IDENTITY(1,1) NOT NULL,  -- 自動生成的欄位
    [PatGender] NVARCHAR(10) NULL DEFAULT '未提供',  -- 設定預設值為 '未提供'
    [PatPhone] NVARCHAR(10) NULL DEFAULT '未提供',  -- 設定預設值為 '未提供'
    [PatAddress] NVARCHAR(200) NULL DEFAULT '未提供',  -- 設定預設值為 '未提供'
    [PatMail] NVARCHAR(100) NULL DEFAULT '未提供'  -- 設定預設值為 '未提供'
)

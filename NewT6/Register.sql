CREATE TABLE [dbo].[Register]
(
	[PatIdno] NVARCHAR(20) NOT NULL PRIMARY KEY,  -- 病歷號碼作為主鍵
    [PatPassword] NVARCHAR(20) NOT NULL          -- 密碼，不能為空
)

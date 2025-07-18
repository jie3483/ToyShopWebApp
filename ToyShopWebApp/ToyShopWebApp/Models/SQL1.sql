CREATE TABLE [dbo].[Users]
(
    [UserID] INT IDENTITY(1,1) PRIMARY KEY,          -- 主键，自增
    [Username] NVARCHAR(100) NOT NULL,               -- 用户名
    [PasswordHash] NVARCHAR(200) NOT NULL,           -- 加密密码
    [Email] NVARCHAR(150) NULL,                      -- 邮箱（可为空）
    [Address] NVARCHAR(200) NULL                     -- 地址（可为空）
);
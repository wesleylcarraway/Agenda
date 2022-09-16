IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [InteractionTypes] (
    [id] int NOT NULL,
    [name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_InteractionTypes] PRIMARY KEY ([id])
);
GO

CREATE TABLE [PhoneTypes] (
    [id] int NOT NULL,
    [name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_PhoneTypes] PRIMARY KEY ([id])
);
GO

CREATE TABLE [UserRoles] (
    [id] int NOT NULL,
    [name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([id])
);
GO

CREATE TABLE [tb_user] (
    [id] int NOT NULL IDENTITY,
    [name] varchar(50) NOT NULL,
    [userName] varchar(50) NOT NULL,
    [email] nvarchar(50) NOT NULL,
    [password] nvarchar(50) NOT NULL,
    [UserRoleId] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_tb_user] PRIMARY KEY ([id]),
    CONSTRAINT [FK_tb_user_UserRoles_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [UserRoles] ([id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [tb_contact] (
    [id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [name] varchar(50) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_tb_contact] PRIMARY KEY ([id]),
    CONSTRAINT [FK_tb_contact_tb_user_UserId] FOREIGN KEY ([UserId]) REFERENCES [tb_user] ([id]) ON DELETE CASCADE
);
GO

CREATE TABLE [tb_interaction] (
    [id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [InteractionTypeId] int NOT NULL,
    [message] nvarchar(200) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_tb_interaction] PRIMARY KEY ([id]),
    CONSTRAINT [FK_tb_interaction_InteractionTypes_InteractionTypeId] FOREIGN KEY ([InteractionTypeId]) REFERENCES [InteractionTypes] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_tb_interaction_tb_user_UserId] FOREIGN KEY ([UserId]) REFERENCES [tb_user] ([id]) ON DELETE CASCADE
);
GO

CREATE TABLE [tb_phone] (
    [id] int NOT NULL IDENTITY,
    [number] nvarchar(9) NOT NULL,
    [ddd] int NOT NULL,
    [formattedNumber] nvarchar(15) NOT NULL,
    [description] nvarchar(100) NOT NULL,
    [ContactId] int NOT NULL,
    [PhoneTypeId] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_tb_phone] PRIMARY KEY ([id]),
    CONSTRAINT [FK_tb_phone_PhoneTypes_PhoneTypeId] FOREIGN KEY ([PhoneTypeId]) REFERENCES [PhoneTypes] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_tb_phone_tb_contact_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [tb_contact] ([id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'name') AND [object_id] = OBJECT_ID(N'[InteractionTypes]'))
    SET IDENTITY_INSERT [InteractionTypes] ON;
INSERT INTO [InteractionTypes] ([id], [name])
VALUES (1, N'Create Contact'),
(2, N'Update Contact'),
(3, N'Delete Contact'),
(4, N'View Contact'),
(5, N'View Phones');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'name') AND [object_id] = OBJECT_ID(N'[InteractionTypes]'))
    SET IDENTITY_INSERT [InteractionTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'name') AND [object_id] = OBJECT_ID(N'[PhoneTypes]'))
    SET IDENTITY_INSERT [PhoneTypes] ON;
INSERT INTO [PhoneTypes] ([id], [name])
VALUES (1, N'Residencial'),
(2, N'Cellphone'),
(3, N'Commercial');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'name') AND [object_id] = OBJECT_ID(N'[PhoneTypes]'))
    SET IDENTITY_INSERT [PhoneTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'name') AND [object_id] = OBJECT_ID(N'[UserRoles]'))
    SET IDENTITY_INSERT [UserRoles] ON;
INSERT INTO [UserRoles] ([id], [name])
VALUES (1, N'Admin'),
(2, N'Common');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'name') AND [object_id] = OBJECT_ID(N'[UserRoles]'))
    SET IDENTITY_INSERT [UserRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'CreatedAt', N'email', N'name', N'password', N'UpdatedAt', N'UserRoleId', N'userName') AND [object_id] = OBJECT_ID(N'[tb_user]'))
    SET IDENTITY_INSERT [tb_user] ON;
INSERT INTO [tb_user] ([id], [CreatedAt], [email], [name], [password], [UpdatedAt], [UserRoleId], [userName])
VALUES (1, '2022-05-30T00:00:00.0000000', N'admin@api.com', 'Admin Root Application', N'AQAAAAEAAAPoAAAAEEJ13zhhdMei3q0owYMX/KTStFyUezC+zs/Aov/a92JUOq8Vke7ehcX0/zxo8g76pA==', '0001-01-01T00:00:00.0000000', 1, 'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'CreatedAt', N'email', N'name', N'password', N'UpdatedAt', N'UserRoleId', N'userName') AND [object_id] = OBJECT_ID(N'[tb_user]'))
    SET IDENTITY_INSERT [tb_user] OFF;
GO

CREATE INDEX [IX_tb_contact_UserId] ON [tb_contact] ([UserId]);
GO

CREATE INDEX [IX_tb_interaction_InteractionTypeId] ON [tb_interaction] ([InteractionTypeId]);
GO

CREATE INDEX [IX_tb_interaction_UserId] ON [tb_interaction] ([UserId]);
GO

CREATE INDEX [IX_tb_phone_ContactId] ON [tb_phone] ([ContactId]);
GO

CREATE INDEX [IX_tb_phone_PhoneTypeId] ON [tb_phone] ([PhoneTypeId]);
GO

CREATE INDEX [IX_tb_user_UserRoleId] ON [tb_user] ([UserRoleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220602180725_First_Migration', N'6.0.5');
GO

COMMIT;
GO


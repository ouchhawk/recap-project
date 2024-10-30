CREATE TABLE [dbo].[Brands] (
    [id]   INT           NOT NULL,
    [name] VARCHAR (255) NULL,
    CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[Cars] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [BrandId]     INT           NULL,
    [ColorId]     INT           NULL,
    [DailyPrice]  FLOAT (53)    NULL,
    [ModelYear]   VARCHAR (255) NULL,
    [Description] VARCHAR (255) NULL,
    CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cars_Brands] FOREIGN KEY ([BrandId]) REFERENCES [dbo].[Brands] ([id]),
    CONSTRAINT [FK_Cars_Colors] FOREIGN KEY ([ColorId]) REFERENCES [dbo].[Colors] ([id])
);

CREATE TABLE [dbo].[Colors] (
    [id]   INT           NOT NULL,
    [name] VARCHAR (255) NULL,
    CONSTRAINT [PK_Colors] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[Customers] (
    [Id]          INT           NOT NULL,
    [UserId]      INT           NULL,
    [CompanyName] VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Customers_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[Rentals] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [CarId]      INT      NULL,
    [CustomerId] INT      NULL,
    [RentDate]   DATETIME NULL,
    [ReturnDate] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Rentals_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]),
    CONSTRAINT [FK_Rentals_Cars] FOREIGN KEY ([CarId]) REFERENCES [dbo].[Cars] ([Id])
);

CREATE TABLE [dbo].[Users] (
    [Id]        INT           NOT NULL,
    [FirstName] VARCHAR (255) NULL,
    [LastName]  VARCHAR (255) NULL,
    [Email]     VARCHAR (255) NULL,
    [Password]  VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[CarImages]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CarId] INT NULL, 
    [ImagePath] VARCHAR(100) NULL, 
    [Date] DATETIME NULL, 
    CONSTRAINT [FK_CarImages_Cars] FOREIGN KEY ([CarId]) REFERENCES [Cars]([Id])
)

-- Bảng UserRoles
CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL
);

-- Bảng Users
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    RoleId INT,
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

-- Bảng Categories
CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(50) NOT NULL
);

-- Bảng Tasks
CREATE TABLE Tasks (
    TaskId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    IsCompleted BIT DEFAULT 0,
    DueDate DATETIME,
    CreatedDate DATETIME DEFAULT GETDATE(),
    Priority INT CHECK (Priority BETWEEN 1 AND 3),
    CategoryId INT,
    UserId INT,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
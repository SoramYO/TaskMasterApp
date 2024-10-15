-- Tạo cơ sở dữ liệu TaskMaster
CREATE DATABASE IF NOT EXISTS TaskMaster;
USE TaskMaster;
-- Bảng Users
CREATE TABLE IF NOT EXISTS Users (
    user_id INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) UNIQUE NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
CREATE TABLE Roles (
    role_id INT PRIMARY KEY IDENTITY(1, 1),
    role_name NVARCHAR(50) UNIQUE NOT NULL
);
CREATE TABLE UserRoles (
    user_id INT,
    role_id INT,
    PRIMARY KEY (user_id, role_id),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (role_id) REFERENCES Roles(role_id)
);
-- Bảng Spaces
CREATE TABLE IF NOT EXISTS Spaces (
    space_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL,
    description TEXT,
    background_type ENUM('Video', 'Image', 'Color') NOT NULL,
    background_url VARCHAR(255),
    background_color VARCHAR(7),
    is_public BOOLEAN DEFAULT FALSE,
    created_by INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (created_by) REFERENCES Users(user_id)
);
-- Bảng UserSpaces
CREATE TABLE IF NOT EXISTS UserSpaces (
    user_id INT,
    space_id INT,
    is_favorite BOOLEAN DEFAULT FALSE,
    last_used TIMESTAMP,
    PRIMARY KEY (user_id, space_id),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (space_id) REFERENCES Spaces(space_id)
);
-- Bảng TodoLists
CREATE TABLE IF NOT EXISTS TodoLists (
    list_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    space_id INT,
    title VARCHAR(100) NOT NULL,
    description TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (space_id) REFERENCES Spaces(space_id)
);
-- Bảng Tags
CREATE TABLE IF NOT EXISTS Tags (
    tag_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    emoji VARCHAR(10),
    color VARCHAR(7),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
-- Bảng TodoItems
CREATE TABLE IF NOT EXISTS TodoItems (
    item_id INT PRIMARY KEY AUTO_INCREMENT,
    list_id INT,
    title VARCHAR(100) NOT NULL,
    description TEXT,
    due_date DATE,
    priority ENUM('Low', 'Medium', 'High') DEFAULT 'Medium',
    status ENUM(
        'Incomplete',
        'Complete',
        'Overdue',
        'Unscheduled'
    ) DEFAULT 'Incomplete',
    progress INT DEFAULT 0,
    is_outdoor BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    completed_at TIMESTAMP NULL,
    FOREIGN KEY (list_id) REFERENCES TodoLists(list_id)
);
-- Bảng TodoItems_Tags
CREATE TABLE IF NOT EXISTS TodoItems_Tags (
    item_id INT,
    tag_id INT,
    PRIMARY KEY (item_id, tag_id),
    FOREIGN KEY (item_id) REFERENCES TodoItems(item_id),
    FOREIGN KEY (tag_id) REFERENCES Tags(tag_id)
);
-- Bảng Notes
CREATE TABLE IF NOT EXISTS Notes (
    note_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    title VARCHAR(100) NOT NULL,
    content TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);
-- Bảng AI_Suggestions
CREATE TABLE IF NOT EXISTS AI_Suggestions (
    suggestion_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    suggestion_text TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_applied BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);
-- Bảng FocusMedia
CREATE TABLE IF NOT EXISTS FocusMedia (
    media_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    media_type ENUM('YouTube', 'Spotify', 'Other') NOT NULL,
    media_link VARCHAR(255) NOT NULL,
    title VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);
-- Bảng Pomodoro_Sessions
CREATE TABLE IF NOT EXISTS Pomodoro_Sessions (
    session_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    item_id INT,
    media_id INT,
    space_id INT,
    start_time TIMESTAMP,
    end_time TIMESTAMP,
    duration INT,
    is_completed BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (item_id) REFERENCES TodoItems(item_id),
    FOREIGN KEY (media_id) REFERENCES FocusMedia(media_id),
    FOREIGN KEY (space_id) REFERENCES Spaces(space_id)
);
-- Bảng Shared_Lists
CREATE TABLE IF NOT EXISTS Shared_Lists (
    share_id INT PRIMARY KEY AUTO_INCREMENT,
    list_id INT,
    shared_by INT,
    shared_with INT,
    permissions ENUM('read', 'write', 'admin') DEFAULT 'read',
    shared_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (list_id) REFERENCES TodoLists(list_id),
    FOREIGN KEY (shared_by) REFERENCES Users(user_id),
    FOREIGN KEY (shared_with) REFERENCES Users(user_id)
);
-- Bảng Inspirational_Quotes
CREATE TABLE IF NOT EXISTS Inspirational_Quotes (
    quote_id INT PRIMARY KEY AUTO_INCREMENT,
    quote_text TEXT,
    author VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
-- Bảng Deep_Focus_Sessions
CREATE TABLE IF NOT EXISTS Deep_Focus_Sessions (
    session_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    space_id INT,
    start_time TIMESTAMP,
    end_time TIMESTAMP,
    duration INT,
    is_completed BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (space_id) REFERENCES Spaces(space_id)
);
-- Bảng Mood_Entries
CREATE TABLE IF NOT EXISTS Mood_Entries (
    entry_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    item_id INT,
    mood ENUM(
        'Very Bad',
        'Bad',
        'Neutral',
        'Good',
        'Very Good'
    ),
    notes TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (item_id) REFERENCES TodoItems(item_id)
);
-- Bảng Weather_Data
CREATE TABLE IF NOT EXISTS Weather_Data (
    weather_id INT PRIMARY KEY AUTO_INCREMENT,
    location VARCHAR(100),
    forecast TEXT,
    temperature FLOAT,
    humidity INT,
    wind_speed FLOAT,
    date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
-- Bảng SpaceElements
CREATE TABLE IF NOT EXISTS SpaceElements (
    element_id INT PRIMARY KEY AUTO_INCREMENT,
    space_id INT,
    element_type ENUM('Widget', 'Decoration', 'Sound') NOT NULL,
    name VARCHAR(100),
    content TEXT,
    position_x INT,
    position_y INT,
    size_width INT,
    size_height INT,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (space_id) REFERENCES Spaces(space_id)
);
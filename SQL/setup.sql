
CREATE DATABASE DB_Mood;
GO

USE DB_Mood;
GO

CREATE TABLE account (
    id_account INT PRIMARY KEY,
    mail_account NVARCHAR(255) not null,
    phone_number_account NVARCHAR(255),
    birth_date_account DATE not null,
    name_account NVARCHAR(255) not null,
    description_account NVARCHAR(255)
);
GO
CREATE TABLE user (
    id_user INT PRIMARY KEY,
    login_user NVARCHAR(255) UNIQUE not null,
    password_user NVARCHAR(255) not null,
    role_user INT not null,
    titre_user NVARCHAR(255),
    id_account INT FOREIGN KEY REFERENCES account(id_account)
);
GO
CREATE TABLE friend (
    id_account INT FOREIGN KEY REFERENCES account(id_account) not null,
    id_friend INT FOREIGN KEY REFERENCES account(id_account) not null
);
GO
CREATE TABLE groupe(
    id_groupe INT PRIMARY KEY,
    name_groupe NVARCHAR(255)
);
GO
CREATE TABLE User_Groupe(
    id_user INT FOREIGN KEY REFERENCES user(id_user) not null,
    id_groupe INT FOREIGN KEY REFERENCES groupe(id_groupe) not null
);
GO
CREATE TABLE txt_msg(
    id_txtMsg INT PRIMARY KEY,
    content_txtMsg NVARCHAR(255) not null,
    send_date_txtMsg DATETIME not null,
    id_user_group INT FOREIGN KEY REFERENCES User_Groupe(id_user)
);
GO
CREATE TABLE msg(
    id_msg INT PRIMARY KEY,
    send_date_msg DATETIME not null,
);
GO
CREATE TABLE friend_request_msg(
    id_friendReqMsg INT PRIMARY KEY,
    id_msg INT FOREIGN KEY REFERENCES msg(id_msg),
    id_user INT FOREIGN KEY REFERENCES user(id_user),
    id_friend INT FOREIGN KEY REFERENCES user(id_user)
);
GO
CREATE TABLE publication(
    id_pub INT PRIMARY KEY,
    content_pub NVARCHAR(255) not null,
);
GO
CREATE TABLE photo_pub (
    id_photo_pub INT PRIMARY KEY,
    extention_photo_pub NVARCHAR(16),
    content_photo_pub NVARCHAR(255),
    id_pub INT FOREIGN KEY REFERENCES publication(id_pub)
);
GO

CREATE TABLE video_pub (
    id_photo_pub INT PRIMARY KEY,
    extention_video_pub NVARCHAR(16),
    content_video_pub NVARCHAR(255),
    id_pub INT FOREIGN KEY REFERENCES publication(id_pub)
);
GO
CREATE TABLE survey_pub (
    id_survey_pub INT PRIMARY KEY,
    content_survey_pub NVARCHAR(255),
    id_pub INT FOREIGN KEY REFERENCES publication(id_pub)
);
GO

CREATE TABLE comment (
    id_comment INT PRIMARY KEY,
    date_comment DATETIME,
    content_comment NVARCHAR(255),
    id_pub INT FOREIGN KEY REFERENCES publication(id_pub),
    id_account INT FOREIGN KEY REFERENCES user(id_user)
);
GO
CREATE TABLE liked (
    id_like INT PRIMARY KEY,
    date_like DATETIME,
    apreciation INT,
    id_pub INT FOREIGN KEY REFERENCES publication(id_pub)
);
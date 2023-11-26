
CREATE DATABASE DB_Mood;
GO

USE DB_Mood;
GO

CREATE TABLE accounts (
    acc_id INT PRIMARY KEY,
    acc_phone_number NVARCHAR(255),
    acc_birth_date DATE not null,
    acc_description NVARCHAR(255)
);

CREATE TABLE users (
    user_id INT PRIMARY KEY,
    user_mail NVARCHAR(255) not null,
    user_login NVARCHAR(255) UNIQUE not null,
    user_name NVARCHAR(255) not null,
    user_password NVARCHAR(255) not null,
    user_role INT not null,
    user_title NVARCHAR(255),
    acc_id INT FOREIGN KEY REFERENCES accounts(acc_id)
);

CREATE TABLE friends (
    acc_id INT FOREIGN KEY REFERENCES accounts(acc_id) not null,
    friend_id INT FOREIGN KEY REFERENCES accounts(acc_id) not null
);

CREATE TABLE groups (
    group_id INT PRIMARY KEY,
    group_name NVARCHAR(255)
);

CREATE TABLE user_groups (
    user_group_id INT PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES users(user_id) not null,
    group_id INT FOREIGN KEY REFERENCES groups(group_id) not null
);

CREATE TABLE messages (
    msg_id INT PRIMARY KEY,
    msg_date DATETIME not null,
);

CREATE TABLE text_messages (
    txtMsg_id INT PRIMARY KEY,
    txtMsg_content NVARCHAR(255) not null,
    user_id INT FOREIGN KEY REFERENCES user_groups(user_group_id),
    msg_id INT FOREIGN KEY REFERENCES messages(msg_id)
);

CREATE TABLE friend_request_messages (
    reqMsg_id INT PRIMARY KEY,
    msg_id INT FOREIGN KEY REFERENCES messages(msg_id),
    user_id INT FOREIGN KEY REFERENCES users(user_id),
    friend_id INT FOREIGN KEY REFERENCES users(user_id)
);

CREATE TABLE publications (
    pub_id INT PRIMARY KEY,
    pub_content NVARCHAR(255) not null,
);

CREATE TABLE photo_publications (
    photoPub_id INT PRIMARY KEY,
    photoPub_extention NVARCHAR(16),
    photoPub_content NVARCHAR(255),
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id)
);

CREATE TABLE video_publications (
    videoPub_id INT PRIMARY KEY,
    videoPub_extention NVARCHAR(16),
    videoPub_content NVARCHAR(255),
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id)
);

CREATE TABLE survey_publications (
    surveyPub_id INT PRIMARY KEY,
    surveyPub_content NVARCHAR(255),
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id)
);

CREATE TABLE comments (
    cmt_id INT PRIMARY KEY,
    cmt_date DATETIME,
    cmt_content NVARCHAR(255),
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id),
    acc_id INT FOREIGN KEY REFERENCES users(user_id)
);

CREATE TABLE likes (
    like_id INT PRIMARY KEY,
    like_date DATETIME,
    like_apreciation INT,
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id)
);

GO
INSERT INTO accounts VALUES (1, '0600000000', '1990-01-01', 'Compte 1');
INSERT INTO accounts VALUES (2, '0611111111', '1990-01-01', 'Compte 2');

GO
INSERT INTO users VALUES (1, 'user1@mail.com', 'login1', 'User 1', 'password1', 1, 'Title 1', 1);
INSERT INTO users VALUES (2, 'user2@mail.com', 'login2', 'User 2', 'password2', 1, 'Title 2', 2);


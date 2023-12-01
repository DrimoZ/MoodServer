
CREATE DATABASE DB_Mood;

GO
USE DB_Mood;

GO
CREATE TABLE accounts (
    acc_id CHAR(32) PRIMARY KEY,
    acc_phone_number NVARCHAR(255),
    acc_birth_date DATE NOT NULL,
    acc_description NVARCHAR(255),
    acc_isDeleted BIT NOT NULL DEFAULT 0,
);

CREATE TABLE users (
    user_id CHAR(32) PRIMARY KEY,
    user_mail NVARCHAR(255) UNIQUE NOT NULL,
    user_login NVARCHAR(255) UNIQUE NOT NULL,
    user_name NVARCHAR(255) NOT NULL,
    user_password NVARCHAR(255) NOT NULL,
    user_role INT NOT NULL,
    user_title NVARCHAR(255),
    user_isDeleted BIT NOT NULL DEFAULT 0,
    acc_id CHAR(32) FOREIGN KEY REFERENCES accounts(acc_id)
);

CREATE TABLE friends (
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id),
    friend_id CHAR(32) FOREIGN KEY REFERENCES users(user_id)
);

CREATE TABLE groups (
    group_id CHAR(32) PRIMARY KEY,
    group_isDeleted BIT NOT NULL DEFAULT 0,
    group_name NVARCHAR(255)
);

CREATE TABLE user_groups (
    user_group_id CHAR(32) PRIMARY KEY,
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id),
    group_id CHAR(32) FOREIGN KEY REFERENCES groups(group_id)
);

CREATE TABLE communications (
    comm_id CHAR(32) PRIMARY KEY,
    comm_date DATETIME NOT NULL,
    comm_isDeleted BIT NOT NULL DEFAULT 0
);

CREATE TABLE messages (
    msg_id CHAR(32) PRIMARY KEY,
    msg_content NVARCHAR(255) NOT NULL,
    user_id CHAR(32) FOREIGN KEY REFERENCES user_groups(user_group_id),
    comm_id CHAR(32) FOREIGN KEY REFERENCES communications(comm_id)
);

CREATE TABLE friend_requests (
    req_id CHAR(32) PRIMARY KEY,
    comm_id CHAR(32) FOREIGN KEY REFERENCES communications(comm_id),
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id),
    friend_id CHAR(32) FOREIGN KEY REFERENCES users(user_id)
);

CREATE TABLE publications (
    pub_id CHAR(32) PRIMARY KEY,
    pub_content NVARCHAR(255) NOT NULL,
    pub_date DATETIME NOT NULL,
    pub_isDeleted BIT NOT NULL DEFAULT 0,
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id)
);

CREATE TABLE photo_publications (
    photoPub_id CHAR(32) PRIMARY KEY,
    photoPub_extention NVARCHAR(16) NOT NULL,
    photoPub_content NVARCHAR(255) NOT NULL,
    pub_id CHAR(32) FOREIGN KEY REFERENCES publications(pub_id)
);

CREATE TABLE video_publications (
    videoPub_id CHAR(32) PRIMARY KEY,
    videoPub_extention NVARCHAR(16) NOT NULL,
    videoPub_content NVARCHAR(255) NOT NULL,
    pub_id CHAR(32) FOREIGN KEY REFERENCES publications(pub_id)
);

CREATE TABLE survey_publications (
    surveyPub_id CHAR(32) PRIMARY KEY,
    surveyPub_content NVARCHAR(255) NOT NULL,
    pub_id CHAR(32) FOREIGN KEY REFERENCES publications(pub_id)
);

CREATE TABLE comments (
    cmt_id CHAR(32) PRIMARY KEY,
    cmt_date DATETIME NOT NULL,
    cmt_content NVARCHAR(255) NOT NULL,
    cmt_isDeleted BIT NOT NULL DEFAULT 0,
    pub_id CHAR(32) FOREIGN KEY REFERENCES publications(pub_id),
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id),
);

CREATE TABLE likes (
    like_id CHAR(32) PRIMARY KEY,
    like_date DATETIME NOT NULL,
    like_type INT NOT NULL,
    pub_id CHAR(32) FOREIGN KEY REFERENCES publications(pub_id),
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id)
);

GO
INSERT INTO accounts VALUES ('ku784AJalJIwx98kPxQPM6QeLyuPgrno', '0600000000', '1990-01-01', 'Compte 1', 0);
INSERT INTO accounts VALUES ('fQPBu7Dtuqn32rAp7ocsonMiK6MAkOoE', '0611111111', '1990-01-01', 'Compte 2', 0);


INSERT INTO users VALUES ('lXLVeLbaid03vOItRZP11EWdzhq2k7YH', 'user1@mail.com', 'login1', 'User 1',
    '$2a$11$SKVecUmTEzKAF43qk.QsFuziowabj9HKHpp5UkGacGb./YI6/7Yaa', 1, 'Title 1',0, 'ku784AJalJIwx98kPxQPM6QeLyuPgrno');
INSERT INTO users VALUES ('lSELhMwz5sB3mvcOwvHQKzGzhmqk5D0t', 'user2@mail.com', 'login2', 'User 2',
    '$2a$11$40l2odRdREdQrMK75k57euzAHPmHsEGIb7SL8zEnXjzeAdDI1hvAS', 1, 'Title 2',0, 'fQPBu7Dtuqn32rAp7ocsonMiK6MAkOoE');

INSERT INTO publications VALUES ('au784AJalJIwx98kPxQPM6QeLyuPgrno', 'content 1', '1990-01-02',0, 'lXLVeLbaid03vOItRZP11EWdzhq2k7YH');
INSERT INTO publications VALUES ('aa784AJalJIwx98kPxQPM6QeLyuPgrno', 'content 3', '1990-01-03',0, 'lXLVeLbaid03vOItRZP11EWdzhq2k7YH');
INSERT INTO publications VALUES ('aaPBu7Dtuqn32rAp7ocsonMiK6MAkOoE', 'content 2', '1990-01-02',0, 'lSELhMwz5sB3mvcOwvHQKzGzhmqk5D0t');


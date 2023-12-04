
CREATE DATABASE DB_Mood;

GO
USE DB_Mood;

GO
CREATE TABLE accounts (
    acc_id CHAR(32) PRIMARY KEY,
    acc_phone_number NVARCHAR(255),
    acc_birth_date DATE NOT NULL,
    acc_description NVARCHAR(255)
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
    group_id INT PRIMARY KEY IDENTITY,
    group_isDeleted BIT NOT NULL DEFAULT 0,
    group_name NVARCHAR(255)
);

CREATE TABLE user_groups (
    user_group_id INT PRIMARY KEY IDENTITY,
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id),
    group_id INT FOREIGN KEY REFERENCES groups(group_id)
);

CREATE TABLE communications (
    comm_id INT PRIMARY KEY IDENTITY,
    comm_date DATETIME NOT NULL,
    comm_isDeleted BIT NOT NULL DEFAULT 0
);

CREATE TABLE messages (
    msg_id INT PRIMARY KEY IDENTITY,
    msg_content NVARCHAR(255) NOT NULL,
    user_group_id INT FOREIGN KEY REFERENCES user_groups(user_group_id),
    comm_id INT FOREIGN KEY REFERENCES communications(comm_id)
);

CREATE TABLE friend_requests (
    req_id INT PRIMARY KEY IDENTITY,
    comm_id INT FOREIGN KEY REFERENCES communications(comm_id),
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id),
    friend_id CHAR(32) FOREIGN KEY REFERENCES users(user_id)
);

CREATE TABLE publications (
    pub_id INT PRIMARY KEY IDENTITY,
    pub_content NVARCHAR(255) NOT NULL,
    pub_date DATETIME NOT NULL,
    pub_isDeleted BIT NOT NULL DEFAULT 0,
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id)
);

CREATE TABLE photo_publications (
    photoPub_id INT PRIMARY KEY IDENTITY,
    photoPub_extention NVARCHAR(16) NOT NULL,
    photoPub_content NVARCHAR(255) NOT NULL,
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id)
);

CREATE TABLE video_publications (
    videoPub_id INT PRIMARY KEY IDENTITY,
    videoPub_extention NVARCHAR(16) NOT NULL,
    videoPub_content NVARCHAR(255) NOT NULL,
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id)
);

CREATE TABLE survey_publications (
    surveyPub_id INT PRIMARY KEY IDENTITY,
    surveyPub_content NVARCHAR(255) NOT NULL,
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id)
);

CREATE TABLE comments (
    cmt_id INT PRIMARY KEY IDENTITY,
    cmt_date DATETIME NOT NULL,
    cmt_content NVARCHAR(255) NOT NULL,
    cmt_isDeleted BIT NOT NULL DEFAULT 0,
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id),
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id),
);

CREATE TABLE likes (
    like_id INT PRIMARY KEY IDENTITY,
    like_date DATETIME NOT NULL,
    like_type INT NOT NULL,
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id),
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id)
);

GO
INSERT INTO accounts VALUES ('acc_ku784AJalJIkPxQPM6QeLyuPgrno', '0600000000', '1990-01-01', 'Compte 1');
INSERT INTO accounts VALUES ('acc_f7Dtuqn32rAp7ocsonMiK6MAkOoE', '0611111111', '1990-01-01', 'Mec trop chaud au lit');
INSERT INTO accounts VALUES ('acc_caTaXKCD7D9AgGAQxT9kPOxxxIna', '0633333333', '2000-01-01', 'Yo moi cest mod3 : ) ');
INSERT INTO accounts VALUES ('acc_8vLHdpRjrqF_RRM2NJcVEFqmZkGt', '0633333333', '2003-01-01', 'Je stream tous les jours sur Womix, rejoignez moi !');

INSERT INTO users VALUES ('usr_lXLVeLbaid03vOItRZP11EWdzhqH', 'user1@mail.com', 'login1', 'Arthur', '$2a$11$SKVecUmTEzKAF43qk.QsFuziowabj9HKHpp5UkGacGb./YI6/7Yaa', 1, 'Title 1',0, 'acc_ku784AJalJIkPxQPM6QeLyuPgrno'); --password1
INSERT INTO users VALUES ('usr_lSELhMwz5sB3mvcOwvHQKzqk5D0t', 'user2@mail.com', 'login2', 'UserAAAA', '$2a$11$40l2odRdREdQrMK75k57euzAHPmHsEGIb7SL8zEnXjzeAdDI1hvAS', 1, 'Title 2',0, 'acc_f7Dtuqn32rAp7ocsonMiK6MAkOoE'); --password2
INSERT INTO users VALUES ('usr_VpKvQZ6zpEJt9y2ykFptGRX4gaSd', 'mod3@mail.com', 'login3', 'User 3', '$2a$11$40l2odRdREdQrMK75k57euzAHPmHsEGIb7SL8zEnXjzeAdDI1hvAS', 2, 'MegaTropFor',0, "acc_caTaXKCD7D9AgGAQxT9kPOxxxIna"); --password2
INSERT INTO users VALUES ('usr_8vLHdpRjrqF_RRM2NJcVEFqmZkGt', 'cestmoihaha@me.be', 'coucou', 'Theo Mille', '$2a$11$hbAXocUS0vEih436Lw8ddeaVN21kvdkHDruRyv/CTir2B1ZbJvuXW', 1, 'Verified',0, "acc_8vLHdpRjrqF_RRM2NJcVEFqmZkGt"); --Strong#1



INSERT INTO friends VALUES ('usr_lSELhMwz5sB3mvcOwvHQKzqk5D0t', 'usr_lXLVeLbaid03vOItRZP11EWdzhqH');
INSERT INTO friends VALUES ('usr_lSELhMwz5sB3mvcOwvHQKzqk5D0t', 'usr_VpKvQZ6zpEJt9y2ykFptGRX4gaSd');
INSERT INTO friends VALUES ('usr_lXLVeLbaid03vOItRZP11EWdzhqH', 'usr_lSELhMwz5sB3mvcOwvHQKzqk5D0t');
INSERT INTO friends VALUES ('usr_VpKvQZ6zpEJt9y2ykFptGRX4gaSd', 'usr_lSELhMwz5sB3mvcOwvHQKzqk5D0t');
INSERT INTO friends VALUES ('usr_lXLVeLbaid03vOItRZP11EWdzhqH', 'usr_VpKvQZ6zpEJt9y2ykFptGRX4gaSd');
INSERT INTO friends VALUES ('usr_VpKvQZ6zpEJt9y2ykFptGRX4gaSd', 'usr_lXLVeLbaid03vOItRZP11EWdzhqH');
INSERT INTO friends VALUES ('usr_lXLVeLbaid03vOItRZP11EWdzhqH', 'usr_8vLHdpRjrqF_RRM2NJcVEFqmZkGt');
INSERT INTO friends VALUES ('usr_8vLHdpRjrqF_RRM2NJcVEFqmZkGt', 'usr_lXLVeLbaid03vOItRZP11EWdzhqH');



INSERT INTO publications(pub_content, pub_date, user_id) VALUES ('content 1', '1990-01-02', 'usr_lXLVeLbaid03vOItRZP11EWdzhqH');
INSERT INTO publications(pub_content, pub_date, user_id) VALUES ( 'content 3', '1990-01-03', 'usr_lXLVeLbaid03vOItRZP11EWdzhqH');
INSERT INTO publications(pub_content, pub_date, user_id) VALUES ( 'content 2', '1990-01-02', 'usr_lSELhMwz5sB3mvcOwvHQKzqk5D0t');


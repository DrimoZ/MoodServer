
CREATE DATABASE DB_Mood;

GO
USE DB_Mood;

GO
CREATE TABLE images (
    img_id INT PRIMARY KEY IDENTITY,
    img_data varbinary(MAX) NOT NULL,
    img_date DATETIME DEFAULT (GETDATE())
);

CREATE TABLE accounts (
    acc_id CHAR(32) PRIMARY KEY,
    acc_phone_number NVARCHAR(255),
    acc_birth_date DATE NOT NULL,
    acc_description NVARCHAR(255),
    img_id INT FOREIGN KEY REFERENCES images(img_id) DEFAULT NULL
);

CREATE TABLE users (
    user_id CHAR(32) PRIMARY KEY,
    user_mail NVARCHAR(255) NOT NULL,
    user_login NVARCHAR(255) NOT NULL,
    user_name NVARCHAR(255) NOT NULL,
    user_password NVARCHAR(255) NOT NULL,
    user_role INT NOT NULL,
    user_title NVARCHAR(255),
    user_isDeleted BIT NOT NULL DEFAULT 0,
    user_isPublic BIT NOT NULL DEFAULT 1,
    user_isFriendPublic BIT NOT NULL DEFAULT 1,
    user_isPublicationPublic BIT NOT NULL DEFAULT 1,
    acc_id CHAR(32) FOREIGN KEY REFERENCES accounts(acc_id)
);

CREATE TABLE friends (
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id),
    friend_id CHAR(32) FOREIGN KEY REFERENCES users(user_id)
);

CREATE TABLE groups (
    group_id INT PRIMARY KEY IDENTITY,
    group_isDeleted BIT NOT NULL DEFAULT 0,
    group_name NVARCHAR(255),
    group_isPrivate BIT NOT NULL,
    group_proprio_id CHAR(32) NULL FOREIGN KEY REFERENCES users(user_id)
);

CREATE TABLE user_groups (
    user_group_id INT PRIMARY KEY IDENTITY,
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id),
    group_id INT FOREIGN KEY REFERENCES groups(group_id) ON DELETE CASCADE,
    user_has_left BIT NOT NULL DEFAULT 0
);

CREATE TABLE messages (
    msg_id INT PRIMARY KEY IDENTITY,
    msg_content NVARCHAR(255) NOT NULL,
    user_group_id INT FOREIGN KEY REFERENCES user_groups(user_group_id) ON DELETE CASCADE,
    msg_date DATETIME DEFAULT (GETDATE()),
    msg_isDeleted BIT NOT NULL DEFAULT 0
);

CREATE TABLE friend_requests (
    req_id INT PRIMARY KEY IDENTITY,
    req_date DATETIME DEFAULT (GETDATE()),
    req_isDone BIT NOT NULL DEFAULT 0,
    req_isAccepted BIT NOT NULL DEFAULT 0,
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id),
    friend_id CHAR(32) FOREIGN KEY REFERENCES users(user_id)
);

CREATE TABLE publications (
    pub_id INT PRIMARY KEY IDENTITY,
    pub_content NVARCHAR(255) NOT NULL,
    pub_date DATETIME DEFAULT (GETDATE()),
    pub_isDeleted BIT NOT NULL DEFAULT 0,
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id)
);

CREATE TABLE publication_elements (
    elmt_id INT PRIMARY KEY IDENTITY,
    img_id INT FOREIGN KEY REFERENCES images(img_id),
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id) ON DELETE CASCADE
);

CREATE TABLE comments (
    cmt_id INT PRIMARY KEY IDENTITY,
    cmt_date DATETIME DEFAULT (GETDATE()),
    cmt_content NVARCHAR(255) NOT NULL,
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id) ON DELETE CASCADE,
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id),
);

CREATE TABLE likes (
    like_id INT PRIMARY KEY IDENTITY,
    like_date DATETIME DEFAULT (GETDATE()),
    pub_id INT FOREIGN KEY REFERENCES publications(pub_id) ON DELETE CASCADE,
    user_id CHAR(32) FOREIGN KEY REFERENCES users(user_id)
);


-- GO
-- INSERT INTO accounts VALUES 
-- ('acc_ku784AJalJIkPxQPM6QeLyuPgrno', '901-234-5678', '1990-01-01', 'Compte 1', NULL),
-- ('acc_f7Dtuqn32rAp7ocsonMiK6MAkOoE', '012-345-6789', '1990-01-01', 'Mec trop chaud au lit', NULL),
-- ('acc_caTaXKCD7D9AgGAQxT9kPOxxxIna', '890-123-4567', '2000-01-01', 'Yo moi cest mod3 : ) ', NULL),
-- ('acc_8vLHdpRjrqF_RRM2NJcazeazZkGt', '234-567-8901', '2003-11-01', 'Je stream tous les jours sur Womix, rejoignez moi !', NULL),
-- ('acc_8vLHdpRjrRZEeaM2NJcVEFqmZkGt', '456-789-0123', '1991-02-02', 'Régulièrement des photos de ceux qui me sont passés dessus / dessous 🥸', NULL),
-- ('acc_8vLHdpRjrqFbn8HDZJcVEFqmZkGt', '567-890-1234', '1996-07-07', 'CEFUC 22', NULL),
-- ('acc_B_GSi7AQp4YEZ4puNIBE0fifSB3s', '678-901-2345', '1991-12-23', '14/11/2002 --> 21 ans Judo 🥋🥋 Coaster addict 🎢🎡', NULL),
-- ('acc_8vLHdpRazezasRM2NJcVEFqmZkGt', '789-012-3456', '1997-08-08', '🧑‍💻 Web Dev & Coding Content 📈 Level up your CSS Skills ✨ Get 100+ Ultimate CSS Tips and Tricks Ebook 👇 bit.ly/3OfA56R', NULL);

-- INSERT INTO users (user_id, user_mail, user_login, user_name, user_password, user_role, acc_id) VALUES 
-- ('usr_lXLVeLbaid03vOItRZP11EWdzhqH', 'user1@mail.com',      'login1',           'Arthur',                       '$2a$11$SKVecUmTEzKAF43qk.QsFuziowabj9HKHpp5UkGacGb./YI6/7Yaa', 1, 'acc_ku784AJalJIkPxQPM6QeLyuPgrno'), --password1
-- ('usr_lSELhMwz5sB3mvcOwvHQKzqk5D0t', 'user2@mail.com',      'login2',           'UserAAAA',                     '$2a$11$40l2odRdREdQrMK75k57euzAHPmHsEGIb7SL8zEnXjzeAdDI1hvAS', 1, 'acc_f7Dtuqn32rAp7ocsonMiK6MAkOoE'), --password2
-- ('usr_VpKvQZ6zpEJt9y2ykFptGRX4gaSd', 'cestmoihaha@me.be',   'cestmoi',          'Moi',                          '$2a$11$hbAXocUS0vEih436Lw8ddeaVN21kvdkHDruRyv/CTir2B1ZbJvuXW', 1, 'acc_caTaXKCD7D9AgGAQxT9kPOxxxIna'), --Strong#1
-- ('usr_8vLHdpRjrqF_RRM2NJcVEFqmZkGt', 'user3@example.com',   'user_33_deleted',  'User 33',                      '$2a$11$hbAXocUS0vEih436Lw8ddeaVN21kvdkHDruRyv/CTir2B1ZbJvuXW', 1, 'acc_8vLHdpRjrqF_RRM2NJcazeazZkGt'), --Strong#1
-- ('usr_8vLHdpRjrqF_RRM2azcVEFqmZkGt', 'marine@example.com',  'marine0023',       'Marine <3',                    '$2a$11$hbAXocUS0vEih436Lw8ddeaVN21kvdkHDruRyv/CTir2B1ZbJvuXW', 2, 'acc_8vLHdpRjrRZEeaM2NJcVEFqmZkGt'), --Strong#1
-- ('usr_lXLVeLbaid03azeItRP11EWdzhqH', 'yoplait@example.com', 'martinp',          'Martin Pecheur',               '$2a$11$hbAXocUS0vEih436Lw8ddeaVN21kvdkHDruRyv/CTir2B1ZbJvuXW', 3, 'acc_8vLHdpRjrqFbn8HDZJcVEFqmZkGt'), --Strong#1
-- ('usr_VpKvQZ6zpEzae9y2ykFptGRXgaSd', 'user6@example.com',   'engh_bxl',         'Enghiennoise De Bruxelles',    '$2a$11$hbAXocUS0vEih436Lw8ddeaVN21kvdkHDruRyv/CTir2B1ZbJvuXW', 2, 'acc_B_GSi7AQp4YEZ4puNIBE0fifSB3s'), --Strong#1
-- ('usr_lSELhMwezsB3mvcOwvHQKzqk5D0t', 'user7@example.com',   'frontend_champ',   'Front End Development',        '$2a$11$hbAXocUS0vEih436Lw8ddeaVN21kvdkHDruRyv/CTir2B1ZbJvuXW', 1, 'acc_8vLHdpRazezasRM2NJcVEFqmZkGt'); --Strong#1


-- GO
-- INSERT INTO friends VALUES ('usr_lSELhMwz5sB3mvcOwvHQKzqk5D0t', 'usr_lXLVeLbaid03vOItRZP11EWdzhqH');
-- INSERT INTO friends VALUES ('usr_lSELhMwz5sB3mvcOwvHQKzqk5D0t', 'usr_VpKvQZ6zpEJt9y2ykFptGRX4gaSd');
-- INSERT INTO friends VALUES ('usr_lXLVeLbaid03vOItRZP11EWdzhqH', 'usr_lSELhMwz5sB3mvcOwvHQKzqk5D0t');
-- INSERT INTO friends VALUES ('usr_VpKvQZ6zpEJt9y2ykFptGRX4gaSd', 'usr_lSELhMwz5sB3mvcOwvHQKzqk5D0t');
-- INSERT INTO friends VALUES ('usr_lXLVeLbaid03vOItRZP11EWdzhqH', 'usr_VpKvQZ6zpEJt9y2ykFptGRX4gaSd');
-- INSERT INTO friends VALUES ('usr_VpKvQZ6zpEJt9y2ykFptGRX4gaSd', 'usr_lXLVeLbaid03vOItRZP11EWdzhqH');
-- INSERT INTO friends VALUES ('usr_lXLVeLbaid03vOItRZP11EWdzhqH', 'usr_8vLHdpRjrqF_RRM2NJcVEFqmZkGt');
-- INSERT INTO friends VALUES ('usr_8vLHdpRjrqF_RRM2NJcVEFqmZkGt', 'usr_lXLVeLbaid03vOItRZP11EWdzhqH');
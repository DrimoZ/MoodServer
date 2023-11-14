-- Créez la base de données "todos"
CREATE DATABASE DB_Mood;
GO
-- Utilisez la base de données "todos"
USE DB_Mood;
GO
-- Créez la table "account"
CREATE TABLE account (
    id_account INT PRIMARY KEY,
    mail_account NVARCHAR(255),
    phone_number_account NVARCHAR(255),
    birth_date_account DATE,
    description NVARCHAR(255)
);
GO
CREATE TABLE user (
    id_user INT PRIMARY KEY,
    login_user NVARCHAR(255),
    password_user NVARCHAR(255),
);
GO
CREATE TABLE message(
    id_msg INT PRIMARY KEY,
    send_date_msg DATE,
    type INT,
    
);
GO
CREATE TABLE txt_message(
    id_txtMsg INT PRIMARY KEY,
    content_msg NVARCHAR(255)
);
GO
CREATE TABLE friend_request_message(
    id_friendReqMsg INT PRIMARY KEY
);
GO
CREATE TABLE user (
    id_user INT PRIMARY KEY,
    login_user NVARCHAR(255),
    password_user NVARCHAR(255),
);
GO

-- Insérez quelques lignes de données dans la table "items"
INSERT INTO items (id, description) VALUES (1, 'Faire les courses');
INSERT INTO items (id, description) VALUES (2, 'Terminer le projet');
INSERT INTO items (id, description) VALUES (3, 'Répondre aux e-mails');
INSERT INTO items (id, description) VALUES (4, "Faire de l'exercice");
INSERT INTO items (id, description) VALUES (5, 'Lire un livre');
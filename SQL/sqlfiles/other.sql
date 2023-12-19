USE DB_MOOD;
GO


-- ACCOUNTS
INSERT INTO ACCOUNTS (acc_id, acc_phone_number, acc_birth_date, acc_description, img_id) 
VALUES 
(N'acc_0P5by5krd0q5wW1sSp5n4bEz6SNP', null, N'1990-01-01', N'Mec trop chaud au lit', 2002),
(N'acc_ch54v0RL_ZhtnMknoh82Dfvzsr3_', null, N'1983-04-12', N'🧑‍💻 Web Dev & Coding Content 📈 Level up your CSS Skills ✨ Get 100+ Ultimate CSS Tips and Tricks Ebook 👇 bit.ly/3OfA56R', 2006),
(N'acc_cyYrHDrj12pywYLInXpdtz5PAcHr', null, N'1972-08-23', N'Belgique 🍟🍫🍻', 2008),
(N'acc_DOBadyzJbvZYNdvvnRTZ8rUnOfKU', null, N'2000-04-30', null, 2007),
(N'acc_M2n561_CM5PTeGn0XEh9LyI0OO2p', null, N'1990-01-01', null, null),
(N'acc_m7az90Rs2SGH5GRUI6QLqRS1zhsx', null, N'1990-01-01', null, null),
(N'acc_nLhmM2C1CuRFKnXalS2f4F8oow44', null, N'2001-12-31', null, null),
(N'acc_pn1JXApTYQE1smvMZUZXypmxHzX9', null, N'2001-01-01', N'CEFUC 22', 2004),
(N'acc_r4Mo4H0uYd7pfKEfxTauwTHlzniP', null, N'2000-11-02', N'Régulièrement des photos de ceux qui me sont passés dessus / dessous 🥸', 2003),
(N'acc_VFfu39dvIaPN3EEIxdlZVqqWKSGV', null, N'1990-01-01', N'The First. The Strongest.', 2001),
(N'acc_vQeoxnE1HcOdtgJl989W4s4DDCwZ', N'0489286339', N'2003-04-01', N'Aspirant Développeur - Contact : theomille1@gmail.com', 1),
(N'acc_xndoE_rsGdkuZ8YbGHjNl2s0LXn1', null, N'2009-02-23', N'Je stream tous les jours sur Kick, rejoignez moi !', null);

GO


-- USERS
INSERT INTO USERS (user_id, user_mail, user_login, user_name, user_password, user_role, user_title, user_isDeleted, user_isPublic, user_isFriendPublic, user_isPublicationPublic, acc_id) 
VALUES 
(N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah', N'user2@mail.com', N'login222', N'UserAAAA', N'$2a$11$MjN19juWm/6PuEF9ysqj9.tmml3c7HQf1JfJQ0IWgyVnAGFyIaCs6', 1, N'Musclé', 0, 1, 1, 0, N'acc_0P5by5krd0q5wW1sSp5n4bEz6SNP'),
(N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'theomille1@gmail.com', N'theom_01', N'Theo Mille', N'$2a$11$aPpmgaTVlp//sr.B2.4M5OoE4XGacyP4W7GtjUD.FAG7vDgdxI09S', 1, N'Verified', 0, 1, 0, 1, N'acc_vQeoxnE1HcOdtgJl989W4s4DDCwZ'),
(N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl', N'produfoot@live.be', N'akisekai', N'Louis Testolin', N'$2a$11$tlqtCMcbHz9gXsMYsSFU3ulebcUjhLXOAFxBqGaccMHSKRyghAbxm', 1, N'NaziMalgréLui', 0, 1, 1, 1, N'acc_DOBadyzJbvZYNdvvnRTZ8rUnOfKU'),
(N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', N'marine@defrance.com', N'marine0023', N'Marine <3', N'$2a$11$RAxHqyeURf80XZadno1SUOl8.VREvMHm.tr789YWJzA3KMhMEOWVq', 1, null, 0, 1, 1, 1, N'acc_r4Mo4H0uYd7pfKEfxTauwTHlzniP'),
(N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'yoplait@example.com', N'martin_p', N'Martin Pecheur', N'$2a$11$6UKm8Jc786NcIpuEH9ck8eC0nc0EqhcSOhJVVmNZCBmJbLVx/sjqC', 1, null, 0, 1, 1, 1, N'acc_nLhmM2C1CuRFKnXalS2f4F8oow44'),
(N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', N'user1@mail.com', N'login111', N'Arthur', N'$2a$11$khT276mDzLj1bpqsKtsQKOXZgOfuT9y01OQBlQWsdRhuF2sVEDk.y', 1, N'', 0, 1, 1, 1, N'acc_VFfu39dvIaPN3EEIxdlZVqqWKSGV'),
(N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre', N'enghien@bxl.be', N'engh_bxl', N'Enghiennoise De Bruxelles', N'$2a$11$Ynzc2gfRaJvITbI6q4rfWOg44ubgtsUuh9OGLnUAoiNN6I6T5pbCa', 1, null, 0, 1, 1, 1, N'acc_pn1JXApTYQE1smvMZUZXypmxHzX9'),
(N'usr_u9QsL12YlLzxvtEyqJerUeklQEam', N'frontend.dev@outlook.com', N'frontend_champ', N'Front End Development', N'$2a$11$YYp03JsPU1CS4O2u36ESQOUsAJnCGFQENw6BLjqXqiCtS2wz1vG5e', 1, null, 0, 1, 1, 1, N'acc_ch54v0RL_ZhtnMknoh82Dfvzsr3_'),
(N'usr_uBs5REXYpvO0y1hQbXTUcphbZqCN', N'user1@mail', N'login1àçé', N'Arthur', N'$2a$11$31AYyPjZ0v.TvwdeR0RPxuA6dCB.Q0kiy/CQoJrzXfQjP.QLExWHK', 1, null, 1, 1, 1, 1, N'acc_M2n561_CM5PTeGn0XEh9LyI0OO2p'),
(N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL', N'titilafrite@gmail.com', N'titi_le_belge', N'Titi', N'$2a$11$zuo2lF3TrW/QTQaPR48/WOFVjNXxWpBO6NjWdB4bvRSvf3VqKxyRu', 1, N'Belgian', 0, 1, 1, 1, N'acc_cyYrHDrj12pywYLInXpdtz5PAcHr'),
(N'usr_XjbecnC9jyqY6Riit96OC2ol5RtR', N'user1@mail', N'login111', N'Arthur', N'$2a$11$xNSt9uxeAuB8bID3A0jHOudSW7pVJZDmi5fw4fxgdc.xrz7BKIX0a', 1, null, 1, 1, 1, 1, N'acc_m7az90Rs2SGH5GRUI6QLqRS1zhsx'),
(N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq', N'deleted33@frenshy.fr', N'user_33_deleted', N'User 33', N'$2a$11$cxGdVBpNc4EtMwCb8/jvue.W7WsshUSM2/YsLRwbMVuuystZ2eTni', 1, null, 0, 1, 1, 1, N'acc_xndoE_rsGdkuZ8YbGHjNl2s0LXn1');

GO


-- FRIENDS
INSERT INTO FRIENDS (user_id, friend_id) 
VALUES 
(N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq'),
(N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah'),
(N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah', N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre'),
(N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre', N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah'),
(N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah', N'usr_9yWVHaHniTc9164fQJfzCMssOb4e'),
(N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah'),
(N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl', N'usr_9yWVHaHniTc9164fQJfzCMssOb4e'),
(N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(N'usr_u9QsL12YlLzxvtEyqJerUeklQEam', N'usr_9yWVHaHniTc9164fQJfzCMssOb4e'),
(N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_u9QsL12YlLzxvtEyqJerUeklQEam'),
(N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah'),
(N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre'),
(N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre', N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq', N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL'),
(N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq'),
(N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq'),
(N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl', N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', N'usr_9yWVHaHniTc9164fQJfzCMssOb4e'),
(N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre'),
(N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq', N'usr_u9QsL12YlLzxvtEyqJerUeklQEam'),
(N'usr_u9QsL12YlLzxvtEyqJerUeklQEam', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq'),
(N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq', N'usr_9yWVHaHniTc9164fQJfzCMssOb4e'),
(N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq');

GO


-- FRIEND REQUESTS
SET IDENTITY_INSERT FRIEND_REQUESTS ON;
INSERT INTO FRIEND_REQUESTS (req_id, req_date, req_isDone, req_isAccepted, user_id, friend_id) 
VALUES 
(1, N'2023-12-19 20:47:33.043', 1, 1, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(2, N'2023-12-19 22:03:55.203', 1, 1, N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(3, N'2023-12-19 22:03:55.913', 1, 1, N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(4, N'2023-12-19 22:05:15.070', 1, 1, N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(5, N'2023-12-19 22:05:15.787', 0, 0, N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq', N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah'),
(6, N'2023-12-19 22:05:16.330', 1, 1, N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(7, N'2023-12-19 22:16:28.007', 1, 1, N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(8, N'2023-12-19 22:16:28.640', 0, 0, N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah'),
(9, N'2023-12-19 22:16:29.033', 1, 1, N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(10, N'2023-12-19 22:22:41.307', 1, 1, N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(11, N'2023-12-19 22:22:42.263', 1, 1, N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre', N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(12, N'2023-12-19 22:22:43.120', 1, 1, N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre', N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah'),
(13, N'2023-12-19 22:25:40.453', 0, 0, N'usr_u9QsL12YlLzxvtEyqJerUeklQEam', N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah'),
(14, N'2023-12-19 22:25:41.373', 1, 0, N'usr_u9QsL12YlLzxvtEyqJerUeklQEam', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(15, N'2023-12-19 22:25:42.833', 1, 1, N'usr_u9QsL12YlLzxvtEyqJerUeklQEam', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq'),
(16, N'2023-12-19 22:30:13.613', 1, 1, N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl', N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(17, N'2023-12-19 22:30:16.883', 1, 1, N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(18, N'2023-12-19 22:38:32.750', 0, 0, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL'),
(19, N'2023-12-19 22:38:33.140', 0, 0, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre'),
(20, N'2023-12-19 22:38:33.543', 0, 0, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(21, N'2023-12-19 22:38:33.997', 0, 0, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(22, N'2023-12-19 22:38:34.670', 1, 1, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(23, N'2023-12-19 22:38:35.030', 1, 1, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(24, N'2023-12-19 22:38:35.453', 1, 1, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_u9QsL12YlLzxvtEyqJerUeklQEam'),
(25, N'2023-12-19 22:38:35.943', 1, 1, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq'),
(26, N'2023-12-19 22:38:36.733', 1, 1, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah'),
(27, N'2023-12-19 22:41:31.377', 0, 0, N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL', N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre'),
(28, N'2023-12-19 22:41:31.720', 1, 1, N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq');

SET IDENTITY_INSERT FRIEND_REQUESTS OFF;
GO


-- PUBLICATIONS
SET IDENTITY_INSERT PUBLICATIONS ON;
INSERT INTO PUBLICATIONS (pub_id, pub_content, pub_date, pub_isDeleted, user_id)
VALUES
(1, N'The Beginning After The End', N'2023-12-19 18:24:40.300', 0, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(2, N'Solo Leveling', N'2023-12-19 19:07:46.737', 0, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(3, N'Noblesse', N'2023-12-19 19:08:46.290', 0, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(4, N'Martial God Regressed To Level 2', N'2023-12-19 19:11:38.260', 0, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(5, N'The Eminence in Shadow', N'2023-12-19 19:12:26.797', 0, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(6, N'Martial Peak', N'2023-12-19 19:15:31.960', 0, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(7, N'Le comité', N'2023-12-19 22:23:01.643', 0, N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre');


SET IDENTITY_INSERT PUBLICATIONS OFF;
GO


-- PUBLICATIONS ELEMENTS
SET IDENTITY_INSERT PUBLICATION_ELEMENTS ON;
INSERT INTO PUBLICATION_ELEMENTS (elmt_id, img_id, pub_id)
VALUES
(1, 2, 1),
(2, 1001, 2),
(3, 1002, 3),
(4, 1003, 4),
(5, 1004, 4),
(6, 1005, 5),
(7, 1006, 6),
(8, 1007, 6),
(9, 1008, 6),
(10, 2005, 7);

SET IDENTITY_INSERT PUBLICATION_ELEMENTS OFF;
GO


-- COMMENTS
SET IDENTITY_INSERT COMMENTS ON;

INSERT INTO COMMENTS (cmt_id, cmt_date, cmt_content, pub_id, user_id) 
VALUES 
(1, N'2023-12-19 19:43:03.413', N'Sorry for the poor quality images ... Insane ones coming soon !!!', 6, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F');

SET IDENTITY_INSERT COMMENTS OFF;
GO


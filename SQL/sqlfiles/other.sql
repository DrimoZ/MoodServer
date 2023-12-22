USE DB_MOOD;
GO


-- ACCOUNTS
INSERT INTO ACCOUNTS (acc_id, acc_phone_number, acc_birth_date, acc_description, img_id) 
VALUES 
(N'acc_0P5by5krd0q5wW1sSp5n4bEz6SNP', null, N'1990-01-01', N'Mec trop chaud au lit', 2002),
(N'acc_9p5tLLnyR7WnnXlG5iKn_5IdDq5x', null, N'2003-09-03', N'J''en ai aucune idée', 3007),
(N'acc_ch54v0RL_ZhtnMknoh82Dfvzsr3_', null, N'1983-04-12', N'🧑‍💻 Web Dev & Coding Content 📈 Level up your CSS Skills ✨ Get 100+ Ultimate CSS Tips and Tricks Ebook 👇 bit.ly/3OfA56R', 2006),
(N'acc_cyYrHDrj12pywYLInXpdtz5PAcHr', null, N'1972-08-23', N'Belgique 🍟🍫🍻', 2008),
(N'acc_DOBadyzJbvZYNdvvnRTZ8rUnOfKU', null, N'2000-04-30', null, 2007),
(N'acc_EWkea49dUtSSgtof1ioZX8z91nO5', null, N'1990-01-01', N'azertyujk', null),
(N'acc_M2n561_CM5PTeGn0XEh9LyI0OO2p', null, N'1990-01-01', null, null),
(N'acc_m7az90Rs2SGH5GRUI6QLqRS1zhsx', null, N'1990-01-01', null, null),
(N'acc_nLhmM2C1CuRFKnXalS2f4F8oow44', null, N'2001-12-31', null, null),
(N'acc_NTNmZiDvjl6Ma9GUGhCbO52Qwtka', null, N'1997-08-23', null, null),
(N'acc_o8FyLRgjgViwNFASw0n8U_GlBM7P', null, N'1999-04-11', null, 4001),
(N'acc_pn1JXApTYQE1smvMZUZXypmxHzX9', null, N'2001-01-01', N'CEFUC 22', 2004),
(N'acc_r4Mo4H0uYd7pfKEfxTauwTHlzniP', null, N'2000-11-02', N'Photos régulières sur le compte.', 2003),
(N'acc_VFfu39dvIaPN3EEIxdlZVqqWKSGV', null, N'1990-01-01', N'The First. The Strongest.', 3004),
(N'acc_vQeoxnE1HcOdtgJl989W4s4DDCwZ', N'0489286339', N'2003-04-01', N'Aspirant Développeur - Contact : theomille1@gmail.com', 1),
(N'acc_xndoE_rsGdkuZ8YbGHjNl2s0LXn1', null, N'2009-02-23', N'Je stream tous les jours sur Kick, rejoignez moi !', null);

GO


-- USERS
INSERT INTO USERS (user_id, user_mail, user_login, user_name, user_password, user_role, user_title, user_isDeleted, user_isPublic, user_isFriendPublic, user_isPublicationPublic, acc_id) 
VALUES 
(N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah', N'user2@mail.com', N'login222', N'UserAAAA', N'$2a$11$MjN19juWm/6PuEF9ysqj9.tmml3c7HQf1JfJQ0IWgyVnAGFyIaCs6', 1, N'Musclé', 0, 1, 1, 0, N'acc_0P5by5krd0q5wW1sSp5n4bEz6SNP'),
(N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'theomille1@gmail.com', N'theom_01', N'Theo Mille', N'$2a$11$aPpmgaTVlp//sr.B2.4M5OoE4XGacyP4W7GtjUD.FAG7vDgdxI09S', 2, N'God', 0, 1, 0, 1, N'acc_vQeoxnE1HcOdtgJl989W4s4DDCwZ'),
(N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl', N'produfoot@live.be', N'akisekai', N'Louis Testolin', N'$2a$11$tlqtCMcbHz9gXsMYsSFU3ulebcUjhLXOAFxBqGaccMHSKRyghAbxm', 2, null, 0, 1, 1, 1, N'acc_DOBadyzJbvZYNdvvnRTZ8rUnOfKU'),
(N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', N'marine@defrance.com', N'marine0023', N'Marine <3', N'$2a$11$RAxHqyeURf80XZadno1SUOl8.VREvMHm.tr789YWJzA3KMhMEOWVq', 1, null, 0, 1, 1, 1, N'acc_r4Mo4H0uYd7pfKEfxTauwTHlzniP'),
(N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'yoplait@example.com', N'martin_p', N'Martin Pecheur', N'$2a$11$6UKm8Jc786NcIpuEH9ck8eC0nc0EqhcSOhJVVmNZCBmJbLVx/sjqC', 1, null, 0, 1, 1, 1, N'acc_nLhmM2C1CuRFKnXalS2f4F8oow44'),
(N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', N'user1@mail.com', N'login111', N'Arthur', N'$2a$11$khT276mDzLj1bpqsKtsQKOXZgOfuT9y01OQBlQWsdRhuF2sVEDk.y', 3, N'', 0, 1, 0, 1, N'acc_VFfu39dvIaPN3EEIxdlZVqqWKSGV'),
(N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre', N'enghien@bxl.be', N'engh_bxl', N'Enghiennoise De Bruxelles', N'$2a$11$Ynzc2gfRaJvITbI6q4rfWOg44ubgtsUuh9OGLnUAoiNN6I6T5pbCa', 1, null, 0, 1, 1, 0, N'acc_pn1JXApTYQE1smvMZUZXypmxHzX9'),
(N'usr_FGeJ1NN0UMo8ST0aRLgpl62JBU1c', N'ezrzerzer@aze.zeaze', N'azeazeazeaze', N'eazezaeaze', N'$2a$11$wVMzzuxIAlLyMzoC0DTeFuYTY2FzmlICE/q9XZZPirFSq8BDCDOZW', 1, N'Shut Up.', 1, 1, 1, 1, N'acc_EWkea49dUtSSgtof1ioZX8z91nO5'),
(N'usr_mS7RwCLl1TyJySw7byzmQ9zo2kcF', N'dempsey@gmail.com', N'dempsey__', N'Dempsey', N'$2a$11$h/JZV3ALXtjL0NxXbLeYg.IBPeUjLYmYS24xV31B4VO8MwPOCdPyq', 1, null, 0, 1, 1, 1, N'acc_o8FyLRgjgViwNFASw0n8U_GlBM7P'),
(N'usr_S2NsQR2ix298CNUpt1yp0B0iZcDO', N'pervicjulia@skynet.net', N'juju_pervic', N'Julia Pervic', N'$2a$11$G.1cxrLH.zjIV6SngYF0yugA4He3dn.AuEmv68tuq/z2yC6ByHfcy', 1, null, 0, 1, 1, 1, N'acc_NTNmZiDvjl6Ma9GUGhCbO52Qwtka'),
(N'usr_u9QsL12YlLzxvtEyqJerUeklQEam', N'frontend.dev@outlook.com', N'frontend_champ', N'Front End Development', N'$2a$11$YYp03JsPU1CS4O2u36ESQOUsAJnCGFQENw6BLjqXqiCtS2wz1vG5e', 1, null, 0, 1, 1, 1, N'acc_ch54v0RL_ZhtnMknoh82Dfvzsr3_'),
(N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0', N'charlier.themis@gmail.com', N'Kohaku99', N'Themis Charlier', N'$2a$11$xlpkrmXqJetoIdSgzHURSe4PYV9aZ22h464ROf5dTh7GDInXhhjQi', 2, N'Goddess', 0, 1, 1, 1, N'acc_9p5tLLnyR7WnnXlG5iKn_5IdDq5x'),
(N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL', N'titilafrite@gmail.com', N'titi_le_belge', N'Titi', N'$2a$11$zuo2lF3TrW/QTQaPR48/WOFVjNXxWpBO6NjWdB4bvRSvf3VqKxyRu', 1, N'Belgian', 0, 1, 1, 1, N'acc_cyYrHDrj12pywYLInXpdtz5PAcHr'),
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
(N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', N'usr_9yWVHaHniTc9164fQJfzCMssOb4e'),
(N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre'),
(N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre', N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq', N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL'),
(N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq'),
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
(N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq'),
(N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0'),
(N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0'),
(N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL', N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0'),
(N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0', N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL'),
(N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl', N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0'),
(N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0', N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0', N'usr_mS7RwCLl1TyJySw7byzmQ9zo2kcF'),
(N'usr_mS7RwCLl1TyJySw7byzmQ9zo2kcF', N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0'),
(N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'usr_mS7RwCLl1TyJySw7byzmQ9zo2kcF'),
(N'usr_mS7RwCLl1TyJySw7byzmQ9zo2kcF', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl', N'usr_mS7RwCLl1TyJySw7byzmQ9zo2kcF'),
(N'usr_mS7RwCLl1TyJySw7byzmQ9zo2kcF', N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(N'usr_FGeJ1NN0UMo8ST0aRLgpl62JBU1c', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'usr_FGeJ1NN0UMo8ST0aRLgpl62JBU1c'),
(N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd');

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
(20, N'2023-12-19 22:38:33.543', 1, 1, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(21, N'2023-12-19 22:38:33.997', 0, 0, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(22, N'2023-12-19 22:38:34.670', 1, 1, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(23, N'2023-12-19 22:38:35.030', 1, 1, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(24, N'2023-12-19 22:38:35.453', 1, 1, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_u9QsL12YlLzxvtEyqJerUeklQEam'),
(25, N'2023-12-19 22:38:35.943', 1, 1, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq'),
(26, N'2023-12-19 22:38:36.733', 1, 1, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah'),
(27, N'2023-12-19 22:41:31.377', 0, 0, N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL', N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre'),
(28, N'2023-12-19 22:41:31.720', 1, 1, N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq'),
(1005, N'2023-12-21 18:04:14.823', 1, 0, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', N'usr_FGeJ1NN0UMo8ST0aRLgpl62JBU1c'),
(1006, N'2023-12-21 19:36:30.710', 1, 0, N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0', N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(1007, N'2023-12-21 19:36:36.733', 1, 1, N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(1008, N'2023-12-21 19:36:39.827', 1, 1, N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0', N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(1009, N'2023-12-21 19:36:46.043', 1, 1, N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(1010, N'2023-12-21 19:37:03.960', 1, 1, N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0', N'usr_vlW55hcfXZEzCin6pseVFt0QsLHL'),
(2005, N'2023-12-22 10:07:36.533', 1, 1, N'usr_mS7RwCLl1TyJySw7byzmQ9zo2kcF', N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(2006, N'2023-12-22 10:07:37.770', 1, 1, N'usr_mS7RwCLl1TyJySw7byzmQ9zo2kcF', N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(2007, N'2023-12-22 10:07:38.773', 1, 1, N'usr_mS7RwCLl1TyJySw7byzmQ9zo2kcF', N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0'),
(2008, N'2023-12-22 12:45:21.050', 0, 0, N'usr_FGeJ1NN0UMo8ST0aRLgpl62JBU1c', N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq'),
(2009, N'2023-12-22 12:45:21.720', 0, 0, N'usr_FGeJ1NN0UMo8ST0aRLgpl62JBU1c', N'usr_mS7RwCLl1TyJySw7byzmQ9zo2kcF'),
(2010, N'2023-12-22 12:45:22.460', 0, 0, N'usr_FGeJ1NN0UMo8ST0aRLgpl62JBU1c', N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre'),
(2011, N'2023-12-22 13:49:13.850', 1, 1, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'usr_FGeJ1NN0UMo8ST0aRLgpl62JBU1c'),
(2012, N'2023-12-22 15:28:58.320', 1, 1, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(2013, N'2023-12-22 15:28:58.953', 0, 0, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', N'usr__PsW4ph4Bh7gHEWuppNu_kx6Z8ah');

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
(7, N'Le comité', N'2023-12-19 22:23:01.643', 0, N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre'),
(1001, N'Kawaiii', N'2023-12-20 17:42:25.267', 1, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(1002, N'Magic Emperor', N'2023-12-21 10:42:56.553', 0, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(1003, N'Fuck le monde', N'2023-12-21 18:59:04.347', 1, N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(1004, N'Fuck le monde 2', N'2023-12-21 19:00:09.767', 1, N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(2001, N'zergf', N'2023-12-22 10:30:43.647', 1, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(2002, N'Shut Up.', N'2023-12-22 12:45:47.220', 0, N'usr_FGeJ1NN0UMo8ST0aRLgpl62JBU1c'),
(2003, N'Boobies', N'2023-12-22 22:38:29.370', 0, N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(2004, N'My mounts', N'2023-12-22 22:42:44.303', 0, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(2005, N'Care', N'2023-12-22 22:44:27.673', 0, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(2006, N'Programmer Vibes', N'2023-12-22 22:47:37.893', 0, N'usr_u9QsL12YlLzxvtEyqJerUeklQEam'),
(2007, N'Wanna help me ?', N'2023-12-14 22:49:28.323', 0, N'usr_u9QsL12YlLzxvtEyqJerUeklQEam');


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
(10, 2005, 7),
(1001, 3002, 1001),
(1002, 3003, 1002),
(1003, 3005, 1003),
(1004, 3006, 1004),
(2001, 4002, 2001),
(2002, 4003, 2002),
(2003, 4004, 2003),
(2004, 4005, 2004),
(2005, 4006, 2004),
(2006, 4007, 2005),
(2007, 4008, 2006),
(2008, 4009, 2007);

SET IDENTITY_INSERT PUBLICATION_ELEMENTS OFF;
GO


-- COMMENTS
SET IDENTITY_INSERT COMMENTS ON;
INSERT INTO COMMENTS (cmt_id, cmt_date, cmt_content, pub_id, user_id) 
VALUES 
(1001, N'2023-12-20 11:43:34.923', N'Trop fort le couz', 5, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(1002, N'2023-12-21 14:32:18.890', N'Strong', 1002, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(2002, N'2023-12-22 10:29:07.423', N'Stylé', 1, N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(2003, N'2023-12-22 12:54:01.383', N'KEKW YOU NOOOOOOOOB', 1002, N'usr_FGeJ1NN0UMo8ST0aRLgpl62JBU1c'),
(2004, N'2023-12-22 22:50:55.927', N'Joli !', 2004, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F');

SET IDENTITY_INSERT COMMENTS OFF;
GO


-- LIKES
SET IDENTITY_INSERT LIKES ON;
INSERT INTO LIKES (like_id, like_date, pub_id, user_id)
VALUES 
(1, N'2023-12-20 11:42:52.870', 6, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(2, N'2023-12-20 18:57:01.167', 7, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(3, N'2023-12-21 14:32:13.620', 1002, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(1006, N'2023-12-22 10:28:36.590', 1002, N'usr_9664tGClnuljaZJ2ymEfJGSQWnMl'),
(1007, N'2023-12-22 22:38:36.027', 1002, N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(1008, N'2023-12-22 22:38:40.627', 1, N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(1009, N'2023-12-22 22:41:34.707', 2003, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd'),
(1010, N'2023-12-22 22:50:44.297', 2007, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(1011, N'2023-12-22 22:50:49.240', 2004, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(1012, N'2023-12-22 22:51:09.917', 2005, N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0'),
(1013, N'2023-12-22 22:51:13.293', 1, N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0');

SET IDENTITY_INSERT LIKES OFF;
GO


-- GROUPS
SET IDENTITY_INSERT GROUPS ON;
INSERT INTO GROUPS (group_id, group_isDeleted, group_name, group_isPrivate, group_proprio_id) 
VALUES 
(2, 0, null, 1, null),
(3, 0, null, 1, null),
(4, 0, null, 0, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F'),
(5, 0, null, 1, null),
(6, 0, N'Big 4', 0, N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9'),
(7, 0, null, 1, null);

SET IDENTITY_INSERT GROUPS OFF;
GO


-- USER_GROUPS
SET IDENTITY_INSERT USER_GROUPS ON;
INSERT INTO USER_GROUPS (user_group_id, user_id, group_id, user_has_left)
VALUES 
(4, N'usr_yF4kds6YRMWeWalvxDiMKLqCH_Vq', 2, 0),
(5, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', 2, 0),
(6, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', 3, 0),
(7, N'usr_FGeJ1NN0UMo8ST0aRLgpl62JBU1c', 3, 0),
(8, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', 4, 0),
(9, N'usr_FGeJ1NN0UMo8ST0aRLgpl62JBU1c', 4, 0),
(10, N'usr_VeVj5h1s0Wma6QsRUw_MyUlkkNy0', 4, 1),
(11, N'usr_6JPaWL2COoCYBzJ6O0QOfnfzPy4F', 4, 0),
(12, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', 5, 0),
(13, N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', 5, 0),
(14, N'usr_dFdJuHV29RkIbaPrZ7KCe2FLdGre', 6, 0),
(15, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', 6, 0),
(16, N'usr_DBXDDt_2Naq7wrTMPsaM1p_9xdKd', 6, 0),
(17, N'usr_9NGoKFkHBIv6qwHqLQMwegO2YQr9', 6, 0),
(18, N'usr_9yWVHaHniTc9164fQJfzCMssOb4e', 7, 0),
(19, N'usr_u9QsL12YlLzxvtEyqJerUeklQEam', 7, 0);

SET IDENTITY_INSERT USER_GROUPS OFF;
GO


-- MESSAGES
SET IDENTITY_INSERT MESSAGES ON;
INSERT INTO MESSAGES (msg_id, msg_content, user_group_id, msg_date, msg_isDeleted) 
VALUES 
(1, N'Hello Premier Message', 5, N'2023-12-22 09:59:20.113', 0),
(2, N'Comment ca va ?', 5, N'2023-12-22 09:59:39.260', 0),
(4, N'Trql et toi ?', 4, N'2023-12-22 10:31:27.447', 0),
(5, N'Goooooooooooooooo', 4, N'2023-12-22 10:35:25.937', 0),
(7, N'Aazeaz', 6, N'2023-12-22 13:50:59.657', 0),
(8, N'azeazeaze', 7, N'2023-12-22 13:51:02.820', 0),
(9, N'zaeaze', 7, N'2023-12-22 13:51:19.177', 0),
(10, N'zaezea', 6, N'2023-12-22 13:51:26.490', 0),
(11, N'azeaze', 11, N'2023-12-22 13:57:02.030', 0),
(12, N'azeazeae', 9, N'2023-12-22 13:57:03.957', 0),
(13, N'azea', 9, N'2023-12-22 13:57:06.073', 0),
(14, N'azeaze', 11, N'2023-12-22 13:57:07.873', 0),
(15, N'Hello Arthur, wanna hang out ?', 13, N'2023-12-22 22:39:09.230', 0),
(16, N'Sunday 23 May 2024', 17, N'2023-12-22 22:39:49.613', 0),
(17, N'Yeah sure, coming to your home now', 12, N'2023-12-22 22:40:33.003', 0),
(18, N'Ok lol', 8, N'2023-12-22 22:41:06.000', 0),
(19, N'Why not', 8, N'2023-12-22 22:41:10.527', 0),
(20, N'20% discount on the purchase of an HTML or Java course', 19, N'2023-12-22 22:45:54.460', 0),
(21, N'Answer for link', 19, N'2023-12-22 22:46:01.613', 0),
(22, N'No.', 10, N'2023-12-22 22:51:24.203', 0);

SET IDENTITY_INSERT MESSAGES OFF;
GO
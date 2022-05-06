--
-- Fichier généré par SQLiteStudio v3.3.3 sur ven. mai 6 14:45:00 2022
--
-- Encodage texte utilisé : System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table : t_category
CREATE TABLE t_category (idCat INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, catName TEXT, catColor TEXT);
INSERT INTO t_category (idCat, catName, catColor) VALUES (1, 'Productif', '1');
INSERT INTO t_category (idCat, catName, catColor) VALUES (2, 'Objectif', '2');
INSERT INTO t_category (idCat, catName, catColor) VALUES (3, 'Plaisir', '3');

-- Table : t_task
CREATE TABLE t_task (idTas INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, tasName TEXT, tasActive BOOLEAN, tasPassingDate DATETIME, tasInToDoList BOOLEAN, fdCategory INT REFERENCES t_category (idCat) ON DELETE SET NULL ON UPDATE NO ACTION);
INSERT INTO t_task (idTas, tasName, tasActive, tasPassingDate, tasInToDoList, fdCategory) VALUES (1, 'manger la banane', 1, '2022-05-17 00:00:00', 1, 2);
INSERT INTO t_task (idTas, tasName, tasActive, tasPassingDate, tasInToDoList, fdCategory) VALUES (2, 'ranger ma chambre', 1, '2022-05-10 00:00:00', 0, 1);
INSERT INTO t_task (idTas, tasName, tasActive, tasPassingDate, tasInToDoList, fdCategory) VALUES (3, 'écouter un film', 1, '2022-05-12 00:00:00', 1, 3);
INSERT INTO t_task (idTas, tasName, tasActive, tasPassingDate, tasInToDoList, fdCategory) VALUES (4, 'vendre un ours', 1, '2022-05-09 00:00:00', 1, 2);
INSERT INTO t_task (idTas, tasName, tasActive, tasPassingDate, tasInToDoList, fdCategory) VALUES (5, 'calculer la vie', 1, '2022-05-03 00:00:00', 0, 2);
INSERT INTO t_task (idTas, tasName, tasActive, tasPassingDate, tasInToDoList, fdCategory) VALUES (6, 'trouver l''amour', 1, '2022-05-15 00:00:00', 0, 2);
INSERT INTO t_task (idTas, tasName, tasActive, tasPassingDate, tasInToDoList, fdCategory) VALUES (7, 'créer robot', 1, '2022-06-07 00:00:00', 1, 1);
INSERT INTO t_task (idTas, tasName, tasActive, tasPassingDate, tasInToDoList, fdCategory) VALUES (8, 'étudier la cuisine', 1, '2022-06-27 00:00:00', 1, 1);
INSERT INTO t_task (idTas, tasName, tasActive, tasPassingDate, tasInToDoList, fdCategory) VALUES (9, 'faire un trickshot sur minecraft', 1, '2022-05-25 00:00:00', 0, 1);
INSERT INTO t_task (idTas, tasName, tasActive, tasPassingDate, tasInToDoList, fdCategory) VALUES (10, 'lire le pendule de foucault', 1, '2022-05-28 00:00:00', 1, 2);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;

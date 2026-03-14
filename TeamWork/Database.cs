using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TeamWork
{
    internal class Database
    {
        private SQLiteConnection connection;
        static Database()
        {
            SQLiteConnection.CreateFile("database.db");
            SQLiteConnection connection = new SQLiteConnection("Data Source=database.db;Version=3;");
            connection.Open();
SQLiteCommand command = new SQLiteCommand(
@"
CREATE TABLE IF NOT EXISTS Преподаватель (
    ID_преподавателя INTEGER PRIMARY KEY AUTOINCREMENT,
    Фамилия TEXT NOT NULL,
    Имя TEXT NOT NULL,
    Отчество TEXT,
    Логин TEXT UNIQUE,
    Пароль TEXT
);

CREATE TABLE IF NOT EXISTS Группы (
    НазваниеГруппы TEXT PRIMARY KEY,
    ЛогинСтаросты TEXT,
    ПарольСтаросты TEXT
);

CREATE TABLE IF NOT EXISTS Студенты (
    ID_Студента INTEGER PRIMARY KEY AUTOINCREMENT,
    Фамилия TEXT NOT NULL,
    Имя TEXT NOT NULL,
    Отчество TEXT,
    Стипендия REAL,
    НазваниеГруппы_FK TEXT,
    СреднийБалл REAL,
    Должник INTEGER,
    FOREIGN KEY (НазваниеГруппы_FK) REFERENCES Группы(НазваниеГруппы)
);

CREATE TABLE IF NOT EXISTS Предметы (
    ID_Предмета INTEGER PRIMARY KEY AUTOINCREMENT,
    ID_Преподавателя_FK INTEGER,
    НазваниеПредмета TEXT NOT NULL,
    FOREIGN KEY (ID_Преподавателя_FK) REFERENCES Преподаватель(ID_преподавателя)
);

CREATE TABLE IF NOT EXISTS Занятие (
    ID_занятия INTEGER PRIMARY KEY AUTOINCREMENT,
    ID_предмета_FK INTEGER,
    ID_студента_FK INTEGER,
    Посещение INTEGER,
    Оценка INTEGER,
    КомментарийОценки TEXT,
    FOREIGN KEY (ID_предмета_FK) REFERENCES Предметы(ID_Предмета),
    FOREIGN KEY (ID_студента_FK) REFERENCES Студенты(ID_Студента)
);

CREATE TABLE IF NOT EXISTS Деканат (
    Логин TEXT PRIMARY KEY,
    Пароль TEXT
);
",
connection
);

command.ExecuteNonQuery();
        }
    }

}

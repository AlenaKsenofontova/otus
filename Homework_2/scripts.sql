CREATE TABLE Users (
	Id SERIAL PRIMARY KEY,
	Name varchar(40) NOT NULL,
	LastName varchar(40) NOT NULL,
	Age integer,
	CreatedOn timestamp
);

CREATE TABLE Announcement (
	Id SERIAL PRIMARY KEY,
	Title varchar(100) NOT NULL,
	CreatedOn timestamp,
	CreatedById integer REFERENCES Users (Id) ON DELETE CASCADE
);

CREATE TABLE Review (
	Id SERIAL PRIMARY KEY,
	Title varchar(100) NOT NULL,
	Body varchar(2000) NOT NULL,
	CreatedOn timestamp,
	CreatedById integer REFERENCES Users (Id) ON DELETE CASCADE,
	UserId integer REFERENCES Users (Id) ON DELETE CASCADE
);

insert into Users (Name, LastName, Age, CreatedOn) values 
('Иван', 'Иванов', '18', '2022-12-12 12:12:12'),
('Петр', 'Петров', '23', '2022-10-12 13:12:12'),
('Антон', 'Антонов', '50', '2022-05-12 18:12:12'),
('Юлия', 'Киселева', '40', '2023-03-12 12:12:12'),
('Анна', 'Андреева', '30', '2022-06-12 12:19:12');

insert into Announcement (Title, CreatedOn, CreatedById) values
('Продам раскладной диван', '2023-01-12 12:12:12', '3'),
('Продам туфли.Неношеные.37р', '2023-02-15 12:12:12', '5'),
('Отдам котенка в добрые руки', '2023-08-12 12:12:12', '5'),
('Приму в дар книги.Детективы.', '2023-05-12 12:12:12', '4'),
('Продам.Щенок хаски.Чистокровный.', '2023-01-18 12:12:12', '2');

insert into Review (Title, Body, CreatedOn, CreatedById, UserId) values
('Супер', 'Очень вежливый продавец', '2023-06-01 12:12:12', '5', '3'),
('Отлично', 'Покупкой доволен', '2023-03-01 12:12:12', '2', '5'),
('Отлично', 'Очень вежливый продавец', '2023-01-12 12:12:12', '3', '5'),
('Отлично', '-', '2023-04-01 12:12:12', '2', '4'),
('Ужас', 'Хам!', '2023-02-01 12:12:12', '3', '1');
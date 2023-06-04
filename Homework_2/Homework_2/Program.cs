using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Homework_2
{
    class Program
    {
        static void Main(string[] args)
        {
            SelectFromTables();

            Console.WriteLine();
            Console.Write("Вы хотите добавить данные в таблицу? Y/N?  ");

            switch (Console.ReadLine())
            {
                case "Y":
                case "y":
                    InsertDataInTable();
                    Console.ReadKey();
                    break;
                case "N":
                case "n":
                    Console.ReadKey();
                    break;
            }

            Console.ReadKey();
        }

        const string connectionString = "Host=localhost;Username=postgres;Password=hfphf,jnrf;Database=otus_hw";

        /// <summary>
        /// Выборка из таблиц
        /// </summary>
        static void SelectFromTables()
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var sql = @"
SELECT Id, Name, LastName, Age, CreatedOn FROM Users
";

            using var cmd = new NpgsqlCommand(sql, connection);

            Console.WriteLine("Users:");
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt64(0);
                var name = reader.GetString(1);
                var lastName = reader.GetString(2);
                var age = reader.GetInt64(3);
                var createdOn = reader.GetDateTime(4);

                Console.WriteLine($"Read: [id={id},name={name},lastName={lastName},age={age},createdOn={createdOn}]");
            }
            reader.Close();

            sql = @"
SELECT * FROM Announcement
";

            using var cmd2 = new NpgsqlCommand(sql, connection);

            Console.WriteLine("Announcement:");
            reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt64(0);
                var title = reader.GetString(1);
                var createdOn = reader.GetDateTime(2);
                var createdById = reader.GetInt64(3);

                Console.WriteLine($"Read: [id={id},title={title},createdOn={createdOn},createdById={createdById}]");
            }
            reader.Close();

            sql = @"
SELECT * FROM Review
";

            using var cmd3 = new NpgsqlCommand(sql, connection);

            Console.WriteLine("Review:");
            reader = cmd3.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt64(0);
                var title = reader.GetString(1);
                var body = reader.GetString(2);
                var createdOn = reader.GetDateTime(3);
                var createdById = reader.GetInt64(4);
                var userId = reader.GetInt64(5);

                Console.WriteLine($"Read: [id={id},title={title},body={body},createdOn={createdOn},createdById={createdById},userId={userId}]");
            }
            reader.Close();
            connection.Close();
        }

        /// <summary>
        /// Добавление данных в таблицу
        /// </summary>
        static void InsertDataInTable()
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            Console.Write("В какую таблицу добавить данные? U - Users, A - Announcement, R - Review  ");
            switch (Console.ReadLine())
            {
                case "U":
                case "u":
                    {
                        Console.Write("Поле Имя: ");
                        var name = Console.ReadLine();
                        Console.Write("Поле Фамилия: ");
                        var lastName = Console.ReadLine();
                        Console.Write("Поле Возраст: ");
                        var age = Console.ReadLine();
                        var sql = $@"
INSERT INTO users (name, lastName, age, createdOn) 
VALUES ('{name}', '{lastName}', '{age}', '{DateTime.Now}');
";
                        using var cmd = new NpgsqlCommand(sql, connection);

                        var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                        Console.WriteLine($"Insert into Users table. Affected rows count: {affectedRowsCount}");
                        connection.Close();
                        break;
                    }
                case "A":
                case "a":
                    {
                        Console.Write("Поле Заголовок: ");
                        var title = Console.ReadLine();
                        Console.Write("Поле Id пользователя из таблицы Users: ");
                        var createdById = Console.ReadLine();
                        var sql = $@"
INSERT INTO Announcement (title, createdOn, createdById) 
VALUES ('{title}', '{DateTime.Now}', '{createdById}');
";
                        using var cmd = new NpgsqlCommand(sql, connection);

                        var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                        Console.WriteLine($"Insert into Announcement table. Affected rows count: {affectedRowsCount}");
                        connection.Close();
                        break;
                    }
                case "R":
                case "r":
                    {
                        Console.Write("Поле Заголовок: ");
                        var title = Console.ReadLine();
                        Console.Write("Поле Описание: ");
                        var body = Console.ReadLine();
                        Console.Write("Поле Id - Id пользователя из таблицы Users: ");
                        var createdById = Console.ReadLine();
                        Console.Write("Поле UserId - Id пользователя из таблицы Users: ");
                        var userId = Console.ReadLine();
                        var sql = $@"
INSERT INTO Review (title, body, createdOn, createdById, userId) 
VALUES ('{title}', '{body}', '{DateTime.Now}', '{createdById}', '{userId}');
";
                        using var cmd = new NpgsqlCommand(sql, connection);

                        var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                        Console.WriteLine($"Insert into Review table. Affected rows count: {affectedRowsCount}");
                        connection.Close();
                        break;
                    }
            }
        }

    }
}
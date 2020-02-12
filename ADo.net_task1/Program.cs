using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using static System.Console;
namespace ADo.net_task1
{
    class Program
    {
        static void Main(string[] args)
        {

            //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Universities;Integrated Security=True;Connect Timeout=3;
            //                      Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;User Id = sa; Password = 1234567fd";

            //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //Console.WriteLine(connectionString);

            //Console.Read();

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    Console.WriteLine("Подключение открыто");
            //}
            //Console.WriteLine("Подключение закрыто...");

            //ConnectWithDB().GetAwaiter();

            //InsertDbElements();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Universities;Integrated Security=True;Connect Timeout=3;
                                  Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;User Id = sa; Password = 1234567fd";
            //  ConsoleInsert_Update(connectionString);

            // DataReader(connectionString);
            SkaLyarData(connectionString);


        }

        private static async Task ConnectWithDB(string connectionString)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //await connection.OpenAsync();
                Console.WriteLine("Подключение открыто");
                Console.WriteLine("Свойства подключения:");
                Console.WriteLine("\tСтрока подключения: {0}", connection.ConnectionString);
                Console.WriteLine("\tБаза данных: {0}", connection.Database);
                Console.WriteLine("\tСервер: {0}", connection.DataSource);
                Console.WriteLine("\tВерсия сервера: {0}", connection.ServerVersion);
                Console.WriteLine("\tСостояние: {0}", connection.State);
                Console.WriteLine("\tWorkstationld: {0}", connection.WorkstationId);
            }
            Console.WriteLine("Подключение закрыто...");

        }
        private static void InsertDbElements(string connectionString)
        {

            string sqlExpression = "INSERT INTO Students (Id,Name,Surname, Age) VALUES (2,'Jon','Jones', 23)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Added objects: {0}", number);

            }
            Console.Read();
        }

        private static void DeleteObj(string connectionString)
        {

            string sqlExpression = "DELETE FROM Students WHERE ID=2 ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("DELETE objects: {0}", number);

            }
            Console.Read();

        }
        private static void Update(string connectionString)
        {

            string sqlExpression = "UPDATE Students Set Age=20 Where ID=1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Update objects: {0}", number);

            }
            Console.Read();

        }
        static void ConsoleInsert_Update(string connectionString)
        {
            WriteLine("Enter Id");
            int id = int.Parse(ReadLine());

            WriteLine("Enter Name");
            string name = ReadLine();

            WriteLine("Enter SurName");
            string surname = ReadLine();

            WriteLine("Enter age");
            int age = int.Parse(ReadLine());



            string sqlExpression = String.Format("Insert Into Students(Id,Name,Surname,Age)Values({0},'{1}','{2}',{3})", id, name, surname, age);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int num = command.ExecuteNonQuery();
                WriteLine("Added object:  {0}", num);

                WriteLine("Enter Name");
                name = ReadLine();
                sqlExpression = String.Format("Update Students Set Name = '{0}' Where Id = {1}", id, name);
                command.CommandText = sqlExpression;
                num = command.ExecuteNonQuery();
                WriteLine("Updated object:  {0}", num);
            }
            Read();
        }
        static void DataReader(string connectionString)
        {
            string sqlExpression = "Select * From Students";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                SqlCommand comannd = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = comannd.ExecuteReader();


                if (reader.HasRows)
                {

                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3));
                    while (reader.Read())
                    {
                        var id = reader.GetValue(0);
                        var name = reader.GetValue(1);
                        var surename = reader.GetValue(2);
                        var age = reader.GetValue(3);
                        Console.WriteLine("{0} \t{1} \t{2}\t{3}", id, name, surename, age);
                    }

                }


            }

            ReadKey();


        }
        static void SkaLyarData(string connectionString)
        {
            string sqlExpression = "Select Count (*)From Students ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                var count = command.ExecuteScalar();

                command.CommandText = "SELECT MIN(Age) FROM Students";
                var minAge = command.ExecuteScalar();

                Console.WriteLine("В таблице {0} объектов", count);
                Console.WriteLine("Минимальный возраст: {0}", minAge);

            }
        }
    }
}
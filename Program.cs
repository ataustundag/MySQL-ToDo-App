using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace ToDoApp
{
    class Program : AppManagement
    {
        static string connectionString = "server=localhost;database=todolist;user=;password=;";
        static string query, notCompleted = "[IP]",completed = "[C]";
        static Program app = new Program();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("[0] Exit\n[1] Add Task\n[2]Show Tasks\nOption: ");
                int option = Convert.ToInt32(Console.ReadLine());
                if (option == 0)
                {
                    Environment.Exit(0);
                }
                else if (option == 1)
                {
                    Console.Write("Task: ");
                    string task = Console.ReadLine();
                    app.addItem(connectionString, task, false);

                }
                else if (option == 2)
                {
                    app.showItems(connectionString);
                }
                else
                {
                    Console.WriteLine("Error!");
                }
                Console.Write("Please press ENTER to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public void addItem(string connectionString, string task, bool status)
        {
            try
            {
                query = "INSERT INTO todo (Task, Status) VALUES (@task, @status)";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@task", task);
                command.Parameters.AddWithValue("@status", status ? 1 : 0);
                command.ExecuteNonQuery();
                connection.Close();
            }catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        public void showItems(string connectionString)
        {
            query = "SELECT * FROM todo";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            Console.Clear();

            Console.WriteLine("+---------------------------------------------------------+");
            Console.WriteLine("|ID".PadRight(20) + "Tasks".PadRight(20) + "\t" + "Status".PadRight(10) + "|");
            Console.WriteLine("+---------------------------------------------------------+");

            foreach(DataRow item in table.Rows)
            {
                if (item["Status"].ToString() == "False")
                {
                    Console.WriteLine($"|{item["ID"].ToString().PadRight(18)} {item["Task"].ToString().PadRight(28)} {notCompleted.PadRight(8)} |");
                    Console.WriteLine("+---------------------------------------------------------+");
                }
                else
                {
                    Console.WriteLine($"|{item["ID"].ToString().PadRight(18)} {item["Task"].ToString().PadRight(28)} {completed.PadRight(8)} |");
                    Console.WriteLine("+---------------------------------------------------------+");
                }
            }
        }
    }
}

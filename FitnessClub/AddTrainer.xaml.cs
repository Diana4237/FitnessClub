using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitnessClub
{
    /// <summary>
    /// Логика взаимодействия для AddTrainer.xaml
    /// </summary>
    public partial class AddTrainer : Window
    {
        int tit;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public AddTrainer()
        {
            InitializeComponent();


            string query3 = "SELECT Title FROM Type_subscription";



            using (SqlConnection connection2 = new SqlConnection(connectionString))
            {
                // Открытие подключения
                connection2.Open();

                // Создание команды SQL
                using (SqlCommand command3 = new SqlCommand(query3, connection2))
                {
                    // Выполнение команды и получение SqlDataReader для чтения данных
                    using (SqlDataReader reader2 = command3.ExecuteReader())
                    {
                        // Очистка текущих элементов ListBox
                        Typesub.Items.Clear();

                        // Чтение данных из reader и добавление их в ListBox
                        while (reader2.Read())
                        {
                            Typesub.Items.Add(reader2["Title"].ToString());

                        }
                        reader2.Close();
                    }
                }
            }
            //using (SqlConnection con = new SqlConnection(connectionString))
            //{
            //    con.Open();
            //    SqlCommand sqlCommand = new SqlCommand(sqlExp, con);
            //    SqlDataReader reader = sqlCommand.ExecuteReader();
            //    List<string> types = new List<string>();
            //    while (reader.HasRows)
            //    {

            //        string title = reader.GetString(0);
            //        types.Add(title);

            //    }
            //    Typesub.DataContext = types;//нужно транкейтнуть subscription т к не с 0 начинается

            //}
        }
        private void reg_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"INSERT INTO Staff (FirstName,LastName,Patronymic,Telephone,PassportData," +
                $" Id_role,Achievements,Subscription) VALUES ('{first.Text}','{last.Text}','{pat.Text}'," +
                $"'{tel.Text}','{passp.Text}',2,'{achivem.Text}','{Typesub.SelectedItem.ToString()}')";
            cmd.Connection = connection;
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            string sqlExpression = $"SELECT Id_staff FROM Staff WHERE LastName='{last.Text}' AND Patronymic='{pat.Text}'";
            using (SqlConnection connection5 = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tit = reader.GetInt32(0);
                       
                    }
                }
            }

           


            SqlConnection connection2 = new SqlConnection(connectionString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.CommandText = $"INSERT INTO Users VALUES ({tit},'{login.Text}','{password.Text}',2)";
            cmd2.Connection = connection2;
            connection2.Open();
            cmd2.ExecuteNonQuery();
            connection2.Close();

            Trainers trainers = new Trainers();
            trainers.Show();
        }
    }
}

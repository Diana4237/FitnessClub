using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        int tit;
        int User;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public AddClient(int IdUser)
        {
            InitializeComponent();

            User=IdUser;

            string query3 = "SELECT Title FROM Type_subscription";



          

        }
        private void reg_Click(object sender, RoutedEventArgs e)
        {


            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"INSERT INTO Client (FirstName,LastName,Patronymic,Telephone,PassportData) VALUES ('{first.Text}','{last.Text}','{pat.Text}','{tel.Text}','{passp.Text}')";
            cmd.Connection = connection;
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            string sqlExpression = $"SELECT Id_client FROM Client WHERE LastName='{last.Text}' AND Patronymic='{pat.Text}'";
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
            cmd2.CommandText = $"INSERT INTO Users VALUES ({tit},'{login.Text}','{password.Text}',1)";
            cmd2.Connection = connection2;
            connection2.Open();
            cmd2.ExecuteNonQuery();
            connection2.Close();

            Client client = new Client(User);
            client.Show();
        }
    }
}


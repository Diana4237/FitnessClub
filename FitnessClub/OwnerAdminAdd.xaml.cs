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
    /// Логика взаимодействия для OwnerAdminAdd.xaml
    /// </summary>
    public partial class OwnerAdminAdd : Window
    {
        int idUs;
        int User;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public OwnerAdminAdd(int IdUser)
        {
            InitializeComponent();
            User=IdUser;
        }
        private void reg_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"INSERT INTO Staff (FirstName,LastName,Patronymic,Telephone,PassportData," +
                $" Id_role) VALUES ('{first.Text}','{last.Text}','{pat.Text}'," +
                $"'{tel.Text}','{passp.Text}',3)";
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
                        idUs = reader.GetInt32(0);

                    }
                }
            }


            DateTime dateTime = DateTime.Now;

            SqlConnection connection2 = new SqlConnection(connectionString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.CommandText = $"INSERT INTO Users VALUES ({idUs},'{login.Text}','{password.Text}',3,'{dateTime}')";
            cmd2.Connection = connection2;
            connection2.Open();
            cmd2.ExecuteNonQuery();
            connection2.Close();

            Admins trainers = new Admins(User);
            trainers.Show();
        }
    }
}

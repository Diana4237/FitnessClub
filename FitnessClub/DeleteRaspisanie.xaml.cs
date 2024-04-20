using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
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
    /// Логика взаимодействия для DeleteRaspisanie.xaml
    /// </summary>
    public partial class DeleteRaspisanie : Window
    {
        int Role;
        int User;
        public int id_client;
        public int Id_hall;
        DateTime date_time;
        int id_type = 0;
        int id_subsc = 0;
        public int Id_staff;
        public string title;
        public decimal cost;
        public string fio;
        public byte[] photo;
        int id_staff2 = 0;
        TextBox TextB;
        DateTime Dat;

        string type_sub;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public DeleteRaspisanie(int idRole, int id_user, int id_staff)
        {
            InitializeComponent();

            Role = idRole;
            User = id_user;
            Id_staff = id_staff;


            if (idRole == 1)
            {



                string sqlExp = $"SELECT Id_subscription,Id_type_subscription FROM Subscription WHERE Id_client='{id_user}'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExp, connection);
                    SqlDataReader reader1 = command.ExecuteReader();

                    if (reader1.HasRows)
                    {

                        while (reader1.Read())
                        {
                            id_subsc = reader1.GetInt32(0);

                            id_type = reader1.GetInt32(1);

                        }
                    }
                    reader1.Close();
                }





            }
            if (idRole == 2)
            {







                string sqlExp = $"SELECT Subscription FROM Staff WHERE Id_staff='{id_user}'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExp, connection);
                    SqlDataReader reader1 = command.ExecuteReader();

                    if (reader1.HasRows)
                    {

                        while (reader1.Read())
                        {
                            type_sub = reader1.GetString(0);


                        }
                    }
                    reader1.Close();


                }




            }
            if (idRole == 3)
            {








            }


        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            if (Role == 2)
            {


            }

            if (Role == 3)
            {


            }
        }
    }
}


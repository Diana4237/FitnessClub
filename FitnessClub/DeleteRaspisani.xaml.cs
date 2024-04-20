using DocumentFormat.OpenXml.Office2010.ExcelAc;
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
    /// Логика взаимодействия для DeleteRaspisani.xaml
    /// </summary>
    public partial class DeleteRaspisani : Window
    {
        int Role;
        int User;
        ListBox list2;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        string stroka;
        public DeleteRaspisani(int idRole, int idUser)
        {
            InitializeComponent();
            Role = idRole;
            User = idUser;
            DateTime Dat;
            int id_staff = 0;
            string type_sub="";
           


            if (Role == 3)
            {
                string query = $"SELECT Date_Time FROM Class";



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Открытие подключения
                    connection.Open();

                    // Создание команды SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Выполнение команды и получение SqlDataReader для чтения данных
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Очистка текущих элементов ListBox
                            list.Items.Clear();

                            // Чтение данных из reader и добавление их в ListBox
                            while (reader.Read())

                            {
                                Dat = reader.GetDateTime(0);
                                string sqlExp = $"SELECT Id_Staff FROM Class WHERE Date_Time='{Dat}'";
                                using (SqlConnection connection4 = new SqlConnection(connectionString))
                                {
                                    connection4.Open();
                                    SqlCommand command1 = new SqlCommand(sqlExp, connection4);
                                    SqlDataReader reader1 = command1.ExecuteReader();

                                    if (reader1.HasRows)
                                    {

                                        while (reader1.Read())
                                        {
                                            id_staff = reader1.GetInt32(0);


                                        }
                                    }
                                    reader1.Close();


                                }






                                string sqlExp2 = $"SELECT Subscription FROM Staff WHERE Id_Staff={id_staff}";
                                using (SqlConnection connection3 = new SqlConnection(connectionString))
                                {
                                    connection3.Open();
                                    SqlCommand command3 = new SqlCommand(sqlExp2, connection3);
                                    SqlDataReader reader3 = command3.ExecuteReader();

                                    if (reader3.HasRows)
                                    {

                                        while (reader3.Read())
                                        {
                                            type_sub = reader3.GetString(0);


                                        }
                                    }
                                    reader3.Close();
                                }

                            
                                list.Items.Add(reader["Date_Time"].ToString() + "_" + type_sub);
                            }
                        }
                    }
                }






















            }


            

        }

        public void EditButton(object sender, RoutedEventArgs e)
        {
            try
            {

                stroka = list.SelectedItem.ToString();
                string[] words = stroka.Split('_');

                stroka = words[0];


                string updateQuery = "DELETE FROM Class WHERE  Date_Time= @stroka";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                       // command.Parameters.AddWithValue("@newData", newData);
                        command.Parameters.AddWithValue("@User", User);
                        command.Parameters.AddWithValue("@stroka", stroka);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {





                            MessageBox.Show("Дата успешно удалена");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при удалении данных.");
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("Ошибка.");
            }

        }


    }
}

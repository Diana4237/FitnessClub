using DocumentFormat.OpenXml.Office2010.ExcelAc;
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
    /// Логика взаимодействия для AddRaspisanie.xaml
    /// </summary>
    public partial class AddRaspisanie : Window
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
        int id_client2 = 0;
        int id_sub = 0;
        string sub;
        TextBox TextB;
        DateTime Dat;
        ListBox list1;
        int Idclass;

        string type_sub;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public AddRaspisanie(int idRole, int id_user, int id_staff)
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

                lab1.Visibility = Visibility.Visible;
                staff.Visibility = Visibility.Visible;


                lab2.Visibility = Visibility.Visible;
                client.Visibility = Visibility.Visible;

                string query2 = $"SELECT  LastName FROM Staff";



                using (SqlConnection connection1 = new SqlConnection(connectionString))
                {
                    // Открытие подключения
                    connection1.Open();

                    // Создание команды SQL
                    using (SqlCommand command2 = new SqlCommand(query2, connection1))
                    {
                        // Выполнение команды и получение SqlDataReader для чтения данных
                        using (SqlDataReader reader1 = command2.ExecuteReader())
                        {
                            // Очистка текущих элементов ListBox
                            staff.Items.Clear();

                            // Чтение данных из reader и добавление их в ListBox
                            while (reader1.Read())
                            {
                                staff.Items.Add(reader1["LastName"].ToString());
                                
                            }
                            reader1.Close();
                        }
                    }
                }

                string query3 = $"SELECT  LastName FROM Client";



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
                            client.Items.Clear();

                            // Чтение данных из reader и добавление их в ListBox
                            while (reader2.Read())
                            {
                                client.Items.Add(reader2["LastName"].ToString());

                            }
                            reader2.Close();
                        }
                    }
                }

            }


        }
        public void InformLogin(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow(Role, User);
            mainWindow.Show();
        }
        public void MyAccountClick(object sender, EventArgs e)
        {
            this.Close();
            MyAccount autorization = new MyAccount(Role, User);
            autorization.Show();
        }
        public void TypeActLogin(object sender, RoutedEventArgs e)
        {
            this.Close();
            TypesActivities types = new TypesActivities(Role, User);
            types.Show();
        }
        public BitmapImage GetImageFromDatabaseStaff(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = $"SELECT Photo FROM Staff WHERE Id_staff = '{id}' AND Photo IS NOT NUll";
                // string query = $"SELECT Photo FROM Doctors WHERE Id = '{id}' AND Photo IS NOT NUll";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] imageData = (byte[])reader["Photo"];
                            BitmapImage bitmapImage = new BitmapImage();
                            using (MemoryStream stream = new MemoryStream(imageData))
                            {
                                bitmapImage.BeginInit();
                                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                bitmapImage.StreamSource = stream;
                                bitmapImage.EndInit();
                            }
                            return bitmapImage;
                        }
                    }
                }
            }
            return null;
        }
        public BitmapImage GetImageFromDatabaseCl(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = $"SELECT Photo FROM Client WHERE Id_client = '{id}' AND Photo IS NOT NUll";
                // string query = $"SELECT Photo FROM Doctors WHERE Id = '{id}' AND Photo IS NOT NUll";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] imageData = (byte[])reader["Photo"];
                            BitmapImage bitmapImage = new BitmapImage();
                            using (MemoryStream stream = new MemoryStream(imageData))
                            {
                                bitmapImage.BeginInit();
                                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                bitmapImage.StreamSource = stream;
                                bitmapImage.EndInit();
                            }
                            return bitmapImage;
                        }
                    }
                }
            }
            return null;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            if (Role == 2)
            {

                string newData = time.Text;
                string nomer_hall = hall.Text;

                string addQuery = "INSERT INTO Class VALUES (@newData, @hall, @staff )";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(addQuery, connection))
                    {
                        command.Parameters.AddWithValue("@newData", newData);
                        command.Parameters.AddWithValue("@hall", nomer_hall);
                        command.Parameters.AddWithValue("@staff", User);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {





                            MessageBox.Show("Дата успешно изменена");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении данных.");
                        }
                    }

                }
            }

            if (Role == 3)
            {


                string newData = time.Text;
                string nomer_hall = hall.Text;
                string stafchick = staff.SelectedItem.ToString();
                string clientik = client.SelectedItem.ToString();




                string query4 = $"SELECT Subscription FROM Staff WHERE LastName='{stafchick}'";



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query4, connection);
                    SqlDataReader reader1 = command.ExecuteReader();

                    if (reader1.HasRows)
                    {

                        while (reader1.Read())
                        {
                            sub = reader1.GetString(0);


                        }
                    }
                    reader1.Close();


                }

                string query5 = $"SELECT Id_type_subscription FROM Type_subscription WHERE Title='{sub}'";



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query5, connection);
                    SqlDataReader reader1 = command.ExecuteReader();

                    if (reader1.HasRows)
                    {

                        while (reader1.Read())
                        {
                            id_sub = reader1.GetInt32(0);


                        }
                    }
                    reader1.Close();


                }

                string query2 = $"SELECT Id_Staff FROM Staff WHERE  LastName='{stafchick}'";



                using (SqlConnection connection4 = new SqlConnection(connectionString))
                {
                    connection4.Open();
                    SqlCommand command1 = new SqlCommand(query2, connection4);
                    SqlDataReader reader1 = command1.ExecuteReader();

                    if (reader1.HasRows)
                    {

                        while (reader1.Read())
                        {
                            id_staff2 = reader1.GetInt32(0);


                        }
                    }
                    reader1.Close();


                }


                string query3 = $"SELECT Id_Client FROM Client WHERE  LastName='{clientik}'";



                using (SqlConnection connection4 = new SqlConnection(connectionString))
                {
                    connection4.Open();
                    SqlCommand command1 = new SqlCommand(query3, connection4);
                    SqlDataReader reader1 = command1.ExecuteReader();

                    if (reader1.HasRows)
                    {

                        while (reader1.Read())
                        {
                            id_client2 = reader1.GetInt32(0);


                        }
                    }
                    reader1.Close();


                }

                string addQuery = "INSERT INTO Class VALUES (@newData, @hall, @staff )";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(addQuery, connection))
                    {
                        command.Parameters.AddWithValue("@newData", newData);
                        command.Parameters.AddWithValue("@hall", nomer_hall);
                        command.Parameters.AddWithValue("@staff", id_staff2);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Дата успешно изменена");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении данных.");
                        }
                    }

                }
                string addQuery1 = "INSERT INTO Subscription VALUES (@client, 1, @sub )";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(addQuery1, connection))
                    {
                        command.Parameters.AddWithValue("@client", id_client2);
                        
                        command.Parameters.AddWithValue("@sub", id_sub);

                        int rowsAffected = command.ExecuteNonQuery();

                       
                    }

                }

                //string query6 = $"SELECT Id_Class FROM Class WHERE Date_Time='@newData',Id_Hall=@hall,Id_Staff=@staff";



                //using (SqlConnection connection5 = new SqlConnection(connectionString))
                //{
                //    connection5.Open();
                //    SqlCommand command1 = new SqlCommand(query6, connection5);
                //    SqlDataReader reader1 = command1.ExecuteReader();

                //    if (reader1.HasRows)
                //    {

                //        while (reader1.Read())
                //        {
                //            Idclass = reader1.GetInt32(0);


                //        }
                //    }
                //    reader1.Close();


                //}

                //string addQuery2 = "INSERT INTO Timing VALUES ('{Idclass}', @sub )";

                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    connection.Open();

                //    using (SqlCommand command = new SqlCommand(addQuery2, connection))
                //    {
                //        command.Parameters.AddWithValue("@client", id_client2);

                //        command.Parameters.AddWithValue("@sub", id_sub);

                //        int rowsAffected = command.ExecuteNonQuery();


                //    }

                //}
            }

        }
                


            }
    }


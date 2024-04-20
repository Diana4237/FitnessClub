using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Subscription.xaml
    /// </summary>
    public partial class Subscriptions : Window
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
        int id_class = 0;
        TextBox TextB;
        DateTime Dat;
        string NameTren;
        ListBox list = new ListBox();

        public string Title{ get; set; }
        public DateTime Period { get; set; }
        public string TrenerName { get; set; }
        public decimal Cost { get; set; }





        string type_sub;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public Subscriptions( int id_user)
        {
            InitializeComponent();
            subscriptionsDataGrid.ItemsSource = LoadSubscriptions();


            User = id_user;



        }
        public Subscriptions () {

            InitializeComponent();
            subscriptionsDataGrid.ItemsSource = LoadSubscriptions();
        }




        public ObservableCollection<Subscriptions> LoadSubscriptions()
        {
            var subscriptions = new ObservableCollection<Subscriptions>();

           

           






            string sqlExp = $"SELECT Id_subscription,Id_type_subscription FROM Subscription WHERE Id_client='{User}'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExp, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        id_subsc = reader.GetInt32(0);

                        id_type = reader.GetInt32(1);


                        string sqlExp1 = $"SELECT Id_Class FROM Timing WHERE Id_subscription='{id_subsc}'";
                        using (SqlConnection connection1 = new SqlConnection(connectionString))
                        {
                            connection1.Open();
                            SqlCommand command2 = new SqlCommand(sqlExp1, connection1);
                            SqlDataReader reader2 = command2.ExecuteReader();

                            if (reader2.HasRows)
                            {

                                while (reader2.Read())
                                {
                                    id_class = reader2.GetInt32(0);


                                }
                            }
                            reader2.Close();
                        }



                        string sqlExp2 = $"SELECT Date_Time, Id_Staff FROM Class WHERE Id_Class= '{id_class}'";
                        using (SqlConnection connection2 = new SqlConnection(connectionString))
                        {
                            connection2.Open();
                            SqlCommand command3 = new SqlCommand(sqlExp2, connection2);
                            SqlDataReader reader3 = command3.ExecuteReader();

                            if (reader3.HasRows)
                            {

                                while (reader3.Read())
                                {

                                    date_time = reader3.GetDateTime(0);
                                    Id_staff = reader3.GetInt32(1);

                                }
                            }
                            reader3.Close();
                        }


                        string sqlExp5 = $"SELECT FirstName, LastName, Patronymic, Photo FROM Staff WHERE Id_staff= '{Id_staff}'";
                        using (SqlConnection connection4 = new SqlConnection(connectionString))
                        {
                            connection4.Open();
                            SqlCommand command4 = new SqlCommand(sqlExp5, connection4);
                            SqlDataReader reader4 = command4.ExecuteReader();

                            if (reader4.HasRows)
                            {

                                while (reader4.Read())
                                {
                                    string s1 = reader4.GetString(0);
                                    string s2 = reader4.GetString(1);
                                    string s3 = reader4.GetString(2);


                                    NameTren = s1 + " " + s2 + " " + s3;


                                }
                            }
                            reader4.Close();
                        }


                        string sqlExp4 = $"SELECT Title,Cost FROM Type_subscription WHERE Id_type_subscription= '{id_type}'";
                        using (SqlConnection connection5 = new SqlConnection(connectionString))
                        {
                            connection5.Open();
                            SqlCommand command5 = new SqlCommand(sqlExp4, connection5);
                            SqlDataReader reader5 = command5.ExecuteReader();

                            if (reader5.HasRows)
                            {

                                while (reader5.Read())
                                {
                                    title = reader5.GetString(0);
                                    cost = reader5.GetDecimal(1);

                                }
                            }
                            reader5.Close();


                        }

                            subscriptions.Add(new Subscriptions
                            {
                                Title = title,
                                Period = date_time,
                                TrenerName = NameTren,
                                Cost = cost
                            });

                        

                    }
                }
            }







                        return subscriptions;
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


        public void EditButton(object sender, RoutedEventArgs e)
        {
            //Label label = new Label
            //{
            //    Content = this.Dat.ToString(),
            //};
            //gridTrainers.Children.Add(label);

            EditRaspisanie types = new EditRaspisanie(Role, User);
            types.Show();
        }



        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            // Обработка нажатия на кнопку "Купить"
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
    }
}
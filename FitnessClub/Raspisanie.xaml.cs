using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Raspisanie.xaml
    /// </summary>
    public partial class Raspisanie : Window
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
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public Raspisanie(int idRole, int id_user)
        {
            InitializeComponent();

            Role = idRole;
            User = id_user;


            if (idRole == 1)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                MenuItem inf = new MenuItem { Header = "Infirmation About Club" };
                inf.Click += new RoutedEventHandler(InformLogin);
                MenuItem act = new MenuItem { Header = "Types Of Sport Activities" };
                string sqlExpression = "SELECT Title FROM Type_subscription";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string title = reader.GetString(0);
                            MenuItem menuItem = new MenuItem { Header = title };
                            act.Items.Add(menuItem);
                        }
                    }
                    reader.Close();
                }
                MenuItem Train = new MenuItem { Header = "Trainers", IsEnabled = false };
                MenuItem time = new MenuItem { Header = "Timing" };
                BitmapImage bitmapImage = GetImageFromDatabaseCl(id_user);
                Image img = new Image { Width = 40, Source = bitmapImage };
                MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);


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
                            string s1= reader4.GetString(0);
                            string s2= reader4.GetString(1);
                            string s3= reader4.GetString(2);

                            // fio = s1+s2+s3;
                            NameTren.Text= s1 +" "+ s2+ " " + s3;


                        }
                    }
                    reader4.Close();
                }

                string sqlExp6 = $"SELECT Photo FROM Staff WHERE Id_staff= '{Id_staff}' AND Photo IS NOT NUll";
                using (SqlConnection connection4 = new SqlConnection(connectionString))
                {
                    connection4.Open();
                    SqlCommand command6 = new SqlCommand(sqlExp5, connection4);
                    SqlDataReader reader6 = command6.ExecuteReader();

                    if (reader6.HasRows)
                    {

                        while (reader6.Read())
                        {
                           byte[]phot = (byte[])reader6["Photo"];
                            BitmapImage bitmapImageTren = new BitmapImage();
                            using (MemoryStream stream = new MemoryStream(phot))
                            {
                                bitmapImageTren.BeginInit();
                                bitmapImageTren.CacheOption = BitmapCacheOption.OnLoad;
                                bitmapImageTren.StreamSource = stream;
                                bitmapImageTren.EndInit();
                            }
                            ImageBrush myImageBrush = new ImageBrush(bitmapImageTren);
                            trener.Fill=myImageBrush;

                        }
                    }
                    reader6.Close();
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
                if (title == "Step")
                {
                    ImageBrush myBrush = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("C:/Users/Пользователь/Desktop/OOP/FitnessClub/FitnessClub/images/StepClass.JPG"))
                    };
                    ImageBrush myBrush2 = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("C:/Users/Пользователь/Desktop/OOP/FitnessClub/FitnessClub/images/step2.JPG"))
                    };
                    ImageBrush myBrush3 = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("C:/Users/Пользователь/Desktop/OOP/FitnessClub/FitnessClub/images/step1.jpeg"))
                    };
                    poly1.Fill = myBrush;
                    poly2.Fill = myBrush2;
                    poly3.Fill = myBrush3;
                }
                Class.Text += title;
                string [] date=date_time.ToString().Split(' ');
                Time.Text+= date[1].Substring(0, date[1].Length - 3);
                Date.Text+= date[0];
                CostClass.Text+= cost.ToString();
                //BitmapImage bitmapImage = new BitmapImage();
                //using (MemoryStream stream = new MemoryStream(photo))
                //{
                //    bitmapImage.BeginInit();
                //    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                //    bitmapImage.StreamSource = stream;
                //    bitmapImage.EndInit();
                //}

                //img.Source= bitmapImage;











            }
            if (idRole == 2)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                MenuItem inf = new MenuItem { Header = "Infirmation About Club" };
                inf.Click += new RoutedEventHandler(InformLogin);
                MenuItem act = new MenuItem { Header = "Types Of Sport Activities" };
                string sqlExpression = "SELECT Title FROM Type_subscription";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string title = reader.GetString(0);
                            MenuItem menuItem = new MenuItem { Header = title };
                            act.Items.Add(menuItem);
                        }
                    }
                }
                MenuItem Train = new MenuItem { Header = "Trainers", IsEnabled = false };
                MenuItem time = new MenuItem { Header = "Timing" };
                Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };

                MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);
            }
            if (idRole == 3)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                MenuItem inf = new MenuItem { Header = "Infirmation About Club" };
                inf.Click += new RoutedEventHandler(InformLogin);
                MenuItem act = new MenuItem { Header = "Types Of Sport Activities" };
                string sqlExpression = "SELECT Title FROM Type_subscription";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string title = reader.GetString(0);
                            MenuItem menuItem = new MenuItem { Header = title };
                            act.Items.Add(menuItem);
                        }
                    }
                }
                MenuItem Train = new MenuItem { Header = "Trainers", IsEnabled = false };
                MenuItem time = new MenuItem { Header = "Timing" };
                MenuItem clients = new MenuItem { Header = "Clients" };
                Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
                MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(clients);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);
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
    }
}
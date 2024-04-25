using DocumentFormat.OpenXml.Wordprocessing;
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
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;

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
        TextBox TextB;
        DateTime Dat;

        string type_sub;
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
                MenuItem Train = new MenuItem { Header = "Trainers" };
                Train.Click += new RoutedEventHandler(TrainersClick);
                MenuItem time = new MenuItem { Header = "Timing", IsEnabled = false };
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
                
                MenuItem Train = new MenuItem { Header = "Trainers" };
                Train.Click += new RoutedEventHandler(TrainersClick);
                MenuItem time = new MenuItem { Header = "Timing", IsEnabled = false };
                BitmapImage bitmapImage = GetImageFromDatabaseStaff(id_user);
                Image img = new Image { Width = 40, Source = bitmapImage };

                System.Windows.Controls.MenuItem account = new System.Windows.Controls.MenuItem { Header = img, HorizontalAlignment = System.Windows.HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);



              //  grandStack.Visibility = Visibility.Collapsed;
              poly1.Visibility = Visibility.Collapsed;
                poly2.Visibility = Visibility.Collapsed;
                poly3.Visibility = Visibility.Collapsed;
                trener.Visibility = Visibility.Collapsed;
                CostClass.Visibility = Visibility.Collapsed;    
                scroll.Visibility = Visibility.Collapsed;
                button1.Visibility = Visibility.Visible;
                button2.Visibility = Visibility.Visible;
                button1.Click += new RoutedEventHandler(EditButton);
                button2.Click += new RoutedEventHandler(EditButton2);
                //Trainer.Children.Add(button1);
                //Trainer.Children.Add(button2);



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



                string sqlExp1 = $"SELECT Date_Time FROM Class WHERE Id_staff='{id_user}'";
                using (SqlConnection connection1 = new SqlConnection(connectionString))
                {
                    connection1.Open();
                    SqlCommand command1 = new SqlCommand(sqlExp1, connection1);
                    SqlDataReader reader2 = command1.ExecuteReader();

                    if (reader2.HasRows)
                    {

                        while (reader2.Read())
                        {
                             Dat = reader2.GetDateTime(0);
                           
                           

                Grid gr = new Grid();
                var b = new System.Windows.Controls.Border
                {
                    Margin = new Thickness(30),
                    Background = Brushes.Red,
                    Padding = new Thickness(10),

                };
                            var TextA = new TextBlock
                            {
                                TextWrapping = TextWrapping.Wrap,
                                Margin = new Thickness(10),
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Text = type_sub,
                                FontWeight = FontWeights.Bold,
                                Foreground = Brushes.White,
                            };
                            TextB = new TextBox
                            {
                                TextWrapping = TextWrapping.Wrap,
                                Margin = new Thickness(10),
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Text = Dat.ToString(),
                                
                                FontWeight = FontWeights.Bold,
                                IsEnabled = false
                            };



                            
                            gr.RowDefinitions.Add(new RowDefinition());
                            gr.RowDefinitions.Add(new RowDefinition());
                          
                            Grid.SetColumn(TextA, 0);
                            Grid.SetRow(TextA, 0);
                            Grid.SetColumn(TextB, 1);
                            Grid.SetRow(TextB, 1);
                          
                            gr.Children.Add(TextA);
                            gr.Children.Add(TextB);
                            
                            b.Child = gr;
                            
                            gridTrainers.Children.Add(b);
                            
                        }
                    }
                    reader2.Close();
                }











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
                MenuItem Train = new MenuItem { Header = "Trainers" };
                Train.Click += new RoutedEventHandler(TrainersClick);
                MenuItem time = new MenuItem { Header = "Timing", IsEnabled = false };
                System.Windows.Controls.MenuItem clients = new System.Windows.Controls.MenuItem { Header = "Clients" };
                clients.Click += new RoutedEventHandler(ClientClick);
                BitmapImage bitmapImage = GetImageFromDatabaseStaff(id_user);
                Image img = new Image { Width = 40, Source = bitmapImage };

                System.Windows.Controls.MenuItem account = new System.Windows.Controls.MenuItem { Header = img, HorizontalAlignment = System.Windows.HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(clients);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);



                //  grandStack.Visibility = Visibility.Collapsed;
                poly1.Visibility = Visibility.Collapsed;
                poly2.Visibility = Visibility.Collapsed;
                poly3.Visibility = Visibility.Collapsed;
                trener.Visibility = Visibility.Collapsed;
                CostClass.Visibility = Visibility.Collapsed;
                scroll.Visibility = Visibility.Collapsed;
                button1.Visibility = Visibility.Visible;
                button2.Visibility = Visibility.Visible;
                button3.Visibility = Visibility.Visible;  
                button1.Click += new RoutedEventHandler(EditButton);
                button2.Click += new RoutedEventHandler(EditButton2);
                button3.Click += new RoutedEventHandler(EditButton3);
                //Trainer.Children.Add(button1);
                //Trainer.Children.Add(button2);



              


                string sqlExp1 = $"SELECT Date_Time FROM Class";
                using (SqlConnection connection1 = new SqlConnection(connectionString))
                {
                    connection1.Open();
                    SqlCommand command1 = new SqlCommand(sqlExp1, connection1);
                    SqlDataReader reader2 = command1.ExecuteReader();

                    if (reader2.HasRows)
                    {

                       

                        while (reader2.Read())
                        {
                            Dat = reader2.GetDateTime(0);



                            string sqlExp = $"SELECT Id_Staff FROM Class WHERE Date_Time='{Dat}'";
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


                                    }
                                }
                                reader1.Close();


                            }






                            string sqlExp2 = $"SELECT Subscription FROM Staff WHERE Id_Staff={id_subsc}";
                            using (SqlConnection connection3 = new SqlConnection(connectionString))
                            {
                                connection3.Open();
                                SqlCommand command = new SqlCommand(sqlExp2, connection3);
                                SqlDataReader reader3 = command.ExecuteReader();

                                if (reader3.HasRows)
                                {

                                    while (reader3.Read())
                                    {
                                        type_sub = reader3.GetString(0);


                                    }
                                }
                                reader3.Close();


                            }

                            Grid gr = new Grid();
                            var b = new System.Windows.Controls.Border
                            {
                                Margin = new Thickness(30),
                                Background = Brushes.Red,
                                Padding = new Thickness(10),

                            };
                            var TextA = new TextBlock
                            {
                                TextWrapping = TextWrapping.Wrap,
                                Margin = new Thickness(10),
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Text = type_sub,
                                FontWeight = FontWeights.Bold,
                                Foreground = Brushes.White,
                            };
                            TextB = new TextBox
                            {
                                TextWrapping = TextWrapping.Wrap,
                                Margin = new Thickness(10),
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Text = Dat.ToString(),

                                FontWeight = FontWeights.Bold,
                                IsEnabled = false
                            };



                            //var button1 = new Button
                            //{
                            //    Content = "Изменить",
                            //};

                            //var button2 = new Button
                            //{
                            //    Content = "Создать",
                            //};

                            gr.RowDefinitions.Add(new RowDefinition());
                            gr.RowDefinitions.Add(new RowDefinition());
                            // gr.RowDefinitions.Add(new RowDefinition());
                            // gr.RowDefinitions.Add(new RowDefinition());
                            Grid.SetColumn(TextA, 0);
                            Grid.SetRow(TextA, 0);
                            Grid.SetColumn(TextB, 1);
                            Grid.SetRow(TextB, 1);
                            //  Grid.SetColumn(button1, 2);
                            //   Grid.SetRow(button1, 2);
                            //   button1.Click += new RoutedEventHandler(EditButton);
                            // Grid.SetColumn(button2, 3);
                            // Grid.SetRow(button2, 3);
                            // button2.Click += new RoutedEventHandler(EditButton2);
                            gr.Children.Add(TextA);
                            gr.Children.Add(TextB);
                            //gr.Children.Add(button1);
                            //gr.Children.Add(button2);
                            b.Child = gr;

                            gridTrainers.Children.Add(b);

                        }
                    }
                    reader2.Close();
                }
            }


        }
        public void ClientClick(object sender, RoutedEventArgs e)
        {

            Client t = new Client(User);
            t.Show();
        }
        public void TrainersClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Trainers t = new Trainers(Role, User);
            t.Show();
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
            EditRaspisanie types = new EditRaspisanie(Role, User);
            types.Show();
        }

        public void EditButton2(object sender, RoutedEventArgs e)
        {
            

            AddRaspisanie types = new AddRaspisanie(Role, User, Id_staff);
            types.Show();
        }

        public void EditButton3(object sender, RoutedEventArgs e)
        {


            DeleteRaspisani types = new DeleteRaspisani(Role, User);
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
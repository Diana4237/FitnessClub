using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
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
    /// Логика взаимодействия для TypesActivities.xaml
    /// </summary>
    public partial class TypesActivities : Window
    {
        int Role;
        int User;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public TypesActivities()
        {
            InitializeComponent();
            var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
            ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
            Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

            MenuItem inf = new MenuItem { Header = "Infirmation About Club" };
            inf.Click += new RoutedEventHandler(Inform);
            MenuItem act = new MenuItem { Header = "Types Of Sport Activities", IsEnabled = false };
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
            Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
            MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
            account.Click += new RoutedEventHandler(AccountClick);
            menu.Items.Add(inf);
            menu.Items.Add(act);
            menu.Items.Add(Train);
            menu.Items.Add(account);
            Menustack.Children.Add(menu);
        }
        public TypesActivities(int idRole,int idUser)
        {
            InitializeComponent();
            Role = idRole;
            User = idUser;
            if (idRole == 1)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                MenuItem inf = new MenuItem { Header = "Infirmation About Club" };
                inf.Click += new RoutedEventHandler(InformLogin);
                MenuItem act = new MenuItem { Header = "Types Of Sport Activities",IsEnabled=false};
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
                Train.Click += new RoutedEventHandler(TrainersClickLogin);
                MenuItem time = new MenuItem { Header = "Timing" };
                time.Click += new RoutedEventHandler(TimeClick);
                BitmapImage bitmapImage = GetImageFromDatabaseCl(idUser);
                Image img = new Image { Width = 40, Source = bitmapImage };

                System.Windows.Controls.MenuItem account = new System.Windows.Controls.MenuItem { Header = img, HorizontalAlignment = System.Windows.HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);
            }
            if (idRole == 2)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                MenuItem inf = new MenuItem { Header = "Infirmation About Club" };
                inf.Click += new RoutedEventHandler(InformLogin);
                MenuItem act = new MenuItem { Header = "Types Of Sport Activities" ,IsEnabled = false };
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
                MenuItem Train = new MenuItem { Header = "Trainers"};
                Train.Click += new RoutedEventHandler(TrainersClickLogin);
                MenuItem time = new MenuItem { Header = "Timing" };
                time.Click += new RoutedEventHandler(TimeClick);
                BitmapImage bitmapImage = GetImageFromDatabaseStaff(idUser);
                Image img = new Image { Width = 40, Source = bitmapImage };

                System.Windows.Controls.MenuItem account = new System.Windows.Controls.MenuItem { Header = img, HorizontalAlignment = System.Windows.HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
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
                MenuItem act = new MenuItem { Header = "Types Of Sport Activities", IsEnabled = false };
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
                Train.Click += new RoutedEventHandler(TrainersClickLogin);
                MenuItem time = new MenuItem { Header = "Timing" };
                time.Click += new RoutedEventHandler(TimeClick);
                MenuItem clients = new MenuItem { Header = "Clients" };
                clients.Click += new RoutedEventHandler(ClientClick);
                BitmapImage bitmapImage = GetImageFromDatabaseStaff(idUser);
                Image img = new Image { Width = 40, Source = bitmapImage };

                System.Windows.Controls.MenuItem account = new System.Windows.Controls.MenuItem { Header = img, HorizontalAlignment = System.Windows.HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
                //Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
                //MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(clients);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);
            }
            if (idRole == 4)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                System.Windows.Controls.Menu menu = new System.Windows.Controls.Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                System.Windows.Controls.MenuItem inf = new System.Windows.Controls.MenuItem { Header = "Infirmation About Club" };
                inf.Click += new RoutedEventHandler(InformLogin);
                System.Windows.Controls.MenuItem act = new System.Windows.Controls.MenuItem { Header = "Types Of Sport Activities", IsEnabled = false };
               // act.Click += new RoutedEventHandler(TypeAct);
                
                System.Windows.Controls.MenuItem Train = new System.Windows.Controls.MenuItem { Header = "Trainers" };
                Train.Click += new RoutedEventHandler(TrainersClickLogin);
                System.Windows.Controls.MenuItem aadmins = new System.Windows.Controls.MenuItem { Header = "Admins" };
                aadmins.Click += new RoutedEventHandler(ListAdmins);
                System.Windows.Controls.MenuItem statistic = new System.Windows.Controls.MenuItem { Header = "Dynamics of customer" };
                statistic.Click += new RoutedEventHandler(Statistic);
                BitmapImage bitmapImage = GetImageFromDatabaseStaff(idUser);
                Image img = new Image { Width = 40, Source = bitmapImage };

                System.Windows.Controls.MenuItem account = new System.Windows.Controls.MenuItem { Header = img, HorizontalAlignment = System.Windows.HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(Train);
                menu.Items.Add(aadmins);
                menu.Items.Add(statistic);
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
        public void TypeActLogin(object sender, RoutedEventArgs e)
        {
            //   this.Close();
            TypesActivities types = new TypesActivities(Role, User);
            types.Show();
        }
        public void ClientClick(object sender, RoutedEventArgs e)
        {

            Client t = new Client(User);
            t.Show();
        }
        public void TimeClick(object sender, RoutedEventArgs e)
        {
            // this.Close();
            Raspisanie t = new Raspisanie(Role, User);
            t.Show();
        }
        public void MyAccountClick(object sender, EventArgs e)
        {
            this.Close();
            MyAccount autorization = new MyAccount(Role, User);
            autorization.Show();
        }
        public void Statistic(object sender, RoutedEventArgs e)
        {
            OwnerStatistic ownerStatistic = new OwnerStatistic(User);
            ownerStatistic.Show();
        }
        public void ListAdmins(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Admins t = new Admins(User);
            t.Show();
        }
        public void AccountClick(object sender, EventArgs e)
        {
            Autorization autorization = new Autorization();
            autorization.Show();
        }
        public void TrainersClick(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Trainers t = new Trainers();
            t.Show();
        }
        public void TrainersClickLogin(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Trainers t = new Trainers(Role, User);
            t.Show();
        }
        public void TypeAct(object sender, RoutedEventArgs e)
        {
            TypesActivities types = new TypesActivities();
            types.Show();
        }
        public void Inform(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}

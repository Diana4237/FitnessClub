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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitnessClub
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int Role;
        int User;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public MainWindow()
        {
            InitializeComponent();
            var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
            ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
            Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

            MenuItem inf = new MenuItem { Header = "Infirmation About Club", IsEnabled = false };
            MenuItem act = new MenuItem { Header = "Types Of Sport Activities" };
           act.Click += new RoutedEventHandler(TypeAct);
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
            Train.Click += new RoutedEventHandler(TrainersClick);

            MenuItem Subscription = new MenuItem { Header = "Subscription" };
            Subscription.Click += new RoutedEventHandler(SubscriptionClick);

            Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
            MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
            account.Click+=new RoutedEventHandler(AccountClick);
            menu.Items.Add(inf);
            menu.Items.Add(act);
            menu.Items.Add(Train);
            menu.Items.Add(account);
            Menustack.Children.Add(menu);
        }
        public MainWindow(int idRole,int IdUser)
        {
            InitializeComponent();
            Role=idRole;
            User=IdUser;
            if(idRole == 1)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                MenuItem inf = new MenuItem { Header = "Infirmation About Club", IsEnabled = false };
                MenuItem act = new MenuItem { Header = "Types Of Sport Activities" };
                act.Click += new RoutedEventHandler(TypeActLogin);
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
               
                BitmapImage bitmapImage = GetImageFromDatabaseCl(IdUser);
                Image img=new Image { Width=40,Source=bitmapImage};
                // Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
                MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);
            }
            if(idRole == 2)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                MenuItem inf = new MenuItem { Header = "Infirmation About Club", IsEnabled = false };
                MenuItem act = new MenuItem { Header = "Types Of Sport Activities" };
                act.Click += new RoutedEventHandler(TypeActLogin);
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
                BitmapImage bitmapImage = GetImageFromDatabaseStaff(IdUser);
                Image img = new Image { Width = 40, Source = bitmapImage };
                // Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
                MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);
            }
            if(idRole == 3)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                MenuItem inf = new MenuItem { Header = "Infirmation About Club", IsEnabled = false };
                MenuItem act = new MenuItem { Header = "Types Of Sport Activities" };
                act.Click += new RoutedEventHandler(TypeActLogin);
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
                BitmapImage bitmapImage = GetImageFromDatabaseStaff(IdUser);
                Image img = new Image { Width = 40, Source = bitmapImage };
                // Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
                MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
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
                Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                MenuItem inf = new MenuItem { Header = "Infirmation About Club", IsEnabled = false };
                MenuItem aadmins = new MenuItem { Header = "Admins" };
                aadmins.Click += new RoutedEventHandler(ListAdmins);
                BitmapImage bitmapImage = GetImageFromDatabaseStaff(IdUser);
                Image img = new Image { Width = 40, Source = bitmapImage };
                
                MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
                menu.Items.Add(inf);
                menu.Items.Add(aadmins);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);
            }
        }
        public void AccountClick(object sender, EventArgs e)
        {
            //this.Close();
            Autorization autorization = new Autorization();
            autorization.Show();
        }
        public void MyAccountClick(object sender, EventArgs e)
        {
           // this.Close();
            MyAccount autorization = new MyAccount(Role,User);
            autorization.Show();
        }
        public void TrainersClick(object sender, RoutedEventArgs e)
        {
           // this.Close();
            Trainers t = new Trainers();
            t.Show();
        }
        public void TrainersClickLogin(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Trainers t = new Trainers(Role, User);
            t.Show();
        }
        public void ListAdmins(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Admins t = new Admins(User);
            t.Show();
        }
        public void SubscriptionClick(object sender, RoutedEventArgs e)
        {
           
            Subscriptions t = new Subscriptions();
            t.Show();
        }

        public void ClientClick(object sender, RoutedEventArgs e)
        {
            
            Client t = new Client(User);
            t.Show();
        } 
        public void TimeClick(object sender, RoutedEventArgs e)
        {
           // this.Close();
            Raspisanie t = new Raspisanie(Role,User);
            t.Show();
        }


        public void TypeAct(object sender, RoutedEventArgs e)
        {
            //this.Close();
            TypesActivities types = new TypesActivities();
            types.Show();
        }
        public void TypeActLogin(object sender, RoutedEventArgs e)
        {
         //   this.Close();
            TypesActivities types = new TypesActivities(Role,User);
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

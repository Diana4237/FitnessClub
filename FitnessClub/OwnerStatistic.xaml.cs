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
    /// Логика взаимодействия для OwnerStatistic.xaml
    /// </summary>
    public partial class OwnerStatistic : Window
    {
        int idUs;
        int User;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public OwnerStatistic(int IdUser)
        {
            InitializeComponent();
            User = IdUser;
            DateTime now= DateTime.Now;
            int year=now.Year;
            string sqlExpression = $"SELECT COUNT(*) FROM Users WHERE DateReg>'{year}-01-01 00:00:00.000' AND IdRole=1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count.Text = reader.GetInt32(0).ToString();
                        
                    }
                }
            }
            var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
            ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
            Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

            MenuItem inf = new MenuItem { Header = "Infirmation About Club" };
            inf.Click+= new RoutedEventHandler(Main);
            System.Windows.Controls.MenuItem act = new System.Windows.Controls.MenuItem { Header = "Types Of Sport Activities" };
            act.Click += new RoutedEventHandler(TypeAct);
            string sqlExpression1 = "SELECT Title FROM Type_subscription";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression1, connection);
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
            MenuItem aadmins = new MenuItem { Header = "Admins" };
            aadmins.Click += new RoutedEventHandler(ListAdmins);
            MenuItem statistic = new MenuItem { Header = "Dynamics of customer", IsEnabled = false };
           // statistic.Click += new RoutedEventHandler(Statistic);
            BitmapImage bitmapImage = GetImageFromDatabaseStaff(IdUser);
            Image img = new Image { Width = 40, Source = bitmapImage };

            MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
            account.Click += new RoutedEventHandler(MyAccountClick);
            menu.Items.Add(inf);
            menu.Items.Add(act);
            menu.Items.Add(Train);
            menu.Items.Add(aadmins);
            menu.Items.Add(statistic);
            menu.Items.Add(account);
            Menustack.Children.Add(menu);
        }
        public void TrainersClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Trainers t = new Trainers(4, User);
            t.Show();
        }

        public void TypeAct(object sender, RoutedEventArgs e)
        {
            this.Close();
            TypesActivities types = new TypesActivities(4, User);
            types.Show();
        }
        public void MyAccountClick(object sender, EventArgs e)
        {
            // this.Close();
            MyAccount autorization = new MyAccount(4, User);
            autorization.Show();
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
        public void Main(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow(4, User);
            mainWindow.Show();
        }
        public void ListAdmins(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Admins t = new Admins(User);
            t.Show();
        }

       
    }
}

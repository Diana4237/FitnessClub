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
            string sqlExpression = "SELECT COUNT(*) FROM Client";
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
            MenuItem aadmins = new MenuItem { Header = "Admins" };
            aadmins.Click += new RoutedEventHandler(ListAdmins);
            MenuItem statistic = new MenuItem { Header = "Dynamics of customer", IsEnabled = false };
           // statistic.Click += new RoutedEventHandler(Statistic);
            BitmapImage bitmapImage = GetImageFromDatabaseStaff(IdUser);
            Image img = new Image { Width = 40, Source = bitmapImage };

            MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
            account.Click += new RoutedEventHandler(MyAccountClick);
            menu.Items.Add(inf);
            menu.Items.Add(aadmins);
            menu.Items.Add(statistic);
            menu.Items.Add(account);
            Menustack.Children.Add(menu);
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

        public void gif_Media(object sender,RoutedEventArgs e)
        {
            gif.Position = new TimeSpan(0, 0, 1);
            gif.Play();
        }
    }
}

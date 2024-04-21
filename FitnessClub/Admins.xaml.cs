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
    /// Логика взаимодействия для Admins.xaml
    /// </summary>
    public partial class Admins : Window
    {
        int Role;
        int User;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public Admins(int IdUser)
        {

            InitializeComponent();
            User = IdUser;
            Role = 4;
            var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
            ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
            Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

            MenuItem inf = new MenuItem { Header = "Infirmation About Club" };
            inf.Click += new RoutedEventHandler(Inform);
            MenuItem aadmins = new MenuItem { Header = "Admins", IsEnabled = false };
           // aadmins.Click += new RoutedEventHandler(ListAdmins);
            BitmapImage bitmapImage = GetImageFromDatabaseStaff(IdUser);
            Image img = new Image { Width = 40, Source = bitmapImage };
            // Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
            MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
            account.Click += new RoutedEventHandler(MyAccountClick);
            menu.Items.Add(inf);
            menu.Items.Add(aadmins);
            menu.Items.Add(account);
            Menustack.Children.Add(menu);
            butAddT.Visibility = Visibility.Visible;
            string sqlExp = "SELECT Lastname,Firstname,Patronymic,Id_staff,Telephone FROM Staff WHERE Id_role=3";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExp, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        string Lastname = reader.GetString(0);
                        string Firstname = reader.GetString(1);
                        string Patronymic = reader.GetString(2);
                        string tel = reader.GetString(4);
                        int Id = reader.GetInt32(3);

                        Grid gr = new Grid();
                        var b = new Border
                        {
                            Margin = new Thickness(30),
                            Background = Brushes.White,
                            Padding = new Thickness(10),
                            CornerRadius = new CornerRadius(10)
                        };
                        BitmapImage bitmapImage2 = GetImageFromDatabaseStaff(Id);
                        var b1 = new Border
                        {
                            BorderThickness = new Thickness(2),
                            BorderBrush = Brushes.Red,
                            Width = 110,
                            Height = 130,
                        };
                        var imag = new Image
                        {
                            Margin = new Thickness(10),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Width = 100,
                            Height = 120,
                            Source = bitmapImage2,

                        };
                        var TextB = new TextBlock
                        {
                            TextWrapping = TextWrapping.Wrap,
                            Margin = new Thickness(10),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Text = Lastname + " " + Firstname + " " + Patronymic,
                            FontWeight = FontWeights.Bold
                        };
                        var TextB2 = new TextBlock
                        {
                            TextWrapping = TextWrapping.Wrap,
                            Text = "ContactData: " + tel,
                            FontSize = 10,
                        };


                        gr.RowDefinitions.Add(new RowDefinition());
                        gr.RowDefinitions.Add(new RowDefinition());
                        gr.RowDefinitions.Add(new RowDefinition());

                        gr.ColumnDefinitions.Add(new ColumnDefinition());
                        Grid.SetColumn(imag, 0);
                        Grid.SetRow(imag, 0);
                        Grid.SetColumn(TextB, 0);
                        Grid.SetRow(TextB, 1);
                        Grid.SetColumn(TextB2, 0);
                        Grid.SetRow(TextB2, 2);

                        b1.Child = imag;
                        gr.Children.Add(b1);
                        gr.Children.Add(TextB);
                        gr.Children.Add(TextB2);

                        b.Child = gr;
                        gridTrainers.Children.Add(b);
                    }
                }
                reader.Close();
            }
           
        }
        public void Inform(object sender, RoutedEventArgs e)
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
        public void Addadmin(object sender, EventArgs e)
        {
            OwnerAdminAdd addTrainer = new OwnerAdminAdd(User);
            addTrainer.Show();
        }
        
        public void MyAccountClick(object sender, EventArgs e)
        {
            // this.Close();
            MyAccount autorization = new MyAccount(Role, User);
            autorization.Show();
        }
    }
}

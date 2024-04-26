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
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        int Role;
        int User;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public Client(int IdUser)
        {
            InitializeComponent();
            User = IdUser;

            var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
            ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
            Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

            MenuItem inf = new MenuItem { Header = "Infirmation About Club" };
            inf.Click += new RoutedEventHandler(InformLogin);
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
            Train.Click += new RoutedEventHandler(TrainersClick);
            MenuItem time = new MenuItem { Header = "Timing" };
            time.Click += new RoutedEventHandler(TimeClick);
            MenuItem clients = new MenuItem { Header = "Clients", IsEnabled = false };
            BitmapImage bitmapImag = GetImageFromDatabaseStaff(IdUser);
            Image img = new Image { Width = 40, Source = bitmapImag };
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
            butAddT.Visibility = Visibility.Visible;
            string sqlExp = "SELECT Lastname,Firstname,Patronymic,Id_client,Telephone FROM Client";
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
                        BitmapImage bitmapImage = GetImageFromDatabaseStaff(Id);
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
                            Source = bitmapImage,

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
    public void TimeClick(object sender, RoutedEventArgs e)
    {
        this.Close();
        Raspisanie t = new Raspisanie(3, User);
        t.Show();
    }
    public void MyAccountClick(object sender, EventArgs e)
    {
        this.Close();
        MyAccount autorization = new MyAccount(3, User);
        autorization.Show();
    }
    public void TypeActLogin(object sender, RoutedEventArgs e)
    {
        this.Close();
        TypesActivities types = new TypesActivities(3, User);
        types.Show();
    }
    public BitmapImage GetImageFromDatabaseStaff(int id)
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
        public void TrainersClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Trainers t = new Trainers(3, User);
            t.Show();
        }
        public void AddClient(object sender, EventArgs e)
    {
        AddClient addClient = new AddClient(User);
        addClient.Show();
    }
    public void AccountClick(object sender, EventArgs e)
    {
        this.Close();
        Autorization autorization = new Autorization();
        autorization.Show();
    }
    public void Inform(object sender, RoutedEventArgs e)
    {
        this.Close();
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }
    public void InformLogin(object sender, RoutedEventArgs e)
    {
        this.Close();
        MainWindow mainWindow = new MainWindow(3, User);
        mainWindow.Show();
    }
    public void TypeAct(object sender, RoutedEventArgs e)
    {
        this.Close();
        TypesActivities types = new TypesActivities();
        types.Show();
    }
}
}

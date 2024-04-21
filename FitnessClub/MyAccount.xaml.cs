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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitnessClub
{
    /// <summary>
    /// Логика взаимодействия для MyAccount.xaml
    /// </summary>
    public partial class MyAccount : Window
    {
        int Role;
        int User;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public MyAccount(int idRole,int IdUser)
        {
            Role = idRole;
            User = IdUser;
            InitializeComponent();
            if (idRole == 1)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                System.Windows.Controls.Menu menu = new System.Windows.Controls.Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                System.Windows.Controls.MenuItem inf = new System.Windows.Controls.MenuItem { Header = "Infirmation About Club" };
                inf.Click += new RoutedEventHandler(Inform);
                System.Windows.Controls.MenuItem act = new System.Windows.Controls.MenuItem { Header = "Types Of Sport Activities" };
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
                            System.Windows.Controls.MenuItem menuItem = new System.Windows.Controls.MenuItem { Header = title };
                            act.Items.Add(menuItem);
                        }
                    }
                }
                System.Windows.Controls.MenuItem Train = new System.Windows.Controls.MenuItem { Header = "Trainers" };
                Train.Click += new RoutedEventHandler(TrainersClick);
                System.Windows.Controls.MenuItem time = new System.Windows.Controls.MenuItem { Header = "Timing" };
                time.Click += new RoutedEventHandler(TimeClick);
                System.Windows.Controls.MenuItem sub = new System.Windows.Controls.MenuItem { Header = "Subscriptions" };

                BitmapImage bitmapImage = GetImageFromDatabaseCl(IdUser);
                Image img = new Image { Width = 40, Source = bitmapImage };
               
                System.Windows.Controls.MenuItem account = new System.Windows.Controls.MenuItem { Header = img,HorizontalAlignment=System.Windows.HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);


                string sqlMydata = $"SELECT FirstName, LastName, Telephone FROM Client WHERE Id_client='{IdUser}'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlMydata, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string title = reader.GetString(1)+" "+reader.GetString(0) ;
                            fio.Text += title;
                            string phone= reader.GetString(2);
                            phon.Text += phone;
                        }
                    }
                }
                imgAcc.ImageSource = GetImageFromDatabaseCl(IdUser);
            }
            if (idRole == 2)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                System.Windows.Controls.Menu menu = new System.Windows.Controls.Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                System.Windows.Controls.MenuItem inf = new System.Windows.Controls.MenuItem { Header = "Infirmation About Club", IsEnabled = false };
                System.Windows.Controls.MenuItem act = new System.Windows.Controls.MenuItem { Header = "Types Of Sport Activities" };
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
                            System.Windows.Controls.MenuItem menuItem = new System.Windows.Controls.MenuItem { Header = title };
                            act.Items.Add(menuItem);
                        }
                    }
                }
                System.Windows.Controls.MenuItem Train = new System.Windows.Controls.MenuItem { Header = "Trainers" };
                Train.Click += new RoutedEventHandler(TrainersClick);
                System.Windows.Controls.MenuItem time = new System.Windows.Controls.MenuItem { Header = "Timing" };
                BitmapImage bitmapImage = GetImageFromDatabaseStaff(IdUser);
                Image img = new Image { Width = 40, Source = bitmapImage };
                // Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
                System.Windows.Controls.MenuItem account = new System.Windows.Controls.MenuItem { Header = img, HorizontalAlignment = System.Windows.HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);
                string sqlMydata = $"SELECT FirstName, LastName, Patronymic, Telephone FROM Staff WHERE Id_staff='{IdUser}'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlMydata, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string title = reader.GetString(1) + " " + reader.GetString(0);
                            fio.Text += title;
                            string phone = reader.GetString(2);
                            phon.Text += phone;
                        }
                    }
                }
                imgAcc.ImageSource = GetImageFromDatabaseStaff(IdUser);
            }
            if (idRole == 3)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                System.Windows.Controls.Menu menu = new System.Windows.Controls.Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                System.Windows.Controls.MenuItem inf = new System.Windows.Controls.MenuItem { Header = "Infirmation About Club", IsEnabled = false };
                System.Windows.Controls.MenuItem act = new System.Windows.Controls.MenuItem { Header = "Types Of Sport Activities" };
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
                            System.Windows.Controls.MenuItem menuItem = new System.Windows.Controls.MenuItem { Header = title };
                            act.Items.Add(menuItem);
                        }
                    }
                }
                System.Windows.Controls.MenuItem Train = new System.Windows.Controls.MenuItem { Header = "Trainers" };
                Train.Click += new RoutedEventHandler(TrainersClick);
                System.Windows.Controls.MenuItem time = new System.Windows.Controls.MenuItem { Header = "Timing" };
                System.Windows.Controls.MenuItem clients = new System.Windows.Controls.MenuItem { Header = "Clients" };
                BitmapImage bitmapImage = GetImageFromDatabaseStaff(IdUser);
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
                string sqlMydata = $"SELECT FirstName, LastName, Patronymic, Telephone FROM Staff WHERE Id_staff='{IdUser}'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlMydata, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string title = reader.GetString(1) + " " + reader.GetString(0);
                            fio.Text += title;
                            string phone = reader.GetString(2);
                            phon.Text += phone;
                        }
                    }
                }
                imgAcc.ImageSource = GetImageFromDatabaseStaff(IdUser);
            }
            if (idRole == 4)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                System.Windows.Controls.Menu menu = new System.Windows.Controls.Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                System.Windows.Controls.MenuItem inf = new System.Windows.Controls.MenuItem { Header = "Infirmation About Club", IsEnabled = false };
                System.Windows.Controls.MenuItem aadmins = new System.Windows.Controls.MenuItem { Header = "Admins" };
                aadmins.Click += new RoutedEventHandler(ListAdmins);
                BitmapImage bitmapImage = GetImageFromDatabaseStaff(IdUser);
                Image img = new Image { Width = 40, Source = bitmapImage };

                System.Windows.Controls.MenuItem account = new System.Windows.Controls.MenuItem { Header = img, HorizontalAlignment = System.Windows.HorizontalAlignment.Right };
                account.Click += new RoutedEventHandler(MyAccountClick);
                menu.Items.Add(inf);
                menu.Items.Add(aadmins);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);
            }
        }
        public void ListAdmins(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Admins t = new Admins(User);
            t.Show();
        }
        public void MyAccountClick(object sender, EventArgs e)
        {
            this.Close();
            MyAccount autorization = new MyAccount(Role, User);
            autorization.Show();
        }
        public void TrainersClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Trainers t = new Trainers(Role,User);
            t.Show();
        }

        public void TimeClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Raspisanie t = new Raspisanie(Role, User);
            t.Show();
        }


        public void TypeAct(object sender, RoutedEventArgs e)
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
        public void Inform(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow(Role, User);
            mainWindow.Show();
        }
        public void photoAdd(object sender, RoutedEventArgs e)
        {
            int c = 0;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            try
            {
                byte[] image_bytes = File.ReadAllBytes(openFileDialog.FileName);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    if (Role > 1)
                    {
                        command.CommandText = $"UPDATE Staff SET Photo=@ImageData WHERE Id_staff='{User}'";
                    }
                    else
                    {
                        command.CommandText = $"UPDATE Client SET Photo=@ImageData WHERE Id_client='{User}'";

                    }
                    command.Parameters.Add("@ImageData", SqlDbType.Image, 1000000);
                    command.Parameters["@ImageData"].Value = image_bytes;
                    command.ExecuteNonQuery();
                    c++;
                }
                if (c > 0)
                {
                    if (Role > 1)
                    {
                        BitmapImage bitmapImage = GetImageFromDatabaseStaff(User);
                        imgAcc.ImageSource = bitmapImage;
                    }
                    else
                    {
                        BitmapImage bitmapImage = GetImageFromDatabaseCl(User);
                        imgAcc.ImageSource = bitmapImage;
                    }
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("The photo was not uploaded");
            }
        }
    }
}

﻿using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
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
    /// Логика взаимодействия для Trainers.xaml
    /// </summary>
    public partial class Trainers : Window
    {
        int Role;
        int User;
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public Trainers()
        {
            InitializeComponent();
            var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
            ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
            Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

            MenuItem inf = new MenuItem { Header = "Infirmation About Club" };
            inf.Click += new RoutedEventHandler(Inform);
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
            MenuItem Train = new MenuItem { Header = "Trainers", IsEnabled=false };
            Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
            MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
            account.Click += new RoutedEventHandler(AccountClick);
            menu.Items.Add(inf);
            menu.Items.Add(act);
            menu.Items.Add(Train);
            menu.Items.Add(account);
            Menustack.Children.Add(menu);
            string sqlExp = "SELECT Lastname,Firstname,Patronymic,Id_staff,Telephone,Achievements FROM Staff WHERE Id_role=2";
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
                        string tel=reader.GetString(4);
                        int Id = reader.GetInt32(3);
                        string achieve=reader.GetString(5);
                        Grid gr = new Grid();
                        var b = new Border
                        {
                            Margin = new Thickness(30),
                            Background = Brushes.White,
                            Padding = new Thickness(10),
                            CornerRadius=new CornerRadius(10)
                        };
                        BitmapImage bitmapImage = GetImageFromDatabaseStaff(Id);
                        var b1 = new Border { BorderThickness = new Thickness(2),BorderBrush=Brushes.Red,
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
                            FontWeight= FontWeights.Bold
                        };
                        var TextB2 = new TextBlock
                        {
                            TextWrapping = TextWrapping.Wrap,
                            Text = "ContactData: "+ tel,
                            FontSize = 10,
                        };
                        var TextB3 = new TextBlock
                        {
                            TextWrapping = TextWrapping.Wrap,
                            Text = achieve,
                            FontSize = 10,
                        };
                        gr.RowDefinitions.Add(new RowDefinition());
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
                        Grid.SetColumn(TextB3, 0);
                        Grid.SetRow(TextB3, 3);
                        b1.Child = imag;
                        gr.Children.Add(b1);
                        gr.Children.Add(TextB);
                        gr.Children.Add(TextB2);
                        gr.Children.Add(TextB3);
                        b.Child = gr;
                        gridTrainers.Children.Add(b);
                    }
                }
                reader.Close();
            }
            
        }
        public Trainers(int idRole,int IdUser)
        {
            InitializeComponent();
            Role = idRole;
            User = IdUser;
            if (idRole == 1)
            {
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
                MenuItem Train = new MenuItem { Header = "Trainers", IsEnabled = false };
                MenuItem time = new MenuItem { Header = "Timing" };
                time.Click += new RoutedEventHandler(TimeClick);
                BitmapImage bitmapImage = GetImageFromDatabaseCl(IdUser);
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
                string sqlExp = "SELECT Lastname,Firstname,Patronymic,Id_staff,Telephone,Achievements FROM Staff WHERE Id_role=2";
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
                            string achieve = reader.GetString(5);
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
                            var TextB3 = new TextBlock
                            {
                                TextWrapping = TextWrapping.Wrap,
                                Text = achieve,
                                FontSize = 10,
                            };
                            gr.RowDefinitions.Add(new RowDefinition());
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
                            Grid.SetColumn(TextB3, 0);
                            Grid.SetRow(TextB3, 3);
                            b1.Child = imag;
                            gr.Children.Add(b1);
                            gr.Children.Add(TextB);
                            gr.Children.Add(TextB2);
                            gr.Children.Add(TextB3);
                            b.Child = gr;
                            gridTrainers.Children.Add(b);
                        }
                    }
                    reader.Close();
                }

            }
            if (idRole == 2)
            {
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
                MenuItem Train = new MenuItem { Header = "Trainers", IsEnabled = false };
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
                string sqlExp = "SELECT Lastname,Firstname,Patronymic,Id_staff,Telephone,Achievements FROM Staff WHERE Id_role=2";
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
                            string achieve = reader.GetString(5);
                            Grid gr = new Grid();
                            var b = new Border
                            {
                                Margin = new Thickness(30),
                                Background = Brushes.White,
                                Padding = new Thickness(10),
                                CornerRadius = new CornerRadius(10)
                            };
                            BitmapImage bitmapImage3 = GetImageFromDatabaseStaff(Id);
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
                                Source = bitmapImage3,

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
                            var TextB3 = new TextBlock
                            {
                                TextWrapping = TextWrapping.Wrap,
                                Text = achieve,
                                FontSize = 10,
                            };
                            gr.RowDefinitions.Add(new RowDefinition());
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
                            Grid.SetColumn(TextB3, 0);
                            Grid.SetRow(TextB3, 3);
                            b1.Child = imag;
                            gr.Children.Add(b1);
                            gr.Children.Add(TextB);
                            gr.Children.Add(TextB2);
                            gr.Children.Add(TextB3);
                            b.Child = gr;
                            gridTrainers.Children.Add(b);
                        }
                    }
                    reader.Close();
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
                MenuItem Train = new MenuItem { Header = "Trainers", IsEnabled = false };
                MenuItem time = new MenuItem { Header = "Timing" };
                time.Click += new RoutedEventHandler(TimeClick);
                MenuItem clients = new MenuItem { Header = "Clients" };
                clients.Click += new RoutedEventHandler(ClientClick);
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
                string sqlExp = "SELECT Lastname,Firstname,Patronymic,Id_staff,Telephone,Achievements FROM Staff WHERE Id_role=2";
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
                            string achieve = reader.GetString(5);
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
                            var TextB3 = new TextBlock
                            {
                                TextWrapping = TextWrapping.Wrap,
                                Text = achieve,
                                FontSize = 10,
                            };
                            gr.RowDefinitions.Add(new RowDefinition());
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
                            Grid.SetColumn(TextB3, 0);
                            Grid.SetRow(TextB3, 3);
                            b1.Child = imag;
                            gr.Children.Add(b1);
                            gr.Children.Add(TextB);
                            gr.Children.Add(TextB2);
                            gr.Children.Add(TextB3);
                            b.Child = gr;
                            gridTrainers.Children.Add(b);
                        }
                    }
                    reader.Close();
                }
            }
            if (idRole == 4)
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
                System.Windows.Controls.MenuItem Train = new System.Windows.Controls.MenuItem { Header = "Trainers",IsEnabled=false };
                //Train.Click += new RoutedEventHandler(TrainersClick);
                System.Windows.Controls.MenuItem aadmins = new System.Windows.Controls.MenuItem { Header = "Admins" };
                aadmins.Click += new RoutedEventHandler(ListAdmins);
                System.Windows.Controls.MenuItem statistic = new System.Windows.Controls.MenuItem { Header = "Dynamics of customer" };
                statistic.Click += new RoutedEventHandler(Statistic);
                BitmapImage bitmapImage = GetImageFromDatabaseStaff(IdUser);
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
                string sqlExp = "SELECT Lastname,Firstname,Patronymic,Id_staff,Telephone,Achievements FROM Staff WHERE Id_role=2";
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
                            string achieve = reader.GetString(5);
                            Grid gr = new Grid();
                            var b = new Border
                            {
                                Margin = new Thickness(30),
                                Background = Brushes.White,
                                Padding = new Thickness(10),
                                CornerRadius = new CornerRadius(10)
                            };
                            BitmapImage bitmapImage4 = GetImageFromDatabaseStaff(Id);
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
                                Source = bitmapImage4,

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
                            var TextB3 = new TextBlock
                            {
                                TextWrapping = TextWrapping.Wrap,
                                Text = achieve,
                                FontSize = 10,
                            };
                            gr.RowDefinitions.Add(new RowDefinition());
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
                            Grid.SetColumn(TextB3, 0);
                            Grid.SetRow(TextB3, 3);
                            b1.Child = imag;
                            gr.Children.Add(b1);
                            gr.Children.Add(TextB);
                            gr.Children.Add(TextB2);
                            gr.Children.Add(TextB3);
                            b.Child = gr;
                            gridTrainers.Children.Add(b);
                        }
                    }
                    reader.Close();
                }
            }
        }
        public void ClientClick(object sender, RoutedEventArgs e)
        {

            Client t = new Client(User);
            t.Show();
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
        public void TimeClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Raspisanie t = new Raspisanie(Role, User);
            t.Show();
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
        public void AddTren(object sender, EventArgs e)
        {
            AddTrainer addTrainer = new AddTrainer();
            addTrainer.Show();
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
            MainWindow mainWindow = new MainWindow(Role,User);
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

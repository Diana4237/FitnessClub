﻿using System;
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

                BitmapImage bitmapImage = GetImageFromDatabase(IdUser);
                Image img = new Image { Width = 40, Source = bitmapImage };
                // Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
                System.Windows.Controls.MenuItem account = new System.Windows.Controls.MenuItem { Header = img };
                account.Click += new RoutedEventHandler(MyAccountClick);
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);


                string sqlMydata = $"SELECT FirstName, LastName, Patronymic FROM Client WHERE Id_client='{IdUser}'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlMydata, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string title = reader.GetString(0)+reader.GetString(1) + reader.GetString(2) ;
                            fio.Text = title;
                        }
                    }
                }
                imgAcc.Source = GetImageFromDatabase(IdUser);
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
                Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
                //, HorizontalAlignment = HorizontalAlignment.Right
                System.Windows.Controls.MenuItem account = new System.Windows.Controls.MenuItem { Header = img };
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
                Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
                System.Windows.Controls.MenuItem account = new System.Windows.Controls.MenuItem { Header = img };
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(clients);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);
            }
        }
        public void MyAccountClick(object sender, EventArgs e)
        {
            MyAccount autorization = new MyAccount(Role, User);
            autorization.Show();
        }
        public void TrainersClick(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Trainers t = new Trainers();
            t.Show();
        }

        public void TimeClick(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Raspisanie t = new Raspisanie(Role, User);
            t.Show();
        }


        public void TypeAct(object sender, RoutedEventArgs e)
        {
            TypesActivities types = new TypesActivities();
            types.Show();
        }

        public BitmapImage GetImageFromDatabase(int id)
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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
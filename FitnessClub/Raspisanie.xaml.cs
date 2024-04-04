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
    /// Логика взаимодействия для Raspisanie.xaml
    /// </summary>
    public partial class Raspisanie : Window
    {
        
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
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public Raspisanie(int idRole, int id_user)
        {
            InitializeComponent();

            


            if (idRole == 1)
            {
                var factoryPanel = new FrameworkElementFactory(typeof(DockPanel));
                ItemsPanelTemplate it = new ItemsPanelTemplate { VisualTree = factoryPanel };
                Menu menu = new Menu { Background = new SolidColorBrush(Colors.Red), ItemsPanel = it };

                MenuItem inf = new MenuItem { Header = "Infirmation About Club" };
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
                MenuItem Train = new MenuItem { Header = "Trainers", IsEnabled = false };
                MenuItem time = new MenuItem { Header = "Timing" };
                Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
                MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
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

                           fio = s1+s2+s3;
                           // if ((byte[])reader4["Photo"] != null) { 
                           //photo= (byte[])reader4["Photo"];
                           // }
                        }
                    }
                    reader4.Close();
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
                    ImageBrush myBrush = new ImageBrush {ImageSource= 
                        new BitmapImage(new Uri(@"\images\StepClass.JPG")) };
                    grandStack.Background = myBrush;

                }
                text1.Text += title;
                text2.Text+= date_time.ToString();
                text3.Text+= cost.ToString();
                text4.Text+= fio.ToString();

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
                MenuItem Train = new MenuItem { Header = "Trainers", IsEnabled = false };
                MenuItem time = new MenuItem { Header = "Timing" };
                Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };

                MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
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
                MenuItem Train = new MenuItem { Header = "Trainers", IsEnabled = false };
                MenuItem time = new MenuItem { Header = "Timing" };
                MenuItem clients = new MenuItem { Header = "Clients" };
                Image img = new Image { Width = 40, Source = new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\FitnessClub\\FitnessClub\\images\\klipartz.com.png")) };
                MenuItem account = new MenuItem { Header = img, HorizontalAlignment = HorizontalAlignment.Right };
                menu.Items.Add(inf);
                menu.Items.Add(act);
                menu.Items.Add(time);
                menu.Items.Add(Train);
                menu.Items.Add(clients);
                menu.Items.Add(account);
                Menustack.Children.Add(menu);
            }
        }
    }
}
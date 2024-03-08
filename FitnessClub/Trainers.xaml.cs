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
        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
        public Trainers()
        {
            InitializeComponent();
            string sqlExpression = "SELECT Lastname,Firstname,Patronymic,Id_staff,Telephone,Achievements FROM Staff WHERE Id_role=2";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
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
                           // Width = 220,
                            //Height = 190,
                        };
                        BitmapImage bitmapImage = GetImageFromDatabase(Id);
                        var b1 = new Border { BorderThickness = new Thickness(1),BorderBrush=Brushes.Red,
                            Width = 110,
                            Height = 130,
                        };
                        var img = new Image
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
                            Margin = new Thickness(10),
                            //HorizontalAlignment = HorizontalAlignment.Center,
                            Text = "ContactData: "+ tel,
                            FontSize = 10,
                        };
                        var TextB3 = new TextBlock
                        {
                            TextWrapping = TextWrapping.Wrap,
                            Margin = new Thickness(10),
                            //HorizontalAlignment = HorizontalAlignment.Center,
                            Text = achieve,
                            FontSize = 10,
                        };
                        gr.RowDefinitions.Add(new RowDefinition());
                        gr.RowDefinitions.Add(new RowDefinition());
                        gr.RowDefinitions.Add(new RowDefinition());
                        gr.ColumnDefinitions.Add(new ColumnDefinition());
                        Grid.SetColumn(img, 0);
                        Grid.SetRow(img, 0);
                        Grid.SetColumn(TextB, 0);
                        Grid.SetRow(TextB, 1);
                        Grid.SetColumn(TextB2, 0);
                        Grid.SetRow(TextB2, 2);
                        Grid.SetColumn(TextB3, 0);
                        Grid.SetRow(TextB3, 3);
                        b1.Child = img;
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
        public BitmapImage GetImageFromDatabase(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT Photo FROM Staff WHERE Id_staff = '{id}' AND Photo IS NOT NUll";
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

using DocumentFormat.OpenXml.Spreadsheet;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Button = System.Windows.Controls.Button;
using ListBox = System.Windows.Controls.ListBox;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;


namespace FitnessClub
{
    /// <summary>
    /// Логика взаимодействия для EditRaspisanie.xaml
    /// </summary>
    public partial class EditRaspisanie : Window
    {

        TextBox text1 = new System.Windows.Controls.TextBox();
        int Role;
        string stroka;
        int User;
        int id_type = 0;
        int id_subsc = 0;
        public int Id_staff;
        public string type_sub;
        public decimal cost;
        public string fio;
        public byte[] photo;
        int id_class = 0;
       
        DateTime Dat;


        string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
      //  System.Windows.Controls.ComboBox list2 = new System.Windows.Controls.ComboBox();
        public EditRaspisanie(int idRole,int idUser)
        {
            InitializeComponent();
            Role = idRole;
            User = idUser;

            // Date = date;

            // System.Windows.Controls.ComboBox list2 = new System.Windows.Controls.ComboBox();
            if (Role == 2)
            {
                string query = $"SELECT Date_Time FROM Class WHERE Id_staff='{idUser}'";



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Открытие подключения
                    connection.Open();

                    // Создание команды SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Выполнение команды и получение SqlDataReader для чтения данных
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Очистка текущих элементов ListBox
                            list.Items.Clear();

                            // Чтение данных из reader и добавление их в ListBox
                            while (reader.Read())
                            {
                                list.Items.Add(reader["Date_Time"].ToString());
                            }
                        }
                    }
                }
            }

            if (Role == 3)
            {
                string query = $"SELECT Date_Time FROM Class";



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Открытие подключения
                    connection.Open();

                    // Создание команды SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Выполнение команды и получение SqlDataReader для чтения данных
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Очистка текущих элементов ListBox
                            list.Items.Clear();

                            // Чтение данных из reader и добавление их в ListBox
                            while (reader.Read())

                            {
                                      Dat = reader.GetDateTime(0); 
                                string sqlExp = $"SELECT Id_Staff FROM Class WHERE Date_Time='{Dat}'";
                                using (SqlConnection connection4 = new SqlConnection(connectionString))
                                {
                                    connection4.Open();
                                    SqlCommand command1 = new SqlCommand(sqlExp, connection4);
                                    SqlDataReader reader1 = command1.ExecuteReader();

                                    if (reader1.HasRows)
                                    {

                                        while (reader1.Read())
                                        {
                                            id_subsc = reader1.GetInt32(0);


                                        }
                                    }
                                    reader1.Close();


                                }






                                string sqlExp2 = $"SELECT Subscription FROM Staff WHERE Id_Staff={id_subsc}";
                                using (SqlConnection connection3 = new SqlConnection(connectionString))
                                {
                                    connection3.Open();
                                    SqlCommand command3 = new SqlCommand(sqlExp2, connection3);
                                    SqlDataReader reader3 = command3.ExecuteReader();

                                    if (reader3.HasRows)
                                    {

                                        while (reader3.Read())
                                        {
                                            type_sub = reader3.GetString(0);


                                        }
                                    }
                                    reader3.Close();


                                }





                                list.Items.Add(reader["Date_Time"].ToString() +"_"+ type_sub) ;
                               
                            }
                        }
                    }
                }






















            }



            }


        public class Time
        {
            public string time { get; set; } = "";
            
        }

        public void EditButton(object sender, RoutedEventArgs e)
        {
            stroka = list.SelectedItem.ToString();
            string[] words = stroka.Split('_');

             stroka = words[0];


            TextBlock Block = new TextBlock
            {
                Text = "Выберите новое время:",
            };

         text1.Text = stroka;

            Button but1 = new System.Windows.Controls.Button
            {
                Content="Изменить",
            };
            but1.Click += new RoutedEventHandler(EditButton1);
            stack.Children.Add(Block);
            stack.Children.Add(text1);
            stack.Children.Add(but1);


        }

        public void EditButton1(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Role == 2)
                {
                    string newData = text1.Text;
                    //string newData = "2024-07-07 08:00:00.000"; // Новое имя, которое вы хотите установить

                    string updateQuery = "UPDATE Class SET Date_Time = @newData WHERE Id_Staff = @User AND Date_Time= @stroka";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@newData", newData);
                            command.Parameters.AddWithValue("@User", User);
                            command.Parameters.AddWithValue("@stroka", stroka);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {





                                MessageBox.Show("Дата успешно изменена");
                            }
                            else
                            {
                                MessageBox.Show("Ошибка при обновлении данных.");
                            }
                        }

                    }
                }

                if (Role == 3)
                {



                    string newData = text1.Text;
                    //string newData = "2024-07-07 08:00:00.000"; // Новое имя, которое вы хотите установить

                    string updateQuery = "UPDATE Class SET Date_Time = @newData WHERE  Date_Time= @stroka";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@newData", newData);
                            command.Parameters.AddWithValue("@User", User);
                            command.Parameters.AddWithValue("@stroka", stroka);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {





                                MessageBox.Show("Дата успешно изменена");
                            }
                            else
                            {
                                MessageBox.Show("Ошибка при обновлении данных.");
                            }
                        }

                    }


                }
            }

            catch
            {
                MessageBox.Show("Ошибка в вводе данных. Перепроверьте поле.");
            }

        }

        
    }
}
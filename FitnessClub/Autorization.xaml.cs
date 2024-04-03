using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Forms;

namespace FitnessClub
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        DB dataBase = new DB();
        public int Idrol;
        public int IdUs;
        public Autorization()
        {
            InitializeComponent();
        }
        public void autorization_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var log = login.Text;
                var pass = password.Text;
                if (log.ToString().All(char.IsWhiteSpace) || pass.ToString().All(char.IsWhiteSpace))
                {
                    System.Windows.MessageBox.Show("Text in TextBox=NULL", "Error", (MessageBoxButton)MessageBoxButtons.OK,
                    (MessageBoxImage)MessageBoxIcon.Error);
                }
                else
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataTable table = new DataTable();
                    string querystring = $"SELECT * FROM Users WHERE LoginUser = '{log}' AND PasswordUser= '{pass}' ";
                    string find = $"SELECT IdRole,IdUser FROM Users WHERE LoginUser = '{log}' AND PasswordUser= '{pass}' ";
                    string connectionString = @"Data Source=-PC\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(find, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                               Idrol = reader.GetInt32(0);
                               IdUs = reader.GetInt32(1);

                            }
                        }
                        reader.Close();
                    }
                    SqlCommand command1 = new SqlCommand(querystring, dataBase.getConnection());
                    adapter.SelectCommand = command1;
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        System.Windows.MessageBox.Show("Successful authorization!!! ");
                        MainWindow form = new MainWindow(Idrol,IdUs);
                        this.Hide();
                        form.ShowDialog();

                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Login or password incorrect", "Error", (MessageBoxButton)MessageBoxButtons.OK,
                        (MessageBoxImage)MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                login.Clear();
                password.Clear();
                System.Windows.MessageBox.Show("Incorrect input, please try again", "Error", (MessageBoxButton)MessageBoxButtons.OK,
            (MessageBoxImage)MessageBoxIcon.Error);
            }
        }
    }
}

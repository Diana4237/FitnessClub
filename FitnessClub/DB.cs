using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class DB
    {
        SqlConnection sqlconnection = new SqlConnection("Data Source=-PC\\MSSQLSERVER01; Initial Catalog=FitnessClub;Integrated Security=True;");

        public void OpenConnection()
        {
            if (sqlconnection.State == System.Data.ConnectionState.Closed)
            {
                sqlconnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (sqlconnection.State == System.Data.ConnectionState.Open)
            {
                sqlconnection.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return sqlconnection;
        }

    }
}

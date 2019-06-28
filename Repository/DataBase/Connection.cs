using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DataBase
{
    public class Connection
    {
        public static SqlCommand OpenConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            return command;
        }
    }
}

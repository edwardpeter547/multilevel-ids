using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using Dapper;
using System.Configuration;

namespace TaskWork1
{
    internal class DataAccessLayer
    {

        public static void CreateUser(UserModel user)
        {
            using(var connection = new OleDbConnection(GetConectionString()))
            {
                connection.Execute("insert into users(email, username, password, firstname, lastname) values(@email, @username, @password, @firstname, @lastname)", user);
            }
        }
        public static string GetConectionString(string ConnectionId = "family")
        {
            return ConfigurationManager.ConnectionStrings[ConnectionId].ConnectionString;
        }
    }
}

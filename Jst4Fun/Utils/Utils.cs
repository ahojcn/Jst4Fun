using System;
using MySql.Data.MySqlClient;
using Dapper;
using System.Text.RegularExpressions;

namespace Jst4Fun.Utils
{
    public class Utils
    {
        public Utils()
        {
        }
    }

    public class Fun
    {
        public MySqlConnection GetSqlConn()
        {
            //string connStr = "Server=127.0.0.1;database=bjpowernode;uid=root;password=200212;";
            string connStr = "Server=127.0.0.1;database=jst4fun;uid=root;password=200212;";
            //conn.Execute("insert into t_user(username) values('conn')");
            //var list = conn.Query("select * from t_user");
            //conn.CloseAsync();
            return new MySqlConnection(connStr);
        }

        public bool CheckEmail(string e)
        {
            Regex regex_email = new Regex(@"^[\w-]+@[\w-]+\.(com|net|org|edu|mil|tv|biz|info)$");
            Match m = regex_email.Match(e);

            return m.Success;
        }
    }
}

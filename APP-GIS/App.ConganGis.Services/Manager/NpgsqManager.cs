using Npgsql;
using System.Data;

namespace App.CongAnGis.Services.Manager
{
    public class NpgsqManager
    {
        public static DataTable SelectDataBySql(string sql, string ConnectionString)
        {
            NpgsqlConnection con = new NpgsqlConnection(ConnectionString);
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            var dataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dataReader);
            con.Close();
            return dt; 
        }
    }
}

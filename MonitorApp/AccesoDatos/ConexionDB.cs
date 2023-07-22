using System;
using System.Data;
using System.Data.SqlClient;

namespace MonitorApp.AccesoDatos
{
    public static class ConexionDB
    {
        static readonly string serverName = "LUGOBO-LAPTOP";
        static SqlConnection conn = new SqlConnection($"Data Source={serverName};Initial Catalog=DB_RESTAURANTE;Integrated Security=True");
        //Data Source=LUGOBO-LAPTOP;Initial Catalog=DB_RESTAURANTE;Integrated Security=True

        public static bool Connect()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (SqlException ex)
            {
                conn = null;
                new Exception("It's not possible to connect to database:\n" + ex.Message);
            }

            return false;
        }

        public static void CloseConnection()
        {
            conn?.Close();
        }

        public static SqlConnection GetConnection()
        {
            return conn;
        }

        public static bool CheckConnectionLost()
        {
            return conn == null || (conn != null && conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed);
        }
    }
}
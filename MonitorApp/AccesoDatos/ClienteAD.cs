using Entidades;
using System;
using System.Data.SqlClient;
//using System.Windows.Forms;

namespace MonitorApp.AccesoDatos
{
    public class ClienteAD
    {
        public ClienteAD()
        {
        }

        public Cliente GetClienteById(int id)
        {
            Cliente cliente = new Cliente();
            string query = $"SELECT * FROM CLIENTE WHERE CLIENTEID ={id}";

            //string consultaEstudiantes = "SELECT Estudiante.*, Sede.*  FROM [UEduca].[dbo].[Estudiante] INNER JOIN dbo.Sede ON dbo.Sede.IdSede = dbo.Estudiante.IdSede";

            //if (sede != -1)
            //{
            //    consultaEstudiantes += " WHERE dbo.Estudiante.IdSede = @sede";
            //}

            SqlDataReader reader = null;
            try
            {
                if (ConexionDB.Connect())
                {
                    SqlCommand comand = new SqlCommand(query, ConexionDB.GetConnection());
                    //if (sede != -1)
                    //    comand.Parameters.Add(new SqlParameter("@sede", sede));

                    reader = comand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        //while (reader.Read())
                        //{
                        //    cliente.Add(new Estudiante(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(4)
                        //        , new Sede_universitaria(reader.GetInt32(7), reader.GetString(8)), reader.GetDateTime(5), reader.GetString(6).ToCharArray()[0]));
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:\n Con base de datos por favor revisar conexion.\n" + ex.Message);
            }
            finally
            {
                try
                {
                    if (reader != null && !reader.IsClosed) reader.Close();
                }
                catch (Exception ea)
                {
                }
            }

            return cliente;
        }
    }
}
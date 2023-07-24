using TcpTestLN.Specific;   
using System;
using System.Data.SqlClient;

namespace MonitorApp.AccesoDatos
{
    public class ClienteAD
    {
        public ClienteAD()
        {
        }

        public Cliente GetClienteById(string id)
        {
            Cliente cliente = null;
            string query = $"SELECT IdCliente, Nombre, PrimerApellido, SegundoApellido, FechaNacimiento, Genero, Estado FROM Cliente WHERE IdCliente ={id}";

            SqlDataReader reader = null;
            try
            {
                if (ConexionDB.Connect())
                {
                    SqlCommand comand = new SqlCommand(query, ConexionDB.GetConnection());
                    reader = comand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            cliente = new Cliente
                            {
                                IdCliente = reader.GetString(0),
                                Nombre = reader.GetString(1),
                                PrimerApellido = reader.GetString(2),
                                SegundoApellido = reader.GetString(3),
                                FechaNacimiento = reader.GetDateTime(4),
                                Genero = reader.GetString(5).ToCharArray()[0],
                                Estado = reader.GetBoolean(6)
                            };

                            return cliente;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:\n The database Cannot be accessed.\n" + ex.Message);
            }
            finally
            {
                try
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                        ConexionDB.CloseConnection();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return cliente;
        }
    }
}
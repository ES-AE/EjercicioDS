using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda_Entidades;
using System.Data.SqlClient;

namespace Tienda_AccesoDatos
{
    public class ClienteDal
    {
        public void Insert(ECliente cliente)
        {
            using (var cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["MiprimeravezConnectionString"].ToString()))
            {
                cnx.Open();

                const string sqlConsulta = "INSERT INTO CLIENTE (nombre, direccion, telefono, idTipoCliente) Values (@nombre, @direccion, @telefono, @idTipoCliente)";

                using (SqlCommand cmd = new SqlCommand(sqlConsulta, cnx))
                {
                    cmd.Parameters.AddWithValue("@nombre", cliente.nombre);
                    cmd.Parameters.AddWithValue("@direccion", cliente.direccion);
                    cmd.Parameters.AddWithValue("@telefono", cliente.telefono);
                    cmd.Parameters.AddWithValue("@idTipoCliente", cliente.idTipoCliente);
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();

            }

        }

        public ECliente ObtenerIDCliente(int idCliente)
        {
            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["MiprimeravezConnectionString"].ConnectionString))
            {
                cnx.Open();

                const string sqlGetById = "SELECT * FROM CLIENTE WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnx))
                {
                    //
                    //Utilizamos el valor del parámetro idProducto para enviarlo al parámetro declarado en la consulta
                    //de selección SQL
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    //
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        ECliente cliente = new ECliente
                        {
                            id = Convert.ToInt32(dataReader["Id"]),
                            nombre = Convert.ToString(dataReader["nombre"]),
                            telefono = Convert.ToString(dataReader["telefono"]),
                            direccion = Convert.ToString(dataReader["direccion"]),
                            idTipoCliente = Convert.ToInt32(dataReader["idTipoCliente"])

                        };

                        return cliente;
                    }
                }
            }

            return null;
        }

        public void Actualizar(ECliente cliente)
        {
            using (var cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["MiprimeravezConnectionString"].ToString()))
            {
                cnx.Open();

                const string sqlConsulta = "UPDATE CLIENTE SET nombre = @nombre, direccion = @direccion,telefono = @telefono, idTipoCliente = @idTipoCliente";

                using (SqlCommand cmd = new SqlCommand(sqlConsulta, cnx))
                {
                    cmd.Parameters.AddWithValue("@nombre", cliente.nombre);
                    cmd.Parameters.AddWithValue("@direccion", cliente.direccion);
                    cmd.Parameters.AddWithValue("@telefono", cliente.telefono);
                    cmd.Parameters.AddWithValue("@idTipoCliente", cliente.idTipoCliente);
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();

            }
        }

        public List<ECliente> GetAll()
        {

            List<ECliente> cliente = new List<ECliente>();

            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["MiprimeravezConnectionString"].ToString()))
            {
                cnx.Open();

                const string sqlQuery = "SELECT * FROM CLIENTE c ORDER BY c.Id ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnx))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto ECLIENTE para llenar sus propiedades
                        ECliente c = new ECliente
                        {
                            id = Convert.ToInt32(dataReader["Id"]),
                            nombre = Convert.ToString(dataReader["nombre"]),
                            direccion = Convert.ToString(dataReader["direccion"]),
                            telefono = Convert.ToString(dataReader["telefono"]),
                            idTipoCliente = Convert.ToInt32(dataReader["idTipoCliente"]),

                        };
                        //
                        //Insertamos el objeto Producto dentro de la lista Productos
                        cliente.Add(c);
                    }
                }
            }
            return cliente;
        }
    }
}

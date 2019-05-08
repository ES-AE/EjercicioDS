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
    public class ProductoDal
    {
        public void Insert(EProducto producto)
        {

            //Creamos nuestro objeto de conexion usando nuestro archivo de configuraciones
            using (var cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["MiprimeravezConnectionString"].ToString()))
            {
                cnx.Open();
                //Declaramos nuestra consulta de Acción Sql parametrizada
                const string sqlQuery =
                    "INSERT INTO Vehículo (Descripcion, Modelo, Marca) VALUES (@descripcion, @marca, @modelo)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnx))
                {
                    //El primero de los cambios significativos con respecto al ejemplo descargado es que aqui...
                    //ya no leeremos controles sino usaremos las propiedades del Objeto EProducto de nuestra capa
                    //de entidades...
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@marca", producto.Marca);
                    cmd.Parameters.AddWithValue("@modelo", producto.Modelo);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<EProducto> GetAll()
        {

            List<EProducto> productos = new List<EProducto>();

            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["MiprimeravezConnectionString"].ToString()))
            {
                cnx.Open();

                const string sqlQuery = "SELECT * FROM Vehículo ORDER BY Id ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnx))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto Eproducto para llenar sus propiedades
                        EProducto producto = new EProducto
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Descripcion = Convert.ToString(dataReader["Descripcion"]),
                            Marca = Convert.ToString(dataReader["Modelo"]),
                            Modelo = Convert.ToInt32(dataReader["Marca"])
                        };
                        //
                        //Insertamos el objeto Producto dentro de la lista Productos
                        productos.Add(producto);
                    }
                }
            }
            return productos;
        }


        public EProducto GetByid(int idProducto)
        {
            //var cadena = ConfigurationManager.ConnectionStrings["miPrimeraVezConnectionString"].ConnectionString;

            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["MiprimeravezConnectionString"].ConnectionString))
            {
                cnx.Open();

                const string sqlGetById = "SELECT * FROM Vehículo WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnx))
                {
                    //
                    //Utilizamos el valor del parámetro idProducto para enviarlo al parámetro declarado en la consulta
                    //de selección SQL
                    cmd.Parameters.AddWithValue("@id", idProducto);
                    //
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        EProducto producto = new EProducto
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Descripcion = Convert.ToString(dataReader["Descripcion"]),
                            Marca = Convert.ToString(dataReader["Marca"]),
                            Modelo = Convert.ToInt32(dataReader["Modelo"])
                        };

                        return producto;
                    }
                }
            }

            return null;
        }


        public void Update(EProducto producto)
        {
            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["MiprimeravezConnectionString"].ToString()))
            {
                cnx.Open();
                const string sqlQuery =
                    "UPDATE Vehículo SET Descripcion = @descripcion, Marca = @marca, Modelo = @modelo WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnx))
                {
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@marca", producto.Marca);
                    cmd.Parameters.AddWithValue("@modelo", producto.Modelo);
                    cmd.Parameters.AddWithValue("@id", producto.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void Delete(int idproducto)
        {
            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["MiprimeravezConnectionString"].ToString()))
            {
                cnx.Open();
                const string sqlQuery = "DELETE FROM Vehículo WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnx))
                {
                    cmd.Parameters.AddWithValue("@id", idproducto);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;
using System.Data.SqlClient;
using System.Data;


namespace App_Conection_BBDD_ArchivoExterno
{
    internal class Program
    {


        static void Main(string[] args)
        {

            try
            {
                // Obtén la cadena de conexión del archivo app.config
                string connectionString = ConfigurationManager.ConnectionStrings["conexion-bbdd"].ConnectionString;
               //Nombre de la tabla en la que quiero hacer la consulta.
                string tableName = "gbp_alm_cat_libros";
                // Nos connectamos con la base de datos
                using (var connection = new Npgsql.NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    //Query que quiero que se ejecute
                    string sqlQuery = $"SELECT * FROM {tableName}";
                    // Realizamos el select aqui.

                    //Aquí se crea un objeto NpgsqlCommand llamado command que representa la consulta SQL que deseas ejecuta
                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        //Se crea un objeto NpgsqlDataAdapter llamado adapter que se utiliza para ejecutar la consulta y llenar un objeto DataTable con los resultados
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                        {
                            //Se crea un nuevo objeto DataTable llamado dataTable que se utilizará para almacenar los resultados de la consulta
                            DataTable dataTable = new DataTable();
                            //Los datos se almacenan en el DataTable para su posterior procesamiento.
                            adapter.Fill(dataTable);

                            // Mostrar los datos de la tabla
                            foreach (DataRow row in dataTable.Rows)
                            {
                                Console.WriteLine($"ID: {row["idLibro"]}, Titulo: {row["titulo"]}, Autor: {row["autor"]} , Isbn: {row["isbn"]}");
                            }
                        }
                    }

                        connection.Close();
                }

                Console.WriteLine("Conexión exitosa a PostgreSQL.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar a PostgreSQL: " + ex.Message);
            }
            Console.ReadLine();
        }
    }
}


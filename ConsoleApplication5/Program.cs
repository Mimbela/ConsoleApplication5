using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace sqlAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            string cadenaConexion = @"Data Source=DESKTOP-7KN5JV1\SQLEXPRESS;Initial Catalog=NORTHWND;Integrated Security=True";
            string query = "update Customers set CompanyName=@CompanyNueva  where CustomerID= @clienteID ";

            Console.WriteLine("Introduce compañía nueva");
            string compañiaNueva = Console.ReadLine();

            Console.WriteLine("Introduce ID del cliente que está en la base de datos");
            string clienteID = Console.ReadLine();

            try
            {
                using (SqlConnection objConexion = new SqlConnection(cadenaConexion))
                {
                    objConexion.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    //************************************* UPDATE /ACTUALIZAR *********************************************************************************//
                    SqlCommand commandUpdate = new SqlCommand(query, objConexion);

                    commandUpdate.Parameters.Add("@CompanyNueva", SqlDbType.NVarChar, 40);
                    commandUpdate.Parameters["@CompanyNueva"].Value = compañiaNueva;

                    commandUpdate.Parameters.Add("@ClienteID", SqlDbType.NChar, 5);
                    commandUpdate.Parameters["@ClienteID"].Value = clienteID;

                    adapter.UpdateCommand = commandUpdate;
                    int filasActualizadas = adapter.UpdateCommand.ExecuteNonQuery();
                    Console.WriteLine("{0} filas actualizadas", filasActualizadas);

                    //***************************************  INSERT /AGREGAR  *******************************************************************//
                    SqlCommand commandInsert = new SqlCommand("INSERT INTO Customers (CustomerID , CompanyName)" +
                        "VALUES (@CustomerID , @CompanyName)", objConexion);

                    commandInsert.Parameters.Add("@CustomerID", SqlDbType.NChar, 5);
                    commandInsert.Parameters["@CustomerID"].Value = "PIPAS";

                    commandInsert.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 40);
                    commandInsert.Parameters["@CompanyName"].Value = "PIPAS FACUNDO";

                    adapter.InsertCommand = commandInsert;
                    int filasInsertadas = adapter.InsertCommand.ExecuteNonQuery();

                    //**************************************** DELETE /BORRAR *****************************************************************//
                    SqlCommand commandDelete = new SqlCommand("DELETE FROM CUSTOMERS" +
                        "WHERE CustomerID = @CustomerId ", objConexion);

                    SqlParameter parameter = commandDelete.Parameters.Add("@CustomerId", SqlDbType.NVarChar, 5);
                    parameter.Value = "PIPAS";
                    adapter.DeleteCommand = commandDelete;

                    int filasBorradas = adapter.DeleteCommand.ExecuteNonQuery();

                    //************************************ EJECUCIÓN ************************************************/



                    Console.ReadKey();
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;


namespace APIWEB
{
    public class conex
    {
        /****************FUNCION DEVEULVE STRING DE CONEXION************************************************************************/

        public static SqlConnection cnx()
        {
           
            SqlConnection cn = new SqlConnection (@"Data Source=localhost;Integrated Security=SSPI;Initial Catalog=stock");

            return cn;
        }

        /**********VARIABLE QUE ATRAPA LA CONEXION***********************************************************************************/

        public SqlConnection con = cnx();



        /***********METODO PARA EJECUTAR PROCEDIMETOS CON VERVOS POST, PUT, DELETE ***************************************************/
        public void EjecutarSP(string nombreSP, List<SqlParameter> parametros = null)
        {
            try
            {
                con.Open();
                var comand = con.CreateCommand();
                comand.CommandText = nombreSP;
                comand.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null) comand.Parameters.AddRange(parametros.ToArray());
                comand.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                throw;
            }
        }

        /***********METODO PARA EJECUTAR PROCEDIMEINTOS DE VERVO GET**********************************************************************/

        public DataSet ConsultarSP(string nombreSP, List<SqlParameter> parametros = null)
        {
            DataSet dsDatos = new DataSet();
            try
            {
                con.Open();
                var comand = con.CreateCommand();
                comand.CommandText = nombreSP;
                comand.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null) comand.Parameters.AddRange(parametros.ToArray());
                var adapter = new SqlDataAdapter(comand);
                adapter.Fill(dsDatos);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dsDatos;
        }
    }
}
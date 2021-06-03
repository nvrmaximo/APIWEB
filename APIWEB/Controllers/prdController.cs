using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Newtonsoft.Json;

namespace APIWEB.Controllers
{
    public class prdController : ApiController
    {

        /*****INSTACIA DE CONEXION ENCAPSULADA PARA EJECUCION DE LOS METODOS ************************************************************/
        conex conex = new conex();
        
        /*****METODO PARA CONSULTAR TODOS LOS PRODUCTOS *********************************************************************************/
        [HttpGet]
        [Route("api/produc")]
        public IHttpActionResult GetAll()
        {
            var ds = conex.ConsultarSP("sp_viewAll");
            var datos = ds.Tables[0].AsEnumerable().Select(dataRow => new prdmodel
            {
                prd_id = dataRow["prd_id"].ToString()
                 ,
                prd_name = dataRow["prd_name"].ToString()
                ,
                prd_category = dataRow["prd_category"].ToString()
                ,
                prd_price = dataRow["prd_price"].ToString()

              }).ToList();

           
             

            return Json(datos);
        }


        /*****METODO PARA CONSULTAR TODOS LOS PRODUCTOS POR ID****************************************************************************/
        [HttpGet]
        [Route("api/produc/{id}")]
        public IHttpActionResult GetID(string id)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("prd_id", id));
            var ds = conex.ConsultarSP("sp_viewId", param);
            var datos = ds.Tables[0].AsEnumerable().Select(dataRow => new prdmodel
            {
                prd_id = dataRow["prd_id"].ToString()
                 ,
                prd_name = dataRow["prd_name"].ToString()
                ,
                prd_category = dataRow["prd_category"].ToString()
                ,
                prd_price = dataRow["prd_price"].ToString()

            }).ToList();

            return Json(datos.Any() ? datos[0] : null);
        }


        /*****METODO PARA INSERTAR LOS PRODUCTOS ****************************************************************************************/
        [HttpPost]
        [Route("api/produc")]
        public IHttpActionResult Ins(prdmodel prd)
        {
           
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("prd_name", prd.prd_name ));
            param.Add(new SqlParameter("prd_category", prd.prd_category ));
            param.Add(new SqlParameter("prd_price", prd.prd_price ));
   
            conex.EjecutarSP("sp_insprd", param);
            return Ok(GetAll());
        }


        /*****METODO PARA ACTUALIZAR LOS PRODUCTOS LOS PRODUCTOS **************************************************************************/
        [HttpPut]
        [Route("api/produc")]
        public IHttpActionResult Upd([FromBody] prdmodel prd)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("prd_id", prd.prd_id ));
            param.Add(new SqlParameter("prd_name", prd.prd_name ));
            param.Add(new SqlParameter("prd_category", prd.prd_category ));
            param.Add(new SqlParameter("prd_price", prd.prd_price ));

            conex.EjecutarSP("sp_upprd", param);
            return Ok(GetAll());
        }


        /*****METODO PARA ELIMINAR LOS PRODUCTOS LOS PRODUCTOS **************************************************************************/
        [HttpDelete]
        [Route("api/produc")]
        public IHttpActionResult Del([FromBody] prdmodel prd)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("prd_id", prd.prd_id));
            conex.EjecutarSP("sp_delprd", param);
            return Ok(GetAll());
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Linq;

namespace UniStaffWebAPIs.Controllers
{
    public class GenderController : ApiController
    {
        SqlConnection con = new SqlConnection("server=LT2208PPKAUSHAL\\SQLEXPRESS; database=Employees; Integrated Security=SSPI; User Id=test; Password =test;");
        // GET api/values
        public IHttpActionResult Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Gender", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return Ok(JsonConvert.SerializeObject(dt));
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Gender WHERE id = ' " + id + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return Ok(JsonConvert.SerializeObject(dt));
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        // POST api/values
        public IHttpActionResult Post([FromBody] string value)
        {
            SqlCommand cmd = new SqlCommand("Insert into Gender(Gender_Type) VALUES('" + value + "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                return StatusCode(HttpStatusCode.OK);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/values/5
        public IHttpActionResult Put(int id, [FromBody] string value)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Gender SET Gender_Type = '" + value + " ' WHERE ID = '" + id + "'", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                return StatusCode(HttpStatusCode.OK);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Gender WHERE ID = '" + id + "'", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                return StatusCode(HttpStatusCode.OK);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
    }
}

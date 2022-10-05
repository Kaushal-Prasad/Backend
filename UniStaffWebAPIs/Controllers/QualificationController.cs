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
    public class QualificationController : ApiController
    {
        SqlConnection con = new SqlConnection("server=LT2208PPKAUSHAL\\SQLEXPRESS; database=Employees; Integrated Security=SSPI; User Id=test; Password =test;");
        public IHttpActionResult Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Qualification", con);
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
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Qualification WHERE id = ' " + id + "'", con);
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
        public IHttpActionResult Post([FromBody] JObject data)
        {
            SqlCommand cmd = new SqlCommand("Insert into Qualification(Level_Quali,Level_Desc) VALUES('" + data["Level_Quali"] + "','" + data["Level_Desc"] + "')", con);
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
        public IHttpActionResult Put(int id, [FromBody] JObject data)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Qualification SET Level_Quali='" + data["Level_Quali"] + "', Level_Desc='" + data["Level_Desc"] + "' WHERE ID = '" + id + "'", con);
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
            SqlCommand cmd = new SqlCommand("DELETE FROM Qualification WHERE ID = '" + id + "'", con);
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

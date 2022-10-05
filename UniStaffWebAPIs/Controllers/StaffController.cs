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
    public class StaffController : ApiController
    {
        SqlConnection con = new SqlConnection("server=LT2208PPKAUSHAL\\SQLEXPRESS; database=Employees; Integrated Security=SSPI; User Id=test; Password =test;");
        // GET api/values
        // GET api/values
        public IHttpActionResult Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT S.ID,S.Employee_Number,S.First_Name,S.Last_Name,CONVERT(NVARCHAR,S.Date_of_Birth,107)AS Date_of_Birth,S.Salary,S.Experience,S.Gender_ID " +
                " ,S.Qualification_ID,G.Gender_Type,Q.Level_Quali FROM Staff S "
                + " INNER JOIN Gender G ON S.Gender_ID = G.ID INNER JOIN Qualification Q ON Q.ID = S.Qualification_ID", con);
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
        
        [HttpPost]
        [Route("api/Staff/GetById")]
        public IHttpActionResult Post([FromBody] JObject data)
        {
            //int employee_number = id;
            SqlDataAdapter da = new SqlDataAdapter("SELECT S.ID,S.Employee_Number,S.First_Name,S.Last_Name,CONVERT(NVARCHAR,S.Date_of_Birth,107)AS Date_of_Birth,S.Salary,S.Experience,S.Gender_ID " +
                " ,S.Qualification_ID,G.Gender_Type,Q.Level_Quali FROM Staff S "
                + " INNER JOIN Gender G ON S.Gender_ID = G.ID INNER JOIN Qualification Q ON Q.ID = S.Qualification_ID" +
                " WHERE Employee_Number = '" + (string)data["Employee_Number"] + "' ", con);
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

        public static double calculateStaffSalary(double level_q, double experiance)
        {
            return Math.Round((level_q / 10.00) * (experiance / 5.00) * 100000, 2, MidpointRounding.AwayFromZero);
        }
        //POST api/values/5
        public IHttpActionResult POST([FromBody] JObject data)
        {


            SqlCommand command = new SqlCommand("SELECT Level_Quali FROM Qualification " +
                                                "WHERE id = @id", con);



            // Add the parameters for the SelectCommand.

            command.Parameters.Add("@id", SqlDbType.Int).Value = (string)data["Qualification_ID"];
            con.Open();
            object Level_Quali = command.ExecuteScalar();

           
            double calculatedSalary = calculateStaffSalary((int)Level_Quali, (double)data["Experience"]);// ((int)Level_Quali / 10.00) * ((double)data["Experience"] / 5.00) * 100000;

           


            // Create the InsertCommand.
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Staff (Employee_Number, First_Name,Last_Name,Date_of_Birth,Salary,Experience,Gender_ID,Qualification_ID) " +
                "VALUES (@Employee_Number, @First_Name, @Last_Name,@Date_of_Birth,@Salary,@Experience,@Gender_ID,@Qualification_ID)", con);

            // Add the parameters for the InsertCommand.
            cmd.Parameters.Add("@Employee_Number", SqlDbType.VarChar, 500).Value = (string)data["Employee_Number"];
            cmd.Parameters.Add("@First_Name", SqlDbType.VarChar, 50).Value = (string)data["First_Name"];
            cmd.Parameters.Add("@Last_Name", SqlDbType.VarChar, 50).Value = (string)data["Last_Name"];
            cmd.Parameters.Add("@Date_of_Birth", SqlDbType.Date).Value = (string)data["Date_of_Birth"];
            cmd.Parameters.Add("@Salary", SqlDbType.Decimal).Value = calculatedSalary;
            cmd.Parameters.Add("@Experience", SqlDbType.Int, 50).Value = (int)data["Experience"];
            cmd.Parameters.Add("@Gender_ID", SqlDbType.Int, 50).Value = (int)data["Gender_ID"];
            cmd.Parameters.Add("@Qualification_ID", SqlDbType.Int, 50).Value = (int)data["Qualification_ID"];

            //con.Open();
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
        //PUT api/values/5
        public IHttpActionResult PUT([FromBody] JObject data)
        {
            SqlCommand command = new SqlCommand("SELECT Level_Quali FROM Qualification " +
                                                "WHERE id = @id", con);
            // Add the parameters for the SelectCommand.

            command.Parameters.Add("@id", SqlDbType.Int).Value = (string)data["Qualification_ID"];
            con.Open();
            object Level_Quali = command.ExecuteScalar();

            double calculatedSalary = calculateStaffSalary((int)Level_Quali, (double)data["Experience"]); //((int)Level_Quali / 10.00) * ((double)data["Experience"] / 5.00) * 100000;

            // Create the UpdateCommand.
            SqlCommand cmd = new SqlCommand(
                "UPDATE Staff SET First_Name = '" + (string)data["First_Name"] + "', Last_Name = '" + (string)data["Last_Name"] + "' " +
                ",Date_of_Birth = '" + (string)data["Date_of_Birth"] + "',Salary = '" + calculatedSalary + "',Experience = '" + (int)data["Experience"] + "'" +
                ",Gender_ID = '" + (int)data["Gender_ID"] + "',Qualification_ID = '" + (int)data["Qualification_ID"] + "' "
                + "WHERE Employee_Number = '" + (string)data["Employee_Number"] + "'", con);

            
            con.Close();
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
            SqlCommand cmd = new SqlCommand("DELETE FROM Staff WHERE ID = '" + id + "'", con);
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

        [Route("api/staff/login")]
        [HttpPost]
        public string login(Login.Registration registration)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Staff WHERE Employee_Number = '" + registration.Employee_Number + "' ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return "Valid User";
            }
            else
            {
                return "Invalid User";
            }

        }

        [HttpPost]
        [Route("api/Staff/UpdateStaff")]
        public IHttpActionResult UpdateStaff([FromBody] JObject data)
        {
            //int employee_number = id;
            SqlCommand cmd = new SqlCommand(
                "UPDATE Staff SET First_Name = '" + (string)data["First_Name"] + "', Last_Name = '" + (string)data["Last_Name"] + "', Date_of_Birth = '" + (string)data["Date_of_Birth"] + "' WHERE Employee_Number = '" + (string)data["Employee_Number"] + "'", con);
                      
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
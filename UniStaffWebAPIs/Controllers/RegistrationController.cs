using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FluentAssertions.Common;
using Microsoft.Extensions.Configuration;
using UniStaffWebAPIs.Login;

namespace UniStaffWebAPIs.Controllers
{
    public class RegistrationController : ApiController
    {
        private readonly IConfiguration _configuration;

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("registration")]

        public string registration(Login.Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("RegCon").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Registration(UserName,Password,Email,IsActive) VALUES('" + registration.UserName + "','" + registration.Password + "','" + registration.Email + "','" + registration.IsActive + "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Data Inserted";
            }
            else
            {
                return "Error";
            }
        }

        [HttpPost]
        [Route("login")]
        public string login(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("RegCon").ToString());
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
    }
}

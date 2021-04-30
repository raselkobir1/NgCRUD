using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/appauth")]
    public class AppAuthController : ApiController
    {  
        [Route("login")]
        [HttpGet]
        public HttpResponseMessage Login(string email, string password)
        {
            #region HARD CODE TEST
            //string userIdDb = "e@e.com";
            //string passwordDb = "1234";
            //if (userIdDb == email && passwordDb == password)
            //{
            //    return Request.CreateResponse(HttpStatusCode.OK,"Ok");
            //}
            //else
            //{
            //    return Request.CreateResponse(HttpStatusCode.NoContent,"Fail");
            //}
            #endregion

            string query = @"SELECT UserId,Password FROM Users WHERE UserId ='" + email + "' and Password ='"+ password+"'";
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var dataAdp = new SqlDataAdapter(cmd))

            {
                cmd.CommandType = CommandType.Text;
                dataAdp.Fill(dt);
            }
            if(dt.Rows.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "Fail");
            }
           
        }
    }
}

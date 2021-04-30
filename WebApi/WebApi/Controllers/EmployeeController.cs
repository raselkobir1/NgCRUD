using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    //[RoutePrefix("api")]
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {

            string query = @"SELECT EmployeeId,EmployeeName,Department,DateOfJoining,PhotoFileName,'http://localhost:56259/Content/Photos/'+PhotoFileName as PhotoFilePath from Employee";
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var dataAdp = new SqlDataAdapter(cmd))

            {
                cmd.CommandType = CommandType.Text;
                dataAdp.Fill(dt);
            }
            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }

        public string Post(Employee  employee)
        {
            try
            {
                string query = @"INSERT INTO Employee (EmployeeName,Department,DateOfJoining,PhotoFileName)VALUES
                                ('"+employee.EmployeeName+"'," +
                                "'"+employee.Department+"'," +
                                "'"+employee.DateOfJoining+"'," +
                                "'"+employee.PhotoFileName+"')";
                sqlConnectionString(query);
                return "Employee Insert Succesfully !!";
            }
            catch (Exception ex)
            {
                return "Faild to Add Employee";
                throw ex;
            }

        }

        public string Put(Employee employee)
        {
            try
            {
                string query = @"UPDATE Employee SET EmployeeName = '"+employee.EmployeeName+"'," +
                                                     "Department='"+employee.Department+"'," +
                                                     "DateOfJoining='"+employee.DateOfJoining+"'," +
                                                     "PhotoFileName='"+employee.PhotoFileName+"'" +
                                                     "WHERE EmployeeId ="+employee.EmployeeId+";";
                sqlConnectionString(query);
                return "Employee Update Succesfully !!";
            }
            catch (Exception ex)
            {
                return "Faild to Update Department";
                throw ex;
            }

        }

        [HttpDelete]
        //[Route("Delete")]
        public string Delete(int Id)
        {
            try
            {
               // int Id = 1;
                string query = @"DELETE FROM Employee WHERE EmployeeId=" + Id + "";
                sqlConnectionString(query);
                return "Employee Delete Succesfully !!";
            }
            catch (Exception ex)
            {
                return "Faild to Delete Department";
                throw ex;
            }

        }

        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = @"SELECT DepartmentName from Department";
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var dataAdp = new SqlDataAdapter(cmd))

            {
                cmd.CommandType = CommandType.Text;
                dataAdp.Fill(dt);
            }
            return Request.CreateResponse(HttpStatusCode.OK, dt);

        }

        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        { 
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Content/Photos/" + fileName);
                postedFile.SaveAs(physicalPath);
                return fileName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void sqlConnectionString(string query)
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var dataAdp = new SqlDataAdapter(cmd))

            {
                cmd.CommandType = CommandType.Text;
                dataAdp.Fill(dt);
            }
        }
    }
}

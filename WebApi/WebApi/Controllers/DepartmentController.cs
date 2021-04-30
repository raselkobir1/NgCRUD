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
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {

            string query = @"select DepartmentId,DepartmentName from Department";
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var dataAdp = new SqlDataAdapter(cmd))

            {
                cmd.CommandType = CommandType.Text;
                dataAdp.Fill(dt);
            }
            return Request.CreateResponse(HttpStatusCode.OK,dt);
        }

        public string Post(Department department)
        {
            try
            {
                string query = @"INSERT INTO Department (DepartmentName) VALUES('" + department.DepartmentName + "');";
                sqlConnectionString(query);
                return "Department Insert Succesfully !!";
            }
            catch (Exception ex)
            {
                return "Faild to Add Department";
                throw ex;
            }
          
        }

        public string Put(Department department) 
        {
            try
            {
                string query = @"UPDATE Department SET DepartmentName = '"+department.DepartmentName+"' WHERE DepartmentId ='"+department.DepartmentId+"';";
                sqlConnectionString(query);
                return "Department Update Succesfully !!";
            }
            catch (Exception ex)
            {
                return "Faild to Update Department";
                throw ex;
            }

        }
        [HttpDelete]
        public string Delete(int Id) 
        {
            try
            {
                string query = @"DELETE FROM Department WHERE DepartmentId="+Id+"";
                sqlConnectionString(query);
                return "Department Delete Succesfully !!";
            }
            catch (Exception ex)
            {
                return "Faild to Delete Department";
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

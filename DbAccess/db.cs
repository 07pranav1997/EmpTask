using EmpTask.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmpTask.DbAccess
{
    public class db
    {
        string ConnectionStringsKey;
        string strConnString;

        public string ConnectionStr()
        {
            ConnectionStringsKey = "ConnectionString";
            strConnString = System.Configuration.ConfigurationManager.AppSettings[ConnectionStringsKey];
            return strConnString;
        }

        //Get Records
        public DataSet get_record()
        {
            /*string query = "SELECT id, FirstName, LastName, ReportingManager, DateOfJOining, Department FROM EmpDetails";*/
            string query = "SELECT * FROM EmpDetails";
            SqlConnection cnDB = default(SqlConnection);
            DataSet dtTemp = null;
            SqlDataAdapter dsAdp = default(SqlDataAdapter);

            cnDB = new SqlConnection(ConnectionStr());
            cnDB.Open();

            dsAdp = new SqlDataAdapter(query, cnDB);
            dtTemp = new DataSet();
            dsAdp.Fill(dtTemp);
            cnDB.Close();

            return dtTemp;
        }
        
        //Get Records by ID
        public DataSet get_recordbyID(int id)
        {
            using (SqlConnection cnDB = new SqlConnection(ConnectionStr()))
            {
                cnDB.Open();
                SqlCommand sqlCmd = new SqlCommand("sp_getbyID", cnDB);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter dsAdp = default(SqlDataAdapter);
                DataSet dtTemp = new DataSet();
                dsAdp.Fill(dtTemp);
                cnDB.Close();

                return dtTemp;
            }
        }

        //Add Records
        public void Add_record(EmployeeModel employeeModel)
        {
            using (SqlConnection cnDB = new SqlConnection(ConnectionStr()))
            {
                cnDB.Open();
                string query = "INSERT INTO EmpDetails VALUES (@FirstName, @LastName, @DOB, @Gender, @Qualification, @Designation, @DateOfJoining, @ReportingManager, @Department)";
                SqlCommand sqlCmd = new SqlCommand(query, cnDB);
                sqlCmd.Parameters.AddWithValue("@FirstName", employeeModel.FirstName);
                sqlCmd.Parameters.AddWithValue("@LastName", employeeModel.LastName);
                sqlCmd.Parameters.AddWithValue("@DOB", employeeModel.DOB);
                sqlCmd.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                sqlCmd.Parameters.AddWithValue("@Qualification", employeeModel.Qualification);
                sqlCmd.Parameters.AddWithValue("@Designation", employeeModel.Designation);
                sqlCmd.Parameters.AddWithValue("@DateOfJoining", employeeModel.DateOfJoining);
                sqlCmd.Parameters.AddWithValue("@ReportingManager", employeeModel.ReportingManager);
                sqlCmd.Parameters.AddWithValue("@Department", employeeModel.Department);
                sqlCmd.ExecuteNonQuery();
            }
        }

        // Update Record
        public void update_record(EmployeeModel employeeModel)
        {
            using (SqlConnection cnDB = new SqlConnection(ConnectionStr()))
            {
                cnDB.Open();
                SqlCommand sqlCmd = new SqlCommand("sp_emp_Update", cnDB);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@id", employeeModel.id);
                sqlCmd.Parameters.AddWithValue("@FirstName", employeeModel.FirstName);
                sqlCmd.Parameters.AddWithValue("@LastName", employeeModel.LastName);
                sqlCmd.Parameters.AddWithValue("@DOB", employeeModel.DOB);
                sqlCmd.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                sqlCmd.Parameters.AddWithValue("@Qualification", employeeModel.Qualification);
                sqlCmd.Parameters.AddWithValue("@Designation", employeeModel.Designation);
                sqlCmd.Parameters.AddWithValue("@DateOfJoining", employeeModel.DateOfJoining);
                sqlCmd.Parameters.AddWithValue("@ReportingManager", employeeModel.ReportingManager);
                sqlCmd.Parameters.AddWithValue("@Department", employeeModel.Department);
                sqlCmd.ExecuteNonQuery();
            }
        }

        //delete data
        public void deleteData(int id)
        {
            using (SqlConnection cnDB = new SqlConnection(ConnectionStr()))
            {
                cnDB.Open();
                SqlCommand sqlCmd = new SqlCommand("sp_emp_delete", cnDB);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@id", id);
                sqlCmd.ExecuteNonQuery();
            }
        }

    }
}
using EmpTask.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpTask.Controllers
{
    public class EmployeeController : Controller
    {
        string ConnectionStringsKey;
        string strConnString;

        public string ConnectionStr()
        {
            ConnectionStringsKey = "ConnectionString";
            strConnString = ConfigurationManager.AppSettings[ConnectionStringsKey];
            return strConnString;
        }



        // GET: Employee
        public ActionResult Index()
        {
            string query = "SELECT id, CONCAT(FirstName, ' ', LastName) AS EmpName, ReportingManager, DateOfJOining, Department FROM EmpDetails";
            SqlConnection cnDB = default(SqlConnection);
            DataTable dtTemp = null;
            SqlDataAdapter dsAdp = default(SqlDataAdapter);

            cnDB = new SqlConnection(ConnectionStr());
            cnDB.Open();

            dsAdp = new SqlDataAdapter(query, cnDB);
            dtTemp = new DataTable();
            dsAdp.Fill(dtTemp);
            cnDB.Close();

            return View(dtTemp);
        }

        // GET: Employee/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new EmployeeModel());
        }

        // POST: Employee/Create
        /*[HttpPost]
        public ActionResult Create(EmployeeModel employeeModel)
        {
            try
            {
                // TODO: Add insert logic here
                SqlConnection cnDB = default(SqlConnection);
                cnDB = new SqlConnection(ConnectionStr());
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

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        */
        [HttpPost]
        public ActionResult Create(EmployeeModel employeeModel)
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
            return RedirectToAction("Index");
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? pid)
        {
            /*if (pid == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);*/
            /*else
            {*/
                EmployeeModel employeeModel = new EmployeeModel();
                DataTable dataTable = new DataTable();
                using (SqlConnection cnDB = new SqlConnection(ConnectionStr()))
                {
                    cnDB.Open();
                    string query = "SELECT * FROM EmpDetails where @id = id";
                    SqlDataAdapter dsAdp = new SqlDataAdapter(query, cnDB);
                    dsAdp.SelectCommand.Parameters.AddWithValue("@id", pid);
                    dsAdp.Fill(dataTable);
                }

                if (dataTable.Rows.Count == 1)
                {
                    employeeModel.FirstName = dataTable.Rows[0][1].ToString();
                    employeeModel.LastName = dataTable.Rows[0][2].ToString();
                    employeeModel.DOB = Convert.ToDateTime(dataTable.Rows[0][3].ToString());
                    employeeModel.Gender = dataTable.Rows[0][4].ToString();
                    employeeModel.Qualification = dataTable.Rows[0][5].ToString();
                    employeeModel.Designation = dataTable.Rows[0][6].ToString();
                    employeeModel.DateOfJoining = Convert.ToDateTime(dataTable.Rows[0][7].ToString());
                    employeeModel.ReportingManager = dataTable.Rows[0][8].ToString();
                    employeeModel.Department = dataTable.Rows[0][9].ToString();
                    return View(employeeModel);
                }

                return RedirectToAction("Index");
            /*}*/
            
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

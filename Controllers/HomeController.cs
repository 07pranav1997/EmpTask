using EmpTask.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpTask.Controllers
{
    public class HomeController : Controller
    {
        DbAccess.db DB = new DbAccess.db();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Display all
        public JsonResult GetData()
        {
            DataSet ds = DB.get_record();
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                employeeModels.Add(new EmployeeModel
                {
                    id = Convert.ToInt32(dr["id"]),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    DOB = Convert.ToDateTime(dr["DOB"]),
                    Gender = dr["Gender"].ToString(),
                    Qualification = dr["Qualification"].ToString(),
                    Designation = dr["Designation"].ToString(),
                    DateOfJoining = Convert.ToDateTime(dr["DateOfJoining"]),
                    ReportingManager = dr["ReportingManager"].ToString(),
                    Department = dr["Department"].ToString()
                });
            }
            return Json(employeeModels, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowAll()
        {
            return View();
        }



        // Get All Qualifications
        public JsonResult GetQualification()
        {
            DataSet ds = DB.get_record();
            List<QualificationModel> qualificationModels = new List<QualificationModel>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                qualificationModels.Add(new QualificationModel
                {
                    qualification = dr["qualification"].ToString(),
                });
            }
            return Json(qualificationModels, JsonRequestBehavior.AllowGet);
        }

        

        //Add record
        public JsonResult AddRecord(EmployeeModel employeeModel)
        {
            string result = string.Empty;
            try
            {
                DB.Add_record(employeeModel);
                result = "Inserted";
            }
            catch (Exception)
            {
                result = "Failed";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        //Update record
        public JsonResult UpdateRecord(EmployeeModel employeeModel)
        {
            string result = string.Empty;
            try
            {
                DB.update_record(employeeModel);
                result = "Updated";
            }
            catch (Exception)
            {
                result = "Failed";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult update_Record(int id)
        {
            return View();
        }

        public JsonResult GetDataById(int id)
        {
            DataSet ds = DB.get_recordbyID(id);
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                employeeModels.Add(new EmployeeModel
                {
                    id = Convert.ToInt32(dr["id"]),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    DOB = Convert.ToDateTime(dr["DOB"]),
                    Gender = dr["Gender"].ToString(),
                    Qualification = dr["Qualification"].ToString(),
                    Designation = dr["Designation"].ToString(),
                    DateOfJoining = Convert.ToDateTime(dr["DateOfJoining"]),
                    ReportingManager = dr["ReportingManager"].ToString(),
                    Department = dr["Department"].ToString()
                });
            }
            return Json(employeeModels, JsonRequestBehavior.AllowGet);
        }

        //Delete Data 
        public JsonResult DeleteRecord(int id)
        {
            string result = string.Empty;
            try
            {
                DB.deleteData(id);
                result = "Deleted";
            }
            catch (Exception)
            {
                result = "Failed";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
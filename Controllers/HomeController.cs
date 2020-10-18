using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using SchoolCRUD.Models;
using Microsoft.Ajax.Utilities;

namespace SchoolCRUD.Controllers
{
    public class HomeController : Controller
    {
        string ConnectionString = @"Data Source=DESKTOP-L7RGLO3\SQLEXPRESS;Initial Catalog=School;Integrated Security=True";
        public ActionResult Index()
        {
            FillGenderDropdown();
            return View(new AdmissionModel());
        }

        [HttpPost]
        public ActionResult Index(AdmissionModel admissionModel) {
            try {
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                String query = "Insert Into Admission values ( @FirstName,@LastName, @DateOfBirth, @PlaceOfBirth, @Gender, @Religion, @Nationality, @PhoneNumber," +
                    "@MobileNumber,@Email, @Country, @StateID, @CityID, @StreetAddress)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", admissionModel.FirstName);
                cmd.Parameters.AddWithValue("@LastName", admissionModel.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", admissionModel.DateOfBirth);
                cmd.Parameters.AddWithValue("@PlaceOfBirth", admissionModel.PlaceOfBirth);
                cmd.Parameters.AddWithValue("@Gender", admissionModel.Gender);
                cmd.Parameters.AddWithValue("@Religion", admissionModel.Religion);
                cmd.Parameters.AddWithValue("@Nationality", admissionModel.Nationality);
                cmd.Parameters.AddWithValue("@PhoneNumber", admissionModel.PhoneNumber);
                cmd.Parameters.AddWithValue("@MobileNumber", admissionModel.MobileNumber);
                cmd.Parameters.AddWithValue("@Email", admissionModel.Email);
                cmd.Parameters.AddWithValue("@Country", admissionModel.Country);
                cmd.Parameters.AddWithValue("@StateID", admissionModel.State);
                cmd.Parameters.AddWithValue("@CityID", admissionModel.City);
                cmd.Parameters.AddWithValue("@StreetAddress", admissionModel.StreetAddress);
                cmd.ExecuteNonQuery();
                return RedirectToAction("index");
            }
            catch (Exception ex) { return View(); }
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
        public ActionResult AdmissionRecord() {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            String query = "Exec GetAllRecordAdmission";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            sda.Fill(dt);
            return View(dt);
        }

        public ActionResult Edit(int id) {
            FillGenderDropdown();
            AdmissionModel admissionModel = new AdmissionModel();
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            String query = "exec Edit "+ id+"";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            sda.Fill(dt);
            if (dt.Rows.Count > 0) { 
            admissionModel.FirstName = dt.Rows[0][1].ToString(); 
            admissionModel.LastName = dt.Rows[0][2].ToString(); 
            admissionModel.DateOfBirth= Convert.ToDateTime(dt.Rows[0][3]);
            admissionModel.PlaceOfBirth= dt.Rows[0][4].ToString();
            admissionModel.Gender= Convert.ToInt32(dt.Rows[0][5].ToString());
            admissionModel.Religion= Convert.ToInt32(dt.Rows[0][6].ToString());
            admissionModel.Nationality= Convert.ToInt32(dt.Rows[0][7]);
            admissionModel.PhoneNumber= dt.Rows[0][8].ToString();
            admissionModel.MobileNumber= dt.Rows[0][9].ToString();
            admissionModel.Email= dt.Rows[0][10].ToString();
            admissionModel.Country= Convert.ToInt32(dt.Rows[0][11]);
            admissionModel.State= Convert.ToInt32(dt.Rows[0][12]);
            admissionModel.City= Convert.ToInt32(dt.Rows[0][13]);
            admissionModel.StreetAddress= dt.Rows[0][14].ToString();
            }
            return View(admissionModel);

        }
        [HttpPost]
        public ActionResult Edit(int id, AdmissionModel admissionModel) {
            return View();
        }
        public void FillGenderDropdown() {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            String query = "Select * from Gender";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            sda.Fill(dt);
            ViewBag.Gender = dt;
            List<SelectListItem> genderlist = new List<SelectListItem>();
            foreach (System.Data.DataRow dr in ViewBag.Gender.Rows)
            {
                genderlist.Add(item: new SelectListItem { Text = @dr["Gender"].ToString(), Value = @dr["Id"].ToString() });
            }
            ViewBag.Gender = genderlist;
        }
    }
}
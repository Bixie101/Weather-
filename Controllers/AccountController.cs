using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRegistration.Models; // Referencing the project to access the models.
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data.Common;

namespace UserRegistration.Controllers
{
    public class AccountController : Controller
    {
        // Load the user registration page.
        public ActionResult Index()
        {
            return View();
        }

        // Add an HTTP Post.
        [HttpPost]
        public ActionResult Index(UserClass uc,HttpPostedFileBase file) // Add the class and a second class to upload the files.
        {
            // Initiate a connection to the database.
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            // Query to insert the new user into the "MVCregUser" table.
            string sqlquery = "insert into [dbo].[MVCregUser] (Username, Useremail, Userpassword) values (@Username, @Useremail, @Userpassword)"; 
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn); // Created object for the new sqlconn class with 2 parameters, command and connection.
            sqlconn.Open(); // This is to open the connection string.
            sqlcomm.Parameters.AddWithValue("@Username", uc.Username); // Parameters are string name and object value of public properties in the models.
            sqlcomm.Parameters.AddWithValue("@Useremail", uc.Useremail);
            sqlcomm.Parameters.AddWithValue("@Userpassword", uc.Userpassword);
            sqlcomm.ExecuteNonQuery();
            sqlconn.Close(); // Close the query.
            ViewData["Message"] = "Thank you " + uc.Username + "! Your account has been created successfully!"; // This message will show if registration was successful.

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginClass lc) // Call the Login class.
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString; // Use the name of your connection in the connection string under Web.config.
            SqlConnection sqlconn = new SqlConnection(mainconn);
            // The query that calls from the properties in the MVCregUser table.
            string sqlquery = "select Useremail, Userpassword from [dbo].[MVCregUser] where Useremail=@Useremail and Userpassword=@Userpassword";
            sqlconn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn); // Created object for the new sqlconn class with 2 parameters, command and connection.
            sqlcomm.Parameters.AddWithValue("@Useremail", lc.Useremail); // Parameters are string name and object value of public properties in the models.
            sqlcomm.Parameters.AddWithValue("@Userpassword", lc.Userpassword);
            SqlDataReader sdr = sqlcomm.ExecuteReader();

            // If statement to check whether the user is registered and validate the User data.
            if (sdr.Read())
            {
                Session["useremail"] = lc.Useremail.ToString();
                return RedirectToAction("Weather"); // Redirect to the weather page after login.
            }
            else
            {
                ViewData["Message"] = "User not found. Please try again."; // Error message should user not be registered.
            }

            sqlconn.Close(); // Close the query.
            return View();
        }

        public ActionResult Weather()
        {
            return View();
        }
    }
}
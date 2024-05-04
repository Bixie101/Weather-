using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRegistration.Models; 
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data.Common;

namespace UserRegistration.Controllers
{
    // Specify allowed usernames in the Authorize attribute
   
    public class TourismDestinationController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString; // Get connection string from Web.config

        public ActionResult Index()
        {
            List<TourismDestination> destinations = GetTourismDestinations();
            return View(destinations);
        }

        public ActionResult Create()
        {
            return View(new TourismDestination());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // To prevent CSRF attacks
       
        public ActionResult Create(TourismDestination destination)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AddTourismDestination(destination);
                    return RedirectToAction("Index"); // Using action name for clarity
                }
                catch (Exception ex)
                {
                    //ModelState.AddModelError("", "An error occurred while creating the destination.");
                    // Log the exception for further investigation
                    return View(destination);
                }
            }
            return View(destination);
        }

        // Restrict the Edit and Delete actions to the admin user only.
        [Authorize(Users = "admin")]
        public ActionResult Edit(int id)
        {
            TourismDestination destination = GetTourismDestinationById(id);

            if (destination == null)
            {
                return HttpNotFound(); // Use built-in HttpNotFound()
            }
            return View(destination);
        }

        [HttpPost]
        // Restrict the Edit and Delete actions to the admin user only.
        [Authorize(Users = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TourismDestination destination)
        {
            if (id != destination.Id)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                UpdateTourismDestination(destination);
                return RedirectToAction("Index");
            }

            return View(destination);
        }

        private ActionResult BadRequest()
        {
            throw new NotImplementedException();
        }

        // Restrict the Edit and Delete actions to the admin user only.
        [Authorize(Users = "admin")]
        public ActionResult Delete(int id)
        {
            TourismDestination destination = GetTourismDestinationById(id);

            if (destination == null)
            {
                return HttpNotFound();
            }

            // You can optionally display a confirmation view here before deletion

            return View(destination); // You can optionally use a confirmation view here
        }
       
        [HttpPost, ActionName("Delete")]
        // Restrict the Edit and Delete actions to the admin user only.
        
        [ValidateAntiForgeryToken] // To prevent CSRF attacks
        [Authorize(Users = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            DeleteTourismDestination(id);

            return RedirectToAction("Index");
        }

        private List<TourismDestination> GetTourismDestinations()
        {
            List<TourismDestination> destinations = new List<TourismDestination>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlquery = "SELECT * FROM [dbo].[SiteVisit]"; // Replace with your table name
                SqlCommand sqlcomm = new SqlCommand(sqlquery, connection);
                SqlDataReader reader = sqlcomm.ExecuteReader();

                while (reader.Read())
                {
                    destinations.Add(new TourismDestination
                    {
                        Id = Convert.ToInt32(reader["Id"]), // Replace with appropriate column names
                        Country = reader["Country"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString()
                    });
                }
                reader.Close();
            }
            return destinations;
        }

        private void AddTourismDestination(TourismDestination destination)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlquery = "INSERT INTO [dbo].[SiteVisit] (Country, Name, Description) VALUES (@Country, @Name, @Description)"; // Replace with your table and column names
                SqlCommand sqlcomm = new SqlCommand(sqlquery, connection);
                sqlcomm.Parameters.AddWithValue("@Country", destination.Country);
                sqlcomm.Parameters.AddWithValue("@Name", destination.Name);
                sqlcomm.Parameters.AddWithValue("@Description", destination.Description);
                sqlcomm.ExecuteNonQuery();
            }
        }

        private TourismDestination GetTourismDestinationById(int id)
        {
            TourismDestination destination = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlquery = "SELECT * FROM [dbo].[SiteVisit] WHERE Id = @Id"; // Replace with your table name and column name
                SqlCommand sqlcomm = new SqlCommand(sqlquery, connection);
                sqlcomm.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = sqlcomm.ExecuteReader();

                if (reader.Read())
                {
                    destination = new TourismDestination
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Country = reader["Country"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString()
                    };
                }
                reader.Close();
            }
            return destination;
        }

        private void UpdateTourismDestination(TourismDestination destination)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlquery = "UPDATE [dbo].[SiteVisit] SET Country = @Country, Name = @Name, Description = @Description WHERE Id = @Id"; // Replace with your table name and column names
                SqlCommand sqlcomm = new SqlCommand(sqlquery, connection);
                sqlcomm.Parameters.AddWithValue("@Id", destination.Id);
                sqlcomm.Parameters.AddWithValue("@Country", destination.Country);
                sqlcomm.Parameters.AddWithValue("@Name", destination.Name);
                sqlcomm.Parameters.AddWithValue("@Description", destination.Description);
                sqlcomm.ExecuteNonQuery();
            }
        }

        private void DeleteTourismDestination(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlquery = "DELETE FROM [dbo].[SiteVisit] WHERE Id = @Id"; // Replace with your table name and column names
                SqlCommand sqlcomm = new SqlCommand(sqlquery, connection);
                sqlcomm.Parameters.AddWithValue("@Id", id);
                sqlcomm.ExecuteNonQuery();
            }
        }
    }
}

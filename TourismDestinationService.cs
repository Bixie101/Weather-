using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UserRegistration.Models;

namespace UserRegistration
{
    public class TourismDestinationService : ITourismDestinationService
    {
        private readonly string _connectionString;

        public TourismDestinationService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddDestination(TourismDestination destination)
        {
            throw new NotImplementedException();
        }

        public void DeleteDestination(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TourismDestination> GetAllDestinations()
        {
            List<TourismDestination> destinations = new List<TourismDestination>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id, Country, Name, Description FROM TourismDestinations";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        destinations.Add(new TourismDestination
                        {
                            Id = (int)reader["Id"],
                            Country = reader["Country"].ToString(),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                        });
                    }
                }
            }
            return destinations;
        }

        public TourismDestination GetDestinationById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDestination(TourismDestination destination)
        {
            throw new NotImplementedException();
        }

     

    }
    
}
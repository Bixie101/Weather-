using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UserRegistration.Models; // Referencing the project to access the models.

namespace UserRegistration.Controllers
{
    public class WeatherController : Controller
    {
        public ActionResult Weather() // Change the default Index action to Weather.
        {
            ViewBag.Message = "This is the Weather App";
            return View();
        }

        // Create an HttpPost.
        [HttpPost]
        public string WeatherDetail(string City)
        {
            // Assign API KEY received from OPENWEATHERMAP.ORG
            string appId = "b266937a0a31f8c3cc681c9a807d647d";

            // URL string with City and API key parameters.
            // {0} and {1} are place holders.
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", City, appId);

            using (WebClient client = new WebClient())
            {
                // Special VIEWMODEL design to send only required fields not all fields which received from   
                // www.openweathermap.org api 
                WeatherViewModel weatherVm = new WeatherViewModel();

                // Attempt to search for the user entered city name.
                try
                {
                    // Received from JSON.
                    string json = client.DownloadString(url);

                    // Converting to OBJECT from JSON string.  
                    RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);
                    // Retrieve the individual values for each field.
                    weatherVm.Country = weatherInfo.sys.country;
                    weatherVm.City = weatherInfo.name;
                    weatherVm.Description = weatherInfo.weather[0].description;
                    weatherVm.Humidity = weatherInfo.main.humidity;
                    weatherVm.Wind = weatherInfo.wind.speed;
                    weatherVm.Temp = weatherInfo.main.temp;
                    weatherVm.TempMin = weatherInfo.main.temp_min;
                    weatherVm.TempMax = weatherInfo.main.temp_max;
                    weatherVm.Sunrise = weatherInfo.sys.sunrise;
                    weatherVm.Sunset = weatherInfo.sys.sunset;
                    weatherVm.WeatherIcon = weatherInfo.weather[0].icon;

                // Catch for when the city name entered does not exist.
                } catch
                {
                    // Set the city name to "Error" used for when a searched city is not found.
                    weatherVm.City = "Error";
                }
                // Converting OBJECT to JSON String.
                var jsonstring = new JavaScriptSerializer().Serialize(weatherVm);

                // Return JSON string.
                return jsonstring;
            }
        }
    }
}
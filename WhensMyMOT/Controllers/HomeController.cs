using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WhensMyMOT.Models;

namespace WhensMyMOT.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CheckRegistration(Registration registration)
        {
            // Reset error state
            registration.ErrorMessage = null;

            // Reset if invalid input
            if (!ModelState.IsValid)
            {
                registration.Make = null;
                registration.Model = null;
                registration.Colour = null;
                registration.ExpiryDate = null;
                registration.Mileage = null;
                registration.ErrorMessage = null;
            }
            else
            {
                // Input good, submit request
                string ApiURL = "https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration=" + registration.RegNumber;

                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("Accept", "application/json+v6");
                        httpClient.DefaultRequestHeaders.Add("x-api-key", "fZi8YcjrZN1cGkQeZP7Uaa4rTxua8HovaswPuIno");

                        var response = await httpClient.GetAsync(ApiURL);
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            apiResponse = apiResponse.Substring(1, apiResponse.Length - 2);
                            JObject JsonObject = JObject.Parse(apiResponse);
                            registration.Make = (string)JsonObject.SelectToken("make");
                            registration.Model = (string)JsonObject.SelectToken("model");
                            registration.Colour = (string)JsonObject.SelectToken("primaryColour");
                            JArray MotTests = (JArray)JsonObject.SelectToken("motTests ");
                            registration.ExpiryDate = (string)MotTests[0].SelectToken("expiryDate");
                            registration.Mileage = (string)MotTests[0].SelectToken("odometerValue");
                        }
                        else
                        {
                            registration.ErrorMessage = "Sorry, we were unable to complete your request, please try again. If the problem persists please contact us.";
                        }
                    }
                }
                catch
                {
                    registration.Make = null;
                    registration.Model = null;
                    registration.Colour = null;
                    registration.ExpiryDate = null;
                    registration.Mileage = null;
                    registration.ErrorMessage = "Sorry, a system error has occured - please contact our support department for further guidance.";
                }                
            }

            return View("Index", registration);
        }
    }
}

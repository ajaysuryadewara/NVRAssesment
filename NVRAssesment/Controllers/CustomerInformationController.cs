using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NotNullAttribute = JetBrains.Annotations.NotNullAttribute;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.IO;


namespace NvrAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerInformationController : ControllerBase
    {

        private IHostingEnvironment _env;
        string filename = "Customers.txt";
        public CustomerInformationController(IHostingEnvironment env)
        {
            _env = env; 
        }

        /// <summary>
        ///     Gets list of customer information
        /// </summary>
        /// <returns>Customer informations</returns>
        [HttpGet]
        [NotNull]
        public IEnumerable<CustomerInformation> Get()
        {

            // application's base path
            string contentRootPath = _env.ContentRootPath;
            string  filePath = contentRootPath + filename;

            var jsonText = System.IO.File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<CustomerInformation>>(jsonText)
                                      ?? new List<CustomerInformation>();
        }

        /// <summary>
        ///     Provides customer information
        /// </summary>
        /// <param name="customerInformation">Customer information</param>
        /// <returns>Customer information</returns>
        [HttpPost()]
        [NotNull]
        public Task CustomerInformation(
            [FromBody] [NotNull] CustomerInformation customerInformation)
        {

            string contentRootPath = _env.ContentRootPath;      
            string filePath = contentRootPath + filename;

            // Reads existing customer information
            var customerData = System.IO.File.ReadAllText(filePath);

                var customerInfo = JsonConvert.DeserializeObject<List<CustomerInformation>>(customerData)
                                      ?? new List<CustomerInformation>();

                // Adds new customer information
                customerInfo.Add(new CustomerInformation()
                {
                    FirstName = customerInformation.FirstName,
                    LastName = customerInformation.LastName,
                    EmailAddress = customerInformation.EmailAddress,
                    PhoneNumber = customerInformation.PhoneNumber
                });

                // Updates customer information
                customerData = JsonConvert.SerializeObject(customerInfo);
                System.IO.File.WriteAllText(filePath, customerData);
            
            

            return Task.CompletedTask;
        }
    }
}

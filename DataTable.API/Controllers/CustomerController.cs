using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace DataTable.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IConfiguration _configuration;

        public CustomerController(
            ILogger<CustomerController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Customer> Get(int pageNumber, int pageSize)
        {
            string conStr = _configuration.GetConnectionString("localdb");

            using var con = new SqlConnection(conStr);
            using var cmd = new SqlCommand("dbo.Pagination_Test", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            con.Open();
            using var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            var customers = new List<Customer>();

            while(data.Read())
            {
                customers.Add(new Customer
                {
                    CustomerID = data.GetString("CustomerID"),
                    CompanyName = data.GetString("CompanyName"),
                    ContactName = data.GetString("ContactName"),
                    Country = data.GetString("Country"),
                    City = data.GetString("City"),
                    Phone = data.GetString("Phone")
                });
            }

            return customers;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ISM_Redesing.Models;
using ISM_Redesign.Models;

namespace ISM_Redesing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly List<Customer> _customers;

        public CustomerController()
        {
            // Initialize the list of customers
            _customers = new List<Customer>
                    {
                        new Customer { CustomerID= 1, CustomerName = "John Doe", CustomerAddress = "johndoe@example.com" },
                        new Customer { CustomerID= 2, CustomerName = "Jane Smith", CustomerAddress = "janesmith@example.com" },
                        new Customer { CustomerID= 3, CustomerName = "Bob Johnson", CustomerAddress = "bobjohnson@example.com" }
                    };
        }

        // GET api/customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            return await Task.FromResult(_customers);
        }

        // GET api/customer/{id}
        [HttpGet("{CustomerId}")]
        public async Task<ActionResult<Customer>> Get(int CustomerId)
        {
            var customer = await Task.FromResult(_customers.FirstOrDefault(c => c.CustomerID == CustomerId));

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // POST api/customer
        [HttpPost]
        public async Task<ActionResult<Customer>> Post(Customer customer)
        {
            customer.CustomerID = _customers.Count + 1;
            _customers.Add(customer);

            return CreatedAtAction(nameof(Get), new { CustomerId = customer.CustomerID }, customer);
        }

        // PUT api/customer/{id}
        [HttpPut("{CustomerId}")]
        public async Task<IActionResult> Put(int CustomerId, Customer customer)
        {
            var existingCustomer = await Task.FromResult(_customers.FirstOrDefault(c => c.CustomerID == CustomerId));

            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.CustomerAddress = customer.CustomerAddress;

            return NoContent();
        }

        // DELETE api/customer/{id}
        [HttpDelete("{CustomerId}")]
        public async Task<IActionResult> Delete(int CustomerId)
        {
            var customer = await Task.FromResult(_customers.FirstOrDefault(c => c.CustomerID == CustomerId));

            if (customer == null)
            {
                return NotFound();
            }

            _customers.Remove(customer);

            return NoContent();
        }
    }
}

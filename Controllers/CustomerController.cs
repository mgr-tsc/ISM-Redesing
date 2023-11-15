using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ISM_Redesing.Models;
using ISM_Redesign.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

namespace ISM_Redesing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            return await Task.FromResult(_customers);
        }

        // GET api/customer/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await Task.FromResult(_customers.FirstOrDefault(c => c.CustomerID == id));

            if (customer == null)
            {
                return NotFound();
            }

            return await Task.FromResult(customer);
        }

        // POST api/customer
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Customer>> Post(Customer customer)
        {
            customer.CustomerID = _customers.Count + 1;
            _customers.Add(customer);

            return await Task.FromResult(CreatedAtAction(nameof(Get), new { CustomerId = customer.CustomerID }, customer));
        }

        // PUT api/customer/{id}
        [HttpPut("{CustomerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

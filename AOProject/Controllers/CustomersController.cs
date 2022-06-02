using AOProject.API.DbContexts;
using AOProject.Entities;
using AOProject.Helpers;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace AOProject.Controllers
{
    public class CustomersController : ODataController
    {
        private readonly AODbContext _aoContext;
        public CustomersController(AODbContext aoContext)
        {
            _aoContext = aoContext
                ?? throw new ArgumentNullException(nameof(aoContext));
        }

        [EnableQuery(MaxExpansionDepth = 3, MaxSkip = 10, MaxTop = 5, PageSize = 4)]
        public IActionResult Get()
        {
            return Ok( _aoContext.Customers);
        }
        [EnableQuery]
        public IActionResult Get(int key)
        {
            var customers =  _aoContext.Customers
                .Where(c => c.Id == key);

            if (!customers.Any())
            {
                return NotFound();

            }

            return Ok(SingleResult.Create(customers));
        }

        [HttpGet("/odata/Customers({key})/Accounts")]
        [EnableQuery]
        public IActionResult GetAccountsPerCustomer(int key)
        {
            var customer = _aoContext.Customers
                .Where(c => c.Id == key);

            if (customer == null)
            {
                return NotFound();

            }

            return Ok(_aoContext.Accounts.Where(c => c.Customer.Id == key));
        }

        [HttpGet("odata/People({key})/Email")]
        [HttpGet("odata/People({key})/FirstName")]
        [HttpGet("odata/People({key})/LastName")]
        [HttpGet("odata/People({key})/DateOfBirth")]
        [HttpGet("odata/People({key})/Gender")]
        public async Task<IActionResult> GetCustomerProperty(int key)
        {
            var customer = await _aoContext.Customers
                .FirstOrDefaultAsync(c => c.Id == key);

            if (customer == null)
            {
                return NotFound();
            }

            var propertyToGet = new Uri(HttpContext.Request.GetEncodedUrl()).Segments.Last();

            if (!customer.HasProperty(propertyToGet))
            {
                return NotFound();
            }

            var propertyValue = customer.GetValue(propertyToGet);

            if (propertyValue == null)
            {
                
                return NoContent();
            }

            return Ok(propertyValue);
        }
  

        [HttpPost("odata/Customers")]
        //name method Post
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                      
            _aoContext.Customers.Add(customer);
            await _aoContext.SaveChangesAsync();

            // return the created person 
            return Created(customer);
        }

        [HttpPut("odata/Customers({key})")]
        public async Task<IActionResult> UpdateCustomer(int key, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentCustomer = await _aoContext.Customers
              .FirstOrDefaultAsync(c=> c.Id == key);

            if (currentCustomer == null)
            {
                return NotFound();

            }

           // customer.Id = currentCustomer.Id;
            _aoContext.Entry(currentCustomer).CurrentValues.SetValues(customer);
            await _aoContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("odata/Customers({key})")]
        public async Task<IActionResult> PartiallyUpdatePerson(int key,
           [FromBody] Delta<Customer> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentCustomer = await _aoContext.Customers
                           .FirstOrDefaultAsync(c => c.Id == key);

            if (currentCustomer == null)
            {
                return NotFound();
            }

            patch.Patch(currentCustomer);
            await _aoContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("odata/Customers({key})")]
        public async Task<IActionResult> DeleteOnePerson(int key)
        {
            var currentPerson = await _aoContext.Customers
                .FirstOrDefaultAsync(c => c.Id == key);

            if (currentPerson == null)
            {
                return NotFound();
            }

            _aoContext.Customers.Remove(currentPerson);
            await _aoContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("odata/Customers({key})/Accounts")]
        public async Task<IActionResult> CreateAccountForCustomer(int key, [FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _aoContext.Customers
              .FirstOrDefaultAsync(c => c.Id == key);

            if (customer == null)
            {
                return NotFound();

            }

            account.Customer = customer;

            _aoContext.Accounts.Add(account);
            await _aoContext.SaveChangesAsync();


            return Created(account);
        }

        [HttpPatch("odata/Customers({key})/Accounts({accountKey})")]
        public async Task<IActionResult> PartiallyUpdateAccountForPerson(int key, 
            int accountKey,
          [FromBody] Delta<Account> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _aoContext.Customers
                           .FirstOrDefaultAsync(c => c.Id == key);

            if (customer == null)
            {
                return NotFound();
            }

            var account = await _aoContext.Accounts
              .FirstOrDefaultAsync(c => c.Id == accountKey && c.Customer.Id == key);

            if (account == null)
            {
                return NotFound();
            }

            patch.Patch(account);
            await _aoContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("odata/Customers({key})/Accounts({accountKey})")]
        public async Task<IActionResult> DeleteAccountForPerson(int key,
            int accountKey)
        {
            var currentPerson = await _aoContext.Customers
                .FirstOrDefaultAsync(c => c.Id == key);

            if (currentPerson == null)
            {
                return NotFound();
            }

            var account = await _aoContext.Accounts
              .FirstOrDefaultAsync(c => c.Id == accountKey && c.Customer.Id == key);

            if (account == null)
            {
                return NotFound();
            }

            _aoContext.Accounts.Remove(account);
            await _aoContext.SaveChangesAsync();

            return NoContent();
        }




    }
}

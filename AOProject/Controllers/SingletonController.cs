using AOProject.API.DbContexts;
using AOProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace AOProject.Controllers
{
    [Microsoft.AspNetCore.Components.Route("odata")]
    public class SingletonController :ODataController
    {
        private readonly AODbContext _aoContext;
        public SingletonController(AODbContext aoContext)
        {
            _aoContext = aoContext
                ?? throw new ArgumentNullException(nameof(aoContext));
        }

        [HttpGet("Wambui")]       
        public async Task<IActionResult> GetSingletonWambui()
        {
            var customer =await _aoContext.Customers
                .FirstOrDefaultAsync(c => c.Id == 1);

                     return Ok(customer);
        }

        [HttpPatch("Wambui")]
        public async Task<IActionResult> PartiallyUpdateWambui([FromBody] Delta<Customer> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                var currentCustomer = await _aoContext.Customers
                .FirstOrDefaultAsync(c => c.Id == 1);

            
            patch.Patch(currentCustomer);
            await _aoContext.SaveChangesAsync();

            return NoContent();
        }

    }
}

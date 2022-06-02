//using AOProject.API.DbContexts;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.OData.Routing.Controllers;
//using Microsoft.EntityFrameworkCore;

//namespace AOProject.Controllers
//{
//    public class AccountsController : ODataController
//    {
//        private readonly AODbContext _aoContext;

//        public AccountsController(AODbContext aoContext)
//        {
//            _aoContext = aoContext
//                ?? throw new ArgumentNullException(nameof(aoContext));

//        }

//        [HttpGet("Accounts")]
//        public async Task<IActionResult> Get()
//        {
//            return Ok(await _aoContext.Accounts.ToListAsync());
//        }

//        [HttpGet("Accounts({key})")]
//        public async Task<IActionResult> Get(int key)
//        {
//            var account = await _aoContext.Accounts
//                .FirstOrDefaultAsync(a => a.Id == key);

//            if (account == null)
//            {
//                return NotFound();

//            }

//            return Ok(account);
//        }

//    }
//}

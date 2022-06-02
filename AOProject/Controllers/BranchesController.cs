using AOProject.API.DbContexts;
using AOProject.Entities;
using AOProject.Helpers;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace AOProject.Controllers
{
    [Route("odata")]
    public class BranchesController : ODataController
    {
        private readonly AODbContext _aoContext;

        public BranchesController(AODbContext aoContext)
        {
            _aoContext = aoContext
                ?? throw new ArgumentNullException(nameof(aoContext));

        }

        [EnableQuery]
        [HttpGet("Branches")]
        public IActionResult GetAllBranches()
        {
            return Ok(_aoContext.Branches);

        }

        public IActionResult GeOneBranch(int key)
        {
            var branches = _aoContext.Branches
                .Where(b => b.BranchId == key);

            if (!branches.Any())
            {
                return NotFound();

            }

            return Ok(SingleResult.Create(branches));
        }

        public IActionResult GetRecordStoreTagsProperty(int key)
        {
            var branch =  _aoContext.Branches
                   .FirstOrDefault(b => b.BranchId == key);

            if (branch == null)
            {
                return NotFound();
            }

            var collectionPropertyToGet = new Uri(HttpContext.Request.GetEncodedUrl()).Segments.Last();

            if (!branch.HasProperty(collectionPropertyToGet))
            {
                return NotFound();
            }

            var collectionPropertyValue = branch.GetValue(collectionPropertyToGet);

            if (collectionPropertyValue == null)
            {
                // null = no content
                return NoContent();
            }

            return Ok(collectionPropertyValue);
        }

        [HttpGet("Branches({key})/AOAccountOpening.Functions.IsHighRated(minimumRating={minimumRating})")]
        public async Task<bool> IsHighRated(int key, int minimumRating)
        {            
            var branch = await _aoContext.Branches
                .FirstOrDefaultAsync(b => b.BranchId == key
                    && b.Ratings.Any()
                    && (b.Ratings.Sum(r => r.Value) / b.Ratings.Count) >= minimumRating);

            return (branch != null);
        }

        [HttpGet("Branches/AOAccountOpening.Functions.AreRatedBy(customerIds={customers})")]
        public async Task<IActionResult> AreRatedBy([FromODataUri] IEnumerable<int> people)
        {
            var recordStores = await _aoContext.Branches
                .Where(p => p.Ratings.Any(r => people.Contains(r.RatedBy.Id)))
                .ToListAsync();

            return Ok(recordStores);
        }

        [EnableQuery]
        [HttpGet("Branches/AOProject.PremiumBranch")]
        public async Task<IActionResult> GetPremiumBranches()
        {
            var premiumBranches =await  _aoContext.Branches
                .Where(p => p is PremiumBranch)
                .ToListAsync();
            return Ok(premiumBranches);
        }
    }
}

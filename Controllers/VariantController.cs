using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace WebHunt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VariantController : ControllerBase
    {

        public HuntContext Context { get; set; }
        public VariantController(HuntContext context)
        {
            Context = context;
            
        }

        [RouteAttribute("GetVariant/{ID}")]
        [HttpGet]
        public async Task<ActionResult> Get(int ID)
        {   
            var Variants = Context.Variant;

            

            var Variant = await Variants.ToListAsync();

            return Ok(Variant);
        }
    }
}

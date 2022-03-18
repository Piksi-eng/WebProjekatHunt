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
    public class GunsVariantController : ControllerBase
    {

        public HuntContext Context { get; set; }
        public GunsVariantController(HuntContext context)
        {
            Context = context;
            
        }

        [RouteAttribute("GetGunVariant/{ID}")]
        [HttpGet]
        public async Task<ActionResult> Get(int ID)
        {  
            var GunsVariants = await Context.GunsVariant.Where(g => g.GunID == ID)
                                                        .Select( p => 
                                                        new 
                                                        {
                                                            Ime = p.Variant.VariantName,
                                                            VariantID = p.VariantID
                                                        })
                                                        .ToListAsync();

            
            //var GVariant = await GunsVariant.ToListAsync();

            return Ok(GunsVariants);
        }
        [RouteAttribute("GetGunVariantByname/{Name}")]
        [HttpGet]
        public async Task<ActionResult> GetByName(String Name)
        {  
            if(Name == null)
            {
                Ok("Bez Varijante");
            }
            
            var GunsVariants = await Context.GunsVariant.Where(g => g.Gun.GunName == Name)
                                                        .Select( p => 
                                                        new 
                                                        {
                                                            Ime = p.Variant.VariantName,
                                                            //VariantID = p.VariantID
                                                        })
                                                        .ToListAsync();

            
            //var GVariant = await GunsVariant.ToListAsync();

            return Ok(GunsVariants);
        }

    
        [Route("GetVariant/{GName}/{VName}")]
        [HttpGet]
        public async Task<ActionResult> GetVariant(string GName, string VName)
        {
            var GunVariant = await Context.GunsVariant.Where(g => g.Gun.GunName == GName)
                                                      .Where(g => g.Variant.VariantName == VName)
                                                      .Select(g => g.ID)
                                                      .ToListAsync();

            return Ok(GunVariant);

        }

        [Route("GetVariantStats/{GName}/{VName}")]
        [HttpGet]
        public async Task<ActionResult> GetVariantStats(string GName, string VName)
        {
            var GunVariant = await Context.GunsVariant.Where(g => g.Gun.GunName == GName)
                                                      .Where(g => g.Variant.VariantName == VName)
                                                      .Select(g => 
                                                      new{
                                                          ID = g.ID,
                                                          totalDmg = g.Gun.GunDmg + g.Variant.DmgC,
                                                          totalRange = g.Gun.GunRange + g.Variant.RangeC,
                                                          totalSpeed = g.Gun.GunSpeed + g.Variant.SpeedC,
                                                          totalPrice = g.Gun.GunPrice + g.Variant.PriceC
                                                      })
                                                      .ToListAsync();

            return Ok(GunVariant);

        }
        
        
    }
}

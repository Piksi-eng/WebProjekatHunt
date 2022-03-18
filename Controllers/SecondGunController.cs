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
    public class SecondGunController : ControllerBase
    {

        public HuntContext Context { get; set; }
        public SecondGunController(HuntContext context)
        {
            Context = context;
            
        }

        [RouteAttribute("GetSecondGunsNames")]
        [HttpGet]
        public async Task<ActionResult> GetSecondGunsNames()
        {   
            var guns = Context.SecondGun;

            var gun = await guns.Select(p => p.SGunName)
                                .ToListAsync();

            return Ok(gun);
        }
                [RouteAttribute("GetSecondGun/{Name}")]
        [HttpGet]
        public async Task<ActionResult> GetSecondGun(string Name)
        {   

            var secondGun = await Context.SecondGun.Where(g => g.SGunName == Name)
                                                        .Select( p => 
                                                        new 
                                                        {   
                                                            SID = p.ID,
                                                            SGunName = p.SGunName,
                                                            SGunDmg = p.SGunDmg,
                                                            SGunRange = p.SGunRange,
                                                            SGunSpeed = p.SGunSpeed,
                                                            SGunPrice = p.SGunPrice
                                                        })
                                                        .ToListAsync();

            return Ok(secondGun);
        }
    }
}

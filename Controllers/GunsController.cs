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
    public class GunsController : ControllerBase
    {

        public HuntContext Context { get; set; }
        public GunsController(HuntContext context)
        {
            Context = context;
            
        }

        [RouteAttribute("GetGuns")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {   
            var guns = Context.Guns;

            var gun = await guns.Select(p => p.GunName)
                                .ToListAsync();

            return Ok(gun);
        }

        

        [Route("AddGun")]
        [HttpPost]
        public async Task<ActionResult> AddGun([FromBody] Guns gun)
        {
            if(string.IsNullOrWhiteSpace(gun.GunName) || gun.GunName.Length > 50)
            {
                return BadRequest("Pogresan ID");
            }

            if(gun.GunDmg < 0 || gun.GunDmg > 360)
            {
                return BadRequest("Invalid DMG");
            }

            try
            {
                Context.Guns.Add(gun);
                await Context.SaveChangesAsync();
                return Ok($"Gun Added ID is: {gun.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("EditGun/{Name}/{Damage}")]
        [HttpPut]
        public async Task<ActionResult> EditGun(string Name, int Damage)
        {
            if(string.IsNullOrWhiteSpace(Name) || Name.Length > 50)
            {
                return BadRequest("Pogresan ime");
            }

            if(Damage < 0 || Damage > 360)
            {
                return BadRequest("Invalid DMG");
            }



            try
            {
                var gun = Context.Guns.Where(p => p.GunName == Name).FirstOrDefault();

                if (gun != null)
                {
                    gun.GunName = Name;
                    gun.GunDmg = Damage;

                    await Context.SaveChangesAsync();
                    return Ok($"Gun edited! ID : {gun.ID}");
                }
                else 
                {
                    return BadRequest("Gun not found");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DeleteGun/{ID}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteGun(int ID)
        {
            if(ID <= 0)
            {
                return BadRequest("Wrong ID");
            }

            try 
            {
                var gun = await Context.Guns.FindAsync(ID);
                string name = gun.GunName;
                Context.Guns.Remove(gun);
                await Context.SaveChangesAsync();
                return Ok($"Gun deleted with ID : {name}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

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
    public class LoadoutController : ControllerBase
    {

        public HuntContext Context { get; set; }
        public LoadoutController(HuntContext context)
        {
            Context = context;
            
        }

        [RouteAttribute("GetLoadoutNames")]
        [HttpGet]
        public async Task<ActionResult> GetLoadoutNames()
        {   
            var loadouts = Context.Loadout;

            var loadout = await loadouts.Select(p => p.LName)
                                .ToListAsync();

            return Ok(loadout);
        }

        [RouteAttribute("GetLoadout/{Name}")]
        [HttpGet]
        public async Task<ActionResult> GetLoadout(string Name)
        {   
            var loadout = await Context.Loadout.Where(l => l.LName == Name)
                                                        .Select( l => 
                                                        new 
                                                        {   
                                                            LID = l.ID,
                                                            LName = l.LName,
                                                            VariantID = l.GunsVariant.ID,
                                                            SecondID = l.SecondGun.ID,
                                                            tool1ID = l.Tool1.ID,
                                                            tool2ID = l.Tool2.ID,
                                                            tool3ID = l.Tool3.ID,
                                                            tool4ID = l.Tool4.ID,
                                                            tool1Name = l.Tool1.ToolName,
                                                            tool2Name = l.Tool2.ToolName,
                                                            tool3Name = l.Tool3.ToolName,
                                                            tool4Name = l.Tool4.ToolName,
                                                            totalMainDmg = l.GunsVariant.Gun.GunDmg + l.GunsVariant.Variant.DmgC, 
                                                            totalMainRange = l.GunsVariant.Gun.GunRange + l.GunsVariant.Variant.RangeC, 
                                                            totalMainSpeed = l.GunsVariant.Gun.GunSpeed + l.GunsVariant.Variant.SpeedC, 
                                                            secondGunDmg = l.SecondGun.SGunDmg,
                                                            secondGunRange = l.SecondGun.SGunRange,
                                                            secondGunSpeed = l.SecondGun.SGunSpeed,
                                                            totalVariantPrice = l.GunsVariant.Gun.GunPrice + l.GunsVariant.Variant.PriceC,
                                                            secondPrice = l.SecondGun.SGunPrice
                                                            // totalPrice = l.GunsVariant.Gun.GunPrice + l.GunsVariant.Variant.PriceC + l.SecondGun.SGunPrice + 
                                                            //             l.Tool1.ToolPrice + l.Tool2.ToolPrice + l.Tool3.ToolPrice + l.Tool4.ToolPrice,

                                                        })
                                                        .ToListAsync();
                                                        
            return Ok(loadout);
        }


        [Route("AddLoadout/{Name}/{VID}/{SID}/{T1ID}/{T2ID}/{T3ID}/{T4ID}")]
        [HttpPost]
        public async Task<ActionResult> AddLoadout(string Name, int VID, int SID, int T1ID, int T2ID,int T3ID, int T4ID)
        {


            var gunVariant = await Context.GunsVariant.Where(g => g.ID == VID).FirstOrDefaultAsync();
            var secodnGun = await Context.SecondGun.Where(s => s.ID == SID).FirstOrDefaultAsync();
            var tool1 = await Context.Tools.Where(t => t.ID == T1ID).FirstOrDefaultAsync();
            var tool2 = await Context.Tools.Where(t => t.ID == T2ID).FirstOrDefaultAsync();
            var tool3 = await Context.Tools.Where(t => t.ID == T3ID).FirstOrDefaultAsync();
            var tool4 = await Context.Tools.Where(t => t.ID == T4ID).FirstOrDefaultAsync();

             

            Loadout L = new Loadout
            {
                LName = Name,
                GunsVariant = gunVariant,
                SecondGun = secodnGun,
                Tool1 = tool1,
                Tool2 = tool2,
                Tool3 = tool3,
                Tool4 = tool4, 
            };

            try
            {
                Context.Loadout.Add(L);
                await Context.SaveChangesAsync();
                return Ok($"Loadout Added");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("EditLoadout/{oldName}/{newName}/{VID}/{SID}/{T1ID}/{T2ID}/{T3ID}/{T4ID}")]
        [HttpPut]
        public async Task<ActionResult> EditLoadout(string oldName ,string newName, int VID, int SID, int T1ID, int T2ID,int T3ID, int T4ID)
        {
            if(string.IsNullOrWhiteSpace(oldName) || oldName.Length > 50)
            {
                return BadRequest("Pogresan ime");
            }

            var gunVariant = await Context.GunsVariant.Where(g => g.ID == VID).FirstOrDefaultAsync();
            var secodnGun = await Context.SecondGun.Where(s => s.ID == SID).FirstOrDefaultAsync();
            var tool1 = await Context.Tools.Where(t => t.ID == T1ID).FirstOrDefaultAsync();
            var tool2 = await Context.Tools.Where(t => t.ID == T2ID).FirstOrDefaultAsync();
            var tool3 = await Context.Tools.Where(t => t.ID == T3ID).FirstOrDefaultAsync();
            var tool4 = await Context.Tools.Where(t => t.ID == T4ID).FirstOrDefaultAsync();

            if(gunVariant == null || secodnGun == null || tool1 == null || tool2 == null || tool3 == null || tool4 == null)
            {
                return BadRequest("Nije dobro");
            }

            try
            {
                var loadout = Context.Loadout.Where(p => p.LName == oldName).FirstOrDefault();

                if (loadout != null)
                {
                        loadout.LName = newName;
                        loadout.GunsVariant = gunVariant;
                        loadout.SecondGun = secodnGun;
                        loadout.Tool1 = tool1;
                        loadout.Tool2 = tool2;
                        loadout.Tool3 = tool3;
                        loadout.Tool4 = tool4;


                    await Context.SaveChangesAsync();
                    return Ok($"Gun edited! ID : {loadout.ID}");
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

        [Route("DeleteLoadout/{ID}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteLoadout(int ID)
        {
            if(ID <= 0)
            {
                return BadRequest("Wrong ID");
            }

            try 
            {
                var loadout = await Context.Loadout.FindAsync(ID);
                if(loadout == null){
                    return BadRequest("Loadout not found");
                }
                string name = loadout.LName;
                Context.Loadout.Remove(loadout);
                await Context.SaveChangesAsync();
                return Ok($"Loadout deleted with ID : {name}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

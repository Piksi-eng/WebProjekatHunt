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
    public class ToolsController : ControllerBase
    {

        public HuntContext Context { get; set; }
        public ToolsController(HuntContext context)
        {
            Context = context;
            
        }

        [RouteAttribute("GetToolsNames")]
        [HttpGet]
        public async Task<ActionResult> GetToolsNames()
        {   
            var tools = Context.Tools;

            var tool = await tools.Select(p => p.ToolName)
                                .ToListAsync();

            return Ok(tool);
        }
        
        [RouteAttribute("GetTool/{Name}")]
        [HttpGet]
        public async Task<ActionResult> GetTool(string Name)
        {   

            var tool = await Context.Tools.Where(g => g.ToolName == Name)
                                                        .Select(  p => 
                                                        new 
                                                        {   
                                                            TID = p.ID,
                                                            TName = p.ToolName,
                                                            TDes = p.ToolDescription,
                                                            TPrice = p.ToolPrice

                                                        })
                                                        .ToListAsync();

            return Ok(tool);
        }
    }
}

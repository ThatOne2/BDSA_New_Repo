using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using TrialProject.Shared;
using TrialProject.Shared.DTO;


namespace TrialProject.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class SupervisorsController : ControllerBase {


    private readonly TrialProject.Server.Controllers.DataContext _context;
     private readonly ILogger<SupervisorsController> _logger;

    public SupervisorsController(ILogger<SupervisorsController> logger, Controllers.DataContext context)
    {
        _logger = logger;
         _context = context;
    }

//Ready to be tested
        [HttpPost("api")]
    public async Task<ActionResult<Supervisor>> CreateSupervisor([FromBody]CreateSupervisorDTO s){
        // TODO: Find where to put await
        await Task.FromResult(0);

        if (_context.Supervisors.Where(x => x.name == s.name || x.Email == s.Email).FirstOrDefault() != null){
            return StatusCode(250, "User already exists"); 
        } else {

            try {
                Supervisor supervisor = new Supervisor {name = s.name, Email = s.Email};
                _context.Supervisors.Add(supervisor);

                _context.SaveChanges();
                return Created("Supervisor created", supervisor);
              
            } catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
        return StatusCode(500);
    }

    //===============================================

    //Returns a single suporvisor by ID'
    [HttpGet("api/desc/{id}")]
    public async Task<ActionResult<SupervisorDescDTO>>? ReadSupervisorDescById(int id){
        try
        {
            // TODO: Find where to put await
            await Task.FromResult(0);

            var s = new SupervisorDescDTO
            {
                ID = _context.Supervisors!.Find(id)!.ID,
                name = _context.Supervisors.Find(id)!.name,
                Email = _context.Supervisors.Find(id)!.Email
            };
            return Ok(s);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }


    //============================================

    [HttpDelete("api")]
    public HttpStatusCode DeleteSupervisor(int supervisorId){
        try
        {
            _context.Supervisors!.Remove(_context.Supervisors.Single(a => a.ID == supervisorId));
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }
        catch (Exception)
        {
            return HttpStatusCode.InternalServerError;
        }
    }


    public void Dispose() {

    }

}
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


        [HttpPost]
    public async Task<IActionResult> CreateSupervisor([FromBody]CreateSupervisorDTO s){
          if (_context.Supervisors.Where(x => x.name == s.name || x.Email == s.Email).FirstOrDefault() != null){
              return StatusCode(250, "User is created"); 
          } else {
            try {
             Supervisor supervisor = new Supervisor {name = s.name, Email = s.Email};
            _context.Supervisors.Add(supervisor);

            _context.SaveChanges();
              return Created("Student creates", supervisor);
              
            }  catch (Exception e){
                 Console.WriteLine(e.Message);
           }
          }
          return StatusCode(500);
      }

    //===============================================


     //Returns a list of all projects a Supervisor has posted(Maybe using  yield return?)
     [HttpGet("{id}")]
    public IReadOnlyCollection<Task< ProjectPreviewDTO>>? ReadAllProjectsPostedBySupervisor(int supervisorID){
        return null;
    }


    //Returns a single suporvisor by ID
    [HttpGet("{id}")]
    public Task< SuperviosPreviewDTO>? ReadSupervisorPreviewById(int supervisorId){
        return null;
    }

    //Returns a single suporvisor by ID'
    [HttpGet("{id}")]
    public Task< SupervisorDescDTO>? ReadSupervisorDescById(int supervisorId){
        return null;
    }


    //============================================

    [HttpDelete]
    public HttpStatusCode DeleteSupervisor(int supervisorId){
          return HttpStatusCode.NotFound;
      }


    public void Dispose() {

    }

}
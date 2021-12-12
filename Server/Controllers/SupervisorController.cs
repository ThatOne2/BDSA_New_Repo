using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using TrialProject.Shared;
using TrialProject.Shared.DTO;


namespace TrialProject.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class SupervisorController : ControllerBase {


    private readonly TrialProject.Server.Controllers.DataContext _context;
     private readonly ILogger<SupervisorController> _logger;

    public SupervisorController(ILogger<SupervisorController> logger, Controllers.DataContext context)
    {
        _logger = logger;
         _context = context;
    }


    [HttpPost]
    public HttpStatusCode CreateSuporvisor( CreateSupervisorDTO s){
        Supervisor supervisor = new Supervisor {name = s.name, Email = s.Email};

        try {
        _context.Supervisors.Add(supervisor);
        _context.SaveChanges();
        } catch (NullReferenceException e){
            Console.WriteLine(e.Message);
        }

        return HttpStatusCode.Created;
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
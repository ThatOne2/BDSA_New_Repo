using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
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
          return HttpStatusCode.NotFound;
      }


    //===============================================


     //Returns a list of all projects a Supervisor has posted(Maybe using  yield return?)
     [HttpGet("{id}")]
    public IReadOnlyCollection<Task< ProjectPreviewDTO>>? ReadAllProjectsPostedBySupervisor(int supervisorID){
        return null;
    }


    //Returns a single suporvisor by ID
    [HttpGet("{id}")]
    public Task< SuperviosPreviewDTO>? ReadSuporvisorPreviewById(int supervisorId){
        return null;
    }

    //Returns a single suporvisor by ID'
    [HttpGet("{id}")]
    public Task< SupervisorDescDTO>? ReadSuporvisorDescById(int supervisorId){
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
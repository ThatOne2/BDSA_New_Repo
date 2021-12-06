using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Net.Http;  
using TrialProject.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Server;

[ApiController]
[Route("[controller]")]
public class SupervisorController : ControllerBase {


    private readonly DataContext _context;
     private readonly ILogger<SupervisorController> _logger;

    public SupervisorController(ILogger<SupervisorController> logger, Server.DataContext context)
    {
        _logger = logger;
         _context = context;
    }


    [HttpPost]
    public HttpStatusCode CreateSuporvisor(TrialProject.Shared.DTO.CreateSupervisorDTO s){
          return HttpStatusCode.NotFound;
      }


    //===============================================


     //Returns a list of all projects a Supervisor has posted(Maybe using  yield return?)
     [HttpGet]
    public IReadOnlyCollection<Task<TrialProject.Shared.DTO.ProjectPreviewDTO>> ReadAllProjectsPostedBySupervisor(int supervisorID){
        return null;
    }


    //Returns a single suporvisor by ID
    [HttpGet]
    public Task<TrialProject.Shared.DTO.SuperviosPreviewDTO> ReadSuporvisorPreviewById(int supervisorId){
        return null;
    }

    //Returns a single suporvisor by ID'
    [HttpGet]
    public Task<TrialProject.Shared.DTO.SupervisorDescDTO> ReadSuporvisorDescById(int supervisorId){
        return null;
    }


    //============================================

    [HttpDelete]
    public HttpStatusCode DeleteSupovisor(int supervisorId){
          return HttpStatusCode.NotFound;
      }


    public void Dispose() {

    }

}
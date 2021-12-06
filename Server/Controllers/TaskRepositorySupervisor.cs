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
public class TaskRepositorySupervisor : ControllerBase {


    private readonly DataContext _context;

        public TaskRepositorySupervisor(Server.DataContext context)
        {
            _context = context;
        }
    

     private readonly ILogger<TaskRepositorySupervisor> _logger;

    public TaskRepositorySupervisor(ILogger<TaskRepositorySupervisor> logger)
    {
        _logger = logger;
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
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
using TrialProject.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Identity.Web.Resource;

namespace TrialProject.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase {


    private readonly DataContext _context;
    private readonly ILogger<ProjectController> _logger;

    public ProjectController(ILogger<ProjectController> logger, Controllers.DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    public HttpStatusCode CreateProject(TrialProject.Shared.DTO.CreateProjectDTO  p) {
        var s = _context.Supervisors.Find(p.SupervisorID);

        if (s == null) { return HttpStatusCode.BadRequest;}

        Project project = new Project {name = p.name, longDescription = p.longDescription, shortDescription = p.shortDescription,/*  SupervisorID = s.ID, */ Tags = p.Tags};

        _context.Projects.Add(project);
        _context.SaveChanges();

        return HttpStatusCode.Created;
    }


    //===============================================


    //Returns a single project by ID
    [HttpGet("{id}")]
    public async Task<TrialProject.Shared.DTO.ProjectPreviewDTO> ReadPreviewProjectById(int id) {
           
            var p = _context.Projects.Find(id);

            if(p.ID == null) {
                return null;
            }
            
            var DTOProject = new TrialProject.Shared.DTO.ProjectPreviewDTO{ID = p.ID, name = p.name, shortDescription = p.shortDescription, Tags = p.Tags};
            return DTOProject;
            
        
    }

  /*    //Returns a single project by ID
    [HttpGet]
    public Task<TrialProject.Shared.DTO.ProjectDescDTO> ReadDescProjectById(int projectId){
        return null;
    } */

    //Returns a list of all projects (Maybe using  yield return?)
    [HttpGet]
    public IEnumerable<TrialProject.Shared.DTO.ProjectPreviewDTO> GetAllProjects() {
        var list = new List<TrialProject.Shared.DTO.ProjectPreviewDTO>();
             foreach (var p in _context.Projects)
            {
                //int sID = p.SupervisorID;
                //var superV = _context.Supervisors.Find(sID);
                var ProjDTO = new TrialProject.Shared.DTO.ProjectPreviewDTO{/* SupervisorName = superV.name, */ name = p.name, shortDescription = p.shortDescription, ID = p.ID, Tags = p.Tags};
                list.Add(ProjDTO);
            }

            if (list.Any())
            {
                return list.ToArray();
            }
            else
            {
                return null;
            }
    }
    

  /*    //Returns a list of all projects a Supervisor has posted(Maybe using  yield return?)
     [HttpGet]
    public IReadOnlyCollection<Task<TrialProject.Shared.DTO.ProjectPreviewDTO>> ReadAllProjectsPostedBySupervisor(int supervisorID){
        return null;
    } */

    //Returns a list of projects that has the selected tag(s)  (Maybe using  yield return?)
    [HttpGet("tag/{tag}")]
    public IReadOnlyCollection<Task<TrialProject.Shared.DTO.ProjectPreviewDTO>> ReadProjectListByTag(string t){
        return null;
    }
     
/*     //Returns a list of projects that matches the given word with the short description  (Maybe using  yield return?)
    [HttpGet]
    public IReadOnlyCollection<Task<TrialProject.Shared.DTO.ProjectPreviewDTO>> ReadProjectListByDescription(string word){
        return null;
    } */


    //=============================================

    [HttpPut("{id}/{desc}")]
    public HttpStatusCode UpdateProjectDesciption(int projectId, string newDescription){
          return HttpStatusCode.NotFound;
    }

    [HttpPut("{id}/{status}")]
    public HttpStatusCode UpdateProjectStatus(int projectId, Status s){
         return HttpStatusCode.NotFound;
    }

    //============================================

    [HttpDelete("{id}")]
    public HttpStatusCode DeleteProject(int projectId){
          return HttpStatusCode.NotFound;
      }


    public void Dispose() {

    }

}
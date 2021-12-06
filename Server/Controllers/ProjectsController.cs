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
public class ProjectController : ControllerBase {


    private readonly DataContext _context;
    private readonly ILogger<ProjectController> _logger;

    public ProjectController(ILogger<ProjectController> logger, Server.DataContext context)
    {
        _logger = logger;
        _context = context;
    }


    [HttpPost]
    public HttpStatusCode CreateProject(TrialProject.Shared.DTO.CreateProjectDTO  p) {
        var s = _context.Supervisors.Find(p.SupervisorID);

        if (s == null) { return HttpStatusCode.BadRequest;}

        Project project = new Project {name = p.name, longDescription = p.longDescription, shortDescription = p.shortDescription, SupervisorID = s.ID, Tags = p.Tags};

        _context.Projects.Add(project);
        _context.SaveChanges();

        return HttpStatusCode.Created;
    }



    //===============================================


    //Returns a single project by ID
    [HttpGet("{id}")]
    public async Task<TrialProject.Shared.DTO.ProjectPreviewDTO> ReadPreviewProjectById(int projectId) {
        var p = await _context.Projects.FindAsync(projectId);
        var superV = await _context.Supervisors.FindAsync(p.SupervisorID);
        //var tagList = new List<Tag>();

        var DTOProject = new TrialProject.Shared.DTO.ProjectPreviewDTO{ID = p.ID, name = p.name, SupervisorName = superV.name, shortDescription = p.shortDescription, Tags = p.Tags };
        return DTOProject;
    }

  /*    //Returns a single project by ID
    [HttpGet]
    public Task<TrialProject.Shared.DTO.ProjectDescDTO> ReadDescProjectById(int projectId){
        return null;
    } */

    //Returns a list of all projects (Maybe using  yield return?)
    [HttpGet]
    public IReadOnlyCollection<Task<TrialProject.Shared.DTO.ProjectPreviewDTO>> ReadAllPreviewProjects(){
        return null;
    }

     //Returns a list of all projects a Supervisor has posted(Maybe using  yield return?)
     [HttpGet]
    public IReadOnlyCollection<Task<TrialProject.Shared.DTO.ProjectPreviewDTO>> ReadAllProjectsPostedBySupervisor(int supervisorID){
        return null;
    }

    //Returns a list of projects that has the selected tag(s)  (Maybe using  yield return?)
    [HttpGet]
    public IReadOnlyCollection<Task<TrialProject.Shared.DTO.ProjectPreviewDTO>> ReadProjectListByTag(Tag t){
        return null;
    }
     
    //Returns a list of projects that matches the given word with the short description  (Maybe using  yield return?)
    [HttpGet]
    public IReadOnlyCollection<Task<TrialProject.Shared.DTO.ProjectPreviewDTO>> ReadProjectListByDescription(string word){
        return null;
    }


    //=============================================

    [HttpPut]
    public HttpStatusCode UpdateProjectDesciption(int projectId, string newDescription){
          return HttpStatusCode.NotFound;
    }

    [HttpPut]
    public HttpStatusCode UpdateProjectStatus(int projectId, Status s){
         return HttpStatusCode.NotFound;
    }

    //============================================

    [HttpDelete]
    public HttpStatusCode DeleteProject(int projectId){
          return HttpStatusCode.NotFound;
      }


    public void Dispose() {

    }

}
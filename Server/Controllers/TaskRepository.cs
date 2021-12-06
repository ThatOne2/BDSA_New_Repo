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
public class TaskRepository : ControllerBase, ITaskRepository {


    private readonly DataContext _context;

        public TaskRepository(Server.DataContext context)
        {
            _context = context;
        }
    

     private readonly ILogger<TaskRepository> _logger;

    public TaskRepository(ILogger<TaskRepository> logger)
    {
        _logger = logger;
    }


    [HttpPost]
    public HttpStatusCode CreateProject(TrialProject.Shared.DTO.CreateProjectDTO  p) {
        var s = _context.Supervisors.Find(p.SupervisorID);

        if (s == null) {
             return HttpStatusCode.BadRequest;
        }

        Project project = new Project {name = p.name, longDescription = p.longDescription, shortDescription = p.shortDescription, SupervisorID = s.ID, Tags = p.Tags};

        _context.Projects.Add(project);
        _context.SaveChanges();

        return HttpStatusCode.Created;
    }

    [HttpPost]
    public HttpStatusCode CreateStudent(TrialProject.Shared.DTO.CreateStudentDTO s){
          return HttpStatusCode.NotFound;
      }

    [HttpPost]
    public HttpStatusCode CreateSuporvisor(TrialProject.Shared.DTO.CreateSupervisorDTO s){
          return HttpStatusCode.NotFound;
      }


    //===============================================


    //Returns a single project by ID
    [HttpGet]
    public async Task<TrialProject.Shared.DTO.ProjectPreviewDTO> ReadPreviewProjectById(int projectId) {
        var p = await _context.Projects.FindAsync(projectId);
        var superV = await _context.Supervisors.FindAsync(p.SupervisorID);
        //var tagList = new List<Tag>();

        var DTOProject = new TrialProject.Shared.DTO.ProjectPreviewDTO{ID = p.ID, name = p.name, SupervisorName = superV.name, shortDescription = p.shortDescription, Tags = p.Tags };
        return DTOProject;
    }

     //Returns a single project by ID
    [HttpGet]
    public Task<TrialProject.Shared.DTO.ProjectDescDTO> ReadDescProjectById(int projectId){
        return null;
    }

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

    //Returns a single student by ID
    [HttpGet]
    public Task<TrialProject.Shared.DTO.StudentPreviewDTO> ReadStudentPreviewById(int studentId){
        return null;
    }

    //Returns a single student by ID
    [HttpGet]
    public Task<TrialProject.Shared.DTO.StudentDescDTO> ReadStudentDEscById(int studentId){
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

    [HttpDelete]
    public HttpStatusCode DeleteSupovisor(int supervisorId){
          return HttpStatusCode.NotFound;
      }

    [HttpDelete]
    public HttpStatusCode DeleteStudent(int studentId){
      return HttpStatusCode.NotFound;
      }

    public void Dispose() {

    }

}
using System.Net;
using TrialProject.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrialProject.Shared.DTO;

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
    public HttpStatusCode CreateProject( CreateProjectDTO  p) {
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
    public async Task<ProjectPreviewDTO> ReadPreviewProjectById(int id) {
           
            var p = await _context.Projects.FindAsync(id);

            var tagList = new List<string>();
                foreach (var t in p.Tags) {
                    tagList.Add(t.Name);
                }
            
            var DTOProject = new ProjectPreviewDTO{ID = p.ID, name = p.name, shortDescription = p.shortDescription, Tags = tagList};
            return DTOProject;
            
        
    }

  /*    //Returns a single project by ID
    [HttpGet]
    public Task< ProjectDescDTO> ReadDescProjectById(int projectId){
        return null;
    } */

    //Returns a list of all projects (Maybe using  yield return?)
    [HttpGet]
    public IEnumerable< ProjectPreviewDTO> GetAllProjects() {
           /*  var proj =  from pr in _context.Projects
                        join su in _context.Supervisors on pr.SupervisorID equals su.ID
                        select new {pr.ID, su.name}; */


        var list = new List< ProjectPreviewDTO>();
             foreach (var p in _context.Projects.Include(tag => tag.Tags).Join(_context.Supervisors,
                                                                                p => p.SupervisorID,
                                                                                ss => ss.ID,
                                                                                (p,ss) => new {
                                                                                    Supervisor = ss.name,
                                                                                    shortDesc = p.shortDescription,
                                                                                    ID = p.ID,
                                                                                    Tags = p.Tags,
                                                                                    Name = p.name
                                                                                }))
            {
                 Console.WriteLine(p.Supervisor);
                var tagList = new List<string>();
                foreach (var t in p.Tags) {
                    tagList.Add(t.Name);
                }

                var ProjDTO = new  ProjectPreviewDTO{SupervisorName = p.Supervisor, name = p.Name, shortDescription = p.shortDesc, ID = p.ID, Tags = tagList};
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
    public IReadOnlyCollection<Task< ProjectPreviewDTO>> ReadAllProjectsPostedBySupervisor(int supervisorID){
        return null;
    } */

    //Returns a list of projects that has the selected tag(s)  (Maybe using  yield return?)
    [HttpGet("tag/{tag}")]
    public IReadOnlyCollection<Task< ProjectPreviewDTO>> ReadProjectListByTag(string t){
        return null;
    }
     
/*     //Returns a list of projects that matches the given word with the short description  (Maybe using  yield return?)
    [HttpGet]
    public IReadOnlyCollection<Task< ProjectPreviewDTO>> ReadProjectListByDescription(string word){
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
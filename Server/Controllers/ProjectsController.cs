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
    public async Task<ActionResult<Project>> CreateProject([FromBody]CreateProjectDTO p) {
        Console.WriteLine(p.SupervisorEmail);
      if(p == null ){
          return BadRequest();
      }
      if(p.Tags == null) {
          return BadRequest();
      }

        var tags = new List<Tag>();
        if (p.Tags != null)
        {
            foreach (TagsEnums tag in p.Tags)
            {
                tags.Add(new Tag { Name = tag.ToString() });
            }
        }

        Project project = new Project {
            name = p.name,
            longDescription = p.longDescription,
            shortDescription = p.shortDescription,
            Tags = tags,
            ProjectStatus = Status.Ongoing
        };
/* 
        var s = _context.Supervisors.Where(x => x.name == p.Supervisor || x.Email == p.SupervisorEmail).FirstOrDefault();
        if (s == null) {
             Supervisor newSupervisor = new Supervisor {name = s.name, Email = s.Email};
            _context.Add(newSupervisor);
            _context.SaveChanges();
            s = _context.Supervisors.Where(x => x.name == p.Supervisor || x.Email == p.SupervisorEmail).FirstOrDefault();
        }
 */
        project.SupervisorID = 1;
  
        _context.Projects!.Add(project);
        _context.SaveChanges();

        return CreatedAtAction("Created project", project,200);
    }


    //===============================================


    //Returns a single project by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadDescProjectById(int id) {

        // TODO: Find where to put await
        await Task.FromResult(0);

        var p =  _context.Projects!.Include(tag => tag.Tags).Join(_context.Supervisors,
                                                                            p => p.SupervisorID,
                                                                            ss => ss.ID,
                                                                            (p,ss) => new {
                                                                                Supervisor = ss.name,
                                                                                shortDesc = p.shortDescription,
                                                                                ID = p.ID,
                                                                                Tags = p.Tags,
                                                                                Name = p.name,
                                                                                LongDesc = p.longDescription,
                                                                                Status = p.ProjectStatus
                                                                            }).Where(x => x.ID == id).FirstOrDefault();
        
        
        var tagList = new List<string>();
        if (p == null)
        {
            return BadRequest(); 
        }
        else
        {
            if (p.Tags != null)
            {
                foreach (var t in p!.Tags!)
                {
                    tagList.Add(t.Name!);
                }
            }
        }

            
        var DTOProject = new ProjectDescDTO{ID = p.ID, name = p.Name, shortDescription = p.shortDesc, Tags = tagList, 
                                            SupervisorName = p.Supervisor, longDescription = p.LongDesc, ProjectStatus = p.Status.ToString()};
        return Ok(DTOProject);

        
    }


    //Returns a list of all projects (Maybe using  yield return?)
    [HttpGet]
    public IEnumerable<ProjectPreviewDTO> GetAllProjects() {
        var list = new List< ProjectPreviewDTO>();
        foreach (var p in _context.Projects!.Include(tag => tag.Tags).Join(_context.Supervisors!,
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
            var tagList = new List<string>();
            foreach (var t in p.Tags!) {
                tagList.Add(t.Name!);
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
            return null!;
        } 
    }
    

   //Returns a list of all projects a Supervisor has posted(Maybe using  yield return?)
   [HttpGet("supervisor/{supervisorID}")]
    public  IEnumerable<ProjectPreviewDTO> ReadAllProjectsPostedBySupervisor(int supervisorID){
        var list = new List<ProjectPreviewDTO>();
        foreach (var p in _context.Projects!.Include(tag => tag.Tags).Join(_context.Supervisors!,
                                                                        p => p.SupervisorID,
                                                                        ss => ss.ID,
                                                                        (p,ss) => new {
                                                                            Supervisor = ss.name,
                                                                            supervisorID = ss.ID,
                                                                            shortDesc = p.shortDescription,
                                                                            ID = p.ID,
                                                                            Tags = p.Tags,
                                                                            Name = p.name
                                                                        }).Where(x => x.supervisorID == supervisorID))
        {
            var tagList = new List<string>();
            foreach (var t in p.Tags!) {
                tagList.Add(t.Name!);
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
            return null!;
        } 
    } 

    //Returns a list of projects that has the selected tag(s)  (Maybe using  yield return?)
    [HttpGet("rawtag/{tag}")]
    public IEnumerable<ProjectPreviewDTO>? ReadProjectListByTag(string tag){
        var list = new List<ProjectPreviewDTO>();
        foreach (var p in _context.Projects!.Include(xtag => xtag.Tags).Join(_context.Supervisors!,
                                                                        p => p.SupervisorID,
                                                                        ss => ss.ID,
                                                                        (p,ss) => new {
                                                                            Supervisor = ss.name,
                                                                            supervisorID = ss.ID,
                                                                            shortDesc = p.shortDescription,
                                                                            ID = p.ID,
                                                                            Tags = p.Tags,
                                                                            Name = p.name
                                                                        }).Where(x => x.Tags.Any(ptag => ptag.Name == tag)))
            //.Where(x => x.Tags!.Any(tag => tag.Name!.ToString() == t))
        {
            var tagList = new List<string>();
            foreach (var ytag in p.Tags!) {
                tagList.Add(ytag.Name!);
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
            return null!;
        } 
    }


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
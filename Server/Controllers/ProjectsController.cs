using System.Net;
using TrialProject.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrialProject.Shared.DTO;

namespace TrialProject.Server.Controllers;

///ProjectController is the class that handels api calls from the Client and returns the expected result. 
///Either a statuscode or a statuscode with a Project/DTO object 
///It is implemeted with the intended CRUD functions 
[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase, IProjectController {


    private readonly DataContext _context;
    private readonly ILogger<ProjectController> _logger;

    public ProjectController(ILogger<ProjectController> logger, Controllers.DataContext context)
    {
        _logger = logger;
        _context = context;
    }

     ///Makes a Project based on the given CreateProjectDTO and adds it to the database
    [HttpPost]
    public async Task<ActionResult<Project>> CreateProject([FromBody]CreateProjectDTO p) {
        await Task.FromResult(0);

        if (p.Tags == null) {
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

        Project project = new Project 
        {
            name = p.name,
            longDescription = p.longDescription,
            shortDescription = p.shortDescription,
            Tags = tags,
            ProjectStatus = Status.Ongoing
        };
 
        var s = _context.Supervisors!.Where(x => x.name == p.Supervisor || x.Email == p.SupervisorEmail).FirstOrDefault();
        if (s == null) {
          return StatusCode(500);
        }
 
        project.SupervisorID = s.ID;
  
        await _context.Projects!.AddAsync(project);
        _context.SaveChanges();

        return Created("Created project", project);
    }


    //===============================================

    ///Returns a single project by ID
    [HttpGet("api/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectDescDTO))]
    public async Task<ActionResult<ProjectDescDTO>> ReadDescProjectById(int id) {

        await Task.FromResult(0);

        var p =  _context.Projects!.Include(tag => tag.Tags).Join(_context.Supervisors!,
                                                                            p => p.SupervisorID,
                                                                            ss => ss.ID,
                                                                            (p,ss) => new {
                                                                                Supervisor = ss.name,
                                                                                Email = ss.Email,
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
        
        var DTOProject = new ProjectDescDTO
        {
            ID = p.ID, 
            name = p.Name, 
            shortDescription = p.shortDesc, 
            Tags = tagList, 
            SupervisorName = p.Supervisor, 
            SupervisorEmail = p.Email,
            longDescription = p.LongDesc, 
            ProjectStatus = p.Status
        };
        if (DTOProject != null) return Ok(DTOProject);
        return BadRequest();
    }


    ///Returns a list of all projects 
    [HttpGet("api")]
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

            var ProjDTO = new  ProjectPreviewDTO
            {
                SupervisorName = p.Supervisor, 
                name = p.Name, 
                shortDescription = p.shortDesc, 
                ID = p.ID, 
                Tags = tagList
            };
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
    
   ///Returns a list of all projects a Supervisor has posted(Maybe using  yield return?)
   [HttpGet("api/supervisor/{supervisorID}")]
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

            
            var ProjDTO = new ProjectPreviewDTO
            {
                SupervisorName = p.Supervisor,
                name = p.Name,
                shortDescription = p.shortDesc,
                ID = p.ID,
                Tags = tagList
            };
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

    ///Returns a list of projects that has the selected tag
    [HttpGet("api/tag/{tag}")]
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
                                                                        }).Where(x => x.Tags!.Any(ptag => ptag.Name == tag)))
        {
            var tagList = new List<string>();
            foreach (var ytag in p.Tags!) {
                tagList.Add(ytag.Name!);
            }
               
            var ProjDTO = new  ProjectPreviewDTO
            { 
                SupervisorName = p.Supervisor, 
                name = p.Name, 
                shortDescription = p.shortDesc, 
                ID = p.ID, 
                Tags = tagList
            };
            list.Add(ProjDTO);
        }

        return list;
    }

    ///Returns all projects that contains the given string  
     [HttpGet("api/search/{s}")]
    public IEnumerable<ProjectPreviewDTO>? ReadProjectListBySearch(string s){
        Console.WriteLine(s);
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
                                                                        }).Where(x => x.Name!.Contains(s)))
           
        {
            var tagList = new List<string>();
            foreach (var ytag in p.Tags!) {
                tagList.Add(ytag.Name!);
            }
               
            var ProjDTO = new  ProjectPreviewDTO
            { 
                SupervisorName = p.Supervisor, 
                name = p.Name, 
                shortDescription = p.shortDesc, 
                ID = p.ID, 
                Tags = tagList
            };
            list.Add(ProjDTO);
        }

        return list;
    }


    //=============================================
    ///Updates the description of project with given id to the given string

    [HttpPut("api/{id:int}/desc")]
    public HttpStatusCode UpdateProjectDesciption(int id, [FromBody] string newDescription){
        try
        {
            var proj = _context.Projects!.FirstOrDefault(x => x.ID == id);

            if (proj != null)
            {
                proj.longDescription = newDescription;
                _context.SaveChanges();
            } else
            {
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.OK;
        }
        catch (Exception)
        {
            return HttpStatusCode.InternalServerError;
        }
    }

    ///Updates the Status of a given project to the given status
    [HttpPut("api/{id}/status")]
    public HttpStatusCode UpdateProjectStatus(int id, [FromBody] Status status){
        try
        {
            var proj = _context.Projects!.FirstOrDefault(x => x.ID == id);

            if (proj != null)
			{
                proj.ProjectStatus = status;
                _context.SaveChanges();
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.OK;
        }
        catch (Exception)
        {
            return HttpStatusCode.InternalServerError;
        }
    }

    //============================================
    
    ///Deletes project with given id
    [HttpDelete("api/{id:int}")]
    public HttpStatusCode DeleteProject(int id) {
        try
        {
            _context.Projects!.Remove(_context.Projects.Single(a => a.ID == id));
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }
        catch (Exception) { 
            return HttpStatusCode.InternalServerError;
        }
    }


    public void Dispose() {

    }

}
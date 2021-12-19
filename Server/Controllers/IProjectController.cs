using System.Net;
using TrialProject.Shared;
using Microsoft.AspNetCore.Mvc;
using TrialProject.Shared.DTO;

namespace TrialProject.Server.Controllers;

public interface IProjectController  {


    //Takes the given CreateProjectDTO, makes it into a project and adds it to the database
    [HttpPost]
    public Task<ActionResult<Project>> CreateProject([FromBody]CreateProjectDTO p);


    //===============================================

    //Returns a single project by given ID
    [HttpGet("api/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectDescDTO))]
    public Task<ActionResult<ProjectDescDTO>> ReadDescProjectById(int id);


    ///Returns a list of all projects 
    [HttpGet("api")]
    public IEnumerable<ProjectPreviewDTO> GetAllProjects();
    
   ///Returns a list of all projects a Supervisor has posted
   [HttpGet("api/supervisor/{supervisorID}")]
    public  IEnumerable<ProjectPreviewDTO> ReadAllProjectsPostedBySupervisor(int supervisorID);

    ///Returns a list of projects that has the selected tag
    [HttpGet("api/tag/{tag}")]
    public IEnumerable<ProjectPreviewDTO>? ReadProjectListByTag(string tag);


    ///Returns all projects that contains the given string  
     [HttpGet("api/search/{s}")]
    public IEnumerable<ProjectPreviewDTO>? ReadProjectListBySearch(string s);


    //=============================================
    
    ///Updates the description of project with given id to the given string
    [HttpPut("api/{id:int}/desc")]
    public HttpStatusCode UpdateProjectDesciption(int id, [FromBody] string newDescription);

    ///Updates the Status of a given project to the given status
    [HttpPut("api/{id}/status")]
    public HttpStatusCode UpdateProjectStatus(int id, [FromBody] Status status);

    //============================================

    ///Deletes project with given id
    [HttpDelete("api/{id:int}")]
    public HttpStatusCode DeleteProject(int id);

}
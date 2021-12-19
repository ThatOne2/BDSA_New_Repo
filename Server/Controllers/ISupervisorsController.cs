using System.Net;
using Microsoft.AspNetCore.Mvc;
using TrialProject.Shared;
using TrialProject.Shared.DTO;


namespace TrialProject.Server.Controllers;

public interface ISupervisorsController {

    //Makes a Supervisor based on the given CreateSupervisorDTO and adds it to the database
    [HttpPost("api")]
    public Task<ActionResult<Supervisor>> CreateSupervisor([FromBody]CreateSupervisorDTO s);

    //===============================================

    //Returns a single suporvisor by ID'
    [HttpGet("api/desc/{id}")]
    public Task<ActionResult<SupervisorDescDTO>>? ReadSupervisorDescById(int id);


    //============================================

    //deletes supervisor with given id
    [HttpDelete("api")]
    public HttpStatusCode DeleteSupervisor(int supervisorId);

}
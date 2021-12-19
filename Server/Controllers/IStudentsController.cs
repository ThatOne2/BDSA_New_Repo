using System.Net;
using Microsoft.AspNetCore.Mvc;
using TrialProject.Shared;
using TrialProject.Shared.DTO;

namespace TrialProject.Server.Controllers;

public interface IStudentsController{


    //Makes a Student based on the given CreateStudentDTO and adds it to the database
    [HttpPost("api")]
    public Task<ActionResult<Student>> CreateStudent([FromBody]CreateStudentDTO s);


    //===============================================


    //Returns a single student by id
    [HttpGet("api/{id}")]
    public Task<ActionResult<StudentDescDTO>> ReadStudentDescById(int id);


    //============================================
 
    //deletes student with given id
    [HttpDelete("api")]
    public HttpStatusCode DeleteStudent(int studentId);

}
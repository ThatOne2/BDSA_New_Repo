using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using TrialProject.Shared;
using TrialProject.Shared.DTO;

namespace TrialProject.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase , IStudentsController {


    private readonly Controllers.DataContext _context;
     private readonly ILogger<StudentsController> _logger;

    public StudentsController(ILogger<StudentsController> logger, Controllers.DataContext context)
    {
        _logger = logger;
         _context = context;
    }

//Ready to be tested
    [HttpPost("api")]
    public async Task<ActionResult<Student>> CreateStudent([FromBody]CreateStudentDTO s){
          if (_context.Students!.Where(x => x.name == s.name || x.Email == s.Email).FirstOrDefault() != null){
              return StatusCode(250, "User is created");
          } else {
            try {
             Student student = new Student {name = s.name, Email = s.Email};
            await _context.Students!.AddAsync(student);

            _context.SaveChanges();
              return Created("Student creates", student);

            }  catch (Exception e){
                 Console.WriteLine(e.Message);
           }
          }
          return StatusCode(500);
      }


    //===============================================


    //Returns a single student by ID
    //Might have to change back to Task<IActionResult>
    [HttpGet("api/{id}")]
    public async Task<ActionResult<StudentDescDTO>> ReadStudentDescById(int id){
        try
        {
            // TODO: Find where to put await
            await Task.FromResult(0);

            var s = new StudentDescDTO
            {
                ID = _context.Students!.Find(id)!.ID,
                name = _context.Students.Find(id)!.name,
                Email = _context.Students.Find(id)!.Email
            };
            return Ok(s);
        }
        catch (Exception e) { 
            Console.WriteLine(e.Message);
            return BadRequest();
        }
        
    }


    //============================================
 

    [HttpDelete("api")]
    public HttpStatusCode DeleteStudent(int studentId){
        try
        {
            _context.Students!.Remove(_context.Students.Single(a => a.ID == studentId));
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }
        catch (Exception)
        {
            return HttpStatusCode.InternalServerError;
        }
    }

    public void Dispose() {

    }

}
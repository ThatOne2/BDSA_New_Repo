using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using TrialProject.Shared;
using TrialProject.Shared.DTO;

namespace TrialProject.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase {


    private readonly Controllers.DataContext _context;
     private readonly ILogger<StudentsController> _logger;

    public StudentsController(ILogger<StudentsController> logger, Controllers.DataContext context)
    {
        _logger = logger;
         _context = context;
    }


    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody]CreateStudentDTO s){
          if (_context.Students.Where(x => x.name == s.name || x.Email == s.Email).FirstOrDefault() != null){
              return StatusCode(250, "User is created");
          } else {
            try {
             Student student = new Student {name = s.name, Email = s.Email};
            _context.Students.Add(student);

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
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadStudentDEscById(int id){

        // TODO: Find where to put await
        await Task.FromResult(0);

        var s = new  StudentDescDTO{ID = _context.Students!.Find(id)!.ID, name = _context.Students.Find(id)!.name, Email = _context.Students.Find(id)!.Email};
        return Ok(s);
    }




    //============================================
 

    [HttpDelete]
    public HttpStatusCode DeleteStudent(int studentId){
      return HttpStatusCode.NotFound;
      }

    public void Dispose() {

    }

}
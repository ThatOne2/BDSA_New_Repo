using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
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
    public HttpStatusCode CreateStudent(Controllers.DataContext s){
          return HttpStatusCode.NotFound;
      }


    //===============================================


    //Returns a single student by ID
    [HttpGet("{id}")]
    public async Task< StudentDescDTO> ReadStudentDEscById(int id){
        
        var s = new  StudentDescDTO{ID = _context.Students.Find(id)!.ID, name = _context.Students.Find(id)!.name, Email = _context.Students.Find(id)!.Email};
        return s;
    }




    //============================================
 

    [HttpDelete]
    public HttpStatusCode DeleteStudent(int studentId){
      return HttpStatusCode.NotFound;
      }

    public void Dispose() {

    }

}
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Server;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase {


    private readonly DataContext _context;
     private readonly ILogger<StudentsController> _logger;

    public StudentsController(ILogger<StudentsController> logger, Server.DataContext context)
    {
        _logger = logger;
         _context = context;
    }


    [HttpPost]
    public HttpStatusCode CreateStudent(TrialProject.Shared.DTO.CreateStudentDTO s){
          return HttpStatusCode.NotFound;
      }


    //===============================================


    //Returns a single student by ID
    [HttpGet("{id}")]
    public async Task<TrialProject.Shared.DTO.StudentDescDTO> ReadStudentDEscById(int id){
        
        var s = new TrialProject.Shared.DTO.StudentDescDTO{ID = _context.Students.Find(id).ID, name = _context.Students.Find(id).name, Email = _context.Students.Find(id).Email};
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
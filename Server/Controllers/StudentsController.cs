using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Net.Http;  
using TrialProject.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

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
    [HttpGet]
    public Task<TrialProject.Shared.DTO.StudentPreviewDTO> ReadStudentPreviewById(int studentId){
        return null;
    }

    //Returns a single student by ID
    [HttpGet]
    public Task<TrialProject.Shared.DTO.StudentDescDTO> ReadStudentDEscById(int studentId){
        return null;
    }




    //============================================
 

    [HttpDelete]
    public HttpStatusCode DeleteStudent(int studentId){
      return HttpStatusCode.NotFound;
      }

    public void Dispose() {

    }

}
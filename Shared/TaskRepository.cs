using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Net.Http;  




namespace TrialProject.Shared;

public class TaskRepository : ITaskRepository {


    private readonly Server.DataContext _context;

        public TaskRepository(Server.DataContext context)
        {
            _context = context;
        }
    

     public Response CreateProject(DTO.CreateProjectDTO  p) {
          Project project = new Project {name = p.name, longDescription = p.longDescription, shortDescription = p.shortDescription, SupervisorID = p.SupervisorName, Tags = p.Tags};

            _context.Projects.Add(project);
            _context.SaveChanges();

            return Response.Error;
      }

    public Response CreateStudent(DTO.CreateStudentDTO s){
         return Response.Error;
      }


    public Response CreateSuporvisor(DTO.CreateSupervisorDTO s){
         return Response.Error;
      }


    //===============================================


    //Returns a single project by ID
    public async Task<DTO.ProjectPreviewDTO> ReadPreviewProjectById(int projectId) {
        var p = await _context.Projects.FindAsync(projectId);
        var superV = await _context.Supervisors.FindAsync(p.SupervisorID);
        //var tagList = new List<Tag>();

        var DTOProject = new DTO.ProjectPreviewDTO{ID = p.ID, name = p.name, SupervisorName = superV.name, shortDescription = p.shortDescription, Tags = p.Tags };
        return DTOProject;
    }

     //Returns a single project by ID
    public (Task<DTO.ProjectDescDTO>, HttpWebResponse) ReadDescProjectById(int projectId){
        return (null,null);
    }

    //Returns a list of all projects (Maybe using  yield return?)
    public (IReadOnlyCollection<Task<DTO.ProjectPreviewDTO>>, HttpWebResponse) ReadAllPreviewProjects(){
        return (null,null);
    }

     //Returns a list of all projects a Supervisor has posted(Maybe using  yield return?)
    public (IReadOnlyCollection<Task<DTO.ProjectPreviewDTO>>, HttpWebResponse) ReadAllProjectsPostedBySupervisor(int supervisorID){
        return (null,null);
    }

    //Returns a list of projects that has the selected tag(s)  (Maybe using  yield return?)
    public (IReadOnlyCollection<Task<DTO.ProjectPreviewDTO>>, HttpWebResponse) ReadProjectListByTag(Tag t){
        return (null,null);
    }
     
    //Returns a list of projects that matches the given word with the short description  (Maybe using  yield return?)
    public (IReadOnlyCollection<Task<DTO.ProjectPreviewDTO>>, HttpWebResponse) ReadProjectListByDescription(string word){
        return (null,null);
    }

    //Returns a single suporvisor by ID
    public (Task<DTO.SuperviosPreviewDTO>, HttpWebResponse) ReadSuporvisorPreviewById(int supervisorId){
        return (null,null);
    }

    //Returns a single suporvisor by ID
    public (Task<DTO.SupervisorDescDTO>, HttpWebResponse) ReadSuporvisorDescById(int supervisorId){
        return (null,null);
    }

    //Returns a single student by ID
    public (Task<DTO.StudentPreviewDTO>, HttpWebResponse) ReadStudentPreviewById(int studentId){
        return (null,null);
    }

    //Returns a single student by ID
    public (Task<DTO.StudentDescDTO>, HttpWebResponse) ReadStudentDEscById(int studentId){
        return (null,null);
    }


    //=============================================

    public Response UpdateProjectDesciption(int projectId, string newDescription){
         return Response.Error;
      }

    public Response UpdateProjectStatus(int projectId, Status s){
         return Response.Error;
      }

    //============================================

    public Response DeleteProject(int projectId){
         return Response.Error;
      }

    public Response DeleteSupovisor(int supervisorId){
         return Response.Error;
      }

    public Response DeleteStudent(int studentId){
         return Response.Error;
      }

    public void Dispose() {

    }

}
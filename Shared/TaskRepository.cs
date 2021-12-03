using System;
using System.Collections.Generic;
using System.Net;

namespace TrialProject.Shared;

public class TaskRepository : ITaskRepository {
     public HttpWebResponse CreateProject(Project p) {
         return null;
      }

    public HttpWebResponse CreateStudent(Student s){
         return null;
      }


    public HttpWebResponse CreateSuporvisor(Supervisor s){
         return null;
      }


    //===============================================


    //Returns a single project by ID
    public (Task<DTO.ProjectPreviewDTO>, HttpWebResponse) ReadPreviewProjectById(int projectId) {
        return (null,null);
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

    public HttpWebResponse UpdateProjectDesciption(int projectId, string newDescription){
         return null;
      }

    public HttpWebResponse UpdateProjectStatus(int projectId, Status s){
         return null;
      }

    //============================================

    public HttpWebResponse DeleteProject(int projectId){
         return null;
      }

    public HttpWebResponse DeleteSupovisor(int supervisorId){
         return null;
      }

    public HttpWebResponse DeleteStudent(int studentId){
         return null;
      }

    public void Dispose() {

    }

}
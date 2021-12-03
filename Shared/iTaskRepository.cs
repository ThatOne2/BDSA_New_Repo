using System;
using System.Collections.Generic;
using System.Net;

namespace TrialProject.Shared;
public interface ITaskRepository : IDisposable
{   
    HttpWebResponse CreateProject(Project p);

    HttpWebResponse CreateStudent(Student s); 

    HttpWebResponse CreateSuporvisor(Supervisor s); 

    //===============================================

    //Returns a single project by ID
    Task<DTO.ProjectPreviewDTO> ReadPreviewProjectById(int projectId);
     //Returns a single project by ID
    (Task<DTO.ProjectDescDTO>, HttpWebResponse) ReadDescProjectById(int projectId);

    //Returns a list of all projects (Maybe using  yield return?)
    (IReadOnlyCollection<Task<DTO.ProjectPreviewDTO>>, HttpWebResponse) ReadAllPreviewProjects();

     //Returns a list of all projects a Supervisor has posted(Maybe using  yield return?)
    (IReadOnlyCollection<Task<DTO.ProjectPreviewDTO>>, HttpWebResponse) ReadAllProjectsPostedBySupervisor(int supervisorID);

    //Returns a list of projects that has the selected tag(s)  (Maybe using  yield return?)
     (IReadOnlyCollection<Task<DTO.ProjectPreviewDTO>>, HttpWebResponse) ReadProjectListByTag(Tag t); 
     
    //Returns a list of projects that matches the given word with the short description  (Maybe using  yield return?)
     (IReadOnlyCollection<Task<DTO.ProjectPreviewDTO>>, HttpWebResponse) ReadProjectListByDescription(string word);

    //Returns a single suporvisor by ID
    (Task<DTO.SuperviosPreviewDTO>, HttpWebResponse) ReadSuporvisorPreviewById(int supervisorId);

    //Returns a single suporvisor by ID
    (Task<DTO.SupervisorDescDTO>, HttpWebResponse) ReadSuporvisorDescById(int supervisorId);

    //Returns a single student by ID
    (Task<DTO.StudentPreviewDTO>, HttpWebResponse) ReadStudentPreviewById(int studentId);

    //Returns a single student by ID
    (Task<DTO.StudentDescDTO>, HttpWebResponse) ReadStudentDEscById(int studentId);

    //=============================================

    HttpWebResponse UpdateProjectDesciption(int projectId, string newDescription); //Make a check if you are autherized to update

    HttpWebResponse UpdateProjectStatus(int projectId, Status s); //Make a check if you are autherized to update

    //============================================

    HttpWebResponse DeleteProject(int projectId); //Make a check if you are autherized to delete

    HttpWebResponse DeleteSupovisor(int supervisorId);//Make a check if you are autherized to delete

    HttpWebResponse DeleteStudent(int studentId);//Make a check if you are autherized to delete




}
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
    (Task<Project>, HttpWebResponse) ReadProjectById(int projectId);

    //Returns a list of all projects (Maybe using  yield return?)
    (IReadOnlyCollection<Task<Project>>, HttpWebResponse) ReadAllProjects();

     //Returns a list of all projects a Supervisor has posted(Maybe using  yield return?)
    (IReadOnlyCollection<Task<Project>>, HttpWebResponse) ReadAllProjectsPostedBySupervisor(int supervisorID);

    //Returns a list of projects that has the selected tag(s)  (Maybe using  yield return?)
     (IReadOnlyCollection<Task<Project>>, HttpWebResponse) ReadProjectListByTag(Tag t); //TODO: Make tag enumm class

    //Returns a list of projects that matches the given word with the short description  (Maybe using  yield return?)
     (IReadOnlyCollection<Task<Project>>, HttpWebResponse) ReadProjectListByDescription(string word);

    //Returns a single suporvisor by ID
    (Task<Supervisor>, HttpWebResponse) ReadSuporvisorById(int supervisorId);

    //Returns a single student by ID
    (Task<Student>, HttpWebResponse) ReadStudentById(int studentId);

    //=============================================

    HttpWebResponse UpdateProjectDesciption(int projectId, string newDescription); //Make a check if you are autherized to update

    HttpWebResponse UpdateProjectStatus(int projectId, Status s); //Make a check if you are autherized to update

    //============================================

    HttpWebResponse DeleteProject(int projectId); //Make a check if you are autherized to delete

    HttpWebResponse DeleteSupovisor(int supervisorId);//Make a check if you are autherized to delete

    HttpWebResponse DeleteStudent(int studentId);//Make a check if you are autherized to delete




}
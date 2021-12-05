using System;
using System.Collections.Generic;
using System.Net;

namespace TrialProject.Shared;
public interface ITaskRepository : IDisposable
{   
    Response CreateProject(DTO.CreateProjectDTO p);

    Response CreateStudent(DTO.CreateStudentDTO s); 

    Response CreateSuporvisor(DTO.CreateSupervisorDTO s); 

    //===============================================

    //Returns a single project by ID
    Task<DTO.ProjectPreviewDTO> ReadPreviewProjectById(int projectId);
     //Returns a single project by ID
    Task<DTO.ProjectDescDTO>ReadDescProjectById(int projectId);

    //Returns a list of all projects (Maybe using  yield return?)
    IReadOnlyCollection<Task<DTO.ProjectPreviewDTO>> ReadAllPreviewProjects();

     //Returns a list of all projects a Supervisor has posted(Maybe using  yield return?)
    IReadOnlyCollection<Task<DTO.ProjectPreviewDTO>> ReadAllProjectsPostedBySupervisor(int supervisorID);

    //Returns a list of projects that has the selected tag(s)  (Maybe using  yield return?)
     IReadOnlyCollection<Task<DTO.ProjectPreviewDTO>> ReadProjectListByTag(Tag t); 
     
    //Returns a list of projects that matches the given word with the short description  (Maybe using  yield return?)
     IReadOnlyCollection<Task<DTO.ProjectPreviewDTO>> ReadProjectListByDescription(string word);

    //Returns a single suporvisor by ID
    Task<DTO.SuperviosPreviewDTO> ReadSuporvisorPreviewById(int supervisorId);

    //Returns a single suporvisor by ID
    Task<DTO.SupervisorDescDTO> ReadSuporvisorDescById(int supervisorId);

    //Returns a single student by ID
    Task<DTO.StudentPreviewDTO> ReadStudentPreviewById(int studentId);

    //Returns a single student by ID
    Task<DTO.StudentDescDTO>ReadStudentDEscById(int studentId);

    //=============================================

    Response UpdateProjectDesciption(int projectId, string newDescription); //Make a check if you are autherized to update

    Response UpdateProjectStatus(int projectId, Status s); //Make a check if you are autherized to update

    //============================================

    Response DeleteProject(int projectId); //Make a check if you are autherized to delete

    Response DeleteSupovisor(int supervisorId);//Make a check if you are autherized to delete

    Response DeleteStudent(int studentId);//Make a check if you are autherized to delete




}
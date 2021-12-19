using Moq;
using Xunit;
using TrialProject.Shared;
using Microsoft.Extensions.Logging;
using TrialProject.Shared.DTO;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrialProject.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System;
using System.Net.Http;

using System.Net;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Server.Tests.Controllers;

public class ProjectControllerTests
{


    public readonly ProjectController controller;
        

    

    public ProjectControllerTests()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<TrialProject.Server.Controllers.DataContext>();
        builder.UseSqlite(connection);
        var context = new TrialProject.Server.Controllers.DataContext(builder.Options);
        context.Database.EnsureCreated();

        var logger = new Mock<ILogger<ProjectController>>();

        Tag Tag1 = new Tag { Name = TagsEnums.Database.ToString() };
        Tag Tag2 = new Tag { Name = TagsEnums.Engineering.ToString() };

        var Project1 = new Project 
        {   
            ID = 1,
            name = "Thesis", 
            shortDescription = "This is a project",
            longDescription = "A very cool project",
            SupervisorID = 1,
            Tags = new List<Tag> { Tag1 }, 
            ProjectStatus = Status.Ongoing
        };

        var Project2 = new Project
        {
            ID = 2,
            name = "Uncool Thesis",
            shortDescription = "This is not a project",
            longDescription = "A very uncool not-project",
            SupervisorID = 2,
            Tags = new List<Tag> { Tag2 },
            ProjectStatus = Status.Ongoing
        };
        var Project3 = new Project
        {
            ID = 3,
            name = "Cooler Thesis",
            shortDescription = "This is another project",
            longDescription = "An unbelievably cool project",
            SupervisorID = 1,
            Tags = new List<Tag> { Tag1 },
            ProjectStatus = Status.Ongoing
        };

        var Supervisor1 = new Supervisor{
            ID = 1,
            name = "Test Testson",
            Email = "testmail@test.com",
            Projects = new List<Project> {Project1, Project3}
        };
        var Supervisor2 = new Supervisor
        {
            ID = 2,
            name = "Not Test Testson",
            Email = "notmail@test.com",
            Projects = new List<Project> { Project2 }
        };

        context.Projects!.Add(Project1);
        context.Projects!.Add(Project2);
        context.Supervisors!.Add(Supervisor1);
        context.Supervisors!.Add(Supervisor2);

        controller = new ProjectController(logger.Object, context);

        context.SaveChanges();
           
    }

    // ==============================================================================================
    // CreateProject
    // ==============================================================================================

    [Fact]
    public async Task Create_Project_Supervisor_Doesnt_Exist_Returns_500()
    {
        //Arrange
        var p = new CreateProjectDTO
        {
            name = "Projecto",
            Supervisor = "",
            shortDescription = "This is a test project",
            longDescription = "A very testy project",
            Tags = new List<TagsEnums> { TagsEnums.Database }
        };

        //Act
        var result = await controller.CreateProject(p);

        //Assert
        var scr = Assert.IsType<StatusCodeResult>(result.Result);
        Assert.Equal(500, scr.StatusCode);
    }

    [Fact]
    public async Task Create_Project_No_Tags_Returns_BadRequest()
    {
        //Arrange
        var p = new CreateProjectDTO
        {
            name = "Projecto",
            Supervisor = "Test Testson",
            shortDescription = "This is a test project",
            longDescription = "A very testy project"
        };

        //Act
        var result = await controller.CreateProject(p);

        //Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public void Create_Project_Returns_Created()
    {
        //Arrange
        var p = new CreateProjectDTO
        {
            name = "Projecto",
            Supervisor = "Test Testson",
            shortDescription = "This is a test project",
            longDescription = "A very testy project",
            Tags = new List<TagsEnums> { TagsEnums.Database }
        };

        var result = controller.CreateProject(p);

        //Assert
        Assert.IsType<CreatedResult>(result.Result.Result);
    }

    [Fact]
    public async Task Create_Project_Is_Created()
    {
        //Arrange
        var p = new CreateProjectDTO
        {
            name = "Projecto",
            Supervisor = "Test Testson",
            shortDescription = "This is a test project",
            longDescription = "A very testy project",
            Tags = new List<TagsEnums> { TagsEnums.Database }
        };
        var expected = new ProjectDescDTO
        {
            ID = 4,
            name = "Projecto",
            SupervisorName = "Test Testson",
            SupervisorEmail = "testmail@test.com",
            shortDescription = "This is a test project",
            longDescription = "A very testy project",
            Tags = new List<string> { TagsEnums.Database.ToString() },
            ProjectStatus = Status.Ongoing
        };

        await controller.CreateProject(p);
        var result = controller.ReadDescProjectById(4).Result.Result;

        //Assert
        var oor = Assert.IsType<OkObjectResult>(result);
        var project = Assert.IsType<ProjectDescDTO>(oor.Value);
        Assert.Equal(expected.ToString(), project.ToString());
    }

    // ==============================================================================================
    // ReadDescProjectByID
    // ==============================================================================================

    [Fact]
    public void ReadDescProjectById_Exists_Returns_ProjectDescDTO()
    {
        //Arrange
        var expected = new ProjectDescDTO
        {
            ID = 1,
            name = "Thesis",
            SupervisorName = "Test Testson",
            SupervisorEmail = "testmail@test.com",
            shortDescription = "This is a project",
            longDescription = "A very cool project",
            Tags = new List<string> { TagsEnums.Database.ToString() },
            ProjectStatus = Status.Ongoing
        };

        //Act
        var result = controller.ReadDescProjectById(1).Result.Result;


        //Assert
        var oor = Assert.IsType<OkObjectResult>(result);
        var project = Assert.IsType<ProjectDescDTO>(oor.Value);
        Assert.Equal(expected.ToString(), project.ToString());
    } 

     [Fact]
    public async Task ReadDescProjectById_Doesnt_Exist_Returns_BadRequest()
    {
        //Arrange
       
        //Act
        var actual = await controller.ReadDescProjectById(-1);
       
        //Assert
        Assert.IsType<BadRequestResult>(actual.Result);
    }

    // ==============================================================================================
    // GetAllProjects
    // ==============================================================================================

    [Fact]
    public void Get_All_Projects_Returns_ProjectPreviewDTOs()
    {
        //Arrange
        var expected1 = new ProjectPreviewDTO
        {
            ID = 1, 
            name = "Thesis", 
            Tags =  new List<string> { TagsEnums.Database.ToString() }, 
            shortDescription = "This is a project", 
            SupervisorName = "Test Testson"
        };
        ProjectPreviewDTO[] exptected = new ProjectPreviewDTO[] { expected1 };
           
        //Act
        var actual = controller.GetAllProjects();
        
        //Assert
        Assert.Equal(exptected.ToString(), actual.ToString());
    }

    // ==============================================================================================
    // ReadAllProjectsPostedBySupervisor
    // ==============================================================================================

    [Fact]
    public void Get_All_Projects_By_Supervisor_Returns_ProjectPreviewDTOs()
    {
        //Arrange
        var expected1 = new ProjectPreviewDTO
        {
            ID = 1,
            name = "Thesis",
            Tags = new List<string> { TagsEnums.Database.ToString() },
            shortDescription = "This is a project",
            SupervisorName = "Test Testson"
        };
        var expected2 = new ProjectPreviewDTO
        {
            ID = 3,
            name = "Cooler Thesis",
            Tags = new List<string> { TagsEnums.Database.ToString() },
            shortDescription = "This is another project",
            SupervisorName = "Test Testson"
        };
        ProjectPreviewDTO[] expected = new ProjectPreviewDTO[] { expected1,expected2 };

        //Act
        var actual = controller.ReadAllProjectsPostedBySupervisor(1) as ProjectPreviewDTO[];
        //var a = actual.Cast<ProjectPreviewDTO>.ToArray();

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected.Length, actual!.Length);
        Assert.Equal(expected[0].ToString(), actual![0].ToString());
        Assert.Equal(expected[1].ToString(), actual![1].ToString());
    }

    [Fact]
    public void Get_All_Projects_By_Supervisor_Doesnt_Exist_Returns_Empty_List()
    {
        //Arrange

        //Act
        var actual = controller.ReadAllProjectsPostedBySupervisor(-1);

        //Assert
        Assert.Null(actual);
    }

    // ==============================================================================================
    // ReadProjectListByTag
    // ==============================================================================================

    [Fact]
    public void Get_Projects_By_Tag_Given_Database_Returns_ProjectPreviewDTOs()
    {
        //Arrange
        var expected1 = new ProjectPreviewDTO
        {
            ID = 1,
            name = "Thesis",
            Tags = new List<string> { TagsEnums.Database.ToString() },
            shortDescription = "This is a project",
            SupervisorName = "Test Testson"
        };
        var expected2 = new ProjectPreviewDTO
        {
            ID = 3,
            name = "Cooler Thesis",
            Tags = new List<string> { TagsEnums.Database.ToString() },
            shortDescription = "This is another project",
            SupervisorName = "Test Testson"
        };
        ProjectPreviewDTO[] expected = new ProjectPreviewDTO[] { expected1, expected2 };

        //Act
        var actual = controller.ReadProjectListByTag("Database") as List<ProjectPreviewDTO>;

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected.Length, actual!.Count);
        Assert.Equal(expected[0].ToString(), actual![0].ToString());
        Assert.Equal(expected[1].ToString(), actual![1].ToString());
    }

    [Fact]
    public void Get_Projects_By_Tag_Given_Engineering_Returns_ProjectPreviewDTOs()
    {
        //Arrange
        var expected1 = new ProjectPreviewDTO
        {
            ID = 2,
            name = "Uncool Thesis",
            shortDescription = "This is not a project",
            Tags = new List<string> { TagsEnums.Engineering.ToString() },
            SupervisorName = "Not Test Testson"

        };
        ProjectPreviewDTO[] expected = new ProjectPreviewDTO[] { expected1 };

        //Act
        var actual = controller.ReadProjectListByTag("Engineering") as List<ProjectPreviewDTO>;

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected.Length, actual!.Count);
        Assert.Equal(expected[0].ToString(), actual![0].ToString());
    }

    [Fact]
    public void Get_Projects_By_Tag_Doesnt_Exist_Returns_Empty_List()
    {
        //Arrange

        //Act
        var actual = controller.ReadProjectListByTag("Nothing") as List<ProjectPreviewDTO>;

        //Assert
        Assert.NotNull(actual);
        Assert.Empty(actual);
    }

    // ==============================================================================================
    // ReadProjectListBySearch
    // ==============================================================================================

    [Fact]
    public void Get_Projects_By_Search_Given_Cool_Returns_ProjectPreviewDTOs()
    {
        //Arrange
        
        var expected1 = new ProjectPreviewDTO
        {
            ID = 1,
            name = "Thesis",
            Tags = new List<string> { TagsEnums.Database.ToString() },
            shortDescription = "This is a project",
            SupervisorName = "Test Testson"
        };
        
        var expected2 = new ProjectPreviewDTO
        {
            ID = 2,
            name = "Uncool Thesis",
            shortDescription = "This is not a project",
            Tags = new List<string> { TagsEnums.Engineering.ToString() },
            SupervisorName = "Not Test Testson"

        };
        var expected3 = new ProjectPreviewDTO
        {
            ID = 3,
            name = "Cooler Thesis",
            Tags = new List<string> { TagsEnums.Database.ToString() },
            shortDescription = "This is another project",
            SupervisorName = "Test Testson"
        };
        ProjectPreviewDTO[] expected = new ProjectPreviewDTO[] { expected1, expected2, expected3 };

        //Act
        var actual = controller.ReadProjectListBySearch("Thesis") as List<ProjectPreviewDTO>;

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected.Length, actual!.Count);
        Assert.Equal(expected[0].ToString(), actual![0].ToString());
        Assert.Equal(expected[1].ToString(), actual![1].ToString());
        Assert.Equal(expected[2].ToString(), actual![2].ToString());
    }

    [Fact]
    public void Get_Projects_By_Search_Given_Engineering_Returns_ProjectPreviewDTOs()
    {
        //Arrange
        var expected1 = new ProjectPreviewDTO
        {
            ID = 2,
            name = "Uncool Thesis",
            shortDescription = "This is not a project",
            Tags = new List<string> { TagsEnums.Engineering.ToString() },
            SupervisorName = "Not Test Testson"

        };
        ProjectPreviewDTO[] expected = new ProjectPreviewDTO[] { expected1 };

        //Act
        var actual = controller.ReadProjectListBySearch("Uncool") as List<ProjectPreviewDTO>;

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected.Length, actual!.Count);
        Assert.Equal(expected[0].ToString(), actual![0].ToString());
    }

    [Fact]
    public void Get_Projects_By_Search_Doesnt_Exist_Returns_Empty_List()
    {
        //Arrange

        //Act
        var actual = controller.ReadProjectListByTag("Nothing") as List<ProjectPreviewDTO>;

        //Assert
        Assert.Empty(actual);
    }

    // ==============================================================================================
    // UpdateProjectDescription
    // ==============================================================================================

    [Fact]
    public void Update_Desc_Project_Doesnt_Exist_Returns_BadRequest()
    {
        //Arrange
        var desc = "Description";

        //Act
        var result = controller.UpdateProjectDesciption(-1,desc);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, result);
    }

    [Fact]
    public void Update_Desc_Returns_OK()
    {
        //Arrange
        var desc = "Description";

        //Act
        var result = controller.UpdateProjectDesciption(1, desc);

        var upd = controller.ReadDescProjectById(1).Result.Result as OkObjectResult;
        var upd1 = upd!.Value as ProjectDescDTO;
        var newDesc = upd1!.longDescription;

        //Assert
        Assert.Equal(HttpStatusCode.OK, result);
        Assert.Equal(desc, newDesc);
    }

    // ==============================================================================================
    // UpdateProjectStatus
    // ==============================================================================================

    [Fact]
    public void Update_Status_Project_Doesnt_Exist_Returns_BadRequest()
    {
        //Arrange

        //Act
        var result = controller.UpdateProjectStatus(-1, Status.Archived);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, result);
    }

    [Fact]
    public void Update_Status_Returns_OK()
    {
        //Arrange

        //Act
        var result = controller.UpdateProjectStatus(1, Status.Archived);

        var upd = controller.ReadDescProjectById(1).Result.Result as OkObjectResult;
        var upd1 = upd!.Value as ProjectDescDTO;

        //Assert
        Assert.Equal(HttpStatusCode.OK, result);
        Assert.Equal(Status.Archived, upd1!.ProjectStatus);
    }



    // ==============================================================================================
    // DeleteProject
    // ==============================================================================================

    [Fact]
    public void Delete_Project_Doesnt_Exist_Returns_InternalServerError()
    {
        //Arrange

        //Act
        var result = controller.DeleteProject(-1);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, result);
    }

    [Fact]
    public void Delete_Supervisor_Exists_Returns_OK()
    {
        //Arrange

        //Act
        var result = controller.DeleteProject(1);

        //Assert
        Assert.Equal(HttpStatusCode.OK, result);
    }

    [Fact]
    public void Delete_Supervisor_Is_Deleted()
    {
        //Arrange

        //Act
        controller.DeleteProject(1);
        var del = controller.ReadDescProjectById(1)!.Result.Result;

        //Assert
        Assert.IsType<BadRequestResult>(del);
    }
}
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

       private readonly TrialProject.Server.Controllers.DataContext? context;

        public readonly ProjectController controller;
        
        private readonly HttpClient client;

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
            var Project1 = new Project {   
                                        ID = 1,
                                        name = "Thesis", 
                                        shortDescription = "This is a project",
                                        longDescription = "A very cool project",
                                        SupervisorID = 1,
                                        Tags = new List<Tag> { Tag1 }, 
                                        ProjectStatus = Status.Ongoing
                                    };
            var Supervisor1 = new Supervisor{
                ID = 1,
                name = "Test Testson",
                Email = "testmail@test.com",
                Projects = new List<Project> {Project1}
            };
            
            context.Projects!.Add(Project1);
            context.Supervisors!.Add(Supervisor1);

            controller = new ProjectController(logger.Object, context);

            context.SaveChanges();
           
            }
    

    [Fact]
    public async Task ReadPreviewProjectById_returns_OkResponse()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        
        //Act
        var actual = await controller.ReadDescProjectById(1);

        //Assert
        Assert.IsType<OkObjectResult>(actual.Result);
    } 
    
  [Fact]
    public async Task ReadPreviewProjectById_returns_Project()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        
        var expected = new ProjectDescDTO{
            ID = 1, 
            name = "Thesis", 
            Tags =  new List<string> { TagsEnums.Database.ToString() }, 
            shortDescription = "This is a project", 
            SupervisorName = "test"
        };
        
        //Act
           var project = controller.ReadDescProjectById(1).Result;
           

        //Assert
        Assert.Equal(project.ToString(), expected.ToString());
    } 

     [Fact]
    public async Task ReadPreviewProjectById_returns_null()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectController>>();
       
        //Act
        var actual = await controller.ReadDescProjectById(-1);
       
        //Assert
       Assert.IsType<BadRequestResult>(actual);
    }

    [Fact]
    public void Get_All_Projects_Returns_Projects()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        
        var expected1 = new ProjectPreviewDTO{
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

    [Fact]
    public async Task Create_Project_Without_Supervisor_returns_bad_request()
    {
       //Arrange
       var Project2 = new CreateProjectDTO {   
                                        name = "Projecto", 
                                        Supervisor = "",
                                        shortDescription = "This is a test project",
                                        longDescription = "A very testy project",
                                        Tags = new List<TagsEnums> { TagsEnums.Database }
                                    };
       
       //Act
       var result = await controller.CreateProject(Project2);
       
       //Assert
       Assert.IsType<StatusCodeResult>(result);
    }

    [Fact]
    public void Create_Project_with_existing_supervisor_returns_accepted()
    {
       //Arrange
       var Project2 = new CreateProjectDTO {   
                                        name = "Projecto", 
                                        Supervisor = "Test Testson",
                                        shortDescription = "This is a test project",
                                        longDescription = "A very testy project",
                                        Tags = new List<TagsEnums> { TagsEnums.Database }
                                    };

       var result = controller.CreateProject(Project2).Result;
       
       //Assert
       Assert.IsType<CreatedAtActionResult>(result);
    }
}
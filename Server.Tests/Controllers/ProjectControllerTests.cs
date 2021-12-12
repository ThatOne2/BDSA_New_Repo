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
using System;

namespace Server.Tests.Controllers;

public class ProjectControllerTests
{

       private readonly TrialProject.Server.Controllers.DataContext? context;

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

            controller = new ProjectController(logger.Object, context);

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
                name = "test",
                Email = "testmail@test.com",
                Projects = new List<Project> {Project1}
            };
            
            context.Projects!.Add(Project1);
            context.Supervisors!.Add(Supervisor1);
        }

    //Fails
    [Fact]
    public async Task ReadPreviewProjectById_returns_Project()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        
        var expected = new ProjectPreviewDTO{
            ID = 1, 
            name = "Thesis", 
            Tags =  new List<string> { TagsEnums.Database.ToString() }, 
            shortDescription = "This is a project", 
            SupervisorName = "test"
        };
       
        
        //Act
        var actual = controller.ReadDescProjectById(1);

        //Assert
        Assert.Equal(actual.ToString(), expected.ToString());
    } 

    //Fails
     [Fact]
    public async Task ReadPreviewProjectById_returns_null()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectController>>();
       
        //Act
        var proj = controller.ReadDescProjectById(-1);
       
        //Assert
       Assert.True(proj.IsFaulted);
    }

    //Fails
    [Fact]
    public void Create_Project_Without_Supervisor_returns_bad_request()
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
       var result = controller.CreateProject(Project2);
       
       //Assert
       Assert.Equal(result, System.Net.HttpStatusCode.BadRequest);
    }

    //Fails
    [Fact]
    public void Create_Project_with_existing_supervisor_returns_accepted()
    {
       //Arrange
       var Supervisor1 = new Supervisor    {
                                                ID = 1,
                                                name = "Frederik Muspelheim",
                                                Email = "asdfyterqwerhjgdf@gmail.com"
                                            };
       var Tag2 = new Tag { Name = "new" };
       var Project2 = new CreateProjectDTO {   
                                        name = "Projecto", 
                                        Supervisor = "Frederik Muspelheim",
                                        shortDescription = "This is a test project",
                                        longDescription = "A very testy project",
                                        Tags = new List<TagsEnums> { TagsEnums.Database }
                                    };
       
       //Act
       try{
       context.Supervisors.Add(Supervisor1);
       } catch (Exception e){
           Console.WriteLine(e.Message);
       }
       var result = controller.CreateProject(Project2);
       
       //Assert
       Assert.Equal(result, System.Net.HttpStatusCode.Accepted);
    }
}
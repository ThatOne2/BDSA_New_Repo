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

        public readonly ProjectController repo;
        Tag Tag1 = new Tag { Name = "fun" };
        

        public ProjectControllerTests()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<TrialProject.Server.Controllers.DataContext>();
            builder.UseSqlite(connection);
            var context = new TrialProject.Server.Controllers.DataContext(builder.Options);
            context.Database.EnsureCreated();

            var logger = new Mock<ILogger<ProjectController>>();

            repo = new ProjectController(logger.Object, context);

             var Project1 = new Project {   
                                        ID = 1,
                                        name = "Thesis", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag1 }, 
                                        shortDescription = "This is a project",
                                        longDescription = "A very cool project"
                                    };

            context.Projects!.Add(Project1);
        }


    [Fact]
    public async Task ReadPreviewProjectById_returns_Project()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        
         var Tag1 = new Tag { Name = "fun" };
        
        var expected = new ProjectPreviewDTO{
            ID = 1, 
            name = "Thesis", 
            Tags =  new List<string> { "fun" }, 
            shortDescription = "This is a project", 
            SupervisorName = null
        };
       
        
        //Act
        var actual = repo.ReadDescProjectById(1);

        //Assert
        Assert.Equal(actual.ToString(), expected.ToString());
    } 

     [Fact]
    public async Task ReadPreviewProjectById_returns_null()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectController>>();
       
        //Act
        var expected = "thing";
        var proj = repo.ReadDescProjectById(-1);
       
        //Assert
       Assert.True(proj.IsFaulted);
    }

    [Fact]
    public void Create_Project_Without_Supervisor_returns_bad_request()
    {
       //Arrange
       var Project2 = new CreateProjectDTO {   
                                        name = "Projecto", 
                                        Supervisor = "",
                                        shortDescription = "This is a test project",
                                        longDescription = "A very testy project",
                                        Tags = new List<string> { "Tag1" }
                                    };
       
       //Act
       var result = repo.CreateProject(Project2);
       
       //Assert
       Assert.Equal(result, System.Net.HttpStatusCode.BadRequest);
    }

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
                                        Tags = new List<string> { "Tag1" }
                                    };
       
       //Act
       try{
       context.Supervisors.Add(Supervisor1);
       } catch (Exception e){
           Console.WriteLine(e.Message);
       }
       var result = repo.CreateProject(Project2);
       
       //Assert
       Assert.Equal(result, System.Net.HttpStatusCode.Accepted);
    }
}
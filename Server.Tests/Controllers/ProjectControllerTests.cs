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

            context.Projects.Add(Project1);
        }


    [Fact]
    public async Task ReadPreviewProjectById_returns_Project()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        
         var Tag1 = new Tag { Name = "fun" };
        
        var expected = new ProjectPreviewDTO{ID = 1, name = "Thesis", Tags =  new List<string> { "fun" }, shortDescription = "This is a project", SupervisorName = null};
       
        
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
}
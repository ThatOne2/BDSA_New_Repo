using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrialProject.Server.Controllers;
using TrialProject.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Server.Tests.Controllers;

public class SupervisorControllerTest
{

       private readonly TrialProject.Server.Controllers.DataContext context;

        public readonly SupervisorsController repo;
 
        

        public SupervisorControllerTest()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<TrialProject.Server.Controllers.DataContext>();
            builder.UseSqlite(connection);
            var context = new TrialProject.Server.Controllers.DataContext(builder.Options);
            context.Database.EnsureCreated();

            var logger = new Mock<ILogger<SupervisorsController>>();

            repo = new SupervisorsController(logger.Object, context);

        }


    [Fact]
    public async Task Create_Supervisor_Already_Exists_Returns_250()
    {
        //Arrange
        var s1 = new CreateSupervisorDTO()
        {
            name = "Sasha",
            Email = "Sasha@email.com"
        };
        var s2 = new CreateSupervisorDTO()
        {
            name = "Emily",
            Email = "Sasha@email.com"
        };

        //Act
        var a = repo.CreateSupervisor(s1);
        var result = repo.CreateSupervisor(s2).Result.Result as ObjectResult;

        //Assert
        Assert.Equal(250, result!.StatusCode);
    }

    [Fact]
    public async Task Create_Supervisor_Proper_Returns_Created()
    {
        //Arrange
        var s = new CreateSupervisorDTO()
        {
            name = "Sasha",
            Email = "Sasha@email.com"
        };

        //Act
        var result = repo.CreateSupervisor(s).Result;

        //Assert
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async Task Create_Supervisor_No_Name_Returns_500()
    {
        //Arrange
        var s = new CreateSupervisorDTO() { Email = "Sasha@email.com" };

        //Act
        //var result = repo.CreateSupervisor(s).Result as StatusCodeResult;
        var result = repo.CreateSupervisor(s).Result.Result as StatusCodeResult;

        //Assert
        Assert.Equal(500, result!.StatusCode);
    }

    [Fact]
    public async Task Create_Supervisor_No_Email_Returns_500()
    {
        //Arrange
        var s = new CreateSupervisorDTO() { name = "Sasha" };

        //Act
        var result = repo.CreateSupervisor(s).Result.Result as StatusCodeResult;

        //Assert
        Assert.Equal(500, result!.StatusCode);
    }
}
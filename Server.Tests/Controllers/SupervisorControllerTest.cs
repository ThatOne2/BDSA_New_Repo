using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrialProject.Server.Controllers;
using TrialProject.Shared.DTO;
using TrialProject.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Server.Tests.Controllers;

public class SupervisorControllerTest
{

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

        var s = new Supervisor
        {
            name = "Martin",
            Email = "Martin@email.com"
        };
        context.Supervisors!.Add(s);

        repo = new SupervisorsController(logger.Object, context);

        context.SaveChanges();
    }

    // ==============================================================================================
    // CreateSupervisor
    // ==============================================================================================

    [Fact]
    public void Create_Supervisor_Already_Exists_Returns_250()
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
    public void Create_Supervisor_Proper_Returns_Created()
    {
        //Arrange
        var s = new CreateSupervisorDTO()
        {
            name = "Sasha",
            Email = "Sasha@email.com"
        };

        //Act
        var result = repo.CreateSupervisor(s).Result.Result;

        //Assert
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public void Create_Supervisor_No_Name_Returns_500()
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
    public void Create_Supervisor_No_Email_Returns_500()
    {
        //Arrange
        var s = new CreateSupervisorDTO() { name = "Sasha" };

        //Act
        var result = repo.CreateSupervisor(s).Result.Result as StatusCodeResult;

        //Assert
        Assert.Equal(500, result!.StatusCode);
    }

    // ==============================================================================================
    // ReadSupervisorDescByID
    // ==============================================================================================

    [Fact]
    public void Read_Supervisor_By_ID_Doesnt_Exist_Returns_BadRequest()
    {
        //Arrange


        //Act
        var result = repo.ReadSupervisorDescById(10)!.Result.Result;

        //Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Read_Supervisor_By_ID_Exists_Returns_SupervisorDescDTO()
    {
        //Arrange
        var expected = new SupervisorDescDTO
        {
            ID = 1,
            name = "Martin",
            Email = "Martin@email.com"
        };

        //Act
        var result = repo.ReadSupervisorDescById(1)!.Result.Result;

        //Assert
        var r = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expected.ToString(), r.Value!.ToString());
    }

    // ==============================================================================================
    // DeleteSupervisor
    // ==============================================================================================

    [Fact]
    public void Delete_Supervisor_Doesnt_Exist_Returns_InternalServerError()
    {
        //Arrange

        //Act
        var result = repo.DeleteSupervisor(10);

        //Assert
        Assert.Equal(System.Net.HttpStatusCode.InternalServerError, result);
    }

    [Fact]
    public void Delete_Supervisor_Exists_Returns_OK()
    {
        //Arrange

        //Act
        var result = repo.DeleteSupervisor(1);

        //Assert
        Assert.Equal(System.Net.HttpStatusCode.OK, result);
    }
}
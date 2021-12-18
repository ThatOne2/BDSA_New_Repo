using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TrialProject.Server.Controllers;
using TrialProject.Shared;
using TrialProject.Shared.DTO;


namespace Server.Tests.Controllers;

public class StudentControllerTests
{

        public readonly StudentsController repo;
 
        

    public StudentControllerTests()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<TrialProject.Server.Controllers.DataContext>();
        builder.UseSqlite(connection);
        var context = new TrialProject.Server.Controllers.DataContext(builder.Options);
        context.Database.EnsureCreated();

        var logger = new Mock<ILogger<StudentsController>>();

        repo = new StudentsController(logger.Object, context);

        var Stundent = new Student{ID = 1, name = "Bru", Email = "Mart@Brumail.com"};

        context.Students!.Add(Stundent);

        context.SaveChanges();

    }

    // ==============================================================================================
    // CreateStudent
    // ==============================================================================================

    [Fact]
    public void Create_Student_Already_Exists_Returns_250()
    {
        //Arrange
        var s1 = new CreateStudentDTO()
        {
            name = "Sasha",
            Email = "Sasha@email.com"
        };
        var s2 = new CreateStudentDTO()
        {
            name = "Emily",
            Email = "Sasha@email.com"
        };

        //Act
        var a = repo.CreateStudent(s1);
        var result = repo.CreateStudent(s2).Result.Result as ObjectResult;

        //Assert
        Assert.Equal(250, result!.StatusCode);
    }

    [Fact]
    public void Create_Student_No_Name_Returns_500()
    {
        //Arrange
        var s = new CreateStudentDTO() { Email = "Sasha@email.com" };

        //Act
        var result = repo.CreateStudent(s).Result.Result as StatusCodeResult;

        //Assert
        Assert.Equal(500, result!.StatusCode);
    }

    [Fact]
    public void Create_Student_No_Email_Returns_500()
    {
        //Arrange
        var s = new CreateStudentDTO() { name = "Sasha" };

        //Act
        var result = repo.CreateStudent(s).Result.Result as StatusCodeResult;

        //Assert
        Assert.Equal(500, result!.StatusCode);
    }

    [Fact]
    public void Create_Student_Returns_Created()
    {
        //Arrange
        var s = new CreateStudentDTO()
        {
            name = "Sasha",
            Email = "Sasha@email.com"
        };

        //Act
        var result = repo.CreateStudent(s).Result.Result;

        //Assert
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async Task Create_Student_Is_Created()
    {
        //Arrange
        var s = new CreateStudentDTO()
        {
            name = "Sasha",
            Email = "Sasha@email.com"
        };
        var expected = new StudentDescDTO()
        {
            ID = 2,
            name = "Sasha",
            Email = "Sasha@email.com"
        };

        //Act
        await repo.CreateStudent(s);
        var result = repo.ReadStudentDescById(2)!.Result.Result;

        //Assert
        var r = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expected.ToString(), r.Value!.ToString());
    }

    // ==============================================================================================
    // ReadStudentDescByID
    // ==============================================================================================

    [Fact]
    public void Read_Student_By_ID_Doesnt_Exist_Returns_BadRequest()
    {
        //Arrange


        //Act
        var result = repo.ReadStudentDescById(10)!.Result.Result;

        //Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Read_Student_By_ID_Exists_Returns_StudentDescDTO()
    {
        //Arrange
        var expected = new StudentDescDTO
        {
            ID = 1,
            name = "Bru",
            Email = "Mart@Brumail.com"
        };

        //Act
        var result = repo.ReadStudentDescById(1)!.Result.Result;

        //Assert
        var r = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expected.ToString(), r.Value!.ToString());
    }

    // ==============================================================================================
    // DeleteStudent
    // ==============================================================================================

    [Fact]
    public void Delete_Student_Doesnt_Exist_Returns_InternalServerError()
    {
        //Arrange

        //Act
        var result = repo.DeleteStudent(10);

        //Assert
        Assert.Equal(System.Net.HttpStatusCode.InternalServerError, result);
    }

    [Fact]
    public void Delete_Student_Exists_Returns_OK()
    {
        //Arrange

        //Act
        var result = repo.DeleteStudent(1);

        //Assert
        Assert.Equal(System.Net.HttpStatusCode.OK, result);
    }

    [Fact]
    public void Delete_Student_Is_Deleted()
    {
        //Arrange

        //Act
        repo.DeleteStudent(1);
        var del = repo.ReadStudentDescById(1)!.Result.Result;

        //Assert
        Assert.IsType<BadRequestResult>(del);
    }
}
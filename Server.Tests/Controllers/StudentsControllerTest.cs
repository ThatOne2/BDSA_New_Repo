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

        [Fact]
    public void Read_Student_by_Id_Given_Negative_Returns_Bad_Request()
    {
        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();

        //Act
        var result = repo.ReadStudentDescById(-1).Result.Result;

        //Assert
        Assert.IsType<BadRequestResult>(result);
    } 

    [Fact]
    public void Student_Reading_By_ID(){

        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();
        var expected = new StudentDescDTO{ID = 1, name = "Bru", Email = "Mart@Brumail.com"};

        //Act
        var project = repo.ReadStudentDescById(1).Result.Result as OkObjectResult;
        var actual = project!.Value;

        //Assert
        Assert.Equal(expected.ToString(), actual!.ToString());

    }

    
}
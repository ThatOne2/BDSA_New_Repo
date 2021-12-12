using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrialProject.Server.Controllers;

namespace Server.Tests.Controllers;

public class SupervisorsControllerTest
{

       private readonly TrialProject.Server.Controllers.DataContext context;

        public readonly SupervisorsController repo;
 
        

        public SupervisorsControllerTest()
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
    public async Task Test()
    {
        //Arrange
    
        
        //Act
   

        //Assert
        //Assert.Equal(1,2);
    } 

    
}
using Moq;
using Xunit;
using TrialProject.Server;
using TrialProject.Shared;
using System;
using Microsoft.Extensions.Logging;
using TrialProject.Shared.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Tests.Controllers;

public class SupervisorsControllerTest
{

       private readonly DataContext context;

        public readonly SupervisorController repo;
 
        

        public SupervisorsControllerTest()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlite(connection);
            var context = new DataContext(builder.Options);
            context.Database.EnsureCreated();

            var logger = new Mock<ILogger<SupervisorController>>();

            repo = new SupervisorController(logger.Object, context);

        }


    [Fact]
    public async Task Test()
    {
        //Arrange
    
        
        //Act
   

        //Assert
      
    } 

    
}
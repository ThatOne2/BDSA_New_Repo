using Moq;
using Xunit;
using TrialProject.Server;
using TrialProject.Shared;
using System;
using Microsoft.Extensions.Logging;
using TrialProject.Shared.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Server.Tests.Controllers;

public class ProjectControllerTests
{
    [Fact]
    public async Task Get_returns_Projects_from_repoAsync()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        var expected = new List<ProjectPreviewDTO>();
        var newExptected = new ReadOnlyCollection<ProjectPreviewDTO>(expected); 
        var context = new Mock<DataContext>();
        var controller = new ProjectController(logger.Object, context.Object);
        
        //Act
        var actual = await controller.GetAllProjects();

        //Assert
        Assert.Equal(actual, newExptected);
    }
}
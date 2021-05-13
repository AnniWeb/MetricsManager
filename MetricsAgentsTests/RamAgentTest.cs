using MetricsAgent.Controller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using MetricsAgent.Repository;
using MetricsAgent.Request;
using MetricsAgent.Response;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace TestMetrics
{
    public class RamAgentTest
    {
        private RamMetricsAgentController _controller;
        private readonly Mock<IRamMetricsRepository> _mock;

        public RamAgentTest()
        {
            _mock = new Mock<IRamMetricsRepository>();
            _controller = new RamMetricsAgentController(new Mock<ILogger<RamMetricsAgentController>>().Object, _mock.Object);
        }

        [Fact]
        public void TestCreate_ReturnOk()
        {
            //Arrange
            var rand = new Random();
            var newMetric = new RamMetricRequest()
            {
                Time = DateTimeOffset.Now.AddMinutes(-rand.Next(0, 60)),
                Value = rand.Next()
            };
            
            //Act
            var result = _controller.Create(newMetric);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void TestGetList_ReturnList()
        {
            //Arrange
            _mock.Setup(repo => repo.GetByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<RamMetricsModel>());
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            
            //Act
            var result = _controller.GetList(fromTime, toTime);

            // Assert
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<ListRamMetricsResponse>(okResult.Value);
        }
        
        [Fact]
        public void TestGetList_ReturnCount()
        {
            //Arrange
            
            //Act
            var result = _controller.GetAvailable();

            // Assert
            _ = Assert.IsAssignableFrom<ActionResult<double>>(result);
        }
    }
}
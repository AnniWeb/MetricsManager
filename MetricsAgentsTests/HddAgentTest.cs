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
    public class HddAgentTest
    {
        private HddMetricsAgentController _controller;
        private readonly Mock<IHddMetricsRepository> _mock;

        public HddAgentTest()
        {
            _mock = new Mock<IHddMetricsRepository>();
            _controller = new HddMetricsAgentController(new Mock<ILogger<HddMetricsAgentController>>().Object, _mock.Object);
        }
        
        [Fact]
        public void TestCreate_ReturnOk()
        {
            //Arrange
            var rand = new Random();
            var newMetric = new HddMetricsRequest()
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
            _mock.Setup(repo => repo.GetByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<HddSpaceMetricsModel>());
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            
            //Act
            var result = _controller.GetList(fromTime, toTime);

            // Assert
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<ListHddSpaceMetricsResponse>(okResult.Value);
        }

        [Fact]
        public void TestGetLeftSpace_ReturnCount()
        {
            //Arrange
            
            //Act
            var result = _controller.GetLeftSpace();

            // Assert
            _ = Assert.IsAssignableFrom<ActionResult<double>>(result);
        }
    }
}
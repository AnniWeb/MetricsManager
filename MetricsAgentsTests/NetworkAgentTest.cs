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
    public class NetworkAgentTest
    {
        private NetworkMetricsAgentController _controller;
        private readonly Mock<INetworkMetricsRepository> _mock;

        public NetworkAgentTest()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            _controller = new NetworkMetricsAgentController(new Mock<ILogger<NetworkMetricsAgentController>>().Object, _mock.Object);
        }
        
        [Fact]
        public void TestCreate_ReturnOk()
        {
            //Arrange
            var rand = new Random();
            var newMetric = new NetworkMetricRequest()
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
            _mock.Setup(repo => repo.GetByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<NetworkMetricsModel>());
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            
            //Act
            var result = _controller.GetList(fromTime, toTime);

            // Assert
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<ListNetworkMetricsResponse>(okResult.Value);
        }
    }
}
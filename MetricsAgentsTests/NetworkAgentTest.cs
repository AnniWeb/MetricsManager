using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsAgent.DAL.Model;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Rest.Response;
using MetricsAgent.Rest.Controller;
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
            var logger = new Mock<ILogger<NetworkMetricsAgentController>>();
            var mapper = new Mock<IMapper>();
            _controller = new NetworkMetricsAgentController(logger.Object, _mock.Object, mapper.Object);
        }
        
        [Fact]
        public void TestGetList_ReturnList()
        {
            //Arrange
            _mock.Setup(repo => repo.GetByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<NetworkMetric>());
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            
            //Act
            var result = _controller.GetByPeriod(fromTime, toTime);

            // Assert
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<ListNetworkMetricsResponse>(okResult.Value);
        }
    }
}
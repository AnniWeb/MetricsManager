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
    public class RamAgentTest
    {
        private RamMetricsAgentController _controller;
        private readonly Mock<IRamMetricsRepository> _mock;

        public RamAgentTest()
        {
            _mock = new Mock<IRamMetricsRepository>();
            var logger = new Mock<ILogger<RamMetricsAgentController>>();
            var mapper = new Mock<IMapper>();
            _controller = new RamMetricsAgentController(logger.Object, _mock.Object, mapper.Object);
        }
        
        [Fact]
        public void TestGetList_ReturnList()
        {
            //Arrange
            _mock.Setup(repo => repo.GetByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<RamMetric>());
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            
            //Act
            var result = _controller.GetByPeriod(fromTime, toTime);

            // Assert
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<ListRamMetricsResponse>(okResult.Value);
        }
    }
}
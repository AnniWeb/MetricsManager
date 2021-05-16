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
    public class DotNetAgentTest
    {
        private DotNetMetricsAgentController _controller;
        private readonly Mock<IDotNetMetricsRepository> _mock;

        public DotNetAgentTest()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            var logger = new Mock<ILogger<DotNetMetricsAgentController>>();
            var mapper = new Mock<IMapper>();
            _controller = new DotNetMetricsAgentController(logger.Object, _mock.Object, mapper.Object);
        }
        
        [Fact]
        public void TestGetList_ReturnList()
        {
            //Arrange
            _mock.Setup(repo => repo.GetByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<DotNetMetric>());
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            
            //Act
            var result = _controller.GetByPeriod(fromTime, toTime);

            // Assert
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<ListDotNetMetricsResponse>(okResult.Value);
        }
    }
}
﻿using MetricsAgent.Controller;
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
    public class DotNetAgentTest
    {
        private DotNetMetricsAgentController _controller;
        private readonly Mock<IDotNetMetricsRepository> _mock;

        public DotNetAgentTest()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            _controller = new DotNetMetricsAgentController(new Mock<ILogger<DotNetMetricsAgentController>>().Object, _mock.Object);
        }

        [Fact]
        public void TestCreate_ReturnOk()
        {
            //Arrange
            var rand = new Random();
            var newMetric = new DotNetMetricRequest()
            {
                Time = DateTimeOffset.Now.AddMinutes(-rand.Next(0, 60)),
                Value = "Error"
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
            _mock.Setup(repo => repo.GetByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<DotNetMetricsModel>());
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            
            //Act
            var result = _controller.GetList(fromTime, toTime);

            // Assert
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<ListDotNetMetricsResponse>(okResult.Value);
        }
        
        [Fact]
        public void TestGetErrorsCount_ReturnCount()
        {
            //Arrange
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            
            //Act
            var result = _controller.GetErrorsCount(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<ActionResult<int>>(result);
        }
    }
}
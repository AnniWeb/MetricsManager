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
    public class CPUAgentTest
    {
        private readonly CPUMetricsAgentController _controller;
        private readonly Mock<ICPUMetricsRepository> _mock;

        public CPUAgentTest()
        {
            _mock = new Mock<ICPUMetricsRepository>();
            _controller = new CPUMetricsAgentController(new Mock<ILogger<CPUMetricsAgentController>>().Object, _mock.Object);
        }

        [Fact]
        public void TestCreate_ReturnOk()
        {
            //Arrange
            _mock.Setup(repo => repo.Create(It.IsAny<CPUMetricsModel>())).Verifiable();
            var rand = new Random();
            var newMetric = new CPUMetricRequest()
            {
                Time = DateTimeOffset.Now.AddMinutes(-rand.Next(0, 60)),
                Value = rand.Next()
            };
            
            //Act
            var result = _controller.Create(newMetric);

            // Assert
            _mock.Verify(repo => repo.Create(It.IsAny<CPUMetricsModel>()), Times.AtMostOnce);
        }
        
        [Fact]
        public void TestGetList_ReturnList()
        {
            //Arrange
            _mock.Setup(repo => repo.GetByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<CPUMetricsModel>());
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            
            //Act
            var result = _controller.GetList(fromTime, toTime);

            // Assert
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<ListCPUMetricsResponse>(okResult.Value);
        }
        
        public void TestGetListWithPercentile_ReturnList()
        {
            //Arrange
            _mock.Setup(repo => repo.GetByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<CPUMetricsModel>());
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            var percentile = Percentile.Median;
            
            //Act
            var result = _controller.GetList(fromTime, toTime, percentile);

            // Assert
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<ListCPUMetricsResponse>(okResult.Value);
        }
    }
}
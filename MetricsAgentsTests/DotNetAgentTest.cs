using MetricsAgent.Controller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using Xunit;

namespace TestMetrics
{
    public class DotNetAgentTest
    {
        private DotNetMetricsAgentController _controller;

        public DotNetAgentTest()
        {
            _controller = new DotNetMetricsAgentController();
        }

        [Fact]
        public void TestGetErrorsCount_ReturnCount()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            
            //Act
            var result = _controller.GetErrorsCount(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<ActionResult<int>>(result);
        }
    }
}
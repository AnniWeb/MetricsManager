using MetricsAgent.Controller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using Xunit;

namespace TestMetrics
{
    public class RamAgentTest
    {
        private RamMetricsAgentController _controller;

        public RamAgentTest()
        {
            _controller = new RamMetricsAgentController();
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
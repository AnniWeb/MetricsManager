using MetricsAgent.Controller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using Xunit;

namespace TestMetrics
{
    public class HddAgentTest
    {
        private HddMetricsAgentController _controller;

        public HddAgentTest()
        {
            _controller = new HddMetricsAgentController();
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
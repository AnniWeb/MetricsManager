using MetricsAgent.Controller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using Xunit;

namespace TestMetrics
{
    public class CPUAgentTest
    {
        private readonly CPUMetricsAgentController _controller;

        public CPUAgentTest()
        {
            _controller = new CPUMetricsAgentController();
        }

        [Fact]
        public void TestGetList_ReturnList()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            
            //Act
            var result = _controller.GetList(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<CPUMetricsModel>>(result);
        }
        
        [Fact]
        public void TestGetListWithPercentile_ReturnList()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var percentile = Percentile.Median;
            
            //Act
            var result = _controller.GetList(fromTime, toTime, percentile);

            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<CPUMetricsModel>>(result);
        }
    }
}
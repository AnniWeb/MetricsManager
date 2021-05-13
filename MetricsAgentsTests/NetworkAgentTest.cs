using MetricsAgent.Controller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using Xunit;

namespace TestMetrics
{
    public class NetworkAgentTest
    {
        private NetworkMetricsAgentController _controller;

        public NetworkAgentTest()
        {
            _controller = new NetworkMetricsAgentController();
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
            _ = Assert.IsAssignableFrom<IEnumerable<NetworkMetricsModel>>(result);
        }
    }
}
﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Model
{
    public class RamMetrics : IMetricModel<double>
    {
        public int Id { get; set; }
        
        [Required]
        public double Value { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}
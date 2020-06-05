using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Models
{
    public class JsonResultModel
    {

        public ICollection<string> Points { get; set; }
        public ICollection<string> Peaks { get; set; }

        public ICollection<double> CorelationResult { get; set; }

        public JsonResultModel()
        {
            this.Points = new List<string>();
            this.Peaks = new List<string>();
            this.CorelationResult = new List<double>();
        }
    }
}

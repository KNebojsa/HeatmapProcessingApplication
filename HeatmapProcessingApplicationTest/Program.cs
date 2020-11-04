using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatmapProcessingApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var processor = new HeatmapProcessorTests();
            processor.ProcessImage();
        }
    }
}

using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Models
{
    public class TfidfTransformedData
    {
        [VectorType(100)]
        public float[] materialName { get; set;}
    }
}

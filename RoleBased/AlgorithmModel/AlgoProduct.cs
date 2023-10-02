using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Models
{
    public class AlgoProduct
    {
        public int Id { get; set; }
        public string materialName { get; set; }

        //token for description
        public float[] CombinedTFIDVector { get; set; }
    }
}

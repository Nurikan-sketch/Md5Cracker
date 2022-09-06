using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Md5Cracker.Models
{
    public class WorkerState
    {
        public bool IsFound { get; set; }
        public DateTime StartAt { get; set; }
        public string Hash { get; set; }
        public string Symbols { get; set; }
        public int Lenght { get; set; }
        public int VariantCount { get; set; }
        public int ThreadNum { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Md5Cracker.Models
{
    public class Worker
    {
        public bool AnswerFound { get; set; }

        public delegate string Hash(string s);

        public Hash HashMethod { get; set; } = Hasher.MD5;

        public WorkerState State { get; set; }

        private VariantGenerator _generator;

        public int Done { get; set; } = 0;

        public string Answer { get; set; } = String.Empty;

        private int _counter = 0;

        private IEnumerator<string> enumerator;

        public void Tick()
        {
            if (enumerator.MoveNext()) {

                var variant = enumerator.Current;
                var valid = HashMethod(variant) == State.Hash;


                if (valid)
                {
                    AnswerFound = true;
                    Answer = variant;
                }
                _counter++;
                Done = (int)(((float)_counter / State.VariantCount) * 100);
            }
            
        }

        public void Init()
        {
            Answer = String.Empty;
            Done = 0;
            _counter = 0;

            _generator = new VariantGenerator(State.Symbols, State.Lenght)
                .Skip(State.VariantCount * State.ThreadNum)
                .Take(State.VariantCount);

            enumerator = _generator.GetEnumerator();
        }
    }
}

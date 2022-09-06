using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Md5Cracker.Models
{
    public class VariantGenerator : IEnumerable<string>
    {
        public VariantGenerator(string symbols, int lenght)
        {
            Symbols = symbols;
            SymbolsCount = Symbols.Length;
            Lenght = lenght;
            indexes = new int[lenght];
            for (int i = 0; i < Lenght; i++)
            {
                indexes[i] = 0;
            }
        }

        protected string Symbols { get; set; }
        protected int Lenght { get; set; }
        protected int SymbolsCount { get; set; }
        protected int MaxVariantCount { get; set; } = 0;
        protected int[] indexes;

        public int TotalVariantsCount => (int)Math.Pow(Symbols.Length, Lenght);

        public IEnumerator<string> GetEnumerator()
        {
            return GetVariants().GetEnumerator();
        }

        public VariantGenerator Skip(int count)
        {
            for (int j = 0; j < count; j++)
            {
                for (int i = Lenght - 1; i >= 0; i--)
                {
                    indexes[i]++;
                    if (indexes[i] >= SymbolsCount)
                    {
                        indexes[i] = 0;
                        continue;
                    }
                    break;
                }
            }

            return this;
        }

        public VariantGenerator Take(int count)
        {
            MaxVariantCount = count;
            return this;
        }

        protected IEnumerable<string> GetVariants()
        {
            int count = 0;
            do
            {
                var sb = new StringBuilder();
                for (int i = 0; i < Lenght; i++)
                {
                    sb.Append(Symbols[indexes[i]]);
                }
                count++;
                yield return sb.ToString();

                for (int i = Lenght - 1; i >= 0; i--)
                {
                    indexes[i]++;
                    if (indexes[i] >= SymbolsCount)
                    {
                        indexes[i] = 0;
                        continue;
                    }
                    break;
                }

            } while (count < MaxVariantCount);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }
}

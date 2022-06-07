using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Huffman_method
{
    class Program
    {
        static double AvWordLen(Sentence s)
        {
            var mas = s.messege.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            double length = 0;
            double count = 0;
            for (int i = 0; i < mas.Length; i++)
            {
                length += mas[i].Length;
                count++;
            }
            return length / count;
        }
        public class Sentence
        {

            public string messege { get; set; }
            public List<char> alphabet
            {
                get
                {
                    List<char> mes = new List<char> { };

                    for (int i = 0; i < messege.Length; i++)
                    {
                        mes.Add(messege[i]);
                    }
                    IEnumerable<char> alfhabet1 = mes.Distinct();
                    List<char> alphabet = alfhabet1.ToList();
                    return alphabet;
                }
            }
            public int numSymbols => messege.Length;
            public int powerOfAlphabet => alphabet.Count;
            public double avLenWord => messege.Length / CountWords();
            public Sentence(string mes)
            {
                messege = mes;
            }

            public int CountWords()
            {
                int count = 0;
                for (int i = 0; i < messege.Length && i != messege.Length - 1; i++)
                {
                    if (messege[i] == ' ')
                    {
                        count++;
                    }
                }
                return count;
            }
            public string[,] frequency
            {
                get
                {
                    double countSymb = 0;
                    string[,] fr = new string[alphabet.Count, 2];
                    for (int i = 0; i < alphabet.Count; i++)
                    {
                        for (int j = 0; j < messege.Length; j++)
                        {
                            if (alphabet[i] == messege[j])
                                countSymb++;
                        }
                        fr[i, 0] = alphabet[i].ToString();
                        fr[i, 1] = (countSymb).ToString();
                        countSymb = 0;
                    }
                    return fr;
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введiть повiдомлення:");
            string input = Console.ReadLine();
            HuffmanTree huffmanTree = new HuffmanTree();

            // Build the Huffman tree
            huffmanTree.Build(input);

            // Encode
            BitArray encoded = huffmanTree.Encode(input);

            Console.Write("Закодовано: ");
            foreach (bool bit in encoded)
            {
                Console.Write((bit ? 1 : 0));
            }
            Console.WriteLine();

            // Decode
            string decoded = huffmanTree.Decode(encoded);

            Console.WriteLine("Декодовано: \n" + decoded + "\b\b" + "i.");

            double AvLenCodeComb = 0;
            double allinf = 0;
            double enthropy = 0;
            double fr;
            string[,] frequency;

            Sentence sen = new Sentence(input);
            frequency = sen.frequency;

            Console.WriteLine("Символ        Частота        Кол-во информации");
            for (int i = 0; i < sen.powerOfAlphabet; i++)
            {
                fr = Convert.ToDouble(frequency[i, 1]) / sen.numSymbols;
                allinf += Convert.ToDouble(frequency[i, 1]) * -1 * fr * Math.Log(fr, 2);
                enthropy += -1 * fr * Math.Log(fr, 2);
                AvLenCodeComb += 11 * fr;

                Console.WriteLine($"{frequency[i, 0]}               {frequency[i, 1]}               {Math.Round(-1 * fr * Math.Log(fr, 2), 2)}");
            }
            Console.WriteLine($"Энтропия: {Math.Round(enthropy, 2).ToString()}");
            Console.WriteLine($"Общее кол-во информации: {Math.Round(allinf, 2).ToString()}");
            Console.WriteLine($"Средняя длина слова: {Math.Round(AvWordLen(sen), 2).ToString()}");
            Console.WriteLine($"Коэф сжатия: {((double)Math.Log(sen.powerOfAlphabet, 2) / enthropy).ToString()}");
            Console.WriteLine($"Коэф еффктивности: {(enthropy / AvLenCodeComb).ToString()}");// энтропия / средняя длина кодовой комбинации
            Console.ReadLine();
        }
    }
}

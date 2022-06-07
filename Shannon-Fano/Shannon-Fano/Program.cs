/*Щаслива людина занадто задоволена сьогоденням, щоб занадто багато думати про майбутнє.*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shannon_Fano
{
    class Program
    {
        static void ShakerSort(List<KeyValuePair<double, char>> List)
        {
            int beg, end;
            int count = 0;

            for (int i = 0; i < List.Count / 2; i++)
            {
                beg = 0;
                end = List.Count - 1;

                do
                {
                    count += 2;
                    if (List[beg].Key < List[beg + 1].Key)
                        Swap(List, beg, beg + 1);
                    beg++;


                    if (List[end - 1].Key < List[end].Key)
                        Swap(List, end - 1, end);
                    end--;

                }
                while (beg <= end);
            }
        }

        static void Swap(List<KeyValuePair<double, char>> List, int i, int j)
        {
            KeyValuePair<double, char> temp;
            temp = List[i];
            List[i] = List[j];
            List[j] = temp;
        }

        static void WriteArray(List<KeyValuePair<double, char>> List)
        {
            foreach (KeyValuePair<double, char> a in List)
                Console.WriteLine("{0}: {1}", a.Value, a.Key);
            Console.WriteLine("\n\n\n");
        }

        static int Dividing(List<KeyValuePair<double, char>> List, int L, int R)
        {
            double HalfProb = 0, PieceProb = 0;
            for (int j = L; j <= R; j++)
            {
                HalfProb += List[j].Key;
            }

            HalfProb /= 2;

            int i = 0;
            for (i = L; PieceProb < HalfProb && i <= R; i++)
            {
                PieceProb += List[i].Key;
            }

            return i - 1;
        }

        static void Fano(List<KeyValuePair<double, char>> Pairs, Dictionary<char, string> Codes, int L, int R)
        {
            try
            {
                if (R - L > 0)
                {
                    int index = Dividing(Pairs, L, R);

                    for (int i = L; i <= index; i++)
                    {
                        if (Codes.ContainsKey(Pairs[i].Value))
                            Codes[Pairs[i].Value] += "1";
                        else
                            Codes.Add(Pairs[i].Value, "1");
                    }

                    for (int i = index + 1; i <= R; i++)
                    {
                        if (Codes.ContainsKey(Pairs[i].Value))
                            Codes[Pairs[i].Value] += "0";
                        else
                            Codes.Add(Pairs[i].Value, "0");
                    }

                    Fano(Pairs, Codes, L, index);
                    Fano(Pairs, Codes, index + 1, R);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите Сообщение:");
            string message = Console.ReadLine();
            Dictionary<char, int> CharsAndProbs = new Dictionary<char, int>(); //считаем сюда сколько раз попадается каждый символ
            List<KeyValuePair<double, char>> List = new List<KeyValuePair<double, char>>();//Массив с парами вероятность&символ
            Dictionary<char, string> Codes = new Dictionary<char, string>(); //Тут результат работы алгоритма

            int MessageLength = 0;
            foreach (char a in message)
            {
                MessageLength++;
                if (CharsAndProbs.ContainsKey(a))
                    CharsAndProbs[a]++;
                else
                    CharsAndProbs.Add(a, 1);
            }

            foreach (KeyValuePair<char, int> a in CharsAndProbs)
            {
                List.Add(new KeyValuePair<double, char>(a.Value, a.Key));
            }
            ShakerSort(List);

            WriteArray(List);
            Fano(List, Codes, 0, List.Count - 1);

            foreach (KeyValuePair<char, string> a in Codes)
                Console.WriteLine("{0}: {1}", a.Key, a.Value);

            string Code = "";
            foreach (char a in message)
            {
                Code += Codes[a];
            }
            Console.WriteLine("\nКод:\n" + Code);

            string Decoded = "";
            for (int i = 0; i < Code.Length; i++)
            {
                foreach (KeyValuePair<char, string> a in Codes)
                {
                    try
                    {
                        if (Code.Substring(i, a.Value.Length) == a.Value)
                        {
                            Decoded += a.Key;
                            i += a.Value.Length - 1;
                            break;
                        }
                    }
                    catch { }
                }
            }

            Console.WriteLine("\nДекодировка:\n" + Decoded + "\b\b" + "i.");

            double AvLenCodeComb = 0;
            double allinf = 0;
            double enthropy = 0;
            double fr;
            string[,] frequency;

            Sentence sen = new Sentence(message);
            frequency = sen.frequency;

            Console.WriteLine("Символ        Частота        Кол-во информации");
            for (int i = 0; i < sen.powerOfAlphabet; i++)
            {
                fr = Convert.ToDouble(frequency[i, 1]) / sen.numSymbols;
                allinf += Convert.ToDouble(frequency[i, 1]) * -1 * fr * Math.Log(fr, 2);
                enthropy += -1 * fr * Math.Log(fr, 2);
                AvLenCodeComb += 11 * fr;

                Console.WriteLine($"{frequency[i, 0]}               {frequency[i, 1]}                {Math.Round(-1 * fr * Math.Log(fr, 2), 2)}");
            }
            Console.WriteLine($"Энтропия: {Math.Round(enthropy, 2).ToString()}");
            Console.WriteLine($"Общее кол-во информации: {Math.Round(allinf, 2).ToString()}");
            Console.WriteLine($"Средняя длина слова: {Math.Round(AvWordLen(sen), 2).ToString()}");
            Console.WriteLine($"Коэф еффктивности: {((double)Math.Log(sen.powerOfAlphabet, 2) / enthropy).ToString()}");
            Console.WriteLine($"Коэф сжатия: {(enthropy / AvLenCodeComb).ToString()}");// энтропия / средняя длина кодовой комбинации
            Console.ReadKey();
        }

        static double AvWordLen(Sentence s)
        {
            var mas = s.messege.Split(new char[] { ' ', ',', '.'}, StringSplitOptions.RemoveEmptyEntries);
            int length = 0;
            int count = 0;
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
    }
}

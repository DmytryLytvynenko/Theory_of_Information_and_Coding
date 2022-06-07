using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LZWAlgorithms;
using System.IO;

namespace LZW
{
    public partial class Form1 : Form
    {

        private Dictionary<int, string> dictionary;
        private List<int> indices;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            LZWCompressor comp = new LZWCompressor();
            indices = new List<int>();
            dictionary = comp.Compressor(text, ref indices);
            foreach (int index in indices)
            {
                textBox2.Text += index.ToString() + "";
            }

            double AvLenCodeComb = 0;
            double allinf = 0;
            double enthropy = 0;
            double fr;
            string s = "";
            string[,] frequency;

            s = textBox1.Text;
            Sentence sen = new Sentence(s);
            frequency = sen.frequency;

            for (int i = 0; i < sen.powerOfAlphabet; i++)
            {
                fr = Convert.ToDouble(frequency[i, 1]) / sen.numSymbols;
                allinf += Convert.ToDouble(frequency[i, 1]) * -1 * fr * Math.Log(fr, 2);
                enthropy += -1 * fr * Math.Log(fr, 2);
                AvLenCodeComb += 5 * fr;

                dataGridView1.Rows.Add(frequency[i, 0], frequency[i, 1], Math.Round(-1 * fr * Math.Log(fr, 2), 2));
            }
            label11.Text = Math.Round(enthropy, 2).ToString();
            label3.Text = Math.Round(allinf, 2).ToString();
            label5.Text = Math.Round(AvWordLen(sen), 2).ToString();
            label7.Text = ((double)Math.Log(sen.powerOfAlphabet, 2) / enthropy).ToString();
            double a = (double)text.Length / ((double)textBox2.Text.Length - (double)textBox2.Text.Length/4);
/*            label9.Text = a.ToString();  // энтропия / средняя длина кодовой комбинации*/


        }




        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dictionary != null && indices != null)
            {
                LZWDecompressor dec = new LZWDecompressor();
                textBox2.Text += dec.Decompressor(dictionary, indices);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string temp = textBox2.Text;
            textBox1.Clear();
            textBox1.Text = temp;
            textBox2.Text = string.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Выберите файл";
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            StreamReader r = new StreamReader(openFileDialog1.FileName);
            textBox1.Text = r.ReadToEnd();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog2.Title = "Выберите файл";
            if (openFileDialog2.ShowDialog() != DialogResult.OK) return;
            File.WriteAllText(openFileDialog2.FileName, textBox2.Text);
        }

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
        /* public double avLenWord => messege.Length / CountWords();*/
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

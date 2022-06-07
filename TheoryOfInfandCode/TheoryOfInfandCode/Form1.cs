using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TheoryOfInfandCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[,] Baudot = new string[26, 2];

        string[,] ASCII = new string[2048, 2];

        private void Form1_Load(object sender, EventArgs e)
        {
            Baudot[0, 0] = "A";
            Baudot[0, 1] = "00011";
            Baudot[1, 0] = "B";
            Baudot[1, 1] = "11001";
            Baudot[2, 0] = "C";
            Baudot[2, 1] = "01110";
            Baudot[3, 0] = "D";
            Baudot[3, 1] = "01001";
            Baudot[4, 0] = "E";
            Baudot[4, 1] = "00001";
            Baudot[5, 0] = "F";
            Baudot[5, 1] = "01101";
            Baudot[6, 0] = "G";
            Baudot[6, 1] = "11010";
            Baudot[7, 0] = "H";
            Baudot[7, 1] = "10100";
            Baudot[8, 0] = "I";
            Baudot[8, 1] = "00110";
            Baudot[9, 0] = "J";
            Baudot[9, 1] = "01011";
            Baudot[10, 0] = "K";
            Baudot[10, 1] = "01111";
            Baudot[11, 0] = "L";
            Baudot[11, 1] = "10010";
            Baudot[12, 0] = "M";
            Baudot[12, 1] = "11100";
            Baudot[13, 0] = "N";
            Baudot[13, 1] = "01100";
            Baudot[14, 0] = "O";
            Baudot[14, 1] = "11000";
            Baudot[15, 0] = "P";
            Baudot[15, 1] = "10110";
            Baudot[16, 0] = "Q";
            Baudot[16, 1] = "10111";
            Baudot[17, 0] = "R";
            Baudot[17, 1] = "01010";
            Baudot[18, 0] = "S";
            Baudot[18, 1] = "00101";
            Baudot[19, 0] = "T";
            Baudot[19, 1] = "10000";
            Baudot[20, 0] = "U";
            Baudot[20, 1] = "00111";
            Baudot[21, 0] = "V";
            Baudot[21, 1] = "11110";
            Baudot[22, 0] = "W";
            Baudot[22, 1] = "10011";
            Baudot[23, 0] = "X";
            Baudot[23, 1] = "11101";
            Baudot[24, 0] = "Y";
            Baudot[24, 1] = "10101";
            Baudot[25, 0] = "Z";
            Baudot[25, 1] = "10001";
            Baudot[25, 0] = " ";
            Baudot[25, 1] = "00100";
            string temp;
            for (int i = 0; i < 2048; i++)
            {
                temp = new string((char)i, 1);
                ASCII[i, 0] = temp;
                ASCII[i, 1] = ToBin(i);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            double AvLenCodeComb = 0;
            double allinf = 0;
            double enthropy = 0;
            double fr;
            string s = "";
            string[,] frequency;

            s = textBox3.Text;
            Sentence sen = new Sentence(s);
            frequency = sen.frequency;

            for (int i = 0; i < sen.powerOfAlphabet; i++)
            {
                fr = Convert.ToDouble(frequency[i, 1]) / sen.numSymbols;
                allinf += Convert.ToDouble(frequency[i, 1]) * -1 * fr * Math.Log(fr, 2);
                enthropy += -1 * fr * Math.Log(fr, 2);
                AvLenCodeComb += 11 * fr;

                dataGridView1.Rows.Add(frequency[i, 0], frequency[i, 1], Math.Round(-1 * fr * Math.Log(fr, 2), 2));
            }
            label2.Text = Math.Round(enthropy, 2).ToString();
            label3.Text = Math.Round(allinf, 2).ToString();
            label5.Text = Math.Round(AvWordLen(sen), 2).ToString();
            label7.Text = ((double)Math.Log(sen.powerOfAlphabet, 2) / enthropy).ToString();
            label9.Text = (enthropy / AvLenCodeComb).ToString();  // энтропия / средняя длина кодовой комбинации
            textBox1.Text = CodeASCII(s);
            textBox2.Text = DecodeASCII(textBox1.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string s = "";
            string[,] frequency;


            s = textBox3.Text;
            Sentence sen = new Sentence(s);
            frequency = sen.frequency;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            double AvLenCodeComb = 0;
            double allinf = 0;
            double enthropy = 0;
            double fr;
            string s = "";
            string[,] frequency;

            s = textBox3.Text;
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
            label2.Text = Math.Round(enthropy, 2).ToString();
            label3.Text = Math.Round(allinf, 2).ToString();
            label5.Text = Math.Round(AvWordLen(sen), 2).ToString();
            label7.Text = ((double)Math.Log(sen.powerOfAlphabet, 2) / enthropy).ToString();
            label9.Text = (enthropy / AvLenCodeComb).ToString();  // энтропия / средняя длина кодовой комбинации
            textBox1.Text = CodeBaudot(s);
            textBox2.Text = DecodeBaudot(textBox1.Text);
        }
        string CodeASCII(string s)
        {
            string result = "";
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < 2048; j++)
                {
                    if (new string(s[i], 1) == ASCII[j, 0])
                    {
                        result += ASCII[j, 1];
                    }
                }
            }
            return result;
        }
        string DecodeASCII(string s)
        {
            string result = "";
            string temp = s;
            char[] bin1 = new char[11];
            string bin2;
            for (int i = 0; ; i++)
            {
                temp.CopyTo(0, bin1, 0, 11);
                bin2 = new string(bin1);
                for (int j = 0; j < 2048; j++)
                {
                    if (bin2 == ASCII[j, 1])
                    {
                        result += ASCII[j, 0];
                    }
                }
                temp = temp.Remove(0, 11);
                if (temp == "")
                {
                    break;
                }
            }
            return result;
        }
        string CodeBaudot(string s)
        {

            string result = "";
            try
            {
                for (int i = 0; i < s.Length; i++)
                {
                    for (int j = 0; j < 26; j++)
                    {

                        if (new string(s[i], 1) == Baudot[j, 0])
                        {
                            result += Baudot[j, 1];
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Некоторых введенных символов нет в алфавите Baudot");
            }

            return result;
        }
        string DecodeBaudot(string s)
        {
            string result = "";
            string temp = s;
            char[] bin1 = new char[5];
            string bin2;
            for (int i = 0; ; i++)
            {
                temp.CopyTo(0, bin1, 0, 5);
                bin2 = new string(bin1);
                for (int j = 0; j < 26; j++)
                {
                    if (bin2 == Baudot[j, 1])
                    {
                        result += Baudot[j, 0];
                    }
                }
                temp = temp.Remove(0, 5);
                if (temp == "")
                {
                    break;
                }
            }
            return result;
        }
        string ToBin(int t)
        {
            string temp = "";
            for (int i = 0; i < 11; i++)
            {
                temp += t % 2;
                t /= 2;
            }
            return Reverse(temp);
        }
        static string Reverse(string str)
        {
            char[] dop;
            char simv;
            int i;
            dop = str.ToCharArray();
            for (i = 0; i < (dop.Length / 2); i++)
            {
                simv = dop[i];
                dop[i] = dop[dop.Length - i - 1];
                dop[dop.Length - i - 1] = simv;

            }
            str = new string(dop);
            return str;
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

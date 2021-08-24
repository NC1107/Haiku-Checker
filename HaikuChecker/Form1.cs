using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HaikuChecker
{
    public partial class Form1 : Form
    {
        private const string Path = "Your_Haiku.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = SyllableCounter(textBox1.Text).ToString() + "/5";
            label2.Text = SyllableCounter(textBox2.Text).ToString() + "/7";
            label3.Text = SyllableCounter(textBox3.Text).ToString() + "/5";

            LineStatus(label1);
            LineStatus(label2);
            LineStatus(label3);

            if (label1.Text == "5/5" && label2.Text == "7/7" && label3.Text == "5/5")
            {
                button2.Visible = true;
            }
        }


        private int SyllableCounter(string test)
        {
            if (string.IsNullOrEmpty(test)) { return 0; }

            int syllables = 0;
            int total = 0;
            foreach (string word in test.Split(' '))
            {
                string tempword = new string(word.Where(c => !char.IsPunctuation(c)).ToArray());
                string newWord = new string(tempword.Where(c => !char.IsDigit(c)).ToArray());

                if (!string.IsNullOrEmpty(newWord))
                {
                    syllables = 0;
                    if ("aeiouy".IndexOf(newWord[0]) >= 0) { syllables += 1; }
                    for (int index = 1; index < newWord.Length; index++)
                    {
                        if ("aeiouy".IndexOf(newWord[index]) >= 0 && "aeiouy".IndexOf(newWord[index - 1]) <= 0) { syllables += 1; }
                    }
                    if (newWord.EndsWith("e")) { syllables -= 1; }
                    if (newWord.EndsWith("le") && newWord.Length > 2)
                    {
                        try
                        {
                            if ("aeiouy".IndexOf(newWord[newWord.Length - 3]) <= 0) { syllables++; }

                        }
                        catch (IndexOutOfRangeException) { }
                    }
                    if (syllables == 0) { syllables += 1; }

                    total += syllables;
                }

            }


            return total;

        }

        private void LineStatus(Label label)
        {
            if (label.Text.EndsWith("/5"))
            {
                switch (label.Text)
                {
                    case "5/5":
                        label.ForeColor = Color.Green;
                        break;
                    default:
                        label.ForeColor = Color.Red;
                        break;

                }
            }
            if (label.Text.EndsWith("/7"))
            {
                switch (label.Text)
                {
                    case "7/7":
                        label.ForeColor = Color.Green;
                        break;
                    default:
                        label.ForeColor = Color.Red;
                        break;

                }
            }

        }

        private void button2_ClickAsync(object sender, EventArgs e)
        {
            SaveHaiku();
        }

        protected void SaveHaiku()
        {
            string file_name = String.Format("Your_Haiku{0}.txt", DateTime.Now.ToString("HH.mm.ss"));
            System.IO.Directory.CreateDirectory("Haikus/");
            using (System.IO.StreamWriter sw = System.IO.File.CreateText("Haikus/" + file_name))
            {
                sw.WriteLine(textBox1.Text);
                sw.WriteLine(textBox1.Text);
                sw.WriteLine(textBox3.Text);
                sw.Close();
            }




        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }






}

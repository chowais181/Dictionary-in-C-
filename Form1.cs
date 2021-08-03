using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Collections;
using System.IO;

namespace Data_Project
{
    public partial class Form1 : Form
    {
        trieNode head;
        Dictionary d;
        Queue qWord, qTemp;
        int wordSize;
        string[] tempArr;
        suggestion start;
        char[] delimiters = { ' ', '\n', ',', '.' };
        public Form1()
        {
            InitializeComponent();
            d=new Dictionary();
            head = new trieNode();

            StreamReader WordinFile = new StreamReader("words_alpha.txt");
            string readwords = WordinFile.ReadToEnd();
            WordinFile.Close();
            char[] wordA = readwords.ToCharArray();
            string[] strWord = new string[1];
            qWord = new Queue();
            qTemp = new Queue();
            int wordB = 0;
            for(int i=0;i<wordA.Length;i++)
            {
                if(wordA[i]!='\n' && wordA[i]!='\r')
                {
                    strWord[wordB] = strWord[wordB] + wordA[i];
                }
                else if (wordA[i]!='\r')
                {
                    qWord.Enqueue(strWord[0]);
                    qTemp.Enqueue(strWord[0]);
                    strWord[0] = "";
                }
                else if(wordA[i]!='\n')
                {

                }
            }
            wordSize = qWord.Count;
            tempArr = new string[wordSize];
            for(int i=0;i<wordSize;i++)
            {
                tempArr[i] = qWord.Dequeue().ToString();
            }
            
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            head=d.getData();
            
            MessageBox.Show("Features loaded Sucessfully");
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices cmnd = new Choices();
            for (int i = 0; i < wordSize; i++)
            {
                cmnd.Add(new string[] { tempArr[i] });
            }
            
        }
        public bool exceptions()
        {
            if (textBox1.Text.Contains("\t"))
            {
                textBox1.Text = textBox1.Text.Replace("\t", "");
            }
            if (textBox1.Text.Length == 0)
            {
                return true;
            }
            textBox1.SelectionStart = textBox1.Text.Length;//puts cursor on specified lcoation
            int val = textBox1.Text[textBox1.Text.Length - 1];
            if (val > 64 && val < 91 ||
                val > 96 && val < 123
                )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void sugggg(object sender, EventArgs e)
        {
            if (exceptions()) return;

            string lastWord = textBox1.Text.Split(delimiters).Last();//
            string currentSuggestion = t.Text;
            if (!(lastWord[0] > 64 && lastWord[0] < 91 || lastWord[0] > 96 && lastWord[0] < 123))
            {
                return;
            }
            if (!currentSuggestion.StartsWith(lastWord))
            {

                StreamReader sw = new StreamReader("words_alpha.txt");
                if (start == null || start.data != lastWord[0]) //if trie is null or head is not equal to users input[0]
                {
                    start = null;
                    start = new suggestion(lastWord[0], 0, false);
                    String tmpuse;
                    tmpuse = sw.ReadLine();
                    while (tmpuse.ToLower()[0] != lastWord.ToLower()[0])
                    {
                        tmpuse = sw.ReadLine();
                    }
                    while (tmpuse.ToLower()[0] == lastWord.ToLower()[0])
                    {
                        start.addWord(tmpuse);
                        tmpuse = sw.ReadLine();
                        if (tmpuse == null) break;// for Z
                    }
                }
                String newSuggestion = "";
                Random hehe = new Random();
                int num = hehe.Next(26);
                newSuggestion = lastWord;
                suggestion traverse = null;
                if (start.suggestThing(newSuggestion) != null)
                {
                    traverse = start.suggestThing(newSuggestion);
                }
                else
                {
                    t.Text = "";
                    t.Text = "Wrong Spellings.";
                    return;
                }
                while (!traverse.isComplete)
                {
                    while (traverse.child[num] == null)
                    {
                        num = hehe.Next(26);
                    }
                    traverse = traverse.child[num];
                    newSuggestion += traverse.data;
                }
                t.Text = newSuggestion;
            }
        }
        private void complete(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                if (t.Text == "")
                {
                    textBox1.Text += " ";
                    return;
                }
                textBox1.Text = t.Text;
                t.Text = "";
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text=(String)textBox1.Text;
            string meaning= d.searching(head, text);
            
            MessageBox.Show(text + " Meaning is " + meaning);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string word = (String)textBox3.Text;
            string meaning = (String)textBox4.Text;
            d.insert(head, word, meaning);
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        
    }
}

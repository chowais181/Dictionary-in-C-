using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Data_Project
{
    class Dictionary
        {
            public trieNode createMemory()
            {
                trieNode root;
                root = new trieNode();
                if(root!=null)
                {
                    root.isLeaf = false;
                    for (int i = 0; i < 26; i++)
                        root.childern[i] = null;
                }
                return root;
            }
            public void insert(trieNode root,string word,string mean)
            {
                char[] arrword = word.ToLower().ToCharArray();
                foreach(char c in arrword)
                {
                    int ind = (int)c - (int)'a';
                    if(root.childern[ind]==null)
                    {
                        root.childern[ind] = createMemory();
                    }
                    root = root.childern[ind];
                }
                root.isLeaf = true;
                root.meaning = mean;
            }
            public string searching(trieNode root,string key)
            {
                char[] arrword = key.ToLower().ToCharArray();
                foreach(char c in arrword)
                {
                    int ind = (int)c - (int)'a';
                    if(root.childern[ind]==null)
                    {
                        return "KEY NOT FOUND";
                    }
                    root = root.childern[ind];
                }
                return root.meaning;
            }
            public trieNode getData()
            {
                trieNode root = createMemory();
                string word;
                string meaning;
                StreamReader strRead = new StreamReader("words_alpha.txt");
                while((word=strRead.ReadLine())!=null)
                {
                    meaning = strRead.ReadLine();
                    insert(root, word, meaning);
                }
                return root;
            }
        }    
}

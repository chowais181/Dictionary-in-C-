using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Project
{
    class trieNode
    {
        public trieNode[] childern;
        public bool isLeaf;
        public string meaning;
        public trieNode()
        {
            childern = new trieNode[26];
            isLeaf = false;
            meaning = "\0";
        }
    }
}

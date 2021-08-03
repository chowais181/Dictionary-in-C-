using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Project
{
    class suggestion
    {
        public char data;
        public int depth;
        public bool isComplete;

        public suggestion[] child = new suggestion[26];
        public suggestion(char dat, int dp, bool isComp)
        {
            data = dat;
            depth = dp;
            isComplete = isComp;
        }
        public suggestion suggestThing(String arg)
        {
            suggestion tmp = this;
            for (int i = 1; i < arg.Length; i++)
            {
                if (tmp.child.Length < arg[i] - 'a' || arg[i] - 'a' < 0)
                {
                    return null;
                }
                if (tmp.child[arg[i] - 'a'] == null)
                {
                    return null;
                }
                else
                {
                    tmp = tmp.child[arg[i] - 'a'];
                }
            }
            return tmp;
        }
        public suggestion SearchPath(String arg) //yes
        {

            suggestion tmp = this;
            for (int i = 1; i < arg.Length; i++)
            {
                //Baad ma
                //26

                if (tmp.child.Length < arg[i] - 'a' || arg[i] - 'a' < 0)//CHECK if ascii is less than a or greater than z
                {
                    return tmp;
                }
                //
                if (tmp.child[arg[i] - 'a'] == null) // check if character exists already
                {
                    return tmp;
                }
                else
                {
                    tmp = tmp.child[arg[i] - 'a']; //character exists
                }
            }
            return tmp;
        }

        public void addWord(String arg)
        {
            suggestion tmp = SearchPath(arg);
            if (tmp.depth + 1 == arg.Length)
            {
                return;
            }
            else
            {
                for (int i = tmp.depth + 1; i < arg.Length; i++)
                {
                    if (tmp.child.Length < arg[i] - 'a' || arg[i] - 'a' < 0)
                    {
                        return;
                    }
                    tmp.child[arg[i] - 'a'] = new suggestion(arg[i], tmp.depth + 1, false);
                    tmp = tmp.child[arg[i] - 'a'];
                }
                tmp.isComplete = true;
            }
        }
    }
}

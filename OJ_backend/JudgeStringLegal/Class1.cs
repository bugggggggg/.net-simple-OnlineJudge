using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeStringLegal
{
    public class Class1
    {

        //判断字符串是否合法
        static String[] illegalWord = {
            "dork","nerd","geek","dammit","fuck"
        };
        public static bool Judge(string name)
        {
            name = name.ToLower();
            foreach (string word in illegalWord)
            {
                if(name.Contains(word))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

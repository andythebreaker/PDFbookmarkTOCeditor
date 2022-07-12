using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateTOCFromBookmarks
{
    class l2
    {
        // This constructor has no parameters. The parameterless constructor
        // is invoked in the processing of object initializers.
        // You can test this by changing the access modifier from public to
        // private. The declarations in Main that use object initializers will
        // fail.
        public l2() {
            ttl = new List<string>();
            ttN = new List<string>();
            pgg = new List<string>();
        }
        
        // Properties.
        public List<string> ttl{ get; set; }

        public List<string> ttN { get; set; }
        public List<string> pgg{ get; set; }

        //METH
        public void add(string ttl_, string pgg_)
        {
            ttN.Add(ttl_);
            pgg.Add(pgg_);
            ttN = ttN.Distinct().ToList();
        }

        public Dictionary<string, string> get()
        {
            Dictionary<string, string> MyDic = new Dictionary<string, string>();
            foreach(String el in ttN)
            {//TODO: IMP+ ALG.
                int tmp = 0;
                List<int> dup = new List<int>();
                List<String> dupS = new List<String>();
                foreach (String elm in ttl)
                {
                    if (el == elm)
                    {
                        int tmppggn = -1;
                        bool conversionSuccessful = int.TryParse(pgg.ElementAt(tmp), out tmppggn);
                        dup.Add(conversionSuccessful ? tmppggn : -1);
                        dupS.Add(pgg.ElementAt(tmp));
                    }
                    else {
                        Console.Beep();
                    }
                    tmp++;
                }
                if (dup.Count > 1)
                {
                    dup.RemoveAt(0); dup.RemoveAt(dup.Count - 1);
                }
                string combindedString = string.Join(",", dup.ToArray());
                MyDic.Add(el+" [Dup:"+ combindedString+"+]", dup.Count>1?dupS.First()+"~"+ dupS.Last(): dupS.ToString());
            }
            return MyDic;
        }
    }
}

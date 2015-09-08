using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string _url = "/abc/3/df/434/23/ddfe/10/";

            //string tx = @"\b";//@"\b(\/\w+)(\/\w+)(\/\w+)(\/\d+)(\/\w+)(\/\w+)\b"
            //for (int i = 0; i < _url.Split('/').Length - 3; i++)
            //{
            //    tx += @"(\/\w+)";
            //}
            //tx += @"\b";
            //var mc = Regex.Matches(_url, tx);

            //Console.WriteLine("*************开始循环***************");
            ////List<Dictionary<int, int>> _list = new List<Dictionary<int, int>>();
            //foreach (Match item in mc)
            //{
            //    //Dictionary<int,int> dic=new Dictionary<int,int> ();
            //    //dic.Add(item.Index, item.Length);
            //    //_list.Add(dic);
            //    Console.WriteLine(item.Value);
            //}
            //Console.WriteLine("*************结束循环***************");
           
            //string ms = Regex.Replace(_url, tx, "$1--$2");
            ////string ms = _url.Substring(Convert.ToInt32(_list[2][0]), Convert.ToInt32(_list[2][0]));
            //Console.WriteLine(ms);
            string url = "/artlist/3/1/ss/11/1/2/";
            int which = 2;
            string pageIndex = "3333";

            url = GetPageUrl(url, which, pageIndex);

            Console.WriteLine(url);

            Console.ReadKey();
        }


        public static string GetPageUrl(string url, int which, string pageIndex)
        {
            int place = -1;
            return Regex.Replace(url, @"(\d+)", new MatchEvaluator(m =>
            {
                place++;
                if (place == which)
                {
                    return pageIndex;
                }
                return m.Value;
            }));
        }
    }
}

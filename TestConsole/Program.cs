using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GetSetCallContextData();

            #region 废弃代码
            //string _url = "/abc/3/df/434/23/ddfe/10/";

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
            #endregion
            #region 分页码生成

            //string url = "/artlist/3/1/ss/11/1/2/";
            //int which = 2;
            //string pageIndex = "3333";

            //url = GetPageUrl(url, which, pageIndex);

            //Console.WriteLine(url); 

            #endregion

            Console.ReadKey();
        }

        /// <summary>
        /// 线程模拟使用CallContext
        /// </summary>
        private static void GetSetCallContextData()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine("线程调用时间1:{0}", now);
            CallContext.LogicalSetData("now", now);

            Thread.Sleep(3000);

            AutoResetEvent are = new AutoResetEvent(false);
            Thread t = new Thread(new ParameterizedThreadStart((x) =>
            {
                AutoResetEvent outerAre = x as AutoResetEvent;

                object outerTime = CallContext.GetData("now");
                Console.WriteLine("获取线程存储的时间1:{0}", outerTime);

                DateTime innerNow = DateTime.Now;
                Console.WriteLine("线程调用时间2:{0}", innerNow);
                CallContext.LogicalSetData("now", innerNow);


                object outerTime1 = CallContext.GetData("now");
                Console.WriteLine("刚设置完，获取线程存储的时间:{0}", outerTime1);
                outerAre.Set();
            }));
            t.Start(are);


            are.WaitOne();

            DateTime theTime = (DateTime)CallContext.LogicalGetData("now");
            Console.WriteLine("获取线程存储的时间2:{0}", theTime);


            Func<bool> m = () =>
            {
                object theTime2 = CallContext.LogicalGetData("now");
                Console.WriteLine("获取线程存储的时间3:{0}", theTime2);
                return true;
            };

            var result = m.BeginInvoke((x) => { }, 1);
            m.EndInvoke(result);
            
            Console.Read();
        }

        /// <summary>
        /// 分页码生成
        /// </summary>
        /// <param name="url"></param>
        /// <param name="which"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
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

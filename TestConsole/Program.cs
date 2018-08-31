using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            #region 循环注册极光用户
            //Console.WriteLine("输入开始的用户id，回车继续");
            //string _minUserId = Console.ReadLine();
            //Console.WriteLine("输入单次读取用户条数，回车开始读取用户数据");
            //int _pageSize = Convert.ToInt32(Console.ReadLine());
            ////最后一个用户id
            //string _lastUserId = string.Empty;
            //for (int i = 0; i < 10; i++)
            //{
            //    var _dt = Sales.GetUserDb(i, _pageSize, _minUserId);
            //    Console.WriteLine("本次总读取用户{0}条", _dt.Rows.Count);
            //    List<string> _list = new List<string>();
            //    for (int j = 0; j < _dt.Rows.Count; j++)
            //    {
            //        _list.Add(string.Format("t{0}", _dt.Rows[j]["user_id"]));
            //    }
            //    if (_dt.Rows.Count > 0)
            //    {
            //        _lastUserId = _dt.Rows[_dt.Rows.Count - 1]["user_id"].ToString();
            //    }
            //    string _uids = string.Join(",", _list);
            //    Console.WriteLine("开始注册极光：{0}", _uids);
            //    try
            //    {
            //        string _url = string.Format("http://0.t.93390.cn/Serve/Passport/RegistJPush?uids={0}", _uids);
            //        var _result = Encoding.Default.GetString(new WebClient().DownloadData(_url));
            //        var _return = JObject.Parse(_result);
            //        if (Convert.ToBoolean(_return["success"]))
            //        {
            //            Console.WriteLine("服务器注册极光异常：{0}", _uids);
            //            break;
            //        }
            //        else
            //        {
            //            Console.WriteLine("注册极光成功");
            //        }
            //    }
            //    catch (Exception ex2)
            //    {
            //        Console.WriteLine("注册极光异常：{0}", ex2.Message);
            //        break;
            //    }
            //}
            //Console.WriteLine("本次大于【{0}】的用户注册极光结束，最后一个用户id【{1}】", _minUserId, _lastUserId);

            #endregion

            #region 测试阿里云短信发送
            new AliSms().sendSms();
            #endregion

            //GetSetCallContextData();

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

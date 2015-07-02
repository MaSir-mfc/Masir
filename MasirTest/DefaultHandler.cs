using Masir.Web.Htmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasirTest
{
   public class DefaultHandler :HtmxHandler
    {
       public override void ParsePage()
       {
           Masir.MaLogHelper.GetLogger("TestLog").Info("测试的日志");
           base.ParsePage();
       }
    }
}

using Masir;
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
           this.MaLogInfo(Request.Url.ToString());
           //Masir.MaLogHelper.GetLogger("TestLog").Info(Request.Url.ToString());
           base.ParsePage();
       }
    }
}

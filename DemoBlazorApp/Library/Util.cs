using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DemoBlazorApp.Library
{
    public  static class Util
    {
        public static void Log(string message, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Console.WriteLine("[{0} #{1}] {2}", memberName, sourceLineNumber, message);
        }

        public static void Log(object message, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Console.WriteLine("[{0} #{1}] {2}", memberName, sourceLineNumber, message);
        }
    }
}

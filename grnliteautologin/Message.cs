using System;
using System.Collections.Generic;
using System.Text;

namespace GrnLiteAutoLogin
{
    internal class Message
    {
        internal static string BUSY_NOW_R()
        {
            Random r = new Random();
            int i = r.Next(5);
            switch (i)
            {
                case 0:
                    return ("正在帮你弄呢，你想累死我啊");
                case 1:
                    return ("这么性急的人，我还是第一次碰到");
                case 2:
                    return ("您的指令已被忽视");
                case 3:
                    return ("您的APM已经超出了测试范围");
                case 4 :
                    return ("保重身体， 请休息吧");
            }
            return string.Empty;
        }
    }
}

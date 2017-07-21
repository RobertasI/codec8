using System;
using System.Text;

using das;

//class Program
//{
//    public static string HeadlerInit()
//    {
//        StringBuilder sb = new StringBuilder();
//        sb.AppendLine("GET http://www.baidu.com/ HTTP/1.1");
//        sb.AppendLine("Host: www.baidu.com");
//        sb.AppendLine("Connection: keep-alive");
//        sb.AppendLine("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
//        sb.AppendLine("User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.90 Safari/537.36");
//        sb.AppendLine("Accept-Encoding:deflate, sdch");
//        sb.AppendLine("Accept-Language: zh-CN,zh;q=0.8");
//        sb.AppendLine("\r\n");
//        // There must be something else that might not have data 
//        return sb.ToString();
//    }
//    static void Main(string[] args)
//    {
//        string getStrs = HeadlerInit();
//        string getHtml = Server.SocketSendReceive(getStrs, "www.baidu.com", 80);
//        Console.WriteLine(getHtml);
//    }
//}
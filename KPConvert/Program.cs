using KPConvert;
using PsdSharp;
using System.IO.Compression;
using System.Text;
using System.Xml.Linq;

ConsoleKeyInfo mode;
do
{
    string pr = """

        1: Psd to Kra
        2: Kra to Psd
        """;
    Console.WriteLine(pr);
    mode = Console.ReadKey();
}
while (!(mode.Key == ConsoleKey.NumPad1 || mode.Key == ConsoleKey.NumPad2 || mode.Key == ConsoleKey.D1 || mode.Key == ConsoleKey.D2));


#region 1. 找到当前文件夹下的所有psd kra文件并根据文件名分组整合为元组 （同名.psd,同名.kra）

var files = Directory.GetFiles("./");


// 待处理列表 元组内存储路径字符串
List<(string Psd, string Kra)> waitList = [];
waitList = [..files.Where(x => x.EndsWith(".psd"))
                    .Join(files,
                          psd => psd.Replace(".psd", ".kra"),
                          kra => kra,
                          (psd, kra) => (psd, kra))];

Console.WriteLine(string.Join(",", waitList));


#endregion 

#region 2. 循环元组列表，挨个处理文件

foreach (var (Psd, Kra) in waitList)
{
    if(mode.Key == ConsoleKey.D1 || mode.Key == ConsoleKey.NumPad1)
        P2K.Do(Psd, Kra);
    else
        K2P.Do(Psd, Kra);
}
#endregion








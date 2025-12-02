using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Xml.Linq;

namespace KPConvert;

public class KraOperator : IDisposable
{
    private KraOperator() { }
    // 临时解压kra文件夹路径
    private string tempPath;
    private string maindocPath;
    private XDocument maindoc;
    // 生成的 kra 文件路径
    string resultKraFile;

    public static KraOperator Open(string path, out XDocument rootDoc)
    {
        KraOperator kraOperator = new();
        string filename = Path.GetFileNameWithoutExtension(path);
        var _tempPath = @$"./{filename}_converted";
        kraOperator.tempPath = _tempPath;

        // 解压后的 maindoc.xml 路径
        string _maindocPath = @$"{kraOperator.tempPath}/maindoc.xml";

        kraOperator.maindocPath = _maindocPath;

        // 解压 kra 文件
        ZipFile.ExtractToDirectory(path, _tempPath);

        // 读取 maindoc.xml
        rootDoc = XDocument.Load(_maindocPath);
        kraOperator.maindoc = rootDoc;
        // 生成的 kra 文件路径
        string _resultKraFile = @$"{_tempPath}.kra";
        kraOperator.resultKraFile = _resultKraFile;
        return kraOperator;
    }

    public bool Save()
    {
        if (Directory.Exists(tempPath))
        {
            // 保存 maindoc.xml
            maindoc.Save(maindocPath);
            return true;
        }
        return false;
    }

    public bool Export()
    {
        if (Directory.Exists(tempPath))
        {
            // 导出结果文件
            ZipFile.CreateFromDirectory(tempPath, resultKraFile, CompressionLevel.Fastest, false);
            return true;
        }
        return false;
    }

    public void Dispose()
    {
        if (Directory.Exists(tempPath))
        {
            // 删除临时文件夹
            Directory.Delete(tempPath, true);
        }
        GC.SuppressFinalize(this);
    }
}

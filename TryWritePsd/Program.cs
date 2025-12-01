using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Formatters;
using System.Text;

using FileStream psfs = new("./test.psd", FileMode.Open);

using BinaryReader br = new(psfs);

using FileStream psoutfs = new("./test_converted.psd", FileMode.CreateNew);
using BinaryWriter bw = new(psoutfs);

//for (int i = 0; i < 32; i++)
//{
//    var by = br.ReadBytes(4);
//    var bys = Encoding.Latin1.GetString(by);
//    Console.WriteLine(bys);
//}
int headLength = 26;

int colorModeDataSectionHeadLength = 4;

int colorModeDataSectionLength;

int imageResourcesSectionHeadLength = 4;

int imageResourcesSectionLength;

int layerandMaskInformationSectionHeadLength = 4;

int layerandMaskInformationSectionLength;

for (int i = 0; i < headLength; i++)
{
    var by = br.ReadByte();
    bw.Write(by);
}

bw.Flush();

var by2 = br.ReadBytes(colorModeDataSectionHeadLength);
colorModeDataSectionLength = BitConverter.ToInt32(by2);
bw.Write(by2);
for (int i = 0; i < colorModeDataSectionLength; i++)
{
    var by = br.ReadByte();
    bw.Write(by);
}

bw.Flush();



var by3 = br.ReadBytes(imageResourcesSectionHeadLength);
imageResourcesSectionLength = BitConverter.ToInt32(by2);
bw.Write(by3);
for (int i = 0; i < imageResourcesSectionLength; i++)
{
    var by = br.ReadByte();
    bw.Write(by);
}

bw.Flush();


var by4 = br.ReadBytes(layerandMaskInformationSectionHeadLength);
layerandMaskInformationSectionLength = BitConverter.ToInt32(by2);
bw.Write(by4);
for (int i = 0; i < layerandMaskInformationSectionLength; i++)
{
    var by = br.ReadByte();
    bw.Write(by);
}

bw.Flush();

while (br.BaseStream.Position < br.BaseStream.Length) 
{
    byte data = br.ReadByte();
    // 处理数据
    bw.Write(data);
}

bw.Flush();
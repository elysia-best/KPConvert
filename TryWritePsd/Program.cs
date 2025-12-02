using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Formatters;
using System.Text;
if(File.Exists("./test_converted.psd"))
    File.Delete("./test_converted.psd");
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

// 头部分
for (int i = 0; i < headLength; i++)
{
    var by = br.ReadByte();
    bw.Write(by);
}


// 
var by2 = br.ReadBytes(colorModeDataSectionHeadLength);
colorModeDataSectionLength = BinaryPrimitives.ReadInt32BigEndian(by2);
bw.Write(by2);
for (int i = 0; i < colorModeDataSectionLength; i++)
{
    var by = br.ReadByte();
    bw.Write(by);
}





var by3 = br.ReadBytes(imageResourcesSectionHeadLength);
imageResourcesSectionLength = BinaryPrimitives.ReadInt32BigEndian(by3);
bw.Write(by3);
for (int i = 0; i < imageResourcesSectionLength; i++)
{
    var by = br.ReadByte();
    bw.Write(by);
}



// 主要处理的部分
var by4 = br.ReadBytes(layerandMaskInformationSectionHeadLength);
layerandMaskInformationSectionLength = BinaryPrimitives.ReadInt32BigEndian(by4);
bw.Write(by4);
for (int i = 0; i < layerandMaskInformationSectionLength; i++)
{
    var by = br.ReadByte();
    bw.Write(by);
}



int size = 64 * 1024;
var buffer = new byte[size];
int bytesRead;
while ((bytesRead = br.Read(buffer, 0, buffer.Length)) > 0)
{
    bw.Write(buffer, 0, bytesRead);
}



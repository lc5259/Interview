using System.IO;

namespace demo1
{

    /// <summary>
    /// 排序cav文件的列，按照列的名称对列进行排序，并且不区分大小写
    /// 在生成的bin目录下已经有csv文件了，可以直接打开
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            string csvFilePath = "csvFile.csv";
            //读取csv文件内容
            string fileContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), csvFilePath));
             string result = SortCsvColumns(fileContent);
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static string SortCsvColumns(string csvFileContent)
        {
            {
                try
                {
                    string returnValue = string.Empty;
                    var lines = csvFileContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                              .Select(line => line.Split(','))
                                              .ToArray();
                    var indices = lines[0].Select((x, i) => new { Value = x, Index = i })
                                           .OrderBy(x => x.Value, StringComparer.OrdinalIgnoreCase)
                                           .Select(x => x.Index)
                                           .ToArray();
                    //拼接得到新的内容
                    returnValue = string.Join("\r\n", lines.Select(line => string.Join(",", indices.Select(i => line[i]))));
                    // 设置新的 CSV 文件路径
                    string newFileName = "newFile.csv";
                    string newFilePath = Path.Combine(Directory.GetCurrentDirectory(), newFileName);
                    if (File.Exists(newFilePath))
                    {
                        File.Delete(newFilePath);
                    }
                    // 写入数据到 CSV 文件
                    File.WriteAllText(newFilePath, returnValue);
                    return returnValue;
                }
                catch (Exception)
                {
                    throw; // 请根据需要修改错误处理方式
                }
            }
        }
    }
}

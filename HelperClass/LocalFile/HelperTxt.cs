using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HelperClass.LocalFile
{
    public class HelperTxt
    {
        private Encoding encoding= Encoding.UTF8;
        public Encoding TxtEncoding { set { this.encoding=value; } }

        /// <summary>
        /// 写入txt
        /// </summary>
        /// <param name="path">写入文本路径</param>
        /// <param name="strs">数据集</param>
        public void WriteTxt(string path, List<string> strs)
        {
            FileStream pFileStream = new FileStream(path, FileMode.Create);
            StreamWriter pStreamWriter = new StreamWriter(pFileStream, encoding);
            if (strs != null)
            {
                foreach (var item in strs)
                {
                    pStreamWriter.WriteLine(item);
                }
            }
            pStreamWriter.Flush();//清空缓冲区
            pStreamWriter.Close();//关闭流
            pFileStream.Close();//关闭流
        }

        /// <summary>
        /// 读取txt
        /// </summary>
        /// <param name="path">txt文件路径</param>
        /// <returns>返回读取的数据集</returns>
        public List<string> ReadTxt(string path)
        {
            List<string> strs = new List<string>();
            if (!File.Exists(path)) return strs;
            using (StreamReader reader = new StreamReader(path, encoding))
            {
                //循环读取所有行
                while (!reader.EndOfStream)
                {
                    // string line = Regex.Replace(reader.ReadLine(), "\\s{2,}", " ");
                    string line = reader.ReadLine();
                    if (line == "")
                    {
                        continue;
                    }
                    strs.Add(line);
                }
            }
            return strs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="keys"></param>
        public void WriteTxt_Dic(string path, Dictionary<string, string> keys)
        {
            FileStream pFileStream = new FileStream(path, FileMode.Create);
            StreamWriter pStreamWriter = new StreamWriter(pFileStream, encoding);
            if (keys != null)
            {
                foreach (KeyValuePair<string, string> key in keys)
                {
                    pStreamWriter.WriteLine(key.Key + "," + key.Value);
                }
            }
            pStreamWriter.Flush();//清空缓冲区
            pStreamWriter.Close();//关闭流
            pFileStream.Close();//关闭流
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Dictionary<string, string> ReadTxt_Dic(string path)
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            if (!File.Exists(path)) return keys;
            using (StreamReader reader = new StreamReader(path, encoding))
            {
                //循环读取所有行
                while (!reader.EndOfStream)
                {
                    string line = Regex.Replace(reader.ReadLine(), "\\s{2,}", " ");
                    if (string.IsNullOrEmpty(line)) continue;
                    string[] lines = line.Split(',');
                    if (lines.Length != 2 || string.IsNullOrEmpty(lines[0]) || string.IsNullOrEmpty(lines[1])) continue;
                    keys.Add(lines[0], lines[1]);
                }
            }
            return keys;
        }


    }
}

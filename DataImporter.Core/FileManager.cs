using DataImporter.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataImporter.Core
{
    public class FileManager : IFileManager
    {
        public StreamReader StreamReader(string path)
        {
            return new StreamReader(path);
        }

        public string[] GetFiles(string rootFolderlocation, string fileFilter, SearchOption searchOption)
        {
            return Directory.GetFiles(rootFolderlocation, fileFilter, searchOption);
        }

        public DirectoryInfo GetParent(string filePath)
        {
            return Directory.GetParent(filePath);
        }
    }
}

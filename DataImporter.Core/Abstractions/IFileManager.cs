using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataImporter.Core.Abstractions
{
    public interface IFileManager
    {
        StreamReader StreamReader(string path);

        string[] GetFiles(string rootFolderlocation, string fileFilter, SearchOption searchOption);

        DirectoryInfo GetParent(string filePath);
    }
}

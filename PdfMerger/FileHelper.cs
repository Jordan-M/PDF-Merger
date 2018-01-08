using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMerger
{
    class FileHelper
    {
        /// <summary>
        /// </summary>
        /// <param name="location"></param>
        /// <param name="searchSubfolders"></param>
        /// <returns></returns>
        public static int CalculateNumFiles(string location, bool searchSubfolders)
        {
            SearchOption option = (searchSubfolders) ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            return Directory.GetFiles(location, "*.*", option).Length;
        }
    }
}

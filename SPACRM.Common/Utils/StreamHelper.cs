using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SPACRM.Common.Utils
{
    public class StreamHelper
    {
        public static string Read(Stream stream)
        {
            using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                string res = streamReader.ReadToEnd();
                return res;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HapaxTools
{
    public static class FileStreamExtensions
    {
        /// <summary>
        /// Reads four bytes from the file stream and converts them into an int.
        /// </summary>
        /// <param name="stream">The given file stream instance.</param>
        public static int ReadInt(this FileStream stream)
        {
            int res = stream.ReadByte();

            for (int i = 0; i < 3; i++)
            {
                res <<= 8;
                res += stream.ReadByte();
            }

            return res;
        }
    }
}
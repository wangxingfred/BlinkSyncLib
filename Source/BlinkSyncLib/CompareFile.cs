//**************************************************************************************
//Create By Fred on 2020/06/09.
//
//@Description 比较文件是否相同的方法实现
//**************************************************************************************

using System.Collections.Generic;
using System.IO;

namespace BlinkSyncLib
{
    public static class CompareFile
    {
        private const int BUFFER_SIZE = 4096;
        private static readonly byte[] _buffer1 = new byte[BUFFER_SIZE];
        private static readonly byte[] _buffer2 = new byte[BUFFER_SIZE];

        public static bool AreEqual(FileInfo src, FileInfo dest, CompareMethod method)
        {
            if (method.HasFlag(CompareMethod.SizeInBytes))
            {
                if (src.Length != dest.Length) return false;
            }

            if (method.HasFlag(CompareMethod.LastWriteTime))
            {
                if (src.LastWriteTime != dest.LastWriteTime) return false;
            }

            if (method.HasFlag(CompareMethod.Attributes))
            {
                if (src.Attributes != dest.Attributes) return false;
            }

            // ReSharper disable once InvertIf
            if (method.HasFlag(CompareMethod.Contents))
            {
                if (!AreContentsIdentical(src, dest)) return false;
            }

            return true;
        }

        public static bool AreContentsIdentical(FileInfo fileInfo1, FileInfo fileInfo2)
        {
            using (var stream1 = new FileStream(fileInfo1.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, BUFFER_SIZE, FileOptions.SequentialScan))
            using (var stream2 = new FileStream(fileInfo2.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, BUFFER_SIZE, FileOptions.SequentialScan))
            {
                while (stream1.Position < stream1.Length)
                {
                    int bytesRead1 = stream1.Read(_buffer1, 0, BUFFER_SIZE);
                    int bytesRead2 = stream2.Read(_buffer2, 0, BUFFER_SIZE);
                    if (bytesRead1 != bytesRead2)
                    {
                        // can this ever happen?
                        return false;
                    }

                    for (int i = 0; i < bytesRead1; i++)
                    {
                        if (_buffer1[i] != _buffer2[i])
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
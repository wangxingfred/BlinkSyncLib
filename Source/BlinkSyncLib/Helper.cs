//**************************************************************************************
//Create By Fred on 2020/06/09.
//
//@Description helper functions
//**************************************************************************************

using System.Text.RegularExpressions;

namespace BlinkSyncLib
{
    public static class Helper
    {
        /// <summary>
        /// Sync directory files with default options
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="destinationDirectory"></param>
        /// <returns></returns>
        public static SyncResults Sync(string sourceDirectory, string destinationDirectory)
        {
            var sync = new Sync(sourceDirectory, destinationDirectory);
            return sync.Start();
        }

        /// <summary>
        /// Sync directory files, ignore *.meta files
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="destinationDirectory"></param>
        /// <returns></returns>
        public static SyncResults SyncExcludeMeta(string sourceDirectory, string destinationDirectory)
        {
            return SyncExcludeFileType(sourceDirectory, destinationDirectory, ".meta");

        }

        /// <summary>
        /// Sync directory files, ignore specified file type
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="destinationDirectory"></param>
        /// <param name="fileType">文件后缀名，比如：.meta</param>
        /// <returns></returns>
        public static SyncResults SyncExcludeFileType(string sourceDirectory, string destinationDirectory, string fileType)
        {
            var excludes = new[] {new Regex(@".+\" + fileType + "$")};
            var sync = new Sync(sourceDirectory, destinationDirectory)
            {
                Configuration =
                {
                    ExcludeFiles = excludes,
                    DeleteExcludeFiles = excludes,
                }
            };
            return sync.Start();
        }

        /// <summary>
        /// sync directory with specified file type
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="destinationDirectory"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static SyncResults SyncWithFileType(string sourceDirectory, string destinationDirectory, string fileType)
        {
            var includes = new[] {new Regex(@".+\" + fileType + "$")};
            var sync = new Sync(sourceDirectory, destinationDirectory)
            {
                Configuration =
                {
                    IncludeFiles = includes
                }
            };
            return sync.Start();
        }
    }
}
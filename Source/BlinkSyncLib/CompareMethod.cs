//**************************************************************************************
//Create By Fred on 2020年6月9日.
//
//@Description 文件比较方式，可以组合多个方式
//**************************************************************************************

namespace BlinkSyncLib
{
    [System.Flags]
    public enum CompareMethod
    {
        SizeInBytes = 0,      // 比较文件大小
        LastWriteTime = 1,    // 比较写入时间
        Attributes = 2,       // 比较文件属性
        Contents = 4,         // 比较文件内容

        Default = SizeInBytes | Contents
    }
}
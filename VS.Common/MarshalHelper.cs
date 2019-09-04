using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VS.Common
{
    public static class MarshalHelper
    {

        /// <summary>
        /// 将Byte转换为结构体类型
        /// </summary>
        public static byte[] StructToBytes(object structObj, int size)
        {
            byte[] bytes = new byte[size];
            try
            {
                IntPtr structPtr = Marshal.AllocHGlobal(size);
                //将结构体拷到分配好的内存空间
                Marshal.StructureToPtr(structObj, structPtr, true);
                //从内存空间拷贝到byte 数组
                Marshal.Copy(structPtr, bytes, 0, size);
                //释放内存空间
                Marshal.FreeHGlobal(structPtr);
            }
            catch (Exception ex)
            {

            }
            return bytes;
        }

        /// <summary>
        /// 将Byte转换为结构体类型
        /// </summary>
        public static object ByteToStruct(byte[] bytes, int startIndex, Type type, int length)
        {
            int size = length;
            if (size > bytes.Length)
            {
                return null;
            }
            //分配结构体内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷贝到分配好的内存空间
            Marshal.Copy(bytes, startIndex, structPtr, length);
            //将内存空间转换为目标结构体
            object obj = Marshal.PtrToStructure(structPtr, type);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return obj;
        }

    }
}

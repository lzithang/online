using AutoMapper;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VS.OnLineManager
{
    /// <summary>
    /// 模型映射配置
    /// </summary>
    public class MyDataProfile : Profile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MyDataProfile()
        {
            CreateMap<ConfigBindModel,ChannelStruct>();
            CreateMap<DataTwModel, DataTw>();
            CreateMap<string, char[]>().ConvertUsing<StringToCharArrayConvertert>();
            CreateMap<byte[], float[]>().ConvertUsing<ByteToFloatConvertert>();
        }
    }

    /// <summary>
    /// byte to float数据类型转换
    /// </summary>
    public class ByteToFloatConvertert : ITypeConverter<byte[], float[]>
    {
        /// <summary>
        /// 实现数据类型转换
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public float[] Convert(byte[] source, float[] destination, ResolutionContext context)
        {
            if(source == null || source.Length == 0)
            {
                return new float[0];
            }
            int count = source.Length / 4;
            float[] tw = new float[count];
            for (int i = 0; i < count; i++)
            {
                tw[i] = BitConverter.ToSingle(source, i * 4);
            }
            return tw;
        }
    }

    /// <summary>
    /// datetime to long 数据类型转换
    /// </summary>
    public class DateTimeToLongConvertert : ITypeConverter<DateTime, long>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public long Convert(DateTime source, long destination, ResolutionContext context)
        {
            return new DateTimeOffset(source).ToUnixTimeSeconds();
        }
    }

    /// <summary>
    /// datetime to long 数据类型转换
    /// </summary>
    public class StringToIntArrayConvertert : ITypeConverter<string, int[]>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public int[] Convert(string source, int[] destination, ResolutionContext context)
        {
            return JsonConvert.DeserializeObject<int[]>(source);
        }
    }

    /// <summary>
    /// string to char[] 数据类型转换
    /// </summary>
    public class StringToCharArrayConvertert : ITypeConverter<string, char[]>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public char[] Convert(string source, char[] destination, ResolutionContext context)
        {
            char[] data = new char[30];
            if (string.IsNullOrEmpty(source))
            {
                return data;
            }
            for (int i = 0; i < data.Length; i++)
                data[i] = ' ';
            char[] temp = source.ToCharArray();
            Array.Copy(temp, 0, data, 0, temp.Length);
            return data;
        }
    }


}

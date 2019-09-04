using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DataTwModel
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// </summary>
        /// 数据
        /// </summary>
        public float[] Data { get; set; }

        /// </summary>
        /// 上传时间
        /// </summary>
        public long Time { get; set; }

        /// </summary>
        /// 区域Id
        /// </summary>
        public int AreaId { get; set; }

        /// </summary>
        /// 机器id
        /// </summary>
        public int McId { get; set; }

        /// </summary>
        /// 测点Id
        /// </summary>
        public int ParId { get; set; }

        /// </summary>
        /// 方向Id
        /// </summary>
        public int DirId { get; set; }

        /// </summary>
        /// 数据状态
        /// </summary>
        public int DataState { get; set; }

        /// </summary>
        /// 基线
        /// </summary>
        public int DataBaseLine { get; set; }

        /// </summary>
        /// 谱线数
        /// </summary>
        public int DataLines { get; set; }

        /// </summary>
        /// 数据类型：1、全通；2、高通；3、包络谱
        /// </summary>
        public int DataType { get; set; }

        /// </summary>
        /// 采样点数
        /// </summary>
        public int DataPoints { get; set; }

        /// </summary>
        /// 采样频率
        /// </summary>
        public int DataHz { get; set; }

        /// </summary>
        /// 0波形数据，1频谱数据
        /// </summary>
        public int DataIsFFT { get; set; }

        /// </summary>
        /// 1长时间波形，0非
        /// </summary>
        public int DataIsLongData { get; set; }

        /// </summary>
        /// 数据格式：1、二进制；2、十进制数
        /// </summary>
        public int DataFormat { get; set; }

        /// </summary>
        /// 工况
        /// </summary>
        public int DataWorkStatus { get; set; }
    }
}

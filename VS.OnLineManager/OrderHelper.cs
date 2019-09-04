using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Model;
using VS.Common;

namespace VS.OnLineManager
{
    public static class OrderHelper
    {
        #region 校验命令
        /// <summary>
        /// 获取校验码
        /// </summary>
        /// <param name="data">命令</param>
        /// <param name="startIndex">起始位置</param>
        /// <returns></returns>
        public static byte CheckCode(byte[] data, int startIndex)
        {
            byte bValue = 0;
            for (int i = startIndex; i < data.Length - 1; i++)
            {
                bValue += data[i];
            }
            return bValue;
        }
        /// <summary>
        /// 检查校验码
        /// </summary>
        /// <param name="data">命令</param>
        /// <returns></returns>
        public static bool CheckCode(byte[] data)
        {
            if (CheckCode(data, 0) == data[data.Length - 1])
                return true;
            return false;
        }
        #endregion

        #region 站点配置更新
        /// <summary>
        /// 时间更新
        /// </summary>
        public static byte[] UpdateTime()
        {
            /********************************************站点时间更新命令：*******************************************
          * 【 1	  |  $ 】     
          * 【 2  |   D】     
          * 【 3-16 | 	xxxx xx xx xx xx xx 年月日时分秒】 
          * 【19校验】
          **************************************************************************************************************/
            DateTime time = DateTime.Now;
            string yy = time.Year.ToString("0000");
            string MM = time.Month.ToString("00");
            string dd = time.Day.ToString("00");
            string hh = time.Hour.ToString("00");
            string mm = time.Minute.ToString("00");
            string ss = time.Second.ToString("00");
            byte[] cmd =
            {
                (byte)'$',(byte)'D',
                (byte)yy[0],(byte)yy[1],(byte)yy[2],(byte)yy[3],
                (byte)MM[0],(byte)MM[1],
                (byte)dd[0],(byte)dd[1],
                (byte)hh[0],(byte)hh[1],
                (byte)mm[0],(byte)mm[1],
                (byte)ss[0],(byte)ss[1],
                (byte)'?'
            };
            return cmd;
        }

        /// <summary>
        /// 更新工况值
        /// </summary>
        /// <returns></returns>
        public static byte[] UpdateStatus(List<WorkStatus> list)
        {
            /********************************************下载测点结构：*******************************************
                      * 【 1	    |  $】     
                      * 【 2    | Y】     
                      * 【 3~6   | 	    通道编号：0】 
                      * 【 7,8,9,10  | 	    第1个状态值默认-1，有效状态值≥0】 
                      * 【 11,12,13,14  | 	    延迟时间 t≥0 s】 
                      * 【 ...  | 	   最多5组工况】 
                      * 【47 校验     】
           **************************************************************************************************************/
            byte[] cmd = new byte[47];
            byte[] temp = BitConverter.GetBytes(-1);
            cmd[0] = (byte)'$';
            cmd[1] = (byte)'Y';

            //初始化
            for (int i = 0; i < 10; i++)
            {
                Array.Copy(temp, 0, cmd, 6 + i * 4, 4);
            }

            for (int i = 0; i < list.Count; i++)
            {
                //工况
                temp = BitConverter.GetBytes(list[i].WorkStatusValue);
                Array.Copy(temp, 0, cmd, 6 + i * 8, 4);
                //时间
                temp = BitConverter.GetBytes((int)(list[i].Time * 1000));
                Array.Copy(temp, 0, cmd, 10 + i * 8, 4);
            }
            cmd[46] = (byte)'?';
            return cmd;
        }

        /// <summary>
        /// 站点配置更新
        /// </summary>
        public static byte[] UpdateChannelInfo(ChannelStruct channelStruct)
        {
            /********************************************下载测点结构：*******************************************
                      * 【 1	    |  $】     
                      * 【 2    | P(point)】     
                      * 【 3~264| 	    struct ChannelInfo 】 262字节
                      * 【265校验     】
           **************************************************************************************************************/
            channelStruct.AreaName = new char[30];
            channelStruct.MachineName = new char[30];
            channelStruct.MonitorIDName = new char[30];
            byte[] structData = MarshalHelper.StructToBytes(channelStruct, 266);
            byte[] cmd = new byte[266 + 3];
            cmd[0] = (byte)'$';
            cmd[1] = (byte)'P';
            Array.Copy(structData, 0, cmd, 2, 266);
            cmd[268] = (byte)'$';
            return cmd;
        }

        /// <summary>
        /// 测量参数组更新
        /// </summary>
        /// <param name="meterageList"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static byte[] UpdateMeterageSample(List<MeterageLibModel> meterageList, int groupId)
        {
            if (meterageList.Count <= 0)
                return null;
            /********************************************每块调理版设置参数组：*******************************************
                      * 【 1	    |  $】     
                      * 【 2    | G(Group)】     
                      * 【 3,4,5,6| 	    调理模块编号：1~4 每个模块最多可以设置6个参数组 】
                      * 【7,8,9,10|      设置的参数组数目N：N≤6】
                      * 【14|            数据类型：1：位移 2：速度 3：加速度4：包络】
                     * 【18|分析频率：位移、速度、加速度1~16：表示对应的分析频率范围 包络1~2：表示对应的分析频率范围】
                     * 【22|谱线数：位移、速度、加速度1~6：对应的谱线数 包络1~3：对应的谱线数】
                     * 【26|平均类型：1：时间同步平均2：频谱线性平均（默认此选项）3：频谱指数平均4：频谱峰值平均】
                     * 【30|窗函数：1：汉宁窗2：矩形窗】
                    * 【34|叠加次数：1~8：序号代表平均的次数（1，2，4，8，16，32，64，128 ）】
                     * 【38|叠加百分比：1~8：序号代表叠加的百分比（0，12.5，25，37.5，50，62.5，75，87.5）】
                     * 【42|是否保留波形】
                    * 【46|是否保留频谱】
                      * 【19校验     】
           **************************************************************************************************************/
            byte[] cmd = new byte[10 + 36 * meterageList.Count + 1];
            cmd[0] = (byte)'$';
            cmd[1] = (byte)'G';
            //调理版编号
            byte[] dataTemp = BitConverter.GetBytes(groupId);
            Array.Copy(dataTemp, 0, cmd, 2, 4);
            //采集参数组数量
            dataTemp = BitConverter.GetBytes(meterageList.Count);
            Array.Copy(dataTemp, 0, cmd, 6, 4);
            for (int i = 0; i < meterageList.Count; i++)
            {
                int type, frequency;
                GetTypeAndFrequency(meterageList[i].MlName, out type, out frequency);
                //数据类型
                dataTemp = BitConverter.GetBytes(type);
                Array.Copy(dataTemp, 0, cmd, 10 + 36 * i, 4);
                //分析频率
                dataTemp = BitConverter.GetBytes(frequency);
                Array.Copy(dataTemp, 0, cmd, 14 + 36 * i, 4);
                //谱线数
                dataTemp = BitConverter.GetBytes(meterageList[i].mlLine);
                Array.Copy(dataTemp, 0, cmd, 18 + 36 * i, 4);
                //平均类型                                        
                dataTemp = BitConverter.GetBytes(meterageList[i].MlAvgType);
                Array.Copy(dataTemp, 0, cmd, 22 + 36 * i, 4);
                //窗函数                                          
                dataTemp = BitConverter.GetBytes(meterageList[i].MlWindowFun);
                Array.Copy(dataTemp, 0, cmd, 26 + 36 * i, 4);
                //叠加次数                                        
                dataTemp = BitConverter.GetBytes(meterageList[i].mlAvgNum);
                Array.Copy(dataTemp, 0, cmd, 30 + 36 * i, 4);
                //叠加百分比                                     
                dataTemp = BitConverter.GetBytes(meterageList[i].MlPercent);
                Array.Copy(dataTemp, 0, cmd, 34 + 36 * i, 4);
                //是否保留波形                                    
                dataTemp = BitConverter.GetBytes(meterageList[i].MlFFTWT != 1 ? 1 : 0);
                Array.Copy(dataTemp, 0, cmd, 38 + 36 * i, 4);
                //是否保留频谱                                   
                dataTemp = BitConverter.GetBytes(meterageList[i].MlFFTWT != 2 ? 1 : 0);
                Array.Copy(dataTemp, 0, cmd, 42 + 36 * i, 4);

            }
            cmd[10 + 36 * meterageList.Count] = (byte)'?';
            return cmd;
        }

        /// <summary>
        /// 设置对应每块调理版的参数组是否采集
        /// </summary>
        /// <param name="meterageList"></param>
        /// <param name="relationList"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public static byte[] UpdateMeterageChannelBind(List<MeterageLibModel> meterageLibList, List<MeterageSamplerate> meterageSampleList, int channelId)
        {
            /********************************************通道每组参数是佛采集命令：*******************************************
           * 【 1	    |  $】     
           * 【 2    | W】     
           * 【 3,4,5,6| 	   测量通道编号：1~32 】
           * 【7,8,9,10|      设置的参数组数目N：N≤6】
           * 【14 |      第1组参数是否采集】
           *  ...  ...
           * 【35校验     】
**************************************************************************************************************/
            int num = meterageLibList.Count;
            if (num <= 0)
                return null;

            byte[] dataTemp = BitConverter.GetBytes(channelId);
            byte[] cmd = new byte[10 + num * 4 + 1];
            cmd[0] = (byte)'$';
            cmd[1] = (byte)'W';
            //通道号
            Array.Copy(dataTemp, 0, cmd, 2, 4);
            dataTemp = BitConverter.GetBytes(num);
            Array.Copy(dataTemp, 0, cmd, 6, 4);
            for (int i = 0; i < num; i++)
            {
                MeterageSamplerate meterageSample = meterageSampleList.FirstOrDefault(r => r.MlId == meterageLibList[i].MlId);
                if (meterageSample != null)
                {
                    dataTemp = BitConverter.GetBytes(1);
                    Array.Copy(dataTemp, 0, cmd, 10 + (i * 4), 4);
                }
            }

            cmd[10 + num * 4] = (byte)'?';
            return cmd;
        }

        /// <summary>
        /// 固件更新，告诉下位机固件长度
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] SolidHandshake(int length)
        {
            /********************************************通道每组参数是佛采集命令：*******************************************
            * 【 1	    |  0x??】     
            * 【2，3，4，5 | D，D，B，S】     
            * 【6,7| 	   保留字节，用0填充 】
            * 【8,9,10,11|     表示固件文件大小】
            * 【12,13 |      保留字节，用0填充】
            * 【14，15 |      保留字节，用0填充】
            * 【16 |      保留字节，用0填充】 
            * 【17 |      保留字节，用0填充】
            * 【18|.F：固件（ASCII码等于70）字节大小（没有）】
            * 【19校验     】
            **************************************************************************************************************/
            byte[] dataTemp = BitConverter.GetBytes(length);
            byte[] cmd = {
                0xbb,
                (byte)'D', (byte)'D', (byte)'B', (byte)'S',
                0,0,
                0,0,0,0,
                0,0,
                0,0,
                0,
                0,
                (byte)'F',
                (byte)'?'
            };
            Array.Copy(dataTemp, 0, cmd, 7, 4);
            return cmd;
        }

        public static byte[] UpdateSolid(byte[] data)
        {
            /********************************************通道每组参数是佛采集命令：*******************************************
            * 【 1	    |  $ (美元，ASCII码为36) 】     
            * 【 2    | F (Fireware)】     
            *  ...  ... 数据
            * 【2+Length+1|校验     】
            **************************************************************************************************************/
            byte[] cmd = new byte[3 + data.Length];
            cmd[0] = (byte)'$';
            cmd[1] = (byte)'F';
            Array.Copy(data, 0, cmd, 2, data.Length);
            cmd[2 + data.Length] = CheckCode(cmd, 0);
            return cmd;
        }

        /// <summary>
        /// 设置通道的工作状态
        /// </summary>
        /// <param name="list"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public static byte[] UpdateWorkStatus(List<WorkStatus> list, int channelId)
        {
            int count = list.Count;
            byte[] dataTemp = BitConverter.GetBytes(channelId);
            byte[] cmd = new byte[27];
            cmd[0] = (byte)'$';
            cmd[1] = (byte)'Y';
            Array.Copy(dataTemp, 0, cmd, 2, 4);
            for (int i = 0; i < 5; i++)
            {
                dataTemp = i >= count ? BitConverter.GetBytes(-1) : BitConverter.GetBytes(list[i].WorkStatusValue);
                Array.Copy(dataTemp, 0, cmd, 6 + i * 4, 4);
            }

            cmd[26] = (byte)'?';
            return cmd;
        }

        #endregion

        #region 握手命令
        /// <summary>
        /// 握手命令
        /// </summary>
        public static byte[] Handshake
        {
            /********************************************站点回复握手命令格式：*******************************************
                      * 【 1	  |  0x?? 】     
                      * 【 2，3，4，5 |	S，H，[O，K 或N，O]】     
                      * 【 6，7 | 	(用0填充) 】 
                      * 【 8 、9，10，11|	 (用0填充)】 
                      * 【 12，13，(用0填充)  】
                      * 【 14、15、16、17 、18 | V S 8 0 0】
                      * 【19校验】
           **************************************************************************************************************/
            get
            {
                byte[] cmd = new byte[] {
                    0xbb,
                    (byte)'S',(byte)'H',(byte)'V',(byte)'8',
                    0, 0,
                    0, 0, 0, 0,
                    0, 0,
                    (byte)'V',(byte)'B',(byte)'T',(byte)'8',(byte)'A',
                    (byte)'?'
                };

                return cmd;
            }
        }
        #endregion

        #region 一般命令

        /// <summary>
        /// 重启命令
        /// </summary>
        public static byte[] ReStart(int sn)
        {
            byte[] buffer = BitConverter.GetBytes(sn);
            byte[] cmd =
                {
                    (byte) 0xbb,
                    (byte) 'R', (byte) 'S', (byte) 'V', (byte) '8',
                    buffer[0], buffer[1],
                    0,0,0,0,
                    0,0,
                    (byte)'V',(byte)'S',(byte)'8',(byte)'0',(byte)'0',
                    (byte)'?'
                };


            return cmd;
        }

        /// <summary>
        /// 断开
        /// </summary>
        /// <returns></returns> 
        public static byte[] SiteOff(int sn)
        {

            byte[] buffer = BitConverter.GetBytes(sn);
            byte[] cmd =
                {
                    (byte) 0xbb,
                    (byte) 'D', (byte) 'C', (byte) 'V', (byte) '8',
                    buffer[0], buffer[1],
                    0, 0, 0, 0,
                    0, 0,
                    (byte) 'V', (byte) 'S', (byte) '8', (byte) '0', (byte) '0',
                    (byte) '?'
                };
            return cmd;
        }

        /// <summary>
        /// 置空闲状态
        /// </summary>
        /// <returns></returns>
        public static byte[] SetStop(int sn)
        {
            byte[] buffer = BitConverter.GetBytes(sn);
            byte[] cmd =
                {
                    (byte) 0xbb,
                    (byte) 'D', (byte) 'C', (byte) 'V', (byte) '8',
                    buffer[0], buffer[1],
                    0,0,0,0,
                    0,0,
                    (byte)'V',(byte)'S',(byte)'8',(byte)'0',(byte)'0',
                    (byte)'?'
                };

            return cmd;
        }

        /// <summary>
        /// 开启电源命令
        /// </summary>
        /// <returns></returns>
        public static byte[] PowerOpen()
        {
            byte[] buffer = BitConverter.GetBytes(0);
            byte[] cmd =
            {
                (byte)'$',
                (byte)'S',
                buffer[0],buffer[1],buffer[2],buffer[3],
                buffer[0],buffer[1],buffer[2],buffer[3],
                buffer[0],buffer[1],buffer[2],buffer[3],
                buffer[0],buffer[1],buffer[2],buffer[3],
                (byte)'?'
            };
            return cmd;
        }

        #endregion

        #region 数据获取命令

        /// <summary>
        /// 置站点为空闲状态
        /// </summary>
        /// <returns></returns>
        public static byte[] SetSiteLeisure()
        {
            /********************************************站点时间更新命令：*******************************************
          * 【 1	  |  $ 】     0x??
          * 【 2，3，4，5】     P，P，U，P  ASCII码等于(80，80，66，80)
          * 【 6，7】          保留字节，用0填充
          * 【8，9，10，11】     保留字节，用0填充
          * 【12，13】          保留字节，用0填充 
          * 【14】                    P：准备采集
          * 【15，16，17，18】       测量参数准备上传命令用0填充；
          * 【19】                      服务器自动采集： M：电量参数
          * 【20】                0
           *【21】                    校验
          **************************************************************************************************************/
            byte[] cmd =
            {
                0xbb,
                (byte) 'P', (byte) 'P', (byte) 'U', (byte) 'P',
                0, 0,
                0, 0, 0, 0,
                0, 0,
                (byte)'P', //第14位
                0, 0, 0, 0,
                (byte)'M',
                0,
                (byte) '?'
            };
            return cmd;
        }

        /// <summary>
        /// 振动总值参数提取
        /// </summary>
        /// <param name="channelNum"></param>
        /// <param name="isGetData">true获取总值数据，false获取通道数据的个数</param>
        /// <returns></returns>
        public static byte[] GetDataOaByChannelNum(int channelNum, bool isGetData)
        {
            /********************************************************************************************************************************
                    * 【1            |	0x??】
                    * 【2，3，4，5    |	P，P，U，P  ASCII码等于(80，80，66，80)】
                    * 【6，7         | 保留字节，用0填充】
                    * 【8,9,10,11    |	保留字节，用0填充】
                    * 【12,13        |	保留字节，用0填充】
                    * 【14           |4（振动总值参数提取）】
                    * 【15，16，17，18 |	组态 振动总值参数提取：自动运行监测的通道号（1到32）），发送PointSum次；振动总值参数取完之后发送命令用-1填充。】
                    * 【19 | 服务器自动采集： M：振动总值参数】
                    * 【20 | 0】
                    * 【21 | 校验】
             * *******************************************************************************************************************************/
            byte[] buffer = BitConverter.GetBytes(channelNum);
            byte[] cmd =
            {
                0xbb,
                (byte) 'P', (byte) 'P', (byte) 'U', (byte) 'P',
                0, 0,
                0, 0, 0, 0, //8,9,10,11 
                0, 0,
                0,//14  
                buffer[0], buffer[1], buffer[2], buffer[3], //15，16，17，18
                (byte)'M',
                0,
                (byte) '?'
            };
            cmd[13] = isGetData ? (byte)5 : (byte)4;
            return cmd;
        }

        /// <summary>
        /// 根据通道号采集波形十进制数据命令
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="type"></param>
        /// <param name="frequency"></param>
        /// <param name="line"></param>
        /// <param name="isPrepare">0代表准备 1代表取数据</param>
        /// <returns></returns>
        public static byte[] TwDataByChannel(int channelId, char typeName, int frequency, int line, int isPrepare)
        {
            /********************************************************************************************************************************
                    * 【1                |	0x??】
                    * 【2，3，4，5       |	P，P，U，P  ASCII码等于(80，80，66，80)】
                    * 【6，7              |  保留字节，用0填充】
                    * 【8,9,10,11        |	保留字节，用0填充】
                    * 【12               |	保留字节，用0填充】
                    * 【13               |	保留字节，用0填充】
                    * 【14               | 0获取准备命令（返回直接长度）1十进制数据采集 2获取单通道的波形数据 3获取采集板的信息】
                    * 【15，16，17，18    |	第几通道1，2，~，8。】
                    * 【19               | 采集的组号，总共4组1,2,3,4】
                    * 【20,21,22,23       | 采样点数的序号：1~6】
                    * 【24,25,26,27          | 采样频率的序号：1~16】
                    * 【28               | 24V电源是否开通：0不开通，1开通】
                    * 【29               | A：加速度 V：速度 D：位移 E：包络】
                    * 【30               | 校验】
             * *******************************************************************************************************************************/
            int num = channelId % 8 == 0 ? 8 : channelId % 8;
            int groupId = channelId % 8 == 0 ? channelId / 8 : channelId / 8 + 1;
            byte[] cmd = new byte[30];
            cmd[0] = 0xbb;
            cmd[1] = (byte)'P';
            cmd[2] = (byte)'P';
            cmd[3] = (byte)'U';
            cmd[4] = (byte)'P';
            cmd[13] = (byte)isPrepare; //十进制采集

            //通道号
            byte[] dataTemp = BitConverter.GetBytes(num);
            Array.Copy(dataTemp, 0, cmd, 14, 4);
            //组号
            cmd[18] = (byte)groupId;
            //采样点数
            dataTemp = BitConverter.GetBytes(line);
            Array.Copy(dataTemp, 0, cmd, 19, 4);
            //采样频率
            dataTemp = BitConverter.GetBytes(frequency);
            Array.Copy(dataTemp, 0, cmd, 23, 4);
            //是否开通电源
            cmd[27] = 1;
            //全通，包络
            cmd[28] = (byte)typeName;
            cmd[29] = (byte)'?';
            return cmd;
        }

        #endregion

        /// <summary>
        /// 分析名称获取类型、HZ
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static void GetTypeAndFrequency(string name, out int type, out int frequency)
        {
            string[] str = name.Replace("Hz", "").Split('_');
            type = 0;
            frequency = 0;
            #region 数据处理

            switch (str[0].ToLower())
            {
                case "disp":
                    type = 1; break;
                case "vel":
                    type = 2; break;
                case "acc":
                    type = 3; break;
                case "env":
                    type = 4; break;
            }

            if (type == 4)
            {
                switch (str[1].ToLower())
                {
                    case "1k":
                        frequency = 1;
                        break;
                    case "500":
                        frequency = 2;
                        break;
                }
            }
            else
            {
                switch (str[1].ToLower())
                {
                    case "40k":
                        frequency = 1;
                        break;
                    case "20k":
                        frequency = 2;
                        break;
                    case "10k":
                        frequency = 3;
                        break;
                    case "8k":
                        frequency = 4;
                        break;
                    case "5k":
                        frequency = 5;
                        break;
                    case "4k":
                        frequency = 6;
                        break;
                    case "2k":
                        frequency = 7;
                        break;
                    case "1.6k":
                        frequency = 8;
                        break;
                    case "1k":
                        frequency = 9;
                        break;
                    case "800":
                        frequency = 10;
                        break;
                    case "500":
                        frequency = 11;
                        break;
                    case "400":
                        frequency = 12;
                        break;
                    case "200":
                        frequency = 13;
                        break;
                    case "100":
                        frequency = 14;
                        break;
                    case "50":
                        frequency = 15;
                        break;
                }
            }

            #endregion
        }

        /// <summary>
        /// data是否包含groupNum通道号
        /// </summary>
        /// <param name="groupNum"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool IsCollect(int groupNum, int[] data)
        {
            int start = (groupNum - 1) * 8 + 1;
            int end = groupNum * 8 + 1;
            for (int i = start; i < end; i++)
            {
                if (data.Contains(i))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

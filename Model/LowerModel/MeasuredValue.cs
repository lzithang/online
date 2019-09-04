using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MeasuredValue
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int minute;
        public int second;  //oa_time
        public int WorkStatus;//工作状态值 增加一个存储变量
        public float SensorBias; //传感器偏置电压  增加一个存储变量
        public float Disp; //位移 单位为μm   oa_disp
        public float Speed; //测点转速 单位为Hz   oa_tacho
        public float rms_Acc_W; //加速度时域有效值  单位为g  
        public float rms_Acc_F; //加速度频率有效值  单位为g
        public float CF_Acc; //尖峰系数   oa_CF
        public float Kurtosis_Acc; //峭度   oa_kurt
        public float rms_Vel; //积分之后的速度有效值  单位为mm/s   oa_vel
        public float rms_Vel_BP; //带通之后的速度有效值 单位为mm/s   
        public float rms_HP_W; //高通波形的时域有效值 单位为g
        public float rms_Bg_HP; //高通1K-20K加速度有效值 单位为g  oa_bg
        public float rms_Bv_HP; //高通1K-20K积分之后速度有效值 单位为mm/s  oa_bv
        public float rms_Env; //包络有效值0-1000Hz  单位为g   oa_env
        public float SensorStaticValue; //测量值，代表温度，压力等静态量值
    }
}

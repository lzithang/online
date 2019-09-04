using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS.Common
{
    public class FFTHelper
    {
        private static float DDC_PI = 3.141593f;
        private static float abs_error = 0.0000000001f;
        private static float[] _hann256;
        private static float[] _hann512;
        private static float[] _hann1024;
        private static float[] _hann2048;
        private static float[] _hann4096;
        private static float[] _hann8192;
        private static float[] _hann16384;
        private static float[] _hann32768;
        private static float[] _hann65536;

        #region 字段
        public static float[] hann256
        {
            get
            {
                if (_hann256 == null)
                {
                    _hann256 = GetHannData(256);
                }
                return _hann256;
            }
        }


        public static float[] hann512
        {
            get
            {
                if (_hann512 == null)
                {
                    _hann512 = GetHannData(512);
                }
                return _hann512;
            }
        }



        public static float[] hann1024
        {
            get
            {
                if (_hann1024 == null)
                {
                    _hann1024 = GetHannData(1024);
                }
                return _hann1024;
            }
        }


        public static float[] hann2048
        {
            get
            {
                if (_hann2048 == null)
                {
                    _hann2048 = GetHannData(2048);
                }
                return _hann2048;
            }
        }


        public static float[] hann4096
        {
            get
            {
                if (_hann4096 == null)
                {
                    _hann4096 = GetHannData(4096);
                }
                return _hann4096;
            }
        }


        public static float[] hann8192
        {
            get
            {
                if (_hann8192 == null)
                {
                    _hann8192 = GetHannData(8192);
                }
                return _hann8192;
            }
        }

        public static float[] hann16384
        {
            get
            {
                if (_hann16384 == null)
                {
                    _hann16384 = GetHannData(16384);
                }
                return _hann16384;
            }
        }


        public static float[] hann32768
        {
            get
            {
                if (_hann32768 == null)
                {
                    _hann32768 = GetHannData(32768);
                }
                return _hann32768;
            }
        }


        public static float[] hann65536
        {
            get
            {
                if (_hann65536 == null)
                {
                    _hann65536 = GetHannData(65536);
                }
                return _hann65536;
            }
        }
        #endregion

        /// <summary>
        /// 加汉宁窗
        /// </summary>
        /// <param name="point">采样点</param>
        /// <param name="tw">波形数据</param>
        public static bool AddHann(int point, ref float[] tw)
        {
            float[] hann;
            switch (point)
            {
                case 256:
                    hann = hann256;
                    break;
                case 512: hann = hann512; break;
                case 1024: hann = hann1024; break;
                case 2048: hann = hann2048; break;
                case 4096: hann = hann4096; break;
                case 8192: hann = hann8192; break;
                case 16384: hann = hann16384; break;
                case 32768: hann = hann32768; break;
                case 65536: hann = hann65536; break;
                default:
                    return false;
                    break;
            }
            //加窗函数，系数直接调用以前的，AutoRunSN_AP为FFT处理的点数，低通为16384，高通是4096
            for (int fft_i = 0; fft_i < point; fft_i++)
            {
                tw[fft_i] = tw[fft_i] * hann[fft_i];
            }
            return true;
        }

        /// <summary>
        /// 波形转频谱
        /// </summary>
        /// <param name="NumSamples">波形点数</param>
        /// <param name="InverseTransform">是否反转 一般为false</param>
        /// <param name="RealIn">实部in</param>
        /// <param name="ImagIn">虚部in</param>
        /// <param name="RealOut">实部out</param>
        /// <param name="ImagOut">虚部out</param>
        public static void GetFFTData(int NumSamples, bool InverseTransform, ref float[] RealIn, ref float[] ImagIn, ref float[] RealOut, ref float[] ImagOut)
        {
            //  int IsPowerOfTwo(unsigned x);
            //int NumberOfBitsNeeded(unsigned PowerOfTwo);
            //int ReverseBits(unsigned index, unsigned NumBits);
            int NumBits;    /* Number of bits needed to store indices */
            int i, j, k, n;
            int BlockSize, BlockEnd;
            double angle_numerator = 2.0 * DDC_PI;
            double tr, ti;     /* temp real, temp imaginary */
            if (!IsPowerOfTwo(NumSamples))
            {
                return;
            }
            if (InverseTransform)
                angle_numerator = -angle_numerator;

            NumBits = NumberOfBitsNeeded(NumSamples);
            /*
            **   Do simultaneous data copy and bit-reversal ordering into outputs...
            */
            for (i = 0; i < NumSamples; i++)
            {
                j = ReverseBits(i, NumBits);
                RealOut[i] = RealIn[j];
                ImagOut[i] = (ImagIn.Length == 0) ? 0.0f : ImagIn[j];
            }
            /*
            **   Do the FFT itself...
            */
            BlockEnd = 1;
            for (BlockSize = 2; BlockSize <= NumSamples; BlockSize <<= 1)
            {
                double delta_angle = angle_numerator / (double)BlockSize;
                double sm2 = Math.Sin(-2 * delta_angle);
                double sm1 = Math.Sin(-delta_angle);
                double cm2 = Math.Cos(-2 * delta_angle);
                double cm1 = Math.Cos(-delta_angle);
                double w = 2 * cm1;
                double[] ai = new double[3];
                double[] ar = new double[3];
                for (i = 0; i < NumSamples; i += BlockSize)
                {
                    ar[2] = cm2;
                    ar[1] = cm1;
                    ai[2] = sm2;
                    ai[1] = sm1;
                    for (j = i, n = 0; n < BlockEnd; j++, n++)
                    {
                        ar[0] = w * ar[1] - ar[2];
                        ar[2] = ar[1];
                        ar[1] = ar[0];
                        ai[0] = w * ai[1] - ai[2];
                        ai[2] = ai[1];
                        ai[1] = ai[0];
                        k = j + BlockEnd;
                        tr = ar[0] * RealOut[k] - ai[0] * ImagOut[k];
                        ti = ar[0] * ImagOut[k] + ai[0] * RealOut[k];
                        RealOut[k] = RealOut[j] - (float)tr;
                        ImagOut[k] = ImagOut[j] - (float)ti;
                        RealOut[j] += (float)tr;
                        ImagOut[j] += (float)ti;
                    }
                }
                BlockEnd = BlockSize;
            }
            /*
            **   Need to normalize if inverse transform...
            */
            if (InverseTransform)
            {
                double denom = (double)NumSamples;
                for (i = 0; i < NumSamples; i++)
                {
                    RealOut[i] /= (float)denom;
                    ImagOut[i] /= (float)denom;
                }
            }
        }

        /// <summary>
        /// FFT to Spectrum
        /// </summary>
        /// <param name="RealIn"></param>
        /// <param name="ImagIn"></param>
        /// <param name="point"></param>
        /// <param name="Spectrum"></param>
        public static void CaculateSpectrum(ref float[] RealIn, ref float[] ImagIn, int point, ref float[] Spectrum)
        {
            double CorrectionFactor2 = 2.0;
            int i;
            float temp;
            temp = 0;
            for (i = 0; i < point / 2; i++)
            {
                Spectrum[i] = (float)(Math.Sqrt(Math.Pow(RealIn[i], 2) + Math.Pow(ImagIn[i], 2)) * 2 * CorrectionFactor2 / point);
                if ((i > 3) && (Spectrum[i] > temp))
                {
                    temp = Spectrum[i];
                    //MaxIndex = i;
                }
            }
        }

        public static float[] TwToFFT(float[] tw, int line)
        {
            int point = (int)(line * 2.56);
            float[] spectrum = new float[point / 2];
            float[] imagIn = new float[point];
            float[] realOut = new float[point];
            float[] imagOut = new float[point];
            AddHann(point, ref tw);
            GetFFTData(point, false, ref tw, ref imagIn, ref realOut, ref imagOut);
            CaculateSpectrum(ref realOut, ref imagOut, point, ref spectrum);

            return spectrum;
        }

        private static bool IsPowerOfTwo(int x)
        {
            if (x < 2)
                return false;
            if ((x & (x - 1)) > 0)        // Thanks to 'byang' for this cute trick!
                return false;
            return true;
        }

        private static int ReverseBits(int index, int numBits)
        {
            int i, rev;
            for (i = rev = 0; i < numBits; i++)
            {
                rev = (rev << 1) | (index & 1);
                index >>= 1;
            }
            return rev;
        }

        private static int NumberOfBitsNeeded(int PowerOfTwo)
        {
            int i;
            if (PowerOfTwo < 2)
            {

            }
            for (i = 0; ; i++)
            {
                if ((PowerOfTwo & (1 << i)) > 0)
                    return i;
            }
        }

        /// <summary>
        /// 获取窗数组
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private static float[] GetHannData(int point)
        {
            float[] hannData = new float[point];
            for (int i = 0; i < point; i++)
            {
                hannData[i] = (float)(0.5f * (1 - Math.Cos(2 * 3.1415926f * i / point)));
            }
            return hannData;
        }

        //*------------------------------------------------------------------------------------------------
        //*     函数名称：IntegrateFromSpectrum;
        //*     功能描述：从频率里积分；
        //*     入口参数 : <InWaveformValue>[in]  需要积分的波形
        //*                       : <SampleFre>[in] 波形数据的采样频率
        //*                       : <InPoint>[in] 输入波形数据的点数，为2的幂次方
        //*                       : <Fmin>[in] 最小截止频率
        //*                       : <Fmax>[in] 最大截止频率
        //*                       : <IntTimes>[in] 积分次数 1vel 2disp
        //*                       : <OutWaveformValue>[out]积分出的速度波形数据
        //*------------------------------------------------------------------------------------------------
        public static void IntegrateFromSpectrum(float[] InWaveformValue, int SampleFre, int InPoint, float Fmin, float Fmax, int IntTimes, ref float[] OutWaveformValue)
        {
            //计算频率间隔（Hz/s)
            float diffre = 0;
            diffre = (float)SampleFre / InPoint;
            //计算指定频带对应频率数组的下标
            int nmin, nmax;
            nmin = (int)(Fmin / diffre + 1);
            nmax = (int)(Fmax / diffre + 1);
            //计算圆频率间隔（rad/s）
            float difw;
            difw = 2 * DDC_PI * diffre;
            //建立正、负离散圆频率向量
            int i;
            float[] w = new float[32768];
            for (i = 0; i < (InPoint / 2 - 1); i++)
            {
                w[i] = i * difw;
                w[InPoint / 2 + i] = InPoint / 2 * difw - w[i];
            }
            //以积分次数为指数，建立圆频率变量向量
            if (IntTimes == 2)
            {
                for (i = 0; i < InPoint; i++)
                    w[i] = w[i] * w[i];
            }
            //对输入波形就行FFT变换
            //float ImagIn[32768] = {0};
            float[] RealOut = new float[32768];
            float[] ImagOut = new float[32768];
            float[] temp_real = new float[32768];
            float[] temp_image = new float[32768];
            GetFFTData(InPoint, false, ref InWaveformValue, ref temp_image, ref RealOut, ref ImagOut);
            for (i = 1; i < InPoint - 2; i++)
            {
                temp_real[i] = RealOut[i] / w[i];
                temp_image[i] = ImagOut[i] / w[i];
            }
            if (IntTimes == 2)
            {
                for (i = 1; i < InPoint - 2; i++)
                {
                    RealOut[i] = -temp_real[i];
                    ImagOut[i] = -temp_image[i];
                }
            }
            else
            {
                for (i = 1; i < InPoint - 2; i++)
                {
                    RealOut[i] = temp_image[i];
                    ImagOut[i] = -temp_real[i];
                }
            }
            RealOut[0] = 0;
            ImagOut[0] = 0;
            RealOut[InPoint - 1] = 0;
            ImagOut[InPoint - 1] = 0;
            for (i = 0; i < InPoint; i++)
            {
                if (((i > (nmin - 1)) && (i < (nmax - 1))) || ((i > (InPoint - nmax)) && (i < (InPoint - nmin))))
                {
                    temp_real[i] = RealOut[i];
                    temp_image[i] = ImagOut[i];
                }
                else
                {
                    temp_real[i] = 0;
                    temp_image[i] = 0;
                }
            }
            GetFFTData(InPoint, true, ref temp_real, ref temp_image, ref RealOut, ref ImagOut);
            if (IntTimes == 2)
            {
                for (i = 0; i < InPoint; i++)
                {
                    OutWaveformValue[i] = RealOut[i];
                }
            }
            else
            {
                for (i = 0; i < InPoint; i++)
                {
                    OutWaveformValue[i] = ImagOut[i];
                }
            }
        }


        //*------------------------------------------------------------------------------------------------
        //*     函数名称：IntegrateFromSpectrum1;
        //*     功能描述：频谱积分；
        //*     入口参数 : <InSpect>[in]  需要积分的频谱波形
        //*                       : <SampleFre>[in] 波形数据的采样频率
        //*                       : <InPoint>[in] 输入波形数据的点数
        //*                       : <IntTimes>[in] 积分次数，1为速度，2为位移
        //*                       : <OutSpect[]>[out]积分的频谱
        //*------------------------------------------------------------------------------------------------
        public static void IntegrateFromSpectrum1(float[] InSpect, int SampleFre, int InPoint, int IntTimes, ref float[] OutSpect)
        {
            int MaxPoint = 0;
            MaxPoint = (int)(InPoint / 2);

            int i;
            OutSpect[0] = 0;
            OutSpect[1] = 0;
            OutSpect[2] = 0;

            if (IntTimes == 1)
            {
                for (i = 3; i < MaxPoint; i++)
                {
                    OutSpect[i] = InSpect[i] / (2 * DDC_PI * i * SampleFre / InPoint);
                }
            }
            if (IntTimes == 2)
            {
                for (i = 3; i < MaxPoint; i++)
                {
                    OutSpect[i] = (float)(InSpect[i] / (Math.Pow(2 * DDC_PI * i * SampleFre / InPoint, 2)));
                }
            }
        }

        //*------------------------------------------------------------------------------------------------
        //*     函数名称：RemoveDC;
        //*     功能描述：去直流；
        //*     入口参数 : <InWaveformValue>[in]  需要计算的波形
        //*                       : <InPoint>[in] 输入波形数据的点数
        //*                       : <OutWaveformValue>[out] 返回去直流后的波形
        //*------------------------------------------------------------------------------------------------
        public static void RemoveDC(float[] InWaveformValue, int InPoint, ref float[] OutWaveformValue)
        {
            int i;
            float dc = 0;
            for (i = 0; i < InPoint; i++)  //去直流分量操作
            {
                dc = dc + InWaveformValue[i];
            }
            dc = dc / InPoint;
            for (i = 0; i < InPoint; i++)
            {
                OutWaveformValue[i] = InWaveformValue[i] - dc; //必须是减dc
            }
        }


        //*------------------------------------------------------------------------------------------------
        //*     函数名称：CalculateVibRMS;
        //*     功能描述：计算数组有效值数据；
        //*     入口参数 : <a>[in]  计算的数组
        //*                       : <point>[in] 数据计算的点数
        //*                       : <rms>[out] 计算出的有效值
        //*------------------------------------------------------------------------------------------------
        public static float CalculateVibRMS(float[] a, int point)
        {
            int i;
            float rms = 0;
            for (i = 0; i < point; i++)
                rms += a[i] * a[i];
            rms = (float)Math.Sqrt(rms / point);
            return rms;
        }
    }
}

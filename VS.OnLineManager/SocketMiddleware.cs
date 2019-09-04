using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace VS.OnLineManager
{
    public class SocketMiddleware
    {
        private Socket _socket;
        private object objRev = new object();
        private object objSend = new object();
        public SocketMiddleware(Socket socekt)
        {
            _socket = socekt;
            _socket.SendTimeout = 30000;//20秒
            _socket.ReceiveTimeout = 30000;//20秒
            _socket.ReceiveBufferSize = 2 * 1024 * 1024; //1M
            _socket.SendBufferSize = 2 * 1024 * 2024;//1M
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="data"></param>
        public bool Receive(byte[] data)
        {
            return Receive(data, data.Length);
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="nLen"></param>
        /// <returns></returns>
        private bool Receive(byte[] data, int nLen)
        {
            try
            {
                Monitor.Enter(objRev);
                int revLen = 0;
                int nTimer = 100;
                //_socket.ReceiveBufferSize = nLen;
                int btLen = nLen;
                while (--nTimer > 0)
                {
                    byte[] dataTemp = new byte[btLen];
                    int revLenTemp = _socket.Receive(dataTemp, btLen, SocketFlags.None);
                    Array.Copy(dataTemp, 0, data, revLen, revLenTemp);
                    revLen += revLenTemp;
                    btLen -= revLenTemp;
                    if (revLen >= nLen)
                        return true;
                    else
                    {
                        //等待没有完成的包
                        Thread.Sleep(100);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                //PrintHelper.Info("socket接收数据出现异常！{0}", ex);
                //LogHelper.Error(System.Reflection.MethodBase.GetCurrentMethod(), ex.Message);
                throw;
            }
            finally
            {
                Monitor.Exit(objRev);
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="data"></param>
        public bool Send(byte[] data)
        {
            try
            {
                Monitor.Enter(objSend);
                if (_socket.Send(data) > 0)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                //LogHelper.Error(System.Reflection.MethodBase.GetCurrentMethod(), ex.Message);
            }
            finally
            {
                Monitor.Exit(objSend);
            }
            return false;
        }

        public void Close()
        {
            _socket.Close();
        }
    }
}

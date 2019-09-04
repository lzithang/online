using Autofac;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using VS.Common;
using VS.IService;

namespace VS.OnLineManager
{
    public class HandshakeModule : IHandshakeModule
    {
        private IConnectLogService _connectLogServer;

        /// <summary>
        /// 握手业务
        /// </summary>
        /// <param name="revData"></param>
        /// <returns>握手后状态</returns>
        public SiteModel Handshake(SocketMiddleware socket)
        {
            SiteModel site = null;
            byte[] revData = new byte[15];
        Handshake:
            if (!socket.Send(OrderHelper.Handshake))
                return site;

            //站点反馈的命令           
            if (!socket.Receive(revData))
                return site;

            //判断握手是否正常
            if (revData[1] != 'S' || revData[2] != 'H' || revData[3] != 'O' || revData[4] != 'K')
                return site;

            //设置站点空闲状态
            if (revData[7] != 1)
            {
                //长时间波形采集中 返回
                if (revData[7] == 4)
                {
                    site = new SiteModel(revData);
                    SetSiteDatabase(site.Sn);
                    _connectLogServer.InsertEntity(new ConnectLog() { ClTime = DateTime.Now, SiteSn = site.Sn });
                    return site;
                }

                //置空闲失败
                if (!socket.Send(OrderHelper.SetSiteLeisure()))
                    return null;

                byte[] tempData = new byte[13];
                if (!socket.Receive(tempData))
                    return null;

                if (tempData[1] != 'P' || tempData[2] != 'P' || tempData[3] != 'O' || tempData[4] != 'K')
                    return null;

                Console.WriteLine("握手成功，置空闲状态！");
                Thread.Sleep(5000);
                goto Handshake; //重新握手
            }

            site = new SiteModel(revData);
            SetSiteDatabase(site.Sn);
            _connectLogServer.InsertEntity(new ConnectLog() { ClTime = DateTime.Now, SiteSn = site.Sn });
            return site;
        }

        /// <summary>
        /// 设置数据库信息
        /// </summary>
        private void SetSiteDatabase(int sn)
        {
            ClientInfo clientInfo = Program.ClientInfoList.FirstOrDefault(c => c.Sn == sn);
            CallContext.SetData("clientInfo", clientInfo);
            _connectLogServer = Program.container.Resolve<IConnectLogService>();
        }

    }
}

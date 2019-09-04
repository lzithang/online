using Autofac;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.IService;

namespace VS.OnLineManager
{
    /// <summary>
    /// 通信业务逻辑
    /// </summary>
    public class MessageBusiness
    {
        /// <summary>
        /// socket通信对象
        /// </summary>
        private SocketMiddleware _socket;
        public MessageBusiness(SocketMiddleware socket)
        {
            _socket = socket;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        public void ExcuteAnalyse()
        {
            try
            {
                IHandshakeModule handshakeModule = Program.container.Resolve<IHandshakeModule>();
                SiteModel site = handshakeModule.Handshake(_socket);
                if (site == null)
                {
                    Console.WriteLine("握手失败！");
                    return;
                }

                ISiteModule siteModule = Program.container.Resolve<ISiteModule>();
                siteModule.GetSiteDetails(ref site);

                if (site.HasOaData)
                {
                    IDataOaModule dataOaModule = Program.container.Resolve<IDataOaModule>();
                    dataOaModule.GetDataOa(site, _socket);
                }

                if (site.HasTwData)
                {
                    IDataTwModule dataTwModule = Program.container.Resolve<IDataTwModule>();
                    dataTwModule.GetDataTw(site, _socket);
                }

                #region 配置更新
                if (site.IsUpdate > 0)
                {
                    ISiteService siteService = Program.container.Resolve<ISiteService>();
                    IConfigModule configModule = Program.container.Resolve<IConfigModule>();
                    configModule._site = site;
                    configModule._sokect = _socket;
                    //更新配置
                    switch (site.IsUpdate)
                    {
                        case 1: //更新时间
                            configModule.UpdateTime();
                            break;

                        case 2: //更新配置
                            configModule.UpdateSiteConfig();
                            break;

                        case 3: //更新 时间 配置
                            configModule.UpdateTime();
                            configModule.UpdateSiteConfig();
                            break;

                        case 4: //更新 固件
                            configModule.UpdateSolid();
                            break;

                        case 5: // 更新 时间 固件
                            configModule.UpdateTime();
                            configModule.UpdateSolid();
                            break;

                        case 6: //更新 配置 固件
                            configModule.UpdateSiteConfig();
                            configModule.UpdateSolid();
                            break;

                        case 7: //更新 时间 配置 固件
                            configModule.UpdateTime();
                            configModule.UpdateSiteConfig();
                            configModule.UpdateSolid();
                            break;
                        default:
                            break;

                    }
                    Site mySite = siteService.Query(s => s.SiteSN == site.Sn).FirstOrDefault();
                    mySite.SiteUpdateTag = 0;
                    siteService.UpdateEntity(mySite);
                }
                #endregion

                //断开连接
                if (_socket.Send(OrderHelper.SiteOff(site.Sn)))
                {
                    byte[] dataTemp = new byte[13];
                    if (_socket.Receive(dataTemp))
                    {
                        Console.WriteLine(string.Format("与站点{0}断开成功！", site.Sn));
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LoggerHelper.Error(typeof(MessageBusiness), ex.Message, ex);
                Console.WriteLine(string.Format("执行业务逻辑出现异常！{0}", ex));
            }
            finally
            {
                _socket.Close();
            }
        }

    }
}

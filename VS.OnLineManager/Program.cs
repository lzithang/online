using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using VS.Common;
using VS.IService;
using AutoMapper;
using System.Collections.Generic;
using Model;
using Newtonsoft.Json;
using log4net.Repository;
using log4net;
using log4net.Config;
using System.Net.Mail;
using System.Text;

namespace VS.OnLineManager
{
    class Program
    {
        public static IContainer container;
        public static List<ClientInfo> ClientInfoList;
        public static Queue<EmailModel> EmailModelQueue = new Queue<EmailModel>();
        /// <summary>
        /// 日志仓储
        /// </summary>
        public static ILoggerRepository LoggerRepository { get; set; }
        public static ILoggerHelper LoggerHelper;

        static void Main(string[] args)
        {
            var basePath = Environment.CurrentDirectory;
            string redisIP = AppsettingsHelper.GetConfigString("RedisConfig", "IP");
            string redisPassword = AppsettingsHelper.GetConfigString("RedisConfig", "Password");
            LoggerRepository = LogManager.CreateRepository("BPDM.Logger");
            XmlConfigurator.Configure(LoggerRepository, new FileInfo("Log4net.config"));
            RedisHelper.Initialization(new CSRedis.CSRedisClient($"{redisIP},password={redisPassword},defaultDatabase=13,poolsize=50,ssl=false,writeBuffer=10240,prefix=key前辍"));

            IServiceCollection services = new ServiceCollection();
            services.AddAutoMapper();

            services.AddTransient<IHandshakeModule, HandshakeModule>();
            services.AddTransient<ISiteModule, SiteModule>();
            services.AddTransient<IDataOaModule, DataOaModule>();
            services.AddTransient<IDataTwModule, DataTwModule>();
            services.AddTransient<IExtractModule, ExtractModule>();
            services.AddTransient<IConfigModule, ConfigModule>();
            services.AddTransient<IOaAlarmModule, OaAlarmModule>();
            services.AddSingleton<ILoggerHelper, LogHelper>();
            services.AddTransient<IStopModule, StopModule>();

            ContainerBuilder builder = new ContainerBuilder();
            var servicesDllFile = Path.Combine(basePath, "Vs.Service.dll");
            var assemblysServices = Assembly.LoadFile(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerDependency();

            var repositoryDllFile = Path.Combine(basePath, "Vs.Repository.dll");
            var assemblysRepository = Assembly.LoadFile(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces().InstancePerDependency();

            builder.Populate(services);
            container = builder.Build();
            LoggerHelper = container.Resolve<ILoggerHelper>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (StreamReader sr = new StreamReader(Path.Combine(basePath, "clientInfo.json"),Encoding.GetEncoding("gb2312")))
            {
                string json = sr.ReadToEnd();
                ClientInfoList = JsonConvert.DeserializeObject<List<ClientInfo>>(json);
            }
            string address = AppsettingsHelper.GetConfigString("SocketServer", "Address");
            int port = Convert.ToInt32(AppsettingsHelper.GetConfigString("SocketServer", "port"));
            IPAddress ip = IPAddress.Parse(address);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //绑定IP地址：端口  
            serverSocket.Bind(new IPEndPoint(ip, port));
            //设定最多10个排队连接请求  
            serverSocket.Listen(10);

            //线程池的容量过去设置
            int workerThreads_max = 0;
            int completionPortThreads_max = 0;
            int workerThreads_min = 0;
            int completionPortThreads_min = 0;

            //获取当前系统最大、最小线程池容量
            ThreadPool.GetMaxThreads(out workerThreads_max, out completionPortThreads_max);
            ThreadPool.GetMinThreads(out workerThreads_min, out completionPortThreads_min);

            //自定义线程池容量
            workerThreads_max = 12;// 12;
            completionPortThreads_max = 12; //12;
            workerThreads_min = 5;// 5;
            completionPortThreads_min = 5;// 5;

            ThreadPool.SetMinThreads(workerThreads_min, completionPortThreads_min); // set min thread to 5
            ThreadPool.SetMaxThreads(workerThreads_max, completionPortThreads_max); // set max thread to 12
            ThreadPool.QueueUserWorkItem(SendEmail); //启动发送邮件
            while (true)
            {
                try
                {
                    Socket clientSocket = serverSocket.Accept();
                    Console.WriteLine($"{clientSocket.RemoteEndPoint}连接成功！时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                    SocketMiddleware socket = new SocketMiddleware(clientSocket);
                    ThreadPool.QueueUserWorkItem(ExcuteAnalyse, socket);
                }
                catch (Exception e)
                {
                    LoggerHelper.Error(typeof(Program), e.Message, e);
                    Console.WriteLine(e);
                }
            }
        }


        static void ExcuteAnalyse(object obj)
        {
            try
            {
                SocketMiddleware socket = obj as SocketMiddleware;
                MessageBusiness messageBusiness = new MessageBusiness(socket);
                messageBusiness.ExcuteAnalyse();
            }
            catch (Exception ex)
            {
                LoggerHelper.Error(typeof(Program), ex.Message, ex);
                Console.WriteLine(ex);
            }
            
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="obj"></param>
        static void SendEmail(object obj)
        {
            while (true)
            {
                if (EmailModelQueue.Count != 0)
                {
                    EmailModel model = EmailModelQueue.Dequeue();
                    try
                    {
                        MailAddress receiver = new MailAddress(model.ReceiverAddress, model.ReceiverName);
                        MailAddress sender = new MailAddress(model.SenderAddress, model.SenderName);
                        MailMessage message = new MailMessage();
                        message.From = sender;//发件人
                        message.To.Add(receiver);//收件人
                        message.Subject = model.Title;//标题
                        message.Body = model.Content;//内容
                        message.IsBodyHtml = true;//是否支持内容为HTML

                        #region 解决被服务器认为垃圾邮件
                        message.Headers.Add("X-Priority", "3");
                        message.Headers.Add("X-MSMail-Priority", "Normal");
                        message.Headers.Add("X-Mailer", "Microsoft Outlook Express 6.00.2900.2869");   //本文以outlook名义发送邮件，不会被当作垃圾邮件            
                        message.Headers.Add("X-MimeOLE", "Produced By Microsoft MimeOLE V6.00.2900.2869");
                        message.Headers.Add("ReturnReceipt", "1");
                        #endregion

                        SmtpClient client = new SmtpClient();
                        client.Host = "smtp.exmail.qq.com";
                        client.EnableSsl = true;//是否启用SSL
                        client.Port = 587;
                        client.Timeout = 10000;//超时
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(model.SenderAddress, model.SenderPassword);
                        client.Send(message);
                    }
                    catch (Exception ex)
                    {
                        LoggerHelper.Error(new object(), ex.Message, ex);
                    }
                }
                else
                {
                    Thread.Sleep(2000);
                }
                
            }
            
        }


    }
}

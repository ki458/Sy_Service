using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
namespace Sy_Service
{
    public partial class Service1 : ServiceBase
    {
        //获取端口
        public Service1()
        {
            InitializeComponent();
            int Secondreader = Convert.ToInt32(ConfigurationManager.ConnectionStrings["ShangYe.Data:Hour"].ToString().Trim());
            System.Timers.Timer timereader = new System.Timers.Timer();
            timereader.Elapsed += new System.Timers.ElapsedEventHandler(TimedEventBorrow);
            timereader.Interval = Secondreader * 1000;
            timereader.Enabled = true;
        }
        /// <summary>
        /// 对总馆的数据进行添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimedEventBorrow(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                #region   操作sqlsugar
                SqlSugarClient db = new SqlSugarClient(
               new ConnectionConfig()
               {
                   //ConnectionString = @"server=DESKTOP-FTUFS40\SQLEXPRESS;database=Sy04;uid=sa;pwd=90-=uiop",
                   ConnectionString = "Data Source=.;Initial Catalog=Sy_DataWallHengDong01;Integrated Security=True",
                   DbType = SqlSugar.DbType.SqlServer,//设置数据库类型
                   IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                   InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
               });
                //时间
                var afterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//获取到当前的时间
                var beforeTime = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss");//获取前一小时
                #endregion
                #region  荣桓城北分馆
                string L1 = ConfigurationManager.ConnectionStrings["ShangYe.Data.L1"].ToString().Trim();//获取第一个馆的IP
                //获取第一个馆的设备id，此步骤需要查询第一个馆的数据库，客流设备的ID(切记不能和主馆ID重复)
                #region  第一个ID
                int DeviceID = 2;//设备ID
                var url = L1 + "QuqeryList?SysCustDeviceId=" + DeviceID + "&beforeTime=" + beforeTime + "&afterTime=" + afterTime;
                //获取返回的结果集
                var result = HttpClient.Get(url);
                JsonReader reader = new JsonTextReader(new StringReader(result));
                var resp = "";
                while (reader.Read())
                {
                    resp = reader.Value.ToString();
                }
                if (resp != "[]")
                {
                    Newtonsoft.Json.Linq.JArray userAarray1 = Newtonsoft.Json.Linq.JArray.Parse(resp) as Newtonsoft.Json.Linq.JArray;
                    List<SysCustData> userListModel = userAarray1.ToObject<List<SysCustData>>();
                    foreach (var cus in userListModel)
                    {
                        var datas = new SysCustData()
                        {
                            SysCustDeviceId = DeviceID,//设备ID
                            D_Date = Convert.ToDateTime(cus.D_Date),//时间
                            D_InNum = Convert.ToInt32(cus.D_InNum),//进馆人次
                            D_OutNum = Convert.ToInt32(cus.D_OutNum),//出馆人次
                        };
                        db.Insertable(datas).InsertColumns(it => new { it.SysCustDeviceId, it.D_Date, it.D_InNum, it.D_OutNum }).ExecuteReturnIdentity();
                    }
                    LogHelper.InfoLog("荣桓分馆设备ID" + DeviceID + "|时间从" + beforeTime + "开始|到" + afterTime + "结束");
                }
                #endregion

                #region  第二个ID
                int DeviceIDA = 7;//设备ID
                var urlA = L1 + "QuqeryList?SysCustDeviceId=" + DeviceIDA + "&beforeTime=" + beforeTime + "&afterTime=" + afterTime;
                //获取返回的结果集
                var resultA = HttpClient.Get(urlA);
                JsonReader readerA = new JsonTextReader(new StringReader(resultA));
                var respA = "";
                while (readerA.Read())
                {
                    respA = readerA.Value.ToString();
                }
                if (respA != "[]")
                {
                    Newtonsoft.Json.Linq.JArray userAarray1A = Newtonsoft.Json.Linq.JArray.Parse(respA) as Newtonsoft.Json.Linq.JArray;
                    List<SysCustData> userListModelA = userAarray1A.ToObject<List<SysCustData>>();
                    foreach (var cusA in userListModelA)
                    {
                        var datasA = new SysCustData()
                        {
                            SysCustDeviceId = DeviceIDA,//设备ID
                            D_Date = Convert.ToDateTime(cusA.D_Date),//时间
                            D_InNum = Convert.ToInt32(cusA.D_InNum),//进馆人次
                            D_OutNum = Convert.ToInt32(cusA.D_OutNum),//出馆人次
                        };
                        db.Insertable(datasA).InsertColumns(it => new { it.SysCustDeviceId, it.D_Date, it.D_InNum, it.D_OutNum }).ExecuteReturnIdentity();
                    }
                    LogHelper.InfoLog("荣桓分馆设备ID" + DeviceIDA + "|时间从" + beforeTime + "开始|到" + afterTime + "结束");
                }
                #endregion
                #endregion

                #region  荣桓24小时分馆1
                //获取第一个馆的IP
                string L2 = ConfigurationManager.ConnectionStrings["ShangYe.Data.L2"].ToString().Trim();
                //获取第一个馆的设备id，此步骤需要查询第一个馆的数据库，客流设备的ID(切记不能和主馆ID重复)
                #region  第一个ID
                int DeviceID2 = 4;//设备ID 
                var url2 = L2 + "QuqeryList?SysCustDeviceId=" + DeviceID2 + "&beforeTime=" + beforeTime + "&afterTime=" + afterTime;
                //获取返回的结果集
                var result2 = HttpClient.Get(url2);
                JsonReader reader2 = new JsonTextReader(new StringReader(result2));
                var resp2 = "";
                while (reader2.Read())
                {
                    resp2 = reader2.Value.ToString();
                }
                if (resp2 != "[]")
                {
                    Newtonsoft.Json.Linq.JArray userAarray2 = Newtonsoft.Json.Linq.JArray.Parse(resp2) as Newtonsoft.Json.Linq.JArray;
                    List<SysCustData> userListMode2 = userAarray2.ToObject<List<SysCustData>>();
                    foreach (var cus2 in userListMode2)
                    {
                        var datas2 = new SysCustData()
                        {
                            SysCustDeviceId = DeviceID2,//设备ID
                            D_Date = Convert.ToDateTime(cus2.D_Date),//时间
                            D_InNum = Convert.ToInt32(cus2.D_InNum),//进馆人次
                            D_OutNum = Convert.ToInt32(cus2.D_OutNum),//出馆人次
                        };
                        db.Insertable(datas2).InsertColumns(it => new { it.SysCustDeviceId, it.D_Date, it.D_InNum, it.D_OutNum }).ExecuteReturnIdentity();
                    }
                    LogHelper.InfoLog("24小时1馆ID" + DeviceID2 + "|时间从" + beforeTime + "开始|到" + afterTime + "结束");
                }
                #endregion
                #endregion

                #region  荣桓24小时分馆3
                //获取第一个馆的IP
                string L3 = ConfigurationManager.ConnectionStrings["ShangYe.Data.L3"].ToString().Trim();
                //获取第一个馆的设备id，此步骤需要查询第一个馆的数据库，客流设备的ID(切记不能和主馆ID重复)
                #region  第一个ID
                int DeviceID3 = 6;//设备ID
                var url3 = L3 + "QuqeryList?SysCustDeviceId=" + DeviceID3 + "&beforeTime=" + beforeTime + "&afterTime=" + afterTime;
                //获取返回的结果集
                var result3 = HttpClient.Get(url3);
                JsonReader reader3 = new JsonTextReader(new StringReader(result3));
                var resp3 = "";
                while (reader3.Read())
                {
                    resp3 = reader3.Value.ToString();
                }
                if (resp3 != "[]")
                {
                    Newtonsoft.Json.Linq.JArray userAarray3 = Newtonsoft.Json.Linq.JArray.Parse(resp3) as Newtonsoft.Json.Linq.JArray;
                    List<SysCustData> userListMode3 = userAarray3.ToObject<List<SysCustData>>();
                    foreach (var cus3 in userListMode3)
                    {
                        var datas3 = new SysCustData()
                        {
                            SysCustDeviceId = DeviceID3,//设备ID
                            D_Date = Convert.ToDateTime(cus3.D_Date),//时间
                            D_InNum = Convert.ToInt32(cus3.D_InNum),//进馆人次
                            D_OutNum = Convert.ToInt32(cus3.D_OutNum),//出馆人次
                        };
                        db.Insertable(datas3).InsertColumns(it => new { it.SysCustDeviceId, it.D_Date, it.D_InNum, it.D_OutNum }).ExecuteReturnIdentity();
                    }
                    LogHelper.InfoLog("24小时2馆ID" + DeviceID3 + "|时间从" + beforeTime + "开始|到" + afterTime + "结束");
                }
                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("错误原因：" + ex.Message + "错误位置：" + ex.StackTrace);
            }
        }


        /// <summary>
        /// 服务启动
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {

            LogHelper.InfoLog("服务启动");
        }
        /// <summary>
        /// 程序关闭
        /// </summary>

        protected override void OnStop()
        {
            LogHelper.InfoLog("程序关闭");
        }
    }
}

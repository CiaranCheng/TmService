using TmService.Utils;
using System;
using System.Configuration;
using System.Linq;
using System.Data;
using TmService.Models;

namespace TmService.Services
{
    public class LoginService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static object LonntecLoginCheck(string user, string pwd)
        {
            DataBaseLayer db = new DataBaseLayer();
            DataSet ds = null;
            string sql = string.Format("SELECT F_ZGBH ,F_NAME FROM LSPASS WHERE F_ZGBH= '{0}' ", user);
            try
            {
                ds = db.Query(sql);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    return new { code = "-1", msg = "当前用户不存在！" };
                }
                else
                {
                    string username = ds.Tables[0].Rows[0][1].ToString();
                    string ztbh = ConfigurationManager.AppSettings["ztbh"];
                    string datasource = ConfigurationManager.AppSettings["DataSource"];
                    string connstr = string.Format("User ID=cw{0}{1};password={2};Data Source={3};", ztbh, user, pwd, datasource);
                    db = new DataBaseLayer(connstr, "SqlServer");
                    if (!db.TestConn())
                    {
                        return new { code = "-1", msg = "登陆失败" };
                    }
                    else
                    {                                                
                        string accesstoken = string.Empty;
                        TokenHelper.GetToken(new TokenModel( user), out accesstoken);
                        //redis.Set(string.Format("{0}_{1}_access_token", system, user), accesstoken, 0);                        
                        return new { code = "0", msg = "登录成功", token = accesstoken };
                    }

                }

            }
            catch (Exception e)
            {
                return new { code = "-1", msg = "程序异常:" + e.Message+"\r\n"+e.StackTrace };
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

        }
    }
}
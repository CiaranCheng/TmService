using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace TmService.Utils
{
    public class JsonHelper
    {
        /// <summary>
        /// 对象系列化为json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(Object obj)
        {
            var jser = new JavaScriptSerializer();
            string strJson = jser.Serialize(obj);
            return strJson;
        }
        /// <summary>
        /// json反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static object JsonToObj<T>(string strJson)
        {
            var jser = new JavaScriptSerializer();
            var obj = jser.Deserialize<T>(strJson);
            return obj;
        }

        /// <summary>DataTable序列化
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string SerializeDataTable(DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = DataTableToList(dt);
            return serializer.Serialize(list);//调用Serializer方法 
        }
        /// <summary>
        /// 单行DataSet序列化
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string SerializeFirstDataRow(DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            DataRow dr = dt.Rows[0];
            foreach (DataColumn dc in dt.Columns)
            {
                dic.Add(dc.ColumnName, dr[dc].ToString());
            }
            return serializer.Serialize(dic);//调用Serializer方法 
        }
        public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (dt != null)
                foreach (DataRow dr in dt.Rows)//每一行信息，新建一个Dictionary<string,object>,将该行的每列信息加入到字典
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        result.Add(dc.ColumnName, dr[dc].ToString());
                    }
                    list.Add(result);
                }
            return list;
        }
        /// <summary>DataTable反序列化
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static DataTable DeserializerTable(string strJson)
        {
            DataTable dt = new DataTable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            // var obj = serializer.DeserializeObject(strJson);//反序列化
            ArrayList arralList = serializer.Deserialize<ArrayList>(strJson);//反序列化ArrayList类型
            if (arralList.Count > 0)//反序列化后ArrayList个数不为0
            {
                foreach (Dictionary<string, object> row in arralList)
                {
                    if (dt.Columns.Count == 0)//新建的DataTable中无任何信息，为其添加列名及类型
                    {
                        foreach (string key in row.Keys)
                        {
                            dt.Columns.Add(key, row[key].GetType());//添加dt的列名
                        }
                    }
                    DataRow dr = dt.NewRow();
                    foreach (string key in row.Keys)//讲arrayList中的值添加到DataTable中
                    {

                        dr[key] = row[key];//添加列值
                    }
                    dt.Rows.Add(dr);//添加一行
                }
            }
            return dt;
        }
        //////////////
        public static object JsonToObject(string strJson)
        {
            var jser = new JavaScriptSerializer();
            return jser.DeserializeObject(strJson);
            // JavaScriptConvert.DeserializeObject(jsonString, obj.GetType());
        }
        public static List<T> JSONStringToList<T>(string JsonStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);

            return objs;
        }
        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

    }
}
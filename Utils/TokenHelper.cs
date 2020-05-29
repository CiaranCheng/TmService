using TmService.Models;
using TmService.Utils;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;


namespace TmService.Utils
{
    public class TokenHelper
    {
        private static string secret = "Token.Test.TTEOT";
       
        public static bool GetToken(TokenModel tokenModel, out string result)
        {
            //result = "";
            try
            {
                DateTime UTC = DateTime.Now;
                var payload = new Dictionary<string, object>
                {
                    {"sub",tokenModel.sub},
                    {"jti",tokenModel.jti },//JWT ID,JWT的唯一标识
                    {"iat", tokenModel.iat },//JWT颁发的时间
                    {"issuer",tokenModel.issuer},//签发者
                    {"audience", tokenModel.audience},//jwt的接收该方，非必须              
                    {"exp",tokenModel.exp},//JWT生命周期                           
                };
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
                result = encoder.Encode(payload, secret);
                //RedisHelper redis = new RedisHelper();
                //var a = redis.SetStringKey(EncryptionAlgorithm.EncryptString(result), result, TimeSpan.FromMinutes(5));
                return true;
            }
            catch (Exception )
            {
                // LogHelper.WriteErrorLog("DecodeJwtToken", $"{e}");
                result = "";
                return false;

            }
        }
    
        private static string GetDecodingToken(string strToken)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                //IDateTimeProvider provider = new UtcDateTimeProvider();
                //IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, urlEncoder);

                var json = decoder.Decode(strToken, secret, verify: true);
                return json;
            }
            catch (Exception)
            {
                //return "";
                throw;
            }
        }
       
     
        public static bool ValidateToken(string encryptToken, string user)
        {
            string decryptToken = string.Empty;
            try
            {
                //解析Token
                decryptToken = GetDecodingToken(encryptToken);
                TokenModel tm = JsonConvert.DeserializeObject<TokenModel>(decryptToken);
                //判断Token的绝对过期时间
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
                DateTime dt = startTime.AddMilliseconds(tm.exp);
                if (!tm.audience.Equals(user))
                {
                    return false;
                }
                if (dt > DateTime.Now)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
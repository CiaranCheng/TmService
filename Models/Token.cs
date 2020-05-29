using System;


namespace TmService.Models
{
    public class TokenModel
    {
        public string sub { get; set; }
        public string jti { get; set; }//JWT ID,JWT的唯一标识 Guid.NewGuid().ToString()
        public string iat { get; set; }//JWT颁发的时间
        public string issuer { get; set; }//签发者
        public string audience { get; set; }//jwt的接收该方，非必须       
        public double exp { get; set; } // JWT生命周期 
        public TokenModel() { }
        public TokenModel( string userid )
        {
            sub = "tm";
            jti = Guid.NewGuid().ToString();
            iat = DateTime.Now.ToString();
            issuer = "Inspur";
            // audience = "Duplo_"+ system+"_"+userid;
            audience = userid;
            //EDI accesstoken有效期1小时，refreshtoken有效期2h
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
           
                
            exp = (DateTime.Now.AddHours(1) - startTime).TotalMilliseconds;
                 
             
        }
    }
}
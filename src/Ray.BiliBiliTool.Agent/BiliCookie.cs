using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ray.BiliBiliTool.Config;
using Ray.BiliBiliTool.Config.Options;
using Ray.BiliBiliTool.Infrastructure;

namespace Ray.BiliBiliTool.Agent
{
    public class BiliCookie : CookieInfo
    {
        private readonly ILogger<BiliCookie> _logger;
        private readonly IConfiguration _configuration;
        private readonly BiliBiliCookieOptions _options;

        public BiliCookie(ILogger<BiliCookie> logger,
            IOptionsMonitor<BiliBiliCookieOptions> optionsMonitor,
            IConfiguration configuration) : base(optionsMonitor.CurrentValue.CookieStr)
        {
            _logger = logger;
            _configuration = configuration;
            _options = optionsMonitor.CurrentValue;

            CookieStr = _options.CookieStr ?? "";

            if (CookieStr.IsNotNullOrEmpty())
            {
                foreach (var str in CookieStr.Split(';'))
                {
                    if (str.IsNullOrEmpty()) continue;
                    var list = str.Split('=').ToList();
                    if (list.Count >= 2)
                        CookieDictionary[list[0].Trim()] = list[1].Trim();
                }
            }

            if (true)
            {
                UserId = "29117763";
            }
            if (true)
            {
                BiliJct = "ee39aa0c16fceacb1b63775e24e2dc9b";
            }
            if (true)
            {
                SessData = "d3283235%2C1659814400%2C79531%2A21";
            }

            Check();
        }

        [Description("DedeUserID")]
        public string UserId { get; set; }

        /// <summary>
        /// SESSDATA
        /// </summary>
        [Description("SESSDATA")]
        public string SessData { get; set; }

        [Description("bili_jct")]
        public string BiliJct { get; set; }

        /// <summary>
        /// 其他Cookies
        /// </summary>
        public string OtherCookies { get; set; } = "";

        /// <summary>
        /// 检查是否已配置
        /// </summary>
        /// <returns></returns>

        public override string ToString()
        {   
            if (CookieStr.IsNotNullOrEmpty()) return CookieStr;

            string re = (OtherCookies ?? "").Trim();
            if (!re.EndsWith(";")) re += ";";

            if (UserId.IsNotNullOrEmpty()) re += $" {GetPropertyDescription(nameof(UserId))}={UserId};";
            if (SessData.IsNotNullOrEmpty()) re += $" {GetPropertyDescription(nameof(SessData))}={SessData};";
            if (BiliJct.IsNotNullOrEmpty()) re += $" {GetPropertyDescription(nameof(BiliJct))}={BiliJct};";

            return re;
        }

        private string GetPropertyDescription(string propertyName)
        {
            return GetType().GetPropertyDescription(propertyName);
        }
    }
}

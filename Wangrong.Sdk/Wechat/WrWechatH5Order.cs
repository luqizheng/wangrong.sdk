using System;
using Wangrong.Sdk.Wechat;

namespace Wangrong.Sdk
{
    /// <summary>
    ///     H5 支付
    /// </summary>
    public class WrWechatH5Payment : BaseWechantAppOrder
    {
        public WrWechatH5Payment(string requireNo, string merNo, string orderNo, DateTime orderDate, int transAmt)
            : base(requireNo, merNo, "12", "0109", orderNo, orderDate, transAmt)
        {
        }
    }
}
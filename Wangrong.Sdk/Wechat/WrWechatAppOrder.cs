using System;
using Wangrong.Sdk.Wechat;

namespace Wangrong.Sdk
{
    /// <summary>
    ///     App
    ///     支付
    /// </summary>
    public class WrWechatAppOrder : BaseWechantAppOrder
    {
        public WrWechatAppOrder(string requireNo, string merNo, string orderNo, DateTime orderDate, int transAmt)
            : base(requireNo, merNo, "11", "0104", orderNo, orderDate, transAmt)
        {
        }
    }
}
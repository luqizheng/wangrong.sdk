using System;

namespace Wangrong.Sdk.Wechat
{
    public class WechatQuickResponseOrder : BaseQuickResponseOrder
    {
        public WechatQuickResponseOrder(string requireNo,
            string merNo, string orderNo, DateTime orderTime, int transAmt) :
            base(requireNo, merNo, orderNo, orderTime, transAmt, "10", "0108")
        {
        }
    }
}
using System;

namespace Wangrong.Sdk.Wechat
{
    /// <summary>
    /// </summary>
    public class WechatSwipeCodePayment : SwipeCodePayment
    {
        public WechatSwipeCodePayment(string requestNo, string merNo, string orderNo, DateTime orderDate,
            string autoCode, int transAmt)
            : base(requestNo, merNo, orderNo, orderDate, autoCode, "17", "0113", transAmt)
        {
        }
    }
}
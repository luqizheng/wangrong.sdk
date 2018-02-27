using System;

namespace Wangrong.Sdk.Alipay
{
    /// <summary>
    /// </summary>
    public class AlipaySwipeCodePayment : SwipeCodePayment
    {
        public AlipaySwipeCodePayment(string requestNo, string merNo, string orderNo,
            DateTime orderDate, string autoCode, int amt)
            : base(requestNo, merNo, orderNo, orderDate, autoCode, "17", "0120", amt)
        {
        }
    }
}
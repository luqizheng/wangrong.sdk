using System;

namespace Wangrong.Sdk.Alipay
{
    public class AlipayQuickResponseOrder : BaseQuickResponseOrder
    {
        public AlipayQuickResponseOrder
        (string requireNo, string merNo, string orderNo, DateTime orderDate,
            int transAmt)
            : base(requireNo, merNo, orderNo, orderDate, transAmt, "10", "0119")
        {
        }
    }
}
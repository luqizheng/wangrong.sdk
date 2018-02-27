using System;

namespace Wangrong.Sdk
{
    /// <summary>
    ///     撤销是指撤销当天的交易，其他时间交易撤销始终报失败。
    /// </summary>
    public class WrCancelOrder : RevertTransaction
    {
        public WrCancelOrder(string requestNo, string merNo, string orderNo,
            string oriOrderNo, int amt, DateTime oriOrderDate)
            : base(requestNo, merNo, orderNo, oriOrderNo, oriOrderDate, "03", amt)
        {
        }
    }
}
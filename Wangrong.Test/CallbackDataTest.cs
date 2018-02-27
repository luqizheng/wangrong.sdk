using Wangrong.Sdk.Callback;
using Xunit;

namespace Wangrong
{
    public class CallbackDataTest
    {
        [Fact]
        public void Test()
        {
            var t =
                @"respCode=0000&transactionId=4200000013201710117326951517&orderDate=20171011&respDesc=交易成功&transAmt=100&productId=0113&orderNo=20171011000704&timeEnd=20171011000711&bankType=ABC_CREDIT&transId=17&signature=S5HzDXuyihW5HxDGAcCR1UPVj+ZL6+MWzpO6T1bHn5McD5zZYYWn8t9lwsei0EzZLZ5WVdUcDmTtHYOlhNeFpcNWBKDK4/JHg7JIEe/on2k1O1oXlXTuMZVzqCyZZmTWv/eQo1PWVvKoz3/XvNAmlJ/Hks2nJnq/U7sm1kDKvJtLacPqoyD/2bZkeLhSaRE3ECjAvrNiqe/iFRiHFxZNPOREl3py2bj53eGv1giMJjYUPM6Aaja5R3gTP7alJ8mrwjmQV1/+jlhljMTqnO+gsm/Sm/lI/tdH2J6jLpicNKCJDIgdQqqA8J0Tg65sZz8KHzTkA3mgpoq1Ze0f+sIQdg==&merNo=310440300017218&orderId=10047514499";

            var d = new CallbackData();
            d.FillBy(t);

            Assert.Equal(d.TransactionId, "4200000013201710117326951517");
        }
    }
}
using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Wangrong.Sdk.Callback;
using Wangrong.Sdk.Merchants;
using Wangrong.Sdk.Merchants.JsonEntity;
using Wangrong.Sdk.Utils;
using Wangrong.Sdk.Wechat;

namespace Wangrong.Sdk
{
    public class WangrongService
    {
        private readonly HttpRequestHelper _http;
        private readonly ILogger _logger;
        private readonly Signature _signature;

        private readonly JsonSerializerSettings setting = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public WangrongService(ILogger<WangrongService> logger, string url, Signature signature)
        {
            if (url == null)

                throw new ArgumentNullException(nameof(url));
            _http = new HttpRequestHelper(url, logger);
            _signature = signature;
            _logger = logger;
        }


        /// <summary>
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="dateTime"></param>
        /// <param name="_gatewayProtocol"></param>
        /// <returns></returns>
        public OrderSearchResult Search(OrderSearch trans)
        {
            if (trans == null)
                throw new ArgumentNullException(nameof(trans));

            ValidateTransaction(trans);
            return _http.Post<OrderSearchResult>(trans, _signature);
        }

        /// <summary>
        /// </summary>
        /// <param name="requireNo"></param>
        /// <param name="orderNo"></param>
        /// <param name="oriOrderNo"></param>
        /// <param name="amount">单位：分</param>
        /// <param name="oriOrderDate"></param>
        /// <param name="gatewayProtocol"></param>
        /// <returns></returns>
        public CancelOrderResult Cancel(WrCancelOrder trans)
        {
            if (trans == null)
                throw new ArgumentNullException(nameof(trans));
            ValidateTransaction(trans);
            ValidateRevertOrder(trans);
            return _http.Post<CancelOrderResult>(trans, _signature);
        }

        /// <summary>
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="oriOrderNo"></param>
        /// <param name="amount"></param>
        /// <param name="refundReason"></param>
        /// <param name="gatewayProtocol"></param>
        /// <returns></returns>
        public RefundOrderResult Refund(RefundTranscation trans)
        {
            if (trans == null)
                throw new ArgumentNullException(nameof(trans));
            ValidateTransaction(trans);
            ValidateRevertOrder(trans);
            var result = _http.Post<RefundOrderResult>(trans, _signature);
            return result;
        }


        public OfficalAccountPaymentResult Pay(OfficialAccountPayment payment)
        {
            if (string.IsNullOrEmpty(payment.OpenId))
                throw new ArgumentNullException(nameof(payment), "payment.OpenId can't be null.");
            ValidatePayment(payment);
            ValidateTransaction(payment);
            var result = _http.Post<OfficalAccountPaymentResult>(payment, _signature);

            return result;
        }

        /// <summary>
        ///     微信或者支付宝扫码支付
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="gatewayProtocol"></param>
        /// <returns></returns>
        public SwipeCodeResult Pay(SwipeCodePayment payment)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));
            if (string.IsNullOrEmpty(payment.AutoCode))
                throw new ArgumentException(nameof(payment), "payment.Auto is empty.");
            ValidatePayment(payment);
            ValidateTransaction(payment);
            if (string.IsNullOrEmpty(payment.ReturnUrl))
                throw new ArgumentException(nameof(payment), "ReturnUrl为空");

            var result = _http.Post<SwipeCodeResult>(payment, _signature);

            return result;
        }

        /// <summary>
        ///     生成H5 支付的链接
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public WrWechatH5OrdereResult GetH5PayUrl(WrWechatH5Payment trans)
        {
            if (trans == null)
                throw new ArgumentNullException(nameof(trans));
            ValidatePayment(trans);
            ValidateTransaction(trans);

            var result = _http.Post<WrWechatH5OrdereResult>(trans, _signature);
            return result;
        }

        /// <summary>
        ///     获取Wechant App的支付
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public WrWechatAppOrderResult GetWechatAppPayUrl(WrWechatAppOrder trans)
        {
            if (trans == null)
                throw new ArgumentNullException(nameof(trans));
            ValidatePayment(trans);
            ValidateTransaction(trans);

            var result = _http.Post<WrWechatAppOrderResult>(trans, _signature);
            return result;
        }


        public QuickResponseOrderResult Pay(BaseQuickResponseOrder trans)
        {
            if (trans == null)
                throw new ArgumentNullException(nameof(trans));
            if (string.IsNullOrEmpty(trans.ReturnUrl))
                throw new ArgumentException(nameof(trans), "ReturnUrl为空");
            ValidatePayment(trans);
            ValidateTransaction(trans);


            var result = _http.Post<QuickResponseOrderResult>(trans, _signature);

            return result;
        }

        public CallbackData Callback(string paymentStatus)
        {
            _logger.LogInformation("callback收到的信息:" + paymentStatus);
            var dict = HttpRequestHelper.CoverToResult(paymentStatus);


            var callbakc = new CallbackData();
            callbakc.FillBy(dict);
            _logger.LogDebug("callback-back TransactionId:" + callbakc.TransactionId);
            return callbakc;
        }

        public SignInResult SignIn(SignIn data, contactInfo contactInfo
            , address_info addressInfo, bankcard_info bankInfo = null)
        {
            data.contactInfo = JsonConvert.SerializeObject(contactInfo, Formatting.None, setting);
            data.addressInfo = JsonConvert.SerializeObject(addressInfo, Formatting.None, setting);
            data.bankCardInfo = bankInfo != null ? JsonConvert.SerializeObject(bankInfo, Formatting.None, setting) : "";
            var result = _http.Post<SignInResult>(data, _signature);

            return result;
        }

        public string CallbackResponseText(bool isSuccss)
        {
            return isSuccss ? "SUCCESS" : "FAIL";
        }

        private void ValidateTransaction(TransactionBasicInfo trans)
        {
            if (string.IsNullOrEmpty(trans.MerNo))
                throw new WangrongException("0002");

            if (string.IsNullOrEmpty(trans.RequestNo))
                throw new ArgumentException(nameof(trans), "RequestNo为空");
        }

        private void ValidateRevertOrder(RevertTransaction trans)
        {
            if (string.IsNullOrEmpty(trans.ReturnUrl))
                throw new ArgumentException(nameof(trans), "ReturnUrl为空");

            if (string.IsNullOrEmpty(trans.NotifyUrl))
                throw new ArgumentException(nameof(trans), "NotifyUrl为空");
        }

        private void ValidatePayment(WangrongPayment trans)
        {
            if (string.IsNullOrEmpty(trans.ClientIp))
                throw new ArgumentNullException(nameof(trans), "ClientIp不能为空");
            if (string.IsNullOrEmpty(trans.NotifyUrl))
                throw new ArgumentNullException(nameof(trans), "NotifyUrl不能为空");

            if (string.IsNullOrEmpty(trans.CommodityName))
                throw new WangrongException("0001", "参数commodityName不能为空");
            if (trans.TransAmt == 0)
                throw new WangrongException("0001", "参数TransAmt不能为空");

            if (string.IsNullOrEmpty(trans.SubMchId))
                throw new ArgumentNullException(nameof(trans), "二级商户名SubMchId不能为空");
        }
    }
}
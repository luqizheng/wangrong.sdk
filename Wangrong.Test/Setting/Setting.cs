using Wangrong.Sdk.Utils;

namespace Wangrong.Setting
{
    /*
         * 
    #merchant_no=800440054111002
    #trans_url=http://113.107.235.97:9080/payment-gate-web/gateway/api/backTransReq
    #public_key_path=C:/cer/800440054111002_pub.pem
    #private_key_path=C:/cer/800440054111002_prv.pem
    #private_key_pfx_path=C:/cer/800440054111002.pfx
    #private_key_pwd=840994050940

    merchant_no=800440054111002
    trans_url=http://121.201.32.197:9080/payment-gate-web/gateway/api/backTransReq
    public_key_path=C:/cer/800440054111002_pub.pem
    private_key_path=C:/cer/800440054111002_prv.pem
    private_key_pfx_path=C:/cer/800440054111002.pfx
    private_key_pwd=095839960853



    agent_merchant_no=100003
    agent_public_key_path=C:/cer/100003_pub.pem
    agent_private_key_path=C:/cer/100003_prv.pem
    agent_private_key_pfx_path=C:/cer/100003.pfx
    agent_private_key_pwd=630238276732



    */
    public class Setting
    {
        private Signature _signature;

        public Setting(string name)
        {
        }

        /// <summary>
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// </summary>
        public string CerficationPath { get; set; }

        /// <summary>
        /// </summary>
        public string MerchantNumber { get; set; }

        /// <summary>
        ///     微信二级商户号
        /// </summary>
        public string WechatSubMerchant { get; set; }

        /// <summary>
        ///     支付宝二级商户号
        /// </summary>
        public string AlipaySubMerchant { get; set; }

        /// <summary>
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// </summary>
        public string ReturnUrl { get; set; }

        public string MerchantRequestUrl { get; set; }

        public Signature SignatureHelper
        {
            get
            {
                _signature = new Signature(CerficationPath, Password);
                return _signature;
            }
        }


        public string AgentId { get; set; }
    }
}
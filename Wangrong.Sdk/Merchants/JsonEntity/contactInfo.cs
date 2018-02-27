namespace Wangrong.Sdk.Merchants.JsonEntity
{
    public class contactInfo
    {
        public string name { get; set; }
        public string mobile { get; set; }

        public string phone { get; set; }
        public string email { get; set; }

        /// <summary>
        ///     法人：LEGAL_PERSON
        ///     实际控制人:CONTROLLER
        ///     代理人：AGENT
        ///     代理人：OTHER
        /// </summary>
        public string type { get; set; }

        public string id_card_no { get; set; }
    }

    public class address_info
    {
        public string province_code { get; set; }

        public string city_code { get; set; }

        public string district_code { get; set; }

        public string address { get; set; }

        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class bankcard_info
    {
        public string card_no { get; set; }
        public string card_name { get; set; }
    }
}
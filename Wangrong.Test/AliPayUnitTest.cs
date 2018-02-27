using Ornament.PaymentGateways.Gateways.Wangrong.Utils;
using Wangrong.Setting;
using Xunit;

namespace Wangrong
{
    public class AliPayUnitTest
    {
        [Fact]
        public void Pay1()
        {
            var str = "kdjfkdjfkdjfkdjsakfwjie1";

            var actual = new Signature(SettingHelper.Setting().CerficationPath, SettingHelper.Setting().Password);
            var priKey = actual.PrivateSignData(str);
            var a = actual.VerifySigntureByPrivateKey(str, priKey);
            Assert.True(a);
        }

        [Fact]
        public void Pay()
        {
            var str = "kdjfkdjfkjd";

            var actual = new Signature(SettingHelper.Setting().CerficationPath, SettingHelper.Setting().Password);
            var priKey = actual.PrivateSignData(str);
            var a = actual.VerifySigntureByPrivateKey(str, priKey);
            Assert.True(a);

        }

        [Fact]
        public void PubEncrypt_PrivateEncy()
        {
            var str = "kdjfkdjfkjd";

            var actual = new Signature(SettingHelper.Setting().CerficationPath, SettingHelper.Setting().Password);
            var priKey = actual.PublicKeyEncrypt(str);

            var c = actual.PrivateKeyDecrypt(priKey);
            //var a = actual.VerifySigntureByPrivateKey(str, priKey);
            //Assert.True(a);

        }

        [Fact]
        public void PriEncrypt_PubEncy()
        {
            var str = "kdjfkdjfkjd";

            var actual = new Signature(SettingHelper.Setting().CerficationPath, SettingHelper.Setting().Password);
            var priKey = actual.PrivateKeyEncrypt(str);

            var c = actual.PublicKeyDecrypt(priKey);
            //var a = actual.VerifySigntureByPrivateKey(str, priKey);
            //Assert.True(a);

        }
    }
}
using Wangrong.BatchSubMerchant;

namespace WanrongSubMerchant.Consol
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sql = false;
            var str = "d:\\妍丽导入\\重新生成wecha-70070029t.xlsx";
            if (!sql)
            {
                //var str = "d:\\2.xlsx";
                var importor = new WangrongBatchImport();
                var c = importor.Import(str, false, true);
                importor.Write(c, "d:\\重新生成wecha-70070029t.xlsx");
            }
            else
            {
                //var str = "d:\\2.xlsx";
                var importor = new WangrongBatchImport();

                importor.BuildSql(str, "d:\\妍丽商户(47家新增).sql");
            }
        }
    }
}
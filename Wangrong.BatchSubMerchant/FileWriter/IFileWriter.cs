using System;
using System.Collections.Generic;
using Npoi.Core.SS.UserModel;

namespace Wangrong.BatchSubMerchant.FileWriter
{
    public interface IFileWriter<T>
    {
        void Write(string file, Action<IRow> title, Action<IRow, int, T> addToExcel, IEnumerable<T> dataForWrite,
            string sheetName = "sheet");
    }
}
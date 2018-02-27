using System;
using System.Collections.Generic;
using System.IO;
using Npoi.Core.HSSF.UserModel;
using Npoi.Core.SS.UserModel;
using Npoi.Core.XSSF.UserModel;

namespace Wangrong.BatchSubMerchant.FileWriter
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelFileWriter<T> : IFileWriter<T>
    {
        private readonly bool _forExcel2003;

        public ExcelFileWriter(bool forExcel2003)
        {
            _forExcel2003 = forExcel2003;
        }

        public void Write(string file, Action<IRow> title, Action<IRow, int, T> addToExcel, IEnumerable<T> dataForWrite,
            string sheetName = "sheet")
        {
            var writeBook = GetWorkbook();

            var sheet = writeBook.CreateSheet(sheetName);
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);

            title(row);

            rowIndex++;
            foreach (var item in dataForWrite)
            {
                row = sheet.CreateRow(rowIndex);
                addToExcel(row, rowIndex, item);
                rowIndex++;
            }
            Stream writeStream = File.Create(file);
            writeBook.Write(writeStream);
        }

        private IWorkbook GetWorkbook()
        {
            var workbook = _forExcel2003
                ? new HSSFWorkbook()
                : (IWorkbook) new XSSFWorkbook();
            return workbook;
        }
    }
}
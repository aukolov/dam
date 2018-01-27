using System.Data;
using System.IO;
using Dam.Domain;
using ExcelDataReader;

namespace Dam.Infrastructure.Excel
{
    public class ExcelReader : IExcelReader
    {
        static ExcelReader()
        {
            System.Text.Encoding.RegisterProvider(
                System.Text.CodePagesEncodingProvider.Instance);
        }

        public DataSet Read(byte[] excelBytes)
        {
            using (var memoryStream = new MemoryStream(excelBytes))
            {
                var reader = ExcelReaderFactory.CreateReader(memoryStream);
                return reader.AsDataSet();
            }
        }
    }
}
using System.Data;
using System.IO;
using ExcelDataReader;

namespace Dam.Infrastructure
{
    public class ExcelReader
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
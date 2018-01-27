using System.Data;

namespace Dam.Domain
{
    public interface IExcelReader
    {
        DataSet Read(byte[] excelBytes);
    }
}
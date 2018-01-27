using System.Data;

namespace Dam.Domain
{
    public interface IDataSetToDamSnapshotTranslator
    {
        DamSnapshot[] Translate(DataSet dataSet);
    }
}
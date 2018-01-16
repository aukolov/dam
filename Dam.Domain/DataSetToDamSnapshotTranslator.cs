using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dam.Domain
{
    public class DataSetToDamSnapshotTranslator
    {
        private readonly int[] _rowsWithDams = {
            16, 17, 18, 19, 20, 21, 22, 23,
            26, 27, 28,
            31, 32, 33, 34,
            37, 38, 39
        };

        private const int DateRowIndex = 9;
        private const int DateColumnIndex = 11;

        private const int NameColumnIndex = 1;
        private const int CapacityColumnIndex = 4;
        private const int StorageColumnIndex = 7;

        public DamSnapshot[] Translate(DataSet dataSet)
        {
            if (dataSet.Tables.Count != 1)
            {
                throw new ArgumentException("DataSet must have exactly one table.");
            }

            var table = dataSet.Tables[0];
            if (table.Rows.Count <= _rowsWithDams.Union(new[] { DateRowIndex }).Max())
            {
                throw new ArgumentException("Not enough rows.");
            }

            var date = ParseDate(table);

            var dams = new List<DamSnapshot>();
            foreach (var rowIndex in _rowsWithDams)
            {
                var snapshot = ParseDam(table.Rows[rowIndex], date);
                dams.Add(snapshot);
            }

            return dams.ToArray();
        }

        private static DateTime ParseDate(DataTable table)
        {
            var dateRow = table.Rows[DateRowIndex];
            if (dateRow.ItemArray.Length <= DateColumnIndex)
            {
                throw new ArgumentException("Date column not found.");
            }

            var date = dateRow[DateColumnIndex] as DateTime?;
            if (date == null)
            {
                throw new Exception("Invalid date.");
            }

            var actualDate = date.Value;
            return actualDate;
        }

        private static DamSnapshot ParseDam(DataRow row, DateTime date)
        {
            if (row.ItemArray.Length <= new[] { NameColumnIndex, CapacityColumnIndex, StorageColumnIndex }.Max())
            {
                throw new ArgumentException("Not enough columns in dam row.");
            }

            var name = row[NameColumnIndex] as string;
            var capacity = row[CapacityColumnIndex] as double?;
            var storage = row[StorageColumnIndex] as double?;

            if (name == null)
            {
                throw new Exception("Invalid name.");
            }

            if (capacity == null)
            {
                throw new Exception("Invalid capacity.");
            }

            if (storage == null)
            {
                throw new Exception("Invalid storage.");
            }

            var damData = new DamEntity
            {
                Name = name,
                Capacity = (decimal) capacity
            };
            return new DamSnapshot
            {
                Dam = damData,
                Date = date,
                Storage = (decimal)storage
            };
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dam.Domain
{
    public class DataSetToDamSnapshotTranslator : IDataSetToDamSnapshotTranslator
    {
        private readonly IDamRepository _damRepository;

        private readonly int[] _rowsWithDams = {
            16, 17, 18, 19, 20, 21, 22, 23,
            26, 27, 28,
            31, 32, 33, 34,
            37, 38, 39
        };

        private const int DateRowIndex = 9;
        private const int DateColumnIndex = 11;

        private const int NameColumnIndex = 1;
        private const int StorageColumnIndex = 7;
        private const int LastYearsStorageColumnIndex = 9;

        public DataSetToDamSnapshotTranslator(IDamRepository damRepository)
        {
            _damRepository = damRepository;
        }

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
            var lastYearsDate = ParseDate(table).AddYears(-1);

            var dams = new List<DamSnapshot>();
            foreach (var rowIndex in _rowsWithDams)
            {
                var freshSnapshot = ParseDam(table.Rows[rowIndex], date, StorageColumnIndex);
                dams.Add(freshSnapshot);
                var lastYearsSnapshot = ParseDam(table.Rows[rowIndex], lastYearsDate, LastYearsStorageColumnIndex);
                dams.Add(lastYearsSnapshot);
            }
            Global.Logger.Info("Translated snapshots: {Snapshots}.", dams);

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
                throw new ArgumentException("Invalid date.");
            }

            var actualDate = date.Value;
            return actualDate;
        }

        private DamSnapshot ParseDam(DataRow row, DateTime date, int storageColumnIndex)
        {
            if (row.ItemArray.Length <= new[] { NameColumnIndex, storageColumnIndex }.Max())
            {
                throw new ArgumentException("Not enough columns in dam row.");
            }

            var name = row[NameColumnIndex] as string;
            var storage = row[storageColumnIndex] as double?;

            if (name == null)
            {
                throw new ArgumentException("Invalid name.");
            }

            if (storage == null)
            {
                throw new ArgumentException("Invalid storage.");
            }

            var dam = _damRepository.Items.SingleOrDefault(x => x.Name == name);
            if (dam == null)
            {
                throw new Exception("Dam not found.");
            }

            return new DamSnapshot
            {
                Dam = dam,
                Date = date,
                Storage = (decimal)storage
            };
        }
    }
}
using App.CongAnGis.Services.Business.ConverterProj;
using App.CongAnGis.Services.Business.ConverterProj.VN2000;
using ExcelDataReader;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CongAnGis.Services.Business.Reader
{
    internal class ExcelReader
    {
        private string _Prj;
        private string _PathFile;
        public ExcelReader(string pathSHPFile, string Prj = "4326")
        {
            this._PathFile = pathSHPFile;
            this._Prj = Prj;
        }
        public DataTable GetData()
        {
            DataTable dataSource = new DataTable();

            using (var stream = File.Open(this._PathFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {

                        // Gets or sets a callback to obtain configuration options for a DataTable. 
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    if (result.Tables.Count > 0)
                    {
                        dataSource = result.Tables[0];
                    }
                }
            }
            return dataSource;
        }
        public List<string> GetColumns()
        {
            List<string> fields = new List<string>();

            using (var stream = File.Open(this._PathFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {

                        // Gets or sets a callback to obtain configuration options for a DataTable. 
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    if (result.Tables.Count > 0)
                    {
                        fields = result.Tables[0].Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .Where(x => !x.Equals("SHAPE", StringComparison.CurrentCultureIgnoreCase))
                                 .ToList();
                    }
                }
            }
            return fields;
        }
    }
}

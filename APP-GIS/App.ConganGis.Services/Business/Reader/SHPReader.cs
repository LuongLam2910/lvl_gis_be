using App.CongAnGis.Services.Business.ConverterProj;
using App.CongAnGis.Services.Business.ConverterProj.VN2000;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CongAnGis.Services.Business.Reader
{
    internal class SHPReader
    {
        private string _Prj;
        private string _PathFile;
        public SHPReader(string pathSHPFile)
        {
            this._PathFile = pathSHPFile;
        }
        public DataTable GetData()
        {
            DataTable dataSource = new DataTable();
            dataSource.Columns.Add("SHAPE");
            var factory = GeometryFactory.Default;
            using var reader = new ShapefileDataReader(this._PathFile, factory);
            var defaultEncoding = reader.DbaseHeader.Encoding;
            List<string> fields = new List<string>();
            //đọc danh sách trường
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string name = reader.GetName(i);
                string fieldName = name.ToLower();

                //không đọc những trường meta
                if (fieldName != "geometry" && fieldName != "shape_leng"
                    && fieldName != "shape_area" && fieldName != "objectid")
                {
                    fields.Add(name);
                    dataSource.Columns.Add(name);
                }
            }

            int numberRecord = reader.RecordCount;
            var indexRecord = 0;
            while (indexRecord < numberRecord)
            {
                reader.Read();
                DataRow dr = dataSource.NewRow();
                Geometry geometry = reader.Geometry;
                var wktWriter = new WKTWriter();
                dr["SHAPE"] = wktWriter.Write(geometry);
                for (int i = 0; i < fields.Count; i++)
                {
                    string data = reader[fields[i]] + "";
                    var defaultEncodingVal = defaultEncoding.GetBytes(data);
                    string valueRow = Encoding.UTF8.GetString(defaultEncodingVal);
                    if (string.IsNullOrEmpty(valueRow))
                        dr[fields[i]] = DBNull.Value;
                    else
                        dr[fields[i]] = valueRow;
                }
                dataSource.Rows.Add(dr);
                indexRecord += 1;
            }
            return dataSource;
        }
        public List<string> GetColumns()
        {
            var factory = GeometryFactory.Default;
            using var reader = new ShapefileDataReader(this._PathFile, factory);
            var defaultEncoding = reader.DbaseHeader.Encoding;
            List<string> fields = new List<string>();
            //đọc danh sách trường
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string name = reader.GetName(i);
                string fieldName = name.ToLower();

                //không đọc những trường meta
                if (fieldName != "geometry" && fieldName != "shape_leng"
                    && fieldName != "shape_area" && fieldName != "objectid")
                {
                    fields.Add(name);
                }
            }
            return fields;
        }
    }
}

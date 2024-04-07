using App.CongAnGis.Services.Business.ConverterProj.VN2000;
using App.CongAnGis.Services.Business.ConverterProj;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Data;
using System.IO.Compression;
using System.IO;
using System.Text;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using OfficeOpenXml.Style;
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.HelperClasses;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Wordprocessing;


namespace App.CongAnGis.Services.Manager
{
    public class ExportManager
    {
        public static string DataTableToJSON(DataTable dt, string polygon = "")
        {
            string result = "[";
            foreach (DataRow dataRow in dt.Rows)
            {
                string temp = "";

                string shape = "";
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    temp += "\"" + dataColumn.ColumnName.ToLower() + "\":" + JsonConvert.SerializeObject(dataRow[dataColumn]) + ",";
                    if (dataColumn.ColumnName.ToLower() == "shape") shape = dataRow[dataColumn] + "";
                }
                bool valid = true;
                if (!string.IsNullOrEmpty(polygon) && !string.IsNullOrEmpty(shape))
                {
                    WKTReader wKT = new WKTReader();
                    Geometry source = wKT.Read(polygon);
                    Geometry dest = wKT.Read(shape);
                    if (!source.Intersects(dest)) valid = false;
                }
                if (valid)
                    result += "{" + temp;
                else continue;
                if (result.EndsWith(","))
                    result = result.Substring(0, result.Length - 1);
                result += "},";
            }
            if (result.EndsWith(","))
                result = result.Substring(0, result.Length - 1);
            result += "]";
            return result;
        }
        public static string DataTableToGeoJSON(DataTable dt)
        {
            StringBuilder result = new StringBuilder();
            result.Append("{" + '"' + "type" + '"' + ":" + '"' + "FeatureCollection" + '"' + "," + '"' + "features" + '"' + ":[");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow r_val = dt.Rows[i];
                string geojsonRow = "";
                string ShapeString = WKTToGeoJSON(r_val["SHAPE"].ToString());
                if (ShapeString != "")
                {
                    geojsonRow = "{\"type\":\"Feature\",\"geometry\":" + ShapeString + "," + "\"properties\":{";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.ToLower() == "shape") continue;
                        geojsonRow += "\"" + dt.Columns[j].ColumnName.ToLower() + "\": " + JsonConvert.SerializeObject(r_val[dt.Columns[j].ColumnName]);
                        geojsonRow += ",";
                    }
                    if (geojsonRow.EndsWith(","))
                        geojsonRow = geojsonRow.Substring(0, geojsonRow.Length - 1);
                    geojsonRow += "}}";
                    if (i < dt.Rows.Count - 1)
                        geojsonRow += ",";
                    result.Append(geojsonRow);
                }
            }
            string geojson = result.ToString();
            if (geojson.EndsWith(","))
                geojson = geojson.Substring(0, geojson.Length - 1);
            geojson += "]}";
            return geojson;
        }
        public static string WKTToGeoJSON(string geometry)
        {
            var check = "false";
            if (geometry.Contains("POINT") || geometry.Contains("POLYGON"))
            {
                check = "true";
            }
            if (string.IsNullOrEmpty(geometry) || geometry.ToLower() == "null" || check == "false")
                return "";
            NetTopologySuite.IO.WKTReader wktReader = new NetTopologySuite.IO.WKTReader();
            GeoJsonWriter geoJsonWriter = new GeoJsonWriter();
            Geometry geo = wktReader.Read(geometry);
            if (geo.Coordinates.Length > 0)
                return geoJsonWriter.Write(geo);
            else return "{\"type\": \"Point\",\"coordinates\": []}";
        }

        public static string ExportSHP(DataTable data, string folder, string fileName, string prj = "4326")
        {
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            DataTable dataTable = new DataTable();
            foreach (DataColumn dataColumn in data.Columns)
            {
                if (dataColumn.DataType == typeof(decimal))
                {
                    dataTable.Columns.Add(dataColumn.ColumnName, typeof(long));
                }
                else dataTable.Columns.Add(dataColumn.ColumnName);
            }
            foreach (DataRow dataRow in data.Rows)
            {
                DataRow dr = dataTable.NewRow();
                foreach (DataColumn dataColumn in data.Columns)
                {
                    dr[dataColumn.ColumnName] = dataRow[dataColumn];
                }
                dataTable.Rows.Add(dr);
            }
            List<Feature> features = new List<Feature>();
            var factory = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory();
            foreach (DataRow row in dataTable.Rows)
            {
                var attributes = new AttributesTable();
                Geometry geo = Geometry.DefaultFactory.CreateEmpty(Dimension.Unknown);
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    var dataType = dataColumn.DataType;
                    if (dataColumn.ColumnName.ToLower() == "shape")
                    {
                        NetTopologySuite.IO.WKTReader wktReader = new NetTopologySuite.IO.WKTReader();
                        string geoWKT = row[dataColumn] + "";
                        GeoJsonWriter geoJsonWriter = new GeoJsonWriter();
                        if (!string.IsNullOrEmpty(geoWKT))
                        {
                            geo = wktReader.Read(geoWKT);

                            if (geo.Coordinates.Length > 0)
                            {
                                //chuyển đổi hệ quy chiếu nếu có
                                if (prj != "4326")
                                {
                                    string wkt = VietnamCoordinateSystems.TransformWKTWGS84ToVN2000(geoWKT, ProjecttionHelper.ConvertCoordinateSystemsFromSrid(prj));
                                    geo = wktReader.Read(wkt);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (row[dataColumn] + "" != "")
                            attributes.Add(dataColumn.ColumnName, row[dataColumn]);
                        else attributes.Add(dataColumn.ColumnName, "");
                    }
                }

                if (!geo.IsEmpty)
                {
                    var feature = new Feature(geo, attributes);
                    features.Add(feature);
                }
            }
            var pathFile = string.Format("{0}\\{1}", folder, fileName);
            if (features.Count == 0) return null;
            var shpWriter = new ShapefileDataWriter(pathFile, factory, Encoding.UTF8)
            {
                Header = ShapefileDataWriter.GetHeader(features[0], features.Count, Encoding.UTF8)
            };
            shpWriter.Write(features);
            return GenerateFileZipByNameFile(folder, fileName);
        }
        public static string ExportGeoJson(DataTable data, string folder, string fileName)
        {
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            string fileStringPath = string.Format("{0}\\{1}.json", folder, fileName);
            var dataRepone = DataTableToGeoJSON(data);
            using StreamWriter sw = new StreamWriter(fileStringPath, false);
            sw.Write(dataRepone);
            sw.Close();
            return fileStringPath;
        }
        public static string ExportExcel(string fe,EntityCollection<SysfieldEntity> field,DataTable data, string folder, string fileName, string prj = "4326")
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 13;

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                
                workSheet.Cells[1, 1].Value = fe;
                workSheet.Cells[1, 1, 1, field.Count + 2].Merge = true;
                workSheet.Cells.Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 1].Style.Font.Size = 14;
                workSheet.Cells.Style.Font.Bold = true;
                workSheet.Cells[2, 1].Value = "STT";
                workSheet.Cells[2, 2].Value = "Tọa độ";
                workSheet.Cells[2, 1].Style.Font.Bold = true;
                workSheet.Cells[2, 2].Style.Font.Bold = true;
                workSheet.Cells[2, 1,2, field.Count + 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[2, 1, 2, field.Count + 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[2, 1, 2, field.Count + 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[2, 1, 2, field.Count + 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                
                int col1 = 3;
                foreach (var item in field)
                {
                    workSheet.Cells[2, col1].Style.Font.Bold = true;
                    workSheet.Cells[2, col1].Value = item.Name;
                    col1 += 1;
                }

                int rowIndex = 3;
                int colSTT = 1;
                
                foreach (DataRow row in data.Rows)
                {
                    int colIndex = 2;
                    int col = 3;
                    workSheet.Cells[rowIndex, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowIndex, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    workSheet.Cells[rowIndex, 1].Value = colSTT;
                    workSheet.Cells[rowIndex, 2].Value = row["shape"];
                    workSheet.Cells[rowIndex, 1].AutoFitColumns();
                    foreach (var item in field)
                    {
                        workSheet.Cells[rowIndex, col].Value = row[item.Fieldname];
                        workSheet.Cells[rowIndex, col].AutoFitColumns();
                        col += 1;
                    }
                    colSTT += 1;
                    rowIndex += 1;
                   
                }
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                string fileStringPath = string.Format("{0}\\{1}.xlsx", folder, fileName);
                FileStream fileStream = File.Create(fileStringPath);
                fileStream.Close();
                File.WriteAllBytes(fileStringPath, excel.GetAsByteArray());
                excel.Dispose();
                return fileStringPath;
            }
        }

        public static string ExportTemplateExcel(EntityCollection<SysfieldEntity> data, string folder, string fileName, string name)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 13;
                
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 1].Value = name;
                workSheet.Cells[1, 1, 1, data.Count + 2].Merge = true;
                workSheet.Cells.Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 1].Style.Font.Size = 14;
                workSheet.Cells.Style.Font.Bold = true;
                workSheet.Cells[2, 1].Value = "STT";
                workSheet.Cells[2, 2].Value = "Tọa độ";
                int col1 = 3;
                foreach (var dataColumn in data)
                {
                    workSheet.Cells[2, col1].Value = dataColumn.Name;
                    col1 += 1;
                }
                workSheet.Cells.AutoFitColumns();
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                string fileStringPath = string.Format("{0}\\{1}.xlsx", folder, fileName);
                FileStream fileStream = File.Create(fileStringPath);
                fileStream.Close();
                File.WriteAllBytes(fileStringPath, excel.GetAsByteArray());
                excel.Dispose();
                return fileStringPath;
            }
        }

        public static string ExportDataSetExcel(EntityCollection<SysfeatureclassEntity> data, string folder, string fileName, string name)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 13;

                workSheet.Row(1).Height = 20;
                workSheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 1].Value = name;
                workSheet.Cells[1, 1, 1, 5].Merge = true;
                workSheet.Cells.Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 1].Style.Font.Size = 14;
                workSheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[2, 1].Value = "STT";
                workSheet.Cells[2, 2].Value = "Tên lớp dữ liệu";
                workSheet.Cells[2, 1].Style.Font.Bold = true;
                workSheet.Cells[2, 2].Style.Font.Bold = true;
                workSheet.Cells[2, 2, 2, 5].Merge = true;
                workSheet.Cells[2, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[2, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[2, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[2, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[2, 2, 2, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[2, 2, 2, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[2, 2, 2, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[2, 2, 2, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                int col1 = 3;
                var i = 1;
                foreach (var dataColumn in data)
                {
                    workSheet.Cells[col1, 1].Value = i;
                    workSheet.Cells[col1, 2].Value = dataColumn.Name;
                    workSheet.Cells[col1, 2, col1, 5].Merge = true;
                    workSheet.Cells[col1, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[col1, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[col1, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[col1, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[col1, 2, col1, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[col1, 2, col1, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[col1, 2, col1, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[col1, 2, col1, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    col1 += 1;
                    i += 1;
                }
                workSheet.Cells.AutoFitColumns();
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                string fileStringPath = string.Format("{0}\\{1}.xlsx", folder, fileName);
                FileStream fileStream = File.Create(fileStringPath);
                fileStream.Close();
                File.WriteAllBytes(fileStringPath, excel.GetAsByteArray());
                excel.Dispose();
                return fileStringPath;
            }
        }
        public static string ExportCSV(DataTable data, string folder, string fileName, string prj = "4326")
        {
            fileName = fileName + ".csv";
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            StreamWriter sw = new StreamWriter(folder + "\\" + fileName, false);
            for (int i = 0; i < data.Columns.Count; i++)
            {
                sw.Write(data.Columns[i]);
                if (i < data.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in data.Rows)
            {
                int i = 0;
                foreach (DataColumn dataColumn in data.Columns)
                {
                    var value = dr[dataColumn].ToString();
                    if (dataColumn.ColumnName.ToLower() != "shape")
                    {
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                        }
                        if (value == "")
                        {
                            value = "null";
                        }
                        sw.Write(value);
                    }
                    else
                    {
                        NetTopologySuite.IO.WKTReader wktReader = new NetTopologySuite.IO.WKTReader();
                        GeoJsonWriter geoJsonWriter = new GeoJsonWriter();
                        if (!string.IsNullOrEmpty(dr[dataColumn] + ""))
                        {
                            Geometry geo = wktReader.Read(dr[dataColumn] + "");
                            if (geo.Coordinates.Length > 0)
                            {
                                //chuyển đổi hệ quy chiếu nếu có
                                if (prj != "4326")
                                {
                                    string wkt = VietnamCoordinateSystems.TransformWKTWGS84ToVN2000(geo, ProjecttionHelper.ConvertCoordinateSystemsFromSrid(prj));
                                    sw.Write(wkt);
                                }
                                else
                                    sw.Write(dr[dataColumn]);
                            }
                            else sw.Write("null");
                        }
                        else sw.Write("null");
                    }
                    if (i < data.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
            return folder + "\\" + fileName;
        }
        public static string GenerateFileZipByNameFile(string folder, string fileName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            var files = dirInfo.GetFiles();
            string filezip = string.Format("{0}\\{1}.zip", folder, fileName);
            try
            {
                using (FileStream zipFileStream = new FileStream(filezip, FileMode.Create))
                {
                    using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
                    {
                        foreach (var file in files)
                        {
                            archive.CreateEntryFromFile(file.FullName, file.Name);
                        }
                    }
                }
                return filezip;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

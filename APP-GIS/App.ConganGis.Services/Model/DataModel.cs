using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace App.CongAnGis.Services.Models
{
    public class DataModel
    {

        public class FieldMappingModel
        {
            public string FieldSource { get; set; }
            public string FieldDestination { get; set; }
            public string FieldValue { get; set; }

            public string FieldValueShape { get; set; }
        }

        public class ThongKeModel
        {
            public string maHuyen { get; set; }
            public string maXa { get; set; }
            public string startDate { get; set; }
            public string enDate { get; set; }

            public string months { get; set; }
            public string years { get; set; }
            public List<DataUpdateModel> lstFeatures { get; set; }
        }

        public class ThongKeModelResponse
        {
            public List<DataUpdateModel> lstFeatures { get; set; }
        }

        public class ListFields
        {
            public List<FieldMappingModel> Fields { get; set; }
        }
        public class ImportDataModel
        {
            public decimal FileId { get; set; }
            public string PathFile { get; set; }
            public string FileType { get; set; }
            public string TableName { get; set; }
            public decimal FeatureclassId { get; set; }
            public string Prj { get; set; }
            public List<FieldMappingModel> Fields { get; set; }

        }
        public class ImportExcelModel
        {
            public decimal FileId { get; set; }
            public string PathFile { get; set; }
            public string FileType { get; set; }
            public string TableName { get; set; }
            public decimal FeatureclassId { get; set; }
            public string Prj { get; set; }
            public List<ListFields> ListFields { get; set; }

        }
        public class ExportDataModel
        {
            public string FileType { get; set; }
            public string TableName { get; set; }
            public decimal FeatureclassId { get; set; }
            public string Filter { get; set; }
            public string Prj { get; set; }
            public List<FieldMappingModel> Fields { get; set; }

        }

        public class DataSearchModel
        {
            public string TableName { get; set; }
            public string Status { get; set; }
            public string textSearch { get; set; }
            public string fieldSelect { get; set; }
            public int pageSize { get; set; }
            public int currentPage { get; set; }

        }

        public class IconModel
        {
            public string TableName { get; set; }
            public string IconName { get; set; }

        }
        public class CreateOrUpdateDataModel
        {
            public List<DataUpdateModel> ListData { get; set; }
            public string TableName { get; set; }
            public string LngLat { get; set; }
            public int Id { get; set; }
            public string typeFile { get; set; }
            public List<int> fileDelete { get; set; }
        }
        public class CreateDataModel
        {
            public FieldMappingModel Data { get; set; }
            public string TableName { get; set; }
            public string LngLat { get; set; }
            public int Id { get; set; }
            public string typeFile { get; set; }
            public List<int> fileDelete { get; set; }
        }
        public class DataUpdateModel
        {
            public string key { get; set; }
            public string value { get; set; }
            public string status { get; set; }

        }
        public class insertNhatro
        {
            public List<nhatroModel> ListData { get; set; }
        }
        public class nhatroModel
        {
            public string shape { get; set; }
            public string tennhatro { get; set; }
            public string chunhatro { get; set; }
            public string unitcode { get; set; }
            public string dienthoai { get; set; }
            public string mahuyen { get; set; }
            public string diachi { get; set; }
            public string diachicuthe { get; set; }
            public string mota { get; set; }
            public string sophong { get; set; }
            public string sotang { get; set; }

        }


    }

}

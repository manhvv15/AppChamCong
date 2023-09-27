using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.NhanVien.DonDeXuat
{
    public class DonDoiCa
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class ErrorsAPI_DonDoiCa
        {
            [JsonProperty("noi_dung.doi_ca.ngay_muon_doi")]
            public NoiDungDoiCaNgayMuonDoi noi_dungdoi_cangay_muon_doi { get; set; }
        }

        public class MessageAPI_DonDoiCa
        {
            public ErrorsAPI_DonDoiCa errors { get; set; }
            public string _message { get; set; }
            public string name { get; set; }
            public string message { get; set; }
        }

        public class NoiDungDoiCaNgayMuonDoi
        {
            public string stringValue { get; set; }
            public string valueType { get; set; }
            public string kind { get; set; }
            public string value { get; set; }
            public string path { get; set; }
            public ReasonAPI_DonDoiCa reason { get; set; }
            public string name { get; set; }
            public string message { get; set; }
        }

        public class ReasonAPI_DonDoiCa
        {
            public bool generatedMessage { get; set; }
            public string code { get; set; }
            public bool actual { get; set; }
            public bool expected { get; set; }
            public string @operator { get; set; }
        }

        public class API_DonDoiCa
        {
            public int code { get; set; }
            public MessageAPI_DonDoiCa message { get; set; }
        }


    }
}

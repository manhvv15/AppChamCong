﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi
{
    public class BingMapAPI
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Address
        {
            public string addressLine { get; set; }
            public string adminDistrict { get; set; }
            public string countryRegion { get; set; }
            public string formattedAddress { get; set; }
            public Intersection intersection { get; set; }
            public string locality { get; set; }
            public string postalCode { get; set; }
        }

        public class GeocodePoint
        {
            public string type { get; set; }
            public List<double?> coordinates { get; set; }
            public string calculationMethod { get; set; }
            public List<string> usageTypes { get; set; }
        }

        public class Intersection
        {
            public string baseStreet { get; set; }
            public string secondaryStreet1 { get; set; }
            public string intersectionType { get; set; }
            public string displayName { get; set; }
        }

        public class Point
        {
            public string type { get; set; }
            public List<double?> coordinates { get; set; }
        }

        public class Resource
        {
            public string __type { get; set; }
            public List<double?> bbox { get; set; }
            public string name { get; set; }
            public Point point { get; set; }
            public Address address { get; set; }
            public string confidence { get; set; }
            public string entityType { get; set; }
            public List<GeocodePoint> geocodePoints { get; set; }
            public List<string> matchCodes { get; set; }
        }

        public class ResourceSet
        {
            public int? estimatedTotal { get; set; }
            public List<Resource> resources { get; set; }
        }

        public class Root
        {
            public string authenticationResultCode { get; set; }
            public string brandLogoUri { get; set; }
            public string copyright { get; set; }
            public List<ResourceSet> resourceSets { get; set; }
            public int? statusCode { get; set; }
            public string statusDescription { get; set; }
            public string traceId { get; set; }
        }


    }
}

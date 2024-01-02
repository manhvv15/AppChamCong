using System.Collections.Generic;

public class DataWifi
{
    public bool? result { get; set; }
    public string message { get; set; }
    public int? total { get; set; }
    public List<ItemWifi> data { get; set; }
}

public class ItemWifi
{
    public string _id { get; set; }
    public int? id { get; set; }
    public int? id_com { get; set; }
    public string ip_access { get; set; }
    public string name_wifi { get; set; }
    public int? id_loc { get; set; }
    public string location { get; set; }
}

public class RootWifi
{
    public DataWifi data { get; set; }
    public object error { get; set; }
}
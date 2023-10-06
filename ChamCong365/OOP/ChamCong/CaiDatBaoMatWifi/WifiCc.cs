using System.Collections.Generic;

public class DataWifi
{
    public bool result { get; set; }
    public string message { get; set; }
    public int totalItems { get; set; }
    public List<ItemWifi> items { get; set; }
}

public class ItemWifi
{
    public string _id { get; set; }
    public int wifi_id { get; set; }
    public int com_id { get; set; }
    public string name_wifi { get; set; }
    public string mac_address { get; set; }
    public string ip_address { get; set; }
    public string create_time { get; set; }
    public int is_default { get; set; }
    public int status { get; set; }
}

public class RootWifi
{
    public DataWifi data { get; set; }
    public object error { get; set; }
}
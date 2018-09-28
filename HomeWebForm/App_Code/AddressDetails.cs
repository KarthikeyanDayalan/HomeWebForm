using System;


// Model Class to store the details of Address table

public class AddressDetails
{
    public int ID { get; set; }
    public string AddressLine1 { get; set; }    
    public string City { get; set; }
    public string State { get; set; }    
    public int Zipcode { get; set; }
    public Nullable<int> ExpectedRent { get; set; }
    public Nullable<int> ZestimateRent { get; set; }
    public Nullable<int> ZestimateHousePrice { get; set; }
    public System.DateTime TimeCreated { get; set; }
    public System.DateTime TimeModified { get; set; }
    public int MinRentAmount { get; set; }
    public int MaxRentAmount { get; set; }
    public int MinHouseAmount { get; set; }
    public int MaxHouseAmount { get; set; }
}
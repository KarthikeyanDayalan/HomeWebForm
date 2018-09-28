using System;
using System.Collections.Generic;
using System.Net;
using System.Web.UI;
using System.Xml;

public partial class BelongHome : System.Web.UI.Page
{
    private string Ip = null;
    private int userId = 0;
    private int AddressID = 0;
    private int responseCode = 0;
    private DBContext dbo = null;
    private List<ZillowSearch> addressDetails = new List<ZillowSearch>();
    private XmlNodeList tags = null;
    private int i = 0;
    
    //Getting the IP address of the device while page load
    protected void Page_Load(object sender, EventArgs e)
    {
        string host = Dns.GetHostName();
        Ip = Dns.GetHostEntry(host).AddressList[1].ToString();
        dbo = new DBContext();
    }


    protected void Submit_Click(object sender, EventArgs e)
    {
        //Web API call output
        var output = GetRent(txtstreetAddress.Text, txtzipcode.Text);

        //checking for the webapi response
        tags = output.GetElementsByTagName("message");
        foreach (XmlNode codes in tags)
        {
            responseCode = Convert.ToInt32(codes.SelectSingleNode("code").InnerText);
        }

        //On Successful result
        if (responseCode == 0)
        {
            tags = output.GetElementsByTagName("result");
            foreach (XmlNode codes in tags)
            {
                var details = new ZillowSearch();
                details.Zpid = (Int32.TryParse(codes.SelectSingleNode("zpid").InnerText, out i) ? i : 0);
                details.Street = codes.SelectSingleNode("address/street").InnerText;
                details.Zipcode = codes.SelectSingleNode("address/zipcode").InnerText;
                details.City = codes.SelectSingleNode("address/city").InnerText;
                details.State = codes.SelectSingleNode("address/state").InnerText;
                details.HouseAmount = (Int32.TryParse(codes.SelectSingleNode("zestimate/amount").InnerText, out i) ? i : 0);
                details.MinHouseAmount = (Int32.TryParse(codes.SelectSingleNode("zestimate/valuationRange/low").InnerText, out i) ? i : 0);
                details.MaxHouseAmount = (Int32.TryParse(codes.SelectSingleNode("zestimate/valuationRange/high").InnerText, out i) ? i : 0);

                if (codes.SelectSingleNode("rentzestimate/amount") == null)
                {
                    details.RentAmount = Convert.ToInt32((details.HouseAmount * 5) / 1200.00);
                    details.MinRentAmount = Convert.ToInt32(((details.HouseAmount * 5) / 1200.00) - ((details.HouseAmount * 5) / 12000.00));
                    details.MaxRentAmount = Convert.ToInt32(((details.HouseAmount * 5) / 1200.00) + ((details.HouseAmount * 5) / 12000.00));
                }
                else
                {
                    details.RentAmount = (Int32.TryParse(codes.SelectSingleNode("rentzestimate/amount").InnerText, out i) ? i : 0);
                    details.MinRentAmount = (Int32.TryParse(codes.SelectSingleNode("rentzestimate/valuationRange/low").InnerText, out i) ? i : 0);
                    details.MaxRentAmount = (Int32.TryParse(codes.SelectSingleNode("rentzestimate/valuationRange/high").InnerText, out i) ? i : 0);
                }
                //Parse the XML output and store it in a model list
                addressDetails.Add(details);
            }
            
            //calling function to insert records in the database
            DbOperations();
            //calling the output page
            Response.Redirect("RentOutput.aspx");
        }
        else
        {          
            //Calling JS function to display the modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }

    //Function to call the Api and get the result
    protected XmlDocument GetRent(string address, string citystatezip)
    {       
        var url = "http://www.zillow.com/webservice/GetDeepSearchResults.htm?zws-id=X1-ZWz1gnkp9zf023_26nu3&address=" + address + "&citystatezip=" + citystatezip + "&rentzestimate=true";
        //var url = "http://www.zillow.com/webservice/GetDeepSearchResults.htm?zws-id=X1-ZWz1gnkp9zf023_26nu3&address=" + address + "&citystatezip=" + citystatezip;
        XmlDocument xml = new XmlDocument();
            var conn = new WebClient();
        try
        {
            var xmloutput = conn.DownloadString(url);
            xml.LoadXml(xmloutput);
        }
        catch(Exception ex)
        {
            dbo.InsertExceptions(ex.InnerException.ToString(), "APICall");
            Response.Redirect("ErrorMessage.aspx");
        }
        return xml;
    }

    //Function to insert records in the database
    protected void DbOperations()
    {
        if (txtfirstName.Text != null && txtlastName.Text != null && txtemail.Text != null && txtcontactnumber.Text != null)
        {
            if(!dbo.CheckForUser(txtfirstName.Text.ToString(), txtlastName.Text.ToString(), txtemail.Text.ToString(), txtcontactnumber.Text.ToString()))
            {
                Session["NewUser"] = 1;
            }
            else
            {
                Session["NewUser"] = 0;
            }
            userId = dbo.InsertorFetchUsers(txtfirstName.Text.ToString(), txtlastName.Text.ToString(), txtemail.Text.ToString(), txtcontactnumber.Text.ToString());
            if (userId == -1)
            {
                Response.Redirect("ErrorMessage.aspx");
            }
        }

        Session["userId"] = userId;

        if (txtstreetAddress.Text != null && txtcity.Text != null && txtstate.Text != null && txtzipcode.Text != null)
        {
            foreach (var item in addressDetails)
            {
                AddressID = dbo.InsertAddress(item.Street, item.City, item.State, item.Zipcode, item.RentAmount, item.HouseAmount, item.MinRentAmount, item.MaxRentAmount, item.MinHouseAmount, item.MaxHouseAmount);
                if (AddressID == -1)
                {
                    Response.Redirect("ErrorMessage.aspx");
                }
            }
        }

        if (userId != -1 && AddressID != -1)
        {
            var temp = dbo.InsertLogin(Ip, AddressID, userId);
            if (temp == -1)
            {
                Response.Redirect("ErrorMessage.aspx");
            }
        }
    }

}
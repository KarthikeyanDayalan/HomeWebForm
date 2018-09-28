using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class RentOutput : System.Web.UI.Page
{
    private StringBuilder mailBody = new StringBuilder();
    private DBContext dbo = new DBContext();
    private List<UserDetails> userDetail = null;
    private List<AddressDetails> addressDetail = null;
    private String LastName = String.Empty;

    //Populating the Rent estimate details while loading the page and display it in the grid
    protected void Page_Load(object sender, EventArgs e)
    {       
        try
        {
            addressDetail = dbo.FetchAddress(Convert.ToInt32(Session["userId"].ToString()));
            userDetail = dbo.FetchUsers(Convert.ToInt32(Session["userId"]));

            if (Convert.ToInt32(Session["NewUser"]) == 1)
            {
                foreach (var item in userDetail)
                {
                    MailBodyNewUsers(LastName);
                    Session["NewUser"] = 0;
                    SendEmail(item.EmailID, mailBody.ToString());
                }
            }

            foreach (var item in addressDetail)
            {
                tblHeader.Text = item.AddressLine1.ToString() + " "+item.City.ToString() + " " + item.State.ToString();
                tblRentCell.Text = item.MinRentAmount.ToString() +" - "+ item.MaxRentAmount.ToString();
                tblCellSale.Text = item.MinHouseAmount.ToString()+" - "+item.MaxHouseAmount.ToString();                
            }
        }
        catch
        {           
            Response.Redirect("ErrorMessage.aspx");
        }
    }

    //Function to send email with the SMTP configurations
    protected Boolean SendEmail(string emailAddress, string message)
    {
        try
        {
            MailMessage mail = new MailMessage("Karthik.Belong@gmail.com", emailAddress);
            mail.Subject = "Welcome to Rental World";
            mail.Body = message.ToString();
            mail.IsBodyHtml = true;
            
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;

            NetworkCredential authinfo = new NetworkCredential("karthik.belong@gmail.com", "Belong2018");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = authinfo;
            smtp.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            dbo.InsertExceptions(ex.InnerException.ToString(), "SendEmail");
            Response.Redirect("ErrorMessage.aspx");            
        }
        return false;
    }
    
    //Function to draft the header message for the email body
    protected void GetMailHeader(string lastName)
    {
        mailBody.AppendLine("<html><body>");
        mailBody.AppendLine("<header>");
        mailBody.AppendLine("Hello " + lastName + ",");
        mailBody.AppendLine("</header>");
        mailBody.AppendLine("<br/>");
    }

    //Function to draft the footer message for the email body
    protected void GetMailFooter()
    {
        mailBody.AppendLine("<footer>");
        mailBody.AppendLine("<br/>Regards,");
        mailBody.AppendLine("<br/>Team Rental World");
        mailBody.AppendLine("<br/><i>This is an automated email, if you have questions/suggestions please email to <a href='mailTo:dayalan_karthik@yahoo.co.in'>support team</a></i>");
        mailBody.AppendLine("</footer>");
        mailBody.AppendLine("</body></html>");
    }


    //Function to write an email body that contains details of the user logged in our database
    protected void GetDBDetails()
    {

        mailBody.Append("");
        mailBody.Append("<table border='1' cellpadding='0' cellspacing='0' bgcolor='Khaki'>");
        mailBody.Append("<tr>");


        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
                        "First Name" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
                        "Last Name" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
                "Email ID" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
                "Phone Number" + "</td>");

        mailBody.Append("</tr>");

        foreach (var item in userDetail)
        {
            mailBody.Append("<td align='center' valign='middle'>" + item.FirstName + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.LastName + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.EmailID + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.PhoneNumber + "</td>");
        }
        mailBody.Append("</tr>");

        mailBody.AppendLine("</table>");
        mailBody.AppendLine("<br/>");
        mailBody.AppendLine("<br/>");

        mailBody.Append("");
        mailBody.Append("<table border='1' cellpadding='0' cellspacing='0' bgcolor='Khaki'>");
        mailBody.Append("<tr>");

        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
                        "Street Address" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
                        "City" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
                "State" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
                "Zip Code" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
                        "ExpectedRent" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
                        "ZestimateRent" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
                "ZestimateHousePrice" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
        "MinZestimateHousePrice" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
        "MaxZestimateHousePrice" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
        "MinZestimateRent" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
        "MaxZestimateRent" + "</td>");
        mailBody.Append("<td bgcolor='goldenrod' align='center' valign='middle'>" +
                "Last Login" + "</td>");


        mailBody.Append("</tr>");

        foreach (var item in addressDetail)
        {
            mailBody.Append("<td align='center' valign='middle'>" + item.AddressLine1 + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.City + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.State + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.Zipcode + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.ExpectedRent + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.ZestimateRent + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.ZestimateHousePrice + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.MinRentAmount + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.MaxRentAmount + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.MinHouseAmount + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.MinHouseAmount + "</td>");
            mailBody.Append("<td align='center' valign='middle'>" + item.TimeCreated + "</td>");
        }
        mailBody.Append("</tr>");

        mailBody.AppendLine("</table>");
        mailBody.AppendLine("<br/>");
        mailBody.AppendLine("<br/>");

    }

    //Function to organize the email body for the existing users
    protected void MailBody(string lastName)
    {
        mailBody.Clear();
        GetMailHeader(lastName);
        mailBody.Append("<br/> Please find the below details which you requested along with the details that you entered.<br/><br/>");
        GetDBDetails();
        GetMailFooter();
    }

    //Function to organize the email body for the new users
    protected void MailBodyNewUsers(string lastName)
    {
        mailBody.Clear();
        GetMailHeader(lastName);
        mailBody.Append("<br/> I am Karthikeyan Dayalan, Co-founder of Rental World. I'd like to thank you for signing up with us.");
        mailBody.Append("<br/> Do you know? There are more people ready to rent than the available properties in this world. So, you made the right decision to rent your property.");
        mailBody.Append("<br/> We will help you to select the right tenant who can take care of your property as their own one.<br/>");        
        GetMailFooter();
    }

    //Function that triggers the email option
    protected void sendEmail_Click(object sender, EventArgs e)
    {        
        foreach (var item in userDetail)
        {
            LastName = Char.ToUpper(item.LastName[0])+item.LastName.Substring(1).ToString();
            MailBody(LastName);
            SendEmail(item.EmailID, mailBody.ToString());
        }
        lblEmail.Visible = true;
        lblEmail.Text = "Email Sent Successfully";
    }

    protected void txtEstimatedRent_TextChanged(object sender, EventArgs e)
    {
        int i = 0;
        foreach (var item in addressDetail)
            dbo.UpdateAddress(item.ID,Int32.TryParse(txtEstimatedRent.Text.ToString(), out i)?i:0);
    }
}


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;

// All Database Transactions are maintanined in this file
public class DBContext
{
    //Connection string of database from web.config file
    readonly string connString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();

    //Function to store the exception in the exceptions table.
    //Inner Exceptions and Function/Table name are the arguments
    public void InsertExceptions(string message, string table)
    {
        SqlConnection conn = new SqlConnection(connString);
        SqlCommand comm = new SqlCommand("sp_InsertExceptions", conn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        comm.Parameters.AddWithValue("@errorMessage", message);
        comm.Parameters.AddWithValue("@exceptionTable", table);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
        }
        catch(Exception ex)
        {

        }
        finally
        {
            conn.Close();
        }
    }

    //Function to insert the new users or fetch the user ID of the existing users from the database
    //First, Last Name, email, phone from the page are the input parameters
    public int InsertorFetchUsers(string firstName, string lastName, string email, string phone)
    {
        int i = 0, id = 0;

        SqlConnection conn = new SqlConnection(connString);
        SqlCommand comm = new SqlCommand("sp_InsertorFetchUsers", conn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        comm.Parameters.AddWithValue("@fName", firstName);
        comm.Parameters.AddWithValue("@lName", lastName);
        comm.Parameters.AddWithValue("@email", email);
        comm.Parameters.AddWithValue("@phone", phone);

        SqlParameter prmReturnValue = new SqlParameter("@id", SqlDbType.Int)
        {
            Direction = System.Data.ParameterDirection.Output
        };
        comm.Parameters.Add(prmReturnValue);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
            id = Int32.TryParse(prmReturnValue.Value.ToString(), out i) ? i : -1;
        }
        catch (Exception ex)
        {
            id = -1;
            InsertExceptions(ex.ToString(), "Users");
        }
        finally
        {
            conn.Close();
        }
        return id;
    }

    //Function to insert the address details and the web API results into the database
    //Address, WebAPI results are the input parameters
    public int InsertAddress(string address1, string city, string state, string zip, int zestimaterent, int housevalue, int minRent, int maxRent, int minHouseValue, int maxHouseValue)
    {
        int id = 0;

        SqlConnection conn = new SqlConnection(connString);
        SqlCommand comm = new SqlCommand("sp_InsertAddress", conn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        comm.Parameters.AddWithValue("@address1", address1);
        comm.Parameters.AddWithValue("@city", city);
        comm.Parameters.AddWithValue("@state", state);
        comm.Parameters.AddWithValue("@zipCode", zip);
        comm.Parameters.AddWithValue("@zEstimateRent", zestimaterent);
        comm.Parameters.AddWithValue("@zEstimateHousePrice", housevalue);
        comm.Parameters.AddWithValue("@minzEstimateRent", minRent);
        comm.Parameters.AddWithValue("@maxzEstimateRent", maxRent);
        comm.Parameters.AddWithValue("@minzEstimateHousePrice", minHouseValue);
        comm.Parameters.AddWithValue("@maxzEstimateHousePrice", maxHouseValue);

        SqlParameter prmReturnValue = new SqlParameter("@id", SqlDbType.Int)
        {
            Direction = System.Data.ParameterDirection.Output
        };
        comm.Parameters.Add(prmReturnValue);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
            id = Convert.ToInt32(prmReturnValue.Value);
        }
        catch (Exception ex)
        {
            id = -1;
            InsertExceptions(ex.ToString(), "Address");
        }
        finally
        {
            conn.Close();
        }
        return id;
    }

    //Function to store the IP address and the address accessed by the users
    //previously inserted user and address ID along with IP are the input parameters
    public int InsertLogin(string IP, int addressID, int userID)
    {
        int id = 0;

        SqlConnection conn = new SqlConnection(connString);
        SqlCommand comm = new SqlCommand("sp_InsertLogin", conn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        comm.Parameters.AddWithValue("@ipAddress", IP);
        comm.Parameters.AddWithValue("@addressID", addressID);
        comm.Parameters.AddWithValue("@userID", userID);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
            id = 1;
        }
        catch (Exception ex)
        {
            id = -1;
            InsertExceptions(ex.ToString(), "LoginDetails");
        }
        finally
        {
            conn.Close();
        }
        return id;
    }

    //Function to retrieve the user login details for the corresponding userID
    //User ID maintained in the session is the input parameter
    public List<LoginDetails> FetchLoginID(int userID)
    {
        var logins = new List<LoginDetails>();
        DataTable dtLoginDetails = new DataTable();

        SqlConnection conn = new SqlConnection(connString);

        SqlDataAdapter comm = new SqlDataAdapter("SELECT * FROM LoginDetails WHERE UserID = @userID", conn);
        comm.SelectCommand.Parameters.AddWithValue("@userID", userID);

        try
        {
            comm.Fill(dtLoginDetails);
            logins = dtLoginDetails.AsEnumerable().Select(dr => new LoginDetails
            {
                ID = Convert.ToInt32(dr[0]),
                AddressID = Convert.ToInt32(dr[1]),
                UserID = Convert.ToInt32(dr[2]),
                IPAddress = dr[3].ToString(),
                Timecreated = Convert.ToDateTime(dr[4]),
            }).ToList();
        }
        catch (Exception ex)
        {
            logins = null;
            InsertExceptions(ex.ToString(), "fetchLoginID");
        }
        return logins;
    }

    //Function to retrieve the address details searched by the user
    //User ID maintained in the session is the input parameter
    public List<AddressDetails> FetchAddress(int userID)
    {
        int i = 0;
        var address = new List<AddressDetails>();
        DataTable dtAddressDetails = new DataTable();

        SqlConnection conn = new SqlConnection(connString);

        SqlDataAdapter comm = new SqlDataAdapter("SELECT TOP 1 A.* FROM Address A JOIN LoginDetails L ON A.ID = L.AddressID WHERE L.UserID =@userID ORDER BY Timecreated desc", conn);
        comm.SelectCommand.Parameters.AddWithValue("@userID", userID);

        try
        {
            comm.Fill(dtAddressDetails);
            address = dtAddressDetails.AsEnumerable().Select(dr => new AddressDetails
            {
                ID = Convert.ToInt32(dr[0]),
                AddressLine1 = dr[1].ToString(),
                City = dr[2].ToString(),
                State = dr[3].ToString(),
                Zipcode = Convert.ToInt32(dr[4]),
                ExpectedRent = (Int32.TryParse(dr[5].ToString(), out i) ? i : 0),
                ZestimateRent = Convert.ToInt32(dr[6]),
                ZestimateHousePrice = Convert.ToInt32(dr[7]),
                TimeCreated = Convert.ToDateTime(dr[8]),
                TimeModified = Convert.ToDateTime(dr[9]),
                MinRentAmount = Convert.ToInt32(dr[10]),
                MaxRentAmount = Convert.ToInt32(dr[11]),
                MinHouseAmount = Convert.ToInt32(dr[12]),
                MaxHouseAmount = Convert.ToInt32(dr[13]),
            }).ToList();
        }
        catch (Exception ex)
        {
            address = null;
            InsertExceptions(ex.ToString(), "fetchAddress");
        }
        return address;
    }

    //Function to retrieve the user details for the corresponding userID
    //User ID maintained in the session is the input parameter
    public List<UserDetails> FetchUsers(int userID)
    {
        var user = new List<UserDetails>();
        DataTable dtUserDetails = new DataTable();

        SqlConnection conn = new SqlConnection(connString);

        SqlDataAdapter comm = new SqlDataAdapter("SELECT * FROM Users WHERE ID=@userID", conn);
        comm.SelectCommand.Parameters.AddWithValue("@userID", userID);

        try
        {
            comm.Fill(dtUserDetails);
            user = dtUserDetails.AsEnumerable().Select(dr => new UserDetails
            {
                ID = Convert.ToInt32(dr[0]),
                FirstName = dr[1].ToString(),
                LastName = dr[2].ToString(),
                EmailID = dr[3].ToString(),
                PhoneNumber = dr[4].ToString(),
                Timecreated = Convert.ToDateTime(dr[5]),
            }).ToList();
        }
        catch (Exception ex)
        {
            user = null;
            InsertExceptions(ex.ToString(), "fetchAddress");
        }
        return user;
    }

    //Function to check for the existence of the user. This is to send initial sign up email to users
    public bool CheckForUser(string firstName, string lastName, string email, string phone)
    {
        int i = 0;
        SqlConnection conn = new SqlConnection(connString);
        SqlCommand comm = new SqlCommand("sp_UserExists", conn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        comm.Parameters.AddWithValue("@fName", firstName);
        comm.Parameters.AddWithValue("@lName", lastName);
        comm.Parameters.AddWithValue("@email", email);
        comm.Parameters.AddWithValue("@phone", phone);

        try
        {
            conn.Open();
            var output = comm.ExecuteScalar();
            if (output != null && (Int32.TryParse(output.ToString(), out i) ? i : 0) > 0)
                return true;
        }
        catch (Exception ex)
        {
            InsertExceptions(ex.ToString(), "CheckForUsers");
        }
        finally
        {
            conn.Close();
        }
        return false;
    }


    public void UpdateAddress(int addressID, int expectedRent)
    {
        SqlConnection conn = new SqlConnection(connString);
        SqlCommand comm = new SqlCommand("sp_UpdateAddress", conn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        comm.Parameters.AddWithValue("@addressId", addressID);
        comm.Parameters.AddWithValue("@expectedRent", expectedRent);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            InsertExceptions(ex.ToString(), "UpdateAddress");
        }
        finally
        {
            conn.Close();
        }
    }
}
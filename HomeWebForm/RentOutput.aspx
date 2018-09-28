<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RentOutput.aspx.cs" Inherits="RentOutput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Property Estimator</title>
    <link href="Account/Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="Account/Bootstrap/Style.css" rel="stylesheet" />
</head>

<body style="background-image: url('Account/Images/background.jpg'); background-repeat:no-repeat; background-position:0px 0px; background-color:lightblue">
    <form id="form1" runat="server">
        <h1 style="margin:25px 10px 10px 1275px; color:brown">Rent Estimate Valuator</h1>
        <div class="panel-img">
            <img src="Account/images/form-image.png" style="width:80%;height:550%;margin:-120px 0px 0px 850px" />

            </div>

                <div style="position:absolute; z-index:1;" id="layer1">
                    <div class="modal-body" style="margin:-120px 0px 0px 1350px;">

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="container">
                                    <asp:Table ID="tblEstimate" cssclass ="table table-stripped" runat="server" Height="152px" Width="400px" BorderWidth="0">
                                        <asp:TableRow runat="server">
                                            <asp:TableCell ID="tblHeader" runat="server" Font-Bold="True" BorderWidth="0" ColumnSpan="2" ForeColor="#0000CC" Font-Underline="True"></asp:TableCell>                                            
                                        </asp:TableRow>
                                        <asp:TableRow runat="server" ID="tblRowHeaders" BorderWidth="0">
                                            <asp:TableCell ID="tblRentEstimate" runat="server" Font-Bold="True" BorderStyle="None" BorderWidth="0">Rent Estimate Range($):</asp:TableCell>
                                            <asp:TableCell ID="tblRentCell" runat="server"  cssclass="tbn btn-success btn-block"></asp:TableCell>
                                        </asp:TableRow>
                                         <asp:TableRow runat="server" BorderWidth="0">
                                            <asp:TableCell ID="tblBlank1" runat="server" BorderWidth="0"></asp:TableCell>
                                            <asp:TableCell ID="tblBlank2" runat="server" BorderWidth="0"></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow runat="server">
                                            <asp:TableCell ID="tblSaleEstimate" runat="server" Font-Bold="True" BorderWidth="0">Sale Estimate Range($):</asp:TableCell>
                                            <asp:TableCell ID="tblCellSale" runat="server" cssclass="tbn btn-success btn-block"></asp:TableCell>
                                        </asp:TableRow>                                    
                                         <asp:TableRow runat="server" BorderWidth="0">
                                            <asp:TableCell ID="tblBlank3" runat="server" BorderWidth="0"></asp:TableCell>
                                            <asp:TableCell ID="tblBlank4" runat="server" BorderWidth="0"></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow runat="server">
                                            <asp:TableCell ID="tblRentlabel" runat="server" Font-Bold="True" BorderWidth="0">Expected Rent($):</asp:TableCell>
                                            <asp:TableCell ID="tblRentExpected" runat="server" cssclass="tbn btn-success btn-block"><asp:TextBox ID="txtEstimatedRent" runat="server" CssClass="form-control" OnTextChanged="txtEstimatedRent_TextChanged" AutoPostBack="true"></asp:TextBox></asp:TableCell>
                                        </asp:TableRow>                                    
                                    </asp:Table>
                                </div>
                                <br />
                                <asp:Button ID="sendEmail" runat="server" cssclass="btn btn-success btn-block" Text="Send the details to my email address" OnClick="sendEmail_Click" style="width:33.5%;margin-top:1%;margin-left:2%;color:yellow;background-color:brown"/><br />
                                <asp:Label ID="lblEmail" runat="server" Text="Label" Visible="False"></asp:Label>
                            </div>
                        </div>
                    </div> 
                </div>
        </form>
        <script src="Account/Jscript/jquery-3.1.1.min.js"></script>
        <script src="Account/Jscript/bootstrap.min.js"></script>
    </body>
</html>

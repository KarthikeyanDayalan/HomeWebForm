<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BelongHome.aspx.cs" Inherits="BelongHome" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
   <head runat="server">
      <title>Property Estimator</title>
      <link href="Account/Bootstrap/bootstrap.min.css" rel="stylesheet" />
      <link href="Account/Bootstrap/Style.css" rel="stylesheet" />
      <script src="Account/Jscript/jquery-3.1.1.min.js"></script>
      <script src="Account/Jscript/bootstrap.min.js"></script>
      <script type="text/javascript">
         function openModal() {
             $('#pnlModal').modal('show');
         }
      </script>
   </head>
   <body style="background-image: url('Account/Images/background.jpg'); background-repeat:no-repeat; background-position:0px 0px; height:100%;background-color:lightblue">
      <form id="form1" runat ="server">
         <h1 style="margin:25px 10px 10px 1250px; color:brown">Rent Estimate Valuator</h1>
         <div class="panel-img">
            <img src="Account/images/form-image.png" style="width:100%;height:800%;margin:-230px 0px 0px 775px"" />                  
         </div>
         <div style="position:absolute;" id="layer1">
            <div class="modal-body" style="margin:-150px 0px 0px 1400px;">
               <div class="row">
                  <div class="col-xs-12">
                     <div class="form-group">
                        <asp:Label runat="server" ID="lblfirstname" CssClass="control-label">First Name</asp:Label>
                        <asp:RequiredFieldValidator id="fn" runat="server" Display="Dynamic" ControlToValidate="txtfirstName" ErrorMessage="* Required field." ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtfirstName" runat="server" CssClass="form-control" placeholder="Karthikeyan"></asp:TextBox>
                        <span class="help-block"></span>
                     </div>
                     <div class="form-group">
                        <asp:Label runat="server" ID="lbllastname" CssClass="control-label">Last Name</asp:Label>                        
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtlastName"  ErrorMessage="* Required field." ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtlastName" runat="server" CssClass="form-control" placeholder="Dayalan"></asp:TextBox>
                        <span class="help-block"></span>
                     </div>
                     <div class="form-group">
                        <asp:Label runat="server" ID="lblemailaddress" CssClass="control-label">Email Address</asp:Label>                        
                         <asp:RequiredFieldValidator runat="server" ControlToValidate="txtemail"  ErrorMessage="* Required field." ForeColor="Red"></asp:RequiredFieldValidator>
                        
                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" placeholder="dayalan.karthikeyan@gmail.com"></asp:TextBox>
                        <span class="help-block"></span>
                     </div>
                     <div class="form-group">
                        <asp:Label runat="server" ID="lbladdressline1" CssClass="control-label">Street Address</asp:Label>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtstreetAddress"  ErrorMessage="* Required field." ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtstreetAddress" runat="server" CssClass="form-control" placeholder="3129 Willard Avenue"></asp:TextBox>
                        <span class="help-block"></span>
                     </div>
                     <div class="form-group">
                        <asp:Label runat="server" ID="lbladdressline2" CssClass="control-label">City</asp:Label>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtcity"  ErrorMessage="* Required field." ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtcity" runat="server" CssClass="form-control" placeholder="Rosemead"></asp:TextBox>
                        <span class="help-block"></span>
                     </div>
                     <div class="form-group">
                        <asp:Label runat="server" ID="lbladdressline3" CssClass="control-label">State</asp:Label>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtstate"  ErrorMessage="* Required field." ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtstate" runat="server" CssClass="form-control" placeholder="California"></asp:TextBox>
                        <span class="help-block"></span>
                     </div>
                     <div class="form-group">
                        <asp:Label runat="server" ID="lbladdressline4" CssClass="control-label">Zip Code</asp:Label>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtzipcode"  ErrorMessage="* Required field." ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtzipcode" runat="server" CssClass="form-control" placeholder="91770"></asp:TextBox>
                        <span class="help-block"></span>
                     </div>
                     <div class="form-group">
                        <asp:Label runat="server" ID="lblcontact" CssClass="control-label">Contact Number</asp:Label>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtcontactnumber"  ErrorMessage="* Required field." ForeColor="Red"></asp:RequiredFieldValidator>                         
                        <asp:TextBox ID="txtcontactnumber" runat="server" CssClass="form-control" placeholder="9123456780"></asp:TextBox>
                        <span class="help-block"></span>
                     </div>

                     <asp:Button ID="btnSubmit" runat="server" cssclass="btn btn-success btn-block" Text="Click to Estimate Rent" OnClick="Submit_Click"/>
                     <asp:RegularExpressionValidator runat="server" ControlToValidate="txtemail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$" CssClass="text-danger" ErrorMessage="Enter proper Email ID" /><br />
                      <asp:RegularExpressionValidator runat="server" ControlToValidate="txtcontactnumber" ValidationExpression="^[0-9-]{10}$" CssClass="text-danger" ErrorMessage="Enter Correct phone Number" />

                      <asp:Panel ID="pnlModal" runat="server" role="dialog" CssClass="modal fade pan">
                        <asp:Panel ID="pnlInner" runat="server" CssClass="modal-dialog">
                           <asp:Panel ID="pnlContent" CssClass="modal-content" runat="server">
                              <asp:Panel runat="server" class="modal-header" BackColor="#449D44">
                                 <button type="button" class="close" data-dismiss="modal">
                                 <span aria-hidden="false">&times;</span><span class="sr-only">Close</span>
                                 </button>
                                 <h4 class="modal-title" style="color:white">Warning!!</h4>
                              </asp:Panel>
                              <asp:panel runat="server" class="modal-body">
                                 <p> Address is Invalid. Please check the following fields<br /><br />
                                    * Street Address<br />
                                    * Zip Code
                                 </p>
                              </asp:panel>
                              <asp:panel runat="server" class="modal-footer">
                                 <button type="button" class="btn btn-default" data-dismiss="modal" style="margin-top:1%">Close</button>
                              </asp:panel>
                           </asp:Panel>
                        </asp:Panel>
                     </asp:Panel>
                  </div>
               </div>
            </div>
         </div>
      </form>
   </body>
</html>
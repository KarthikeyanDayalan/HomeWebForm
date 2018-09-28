<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorMessage.aspx.cs" Inherits="ErrorMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
   <head runat="server">
      <title>Property Estimator</title>
      <link href="Account/Bootstrap/bootstrap.min.css" rel="stylesheet" />
      <link href="Account/Bootstrap/Style.css" rel="stylesheet" />
   </head>
   <body style="background-image: url('Account/Images/background.jpg'); background-repeat:no-repeat; background-position:0px 0px; background-color:lightblue">
      <form id="form1" runat ="server">
         <h1 style="margin:25px 10px 10px 1250px; color:forestgreen">Rent Estimate Valuator</h1>
         <div class="panel-img">
            <img src="Account/images/form-image.png" style="width:100%;height:900%;margin:-250px 0px 0px 775px"" />
         </div>
         <div style="position:absolute; z-index:1;" id="layer1">
            <div class="modal-body" style="margin:-150px 0px 0px 1300px;">
               <div class="row">
                  <div class="col-xs-12">
                     <asp:Label ID="lblError" runat="server" cssclass="control-label" Text="Something Went Wrong!! Please contact Administrator" Font-Bold="True" Font-Italic="False" Font-Size="XX-Large" ForeColor="#FF3300" Height="30px"></asp:Label>
                  </div>
               </div>
            </div>
         </div>
      </form>
      <script src="Account/Jscript/jquery-3.1.1.min.js"></script>
      <script src="Account/Jscript/bootstrap.min.js"></script>
   </body>
</html>

<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchCars.aspx.cs" Inherits="CarsEngage.SearchCars" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h3>Search Car details</h3>
        <p class="lead"></p>
        <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>

        <asp:Panel ID="loadPnl" runat="server" Visible="true" Height="450px">


           

            <div class="form-group">
                  
                               <%--<label class="col-sm-1 control-label">where</label>--%>
                <div class="col-sm-3">
                    <asp:ListBox ID="ListBox1" runat="server" Height="400px" Width="250px" ></asp:ListBox>
                    </div>
                                <div class="col-sm-3">
                                     
                                <asp:TextBox runat="server" class="form-control" ID="wherecondition" autocomplete="off" />                                            
                                </div>
                            </div>

             <asp:ListBox ID="ListBox2" runat="server" Height="50px" Width="50PX" ></asp:ListBox>  
            <button type="button" runat="server" id="Button1" onserverclick="ADD" class="btn btn-info"
                            title="Upload">
                            <i class="fa fa-upload"></i>&nbsp;&nbsp;ADD
                        </button>
                  </asp:Panel>

             <div class="row">
                     <label id="lblselectedCondition" class="col-sm-1 control-label" runat="server"></label>
 </div> 
        <div class="row">
            <button type="button" runat="server" id="btnsearch" onserverclick="Search" class="btn btn-info"
                            title="Upload">
                            <i class="fa fa-upload"></i>&nbsp;&nbsp;Search
                        </button>      
                    </div> 

  

    </div>

    <div class="row">
        <div class="col-md-4">
            <asp:Panel ID="resultsPnl" runat="server" Visible="false">
                               <asp:GridView ID="searchResults" runat="server" CssClass="table table-bordered">                           
                                    </asp:GridView>

                         </asp:Panel>
            <%--<h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>--%>
        </div>
      </div>
        <div class="col-md-4">
                       
                      
           <%-- <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>--%>
        </div>
        <%--<div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>--%>
  

</asp:Content>

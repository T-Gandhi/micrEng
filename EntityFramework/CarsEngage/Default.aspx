<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarsEngage._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h3>Load Car details</h3>
        <p class="lead"></p>
        <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>

        <asp:Panel ID="loadPnl" runat="server" Visible="true">
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btn-info btn-lg" />
                        <br />
                        <button type="button" runat="server" id="btnupload" onserverclick="Upload" class="btn btn-info"
                            title="Upload">
                            <i class="fa fa-upload"></i>&nbsp;&nbsp;Upload
                        </button>                     

        </asp:Panel>

    </div>

    <div class="row">
        <div class="col-md-4">
            <asp:Panel ID="resultsPnl" runat="server" Visible="false">
                                <h3><asp:Label runat="server" ID="lblnotmatching"></asp:Label></h3>                          

                                    <h4 runat="server" id="summaryTxt"></h4>
                                    <asp:GridView ID="ExceptionResults" runat="server" CssClass="table table-bordered">                           
                                    </asp:GridView>

                                <button type="button" runat="server" id="Button2" onserverclick="Restart" class="btn btn-warning"
                                    title="Upload">
                                    <i class="fa fa-repeat" aria-hidden="true"></i>&nbsp;&nbsp;Upload Another
                                </button>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Documentor.aspx.cs" Inherits="SitecoreDocumentor.Web.Documentor" %>

<%@ Register Src="~/Sitecore/Admin/UserControls/TemplatesList.ascx" TagPrefix="uc" TagName="TemplatesList" %>
<%@ Register Src="~/Sitecore/Admin/UserControls/RenderingsList.ascx" TagPrefix="uc" TagName="RenderingsList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sitecore Documentor</title>
    <link rel="stylesheet" href="//ajax.aspnetcdn.com/ajax/bootstrap/3.3.4/css/bootstrap.min.css" />
    <script src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-2.1.4.min.js"></script>
    <script src="//ajax.aspnetcdn.com/ajax/bootstrap/3.3.4/bootstrap.min.js"></script>

    <style type="text/css">
        h3 img {
            margin-top: -3px;
            width: 16px;
        }

        .muted {
            color: #777;
        }

        .tblcol-icon {
            width: 30px;
        }

            .tblcol-icon img {
                width: 16px;
            }

        .tblcol-name {
            width: 225px;
        }

        .tblcol-req {
            width: 90px;
            text-align: center;
        }

        .tblcol-src {
            width: 300px;
        }

        .tblcol-type {
            width: 175px;
        }

        .tblrow-indent td:first-child {
            padding-left: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container-fluid">
        <!-- Form -->
        <div class="row">
            <div class="col-md-4">
                <div class="form form-vertical">
                    <div class="form-group">
                        <label class="control-label">Rendering Root</label>
                        <asp:TextBox runat="server" ID="txtRenderingRoot" CssClass="form-control" Placeholder="Rendering path" Text="/sitecore/layout/Renderings/Rolanddga_com" />
                    </div>
                    <div class="form-group">
                        <label class="control-label">Template Root</label>
                        <asp:TextBox runat="server" ID="txtTemplateRoot" CssClass="form-control" Placeholder="Template path" Text="/sitecore/templates/Rolanddga_com" />
                    </div>
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary" Text="Run" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <ul class="nav nav-tabs" data-tabs="tabs" id="tabs" role="tablist">
                    <li class="active"><a href="#renderings" data-toggle="tab" role="tab">Renderings</a></li>
                    <li><a href="#templates" data-toggle="tab" role="tab">Templates</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="renderings">
                        <uc:RenderingsList ID="ucRenderings" runat="server" />
                    </div>
                    <div class="tab-pane" id="templates">
                        <uc:TemplatesList ID="ucTemplates" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            $(function() {
                $("#tabs a").click(function(e) {
                    e.preventDefault();
                    $(this).tab("show");
                });

                $("#tab a:first-child").tab("show");
            });
        </script>
    </form>
</body>
</html>

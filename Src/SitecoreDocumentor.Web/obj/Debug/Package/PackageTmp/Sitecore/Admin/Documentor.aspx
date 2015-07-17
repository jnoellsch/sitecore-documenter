﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Documentor.aspx.cs" Inherits="SitecoreDocumentor.Web.Documentor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sitecore Documentor</title>
    <link rel="stylesheet" href="//ajax.aspnetcdn.com/ajax/bootstrap/3.3.4/css/bootstrap.min.css" />

    <style type="text/css">
        h3 img {
            margin-top: -3px;
            width: 16px;
        }

        .tblcol-icon {
            width: 30px;
        }

            .tblcol-icon img {
                width: 16px;
            }

        .tblcol-name {
            width: 250px;
        }

        .tblcol-req {
            width: 20px;
            text-align: center;
        }

        .tblcol-src {
            width: 300px;
        }

        .tblcol-type {
            width: 200px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container-fluid">
        <!-- Form -->
        <div class="row">
            <div class="col-md-3">
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

        <!-- Renderings -->
        <div class="row">
            <div class="col-md-12">
                <div class="page-header">
                    <h1>Renderings</h1>
                </div>
                <asp:Repeater runat="server" ID="rptRenderingFolders" ItemType="SitecoreDocumentor.Core.Models.RenderingFolder">
                    <ItemTemplate>
                        <h2><%# Item.Name %></h2>

                        <asp:Repeater runat="server" ID="rptRenderings" ItemType="SitecoreDocumentor.Core.Models.RenderingMetaItem">
                            <HeaderTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th class="tblcol-icon"></th>
                                            <th class="tblcol-name">Name</th>
                                            <th>Description</th>
                                            <th>Image</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="tblcol-icon">
                                        <img src="<%# "/sitecore/shell/~/icon/" + Item.Icon %>" />
                                    </td>
                                    <td><%# Item.Name %></td>
                                    <td><%# Item.Description %></td>
                                    <td>
                                        <img src="<%# Item.ThumbnailImage %>" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>

                <!-- Templates -->
                <div class="page-header">
                    <h1>Templates</h1>
                </div>
                <div>
                    <asp:Repeater runat="server" ID="rptTemplateFolders" ItemType="SitecoreDocumentor.Core.Models.TemplateFolder">
                        <ItemTemplate>
                            <div class="page-header">
                                <h2><%# Item.Name %></h2>
                            </div>

                            <asp:Repeater ID="rptTemplates" runat="server" ItemType="SitecoreDocumentor.Core.Models.TemplateMetaItem">
                                <ItemTemplate>
                                    <h3>
                                        <img src="<%# "/sitecore/shell/~/icon/" + Item.Icon %>" />
                                        <%# Item.Name %>
                                    </h3>
                                    <p><%# Item.Description %></p>

                                    <asp:Repeater runat="server" ID="rptFields" ItemType="SitecoreDocumentor.Core.Models.FieldItem">
                                        <HeaderTemplate>
                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th class="tblcol-name">Name</th>
                                                        <th>Description</th>
                                                        <th class="tblcol-type">Type</th>
                                                        <th class="tblcol-src">Source</th>
                                                        <th class="tblcol-req">Required?</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr runat="server" class="active" id="trGroup" Visible="False">
                                                <td colspan="5" class="text-uppercase"><%# Item.Section.Name %></td>
                                            </tr>
                                            <tr>
                                                <td class="tblcol-name"><%# Item.Name %></td>
                                                <td><%# Item.LongDescription %></td>
                                                <td class="tblcol-type"><%# Item.FieldType %></td>
                                                <td class="tblcol-src"><%# Item.Source %></td>
                                                <td class="tblcol-req"><%# Item.IsRequired ? "X" : string.Empty %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                            </table>            
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

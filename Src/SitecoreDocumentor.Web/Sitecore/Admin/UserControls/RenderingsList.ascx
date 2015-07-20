<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RenderingsList.ascx.cs" Inherits="SitecoreDocumentor.Web.UserControls.RenderingsList" %>

<div class="page-header">
    <h1>Renderings</h1>
</div>
<div class="row">
    <div class="col-md-2">
        <asp:Repeater runat="server" ID="rptJumplinks" ItemType="SitecoreDocumentor.Web.Models.RenderingFolder">
            <HeaderTemplate>
                <div class="list-group">
            </HeaderTemplate>
            <ItemTemplate><a href="#<%# Item.Id %>" class="list-group-item"><%# Item.Name %></a></ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="col-md-10">
        <asp:Repeater runat="server" ID="rptRenderingFolders" ItemType="SitecoreDocumentor.Web.Models.RenderingFolder">
            <ItemTemplate>
                <h2 id="<%# Item.Id %>"><%# Item.Name %></h2>

                <asp:Repeater runat="server" ID="rptRenderings" ItemType="SitecoreDocumentor.Web.Models.RenderingMetaItem">
                    <HeaderTemplate>
                        <table class="table table-bordered table-condensed">
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
    </div>
</div>

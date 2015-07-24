<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RenderingsList.ascx.cs" Inherits="SitecoreDocumenter.Web.UserControls.RenderingsList" %>
 
<div class="page-header">
    <h1>Renderings</h1>
</div>
<div class="row">
    <div class="col-md-2 no-print">
        <asp:Repeater runat="server" ID="rptJumplinks" ItemType="SitecoreDocumenter.Web.Models.RenderingFolder">
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
        <asp:Repeater runat="server" ID="rptRenderingFolders" ItemType="SitecoreDocumenter.Web.Models.RenderingFolder">
            <ItemTemplate>
                <h2 id="<%# Item.Id %>"><%# Item.Name %></h2>
                <p class="muted"><%# Item.Path %></p>

                <asp:Repeater runat="server" ID="rptRenderings" ItemType="SitecoreDocumenter.Web.Models.RenderingMetaItem">
                    <HeaderTemplate>
                        <table class="table table-bordered table-condensed">
                            <thead>
                                <tr>
                                    <th class="tblcol-icon"></th>
                                    <th class="tblcol-name">Name</th>
                                    <th>Description</th>
                                    <th class="tblcol-datsource">Data source</th>
                                    <th class="tblcol-image">Image</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="tblcol-icon">
                                <img src="<%# Item.Icon %>" />
                            </td>
                            <td><%# Item.Name %></td>
                            <td><%# Item.Description %></td>
                            <td class="tblcol-datasource">
                                <span runat="server" Visible="<%# Item.DataSourceTemplate != null %>"><a href="#<%# Item.DataSourceTemplate != null ? Item.DataSourceTemplate.Id : Guid.Empty %>"><%# Item.DataSourceTemplate != null ? Item.DataSourceTemplate.Name : string.Empty %></a><br /></span>
                                <span runat="server" Visible="<%# !string.IsNullOrEmpty(Item.DataSourceLocation) %>"><%# Item.DataSourceLocation %><br /></span>
                            </td>
                            <td class="tblcol-image">
                                <asp:Image runat="server" ImageUrl='<%# Item.Image + "?w=" + this.ImageWidths %>' Visible="<%# !string.IsNullOrEmpty(Item.Image) %>" />
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

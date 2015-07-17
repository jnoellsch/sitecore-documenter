<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RenderingsList.ascx.cs" Inherits="SitecoreDocumentor.Web.UserControls.RenderingsList" %>

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

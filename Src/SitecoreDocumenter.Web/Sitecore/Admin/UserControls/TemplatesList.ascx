<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplatesList.ascx.cs" Inherits="SitecoreDocumenter.Web.UserControls.TemplatesList" %>
 
<div class="page-header">
    <h1>Templates</h1>
</div>
<div class="row">
    <div class="col-md-2 no-print">
        <asp:Repeater runat="server" ID="rptJumplinks" ItemType="SitecoreDocumenter.Web.Models.TemplateFolder">
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
        <asp:Repeater runat="server" ID="rptTemplateFolders" ItemType="SitecoreDocumenter.Web.Models.TemplateFolder">
            <ItemTemplate>
                <h2 id="<%# Item.Id %>"><%# Item.Name %></h2>
                <p class="muted"><%# Item.Path %></p>

                <asp:Repeater ID="rptTemplates" runat="server" ItemType="SitecoreDocumenter.Web.Models.TemplateMetaItem">
                    <ItemTemplate>
                        <h3 id="<%# Item.Id %>">
                            <img src="<%# Item.Icon %>" />
                            <%# Item.Name %>
                        </h3>
                        <p><%# Item.Description %></p>

                        <asp:Repeater runat="server" ID="rptBaseTemplates" ItemType="SitecoreDocumenter.Web.Models.TemplateMetaItem">
                            <HeaderTemplate>
                                <p class="muted">Inherited from: </p>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li><a href="#<%# Item.Id %>"><%# Item.Name %></a></li>
                            </ItemTemplate>
                            <FooterTemplate></ul></FooterTemplate>
                        </asp:Repeater>

                        <asp:Repeater runat="server" ID="rptInsertOptions" ItemType="SitecoreDocumenter.Web.Models.TemplateMetaItem">
                            <HeaderTemplate>
                                <p class="muted">Allowed children: </p>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li><a href="#<%# Item.Id %>"><%# Item.Name %></a></li>
                            </ItemTemplate>
                            <FooterTemplate></ul></FooterTemplate>
                        </asp:Repeater>

                        <asp:Repeater runat="server" ID="rptFields" ItemType="SitecoreDocumenter.Web.Models.FieldItem">
                            <HeaderTemplate>
                                <table class="table table-bordered table-condensed">
                                    <caption>Fields:</caption>
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
                                <tr runat="server" class="active" id="trGroup" visible="False">
                                    <td colspan="5"><%# Item.Section.Name %></td>
                                </tr>
                                <tr class="tblrow-indent">
                                    <td class="tblcol-name"><%# Item.Name %></td>
                                    <td><%# Item.LongDescription %></td>
                                    <td class="tblcol-type"><%# Item.FieldType %></td>
                                    <td class="tblcol-src"><small class="muted"><%# Item.Source %></small></td>
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

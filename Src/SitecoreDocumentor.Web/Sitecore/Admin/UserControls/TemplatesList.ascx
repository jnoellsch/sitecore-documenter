<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplatesList.ascx.cs" Inherits="SitecoreDocumentor.Web.UserControls.TemplatesList" %>

<div class="page-header">
    <h1>Templates</h1>
</div>
<div>
    <asp:Repeater runat="server" ID="rptTemplateFolders" ItemType="SitecoreDocumentor.Core.Models.TemplateFolder">
        <ItemTemplate>
            <h2><%# Item.Name %></h2>

            <asp:Repeater ID="rptTemplates" runat="server" ItemType="SitecoreDocumentor.Core.Models.TemplateMetaItem">
                <ItemTemplate>
                    <h3>
                        <img src="<%# "/sitecore/shell/~/icon/" + Item.Icon %>" />
                        <%# Item.Name %>
                    </h3>
                    <p><%# Item.Description %></p>

                    <asp:Repeater runat="server" ID="rptFields" ItemType="SitecoreDocumentor.Core.Models.FieldItem">
                        <HeaderTemplate>
                            <table class="table table-bordered table-condensed">
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
                                <td colspan="5" class="text-uppercase"><%# Item.Section.Name %></td>
                            </tr>
                            <tr class="tblrow-indent">
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

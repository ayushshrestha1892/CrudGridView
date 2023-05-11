<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CrudInGridView.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <script type="text/javascript">
                $("[src*=plus]").live("click", function () {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
                    $(this).attr("src", "Images/minus.png");
                });
                $("[src*=minus]").live("click", function () {
                    $(this).attr("src", "Images/plus.png");
                    $(this).closest("tr").next().remove();
                });
            </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gvPhoneBook" runat="server" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="PhoneBookID"
                showHeaderWhenEmplty="true" AllowSorting="true"
                OnRowCommand="gvPhoneBook_RowCommand" OnRowEditing="gvPhoneBook_RowEditing" OnRowCancelingEdit="gvPhoneBook_RowCancelingEdit" OnRowUpdating="gvPhoneBook_RowUpdating" OnRowDeleting="gvPhoneBook_RowDeleting" OnRowDataBound="gvPhoneBook_RowDataBound"
                OnSorting="gvPhoneBook_Sorting"
             
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="gcPhoneBook_SelectedIndexChanged">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />

                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <img alt="" style="cursor: pointer" src="Images/plus.png"/>
                            <asp:Panel ID="pnlDetail" runat="server" Style="display:none" >
                                <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Address" HeaderText="Address"/>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Gender" HeaderText="Gender"/>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div align="center">No records Found</div>
                                    </EmptyDataTemplate>
                                </asp:GridView>

                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="FirstName" SortExpression="FirstName">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("FirstName") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFirstName" Text='<%# Eval("FirstName") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFirstNameFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LastName" SortExpression="LastName">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("LastName") %>' runat="server" />
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:TextBox ID="txtLastName" Text='<%# Eval("LastName") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtLastNameFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Contact" SortExpression="Contact">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Contact") %>' runat="server" />
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:TextBox ID="txtContact" Text='<%# Eval("Contact") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtContactFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email" SortExpression="Email">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Email") %>' runat="server" />
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmail" Text='<%# Eval("Email") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEmailFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>


                    
                    <asp:TemplateField HeaderText="SortID">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("SortID") %>' runat="server" />
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:TextBox ID="txtSort" Text='<%# Eval("SortID") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtSortFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Salary">
                        <ItemTemplate>
                            <asp:Label ID="salary" Text='<%# Eval("Salary") %>' runat="server" />
                        </ItemTemplate>

<%--                        <EditItemTemplate>
                            <asp:TextBox ID="txtSalary" Text='<%# Eval("Salary") %>' runat="server" />
                        </EditItemTemplate>--%>
                        
                        <FooterTemplate>
                          <%--  <asp:TextBox ID="txtSalary" runat="server" />--%>
                            <asp:Label ID="totalsal" runat="server">Total:</asp:Label>


                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />

                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px" />

                        </EditItemTemplate>
                        <FooterTemplate>
                       
                            <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px" />
                        </FooterTemplate>
                    </asp:TemplateField>    
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblSuccessMessage" Text=" " runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text=" " runat="server" ForeColor="Red" />
        </div>
    </form>

</body>
</html>

<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="LSCA0010.aspx.cs" Inherits="Page_LSCA0010" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="LS.CarbonAccountingModule.LSCASetupMaint"
        PrimaryView="Setup"
        >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Setup" Width="100%" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule  runat="server" StartRow="True"/>
			<px:PXSelector runat="server" ID="slTransactionNumberingID" DataField="TransactionNumberingID"/>
			<px:PXSelector runat="server" ID="slCarbonInventoryID" DataField="CarbonInventoryID"/>
			<px:PXSelector runat="server" ID="slCarbonSiteID" DataField="CarbonSiteID"/>
			
		</Template>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXFormView>
</asp:Content>


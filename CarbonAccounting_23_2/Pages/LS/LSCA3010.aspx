<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="LSCA3010.aspx.cs" Inherits="Page_LSCA3010" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="LS.CarbonAccountingModule.LSCATransactionEntry"
        PrimaryView="Document"
        >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Document" Width="100%" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule  runat="server" StartRow="True"/>
			<px:PXDropDown runat="server" ID="ddTransactionType" DataField="TransactionType" CommitChanges="true" />
			<px:PXSelector runat="server" ID="slReferenceNumber" DataField="ReferenceNumber" CommitChanges="true" />
			<px:PXDropDown runat="server" ID="ddStatus" DataField="Status"  />
			<px:PXDateTimeEdit runat="server" ID="dtTranDate" DataField="TranDate" />

			<px:PXLayoutRule runat="server" StartRow="False" StartColumn="True"/>
			<px:PXDropDown runat="server" ID="ddInventoryTransactionType" DataField="InventoryTransactionType"  />
			<px:PXTextEdit runat="server" ID="txtInventoryTranRefNbr" DataField="InventoryTranRefNbr"/>
			
			<px:PXLayoutRule runat="server" StartRow="True" ColumnSpan="2"/>
			<px:PXTextEdit runat="server" ID="txtDescr" DataField="Descr"/>
			
		</Template>
		<AutoSize Enabled="True" Container="Window" />
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Details" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="Transactions">
			    <Columns>
				    <px:PXGridColumn DataField="TransactionType"/>
				    <px:PXGridColumn DataField="ReferenceNumber"/>
				    <px:PXGridColumn DataField="LineNbr"/>
				    <px:PXGridColumn DataField="InventoryID"/>
				    <px:PXGridColumn DataField="UOM"/>
				    <px:PXGridColumn DataField="Qty"/>
				    <px:PXGridColumn DataField="BaseQty"/>
				    <px:PXGridColumn DataField="Rate"/>
				    <px:PXGridColumn DataField="ExtCarbonEquivQty"/>
				    <px:PXGridColumn DataField="ReasonCode"/>
				    <px:PXGridColumn DataField="TranDescr"/>
			    </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
		<ActionBar >
		</ActionBar>
	</px:PXGrid>
</asp:Content>

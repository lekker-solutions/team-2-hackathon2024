<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Width="100%" DefaultControlID="edProdOrdID" 
            Caption="Prod Order" DataKeyNames="ProdOrdID" DataMember="ProdItemRecords" FilesIndicator="True" ActivityIndicator="True" 
                   NotifyIndicator="True" ActivityField="NoteActivity" NoteIndicator="True">
        <Template>
            <px:PXSegmentMask ID="edBranchID" runat="server" DataField="BranchID" />
            
            <px:PXTextEdit ID="edTotalCarbonEmission2" runat="server" DataField="TotalCarbonEmission" CommitChanges="true"/>
        </Template>
    </px:PXFormView>
</asp:Content>

<Template1>
    <px:PXGrid ID="gridOperations" runat="server" DataSourceID="ds" Width="100%" Height="100%" SkinID="Details" Caption="Operations" SyncPosition="true" >
        <Levels>
            <px:PXGridLevel DataKeyNames="ProdOrdID,OperationID" DataMember="ProdOperRecords">
                <RowTemplate>
                    <px:PXSelector ID="edWcID" runat="server" DataField="WcID" AllowEdit="True" />

                     <px:PXTextEdit ID="edTotalCarbonEmission3" runat="server" DataField="TotalCarbonEmission"/>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="WcID" Width="90px" AutoCallBack="True" />
                    
                    <px:PXGridColumn DataField="TotalCarbonEmission" Width="100px" TextAlign="Right" AutoCallBack="True" />
                </Columns>
            </px:PXGridLevel>
        </Levels>
    </px:PXGrid>
</Template1>
<Template2>
<px:PXTab ID="tab" runat="server" Width="100%">
<Items>
<px:PXTabItem Text="Materials" LoadOnDemand="True" RepaintOnDemand="True">
    <Template>
        <px:PXGrid ID="gridMatl" runat="server" DataSourceID="ds" Width="100%" SkinID="DetailsInTab" SyncPosition="True" TabIndex="2600" StatusField="Availability" >
            <Levels>
                <px:PXGridLevel DataKeyNames="ProdOrdID,OperationID,LineID" DataMember="ProdMatlRecords">
                    <RowTemplate>
                        <px:PXSegmentMask ID="edMatlInventoryID" runat="server" DataField="InventoryID" AllowEdit="True"/>
                                                
                        <px:PXTextEdit ID="edTotalCarbonEmission" runat="server" DataField="TotalCarbonEmission"/>
                    </RowTemplate>
                    <Columns>
                        <px:PXGridColumn DataField="InventoryID" AutoCallBack="True" Width="120px"/>

                        <px:PXGridColumn DataField="TotalCarbonEmission" Width="100px" TextAlign="Right" AutoCallBack="True" />
                    </Columns>
                </px:PXGridLevel>
            </Levels>
        </px:PXGrid>
    </Template>
</px:PXTabItem>
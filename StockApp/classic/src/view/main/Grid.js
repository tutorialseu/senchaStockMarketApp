Ext.define('stockMarketApp.view.main.Grid', {
    extend: 'Ext.grid.Panel',
    store: {type: 'StocksStore'},
    xtype: 'mainGrid',
    title: 'Stocks',
    columns: [
        {text: 'Name', dataIndex: 'name', flex: 1},
        {text: 'Price Now', dataIndex: 'priceNow', flex: 1},
        {
            text: 'Last month',
            dataIndex: 'diff',
            xtype: 'widgetcolumn',
            widget: {
                xtype: 'sparklineline',
                fillColor: '#ddf',
                width: 100,
                height: 20
            },
            flex: 1
        }
    ],
    renderTo: Ext.getBody()
})
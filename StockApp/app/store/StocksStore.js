Ext.define('stockMarketApp.store.StocksStore', {
    extend: 'Ext.data.Store',
    alias: 'store.StocksStore',
    model: 'stockMarketApp.model.Stock',
    autoLoad: true,
    fields: [
        {name: 'name'},
        {name: 'priceNow'},
        {name: 'diff'}
    ],
    proxy: {
        type:'ajax',
        reader: {
            type: 'json',
            rootProperty: 'items'
        },
        url: 'http://localhost:5283/stock' // TODO adjust
    }    
    
});
Ext.define('stockMarketApp.model.Stock', {
    extend: 'stockMarketApp.model.Base',

    store: 'stockMarketApp.store.StockStore',
    fields: [
        {name: 'name', type: 'string'},
        {name: 'priceNow', type: 'float'},
        {name: 'diff', type: 'float'}
    ]
});

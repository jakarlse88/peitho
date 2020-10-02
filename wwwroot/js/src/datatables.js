import {DataTable} from "simple-datatables";

export function createTable(tableId) {
    if (typeof(tableId) === 'undefined'
        || tableId === null
        || tableId.length === 0)
    {
        throw "Parameter 'tableId' was undefined, null, or empty.";
    }
    
    const myTable = new DataTable(`{tableId}`);
}

    
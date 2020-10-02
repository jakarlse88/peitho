import {createTable} from "./datatables";

export function CreateDataTable(tableId) {
    if (typeof(tableId) === 'undefined' 
        || tableId === null
        || tableId.length === 0)
    {
        throw "Parameter 'tableId' was undefined, null, or empty.";
    }
    
    createTable(tableId);
}

export function DestroyDataTable() {
    console.log("asad");
}
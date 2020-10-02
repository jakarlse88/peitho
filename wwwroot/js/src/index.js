import { createTable, destroyTable } from "./datatables";

export function CreateDataTable(tableId) {
    if (typeof(tableId) === 'undefined' 
        || tableId === null
        || tableId.length === 0)
    {
        throw "Parameter 'tableId' was undefined, null, or empty.";
    }
    
    createTable(tableId);
}


// TODO: get rid of this if not needed
// export function DestroyDataTable(tableId) {
//     if (typeof(tableId) === 'undefined'
//         || tableId === null
//         || tableId.length === 0)
//     {
//         throw "Parameter 'tableId' was undefined, null, or empty.";
//     }
//    
//     destroyTable(tableId);
// }
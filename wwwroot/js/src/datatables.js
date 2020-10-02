import {DataTable} from "simple-datatables";

export function createTable(tableId) {
    if (typeof(tableId) === 'undefined'
        || tableId === null
        || tableId.length === 0)
    {
        throw "Parameter 'tableId' was undefined, null, or empty.";
    }
    
    const table = document.querySelector("#" + tableId);
    
    new DataTable(table);
}

// TODO: get rid of this if not needed
// export function destroyTable(tableId) {
//     if (typeof(tableId) === 'undefined'
//         || tableId === null
//         || tableId.length === 0)
//     {
//         throw "Parameter 'tableId' was undefined, null, or empty.";
//     }
//    
//     const table = document.querySelector("#" + tableId);
//    
//     table.DataTable().destroy();
// }
    
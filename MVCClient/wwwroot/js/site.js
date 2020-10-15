// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Syncfusion grid related to default field values

function fieldValue(gridObject, field) {
    return gridObject.editModule.formObj.element.querySelector("#" + gridObject.element.id + field).ej2_instances[0].value;
}

function setFieldValue(gridObject, field1, field2) {
    gridObject.editModule.formObj.element.querySelector("#" + gridObject.element.id + field1).ej2_instances[0].value = fieldValue(gridObject, field2);
}



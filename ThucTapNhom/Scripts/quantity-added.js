if (sessionStorage.getItem('listAdded') != null) {
    var total = 0;
    var listAddedString = sessionStorage.getItem('listAdded');
    listAdded = JSON.parse(listAddedString);
    for (var key in listAdded) {
        if (Object.prototype.hasOwnProperty.call(listAdded, key)) {
            total += listAdded[key].quantity;
        }
    }
    $('#added-quantity').html(total);
}
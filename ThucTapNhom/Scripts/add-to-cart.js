$('.btn-add').click(function () {
    let id = $(this).next().text();
    let tr = $(this).parent().parent().children();
    let name = tr.eq(2).text().trim();
    let price = tr.eq(1).text().trim().slice(0, -2);
    let img = tr.eq(0).children().attr("src");
    let product = {
        'id': id,
        'img': img,
        'price': price,
        'name': name,
        'quantity': 1
    }
    var listAdded = {};
    if (sessionStorage.getItem('listAdded') != null) {
        let listAddedString = sessionStorage.getItem('listAdded');
        listAdded = JSON.parse(listAddedString);
        let currentAdd = listAdded[id];
        if (currentAdd != null) {
            product.quantity += currentAdd.quantity;
        }
    }
    listAdded[product.id] = product;
    sessionStorage.setItem('listAdded', JSON.stringify(listAdded));
    alert("Đã thêm vào giỏ hàng!");
    location.reload();
});
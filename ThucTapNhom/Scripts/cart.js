function load() {
    var totalPrice = 0;
    if (sessionStorage.getItem('listAdded') != null) {
        var listAddedString = sessionStorage.getItem('listAdded');
        var listAdded = JSON.parse(listAddedString);
        var htmlContent = '';
        for (var key in listAdded) {
            if (Object.prototype.hasOwnProperty.call(listAdded, key)) {
                var unitTotalPrice = listAdded[key].price * listAdded[key].quantity;
                htmlContent += '<tr>';
                htmlContent += '   <td width="100px" height="100px" class="package-img-cart">';
                htmlContent += '       <img width="100%" height="100%" src=\'' + listAdded[key].img + '\' alt="">';
                htmlContent += '   </td>';
                htmlContent += '   <td class="text-center package-name-cart">' + listAdded[key].name + '</td>';
                htmlContent += '   <td class="text-center package-price-cart">' + listAdded[key].price + '</td>';
                htmlContent += '   <td class="text-center">';
                htmlContent += '    <btn class="mr-2 cart-add" style="cursor: pointer">+</btn>';
                htmlContent += '   <span class="d-none package-id-cart">' + listAdded[key].id + '</span>';
                htmlContent += '    <input class="text-center package-quantity-cart" name="quantity" type="text" size="1" name="count" value="' + listAdded[key].quantity + '">';
                htmlContent += '    <btn class="ml-2 cart-minus" style="cursor: pointer">-</btn>';
                htmlContent += '   </td>';
                htmlContent += '   <td class="text-center unit-price">' + unitTotalPrice + '</td>';
                htmlContent += '   <td class="text-center "><a href="" class="btn-delete"><i class="fas fa-trash-alt"></i></a></td>';
                htmlContent += '</tr>';
                totalPrice += unitTotalPrice;
            }
        }
        htmlContent += '<td colspan="3">Mã giảm giá/ Quà tặng:</td>';
        htmlContent += ' <td colspan="3" class="text-center font-weight-bold"> Tổng tiền: <span id="total-price">' + totalPrice + '</span> VND</td>';
        $('#list-cart').html(htmlContent);
        let currentId = 0;
        function updateQuantity() {
            if (listAdded[currentId].quantity < 1) {
                delete listAdded[currentId];
                if (Object.keys(listAdded).length < 1) {
                    sessionStorage.removeItem('listAdded');
                }
            }
            sessionStorage.setItem('listAdded', JSON.stringify(listAdded));
            load();
        }
        $('.package-price-cart').each(function () {
            var price = $(this).text();
            $(this).text(numeral(price).format('0,0'));
        });
        $('.unit-price').each(function () {
            var price = $(this).text();
            $(this).text(numeral(price).format('0,0'));
        });
        $('#total-price').each(function () {
            var price = $(this).text();
            $(this).text(numeral(price).format('0,0'));
        });
        $('input[name ="quantity"]').on('keypress', function (e) {
            if (e.which === 13) {
                currentId = $(this).prev().text();
                if (parseInt($(this).val()) <= 0) {
                    let rs = confirm("Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng?");
                    if (rs) {
                        updateQuantity();
                    } else {
                        $(this).val(listAdded[currentId].quantity);
                    }
                } else {
                    listAdded[currentId].quantity = parseInt($(this).val());
                    updateQuantity();
                }

            }
        });
        $('.cart-add').click(function () {
            currentId = $(this).next().text();
            listAdded[currentId].quantity++;
            updateQuantity();
        });
        $('.cart-minus').click(function () {
            currentId = $(this).prev().prev().text();
            listAdded[currentId].quantity--;
            if (listAdded[currentId].quantity === 0) {
                let rs = confirm("Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng?");
                if (rs) {
                    updateQuantity();
                } else {
                    listAdded[currentId].quantity++;
                }
            }
            updateQuantity();
        });
        $('.btn-delete').click(function () {
            currentId = $(this).parent().prev().prev().children().eq(2).text();
            listAdded[currentId].quantity = 0;
            updateQuantity();
        });
        $('#btn-pay').click(function () {
            var customerName = $('#pay-name').val();
            var customerPhone = $('#pay-phone').val();
            var products = [];
            for (let i in listAdded) {
                if (Object.prototype.hasOwnProperty.call(listAdded, i)) {
                    products.push({
                        id: i,
                        quantity: listAdded[i].quantity
                    });
                }
            }
            var orderData = {
                products: products,
                totalPrice: totalPrice,
                customerName: customerName,
                customerPhone: customerPhone
            }
            $.ajax({
                    url: 'api/orders',
                    data: orderData,
                    method: 'POST',
                    success: function (data, textStatus, jqXHR) {
                        alert("Gửi đơn hàng thành công");
                        sessionStorage.clear();
                        location.replace("/products");
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(textStatus);
                    }
                });
            });
    } else {
        alert('Chưa có sản phẩm nào trong giỏ hàng');
    }
}

load();


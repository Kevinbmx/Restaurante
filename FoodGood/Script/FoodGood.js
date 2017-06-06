// Javascript
$(document).ready(function () {

    function doResize() {
        var rightMenu = $('#rightMenu');
        var header = $('#header');
        rightMenu.css({ top: (header.outerHeight() + 20) + 'px' });
        $('#carritoRight').height($(window).height());
        resizeBigImage();
    }
    function abrirCarrito() {
        backPopupOpen();
        $('#carritoRight').removeClass('entradaCarrito');
        $('#carritoRight').removeClass('salidaCarrito');
        //animacion de entrada
        $('#carritoRight').addClass('entradaCarrito');
        $('#carritoRight').fadeIn();
        lockScroll();
    }
    function cerrarCarrito() {
        backPopupClose();
        $('#carritoRight').removeClass('entradaCarrito');
        $('#carritoRight').removeClass('salidaCarrito');
        //animacion de salida
        $('#carritoRight').addClass('salidaCarrito');
        setTimeout("$('#carritoRight').fadeOut()", 500);
        unLockScroll();
    }

    $('#btnOpenCart').on('click', function () {
        abrirCarrito();
        return false;
    });

    $('#carritoClose').on('click', function () {
        cerrarCarrito();
        return false;
    });

    function backPopupClose() {
        //    $('#popupBack').show();
        $('#popupBack').fadeOut(250);
    }


    function backPopupOpen() {
        //    $('#popupBack').show();
        $('#popupBack').fadeIn(250);
    }
    function lockScroll() {
        $('body').css({ 'overflow': 'hidden' });
        $(document).bind('scroll', function () {
            window.scrollTo(0, 0);
        });
    }

    function conteoCarrito() {
        var cantCarrito = $("#itemCarrito_cantItem2").val();
        if (cantCarrito != 0)
            $("#btnOpenCart").text("Carrito" + cantCarrito);
    }

    function unLockScroll() {
        $(document).unbind('scroll');
        $('body').css({ 'overflow': 'visible' });
    }

    function anadidoAlContadoPopupOpen() {
        $('#AnadidoAlContadoPopup').fadeIn();
        backPopupOpen();
        lockScroll();
    }

    function anadidoAlContadoPopupClose() {
        $('#AnadidoAlContadoPopup').fadeOut();
        backPopupClose();
        unLockScroll();
    }
});
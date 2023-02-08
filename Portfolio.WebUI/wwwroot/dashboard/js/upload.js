$('#Images').on('change', function () {
    var countFiles = $(this)[0].files.length;
    var imgPath = $(this)[0].value;
    var extn = imgPath.substring(imgPath.lastIndexOf('.') + 1).toLowerCase();

    if (extn == "png" || extn == "jpg" || extn == "jpeg" || extn == "svg") {

        for (var i = 0; i < countFiles; i++) {
            $type = this.files[i].type;

            var reader = new FileReader();
            reader.onload = function (e) {
                $sayi = $('.PictureItem').length + 1;

                DivID = Password.generate(16);

                $html = '<div style="width:12rem;" id="Resim' + DivID + '" class="PictureItem ">';
                $html += '<div style="width:12rem;" class="item">';
                $html += '<div class="sayi badge badge-dark">' + $sayi + '</div>';
                //$html += '<a class="btn btn-danger btn-sm text-white  UrunResimSil" id="Delete' + DivID + '" data-id="' + DivID + '">X</a>';
                $html += '<div class="ImageBG" data-id="' + DivID + '" data-sort="' + $sayi + '" data-type="' + $type + '" data-val="' + e.target.result + '" style="background-image: url(' + e.target.result + ');"></div>';
                $html += '</div>';
                $html += '</div>';

                $('#ImagesDiv').append($html);

                //$('.UrunResimSil').click(function () {
                //    DivID = $(this).attr("data-id");
                //    console.log(DivID)
                //    $('#Resim' + DivID).remove();
                //});


                //$('.ImageRemove').click(function () {
                //    $id = $(this).attr("data-id");
                //    $('#' + $id).remove();
                //})
            }
            reader.readAsDataURL(this.files[i]);

        }

    } else {
        toastr.warning("File must be Image (jpg,jpeg,png,svg)");
    }
    setTimeout(function () {
        $("body").loading('stop');
    }, 1500);
});

var Password = {

    _pattern: /[a-zA-Z0-9_]/,


    _getRandomByte: function () {
        // http://caniuse.com/#feat=getrandomvalues
        if (window.crypto && window.crypto.getRandomValues) {
            var result = new Uint8Array(1);
            window.crypto.getRandomValues(result);
            return result[0];
        }
        else if (window.msCrypto && window.msCrypto.getRandomValues) {
            var result = new Uint8Array(1);
            window.msCrypto.getRandomValues(result);
            return result[0];
        }
        else {
            return Math.floor(Math.random() * 256);
        }
    },

    generate: function (length) {
        return Array.apply(null, { 'length': length })
            .map(function () {
                var result;
                while (true) {
                    result = String.fromCharCode(this._getRandomByte());
                    if (this._pattern.test(result)) {
                        return result;
                    }
                }
            }, this)
            .join('');
    }

};

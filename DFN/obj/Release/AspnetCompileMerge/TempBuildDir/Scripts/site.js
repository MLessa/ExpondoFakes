checkSGAlert = () => {
    if ($("#checkbox").prop("checked"))
        $("#tweetText").val($("#tweetText").val() + " @slpng_giants_pt");
    else
        $("#tweetText").val($("#tweetText").val().replace(" @slpng_giants_pt", ""));
    $(".charsLasting").text("CARACTERES RESTANTES: " + (200 - $("#tweetText").val().length));
};

submitForm = () => {
    if ($("#tweetText").val().length > 200) {
        $.alert({
            title: 'Psiu!',
            content: 'Sabemos que você quer ajudar, meu seu comentário precisa ter no máximo 200 caracteres.',
        });
        return;
    }

    if ($(".submitBtn").hasClass("enabled"))
        $("#mainForm").submit();
}

isURLValid = (str) => {
    try {
        new URL(str);
    } catch (_) {
        return false;
    }
    return true;
};

$("#tweetText").keydown(function () {
    if ($("#checkbox").prop("checked")) {
        $('#tweetText').prop('selectionEnd', $("#tweetText").val().length - 17);
    }
});

$("#tweetText").keyup(function () {
    let lastingChars = (200 - $("#tweetText").val().length);
    $(".charsLasting").text("CARACTERES RESTANTES: " + lastingChars);

    if (lastingChars < 0)
        $(".charsLasting").css('color', 'olivedrab');
    else
        $(".charsLasting").css('color', '');

});

uploadFile = elm => {
    props = {
        noRevoke: true,
        canvas: false,
        contain: true
    };
    $("#sendFileFieldText").text(elm.files[0].name);
    $(".sendFileBtn span").text("Trocar")
    var loadedImage = loadImage(
        elm.files[0],
        img => {
            $("#postDetailDiv").show();

            if ($("#takenImage").length) {
                $("#takenImage").remove();
            }

            $(img).attr('id', 'takenImage');
            $(img).addClass('tweetImage');
            $(".submitBtn").removeClass('disabled');
            $(".submitBtn").addClass('enabled');
            document.getElementById("imageDiv").appendChild(img);
        },
        props
    );
}

$(document).ready(function () {
    if (posted === "success") {
        $.alert({
            title: 'MUITO OBRIGADO!',
            content: 'Sua denúncia foi postada com sucesso no nosso Twitter. #TodosContraFakeNews',
        });
    } else if (posted === "fail") {
        $.alert({
            title: 'Desculpa, infelizmente tivemos um problema',
            content: 'Não conseguimos postar sua denúncia no Twitter. Por favor, tente novamente. #TodosContraFakeNews',
        });
    }
});
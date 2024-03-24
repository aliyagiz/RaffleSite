
$(document).ready(function () {
    const jsConfetti = new JSConfetti();
    var counter = 0;
    var lastIndex = 0;

    $('.to-do-item').hide();

    $('.to-do-item').each(function (index) {
        var delay = 600 * index;

        setTimeout(function () {
            var scrollPosition = $(document).height() - $(window).height();
            $('html, body').scrollTop(scrollPosition);

            $(this).fadeIn(600);
        }.bind(this), delay);

        if (counter % 2 == 0) {
            setTimeout(function () {
                jsConfetti.addConfetti();
            }, 600 * index)
        }
        counter++;
        lastIndex = index;
    });
});




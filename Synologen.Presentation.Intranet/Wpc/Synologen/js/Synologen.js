(function ($) {
    $.SynologenIntranet = $.SynologenIntranet || {};

    $.extend($.SynologenIntranet, {
        init: function () {
            $("html").addClass("js-enabled");
            $.SynologenIntranet.initYammer();
            $.SynologenIntranet.initProgressBar(6);
            $.SynologenIntranet.initRadioButtonLists();
            $.SynologenIntranet.initAutogiroDetailView();
            $.SynologenIntranet.initDisableTabLinks();
        },

        initDisableTabLinks: function () {
            $('#tab-navigation a').click(function (event) {
                event.preventDefault();
            });
            //the underline hover thing is set to 'none' in the css...
        },

        initYammer: function () {
            //$(".fancybox").fancybox();
        },

        initProgressBar: function (numberOfSteps) {
            function getPercentage(currentStep) {
                if (currentStep == numberOfSteps) return 100;
                return (100 / numberOfSteps) * currentStep;
            }

            for (var i = 1; i <= numberOfSteps; i++) {
                $(".step" + i + " #progressbar").progressbar({
                    value: getPercentage(i)
                });
            }
        },

        initRadioButtonLists: function () {
            $("ul.radioButtonItems").addClass("radio-list");
            $(".radio-list li label").each(function () {
                var text = $(this).text();
                $(this).replaceWith(text);
            });
        },
        
        initAutogiroDetailView: function () {
            var input = $("input.custom-number-of-withdrawals");
            var span = $(".subscription-options li span").last();
            span.append(input);
        }
    });

})(jQuery);

$(document).ready($.SynologenIntranet.init);
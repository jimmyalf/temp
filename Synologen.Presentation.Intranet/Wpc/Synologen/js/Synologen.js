(function ($) {
	$.SynologenIntranet = $.SynologenIntranet || {};

	$.extend($.SynologenIntranet, {
		init: function () {
			$("html").addClass("js-enabled");
			$.SynologenIntranet.initYammer();
			$.SynologenIntranet.initProgressBar(6);
			$.SynologenIntranet.initAutogiroDetailView();
			$.SynologenIntranet.initRadioButtonLists();
		},

		initYammer: function () {
			$(".fancybox").fancybox();
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

		initAutogiroDetailView: function () {
			var input = $("input.custom-number-of-withdrawals");
			var span = $("li .custom-number-of-withdrawals");
			span.append(input);
		},

		initRadioButtonLists: function () {
			$("ul.radioButtonItems").addClass("radio-list");
			$(".radio-list li label").each(function () {
				var text = $(this).text();
				$(this).replaceWith(text);
			});
		}
	});

})(jQuery);

$(document).ready($.SynologenIntranet.init);
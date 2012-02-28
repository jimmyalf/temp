(function ($) {
	$.SynologenIntranet = $.SynologenIntranet || {};

	$.extend($.SynologenIntranet, {
		init: function () {
			$("html").addClass("js-enabled");
			$.SynologenIntranet.initYammer();
			$.SynologenIntranet.initProgressBar(6);
			$.SynologenIntranet.initRadioButtonLists();
			$.SynologenIntranet.initAutogiroDetailView();
			$.SynologenIntranet.initPlaceHolderPolyFill();
		},

		initDisableTabLinks: function () {
			$('#tab-navigation a').click(function (event) {
				event.preventDefault();
			});
			//the underline hover thing is set to 'none' in the css...
		},

		initYammer: function () {
			if (!$.isFunction($.fancybox)) {
				console.log("Fancybox plugin cannot be found.");
				return;
			}
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

		initRadioButtonLists: function () {
			$("ul.radioButtonItems").addClass("radio-list");
			$(".radio-list li label").each(function () {
				var text = $(this).text();
				$(this).replaceWith(text);
			});
		},

		initAutogiroDetailView: function () {
			var input = $("input.custom-number-of-withdrawals");
			var li = $(".step5 .radio-list li").last();
			li.append(input);
		},

		initPlaceHolderPolyFill: function () {
			if (!$.isFunction($('input, textarea').placeholder)) {
				console.log("Placeholder polyfill plugin cannot be found.");
				return;
			}
			$('input, textarea').placeholder();
		}
	});

})(jQuery);

$(document).ready($.SynologenIntranet.init);
(function ($) {
	$.SynologenIntranet = $.SynologenIntranet || {};

	$.extend($.SynologenIntranet, {
		init: function() {
			$("html").addClass("js-enabled");
			$.SynologenIntranet.initYammer();
			$.SynologenIntranet.initProgressBar(6);
		},

		initYammer: function() {
			$(".fancybox").fancybox();
		},

		initProgressBar: function(numberOfSteps) {
			function getPercentage(currentStep) {
				if (currentStep == numberOfSteps) return 100;
				return (100 / numberOfSteps) * currentStep;
			}
			
			for (var i = 1; i <= numberOfSteps; i++) {
				$(".step" + i + " #progressbar").progressbar({
					value: getPercentage(i)
				});
			}
		}
	});

})(jQuery);

$(document).ready($.SynologenIntranet.init);
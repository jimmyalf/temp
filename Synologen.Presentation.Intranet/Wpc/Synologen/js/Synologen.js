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
			$.SynologenIntranet.initMinimizeYammerText();
			$.SynologenIntranet.initCalculateMontlyAGWithdrawalAmounts();
		},

		initDisableTabLinks: function () {
			$('#tab-navigation a').click(function (event) {
				event.preventDefault();
			});
			//the underline hover thing is set to 'none' in the css...
		},

		initYammer: function () {
			if (!$.isFunction($.fancybox)) return;
			$(".fancybox").fancybox();
		},

		initProgressBar: function (numberOfSteps) {
			if (!$.isFunction($("#progressbar").progressbar)) return;
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
			if (!$.isFunction($('input, textarea').placeholder)) return;
			$('input, textarea').placeholder();
		},

		initMinimizeYammerText: function () {
			if (!$.isFunction($('.yammer-content').jTruncate)) return;
			$('.yammer-content').jTruncate({
				length: 100,
				minTrail: 0,
				moreText: "Läs mer",
				lessText: "Minimiera"
			});
		},

		initCalculateMontlyAGWithdrawalAmounts: function () {
			$("#txtProductAmount").change(updateAmounts).keyup(updateAmounts);
			$("#txtFeeAmount").change(updateAmounts).keyup(updateAmounts); ;
			$("#rblSubscriptionTime").change(updateAmounts);
			$("#txtCustomNumberOfTransactions").change(updateAmounts).keyup(updateAmounts); ;

			function updateAmounts() {
				var totalAmount = getTotalAmount();
				var numberOfWithdrawals = getNumerOfWithdrawals();
				var montlyAmount = getMontlyAmount(numberOfWithdrawals, totalAmount);
				if (isNaN(totalAmount)) $("#total-withdrawal-amount").attr("value", "");
				else $("#total-withdrawal-amount").attr("value", totalAmount.toString().replace(".", ","));
				if (isNaN(montlyAmount)) $("#montly-withdrawal-amount").attr("value", "");
				else $("#montly-withdrawal-amount").attr("value", montlyAmount.toString().replace(".", ","));
			}

			function getTotalAmount() {
				var productAmount = parseFloat($("#txtProductAmount").val().replace(",", "."));
				var feeAmount = parseFloat($("#txtFeeAmount").val().replace(",", "."));
				return (productAmount + feeAmount).toFixed(2);
			}

			function getMontlyAmount(numerOfWithdrawals, totalAmount) {
				return (totalAmount / numerOfWithdrawals).toFixed(2);
			}

			function getNumerOfWithdrawals() {
				var selectedNumerOfWithdrawals = parseInt($("#rblSubscriptionTime input:checked").val());
				var customNumberOfWithdrawals = parseInt($("#txtCustomNumberOfTransactions").val());
				if (selectedNumerOfWithdrawals == -1) return customNumberOfWithdrawals;
				return selectedNumerOfWithdrawals;
			}
		}
	});

})(jQuery);

$(document).ready($.SynologenIntranet.init);
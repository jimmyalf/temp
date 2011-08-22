(function($) {
	$.Synologen = $.Synologen || {};

	$.extend($.Synologen, {
		Config: {
			ConfirmAction: "Är du säker på att du vill fortsätta?",
			ConfirmCustom: "Är du säker på att du vill {0}?"
		}
	});

	$.extend($.Synologen, {
		init: function() {
			$("html").addClass("js-enabled");
			$.Synologen.initConfirmAction();
			$.Synologen.initAddContractArticleSPCSAccountAutoUpdate();
			$.Synologen.initWebformsValidationMessageSuccessHiding();
			$.Synologen.initMVCValidationMessageSuccessHiding();
		},

		initConfirmAction: function() {
			$(".confirm-action").click($.Synologen.confirmAction);
		},
		
		confirmAction: function() {
			var title = $(this).attr("title");
			if (title && title.length > 0) {
				title = title.substr(0, 1).toLowerCase() + title.substr(1, title.length - 1);
				var message = $.Synologen.Config.ConfirmCustom.replace("{0}", title);
				return confirm(message);
			}
			return confirm($.Synologen.Config.ConfirmAction);
		},

		initAddContractArticleSPCSAccountAutoUpdate: function() {
			$('.postback-enabled').change(function() {
				var selectedArticle = $(this).attr('value');
				if (selectedArticle > 0) {
					var url = "/components/synologen/contract-sales/article/".concat(selectedArticle, "/json");
					$.getJSON(url, null, function(data) {
						if (data && data.SPCSAccountNumber) {
							var spcsAccountNumberTextBox = $('#spcs-account-number');
							spcsAccountNumberTextBox.val(data.SPCSAccountNumber);
							spcsAccountNumberTextBox
								.animate({ backgroundColor: '#FFDDDD' }, 500)
								.animate({ backgroundColor: '#FFFFFF' }, 500);
						}
					});
				}
			});
		},

		initWebformsValidationMessageSuccessHiding: function() {
			$('#validation-message .success').delay(5000).slideUp('slow', function() {
				$(this).remove();
			});
		},

		initMVCValidationMessageSuccessHiding: function() {
			$('.action-messages.contains-success').delay(5000).slideUp('slow', function() {
				$(this).remove();
			});
		}
	});

})(jQuery);

$(document).ready($.Synologen.init);
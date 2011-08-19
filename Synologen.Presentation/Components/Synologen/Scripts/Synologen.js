(function($) {
	$.Synologen = $.Synologen || {};

	$.extend($.Synologen, {
		init: function() {
			$("html").addClass("js-enabled");
			$.Synologen.initAddContractArticleSPCSAccountAutoUpdate();
			$.Synologen.initWebformsValidationMessageSuccessHiding();
			$.Synologen.initMVCValidationMessageSuccessHiding();
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
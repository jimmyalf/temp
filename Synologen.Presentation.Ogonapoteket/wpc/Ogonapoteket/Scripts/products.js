var Ledlab;
(function() {
	Ledlab = function() {
		var currentProductContainer;
		var originalLinkText;
		var loadedItems = [];
		
		var initProducts = function() {
			$(".go-to-article a").click(hijackRequest);
 		};
		
		var hijackRequest = function() {
			var link = $(this);
			currentProductContainer = $(link.parents(".product-details").get(0));

			if (link.hasClass("expanded")) {
				link.html(originalLinkText);
				currentProductContainer.find(".article-details").remove();
				currentProductContainer.find("label").remove();
			} else {
				originalLinkText = link.html();
				link.html("Dölj detaljer");
				getArticles(link.attr("href"));
			}
			
			link.toggleClass("expanded");
			return false;
		};
		
		var getArticles = function(url) {
			var items = loadedItems[url];
			if (items) {
				getItems(items);
			} else {
				currentProductContainer.append("<div class='article-details loading'>Loading...</div>");
				$.ajax({
					type: "GET",
					url: url,
					contentType: "text/html; charset=utf-8",
					dataType: "html",
					success: loadSuccess,
					error: loadError
				});
			}
		};
		
		var loadSuccess = function(data) {
			var items = $(data).find("#articles > li");
			if (items.length < 1) return; // TODO: Handle error
			loadedItems[this.url] = items;
			getItems(items);
		};
		
		var getItems = function(items) {
			var item;
			if (items.length == 1) {
				item = $(items.get(0));
				var title = item.find(".article-title").html();
			} else {
				if (currentProductContainer.find("select").length == 0) {
					var selectLabel = $("<label />").html("Välj artikel");
					var selectList = $("<select />");
					items.each(function() {
						item = $(this);
						var title = item.find(".article-title").html();
						var id = item.attr("class");
						var option = $("<option/>").attr("value", id).html(title);
						selectList.append(option);
					});
					selectList.change(switchArticle);
					currentProductContainer.append(selectLabel.append(selectList));
				}
				item = $(items.get(0));
			}
			
			showDetails(item);
		};
		
		var showDetails = function(item) {
			currentProductContainer.find(".article-details").remove();
			var details = $("<div class='article-details'/>").hide().append(item.find(".article-details").html());
			currentProductContainer.append(details);
			currentProductContainer.find(".article-details").fadeIn(500);
		};
		
		var switchArticle = function() {
			var selectList = $(this);
			var url = selectList.parents(".product-details").find(".go-to-article a").attr("href");
			var items = loadedItems[url];
			if (items) {
				currentProductContainer = $(selectList.parents(".product-details").get(0));
				var itemClass = selectList.val();
				items.each(function() {
					var item = $(this);
					if (item.hasClass(itemClass)) {
						showDetails(item);
						return false;
					}
				});
			}
		};
		
		var loadError = function() {
			alert("Sorry, could not load the product details. Please try again later!");
		};
		
		return {
			initProducts: initProducts
		};
	}();
})();
$(document).ready(Ledlab.initProducts);
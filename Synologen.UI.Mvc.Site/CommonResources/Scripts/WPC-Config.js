/*	Copyright (c) 2008, WPC
	Author: Kristoffer Ahl, Spinit AB	*/

/* Configuration settings for WPC
=================================================================================================*/
/* =WPC */
var WPC = function() {
	/**
	 * @description Contains the base name.
	 * @property base_name
	 * @private
	 * @static
	 * @type string
	*/
	var base_name = "WPC";
	
    return {
		// =init
		init : function() {
			//alert("TODO: Fix bug in Opera that won't fire the validation on submit with requiredfields");
			var validationSettings;
			switch (WPC.Config.CurrentComponent) {
				case "GC":
					validationSettings = WPC.Config.Components.GC.ValidationSettings;
					break;
				default:
					validationSettings = WPC.Config.JQueryPlugins.Validation.DefaultSettings;
					break;
			}
			validationSettings = (validationSettings) ? validationSettings : WPC.Config.JQueryPlugins.Validation.DefaultSettings;

			// Init and localize validation
			$("body").append('<script type="text/javascript" src="'+$.format(WPC.Config.Paths.JQueryPlugins.Validation.Localization, WPC.Config.ApplicationPath, WPC.Config.CurrentCulture)+'"></script>');
			$("form.validate").validate(validationSettings);
		},
		
		createNamespace : function() {
			var args=arguments, args_length, obj=null, i, j, parts, parts_length;
			for (i=0, args_length=args.length; i<args_length; i++) {
				parts=args[i].split(".");
				obj=eval(base_name);
				for (j=(parts[0] == base_name) ? 1 : 0, parts_length=parts.length; j<parts_length; j++) {
					obj[parts[j]]=obj[parts[j]] || {};
					obj=obj[parts[j]];
				}
			}
			return obj;
		}
    };
}();

WPC.createNamespace("Config");
WPC.Config = function() {
	return {
		ApplicationPath : "/GCSite",
		CurrentComponent : "",
		CurrentCulture : "sv-SE",
		JQueryPlugins : {
			Validation : {
				DefaultSettings : {}
			}
		},
		Paths : {
			JQueryPlugins : {
				Validation : {
					Localization : "{0}/CommonResources/Scripts/jQueryPlugins/Validate/Localization/messages_{1}.js"
				}
			}
		},
		SessionId : "$$SessionID"
	};
}();

WPC.createNamespace("Config.Custom");
WPC.Config.Custom = function() {
	return {
		
	};
}();

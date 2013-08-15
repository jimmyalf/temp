function getUrlFriendlyName(inputName) {
	var name = "";
	var i = 0;
	var length = inputName.length;
	while (i < length) {
		var subject = inputName.charAt(i++).toLowerCase();

		if (subject.match(/å|Å|ä|Ä/)) {
			name += "a";
		}
		else if (subject.match(/ü/)) {
			name += "u";
		}
		else if (subject.match(/ß/)) {
			name += "ss";
		}		
		else if (subject.match(/ñ/)) {
			name += "n";
		}			
		else if (subject.match(/é|è/)) {
			name += "e";
		}
		else if (subject.match(/ö|Ö/)) {
			name += "o";
		}
		else if (subject.match(/ /)) {
			name += "-";
		}
		if (!subject.match(/[^-a-zA-Z0-9_]/)) {
			name += subject;
		}
	}
	return name;
}
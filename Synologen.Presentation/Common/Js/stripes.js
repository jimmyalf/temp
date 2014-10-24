function stripeTables() {
  if (document.getElementsByTagName) {
    var tables = document.getElementsByTagName("table");
    for (var i=0; i<tables.length; i++) {
      if (tables[i].className.match("striped"))
      {
        var rows = tables[i].getElementsByTagName("tr");
        for (var j=0; j<rows.length; j=j+2) {
		  if (!rows[j].className.match("header")) {
            addClass(rows[j],"odd");
          }
        }
      }
    }
  }
}

function stripeDefinitionLists() {
  if (document.getElementsByTagName) {
    var deflist = document.getElementsByTagName("dl");
    for (var i=0; i<deflist.length; i++) {
      if (deflist[i].className.match("striped"))
      {
        var dt = deflist[i].getElementsByTagName("dt");
        for (var j=0; j<dt.length; j=j+2) {
            addClass(dt[j],"odd");
        }
        var dd = deflist[i].getElementsByTagName("dd");
        for (var j=0; j<dd.length; j=j+2) {
            addClass(dd[j],"odd");
        }
      }
    }
  }
}

addEvent(window, 'load', stripeTables);
addEvent(window, 'load', stripeDefinitionLists);
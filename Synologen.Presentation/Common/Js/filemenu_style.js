/* --- geometry and timing of the menu --- */
var MENU_POS1 = new Array();
	// item sizes for different levels of menu
	MENU_POS1['height'] = [21, 18];
	MENU_POS1['width'] = [78, 78];
	// menu block offset from the origin:
	//	for root level origin is upper left corner of the page
	//	for other levels origin is upper left corner of parent item
	MENU_POS1['block_top'] = [0, 20];
	MENU_POS1['block_left'] = [0, 0];
	// offsets between items of the same level
	MENU_POS1['top'] = [0, 17];
	MENU_POS1['left'] = [76, 0];
	// time in milliseconds before menu is hidden after cursor has gone out
	// of any items
	MENU_POS1['hide_delay'] = [200, 200];
	
/* --- dynamic menu styles ---
note: you can add as many style properties as you wish but be not all browsers
are able to render them correctly. The only relatively safe properties are
'color' and 'background'.
*/
var MENU_STYLES1 = new Array();
	// default item state when it is visible but doesn't have mouse over
	MENU_STYLES1['onmouseout'] = [
		'color', ['#000000', '#000000'], 
		'background', ['C6D5D6', 'C6D5D6'],
		'fontWeight', ['normal', 'normal'],
		'textDecoration', ['none', 'none'],
	];
	// state when item has mouse over it
	MENU_STYLES1['onmouseover'] = [
		'color', ['#000000', '#000000'], 
		'background', ['E7E8E9', 'E7E8E9'],
		'fontWeight', ['normal', 'normal'],
		'textDecoration', ['none', 'none'],
	];
	// state when mouse button has been pressed on the item
	MENU_STYLES1['onmousedown'] = [
		'color', ['000000', '000000'], 
		'background', ['F7F7F7', 'F7F7F7'],
		'fontWeight', ['normal', 'normal'],
		'textDecoration', ['none', 'none'],
	];
	

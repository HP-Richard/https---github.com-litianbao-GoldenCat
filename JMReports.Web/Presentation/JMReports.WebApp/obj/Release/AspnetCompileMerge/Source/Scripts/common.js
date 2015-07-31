 $(function() {

 
        $(".group1").show();
		$(".group2").show();
		$(".group3").show();
		$("#customersd").hide();
		$("#customersts").hide();
		$('[id="viewDetail"]').click(function() {
		
		var $this = $(this);
		$("#customers").show();
		$this.parents(".opreatArea").hide();

	});
	
	$('[id="selectAgain"]').click(function() {
		
		var $this = $(this);
		$(".opreatArea").show();
		$this.parents("#customersd").hide();
 
	});
	
    $('[id="hitButton1"]').click(function() {
		
		var $this = $(this).find("em");
		if ($this.hasClass("iconDown")) {
			$this.removeClass("iconDown");
			$this.addClass("iconUp");
			$(".group1").hide();
		} else {
			$this.removeClass("iconUp");
			$this.addClass("iconDown");
			$(".group1").show();
		}
		 
	});
	 $('[id="hitButton2"]').click(function() {
		
		var $this = $(this).find("em");
		if ($this.hasClass("iconDown")) {
			$this.removeClass("iconDown");
			$this.addClass("iconUp");
		    $(".group2").hide();
		} else {
			$this.removeClass("iconUp");
			$this.addClass("iconDown");
			$(".group2").show();
		}
		 
	});
	 $('[id="hitButton3"]').click(function() {
		
		var $this = $(this).find("em");
		if ($this.hasClass("iconDown")) {
			$this.removeClass("iconDown");
			$this.addClass("iconUp");
		    $(".group3").hide();
		} else {
			$this.removeClass("iconUp");
			$this.addClass("iconDown");
			$(".group3").show();
		}
		 
	});
	
	$('[id="option1"]').click(function() {
		$("#customersts").show();
		$(".opreatArea").hide();
	});
	
	$('[id="viewDetail"]').click(function() {
		$("#customersd").show();
		$(".opreatArea").hide();
	});	
		 
})


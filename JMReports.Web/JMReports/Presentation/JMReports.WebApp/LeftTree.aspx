<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftTree.aspx.cs" Inherits="JMReports.WebApp.LeftTree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/table.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/jquery-1.10.2.js" ></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var data = {};
            $.ajax({
                type: "POST",
                url: "MenuByRole.ashx",
                data: data,
                success: function (response) {

                    var divStr = "";
                    var dataobj = eval("(" + response + ")"); //转换为json对象  
                                           
                    var categories = dataobj.ReportByCategory;
                    for (var i = 0; i < categories.length; i++) {
                        var k = i + 1;
                        divStr += "<div class='words2'><div id='hitButton" + k.toString() + "'>" + categories[i].Category + "<em class='iconDown'> </em></div></div>";
                        divStr += "<div class=\"group" + k.toString() + "\">";
                        //$(".left").append( "<div class='words2'><div id='hitButton" + k.toString() + "'>" + categories[i].Category + "<em class='iconDown'> </em></div></div>");
                        //$(".left").append("<div class=\"group" + k.toString() + "\">");
                        var reports = categories[i].Reports;
                        for (var j = 0; j < reports.length; j++) {
                            divStr += "<div class='line2'><div><a href='" + reports[j].URL + "' target='right' onclick='ReportMap(" + reports[j].ReportId + ")'>" + reports[j].ChineseName + "</a><em class='iconRight'></em></div></div>";
                            //$(".left").append( "<div class='line2'><div><a href='" + reports[j].URL + "' target='right' onclick='ReportMap(" + reports[j].ReportId + ")'>" + reports[j].ChineseName + "</a><em class='iconRight'></em></div></div>");
                        }
                        divStr += "</div>";
                        //$(".left").append("</div>");
                    }

                    $(".left").append(divStr);

                    //Robin
                    $(".group1").show();
                    $(".group2").show();
                    $(".group3").show();
                    $("#customersd").hide();
                    $("#customersts").hide();
                    $('[id="viewDetail"]').click(function () {

                        var $this = $(this);
                        $("#customers").show();
                        $this.parents(".opreatArea").hide();

                    });

                    $('[id="selectAgain"]').click(function () {

                        var $this = $(this);
                        $(".opreatArea").show();
                        $this.parents("#customersd").hide();

                    });

                    $('[id="hitButton1"]').click(function () {

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
                    $('[id="hitButton2"]').click(function () {

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
                    $('[id="hitButton3"]').click(function () {

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

                    $('[id="option1"]').click(function () {
                        $("#customersts").show();
                        $(".opreatArea").hide();
                    });

                    $('[id="viewDetail"]').click(function () {
                        $("#customersd").show();
                        $(".opreatArea").hide();
                    });
                    //
                }
            });


            //---------------------------

                $(".group1").hide();
                $(".group2").show();
                $(".group3").show();
                $("#customersd").hide();
                $("#customersts").hide();
                $('[id="viewDetail"]').click(function () {

                    var $this = $(this);
                    $("#customers").show();
                    $this.parents(".opreatArea").hide();

                });

                $('[id="selectAgain"]').click(function () {

                    var $this = $(this);
                    $(".opreatArea").show();
                    $this.parents("#customersd").hide();

                });

                $('[id="hitButton1"]').click(function () {

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
                $('[id="hitButton2"]').click(function () {

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
                $('[id="hitButton3"]').click(function () {

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

                $('[id="option1"]').click(function () {
                    $("#customersts").show();
                    $(".opreatArea").hide();
                });

                $('[id="viewDetail"]').click(function () {
                    $("#customersd").show();
                    $(".opreatArea").hide();
                });

            


            //---------------------------
        });
    </script>

    <script type="text/javascript">
        function ReportMap(type1) {
            //var map1 = "DailyReport";
            parent.document.frames[0].location = "top.aspx?v1=" + type1;
        }
    </script>
</head>
<body style="background-color:steelblue">
    <form id="form1" runat="server">
    <div>
         <div class="left"></div>
    </div>

    </form>
</body>
</html>

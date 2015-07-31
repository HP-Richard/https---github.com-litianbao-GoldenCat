if (typeof (NCIC) == "undefined") { NCIC = { __namespace: true }; }

NCIC.LockForm = {
    Init: function () {
        try {
            var div = parent.document.createElement("div");
            div.id = "myDiv11";
            div.style.styleFloat = "left";
            div.style.overflow = "hidden";
            div.style.marginLeft = "0px";
            div.style.marginTop = "0px";
            div.style.left = "0px";
            div.style.top = "0px";
            div.style.width = "100%";
            div.style.height = "100%";
            div.style.backgroundColor = "#000000";
            div.style.position = "absolute";
            div.style.zIndex = "1000";
            div.style.filter = "alpha(opacity = 20)";
            div.style.opacity = "0.5";
            div.style.display = "none";

            var div1 = parent.document.createElement("div");
            div1.id = "myDiv21";
            div1.style.styleFloat = "left";
            div1.style.overflow = "hidden";
            div1.style.marginLeft = "-180px";
            div1.style.marginTop = "-80px";
            div1.style.left = "50%";
            div1.style.top = "50%";
            div1.style.width = "300px";
            div1.style.height = "150px";
            div1.style.backgroundColor = "#f6f8fa";
            div1.style.position = "absolute";
            div1.style.borderWidth = "2px";
            div1.style.borderColor = "#a5acb5";
            div1.style.borderStyle = "solid";
            div1.style.zIndex = "2000";
            div1.style.display = "none";

            //var serverUrl = Xrm.Page.context.getClientUrl();
            //if (serverUrl.match(/\/$/)) {
            //    serverUrl = serverUrl.substring(0, serverImageUrl.length - 1);
            //}
            //var imageurl = serverUrl + "/_imgs/btn_lookup_resolving.gif";
            //var imageUrl2 = serverUrl + "/WebResources/nodus_/Images/resolving.gif?preview=1";


            var imageurl = "/_imgs/btn_lookup_resolving.gif";
            var imageUrl2 = "/WebResources/nodus_/Images/resolving.gif?preview=1";

            var myhtml = "<table id='myTable' style='width:100%;height:100%;text-align:center'>"
                + "<tr>"
                + "<td><div><img id='resolvingicon' alt=\"\" src='" + imageurl + "'/></div><div id='msg_div'></div></td>"
                + "</tr>"
                + "<tr>"
                + "<td></td>"
                + "</tr>"
                + "</table>";
            div1.innerHTML = myhtml;
            parent.document.body.appendChild(div1);
            parent.document.body.appendChild(div);
        } catch (e) {
            // do nothing
        }
    },
    Show: function (msg) {
        $(window.parent.document).find("#msg_div").html(msg);
        parent.document.getElementById("myDiv11").style.display = "block";
        parent.document.getElementById("myDiv21").style.display = "block";
    },
    ShowNoImg: function (msg) {
        $(window.parent.document).find("#msg_div").html(msg);
        parent.document.getElementById("myDiv11").style.display = "block";
        parent.document.getElementById("myDiv21").style.display = "block";
        parent.document.getElementById("resolvingicon").style.display = "none";
    },
    Hide: function () {
        parent.document.getElementById("myDiv11").style.display = "none";
        parent.document.getElementById("myDiv21").style.display = "none";
    }
}








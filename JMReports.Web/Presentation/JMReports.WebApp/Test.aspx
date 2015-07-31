<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="JMReports.WebApp.Test"  %>

<%@ Register Src="~/HotelByUser.ascx" TagPrefix="uc1" TagName="HotelByUser" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            
            $("#Button3").click(function (evt) {
                var data = {};
                
                //$.getJSON("HotelByUser.ashx", data, function (data) {
                //    $("#container").empty();
                //    for (var i = 0; i < data.length; i++) {
                //        $("#container").append("<tr><td>" + data[i].Name + "</td><td>" + data[i].Age + "</td><td>" + data[i].Age + "</td></tr>");
                //    }
                //    alert("Invoke HotelByUser handler");
                   
                //}

                //$.ajax({
                //    url: "HotelByUser.ashx",
                //    type: "POST",
                //    data: data,
                //    contentType:"application/x-www-form-urlencoded;charset=UTF-8",
                //    dataType: "json",
                //    success: function (result,status,jqXHR) {
                //        $("#container").empty();
                //        for (var i = 0; i < result.length; i++) {
                //            $("#container").append("<tr><td>" + 
                //                result[i].Name + "</td><td>" +
                //                result[i].Age + "</td><td>" +
                //                result[i].Age + "</td></tr>");
                //        }
                //        alert("Employee data retrieved successfully!");
                //    },
                //    error: function (jqXHR,status,err) {
                //        alert("ERROR : " + status);
                //    }
                //}


                $.ajax({  
                    type: "POST",  
                    url: "HotelByUser.ashx",  
                    data: data,  
                    success: function (response) {  
                      
                        alert(response);  
                        var dataobj = eval("(" + response + ")"); //转换为json对象  
                        alert(dataobj);
                        alert("Name:" + dataobj.Name);
                        alert("Time:" + dataobj.Time);

                        $("#DropDownList1 option").remove();
                        $("#DropDownList1").append("<option value='11'>" + dataobj.Name + "</option>");

                    
                    }  
                } 


          

                );
                //evt.preventDefault();
            });
            $("#Button3").click();

            var ddlIndex = $("#<%= hidCurrentDDL.ClientID %>").val();

            $("#DropDownList1").select(function (event, ui) { alert("DropDownList select event fired");})

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidCurrentDDL" runat="server" />
        <div>
            <uc1:HotelByUser runat="server" id="selDept" />
            <asp:Button runat="server" Text="Button" ID="Button1" OnClick="Button1_Click"></asp:Button>
            <input id="Button3" type="button" value="button" />
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Value="" Text="--- 请选择 ---"></asp:ListItem> 
            </asp:DropDownList>
            

        </div> 
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Initial" /><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </ContentTemplate>
           </asp:UpdatePanel>
        
        <table border="1" cellpadding="6" id="container"></table>
    </form>
</body>
</html>

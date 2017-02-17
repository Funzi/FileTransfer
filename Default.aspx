<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Theme.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">

        var intervalID = 0;
        var subintervalID = 0;
        var fileUpload;
        var form;

        function pageLoad() {
            $addHandler($get('upload'), 'click', onUploadClick);
        }
       
        function register(form, fileUpload) {
            this.form = form;
            this.fileUpload = fileUpload;
        }
        //Start upload process
        function onUploadClick() {
            if (fileUpload.value.length > 0) {
                form.submit();
                setProgress(0);
                startProgress();

                intervalID = window.setInterval(function () {
                    PageMethods.GetUploadStatus(function (result) {
                        if (result) {
                            setProgress(result.percentComplete);
                            if (result == 100) {
                                window.clearInterval(intervalID);
                                clearTimeout(subintervalID);
                            }
                        }
                    });
                }, 500);
            }
            else
                onComplete();
        }

        function onComplete() {
            window.clearInterval(intervalID);
            clearTimeout(subintervalID);
            setProgress(0);
        }

        function setProgress(completed) {
            $get('dvProgressPrcent').innerHTML = completed + '%';
            $get('dvProgress').style.width = completed + '%';
        }

        function startProgress() {
            var increase = $get('dvProgressPrcent').innerHTML.replace('%', '');
            increase = Number(increase) + 1;
            if (increase <= 100) {
                setProgress(increase);
                subintervalID = setTimeout("startProgress()", 200);
            }
            else {
                window.clearInterval(subintervalID);
                clearTimeout(subintervalID);
            }
        }

    </script>

    <style type="text/css">
        .auto-style1 {
            margin-left: 66px;
        }
    </style>

</head>
<body style="height: 400px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptManager" runat="server" EnablePageMethods="true" />

        <div>
            Please select a file to upload
        </div>
        <iframe id="uploadFrame" frameborder="0" height="40" width="200" scrolling="no" src="UploadControl.aspx"></iframe>
        <br />

        <div id="dvUploader"></div>

        <input id="upload" type="button" value="Copy to" />
        <asp:TextBox ID="dvDestination" runat="server" Height="20px" Text="" CssClass="auto-style1" Width="200px" OnTextChanged="dvDestination_TextChanged" AutoPostBack="true"></asp:TextBox>
        <br />
        <br />
        <td align="left">
            <div id="dvProgressContainer">
                <div id="dvProgress">
                </div>
            </div>
        </td>

        <td>
            <div id="dvProgressPrcent">
                0%
            </div>
        </td>
    </form>
</body>
</html>

        
        
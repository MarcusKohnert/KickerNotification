<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Web._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.6.4.js"></script>
    <script type="text/javascript" src="Scripts/jquery.signalR-0.5.3.js"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
    <script src="Scripts/kickerNotify.js" type="text/javascript"></script>
</head>
<body>
    <div>
        <div>
            <h1>
                --- KickerNotify ---
            </h1>
        </div>
        <h2>
            <label>Number of players:</label>
            <div id="   " />
        </h2>       
    </div>
    <div>
        <label>Status: </label>
        <div id="statusBar" />
    </div>
</body>
</html>

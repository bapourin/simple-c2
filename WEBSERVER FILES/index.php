<!DOCTYPE html>
<html>
<head>
    <title>Send Commands</title>
</head>
<body>
    <h1>Enter Command</h1>
    <form id="commandForm">
        <input type="text" name="command" id="commandInput" placeholder="Enter your command here">
        <button type="submit">Send Command</button>
    </form>
    

    <script>
        document.getElementById('commandForm').addEventListener('submit', function (event) {
            event.preventDefault();
            const commandInput = document.getElementById('commandInput').value;
            sendCommandToServer(commandInput);
        });

        function sendCommandToServer(command) {
            const xhr = new XMLHttpRequest();
            xhr.open('POST', 'send_command.php', true);
            xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            xhr.onreadystatechange = function () {
                if (xhr.readyState === XMLHttpRequest.DONE) {
                    if (xhr.status === 200) {
                        console.log('Command sent successfully.');
                    } else {
                        console.error('Failed to send the command.');
                    }
                }
            };
            xhr.send('command=' + encodeURIComponent(command));
        }
    </script>
</body>
</html>
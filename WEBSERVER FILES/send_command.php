<?php
error_reporting(E_ALL);
ini_set('display_errors', 1);

$commandFile = 'commands.txt';

// Get the command from the POST data
if (isset($_POST['command'])) {
    $command = $_POST['command'];

    // Write the command to the commands.txt file, overwriting any existing content
    file_put_contents($commandFile, $command);
}
?>

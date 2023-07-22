<?php
// Read the command and repetition count from the text file
$commandFile = 'commands.txt';
if (file_exists($commandFile)) {
    $commandData = file_get_contents($commandFile);
    if (preg_match('/^(\d+)\s+command:(.*)$/s', $commandData, $matches)) {
        $repetitionCount = (int) $matches[1];
        $command = trim($matches[2]);

        // Clear the command file after reading the command
        file_put_contents($commandFile, '');
    } else {
        // Invalid command format, reset to default values
        $repetitionCount = 1;
        $command = '';
    }
} else {
    // No command available, reset to default values
    $repetitionCount = 1;
    $command = '';
}

// Display the command and repetition count (optional)
echo "Command: $command, Repetition Count: $repetitionCount";

// Pass the command and repetition count to the C# application as JSON data
$responseData = array('command' => $command, 'repetition' => $repetitionCount);
echo json_encode($responseData);
?>

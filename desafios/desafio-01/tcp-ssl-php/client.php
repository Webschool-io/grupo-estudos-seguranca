<?php

$ip = "127.0.0.1";
$port = "9090";
$command = "Ta ai?";

// Conectar ao servidor
$socket = stream_socket_client("tcp://{$ip}:{$port}", $errno, $errstr, 30);

if ($socket) {

    // Iniciar SSL
    stream_set_blocking($socket, true);
    stream_socket_enable_crypto($socket, true, STREAM_CRYPTO_METHOD_SSLv3_CLIENT);
    stream_set_blocking($socket, false);

    // Enviar comando
    fwrite($socket, $command);

    $buf = null;
    // Receber resposta do servidor
    while (!feof($socket)) {
        $buf .= fread($socket, 8192);
    }

    // Fechar conexão
    fclose($socket);

    // Retorno do servidor
    echo $buf;

}

<?php

require_once 'conf.php';

echo "Aguardando ({$ip}:{$port}) por conexões\n";

// contaxto para configuração do SSL
$context = stream_context_create();

// Opções de SSL
stream_context_set_option($context, 'ssl', 'local_ccert', $pem_file);
stream_context_set_option($context, 'ssl', 'passphrase', $pem_passphrase);
stream_context_set_option($context, 'ssl', 'allow_self_signed', true);
stream_context_set_option($context, 'ssl', 'verify_peer', false);

// Criando socket stream
$socket = stream_socket_server("tcp://{$ip}:{$port}", $errno, $errstr, STREAM_SERVER_BIND | STREAM_SERVER_LISTEN, $context);
stream_socket_enable_crypto($socket, false);

// Entrando em loop para manter o ouvinte ativo
$exit = false;
$i = 1;
while ($exit == false) {

    // Aceitar qualquer conexão
    $acceptedSocket = stream_socket_accept($socket, "-1", $remoteIp);

    echo "Nova requisição de $remoteIp\n";

    // Iniciando SSL na conexão
    stream_set_blocking($acceptedSocket, true);
    stream_socket_enable_crypto($acceptedSocket, true, STREAM_CRYPTO_METHOD_SSLv3_SERVER);

    // Ler o comando do cliente. Isso irá ler 8192 bytes.
    $command = fread($acceptedSocket, 8192);

    // desbloqueando conexão
    stream_set_blocking($acceptedSocket, false);

    // Interpretando comando e enviando repsosta
    switch ($command) {
        case "sair": {
            $exit = true;
            echo "Finalizando servidor \n";
            break;
        }

        case "Ta ai?": {
            fwrite($acceptedSocket, "Olá {$remoteIp}. Esta é a {$i}ª que me perguntam se estou aqui!\n");
            $i++;

            echo "Respondi ao cliente! \n";
            break;
        }

    }

    // Fechando conexão com o cliente
    fclose($acceptedSocket);
}
exit(0);

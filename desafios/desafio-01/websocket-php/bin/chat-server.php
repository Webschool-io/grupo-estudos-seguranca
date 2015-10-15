<?php

use Ratchet\WebSocket\WsServer;
use Ratchet\Server\IoServer;
use Ratchet\Http\HttpServer;
use App\Chat;

require dirname(__DIR__) . '/vendor/autoload.php';

$server = IoServer::factory(
    new HttpServer(
        new WsServer(
            new Chat()
        )
    ),
    8080
);

$server->run();

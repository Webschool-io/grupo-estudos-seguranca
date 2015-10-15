# Passos para rodar

## Setup

1. Instale o composer https://getcomposer.org/download/
2. Baixe as dependencias do projeto `composer install`

## Execução
1. Entre dentro da pasta `desafios/desafio-01/websocket-php`
2. Inicie o  servidor `php bin/chat-server.php`
3. Abra um navegador (Chrome, FireFox, or Safari)
4. Abra o console javascript e cole o seguinte script:
    ```
    var conn = new WebSocket('ws://localhost:8080');
    conn.onopen = function(e) {
        console.log("Connection established!");
    };

    conn.onmessage = function(e) {
        console.log(e.data);
    };

    ```
5. Após ver a mensagem `Connection established` você pode enviar mensagens ao servidor para outros browsers conectados:

    ```
    conn.send('Hello World!');
    ```

## Referencias
1. http://socketo.me/docs/hello-world
2. http://www.html5rocks.com/pt/tutorials/websockets/basics/

#Server TCP Simples com SSL em PHP

Antes de testar não esqueça de configurar o SSL no arquivo `config.php` e executar o script do arquivo `create_cacert.php`.
```
$ php create_cacert.php
```

Direcione o arquivo de nome configurado na variável `$pem_file` do arquivo `config.php` para a variavel `openssl.cafile` no PHP.ini

Iniciar o servidor:
```
$ php server.php
```

Fazer uma requisição:
```
$ php client.php
```

Artigos que li para essa criação:
[Configurando SSL no servidor de desenvolvimento (Apache)](http://www.phpit.com.br/artigos/configurando-ssl-servidor-de-desenvolvimento-apache.phpit)
[PHP.net - Sockets](http://php.net/manual/pt_BR/sockets.examples.php)
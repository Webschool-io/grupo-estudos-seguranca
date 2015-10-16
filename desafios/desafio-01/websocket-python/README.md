# Passos para rodar

Para rodar você só precisa do Python, se seu sistema operacional for Linux ou Mac OS eles já acompanham um interpretador Python pré-instalado, então basta seguir os procedimentos abaixo:

1. Vá terminal para o diretório websocket-python e inicie o servidor com esse comando `python server.py`
2. Em outro terminal inicie o client `python client.py`
3. Agora é só escrever alguma coisa no cliente e já vai passar para o servidor as informações. inclusive o IP do client, este servidor tem uma parte extra para gerenciar concorrência usando thread (Um bônus bem legal, hehehe...).
    
## Referencias
1. http://wiki.python.org.br/SocketBasico

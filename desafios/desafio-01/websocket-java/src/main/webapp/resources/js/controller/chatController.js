angular.module("chat").controller("chatController", function ($scope, $timeout) {

	$scope.logs = [];
	$scope.usuario = prompt('Qual seu nome?');

	while($scope.usuario === "") {
		$scope.usuario = prompt('Por favor, insira seu nome.');
	}

	var webSocket = new WebSocket("ws://localhost:8080/websocket-java/chat?username="+$scope.usuario);
	$scope.mensagem = "";

	webSocket.onopen = function() {
	};

	webSocket.onmessage = function(message) {
		$timeout(function () {
			$scope.logs.push(message.data);
			console.log(message.data)	
		});
	};

	$scope.enviar = function () {
		webSocket.send($scope.mensagem);
		$scope.mensagem = "";
	};
});


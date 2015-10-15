package br.com.fauker.websocket;

import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServletRequest;
import org.apache.catalina.websocket.StreamInbound;
import org.apache.catalina.websocket.WebSocketServlet;

@WebServlet("/chat")
public class Server extends WebSocketServlet {

	private static final long serialVersionUID = 7772133888599281910L;

	@Override
	protected StreamInbound createWebSocketInbound(String protocol, HttpServletRequest request) {
		return new MyMessageInbound();
	}

}
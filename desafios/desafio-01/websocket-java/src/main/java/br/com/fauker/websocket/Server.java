package br.com.fauker.websocket;

import java.nio.CharBuffer;
import java.util.ArrayList;
import java.util.List;

import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServletRequest;

import org.apache.catalina.websocket.StreamInbound;
import org.apache.catalina.websocket.WebSocketServlet;

@WebServlet("/chat")
public class Server extends WebSocketServlet {

	private static final long serialVersionUID = 7772133888599281910L;
	
	private static final List<MyMessageInbound> connections = new ArrayList<MyMessageInbound>();

	@Override
	protected StreamInbound createWebSocketInbound(String protocolo, HttpServletRequest request) {
		String username = request.getParameter("username");
		return new MyMessageInbound(username);
	}
	
	public static final void broadcast(String message) {
		for (MyMessageInbound con : Server.getConnections()) {
			try {
				con.getWsOutbound().writeTextMessage(CharBuffer.wrap(message));
				con.getWsOutbound().flush();
				System.out.println("Mensagem enviada ao servidor -> " + message);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	}
	
	public static List<MyMessageInbound> getConnections() {
		return connections;
	}

}
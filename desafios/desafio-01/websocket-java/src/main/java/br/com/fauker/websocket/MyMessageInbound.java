package br.com.fauker.websocket;

import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.CharBuffer;

import org.apache.catalina.websocket.MessageInbound;
import org.apache.catalina.websocket.WsOutbound;

public class MyMessageInbound extends MessageInbound {

	private String username;
	
	public MyMessageInbound(String username) {
		this.username = username;
	}

	@Override
	protected void onTextMessage(CharBuffer cb) throws IOException {
		String message = String.format("\"%s\" : %s", this.username, cb.toString());
		Server.broadcast(message);
	}

	@Override
	protected void onOpen(WsOutbound outbound) {
		Server.getConnections().add(this);
		String message = String.format("\"%s\" se conectou.", this.username);
		Server.broadcast(message);
	}

	@Override
	protected void onClose(int status) {
		Server.getConnections().remove(this);
		String message = String.format("\"%s\" saiu do chat.", this.username);
		Server.broadcast(message);
	}

	@Override
	protected void onBinaryMessage(ByteBuffer arg0) throws IOException {
	}
}
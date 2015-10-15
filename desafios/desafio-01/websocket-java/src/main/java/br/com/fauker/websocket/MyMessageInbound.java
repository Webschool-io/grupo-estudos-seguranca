package br.com.fauker.websocket;

import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.CharBuffer;
import java.util.ArrayList;
import java.util.List;

import org.apache.catalina.websocket.MessageInbound;
import org.apache.catalina.websocket.WsOutbound;

public class MyMessageInbound extends MessageInbound {

	private WsOutbound myOutBound;
	private static List<MyMessageInbound> mmiList = new ArrayList<MyMessageInbound>();

	@Override
	protected void onTextMessage(CharBuffer cb) throws IOException {
		System.out.println("Mensagem aceita: " + cb);
		for (MyMessageInbound m : mmiList) {
			CharBuffer buffer = CharBuffer.wrap(cb);
			m.myOutBound.writeTextMessage(buffer);
			m.myOutBound.flush();
		}
	}

	@Override
	protected void onOpen(WsOutbound outbound) {
		try {
			System.out.println("Um usuario se conectou no chat.");
			this.myOutBound = outbound;
			mmiList.add(this);
			outbound.writeTextMessage(CharBuffer.wrap("Ola!!!"));
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	protected void onClose(int status) {
		System.out.println("Um usuario saiu do chat.");
		mmiList.remove(this);
	}

	@Override
	protected void onBinaryMessage(ByteBuffer arg0) throws IOException {
	}
}
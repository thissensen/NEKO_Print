using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sunny.UI;

namespace 翻译姬;
public class TCP工具 {

    public 连接类型 监听类型;
    public int 端口;
    public Socket Socket;
    /// <summary>
    /// 若过小，则会导致数据接收分多批次
    /// </summary>
    public int 单次数据接收上限 = 1024 * 1024;
    public int 连接数;
    public int 超时时间;

    public delegate void 连接成功事件(Socket socket);
    public delegate void 连接断开事件(Socket socket);
    public delegate void 数据接收事件(Socket socket, string text);

    public event 连接成功事件 连接成功;
    public event 连接断开事件 连接断开;
    public event 数据接收事件 数据接收;

    public TCP工具(int 端口, 连接类型 监听类型 = 连接类型.IPV4) {
        this.监听类型 = 监听类型;
        this.端口 = 端口;
    }

    public TCP工具 Clone() => new TCP工具(端口, 监听类型);

    public Socket 开始监听() {
        if (监听类型 == 连接类型.IPV4) {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket.Bind(new IPEndPoint(IPAddress.Any, 端口));
        } else {
            Socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            Socket.Bind(new IPEndPoint(IPAddress.IPv6Any, 端口));
        }
        Socket.ReceiveTimeout = 超时时间 * 1000;
        Socket.SendTimeout = 超时时间 * 1000;
        Socket.Listen(0);
        Task.Run(() => {
            while (true) {
                Socket sc = Socket.Accept();
                连接数++;
                连接成功?.Invoke(sc);
                监听数据(sc);
            }
        });
        return Socket;
    }

    public Socket 开始连接(string IP) {
        if (IP.IsNullOrEmpty()) {
            throw new Exception("IP未填，无法连接");
        }
        if (监听类型 == 连接类型.IPV4) {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        } else {
            Socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
        }
        Socket.ReceiveTimeout = 超时时间 * 1000;
        Socket.SendTimeout = 超时时间 * 1000;
        try {
            Socket.Connect(IPAddress.Parse(IP), 端口);
            连接数++;
            连接成功?.Invoke(Socket);
            监听数据(Socket);
        } catch (Exception ex) {
            连接断开?.Invoke(Socket);
        }
        return Socket;
    }

    private void 监听数据(Socket socket) {
        Task.Run(() => {
            try {
                byte[] buf = new byte[单次数据接收上限];
                while (true) {
                    int len = socket.Receive(buf);
                    string text = Encoding.UTF8.GetString(buf, 0, len);
                    数据接收?.Invoke(socket, text);
                }
            } catch {
                连接断开?.Invoke(socket);
                连接数--;
            }
        });
    }

    public Socket 发送数据(string text) {
        byte[] buf = Encoding.UTF8.GetBytes(text);
        Socket.Send(buf);
        return Socket;
    }

    public void 关闭连接() {
        Socket.Close();
    }

}
public enum 连接类型 {
    IPV4,
    IPV6
}
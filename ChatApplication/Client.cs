using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApplication
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        Socket socketSend;
        //客户端连接服务器端
        private void button1_Click(object sender, EventArgs e)
        {
            //创建一个socket用于通信
            socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(textIP.Text);
            string port = textPort.Text;
            IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(port));
            socketSend.Connect(point);
            ShowMsg("连接成功！");

            Thread th = new Thread(Recieve);
            th.IsBackground = true;
            th.Start();

        }

        //接收服务器端消息
        void Recieve()
        {
            while (true)
            {
                try
                {

                    byte[] buffer = new byte[2 * 1024 * 1024];
                    int r = socketSend.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    if (buffer[0] == 0)
                    {
                        ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + Encoding.UTF8.GetString(buffer,1,r-1));
                    }
                    else if (buffer[0] == 1)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.InitialDirectory = @"E:\Desktop";
                        sfd.Title = "请保存文件";
                        sfd.Filter = "所有文件|*.*";
                        sfd.ShowDialog(this);
                        string filePath = sfd.FileName;
                        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            fs.Write(buffer, 1, r - 1);
                            ShowMsg("保存文件成功！");
                        }
                    }
                    else if (buffer[0] == 2)
                    {
                        ZD();
                    }
                }
                catch
                {

                }
            }
        }

        void ZD()
        {
            for (int i = 0; i < 500; i++)
            {
                this.Location = new Point(this.Location.X + 1, this.Location.Y + 1);
                this.Location = new Point(this.Location.X - 1, this.Location.Y - 1);
            }
        }

        void ShowMsg(string str)
        {
            textLog.AppendText(str + "\r\n");
        }

        private void Client_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        //发送消息
        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(textSend.Text.Trim());
            socketSend.Send(buffer);
        }
    }
}

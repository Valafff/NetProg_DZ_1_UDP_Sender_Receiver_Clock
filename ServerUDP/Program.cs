using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerUDP
{
	internal class Program
	{
		const string ip = "127.0.0.1";
		const int ReceiverPort = 12321;
		const int bufferSize = 256;
		const int delay = 5000;

		//Отправитель данных со временем
		static async Task Main(string[] args)
		{
			//string time = DateTime.Now.ToString();
			//Console.WriteLine(time);

			////Создание сокета
			//Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			////Связывание сокета и конечной точки
			//socket.Bind(endPont);
			////Создание буффера для отправки данных

			//Установление конечной точки
			IPEndPoint ReceivingEndPont = new IPEndPoint(IPAddress.Parse(ip), ReceiverPort);
			UdpClient udpTimeSender = new UdpClient();
			udpTimeSender.Connect(ReceivingEndPont);
			byte[] buffer = new byte[bufferSize];

			Console.WriteLine("Нажмите любую кнопку для начала отправки данных");
			Console.ReadKey();
			
			try
			{
				while (true)
				{
					//Отправка данных о времени
					string timeNow = DateTime.Now.ToString();
					buffer = Encoding.UTF8.GetBytes(timeNow);
					await udpTimeSender.SendAsync(buffer, buffer.Length);

					//Получение данных от получателей времени
					UdpReceiveResult receivedResult = await udpTimeSender.ReceiveAsync();
					string receivedMessage = Encoding.UTF8.GetString(receivedResult.Buffer);
					Console.WriteLine($"Сообщение от получателя: {receivedMessage}");

					//Задержка
					Task.Delay(delay).Wait();
				}
			}
			catch 
			{
                await Console.Out.WriteLineAsync("Соединение не установлено");
            }
			finally
			{
				udpTimeSender.Close();
			}
		}
	}
}

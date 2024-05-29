using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientUDP
{
	internal class Program
	{
		const int ReceiverPort = 12321;

		//Получатель данных о времени
		static async Task Main(string[] args)
		{
			Console.WriteLine("Ожидаю данных о аремени...");
			// Создаем UDP сервер
			using (var udpTimeSender = new UdpClient(ReceiverPort))
			{

				while (true)
				{
					// Ожидаем получение данных от отправителя времени
					UdpReceiveResult receivedResult = await udpTimeSender.ReceiveAsync();
					string receivedMessage = Encoding.UTF8.GetString(receivedResult.Buffer);
					Console.WriteLine($"Время от отправителя: {receivedMessage}");

					// Отправляем ответ клиенту
					byte[] response = Encoding.UTF8.GetBytes("Время обновлено!");
					await udpTimeSender.SendAsync(response, response.Length, receivedResult.RemoteEndPoint);
				}
			}
		}
	}
}

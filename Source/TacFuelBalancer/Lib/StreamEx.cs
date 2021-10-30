/*
	This file is part of Thunder Aerospace Corporation's (TAC) Fuel Balancer /L Unleashed
		© 2019-2021 Lisias T : http://lisias.net <support@lisias.net>
		© 2019 linuxgurugamer
		© 2016-2018 thwebbooth
		© 2013-2015 Taranis Elsu

	TAC Fuel Balancer /L Unleashed is licensed as follows:

		* CC BY-NC-SA 3.0 : http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode

	TAC Fuel Balancer /L Unleashed is distributed in the hope that
	it will be useful, but WITHOUT ANY WARRANTY; without even the implied
	warranty of	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

*/
using System;
using System.IO;



namespace Tac
{
	/// <summary>
	/// Contains extension methods on the Stream class.
	/// </summary>
	internal static class StreamEx {
		/// <summary>
		/// Reads the Stream to the end and returns a byte array containing the contents of the Stream.
		/// </summary>
		/// <param name="stream">The stream to be read.</param>
		/// <returns>A byte array containing the contents of the Stream.</returns>
		public static byte[] ReadToEnd (this Stream stream) {
			long originalPosition = 0;

			if (stream.CanSeek) {
				originalPosition = stream.Position;
				stream.Position = 0;
			}

			try {
				var readBuffer = new byte[4096];

				int totalBytesRead = 0;
				int bytesRead;

				while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0) {
					totalBytesRead += bytesRead;

					if (totalBytesRead == readBuffer.Length) {
						int nextByte = stream.ReadByte();
						if (nextByte != -1) {
							var temp = new byte[readBuffer.Length * 2];
							Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
							Buffer.SetByte(temp, totalBytesRead, (byte) nextByte);
							readBuffer = temp;
							totalBytesRead++;
						}
					}
				}

				var buffer = readBuffer;
				if (readBuffer.Length != totalBytesRead) {
					buffer = new byte[totalBytesRead];
					Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
				}
				return buffer;
			} finally {
				if (stream.CanSeek) {
					stream.Position = originalPosition;
				}
			}
		}
	}
}

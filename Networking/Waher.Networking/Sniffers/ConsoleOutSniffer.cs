﻿using System;
using System.Text;

namespace Waher.Networking.Sniffers
{
	/// <summary>
	/// How binary data is to be presented.
	/// </summary>
	public enum BinaryPresentationMethod
	{
		/// <summary>
		/// Has hexadecimal strings.
		/// </summary>
		Hexadecimal,

		/// <summary>
		/// Has base64 strings.
		/// </summary>
		Base64,

		/// <summary>
		/// Has simple byte counts.
		/// </summary>
		ByteCount
	}

	/// <summary>
	/// Type of line ending.
	/// </summary>
	public enum LineEnding
	{
		/// <summary>
		/// Pad with spaces until next rows. Makes sure line is colored properly.
		/// </summary>
		PadWithSpaces,

		/// <summary>
		/// End with new line characters. Is easier to read in a text editor.
		/// </summary>
		NewLine
	}

	/// <summary>
	/// Outputs sniffed data to <see cref="Console.Out"/>.
	/// </summary>
	public class ConsoleOutSniffer : SnifferBase
	{
		private const int TabWidth = 8;
		private readonly BinaryPresentationMethod binaryPresentationMethod;
		private readonly LineEnding lineEndingMethod;
		private bool consoleWidthWorks = true;

		/// <summary>
		/// Outputs sniffed data to <see cref="Console.Out"/>.
		/// </summary>
		/// <param name="BinaryPresentationMethod">How binary data is to be presented.</param>
		/// <param name="LineEndingMethod">Line ending method.</param>
		public ConsoleOutSniffer(BinaryPresentationMethod BinaryPresentationMethod,
			LineEnding LineEndingMethod)
		{
			this.binaryPresentationMethod = BinaryPresentationMethod;
			this.lineEndingMethod = LineEndingMethod;
		}

		/// <summary>
		/// Called when text has been transmitted.
		/// </summary>
		/// <param name="Text">Text</param>
		/// <param name="Timestamp">Timestamp of event.</param>
		public override void TransmitText(DateTime Timestamp, string Text)
		{
			this.Output(Timestamp, Text, ConsoleColor.Black, ConsoleColor.White);
		}

		/// <summary>
		/// Called when text has been received.
		/// </summary>
		/// <param name="Text">Text</param>
		/// <param name="Timestamp">Timestamp of event.</param>
		public override void ReceiveText(DateTime Timestamp, string Text)
		{
			this.Output(Timestamp, Text, ConsoleColor.White, ConsoleColor.DarkBlue);
		}

		/// <summary>
		/// Called when binary data has been transmitted.
		/// </summary>
		/// <param name="Data">Binary Data.</param>
		/// <param name="Timestamp">Timestamp of event.</param>
		public override void TransmitBinary(DateTime Timestamp, byte[] Data)
		{
			this.BinaryOutput(Timestamp, Data, ConsoleColor.Black, ConsoleColor.White);
		}

		/// <summary>
		/// Called when binary data has been received.
		/// </summary>
		/// <param name="Data">Binary Data.</param>
		/// <param name="Timestamp">Timestamp of event.</param>
		public override void ReceiveBinary(DateTime Timestamp, byte[] Data)
		{
			this.BinaryOutput(Timestamp, Data, ConsoleColor.White, ConsoleColor.DarkBlue);
		}

		private void BinaryOutput(DateTime Timestamp, byte[] Data, ConsoleColor Fg, ConsoleColor Bg)
		{
			switch (this.binaryPresentationMethod)
			{
				case BinaryPresentationMethod.Hexadecimal:
					StringBuilder Row = new StringBuilder();
					int i = 0;

					foreach (byte b in Data)
					{
						if (i > 0)
							Row.Append(' ');

						Row.Append(b.ToString("X2"));

						i = (i + 1) & 31;
						if (i == 0)
						{
							this.Output(Timestamp, Row.ToString(), Fg, Bg);
							Row.Clear();
						}
					}

					if (i != 0)
						this.Output(Timestamp, Row.ToString(), Fg, Bg);
					break;

				case BinaryPresentationMethod.Base64:
					this.Output(Timestamp, Convert.ToBase64String(Data), Fg, Bg);
					break;

				case BinaryPresentationMethod.ByteCount:
					this.Output(Timestamp, "<" + Data.Length.ToString() + " bytes>", Fg, Bg);
					break;
			}
		}

		/// <summary>
		/// Called to inform the viewer of something.
		/// </summary>
		/// <param name="Comment">Comment.</param>
		/// <param name="Timestamp">Timestamp of event.</param>
		public override void Information(DateTime Timestamp, string Comment)
		{
			this.Output(Timestamp, Comment, ConsoleColor.Yellow, ConsoleColor.DarkGreen);
		}

		/// <summary>
		/// Called to inform the viewer of a warning state.
		/// </summary>
		/// <param name="Warning">Warning.</param>
		/// <param name="Timestamp">Timestamp of event.</param>
		public override void Warning(DateTime Timestamp, string Warning)
		{
			this.Output(Timestamp, Warning, ConsoleColor.Black, ConsoleColor.Yellow);
		}

		/// <summary>
		/// Called to inform the viewer of an error state.
		/// </summary>
		/// <param name="Error">Error.</param>
		/// <param name="Timestamp">Timestamp of event.</param>
		public override void Error(DateTime Timestamp, string Error)
		{
			this.Output(Timestamp, Error, ConsoleColor.Yellow, ConsoleColor.Red);
		}

		/// <summary>
		/// Called to inform the viewer of an exception state.
		/// </summary>
		/// <param name="Exception">Exception.</param>
		/// <param name="Timestamp">Timestamp of event.</param>
		public override void Exception(DateTime Timestamp, string Exception)
		{
			this.Output(Timestamp, Exception, ConsoleColor.White, ConsoleColor.DarkRed);
		}

		private void Output(DateTime _, string s, ConsoleColor Fg, ConsoleColor Bg)
		{
			lock (Console.Out)
			{
				if (this.lineEndingMethod == LineEnding.PadWithSpaces)
				{
					ConsoleColor FgBak = Console.ForegroundColor;
					ConsoleColor BgBak = Console.BackgroundColor;
					int i, w;

					Console.ForegroundColor = Fg;
					Console.BackgroundColor = Bg;

					if (this.consoleWidthWorks)
					{
						try
						{
							w = Console.WindowWidth;

							foreach (string Row in s.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n'))
							{
								s = Row;

								if (s.IndexOf('\t') >= 0)
								{
									StringBuilder sb = new StringBuilder();
									string[] Parts = s.Split('\t');
									bool First = true;

									foreach (string Part in Parts)
									{
										if (First)
											First = false;
										else
										{
											i = Console.CursorLeft % TabWidth;
											sb.Append(new string(' ', TabWidth - i));
										}

										sb.Append(Part);
									}

									s = sb.ToString();
								}

								i = s.Length % w;

								if (i > 0)
									s += new string(' ', w - i);

								Console.Out.Write(s);
							}
						}
						catch (Exception)
						{
							this.consoleWidthWorks = false;
							Console.Out.WriteLine(s);
						}
					}
					else
						Console.Out.WriteLine(s);

					Console.ForegroundColor = Fg;
					Console.BackgroundColor = Bg;
				}
				else
					Console.Out.WriteLine(s);
			}
		}

		internal static readonly char[] CRLF = new char[] { '\r', '\n' };
	}
}

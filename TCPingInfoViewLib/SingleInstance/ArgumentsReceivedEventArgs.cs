using System;

namespace TCPingInfoViewLib.SingleInstance
{
	public class ArgumentsReceivedEventArgs : EventArgs
	{
		public string[] Args { get; set; }
	}
}

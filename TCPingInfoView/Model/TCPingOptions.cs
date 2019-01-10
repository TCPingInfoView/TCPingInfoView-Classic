using System.Drawing;
using System.Runtime.Serialization;

namespace TCPingInfoView.Model
{
	[DataContract]
	public class TCPingOptions
	{
		[DataMember]
		public int ReverseDnsTimeout = 3000;

		[DataMember]
		public int Timeout = 3000;

		[DataMember]
		public int HighLatency = 300;

		[DataMember]
		public int TCPingInterval = 60;

		[DataMember]
		public Color TimeoutColor = Color.Red;

		[DataMember]
		public Color HighLatencyColor = Color.Coral;

		[DataMember]
		public Color LowLatencyColor = Color.Green;

		[DataMember]
		public string Title = @"";
	}
}

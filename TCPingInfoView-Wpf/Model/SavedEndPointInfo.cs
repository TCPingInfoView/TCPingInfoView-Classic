using System;
using System.Collections.Generic;

namespace TCPingInfoView.Model
{
	[Serializable]
	public class SavedEndPointInfo
	{
		public IEnumerable<EndPointInfo> EndPointInfo { get; set; }

		public SavedEndPointInfo()
		{
			EndPointInfo = new EndPointInfo[0];
		}
	}
}

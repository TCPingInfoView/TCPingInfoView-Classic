namespace TCPingInfoViewLib.Model
{
	public class Config
	{
		public double StartTop { get; set; }
		public double StartLeft { get; set; }
		public double StartWidth { get; set; }
		public double StartHeight { get; set; }

		public Config()
		{
			StartTop = 0;
			StartLeft = 0;
			StartWidth = 800;
			StartHeight = 450;
		}
	}
}

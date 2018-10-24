using System;
using System.Collections;
using System.Linq;
using System.Net;

namespace TCPingInfoView.NetUtils
{
	internal class IPv4Subnet
	{
		#region data
		public readonly IPAddress Netmask;//子网掩码
		public readonly IPAddress Wildcard;//~子网掩码
		public readonly int CIDR;//无类别域间路由
		public readonly int Hosts;//主机数
		public readonly IPAddress FirstIP;//IP段的第一个IP地址
		public readonly IPAddress LastIP; //IP段的最后一个IP地址
		#endregion

		/// <summary>
		/// IPv4地址转整数(小端)
		/// </summary>
		/// <param name="ipv4">IPv4地址</param>
		/// <returns>IPv4地址所代表的小端整数</returns>
		public static uint IPv42UintLE(IPAddress ipv4)
		{
			var buf = ipv4.GetAddressBytes();
			return BitConverter.ToUInt32(buf, 0);
		}

		/// <summary>
		/// IPv4地址转整数(大端)
		/// </summary>
		/// <param name="ipv4">IPv4地址</param>
		/// <returns>IPv4地址所代表的大端整数</returns>
		public static uint IPv42UintBE(IPAddress ipv4)
		{
			var buf = ipv4.GetAddressBytes();
			Array.Reverse(buf);
			return BitConverter.ToUInt32(buf, 0);
		}

		/// <summary>
		/// 将表示二进制的字符串转为IPv4
		/// </summary>
		/// <param name="str">二进制字符串</param>
		/// <returns>IPv4地址</returns>
		public static IPAddress IPv4BinStrToIPv4(string str)
		{
			var bytesAddress = new[]
			{
					Convert.ToByte(str.Substring(0, 8), 2),
					Convert.ToByte(str.Substring(8, 8), 2),
					Convert.ToByte(str.Substring(16, 8), 2),
					Convert.ToByte(str.Substring(24, 8), 2)
			};
			return new IPAddress(bytesAddress);
		}

		/// <summary>
		/// 将IPv4转为二进制字符串
		/// </summary>
		/// <param name="ipv4">IPv4地址</param>
		/// <returns>二进制字符串</returns>
		public static string IPv4ToIPv4BinStr(IPAddress ipv4)
		{
			var bytesAddress = ipv4.GetAddressBytes();

			return
					$@"{Convert.ToString(bytesAddress[0], 2).PadLeft(8, '0')}{
						Convert.ToString(bytesAddress[1], 2).PadLeft(8, '0')}{
						Convert.ToString(bytesAddress[2], 2).PadLeft(8, '0')}{
						Convert.ToString(bytesAddress[3], 2).PadLeft(8, '0')}";
		}

		public static int Hosts2CIDR(int hosts)
		{
			return 32 - Convert.ToInt32(Math.Log(hosts, 2));
		}

		public static int CIDR2Hosts(int CIDR)
		{
			return Convert.ToInt32(Math.Pow(2, 32 - Convert.ToInt32(CIDR)));
		}

		#region RoutableIpAddress

		/// <summary>
		/// A null or empty string passed as the ipAddress will return true. An invalid ipAddress will be returned as true.
		/// </summary>
		/// <param name="ipAddress"></param>
		/// <returns></returns>
		public static bool IsNonRoutableIpAddress(string ipAddress)
		{
			//Reference: http://en.wikipedia.org/wiki/Reserved_IP_addresses

			//if the ip address string is empty or null string, we consider it to be non-routable
			if (string.IsNullOrEmpty(ipAddress))
			{
				return true;
			}

			if (!IPAddress.TryParse(ipAddress, out var tempIpAddress))
			{
				return true;
			}

			var ipAddressBytes = tempIpAddress.GetAddressBytes();

			if (IPFormatter.IsIPv4Address(tempIpAddress))
			{
				if (IsIpAddressInRange(ipAddressBytes, @"10.0.0.0/8")) //Class A Private network check
				{
					return true;
				}
				if (IsIpAddressInRange(ipAddressBytes, @"172.16.0.0/12")) //Class B private network check
				{
					return true;
				}
				if (IsIpAddressInRange(ipAddressBytes, @"192.168.0.0/16")) //Class C private network check
				{
					return true;
				}
				if (IsIpAddressInRange(ipAddressBytes, @"127.0.0.0/8")) //Loopback
				{
					return true;
				}
				if (IsIpAddressInRange(ipAddressBytes, @"0.0.0.0/8"))   //reserved for broadcast messages
				{
					return true;
				}
				return false;
			}

			//if ipAddress is IPv6
			if (IPFormatter.IsIPv6Address(tempIpAddress))
			{
				if (IsIpAddressInRange(ipAddressBytes, @"::/128"))       //Unspecified address
				{
					return true;
				}
				if (IsIpAddressInRange(ipAddressBytes, @"::1/128"))     //Loopback address for localhost
				{
					return true;
				}
				if (IsIpAddressInRange(ipAddressBytes, @"2001:db8::/32"))   //Addresses used in documentation
				{
					return true;
				}
				throw new NotImplementedException();
				//return false;
			}
			return true;
		}

		private static bool IsIpAddressInRange(byte[] ipAddressBytes, string reservedIpAddress)
		{
			if (string.IsNullOrEmpty(reservedIpAddress))
			{
				return false;
			}

			if (ipAddressBytes == null)
			{
				return false;
			}

			//Split the reserved ip address into a bitmask and ip address
			var ipAddressSplit = reservedIpAddress.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
			if (ipAddressSplit.Length != 2)
			{
				return false;
			}

			var ipAddressRange = ipAddressSplit[0];

			if (!IPAddress.TryParse(ipAddressRange, out var ipAddress))
			{
				return false;
			}

			// Convert the IP address to bytes.
			var ipBytes = ipAddress.GetAddressBytes();

			//parse the bits
			if (!int.TryParse(ipAddressSplit[1], out var bits))
			{
				bits = 0;
			}

			// BitConverter gives bytes in opposite order to GetAddressBytes().
			byte[] maskBytes = null;
			if (IPFormatter.IsIPv4Address(ipAddress))
			{
				var mask = ~(uint.MaxValue >> bits);
				maskBytes = BitConverter.GetBytes(mask).Reverse().ToArray();
			}
			else if (IPFormatter.IsIPv6Address(ipAddress))
			{
				//128 places
				var bitArray = new BitArray(128, false);

				//shift <bits> times to the right
				ShiftRight(bitArray, bits, true);

				//turn into byte array
				maskBytes = ConvertToByteArray(bitArray).Reverse().ToArray();
			}


			var result = true;

			//Calculate
			for (var i = 0; i < ipBytes.Length; ++i)
			{
				result &= (byte)(ipAddressBytes[i] & maskBytes[i]) == ipBytes[i];

			}

			return result;
		}

		private static void ShiftRight(BitArray bitArray, int shiftN, bool fillValue)
		{
			for (var i = shiftN; i < bitArray.Count; ++i)
			{
				bitArray[i - shiftN] = bitArray[i];
			}

			//fill the shifted bits as false
			for (var index = bitArray.Count - shiftN; index < bitArray.Count; ++index)
			{
				bitArray[index] = fillValue;
			}
		}

		private static byte[] ConvertToByteArray(BitArray bitArray)
		{
			// pack (in this case, using the first bool as the lsb - if you want
			// the first bool as the msb, reverse things ;-p)
			var bytes = (bitArray.Length + 7) / 8;
			var arr2 = new byte[bytes];
			var bitIndex = 0;
			var byteIndex = 0;

			for (var i = 0; i < bitArray.Length; ++i)
			{
				if (bitArray[i])
				{
					arr2[byteIndex] |= (byte)(1 << bitIndex);
				}

				++bitIndex;
				if (bitIndex == 8)
				{
					bitIndex = 0;
					++byteIndex;
				}
			}

			return arr2;
		}

		#endregion

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="ipv4">IPv4地址</param>
		/// <param name="hosts">该IP段主机数</param>
		public IPv4Subnet(IPAddress ipv4, int hosts)
		{
			Hosts = hosts;
			CIDR = 32 - Convert.ToInt32(Math.Log(hosts, 2));

			var netmaskStr = new string('1', CIDR) + new string('0', 32 - CIDR);
			Netmask = IPv4BinStrToIPv4(netmaskStr);
			var netmaskUint = IPv42UintLE(Netmask);

			var wildcardStr = new string('0', CIDR) + new string('1', 32 - CIDR);
			Wildcard = IPv4BinStrToIPv4(wildcardStr);
			var wildcardUint = IPv42UintLE(Wildcard);

			var ipv4Uint = IPv42UintLE(ipv4);

			FirstIP = new IPAddress(ipv4Uint & netmaskUint);
			LastIP = new IPAddress(ipv4Uint | wildcardUint);
		}
	}
}

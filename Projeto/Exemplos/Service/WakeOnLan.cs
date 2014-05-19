using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using MPSC.Library.Exemplos;

namespace MP.LBJC.Utils
{
	public class ProgramWakeOnLan : IExecutavel
	{
		[STAThread]
		public void Executar()
		{
			Application.Run(new FormWakeOnLan());
		}
	}

	public class FormWakeOnLan : Form
	{
		private Button btnWakeUp;
		private TextBox txtIp;
		private TextBox txtMac;
		private TextBox txtMask;
		private Label lbMAC;
		private Label lbIP;
		private Label lbNetMask;

		public FormWakeOnLan()
		{
			InitializeComponent();
		}

		private void btnWakeUp_Click(object sender, EventArgs e)
		{
			WakeOnLan.WakeUp(txtMac.Text, txtIp.Text, txtMask.Text);
		}

		private void InitializeComponent()
		{
			btnWakeUp = new Button();
			txtIp = new TextBox();
			txtMac = new  TextBox();
			txtMask = new TextBox();
			lbMAC = new Label();
			lbIP = new Label();
			lbNetMask = new Label();
			SuspendLayout();

			btnWakeUp.Location = new Point(195, 120);
			btnWakeUp.Name = "button1";
			btnWakeUp.Size = new Size(75, 23);
			btnWakeUp.TabIndex = 0;
			btnWakeUp.Text = "On";
			btnWakeUp.UseVisualStyleBackColor = true;
			btnWakeUp.Click += new EventHandler(this.btnWakeUp_Click);

			txtIp.Location = new Point(12, 73);
			txtIp.Name = "txtIp";
			txtIp.Size = new Size(100, 20);
			txtIp.TabIndex = 1;

			txtMac.Location = new Point(12, 24);
			txtMac.Name = "txtMac";
			txtMac.Size = new Size(100, 20);
			txtMac.TabIndex = 2;

			txtMask.Location = new Point(12, 123);
			txtMask.Name = "txtMask";
			txtMask.Size = new Size(100, 20);
			txtMask.TabIndex = 3;

			lbMAC.AutoSize = true;
			lbMAC.Location = new Point(13, 8);
			lbMAC.Name = "lbMAC";
			lbMAC.Size = new Size(30, 13);
			lbMAC.TabIndex = 4;
			lbMAC.Text = "MAC";

			lbIP.AutoSize = true;
			lbIP.Location = new Point(13, 54);
			lbIP.Name = "lbIP";
			lbIP.Size = new Size(17, 13);
			lbIP.TabIndex = 5;
			lbIP.Text = "IP";

			lbNetMask.AutoSize = true;
			lbNetMask.Location = new Point(13, 107);
			lbNetMask.Name = "lbNetMask";
			lbNetMask.Size = new Size(50, 13);
			lbNetMask.TabIndex = 6;
			lbNetMask.Text = "NetMask";

			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(282, 155);
			Controls.Add(lbNetMask);
			Controls.Add(lbIP);
			Controls.Add(lbMAC);
			Controls.Add(txtMask);
			Controls.Add(txtMac);
			Controls.Add(txtIp);
			Controls.Add(btnWakeUp);
			Name = "FormWakeOnLan";
			Text = "Wake On Lan";
			ResumeLayout(false);
			PerformLayout();
		}
	}
	
	public static class WakeOnLan
    {
        public static void WakeUp(string macAddress, string ipAddress, string subnetMask)
        {
            UdpClient client = new UdpClient();

            Byte[] datagram = new byte[102];

            for (int i = 0; i <= 5; i++)
            {
                datagram[i] = 0xff;
            }

            string[] macDigits = null;
            if (macAddress.Contains("-"))
            {
                macDigits = macAddress.Split('-');
            }
            else
            {
                macDigits = macAddress.Split(':');
            }

            if (macDigits.Length != 6)
            {
                throw new ArgumentException("Incorrect MAC address supplied!");
            }

            int start = 6;
            for (int i = 0; i < 16; i++)
            {
                for (int x = 0; x < 6; x++)
                {
                    datagram[start + i * 6 + x] = (byte)Convert.ToInt32(macDigits[x], 16);
                }
            }

            IPAddress address = IPAddress.Parse(ipAddress);
            IPAddress mask = IPAddress.Parse(subnetMask);
            IPAddress broadcastAddress = address.GetBroadcastAddress(mask);

            client.Send(datagram, datagram.Length, broadcastAddress.ToString(), 9);
        }
    }
	public static class SubnetMask
	{
		public static readonly IPAddress ClassA = IPAddress.Parse("255.0.0.0");
		public static readonly IPAddress ClassB = IPAddress.Parse("255.255.0.0");
		public static readonly IPAddress ClassC = IPAddress.Parse("255.255.255.0");

		public static IPAddress CreateByHostBitLength(int hostpartLength)
		{
			int hostPartLength = hostpartLength;
			int netPartLength = 32 - hostPartLength;

			if (netPartLength < 2)
				throw new ArgumentException("Number of hosts is to large for IPv4");

			Byte[] binaryMask = new byte[4];

			for (int i = 0; i < 4; i++)
			{
				if (i * 8 + 8 <= netPartLength)
					binaryMask[i] = (byte)255;
				else if (i * 8 > netPartLength)
					binaryMask[i] = (byte)0;
				else
				{
					int oneLength = netPartLength - i * 8;
					string binaryDigit =
						String.Empty.PadLeft(oneLength, '1').PadRight(8, '0');
					binaryMask[i] = Convert.ToByte(binaryDigit, 2);
				}
			}
			return new IPAddress(binaryMask);
		}

		public static IPAddress CreateByNetBitLength(int netpartLength)
		{
			int hostPartLength = 32 - netpartLength;
			return CreateByHostBitLength(hostPartLength);
		}

		public static IPAddress CreateByHostNumber(int numberOfHosts)
		{
			int maxNumber = numberOfHosts + 1;

			string b = Convert.ToString(maxNumber, 2);

			return CreateByHostBitLength(b.Length);
		}
	}

	public static class IPAddressExtensions
	{
		public static IPAddress GetBroadcastAddress(this IPAddress address, IPAddress subnetMask)
		{
			byte[] ipAdressBytes = address.GetAddressBytes();
			byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

			if (ipAdressBytes.Length != subnetMaskBytes.Length)
				throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

			byte[] broadcastAddress = new byte[ipAdressBytes.Length];
			for (int i = 0; i < broadcastAddress.Length; i++)
			{
				broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
			}
			return new IPAddress(broadcastAddress);
		}

		public static IPAddress GetNetworkAddress(this IPAddress address, IPAddress subnetMask)
		{
			byte[] ipAdressBytes = address.GetAddressBytes();
			byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

			if (ipAdressBytes.Length != subnetMaskBytes.Length)
				throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

			byte[] broadcastAddress = new byte[ipAdressBytes.Length];
			for (int i = 0; i < broadcastAddress.Length; i++)
			{
				broadcastAddress[i] = (byte)(ipAdressBytes[i] & (subnetMaskBytes[i]));
			}
			return new IPAddress(broadcastAddress);
		}

		public static bool IsInSameSubnet(this IPAddress address2, IPAddress address, IPAddress subnetMask)
		{
			IPAddress network1 = address.GetNetworkAddress(subnetMask);
			IPAddress network2 = address2.GetNetworkAddress(subnetMask);

			return network1.Equals(network2);
		}
	}

}

namespace MPSC.Library.Exemplos.Service
{
    using System;
    using System.Drawing;
    using System.Net;
    using System.Net.Sockets;
    using System.Windows.Forms;
    using MPSC.Library.Exemplos;

    public class ProgramWakeOnLan : IExecutavel
    {
        [STAThread]
        public void Executar()
        {
            Application.Run(new FormWakeUpOnLAN());
        }
    }

    public class FormWakeUpOnLAN : Form
    {
        public FormWakeUpOnLAN()
        {
            InitializeComponent();
        }

        private void btnWakeUp_Click(object sender, EventArgs e)
        {
            WakeUpOnLAN.WakeUp(txtMAC.Text);
        }

        private void InitializeComponent()
        {
            lblMAC = new Label();
            txtMAC = new TextBox();
            btnWakeUp = new Button();
            SuspendLayout();

            lblMAC.Location = new Point(10, 12);
            lblMAC.Size = new Size(30, 15);
            lblMAC.TabIndex = 0;
            lblMAC.Name = "lblMAC";
            lblMAC.Text = "MAC";

            txtMAC.Location = new Point(40, 10);
            txtMAC.Size = new Size(120, 19);
            txtMAC.TabIndex = 1;
            txtMAC.Name = "txtMAC";
            txtMAC.Text = "00-1C-C0-C0-F4-29";

            btnWakeUp.Location = new Point(170, 8);
            btnWakeUp.Size = new Size(70, 23);
            btnWakeUp.TabIndex = 2;
            btnWakeUp.Name = "btnWakeUp";
            btnWakeUp.Text = "Wake Up!";
            btnWakeUp.Click += new EventHandler(btnWakeUp_Click);

            Name = "FormWakeUpOnLAN";
            Text = "Wake Up On Lan";
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            ClientSize = new Size(250, 40);
            Controls.Add(lblMAC);
            Controls.Add(txtMAC);
            Controls.Add(btnWakeUp);
            ResumeLayout(false);
            PerformLayout();
        }
        private Button btnWakeUp;
        private TextBox txtMAC;
        private Label lblMAC;
    }

    public static class WakeUpOnLAN
    {
        public static Int32 WakeUp(String macAddress)
        {
            return WakeUp(macAddress, "255.255.255.255", 9, 7, 8, 21, 80);
        }

        public static Int32 WakeUp(String macAddress, String enderecoIP)
        {
            return WakeUp(macAddress, enderecoIP, 9, 7, 8, 21, 23, 80, 81, 88, 443, 1080, 8080, 8081, 8088, 8443, 40000);
        }

        public static Int32 WakeUp(String macAddress, String enderecoIP, params Int32[] portas)
        {
            Byte[] enderecoMAC = ConverterEnderecoMAC(macAddress);
            Byte[] pacoteWakeUp = CriarPacoteWakeUp(enderecoMAC);

            UdpClient udpClient = new UdpClient();
            Int32 bytes = 0;
            foreach (Int32 porta in portas)
                bytes += udpClient.Send(pacoteWakeUp, pacoteWakeUp.Length, enderecoIP, porta);
            return bytes;
        }

        private static Byte[] ConverterEnderecoMAC(String macAddress)
        {
            String[] macString = macAddress.Contains("-") ? macAddress.Split('-') : macAddress.Contains(":") ? macAddress.Split(':') : macAddress.Split('.');
            Byte[] macByte = new Byte[6];
            for (int i = 0; i < 6; i++)
                macByte[i] = Convert.ToByte(macString[i], 16);
            return macByte;
        }

        private static Byte[] CriarPacoteWakeUp(Byte[] enderecoMAC)
        {
            Byte[] pacote = new Byte[17 * 6];
            for (int i = 0; i < 6; i++) // Trailer of 6 times 0xFF.
                pacote[i] = 0xFF;
            for (int i = 1; i <= 16; i++) // Body of magic packet contains 16 times the MAC address.
                for (int j = 0; j < 6; j++)
                    pacote[i * 6 + j] = enderecoMAC[j];
            return pacote;
        }
    }

    public static class SubnetMask
    {
        public static readonly IPAddress ClassA = IPAddress.Parse("255.0.0.0");
        public static readonly IPAddress ClassB = IPAddress.Parse("255.255.0.0");
        public static readonly IPAddress ClassC = IPAddress.Parse("255.255.255.0");

        public static IPAddress CreateByHostBitLength(int hostPartLength)
        {
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
            return CreateByHostBitLength(32 - netpartLength);
        }

        public static IPAddress CreateByHostNumber(int numberOfHosts)
        {
            return CreateByHostBitLength(Convert.ToString(numberOfHosts + 1, 2).Length);
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

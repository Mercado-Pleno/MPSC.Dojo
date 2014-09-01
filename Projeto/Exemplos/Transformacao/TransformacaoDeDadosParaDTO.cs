using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace MPSC.Library.Exemplos.Transformacao
{
	using System;
	using MP.Library.Exemplos.Transformacao.Serializacao;

	public class TransformacaoDeDadosParaDTO : IExecutavel
	{
		public void Executar()
		{
			// Read and write purchase orders.
			Test t = new Test();
			t.CreatePO("po.xml");
			t.ReadPO("po.xml");

		}
		private void test()
		{
			TranformObject vTranformObject = new TransformXML();
			var str = vTranformObject.Serializar("Nome", "Bruno");
			Console.WriteLine(str);
			Console.WriteLine(vTranformObject.Discretizar("Nome", str));

			vTranformObject = new TransformCleanJSON();
			str = vTranformObject.Serializar("Nome", "Bruno");
			Console.WriteLine(str);
			Console.WriteLine(vTranformObject.Discretizar("Nome", str));

			vTranformObject = new TransformJSON();
			str = vTranformObject.Serializar("Nome", "Bruno");
			Console.WriteLine(str);
			Console.WriteLine(vTranformObject.Discretizar("Nome", str));

			TransformManager vTransformManager = new TransformManager();
			vTransformManager.Discretizador = new TransformJSON();
			vTransformManager.Serializador = new TransformJSON();
			Console.WriteLine(vTransformManager.Serializar("Cliente", new Cliente()));
		}
	}

	public class Cliente
	{
		public String Nome { get; set; }
		public String Idade { get; set; }
		public String Sexo { get; set; }
		public String Documento { get; set; }
		public String NomeMae { get; set; }
		public String NomePai { get; set; }
	}








	/* The XmlRootAttribute allows you to set an alternate name 
	   (PurchaseOrder) of the XML element, the element namespace; by 
	   default, the XmlSerializer uses the class name. The attribute 
	   also allows you to set the XML namespace for the element.  Lastly,
	   the attribute sets the IsNullable property, which specifies whether 
	   the xsi:null attribute appears if the class instance is set to 
	   a null reference. */
	[XmlRootAttribute("PurchaseOrder", Namespace = "http://www.cpandl.com", IsNullable = false)]
	public class PurchaseOrder
	{

		//[XmlAttribute]
		public Address ShipTo;

		//[XmlAttribute]
		public string OrderDate;

		[XmlArrayAttribute("Items")]
		public OrderedItem[] OrderedItems;

		//[XmlAttribute]
		public decimal SubTotal;

		//[XmlAttribute]
		public decimal ShipCost;
		
		//[XmlAttribute]
		public decimal TotalCost;
	}

	public class Address
	{
		//[XmlAttribute]
		public string Name;

		//[XmlAttribute]
		public string Line1;

		//[XmlAttribute]
		public string City;

		//[XmlAttribute]
		public string State;

		//[XmlAttribute]
		public string Zip;
	}

	public class OrderedItem
	{
		//[XmlAttribute]
		public string ItemName;

		//[XmlAttribute]
		public string Description;

		//[XmlAttribute]
		public decimal UnitPrice;

		//[XmlAttribute]
		public int Quantity;

		//[XmlAttribute]
		public decimal LineTotal;

		public void Calculate()
		{
			LineTotal = UnitPrice * Quantity;
		}
	}

	public class Test
	{

		public void CreatePO(string filename)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(PurchaseOrder));
			TextWriter writer = new StreamWriter(filename);

			PurchaseOrder po = GetPO();

			serializer.Serialize(writer, po);
			writer.Close();
		}

		private static PurchaseOrder GetPO()
		{
			PurchaseOrder po = new PurchaseOrder();

			// Create an address to ship and bill to.
			Address billAddress = new Address();
			billAddress.Name = "Teresa Atkinson";
			billAddress.Line1 = "1 Main St.";
			billAddress.City = "AnyTown";
			billAddress.State = "WA";
			billAddress.Zip = "00000";
			// Set ShipTo and BillTo to the same addressee.
			po.ShipTo = billAddress;
			po.OrderDate = System.DateTime.Now.ToLongDateString();

			// Create an OrderedItem object.
			OrderedItem i1 = new OrderedItem();
			i1.ItemName = "Widget S";
			i1.Description = "Small widget";
			i1.UnitPrice = (decimal)5.23;
			i1.Quantity = 3;
			i1.Calculate();

			// Insert the item into the array.
			OrderedItem[] items = { i1 };
			po.OrderedItems = items;
			// Calculate the total cost.
			decimal subTotal = new decimal();
			foreach (OrderedItem oi in items)
			{
				subTotal += oi.LineTotal;
			}
			po.SubTotal = subTotal;
			po.ShipCost = (decimal)12.51;
			po.TotalCost = po.SubTotal + po.ShipCost;
			return po;
		}

		public void ReadPO(string filename)
		{
			// Create an instance of the XmlSerializer class;
			// specify the type of object to be deserialized.
			XmlSerializer serializer = new XmlSerializer(typeof(PurchaseOrder));
			/* If the XML document has been altered with unknown 
			nodes or attributes, handle them with the 
			UnknownNode and UnknownAttribute events.*/
			serializer.UnknownNode += new
			XmlNodeEventHandler(serializer_UnknownNode);
			serializer.UnknownAttribute += new
			XmlAttributeEventHandler(serializer_UnknownAttribute);

			// A FileStream is needed to read the XML document.
			FileStream fs = new FileStream(filename, FileMode.Open);
			// Declare an object variable of the type to be deserialized.
			PurchaseOrder po;
			/* Use the Deserialize method to restore the object's state with
			data from the XML document. */
			po = (PurchaseOrder)serializer.Deserialize(fs);
			// Read the order date.
			Console.WriteLine("OrderDate: " + po.OrderDate);

			// Read the shipping address.
			Address shipTo = po.ShipTo;
			ReadAddress(shipTo, "Ship To:");
			// Read the list of ordered items.
			OrderedItem[] items = po.OrderedItems;
			Console.WriteLine("Items to be shipped:");
			foreach (OrderedItem oi in items)
			{
				Console.WriteLine("\t" +
				oi.ItemName + "\t" +
				oi.Description + "\t" +
				oi.UnitPrice + "\t" +
				oi.Quantity + "\t" +
				oi.LineTotal);
			}
			// Read the subtotal, shipping cost, and total cost.
			Console.WriteLine("\t\t\t\t\t Subtotal\t" + po.SubTotal);
			Console.WriteLine("\t\t\t\t\t Shipping\t" + po.ShipCost);
			Console.WriteLine("\t\t\t\t\t Total\t\t" + po.TotalCost);
		}

		protected void ReadAddress(Address a, string label)
		{
			// Read the fields of the Address object.
			Console.WriteLine(label);
			Console.WriteLine("\t" + a.Name);
			Console.WriteLine("\t" + a.Line1);
			Console.WriteLine("\t" + a.City);
			Console.WriteLine("\t" + a.State);
			Console.WriteLine("\t" + a.Zip);
			Console.WriteLine();
		}

		private void serializer_UnknownNode
		(object sender, XmlNodeEventArgs e)
		{
			Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
		}

		private void serializer_UnknownAttribute
		(object sender, XmlAttributeEventArgs e)
		{
			System.Xml.XmlAttribute attr = e.Attr;
			Console.WriteLine("Unknown attribute " +
			attr.Name + "='" + attr.Value + "'");
		}
	}
}
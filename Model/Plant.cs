using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MultiThreading.Model
{
	[XmlRoot("PLANT")]
	public class Plant
	{
		[XmlElement("AVAILABILITY")]
		public string Availability { get; set; }

		[XmlElement("BOTANICAL")]
		public string Botanical { get; set; }

		[XmlElement("COMMON")]
		public string Common { get; set; }

		[XmlElement("LIGHT")]
		public string Light { get; set; }

		[XmlElement("PRICE")]
		public string Price { get; set; }

		[XmlElement("ZONE")]
		public string Zone { get; set; }

		[XmlIgnore]
		public int Number { get; set; }

		public override string ToString()
		{
			return Common;
		}
	}
}

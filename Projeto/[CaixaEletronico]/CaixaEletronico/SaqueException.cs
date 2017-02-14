using System;
using System.Runtime.Serialization;

namespace MP.Library.CaixaEletronico
{
	[Serializable]
	public class SaqueException : Exception
	{
		public SaqueException() { }

		public SaqueException(string message) : base(message) { }

		public SaqueException(string message, Exception exceptionOriginal) : base(message, exceptionOriginal) { }

		protected SaqueException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DireitoECia.Model.Dominio
{
	public abstract class Entidade
	{
		public int Id { get; set; }
		public abstract void IsValid();
	}
}

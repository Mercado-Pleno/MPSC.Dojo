﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.SVNControl.MockData.DataBaseInterface
{
	public interface IStoredProcedure
	{
		String Nome { get; }

		int Executar(MockParameterCollection parameters);
	}
}

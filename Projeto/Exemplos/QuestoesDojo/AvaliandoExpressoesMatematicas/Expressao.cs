using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo.AvaliandoExpressoesMatematicas
{
	public class Expressao
	{
		public int Start { get; } = -1;
		public int Count { get; } = 0;
		public Elementos ElementosDentroDoParenteses { get; } = new Elementos(Enumerable.Empty<string>());

		public bool PossuiParenteses => (Start >= 0) && (Count > 0);

		public Expressao(Elementos elementos)
		{
			var index = 0;
			foreach (var elemento in elementos)
			{
				if (elemento == "(")
					Start = index;
				else if (elemento == ")")
				{
					Count = index - Start + 1;
					ElementosDentroDoParenteses = new Elementos(elementos.Skip(Start).Take(Count));
					break;
				}

				index++;
			}
		}

		public override string ToString()
		{
			return ElementosDentroDoParenteses.Join(" ");
		}
	}
}
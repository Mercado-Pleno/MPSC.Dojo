using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.Delegates
{
	/// <summary>
	/// O trecho baixo vai ajudar a palavra "delegate" fazer mais sentido:
	/// O delegate é uma espécie de recurso que permite que uma classe (ou um método dela) delegue (atribua / permita / solicite) uma pequena parte de sua responsabilidade para um outro MÉTODO (que normalmente está em outra classe)...
	/// Tipo assim: Classe que está me consumindo, eu faço tudo isso que você está me pedindo, mas tem uma pequena coisa que eu não posso (ou não sei) fazer por você... E você TERÁ que fazer essa pequena coisa por mim, ok?
	/// Se você concordar, então, me passa um método com a assinatura do delegate XYZ que eu vou chamá-lo.
	/// e o exemplo que eu coloquei foi o algoritmo  de ordenação... ele sabe ordenar
	/// mas precisa que a classe que está pedindo pra ordenar, tenha uma pequena responsabilidade durante o processo de ordenação... definir por qual campo será ordenado
	/// e aí... podemos usar o delegate InLine (como vc colocou no seu exemplo)
	/// usar uma expressão lambda (linq)
	/// ou referenciar diretamente um método que tenha a mesma "assinatura" definida pelo delegate
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	public delegate Boolean Comparar<T>(T a, T b);

	public static class Ordenacao
	{
		public static IEnumerable<Tipo> Ordenar<Tipo>(this IEnumerable<Tipo> colecao, Comparar<Tipo> comparar)
		{
			var lista = colecao.ToArray();

			for (var i = 0; i < lista.Length - 1; i++)
			{
				for (var j = i + 1; j < lista.Length; j++)
				{
					if (comparar(lista[i], lista[j]))
					{
						Tipo tmp = lista[i];
						lista[i] = lista[j];
						lista[j] = tmp;
					}
				}
			}

			return lista;
		}
	}
}
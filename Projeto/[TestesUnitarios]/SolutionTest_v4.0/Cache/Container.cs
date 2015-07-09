using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Cache
{
	public interface IContainer { }

	public interface IChaveUnica<TKey> { TKey Id { get; } }

	public abstract class ContainerAbstrato<TKey, TValue> : IContainer, IChaveUnica<Int64>
		where TKey : /*struct, IComparable<TKey>, IEquatable<TKey>,*/ IComparable, IFormattable, IConvertible
	{
		private readonly IDictionary<TKey, TValue> _dicionario;
		private readonly Int64 _id;
		public virtual Int64 Id { get { return _id; } }

		protected ContainerAbstrato() : this(new ConcurrentDictionary<TKey, TValue>()) { }
		protected ContainerAbstrato(IDictionary<TKey, TValue> dicionario)
		{
			_id = GetType().GUID.GetHashCode();
			_dicionario = dicionario;
		}

		public override String ToString()
		{
			return String.Format("{0} - {1}", _dicionario.Count, typeof(TValue).Name);
		}

		public virtual TValue Incluir(TKey key, TValue value)
		{
			_dicionario.Add(key, value);
			return value;
		}

		public virtual TValue1 Incluir<TValue1>(TValue1 value) where TValue1 : TValue, IChaveUnica<TKey>
		{
			_dicionario.Add(value.Id, value);
			return value;
		}

		public virtual TValue Obter(TKey key, TValue padrao = default(TValue))
		{
			return _dicionario.ContainsKey(key) ? _dicionario[key] : acionarPesquisaExterna(key, padrao);
		}

		private TValue acionarPesquisaExterna(TKey key, TValue padrao)
		{
			try
			{
				padrao = processarPesquisaExterna(key);
			}
			catch (Exception)
			{
			}

			return _dicionario[key] = padrao;
		}

		protected abstract TValue processarPesquisaExterna(TKey key);

	}

	public class ContainerDeObjetos : ContainerAbstrato<Int64, IContainer>
	{
		public T Obter<T>() where T : IContainer
		{
			return (T)Obter(typeof(T).GUID.GetHashCode());
		}

		protected override IContainer processarPesquisaExterna(Int64 key)
		{
			return null;
		}
	}
}
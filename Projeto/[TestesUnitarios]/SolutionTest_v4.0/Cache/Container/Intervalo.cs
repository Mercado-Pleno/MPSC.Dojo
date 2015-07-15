using System;
using System.Collections.Generic;

namespace Mongeral.ESB.Infraestrutura.Base
{
    public static class Intervalo
    {
        public static IIntervalo<DateTime> DeDatas(DateTime inicio, DateTime fim)
        {
            fim = fim == DateTime.MinValue ? DateTime.MaxValue : fim;

            return Intervalo<DateTime>.Novo(inicio, fim);
        }

        public static IIntervalo<int> DeNumerosInteiros(int inicio, int fim)
        {
            fim = fim == int.MinValue ? int.MaxValue : fim;

            return Intervalo<int>.Novo(inicio, fim);
        }

        public static IIntervalo<short> DeNumerosInteirosCurtos(short? inicio, short? fim)
        {
            if (!inicio.HasValue)
            {
                inicio = 0;
            }

            if (!fim.HasValue)
            {
                fim = short.MaxValue;    
            }
            
            return Intervalo<short>.Novo(inicio.GetValueOrDefault(), fim.GetValueOrDefault());
        }

        public static IIntervalo<decimal> DeValores(decimal? inicio, decimal? fim)
        {
            if (!inicio.HasValue)
            {
                inicio = 0;
            }

            if (!fim.HasValue)
            {
                fim = short.MaxValue;
            }

            return Intervalo<decimal>.Novo(inicio.GetValueOrDefault(), fim.GetValueOrDefault());
        }


        public static IIntervalo<long> DeNumerosInteirosLongos(long inicio, long fim)
        {
            fim = fim == long.MinValue ? long.MaxValue : fim;

            return Intervalo<long>.Novo(inicio, fim);
        }

        public static IIntervalo<decimal> DeValores(decimal inicio, decimal fim)
        {
            fim = fim == decimal.MinValue ? decimal.MaxValue : fim;

            return Intervalo<decimal>.Novo(inicio, fim);
        }
    }

    public interface IIntervalo<T> where T : IComparable<T>, IEquatable<T>
    {
        T Inicio { get; }
        T Fim { get; }

        bool Contem(T candidato);
        bool Equals(T candidato);
    }

    public class Intervalo<T> : IIntervalo<T> where T : IComparable<T>, IEquatable<T>
    {
        private readonly T fim;
        private readonly T inicio;

        private Intervalo(T inicio, T fim)
        {
            this.inicio = inicio;
            this.fim = fim;
        }

        #region IIntervalo<T> Members

        public T Inicio
        {
            get { return inicio; }
        }

        public T Fim
        {
            get { return fim; }
        }

        public bool Contem(T candidato)
        {
            if (Inicio.CompareTo(candidato) > 0
                || Fim.CompareTo(candidato) < 0)
                return false;

            return true;
        }

        #endregion

        #region Equals & HashCode

        public bool Equals(T candidato)
        {
            return Contem(candidato);
        }

        protected bool Equals(Intervalo<T> outroIntervalo)
        {
            return EqualityComparer<T>.Default.Equals(inicio, outroIntervalo.inicio)
                   && EqualityComparer<T>.Default.Equals(fim, outroIntervalo.fim);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (obj is T) return Equals((T) obj);

            if (obj.GetType() != GetType()) return false;

            return Equals((Intervalo<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(inicio)*397) ^
                       EqualityComparer<T>.Default.GetHashCode(fim);
            }
        }

        #endregion

        public static Intervalo<TV> Novo<TV>(TV inicio, TV fim) where TV : IComparable<TV>, IEquatable<TV>
        {
            return new Intervalo<TV>(inicio, fim);
        }

    }
}
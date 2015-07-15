using System;

namespace Mongeral.ESB.Infraestrutura.Base.Containers
{
    public class Validade
    {
        private readonly IIntervalo<DateTime> periodo;
        private readonly long tempoEmSegundos;

        private Validade(long segundos)
        {
            tempoEmSegundos = segundos;

            periodo = CriarPeriodoDeValidade(segundos);
        }

        public bool Expirada
        {
            get { return !periodo.Contem(DateTime.Now); }
        }

        public Validade Renovar()
        {
            return Nova(tempoEmSegundos);
        }

        private IIntervalo<DateTime> CriarPeriodoDeValidade(long segundos)
        {
            DateTime agora = DateTime.Now;
            DateTime ateFimDaValidade = agora.AddSeconds(segundos);

            return Intervalo.DeDatas(agora, ateFimDaValidade);            
        }

        /*
         * 
         * Métodos Estático
         * 
         */

        public static Validade Nova(long segundos)
        {
            return new Validade(segundos);
        }
    }
}
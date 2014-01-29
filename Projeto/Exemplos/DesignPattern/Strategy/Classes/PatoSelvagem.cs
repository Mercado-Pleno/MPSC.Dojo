namespace MPSC.Library.Exemplos.DesignPattern.Strategy.Classes
{
    using MPSC.Library.Exemplos.DesignPattern.Strategy.Comportamento;

    public class PatoSelvagem : Pato
    {
        public PatoSelvagem()
            : base(new Voa(), new Grasnar())
        {
        }
    }
}
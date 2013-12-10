using System;
using System.Xml.Serialization;
using System.Reflection;

namespace TesteCase
{
    public class Objeto
    {
        private static readonly Object LockObject = new Object();
        private static Int64 id = 0;
        private static Int64 Id { get { lock (LockObject) id++; return id; } }

        private Int64 ObjectInstanceId = Id;

    }

    public static class Extensao
    {
        public static String Descricao(this Enum enumerado)
        {
            FieldInfo fieldInfo = enumerado.GetType().GetField(enumerado.ToString());
            Object[] atributos = ((fieldInfo != null) ? fieldInfo.GetCustomAttributes(true) : new Object[] { });
            String descricao = ((atributos.Length > 0) && (atributos[0] is XmlEnumAttribute)) ? (atributos[0] as XmlEnumAttribute).Name : String.Empty;
            //return String.Format("{0}", enumerado);
            return String.Format("{0}={1} ({2})", enumerado.ToString("G"), enumerado.ToString("D"), descricao);
        }
    }

    [FlagsAttribute]
    public enum Dia
    {
        [XmlEnum("Domingo")]
        Domingo,
        [XmlEnum("Segunda-feira")]
        Segunda,
        [XmlEnum("Terça-feira")]
        Terca,
        [XmlEnum("Quarta-feira")]
        Quarta,
        [XmlEnum("Quinta-feira")]
        Quinta,
        [XmlEnum("Sexta-feira")]
        Sexta,
        [XmlEnum("Sábado")]
        Sabado
    }
    public class Example
    {
        enum Days { Saturday, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday };
        enum BoilingPoints { Celsius = 100, Fahrenheit = 212 };
        [FlagsAttribute]
        enum Colors { Red = 1, Green = 2, Blue = 4, Yellow = 8 };

        public static void Demo()
        {
            Type weekdays = typeof(Days);

            Console.WriteLine(String.Format("The day of the week today is {0}.",
                                Enum.Parse(weekdays, DateTime.Now.DayOfWeek.ToString(), false)));

            Console.WriteLine(String.Format("The BoilingPoints Enum defines the following items, and corresponding values:"));
            Console.WriteLine(String.Format("   The boiling point in degrees {0:G} is {0:D}.",BoilingPoints.Celsius));
            Console.WriteLine(String.Format("   The boiling point in degrees {0:G} is {0:X}.",BoilingPoints.Fahrenheit));

            Colors myColors = Colors.Red | Colors.Yellow | Colors.Blue ;
            Console.WriteLine(String.Format("myColors holds the following combination of colors: {0}", myColors));

        }
    }


    public class SaberQuemInstanciou
    {

       
        public static void Main2()
        {
            Dia dia = Dia.Sabado | Dia.Segunda;
            Console.WriteLine(dia.Descricao());

            Example.Demo();

            /*
            A a = new A();
            B b = new B();

            C c1 = new C(null);
            C c2 = a.InstanciarERetornoarC();
            C c3 = b.InstanciarERetornoarC();
            C c4 = new C(new SaberQuemInstanciou());

            Console.WriteLine(c1.ToString());
            Console.WriteLine(c2.ToString());
            Console.WriteLine(c3.ToString());
            Console.WriteLine(c4.ToString());
             * */
        }
    }



    public class A
    {
        public A()
        {
            //...
        }
        public C InstanciarERetornoarC()
        {
            C c = new C(this);
            return c;
        }
    }

    public class B
    {
        public B()
        {
            //...
        }
        public C InstanciarERetornoarC()
        {
            C c = new C(this);
            return c;
        }
    }

    public class C
    {
        private Object owner;

        public C(Object pOwner)
        {
            this.owner = pOwner;
        }
        //...

        public override String ToString()
        {
            String vRetorno = String.Empty;

            if (owner == null)
                vRetorno = "Quem Instanciou C foi um Objeto que Passou um Parâmetro Nulo";
            else if (owner is A)
                vRetorno = "Quem Instanciou C foi um Objeto da Classe A";
            else if (owner is B)
                vRetorno = "Quem Instanciou C foi um Objeto da Classe B";
            else
                vRetorno = "Quem Instanciou C foi um Objeto da Classe " + owner.GetType().Name;

            return vRetorno;
        }
    }
}
namespace MP.SVNControl
{
    using System;
    using MP.SVNControl;
    using System.Threading;

    public static class Program
    {
        public static int Main(String[] args)
        {
            SVNParam vSVNParam = new SVNParam(args);
            Console.WriteLine(vSVNParam.ToString());
            return 0;
        }
    }
}

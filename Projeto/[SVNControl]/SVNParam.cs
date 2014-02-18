namespace MP.SVNControl
{
    using System;

    public class SVNParam
    {
        private const String aspasDuplas = @"""";
        private const String aspasSimples = @"'";

        private String FullScriptName { get; set; }
        public String RepositoryRoot { get; private set; }
        public String ScriptName { get; private set; }
        public String NomeUsuario { get; private set; }
        public String TxnName { get; private set; }
        public String Revisao { get; private set; }
        public String Capabilities { get; private set; }
        public String PropertyName { get; private set; }
        public String Action { get; private set; }
        public String PathOfFile { get; set; }
        public String Token { get; private set; }
        public String BreakUnlock { get; private set; }
        public String StdInVal { get; private set; }
        public String Descricao { get; private set; }
        public String StealLock { get; private set; }
        public String LockTokens { get; private set; }
        public String Adicional1 { get; private set; }
        public String Adicional2 { get; private set; }
        public String Adicional3 { get; private set; }
        public String Adicional4 { get; private set; }
        public Boolean IsOK { get; private set; }

        public SVNParam(String[] param)
        {
            FullScriptName = Get(param, 0, true);
            RepositoryRoot = Get(param, 1, true);
            ScriptName = FullScriptName.Substring(FullScriptName.LastIndexOf("/") + 1);
            IsOK = Get(param, 10).Equals(";");

            if (ScriptName.Contains("start-commit"))
            {
                NomeUsuario = Get(param, 2);
                Capabilities = Get(param, 3);
                TxnName = Get(param, 4);
                Adicional1 = Get(param, 5);
                Adicional2 = Get(param, 6);
                Adicional3 = Get(param, 7);
                Adicional4 = Get(param, 8);
            }
            else if (ScriptName.Contains("pre-commit"))
            {
                TxnName = Get(param, 2);
                LockTokens = Get(param, 3);
                Adicional1 = Get(param, 4);
                Adicional2 = Get(param, 5);
                Adicional3 = Get(param, 6);
                Adicional4 = Get(param, 7);
            }
            else if (ScriptName.Contains("post-commit"))
            {
                Revisao = Get(param, 2);
                TxnName = Get(param, 3);
                Adicional1 = Get(param, 4);
                Adicional2 = Get(param, 5);
                Adicional3 = Get(param, 6);
                Adicional4 = Get(param, 7);
            }
            else if (ScriptName.Contains("pre-lock"))
            {
                PathOfFile = Get(param, 2, true);
                NomeUsuario = Get(param, 3);
                Descricao = Get(param, 4);
                StealLock = Get(param, 5);
                Adicional1 = Get(param, 6);
                Adicional2 = Get(param, 7);
                Adicional3 = Get(param, 8);
                Adicional4 = Get(param, 9);
            }
            else if (ScriptName.Contains("post-lock"))
            {
                NomeUsuario = Get(param, 2);
                Adicional1 = Get(param, 3);
                Adicional2 = Get(param, 4);
                Adicional3 = Get(param, 5);
                Adicional4 = Get(param, 6);
            }
            else if (ScriptName.Contains("pre-unlock"))
            {
                PathOfFile = Get(param, 2, true);
                NomeUsuario = Get(param, 3);
                Token = Get(param, 4);
                BreakUnlock = Get(param, 5);
                Adicional1 = Get(param, 6);
                Adicional2 = Get(param, 7);
                Adicional3 = Get(param, 8);
                Adicional4 = Get(param, 9);
            }
            else if (ScriptName.Contains("post-unlock"))
            {
                NomeUsuario = Get(param, 2);
                Adicional1 = Get(param, 3);
                Adicional2 = Get(param, 4);
                Adicional3 = Get(param, 5);
                Adicional4 = Get(param, 6);
            }
            else if (ScriptName.Contains("pre-revprop-change"))
            {
                Revisao = Get(param, 2);
                NomeUsuario = Get(param, 3);
                PropertyName = Get(param, 4);
                Action = Get(param, 5);
                StdInVal = Get(param, 6);
                Adicional1 = Get(param, 7);
                Adicional2 = Get(param, 8);
                Adicional3 = Get(param, 9);
            }
            else if (ScriptName.Contains("post-revprop-change"))
            {
                Revisao = Get(param, 2);
                NomeUsuario = Get(param, 3);
                PropertyName = Get(param, 4);
                Action = Get(param, 5);
                StdInVal = Get(param, 6);
                Adicional1 = Get(param, 7);
                Adicional2 = Get(param, 8);
                Adicional3 = Get(param, 9);
            }
        }

        public override String ToString()
        {
            var vRetorno = "\nFullScriptName=" + FullScriptName +
                "\nRepositoryRoot=" + RepositoryRoot +
                "\nScriptName=" + ScriptName +
                "\nNomeUsuario=" + NomeUsuario +
                "\nTxnName=" + TxnName +
                "\nRevisao=" + Revisao +
                "\nCapabilities=" + Capabilities +
                "\nPropertyName=" + PropertyName +
                "\nAction=" + Action +
                "\nPathOfFile=" + PathOfFile +
                "\nToken=" + Token +
                "\nBreakUnlock=" + BreakUnlock +
                "\nStdInVal=" + StdInVal +
                "\nDescricao=" + Descricao +
                "\nStealLock=" + StealLock +
                "\nLockTokens=" + LockTokens +
                "\nAdicional1=" + Adicional1 +
                "\nAdicional2=" + Adicional2 +
                "\nAdicional3=" + Adicional3 +
                "\nAdicional4=" + Adicional4 +
                "\nIsOK=" + IsOK;
            return vRetorno;
        }

        private String Get(String[] param, int posicao)
        {
            return Get(param, posicao, false);
        }

        private String Get(String[] param, int posicao, bool path)
        {
            var vRetorno = (param != null) && (param.Length > posicao) ? param[posicao] : String.Empty;

            while (vRetorno.StartsWith(aspasDuplas) && vRetorno.EndsWith(aspasDuplas))
                vRetorno = vRetorno.Substring(1, vRetorno.Length - 2);
            while (vRetorno.StartsWith(aspasSimples) && vRetorno.EndsWith(aspasSimples))
                vRetorno = vRetorno.Substring(1, vRetorno.Length - 2);

            if (path)
                vRetorno = vRetorno.Replace(@"\", @"/").ToLower();

            return vRetorno;
        }
    }
}
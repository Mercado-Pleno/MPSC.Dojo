namespace MPSC.SVNControl
{
    using System;

    public class SVNParam
	{
		#region //Constantes
		public const String cStartCommitCmd = "start-commit.cmd";
		public const String cPreCommitCmd = "pre-commit.cmd";
		public const String cPostCommitCmd = "post-commit.cmd";
		public const String cPreLockCmd = "pre-lock.cmd";
		public const String cPostLockCmd = "post-lock.cmd";
		public const String cPreUnLockCmd = "pre-unlock.cmd";
		public const String cPostUnLockCmd = "post-unlock.cmd";
		public const String cPreRevPropChangeCmd = "pre-revprop-change.cmd";
		public const String cPostRevPropChangeCmd = "post-revprop-change.cmd";

        private const String aspasDuplas = @"""";
        private const String aspasSimples = @"'";
		#endregion //Constantes

		#region //Propriedades Booleanas
		public Boolean IsStartCommitCmd { get { return ScriptName.Equals(cStartCommitCmd); } }
		public Boolean IsPreCommitCmd { get { return ScriptName.Equals(cPreCommitCmd); } }
		public Boolean IsPostCommitCmd { get { return ScriptName.Equals(cPostCommitCmd); } }
		public Boolean IsPreLockCmd { get { return ScriptName.Equals(cPreLockCmd); } }
		public Boolean IsPostLockCmd { get { return ScriptName.Equals(cPostLockCmd); } }
		public Boolean IsPreUnLockCmd { get { return ScriptName.Equals(cPreUnLockCmd); } }
		public Boolean IsPostUnLockCmd { get { return ScriptName.Equals(cPostUnLockCmd); } }
		public Boolean IsPreRevPropChangeCmd { get { return ScriptName.Equals(cPreRevPropChangeCmd); } }
		public Boolean IsPostRevPropChangeCmd { get { return ScriptName.Equals(cPostRevPropChangeCmd); } }
		public Boolean IsOK { get; private set; }
		#endregion //Propriedades Booleanas

		#region //Propriedades
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
		#endregion //Propriedades
		
		public SVNParam(String[] param)
        {
            FullScriptName = Get(param, 0, true);
            RepositoryRoot = Get(param, 1, true);
            ScriptName = FullScriptName.Substring(FullScriptName.LastIndexOf("/") + 1);
            IsOK = Get(param, 10).Equals(";");

            if (IsStartCommitCmd)
            {
                NomeUsuario = Get(param, 2);
                Capabilities = Get(param, 3);
                TxnName = Get(param, 4);
                Adicional1 = Get(param, 5);
                Adicional2 = Get(param, 6);
                Adicional3 = Get(param, 7);
                Adicional4 = Get(param, 8);
            }
			else if (IsPreCommitCmd)
            {
                TxnName = Get(param, 2);
                LockTokens = Get(param, 3);
                Adicional1 = Get(param, 4);
                Adicional2 = Get(param, 5);
                Adicional3 = Get(param, 6);
                Adicional4 = Get(param, 7);
            }
			else if (IsPostCommitCmd)
            {
                Revisao = Get(param, 2);
                TxnName = Get(param, 3);
                Adicional1 = Get(param, 4);
                Adicional2 = Get(param, 5);
                Adicional3 = Get(param, 6);
                Adicional4 = Get(param, 7);
            }
			else if (IsPreLockCmd)
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
			else if (IsPostLockCmd)
            {
                NomeUsuario = Get(param, 2);
                Adicional1 = Get(param, 3);
                Adicional2 = Get(param, 4);
                Adicional3 = Get(param, 5);
                Adicional4 = Get(param, 6);
            }
			else if (IsPreUnLockCmd)
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
			else if (IsPostUnLockCmd)
            {
                NomeUsuario = Get(param, 2);
                Adicional1 = Get(param, 3);
                Adicional2 = Get(param, 4);
                Adicional3 = Get(param, 5);
                Adicional4 = Get(param, 6);
            }
			else if (IsPreRevPropChangeCmd)
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
			else if (IsPostRevPropChangeCmd)
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
            var vRetorno = "FullScriptName=" + FullScriptName +
                "\r\nRepositoryRoot=" + RepositoryRoot +
				"\r\nScriptName=" + ScriptName +
				"\r\nNomeUsuario=" + NomeUsuario +
				"\r\nTxnName=" + TxnName +
				"\r\nRevisao=" + Revisao +
				"\r\nCapabilities=" + Capabilities +
				"\r\nPropertyName=" + PropertyName +
				"\r\nAction=" + Action +
				"\r\nPathOfFile=" + PathOfFile +
				"\r\nToken=" + Token +
				"\r\nBreakUnlock=" + BreakUnlock +
				"\r\nStdInVal=" + StdInVal +
				"\r\nDescricao=" + Descricao +
				"\r\nStealLock=" + StealLock +
				"\r\nLockTokens=" + LockTokens +
				"\r\nAdicional1=" + Adicional1 +
				"\r\nAdicional2=" + Adicional2 +
				"\r\nAdicional3=" + Adicional3 +
				"\r\nAdicional4=" + Adicional4 +
				"\r\nIsOK=" + IsOK + "\r\n";
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
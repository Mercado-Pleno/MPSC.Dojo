program compilador;

uses
  Forms,
  uCodigo in 'uCodigo.pas' {fCodigo},
  uFuncoes in 'uFuncoes.pas',
  UTokens in 'UTokens.pas';

{$R *.RES}

begin
  Application.Initialize;
  Application.CreateForm(TfCodigo, fCodigo);
  Application.Run;
end.

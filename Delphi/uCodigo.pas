unit uCodigo;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, UTokens, Grids;

type
  TfCodigo = class(TForm)
    memoCodigoFonte: TMemo;
    listTokens: TListBox;
    btnListarTokens: TButton;
    btnCriaLista: TButton;
    strgrdTokens: TStringGrid;
    btnSair: TButton;
    procedure btnListarTokensClick(Sender: TObject);
    procedure btnCriaListaClick(Sender: TObject);
    procedure btnSairClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure memoCodigoFonteChange(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    listaTokens:PLista;
  end;

var
  fCodigo: TfCodigo;

implementation

uses UFuncoes;
{$R *.DFM}

procedure TfCodigo.btnListarTokensClick(Sender: TObject);
Var Tokens:TStringList;
var i:word;
begin
    listTokens.Clear();
    FazListaVazia();
    Tokens:= QuebrarString(memoCodigoFonte.Text);
    for i:=0 to Tokens.Count-1 do
        listTokens.Items.Add(Tokens.Strings[i]);
    self.btnCriaLista.Enabled:=true;
end;

procedure TfCodigo.btnCriaListaClick(Sender: TObject);
var aux:PLista;
    i:word;
begin
    aux:=RetornaPonteiro;
    i:=0;
    while aux<>nil do
        begin
        inc(i);
        strgrdTokens.Cells[0,i]:= aux^.token.sequencia;
        strgrdTokens.Cells[1,i]:= inttostr(aux^.token.posicao);
        strgrdTokens.Cells[2,i]:= aux^.token.TipoToStr();
        strgrdTokens.RowCount:= i+2;
        aux:=aux^.prox;
        end;
 end;

procedure TfCodigo.btnSairClick(Sender: TObject);
begin
    close;
end;

procedure TfCodigo.FormCreate(Sender: TObject);
begin
    strgrdTokens.Cells[0,0]:= 'Sequencia';
    strgrdTokens.Cells[1,0]:= 'Posição';
    strgrdTokens.Cells[2,0]:= 'Tipo';
end;

procedure TfCodigo.memoCodigoFonteChange(Sender: TObject);
begin
    btnListarTokens.Enabled:=length(trim(memoCodigoFonte.Text))>0;
end;

end.

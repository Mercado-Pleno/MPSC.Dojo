unit UTokens;

interface


type

    TTipoToken = (
    ttIdentificador,
    ttInicio,
    ttFim,
    ttAtribuicao,
    ttSeparador,
    ttRelacional,
    ttCondicaoSe,
    ttComando,
    ttCondicaoCaso,
    ttAbreParenteses,
    ttFechaParenteses,
    ttFimDeComando,
    ttOperador,
    ttConstante,
    ttNaoEspecifico);

    TToken = class
        posicao:word;
        sequencia:string;
        tipo:TTipoToken;
        function TipoToStr():String;
        end;

    PLista = ^Tlista;
    Tlista = record
       token: TToken;
       prox : Plista;
       end;

procedure encadear(token:TToken);
function RetornaPonteiro:Plista;
procedure FazListaVazia;
function RetornaTipoToken(nomeToken:string):TTipoToken;

implementation
uses sysUtils;
var lista:PLista;

function RetornaTipoToken(nomeToken:string):TTipoToken;
begin
    nomeToken:=UpperCase(nomeToken);
    if nomeToken = 'INICIO' then
        result:= ttInicio
    else if nomeToken = 'FIM' then
        result:= ttFim
    else if pos(nomeToken,':,')>0 then
        result:= ttSeparador
    else if pos(nomeToken,'<>=')>0 then
        result:= ttRelacional
    else if nomeToken = ':=' then
        result:= ttAtribuicao
    else if nomeToken = ';' then
        result:= ttFimDeComando
    else if nomeToken = 'SE' then
        result:= ttCondicaoSe
    else if pos(nomeToken[1],'1234567890')>0 then
        result:= ttConstante
    else if (length(nomeToken)=5) and (pos(nomeToken,'ENTAO,SENAO')>0) then
        result:= ttComando
    else if pos(nomeToken,'+,-,*,/')>0 then
        result:= ttOperador
    else if pos(nomeToken,'+,-,*,/')>0 then
        result:= ttOperador
    else if nomeToken = 'CASO' then
        result:= ttCondicaoCaso
    else if nomeToken = ')' then
        result:= ttFechaParenteses
    else if nomeToken = '(' then
        result:= ttAbreParenteses
    else if nomeToken[1] in ['A'..'Z','_'] then
        result:= ttIdentificador
    else result:= ttNaoEspecifico;
end;

function TToken.TipoToStr():String;
begin
    case tipo of
        ttIdentificador  : result:='ttIdentificador';
        ttInicio         : result:='ttInicio';
        ttFim            : result:='ttFim';
        ttAtribuicao     : result:='ttAtribuicao';
        ttSeparador      : result:='ttSeparador';
        ttRelacional     : result:='ttRelacional';
        ttCondicaoSe     : result:='ttCondicaoSe';
        ttComando        : result:='ttComando';
        ttCondicaoCaso   : result:='ttCondicaoCaso';
        ttAbreParenteses : result:='ttAbreParenteses';
        ttFechaParenteses: result:='ttFechaParenteses';
        ttFimDeComando   : result:='ttFimDeComando';
        ttOperador       : result:='ttOperadores';
        ttConstante      : result:='ttConstante';
        ttNaoEspecifico  : result:='ttNaoEspecifico';
    else
        result:='';
    end;
end;

function RetornaPonteiro:Plista;
begin
    result:=lista;
end;

procedure FazListaVazia;
var aux:PLista;
begin
    aux:=lista;
    while aux<>nil do
        begin
        aux:=aux^.prox;
        dispose(lista);
        lista:=aux;
        end;
    lista:=nil;
end;


procedure encadear(token:TToken);
var novo:Plista;
begin
    if lista = nil then
        begin
        new(lista);
        lista^.token:=token;
        lista^.prox:=nil;
        end
    else
        begin
        novo:=lista;
        while novo^.prox<>nil do
             novo:=novo^.prox;
        new(novo^.prox);
        novo^.Prox^.token:=token;
        novo^.Prox^.prox:=nil;
        end;

end;

begin
    lista:=nil;
end.

 
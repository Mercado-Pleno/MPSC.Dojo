unit uFuncoes;

interface
uses classes,Sysutils;
function QuebrarString(texto:string):TStringList;
        
implementation
uses UTokens;

procedure NovoToken(pos:Integer;nomeToken:string);
var token:TToken;
begin
    token:=TToken.Create();
    token.posicao:=Pos;
    token.sequencia:=nomeToken;
    token.tipo := RetornaTipoToken(nomeToken);
    encadear(token);
end;

function QuebrarString(texto:string):TStringList;
var posicao,i,local:integer;
var separadores,Especiais,lcToken:string;
begin
    separadores:= '+-*/;,=:()><';
    Especiais:= #13+#10+#9+' ';
    result:=TStringList.create;
    texto:=trim(texto)+#13;
    i:=0;
    local:=0;
    while length(texto)>0 do
    begin
        inc(i);
        while (length(texto)>0) and (pos(texto[1],Especiais)>0) do
            begin
            texto:=copy(texto,2,length(texto));
            inc(local);
            end;
        while (length(texto)>0) and (pos(texto[1],Separadores)>0) do
            begin
            inc(local);
            NovoToken(local+1,texto[1]);  //encadear o que tah antes do :=
            result.append(texto[1]);
            texto:=copy(texto,2,length(texto));
            end;
        if (i<>1)and(length(texto)>0) and (pos(texto[i],separadores+Especiais)>0) then
            begin
            if (texto[i]=':')and(i<length(texto))and(texto[i+1]='=') then
                begin
                posicao:=pos(texto[i],texto);
                lcToken:=copy(texto,1,posicao-1);
                NovoToken(local-length(lcToken)+1,lcToken);  //encadear o que tah antes do :=
                result.append(lcToken);
                NovoToken(local-length(lcToken)+2,copy(texto,posicao,2)); //encadear o :=
                result.append(copy(texto,posicao,2));
                texto := copy(texto,posicao+2,length(texto));
                i:=0;
                inc(local);
                end
             else
                if pos(texto[i],separadores)>0 then
                    begin
                    posicao:=pos(texto[i],texto);
                    lcToken:=copy(texto,1,posicao-1);
                    NovoToken(local-length(lcToken)+1,lcToken);  //encadear o que tah antes do :=
                    result.append(lcToken);
                    NovoToken(local+1,copy(texto,posicao,1)); //encadear o : ou o =
                    result.append(copy(texto,posicao,1));
                    texto := copy(texto,posicao+1,length(texto));
                    i:=0;
                    end
                else
                    begin
                    posicao:=pos(texto[i],texto);
                    lcToken:= copy(texto,1,posicao-1);
                    NovoToken(local-length(lcToken)+1,lcToken);
                    result.append(copy(texto,1,posicao-1));
                    texto := copy(texto,posicao+1,length(texto));
                    i:=0;
                    end;
                end;
        inc(local);
    end;
end;


end.
 
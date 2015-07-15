object fCodigo: TfCodigo
  Left = 189
  Top = 101
  Width = 400
  Height = 426
  Caption = 'Meu Primeiro Compilador'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object memoCodigoFonte: TMemo
    Left = 0
    Top = 0
    Width = 273
    Height = 177
    TabOrder = 0
    OnChange = memoCodigoFonteChange
  end
  object listTokens: TListBox
    Left = 272
    Top = 0
    Width = 113
    Height = 313
    ItemHeight = 13
    TabOrder = 1
  end
  object btnListarTokens: TButton
    Left = 281
    Top = 320
    Width = 105
    Height = 25
    Caption = '&Processar'
    Enabled = False
    TabOrder = 2
    OnClick = btnListarTokensClick
  end
  object btnCriaLista: TButton
    Left = 281
    Top = 344
    Width = 105
    Height = 25
    Caption = 'Mostrar Lista'
    Enabled = False
    TabOrder = 3
    OnClick = btnCriaListaClick
  end
  object strgrdTokens: TStringGrid
    Left = 0
    Top = 184
    Width = 273
    Height = 209
    ColCount = 3
    DefaultRowHeight = 16
    FixedCols = 0
    RowCount = 3
    ScrollBars = ssVertical
    TabOrder = 4
    ColWidths = (
      95
      31
      124)
  end
  object btnSair: TButton
    Left = 281
    Top = 368
    Width = 105
    Height = 25
    Caption = 'Sai&r'
    TabOrder = 5
    OnClick = btnSairClick
  end
end

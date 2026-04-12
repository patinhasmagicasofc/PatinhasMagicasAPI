# MER e DER - Patinhas Magicas API

## MER

Modelo entidade-relacionamento conceitual com foco nas entidades principais do dominio:

- `TipoUsuario` classifica `Usuario` em uma relacao `1:N`.
- `Usuario` possui `0:1 Endereco`.
- `Usuario` possui `1:N Animal`.
- `Usuario` realiza `1:N Pedido`.
- `Usuario` possui `1:N PushSubscription`.
- `Usuario` possui `1:N PasskeyCredential`.
- `Especie` classifica `1:N Animal`.
- `TamanhoAnimal` classifica `1:N Animal`.
- `Categoria` classifica `1:N Produto`.
- `Especie` classifica `1:N Produto`.
- `StatusPedido` classifica `1:N Pedido`.
- `Pedido` possui `1:N ItemPedido`.
- `Produto` participa de `1:N ItemPedido`.
- `Pedido` possui `1:N Pagamento`.
- `TipoPagamento` classifica `1:N Pagamento`.
- `StatusPagamento` classifica `0:N Pagamento`.
- `Pedido` possui `1:N Agendamento`.
- `Animal` possui `1:N Agendamento`.
- `StatusAgendamento` classifica `1:N Agendamento`.
- `TipoServico` classifica `1:N Servico`.
- `Servico` e `TamanhoAnimal` se relacionam por `ServicoTamanho`, formando uma associacao `N:N` com atributo `Preco`.
- `Agendamento` e `Servico` se relacionam por `AgendamentoServico`, formando uma associacao `N:N` com atributo `Preco`.

## DER

```mermaid
erDiagram
    TIPO_USUARIO ||--o{ USUARIO : classifica
    USUARIO ||--o| ENDERECO : possui
    USUARIO ||--o{ ANIMAL : possui
    USUARIO ||--o{ PEDIDO : realiza
    USUARIO ||--o{ PUSH_SUBSCRIPTION : registra
    USUARIO ||--o{ PASSKEY_CREDENTIAL : possui

    ESPECIE ||--o{ ANIMAL : classifica
    TAMANHO_ANIMAL ||--o{ ANIMAL : classifica

    CATEGORIA ||--o{ PRODUTO : agrupa
    ESPECIE ||--o{ PRODUTO : destino

    STATUS_PEDIDO ||--o{ PEDIDO : define
    PEDIDO ||--o{ ITEM_PEDIDO : contem
    PRODUTO ||--o{ ITEM_PEDIDO : compoe

    PEDIDO ||--o{ PAGAMENTO : recebe
    TIPO_PAGAMENTO ||--o{ PAGAMENTO : tipo
    STATUS_PAGAMENTO o|--o{ PAGAMENTO : status

    PEDIDO ||--o{ AGENDAMENTO : origina
    ANIMAL ||--o{ AGENDAMENTO : referencia
    STATUS_AGENDAMENTO ||--o{ AGENDAMENTO : status

    TIPO_SERVICO ||--o{ SERVICO : classifica
    SERVICO ||--o{ SERVICO_TAMANHO : precifica
    TAMANHO_ANIMAL ||--o{ SERVICO_TAMANHO : varia_por

    AGENDAMENTO ||--o{ AGENDAMENTO_SERVICO : contem
    SERVICO ||--o{ AGENDAMENTO_SERVICO : executa
    SERVICO_TAMANHO o|--o{ AGENDAMENTO_SERVICO : referencia

    USUARIO {
        int Id PK
        string Nome
        string CPF
        string Email
        string Senha
        int Ddd
        string Telefone
        datetime DataCadastro
        bool Ativo
        int TipoUsuarioId FK
    }

    ENDERECO {
        int Id PK
        string Logradouro
        int Numero
        string Bairro
        string Cidade
        string Estado
        string CEP
        string Complemento
        int UsuarioId FK
    }

    TIPO_USUARIO {
        int Id PK
        string Nome
    }

    ANIMAL {
        int Id PK
        string Nome
        string Raca
        int Idade
        string FotoDataUrl
        int EspecieId FK
        int TamanhoAnimalId FK
        int UsuarioId FK
    }

    ESPECIE {
        int Id PK
        string Nome
    }

    TAMANHO_ANIMAL {
        int Id PK
        string Nome
    }

    PEDIDO {
        int Id PK
        datetime DataPedido
        int UsuarioId FK
        int StatusPedidoId FK
    }

    STATUS_PEDIDO {
        int Id PK
        string Nome
    }

    ITEM_PEDIDO {
        int Id PK
        int Quantidade
        decimal PrecoUnitario
        int ProdutoId FK
        int PedidoId FK
    }

    PRODUTO {
        int Id PK
        string Nome
        int EspecieId FK
        decimal Preco
        string Marca
        string UrlImagem
        string Codigo
        string Descricao
        string DescricaoDetalhada
        date Validade
        int CategoriaId FK
    }

    CATEGORIA {
        int Id PK
        string Nome
    }

    PAGAMENTO {
        int Id PK
        datetime DataPagamento
        decimal Valor
        string Observacao
        int StatusPagamentoId FK
        int TipoPagamentoId FK
        int PedidoId FK
    }

    TIPO_PAGAMENTO {
        int Id PK
        string Nome
    }

    STATUS_PAGAMENTO {
        int Id PK
        string Nome
    }

    AGENDAMENTO {
        int Id PK
        datetime DataAgendamento
        datetime DataCadastro
        int PedidoId FK
        int AnimalId FK
        int StatusAgendamentoId FK
    }

    STATUS_AGENDAMENTO {
        int Id PK
        string Nome
    }

    SERVICO {
        int Id PK
        string Nome
        string Descricao
        string DescricaoDetalhada
        int TempoEstimadoMinutos
        int TipoServicoId FK
    }

    TIPO_SERVICO {
        int Id PK
        string Nome
    }

    SERVICO_TAMANHO {
        int Id PK
        decimal Preco
        int ServicoId FK
        int TamanhoAnimalId FK
    }

    AGENDAMENTO_SERVICO {
        int Id PK
        decimal Preco
        int AgendamentoId FK
        int ServicoId FK
        int ServicoTamanhoId FK
    }

    PUSH_SUBSCRIPTION {
        int Id PK
        int UsuarioId FK
        string Endpoint UK
        string P256DH
        string Auth
        datetime DataCadastro
        datetime UltimoEnvioEm
    }

    PASSKEY_CREDENTIAL {
        int Id PK
        int UsuarioId FK
        string CredentialId UK
        string UserHandle
        string PublicKey
        long SignatureCounter
        string FriendlyName
        string CredType
        string AaGuid
        string Transports
        datetime CreatedAt
        datetime LastUsedAt
    }
```

## Observacoes

- O projeto usa PostgreSQL com Entity Framework Core.
- `Endereco.UsuarioId` e uma chave estrangeira com indice unico, representando relacao `1:1` com `Usuario`.
- `StatusPagamentoId` em `Pagamento` e opcional no modelo atual.
- O snapshot do EF indica um relacionamento adicional opcional de `AgendamentoServico` para `ServicoTamanho`, mas essa navegacao nao aparece explicitamente no modelo `AgendamentoServico.cs`.

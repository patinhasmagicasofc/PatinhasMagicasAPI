# MER - Patinhas Magicas API

## Entidades

- `Usuario`: armazena os dados cadastrais do cliente ou administrador.
- `TipoUsuario`: define o perfil de acesso do usuario.
- `Endereco`: representa o endereco principal vinculado ao usuario.
- `Animal`: representa o pet pertencente ao usuario.
- `Especie`: classifica o animal e tambem os produtos destinados a ele.
- `TamanhoAnimal`: classifica o porte do animal.
- `Pedido`: registra a compra feita por um usuario.
- `StatusPedido`: define a situacao atual do pedido.
- `ItemPedido`: representa cada item incluido no pedido.
- `Produto`: representa os itens comercializados pela plataforma.
- `Categoria`: classifica os produtos.
- `Pagamento`: registra os pagamentos associados a um pedido.
- `TipoPagamento`: define a forma de pagamento utilizada.
- `StatusPagamento`: define a situacao do pagamento.
- `Agendamento`: representa o agendamento de servicos para um animal.
- `StatusAgendamento`: define a situacao do agendamento.
- `Servico`: representa os servicos oferecidos.
- `TipoServico`: classifica os servicos.
- `ServicoTamanho`: associa servico e tamanho do animal, guardando o preco praticado.
- `AgendamentoServico`: associa os servicos efetivamente incluidos em um agendamento.
- `PushSubscription`: guarda inscricoes de notificacao web do usuario.
- `PasskeyCredential`: guarda credenciais de autenticacao sem senha do usuario.

## Relacionamentos

- `TipoUsuario` 1:N `Usuario`
- `Usuario` 1:1 `Endereco`
- `Usuario` 1:N `Animal`
- `Usuario` 1:N `Pedido`
- `Usuario` 1:N `PushSubscription`
- `Usuario` 1:N `PasskeyCredential`
- `Especie` 1:N `Animal`
- `TamanhoAnimal` 1:N `Animal`
- `Categoria` 1:N `Produto`
- `Especie` 1:N `Produto`
- `StatusPedido` 1:N `Pedido`
- `Pedido` 1:N `ItemPedido`
- `Produto` 1:N `ItemPedido`
- `Pedido` 1:N `Pagamento`
- `TipoPagamento` 1:N `Pagamento`
- `StatusPagamento` 1:N `Pagamento`, sendo opcional do lado de `Pagamento`
- `Pedido` 1:N `Agendamento`
- `Animal` 1:N `Agendamento`
- `StatusAgendamento` 1:N `Agendamento`
- `TipoServico` 1:N `Servico`
- `Servico` N:N `TamanhoAnimal` por meio de `ServicoTamanho`
- `Agendamento` N:N `Servico` por meio de `AgendamentoServico`
- `ServicoTamanho` 1:N `AgendamentoServico`, de forma opcional no modelo persistido

## Regras de negocio refletidas no modelo

- Um usuario pode existir sem endereco cadastrado.
- Um pedido sempre pertence a um usuario e possui um status.
- Um pagamento sempre pertence a um pedido e a um tipo de pagamento.
- Um pagamento pode existir sem status informado no banco atual.
- Um agendamento sempre pertence a um pedido, a um animal e a um status de agendamento.
- O preco de um servico pode variar conforme o tamanho do animal.
- O item `AgendamentoServico` registra o preco aplicado no momento do agendamento.

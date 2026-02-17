# 📌 ReqFy

API REST desenvolvida em .NET para controle de solicitações internas.

O objetivo do projeto é simular um fluxo corporativo real de solicitações,
aplicando regras de negócio, controle de estado e separação de responsabilidades
em uma arquitetura em camadas.

---

# 🧠 Objetivo do Projeto

Treinar:

- Modelagem de domínio
- Aplicação de regras de negócio
- Separação de responsabilidades (Controller / Service / Repository)
- Controle de máquina de estados
- Uso correto de status HTTP
- Uso de DTOs para entrada e saída

---

# 🏗 Arquitetura

A aplicação foi estruturada em camadas:

- Controller → Responsável por lidar com HTTP
- Service → Responsável pelas regras de negócio
- Context (Repository) → Persistência de dados
- DTOs → Controle de entrada e saída
- Entity → Modelo de domínio (Solicitacao)

---

# 📦 Entidade Principal

## Solicitacao

| Campo | Descrição |
|-------|-----------|
| Id | Identificador único |
| Descricao | Descrição da solicitação |
| Tipo | Tipo da solicitação |
| Status | Estado atual da solicitação |
| DataCriacao | Data de criação |
| DataAtualizacao | Data da última atualização |

---

# 📌 Regras de Negócio

## ✅ Regra 1 — Criação

- Toda solicitação nasce com:
  - Status = "Aberta"
  - DataCriacao preenchida automaticamente
- O usuário NÃO pode informar o status na criação
- DataAtualizacao inicia como nula

---

## ✅ Regra 2 — Atualização de Dados

- Apenas a **Descrição** pode ser alterada
- O **Tipo não pode ser alterado**
- A cada atualização válida:
  - DataAtualizacao é preenchida automaticamente

---

## ✅ Regra 3 — Controle de Fluxo de Status (Máquina de Estados)

A solicitação segue um fluxo de estados controlado, garantindo integridade do processo e rastreabilidade das decisões.

### Fluxo permitido:

Aberta → Em Analise → Aprovada  
                         ↘  
                          Reprovada

---

### Regras aplicadas:

1. Não é permitido pular etapas.
   - Exemplo inválido: Aberta → Aprovada

2. Não é permitido retornar para um status anterior.
   - Exemplo inválido: Em Analise → Aberta

3. Após atingir um estado final (Aprovada ou Reprovada),
   a solicitação torna-se imutável em relação ao status.

4. Atualizações redundantes (mesmo status atual) não são permitidas.

---

### Tratamento de Erros

- Transições inválidas retornam **400 - BadRequest**
- Solicitações inexistentes retornam **404 - NotFound**

---

### Objetivo da Regra

Essa regra implementa uma máquina de estados explícita, impedindo inconsistências
no fluxo da solicitação e garantindo que o histórico do processo seja preservado.

O controle de transição é realizado na camada de Service,
protegendo o domínio contra alterações indevidas.


---

# 🌐 Endpoints

## 🔹 Criar Solicitação

POST /Solicitacao

Cria uma nova solicitação.

### Entrada:
- Descricao
- Tipo

### Resposta:
- 201 Created
- Retorna os dados completos da solicitação criada

---

## 🔹 Listar Todas as Solicitações

GET /Solicitacao

Retorna todas as solicitações cadastradas.

### Resposta:
- 200 OK
- Lista de solicitações (lista pode estar vazia)

---

## 🔹 Atualizar Descrição

PUT /Solicitacao/{id}

Atualiza apenas a descrição da solicitação.

### Regras:
- Não altera Tipo
- Atualiza DataAtualizacao automaticamente

### Resposta:
- 200 OK
- 404 NotFound (caso id não exista)

---

## 🔹 Atualizar Status

PUT /Solicitacao/{id}/status

Atualiza o status da solicitação respeitando o fluxo permitido.

### Regras:
- Transições inválidas retornam 400
- Solicitação inexistente retorna 404
- Atualiza DataAtualizacao automaticamente

---

# 🚦 Status HTTP Utilizados

| Código | Quando é utilizado |
|--------|-------------------|
| 200 OK | Operação realizada com sucesso |
| 201 Created | Recurso criado com sucesso |
| 400 BadRequest | Violação de regra de negócio |
| 404 NotFound | Recurso não encontrado |

---

# 🎯 Considerações Técnicas

- Uso de DTOs específicos para cada operação
- Regras de negócio centralizadas na camada de Service
- Separação clara entre entrada e saída de dados
- Controle explícito de transição de estados
- Proteção do domínio contra alterações indevidas

---

# 🚀 Próximas Evoluções (Possíveis Melhorias)

- Substituir string de Status por enum
- Implementar tratamento global de exceções
- Adicionar paginação no endpoint GET
- Implementar testes unitários
- Implementar autenticação/autorização

---

# 📌 Autor

Projeto desenvolvido com foco em aprendizado e evolução em backend .NET.

# BestShop API 🛒🛍️

Bem-vindo à API do BestShop! Esta API fornece endpoints para gerenciar produtos e fornecedores para o aplicativo BestShop.

## Sumário
- [Técnologias Utilizadas](#tecnologias)

- [API de Produto](#api-de-produto)
  - [Obter Todos os Produtos](#obter-todos-os-produtos)
  - [Obter Produto por ID](#obter-produto-por-id)
  - [Adicionar um Novo Produto](#adicionar-um-novo-produto)
  - [Excluir um Produto](#excluir-um-produto)
  - [Atualizar um Produto](#atualizar-um-produto)
  - [Limpar Todos os Produtos](#limpar-todos-os-produtos)

- [API de Fornecedor](#api-de-fornecedor)
  - [Obter Todos os Fornecedores](#obter-todos-os-fornecedores)
  - [Obter Fornecedor por ID](#obter-fornecedor-por-id)
  - [Adicionar um Novo Fornecedor](#adicionar-um-novo-fornecedor)
  - [Excluir um Fornecedor](#excluir-um-fornecedor)
  - [Atualizar um Fornecedor](#atualizar-um-fornecedor)
  - [Limpar Todos os Fornecedores](#limpar-todos-os-fornecedores)

## Técnologias Utilizadas
  - .NET;
  - SQL Server;
  - Dapper MicroORM;
  - API Rest;
  - Princípios de SOLID;
  - Muita paciência 💙;

## API de Produto

### Obter Todos os Produtos

```http
GET /api/product/
```
Obter uma lista de todos os produtos.

### Obter Produto por ID

```http
GET /api/product/{id}
```
Obter detalhes do produto por ID.

### Adicionar um Novo Produto

```http
POST /api/product/
```

### Excluir um Produto

```http
DELETE /api/product/{id}
```
Excluir um produto por ID.

### Atualizar um Produto

```http
PUT /api/product/{id}
```
Atualizar detalhes do produto por ID.

### Limpar Todos os Produtos

```http
PUT /api/product/{id}
```
Excluir todos os produtos.

## API de Fornecedor

### Obter Todos os Fornecedores

```http
GET /api/supplier
```
Obter uma lista de todos os fornecedores

### Obter Fornecedor por ID

```http
GET /api/supplier/{id}
```
Obter detalhes do fornecedor por ID.

## Adicionar um Novo Fornecedor

```http
POST /api/supplier
```
Adicionar um novo fornecedor.

## Excluir um Fornecedor

```http
DELETE /api/supplier/{id}
```
Excluir um fornecedor por ID.

## Atualizar um Fornecedor

```http
PUT /api/supplier/{id}
```
Atualizar detalhes do fornecedor por ID.

## Limpar Todos os Fornecedores

```http
DELETE /api/supplier/clear
```
Excluir todos os fornecedores.

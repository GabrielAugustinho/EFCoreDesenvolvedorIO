using System;
using System.Collections.Generic;
using System.Linq;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var existe = db.Database.GetPendingMigrations().Any();
            if (existe)
            {

            }*/
            //InserirDados();
            //InserirDadosEmMassa();
            //ConsultarDados();
            //CadastrarPedido();
            //ConsultarPedidoCarregamentoAdiantado();
            //AtualizarDados();
        }

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicaionContext();
            //var cliente = db.Clientes.Find(1);
            var cliente = new Cliente
            {
                Id = 1
            };

            db.Entry(cliente).State = EntityState.Deleted;
            //db.Remove(cliente);
            //db.Clientes.Remove(cliente);
            db.SaveChanges();
        }

        public static void AtualizarDados()
        {
            using var db = new Data.ApplicaionContext();
            /*var cliente = db.Clientes.Find(1);
            cliente.Nome = "Gabriel Atualizado";*/

            var cliente = new Cliente
            {
                Id = 1
            };

            var clienteDesconectado = new
            {
                Nome = "Cliente Desconectado",
                Telefone = "999999999"
            };

            // Totalmente desconectado do banco
            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            //db.Entry(cliente).State = EntityState.Modified;

            //db.Clientes.Update(cliente);
            db.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicaionContext();
            var pedidos = db.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(p => p.Produto)
                 .ToList();
        }

        public static void ConsultarDados()
        {
            using var db = new Data.ApplicaionContext();
            // Duas formas de consultas
            var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodo = db.Clientes
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
            foreach (var cliente in consultaPorMetodo)
            {
                // Consulta em memória
                db.Clientes.Find(cliente.Id);
                // Consulta diretamente no banco
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }

        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicaionContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                StatusPedido = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10
                    }
                }
            };

            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Gabriel Augustinho Alves",
                CEP = "999999999",
                Cidade = "Taubaté",
                Estado = "SP",
                Telefone = "12 996579079"
            };

            var listaClientes = new[]
            {
               new Cliente
               {
                    Nome = "Teste 1",
                    CEP = "999999999",
                    Cidade = "Taubaté",
                    Estado = "SP",
                    Telefone = "12 996579079"
               },
               new Cliente
               {
                    Nome = "Teste 2",
                    CEP = "999999999",
                    Cidade = "Taubaté",
                    Estado = "SP",
                    Telefone = "12 996579079"
               }
            };

            using var db = new Data.ApplicaionContext();
            //db.AddRange(produto, cliente);
            db.Set<Cliente>().AddRange(listaClientes);
            //db.Clientes.AddRange(listaClientes);

            var registros = db.SaveChanges();
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            // Metodos de rastreamento para incersão
            using var db = new Data.ApplicaionContext();
            db.Produtos.Add(produto);
            /* db.Set<Produto>().Add(produto);
            db.Entry(produto).State = EntityState.Added;
            db.Add(produto);*/

            // Para o dado ir ao banco
            var registros = db.SaveChanges();
        }
    }
}

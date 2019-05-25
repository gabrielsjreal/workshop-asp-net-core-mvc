using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VendasWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        // Código para formatação do E-mail
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //Comando para definir o que vai aparecer na tela, se retirar esse código, o atributo será exibido sem espaço
        [Display(Name = "Data de Nascimento")]
        //Código para formatar a entrada da data - Sem o código antes tinhas que colocar as horas e minutos também
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataDeNascimento { get; set; }
   

        //Comando para definir o que vai aparecer na tela, se retirar esse código, o atributo será exibido sem espaço
        [Display(Name = "Salário Base")]
        //Código para definir a quantidade de casas decimais
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double SalarioBase { get; set; }


        public Departamento Departamento { get; set; }
        public int DepartamentoId { get; set; }

        public ICollection<HistoricoDeVendas> VendasRealizadas { get; set; } = new List<HistoricoDeVendas>();

        public Vendedor()
        {
        }

        public Vendedor(int id, string nome, string email, DateTime dataDeNascimento, double salarioBase, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataDeNascimento = dataDeNascimento;
            SalarioBase = salarioBase;
            Departamento = departamento;
        }

        public void adicionarVenda(HistoricoDeVendas venda)
        {
            VendasRealizadas.Add(venda);
        }

        public void removerVenda(HistoricoDeVendas venda)
        {
            VendasRealizadas.Remove(venda);
        }

        public double totalDeVendas(DateTime dataInicial, DateTime dataFinal)
        {
            return this.VendasRealizadas.Where(hv => hv.Data == dataInicial && hv.Data == dataFinal)
                .Sum(hv => hv.Valor);
        
        }
    }
}

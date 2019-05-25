using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VendasWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        //Comando 'REQUIRED' - Campo Obrigatório
        [Required(ErrorMessage ="{0} Obrigatório")]
        //Comandos a seguir é para definir o tamando e exibir uma mensagem, caso não atenda ao critérios
        // 0 - Nome, 1 - Tamanho máximo, 2 - Tamanho mínimo
        [StringLength(60, MinimumLength =4, ErrorMessage ="O Tamanho do {0} deve ser entre {2} e {1}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [EmailAddress(ErrorMessage ="Digite um E-mail Válido")]
        // Código para formatação do E-mail
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        //Comando para definir o que vai aparecer na tela, se retirar esse código, o atributo será exibido sem espaço
        [Display(Name = "Data de Nascimento:")]
        //Código para formatar a entrada da data - Sem o código antes tinhas que colocar as horas e minutos também
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataDeNascimento { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [Range(100.0, 50000.0, ErrorMessage ="{0} tem que está entre {1} e {2}")]
        //Comando para definir o que vai aparecer na tela, se retirar esse código, o atributo será exibido sem espaço
        [Display(Name = "Salário Base:")]
        //Código para definir a quantidade de casas decimais
        [DisplayFormat(DataFormatString = "{0:F2}")]

        public double SalarioBase { get; set; }

        [Display(Name = "Departamento onde Trabalha:")]
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

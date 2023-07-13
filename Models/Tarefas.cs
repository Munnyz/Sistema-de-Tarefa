using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyTarefas.Models
{
    public class Tarefas
    {
        public int Id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        public string Title { get; set; }

        [DisplayName("Concluído")]
        public bool Done { get; set; }

        [DisplayName("Criado em")]
        public DateTime CreateAt { get; set; } = DateTime.Now;

        [DisplayName("Ultima atualização")]
        public DateTime LastUpdate { get; set; } = DateTime.Now;

        [DisplayName("Usuario")]
        public string User { get; set; }
    }
}

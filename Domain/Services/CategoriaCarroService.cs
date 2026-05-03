using Domain.Interfaces.InterfaceCategoriaCarro;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Notifications;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CategoriaCarroService : ICategoriaCarroService
    {
        private readonly ICategoriaCarro _ICategoriaCarro;

        public CategoriaCarroService(ICategoriaCarro ICategoriaCarro)
        {
            _ICategoriaCarro = ICategoriaCarro;
        }

        private async Task<bool> ValidarCategoria(CategoriaCarro categoria, int? idIgnorar = null)
        {
            var validaNome = categoria.ValidarString(categoria.Nome, "Nome");
            var validaDescricao = categoria.ValidarString(categoria.Descricao, "Descricao", obrigatorio: false);
            var validaValorDiaria = categoria.ValidarDecimal(categoria.ValorDiaria, "ValorDiaria", minimo: 0.01m);

            if (validaNome && await _ICategoriaCarro.NomeJaExiste(categoria.Nome, idIgnorar))
                categoria.Notificacoes.Add(new Notifies { NomePropriedade = "Nome", Mensagem = "Nome já cadastrado" });

            // Impede inativar categoria que tenha carros ativos vinculados
            if (!categoria.Ativo)
            {
                var temCarrosAtivos = await _ICategoriaCarro.TemCarrosAtivosVinculados(categoria.Id);
                if (temCarrosAtivos)
                    categoria.Notificacoes.Add(new Notifies { NomePropriedade = "Ativo", Mensagem = "Não é possível inativar uma categoria que possui carros ativos vinculados" });
            }

            return validaNome && validaDescricao && validaValorDiaria && !categoria.Notificacoes.Any();
        }

        public async Task AddCategoria(CategoriaCarro categoria)
        {
            if (await ValidarCategoria(categoria))
                await _ICategoriaCarro.Add(categoria);
        }

        public async Task UpdateCategoria(CategoriaCarro categoria)
        {
            if (await ValidarCategoria(categoria, categoria.Id))
                await _ICategoriaCarro.Update(categoria);
        }

        public async Task DeleteCategoria(CategoriaCarro categoria)
        {
            var temCarros = await _ICategoriaCarro.TemCarrosVinculados(categoria.Id);
            if (temCarros)
            {
                categoria.Notificacoes.Add(new Notifies { NomePropriedade = "Cancelado", Mensagem = "Não é possível excluir esta categoria pois existem carros vinculados." });
                return;
            }
            await _ICategoriaCarro.Delete(categoria);
        }
    }
}
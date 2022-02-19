using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ApiKPIs.Controllers
{
    [EnableCors("LiberarTudo")]
    [ApiController]
    public class KpiController : ControllerBase
    {
        private readonly Conexoes.SqlServer _sql;

        public KpiController()
        {
            _sql = new Conexoes.SqlServer();
        }

        [HttpPost("v1/InserirKPI")]
        public IActionResult InserirKPI(Entidades.Kpi kpi)
        {

            if (!Utils.Validacao.IsNome(kpi.Nome))
            {
                return StatusCode(400, "Nome inválido! Mínimo 5 caracteres, máximo 255");
            }

            if (!Utils.Validacao.IsUnidadeMedida(kpi.UnidadeMedida))
            {
                return StatusCode(400, "Unidade de Medida inválida! Mínimo 1 caracter, máximo 255");
            }

            try
            {
                _sql.InserirKPI(kpi);
            }

            catch (InvalidOperationException)
            {
                return StatusCode(400, "Dados incorretos!");
            }

            catch (Exception)
            {
                return StatusCode(500, "Algo deu errado!");
            }
            return StatusCode(200, "KPI inserida com sucesso!");
        }

        [HttpPut("v1/AtualizarKPI")]
        public IActionResult AtualizarKPI(Entidades.Kpi kpi)
        {
            if (!Utils.Validacao.IsNome(kpi.Nome))
            {
                return StatusCode(400, "Nome inválido! Mínimo 5 caracteres, máximo 255");
            }

            if (!Utils.Validacao.IsUnidadeMedida(kpi.UnidadeMedida))
            {
                return StatusCode(400, "Unidade de Medida inválida! Mínimo 1 caracter, máximo 255");
            }

            try
            {
                _sql.AtualizarKPI(kpi);
            }
            catch (InvalidOperationException)
            {
                return StatusCode(400, "KPI não encontrada!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo deu errado!");
            }
            return StatusCode(200, "KPI atualizada com sucesso!");
        }
        [HttpDelete("v1/DeletarKPI")]
        public IActionResult DeletarKPI(string nome)
        {
            try
            {
                _sql.DeletarKPI(nome);
            }


            catch (InvalidOperationException)
            {
                return StatusCode(400, "KPI não encontrada!");
            }

            catch (System.Data.SqlClient.SqlException)
            {
                return StatusCode(400, "Insira o nome da KPI!");
            }


            return StatusCode(200, "KPI deletada com sucesso!");
        }
        [HttpGet("v1/SelecionarKPI")]
        public IActionResult SelecionarKPI(string nome)
        {
            Entidades.Kpi kpi;
            try
            {
                kpi = _sql.SelecionarKPI(nome);
            }


            catch (InvalidOperationException)
            {
                return StatusCode(400, "KPI não encontrada!");
            }

            catch (System.Data.SqlClient.SqlException)
            {
                return StatusCode(400, "Insira o nome da KPI!");
            }

            return StatusCode(200, kpi);
        }
        [HttpGet("v1/ListarKPIs")]
        public List<Entidades.Kpi> ListarKPIs()
        {
            var kpis = _sql.ListarKPIS();
            return kpis;

        }
    }
}


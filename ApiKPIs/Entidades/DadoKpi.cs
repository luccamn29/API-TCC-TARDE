using System;

namespace ApiKPIs.Entidades
{
    public class DadoKpi
    {
        public int ID { get; set; }
        public string KPI { get; set; }
        public DateTime MesReferencia { get; set; }
        public decimal ValorEsperado { get; set; }
        public decimal ValorObtido { get; set; }

    }
}

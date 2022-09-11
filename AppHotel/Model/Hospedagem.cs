using System;
using System.Collections.Generic;
using System.Text;

using System.Transactions;

namespace AppHotel.Model
{
    internal class Hospedagem
    {

        public CategoriaQuarto quarto { get; set; }
        public int qnt_adulto { get; set; }
        public int qnt_crianca { get; set; }
        public int qnt_dias { get; set; }
        public DateTime check_in { get; set; }
        public DateTime check_out { get; set; }
        public double valor_total { get; set; }

        /*
         *  Calcula e retorna a diferença em dias da data de check_in e a de check_out
        */
        public static int CalcularTempoEstadia(DateTime checkin, DateTime checkout)
        {
            int dias = checkout.Subtract(checkin).Days;
            return dias;
        }

    }
}

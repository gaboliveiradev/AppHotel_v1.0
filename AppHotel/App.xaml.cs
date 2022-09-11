using System;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppHotel
{
    public partial class App : Application
    {
        /*
         * Instanciando a lista de quartos disponiveis no hotel
        */
        public List<Model.CategoriaQuarto> quartos = new List<Model.CategoriaQuarto>()
        {
            new Model.CategoriaQuarto()
            {
                descricao = "Suíte Premium",
                valor_diaria_adulto = 200.0,
                valor_diaria_crianca = 100.0
            },

            new Model.CategoriaQuarto()
            {
                descricao = "Suíte Confort Premium",
                valor_diaria_adulto = 420.0,
                valor_diaria_crianca = 210.0
            },

            new Model.CategoriaQuarto()
            {
                descricao = "Suíte Deluxe Imperial",
                valor_diaria_adulto = 700.0,
                valor_diaria_crianca = 350.0
            }
        };
        public App()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            MainPage = new NavigationPage(new View.OrcamentoHospedagem() );
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppHotel.Model;

namespace AppHotel.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrcamentoHospedagem : ContentPage
    {
        App PropriedadesApp;

        public OrcamentoHospedagem()
        {
            InitializeComponent();

            PropriedadesApp = (App)Application.Current;

            /*
             * Abastecendo o picker de quartos, com os valores definidos no array
             * de objetos lá do App.xaml.cs
            */
            pck_quarto.ItemsSource = PropriedadesApp.quartos;

            /* 
             * Definimos os valores máximos e mínimos das datas. No mínimo para o cliente não 
             * fazer CheckIn no passado, e no máximo para agendar daqui 6 meses.
            */
            dtpck_checkin.MinimumDate = DateTime.Now;
            dtpck_checkin.MaximumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 6, DateTime.Now.Day);

            /* 
             * No checkout temos que definir que o cliente irá sair pelo menos após uma diária.
            */
            dtpck_checkout.MinimumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);
            dtpck_checkout.MaximumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 6, DateTime.Now.Day);
        }

        private async void btn_calcular_estadia(object sender, EventArgs e)
        {
            try
            {
                int qnt_adulto = Convert.ToInt32(lbl_qnt_adultos.Text);
                int qnt_crianca = Convert.ToInt32(lbl_qnt_crianca.Text);

                if (qnt_adulto == 0 || qnt_crianca == 0) throw new Exception("Desculpe, informe pelo menos um adulto ou uma criança.");

                Model.CategoriaQuarto quarto_selecionado = (Model.CategoriaQuarto)pck_quarto.SelectedItem;

                if (quarto_selecionado == null) throw new Exception("Porfavor, selecione o quarto em que deseja ficar.");

                /* 
                 * Criando um objeto hospedagem que conterá todos os dados relativo
                 * a hospedagem e calculará o valor de acordo com o quarto.
                */
                Model.Hospedagem dados_hospedagem = new Model.Hospedagem()
                {
                    quarto = quarto_selecionado,
                    qnt_adulto = qnt_adulto,
                    qnt_crianca = qnt_crianca,
                    qnt_dias = Model.Hospedagem.CalcularTempoEstadia(dtpck_checkin.Date, dtpck_checkout.Date),
                    check_in = dtpck_checkin.Date,
                    check_out = dtpck_checkout.Date,
                };

                dados_hospedagem.valor_total = dados_hospedagem.CalcularValorEstadia();

                /*
                 * Cria uma instancia de página que mostra os totais, adiciona o modelo Hospedagem ao BindContext
                 * da página para que as informações referentes a estadia estejam disponíveis na outra página, resumindo
                 * calculamos os dados aqui e enviamos para outra página.
                */
                var extratoHospedagem = new ExtratoHospedagem();
                extratoHospedagem.BindingContext = dados_hospedagem;

                await Navigation.PushAsync(extratoHospedagem);

            } catch (Exception err)
            {
                await DisplayAlert("OPS!", err.Message, "Fechar");
            }
        }
    }
}
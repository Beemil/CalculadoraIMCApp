using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CalculadoraIMCApp.Models;
using System.Globalization;

namespace CalculadoraIMCApp.ViewModels
{
    public partial class IMCViewModel : ObservableObject
    {
        // Campos privados con guion bajo
        [ObservableProperty]
        private string _weightText;

        [ObservableProperty]
        private string _heightText;

        [ObservableProperty]
        private string _resultado;

        public IMCViewModel()
        {
            _resultado = string.Empty;
            _weightText = string.Empty;
            _heightText = string.Empty;
        }

        [RelayCommand]
        private void CalcularIMC()
        {
            var wText = (_weightText ?? string.Empty).Trim().Replace(',', '.');
            var hText = (_heightText ?? string.Empty).Trim().Replace(',', '.');

            if (double.TryParse(wText, NumberStyles.Any, CultureInfo.InvariantCulture, out double weight) &&
                double.TryParse(hText, NumberStyles.Any, CultureInfo.InvariantCulture, out double height))
            {
                if (height > 10) height /= 100; // Convertir cm a metros si es necesario

                if (weight <= 0 || height <= 0)
                {
                    Resultado = "Peso y altura deben ser mayores que cero.";
                    return;
                }

                IMCData imcData = new IMCData
                {
                    WeightKg = weight,
                    HeightMeters = height
                };

                double imc = imcData.WeightKg / (imcData.HeightMeters * imcData.HeightMeters);

                string clasif = imc < 18.5 ? "Bajo peso" :
                                imc < 25 ? "Normal" :
                                imc < 30 ? "Sobrepeso" :
                                "Obesidad";

                Resultado = $"IMC: {imc:F2} — {clasif}";
            }
            else
            {
                Resultado = "Ingresa valores numéricos válidos.";
            }
        }
    }
}

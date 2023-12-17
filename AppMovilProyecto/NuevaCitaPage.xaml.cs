using AppMovilProyecto.Models;
using AppMovilProyecto.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace AppMovilProyecto;

public partial class NuevaCitaPage : ContentPage
{

    private Cita _cita;
    private readonly APIService _APIService;

    public NuevaCitaPage(APIService apiservice)
    {
        InitializeComponent();
        _APIService = apiservice;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _cita = BindingContext as Cita;
        if (_cita != null)
        {
            Fecha.Text = _cita.Fecha;
            Hora.Text = _cita.Hora.ToString();
            Descripcion.Text = _cita.Descripcion;
            IdMedico.Text = _cita.IdMedico;
            IdUsuario.Text  = _cita.IdUsuario;
        }
    }

    private async void OnClickGuardarCita(object sender, EventArgs e)
    {
        // Obtén el IdUsuario como entero
        int idUsuarioInt = Preferences.Get("idusuario", 0);

        // Convierte el IdUsuario a cadena
        string idUsuario = idUsuarioInt.ToString();

        if (_cita != null)
        {
            _cita.Fecha = Fecha.Text;
            _cita.Descripcion = Descripcion.Text;
            _cita.Hora = Hora.Text;
            _cita.IdUsuario = IdUsuario.Text;
            _cita.IdMedico = IdMedico.Text;
            await _APIService.updateCita(_cita.IdCita, _cita);
        }
        else
        {
            Cita cita = new Cita
            {
                IdCita = 0,
                Fecha = Fecha.Text,
                Descripcion = Descripcion.Text,
                Hora = Hora.Text,
                IdMedico = IdMedico.Text,
                IdUsuario = idUsuario // Asigna directamente el IdUsuario obtenido de las preferencias
            };
            await _APIService.addCita(cita);
        }
        await Navigation.PopAsync();
    }

}

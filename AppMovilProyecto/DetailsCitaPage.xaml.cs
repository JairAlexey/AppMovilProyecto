using AppMovilProyecto.Models;
using AppMovilProyecto.Services;

namespace AppMovilProyecto;

public partial class DetailsCitaPage : ContentPage
{
    private Cita _cita;
    private readonly APIService _APIService;

    public DetailsCitaPage(APIService apiservice)
    {
        InitializeComponent();
        _APIService = apiservice;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _cita = BindingContext as Cita;
        Fecha.Text = _cita.Fecha;
        Hora.Text = _cita.Hora;
        Descripcion.Text = _cita.Descripcion;
        IdMedico.Text = _cita.IdMedico;
        IdUsuario.Text = _cita.IdUsuario;

    }
    private async void ClickEliminarProducto(object sender, EventArgs e)
    {
        //Utils.Utils.ListaProductos.Remove(_producto);
        await _APIService.deleteCita(_cita.IdCita);
        await Navigation.PopAsync();
    }

    private async void ClickEditarProducto(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NuevaCitaPage(_APIService)
        {
            BindingContext = _cita,
        });
    }
}
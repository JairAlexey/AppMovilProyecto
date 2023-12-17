using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using AppMovilProyecto.Models;
using AppMovilProyecto.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace AppMovilProyecto;


public partial class LoginPage : ContentPage
{
    private User _user;
    private readonly APIService _APIService;

    public LoginPage(APIService apiservice)
    {
        InitializeComponent();
        _APIService = apiservice;
    }


    private async void OnClickLogin(object sender, EventArgs e)
    {
        string correo = Correo.Text;
        string clave = Clave.Text;
        User user = new User
        {
            IdUsuario = 0,
            Correo = correo,
            Clave = clave
        };

        User userEncontrado = await _APIService.IniciarSesion(user);

        if (userEncontrado != null)
        {
            Preferences.Set("correo", correo);
            Preferences.Set("idusuario", userEncontrado.IdUsuario);
            await Navigation.PushAsync(new CitaPage(_APIService));
        }
        else
        {
            var toast = Toast.Make("Nombre de usuario o contraseña incorrecta", ToastDuration.Short, 14);
            await toast.Show();
        }
    }


}
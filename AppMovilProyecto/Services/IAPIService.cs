using AppMovilProyecto.Models;

namespace AppMovilProyecto.Services
{
    public interface IAPIService
    {
        Task<List<User>> getUsuario();
        Task<List<Cita>> getCita();
        Task<Cita> getCita(int IdCita);
        Task<Cita> getCita(string IdUsuario);
        Task<bool> addCita(Cita cita);
        Task<bool> updateCita(int IdCita, Cita cita);
        Task<bool> deleteCita(int IdCita);

        Task<bool> IniciarSesion(User user);

        Task<List<Medico>> getMedicos();

    }
}

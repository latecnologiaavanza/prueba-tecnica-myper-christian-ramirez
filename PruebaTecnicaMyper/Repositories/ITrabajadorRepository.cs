using PruebaTecnicaMyper.Models;

namespace PruebaTecnicaMyper.Repositories
{
    public interface ITrabajadorRepository
    {

        List<Trabajador> listar();
        List<Trabajador> listarPorSexo(string sexo);
        Trabajador obtener(int id);
        void crear(Trabajador trabajador);
        void eliminar(int id);
        void guardar();

    }
}

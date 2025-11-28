using MiWebAPI.Models;
namespace MiWebAPI.Interfaces
{
    public interface IPresupuestoRepository
    {
        void AltaPresupuesto(Presupuestos NuevoPresupuesto);
        List<Presupuestos> GetAll();
        Presupuestos GetDetallesById(int id);
        Presupuestos GetById(int id);
        void agregarAPresupuesto(int idPresupuesto, int idProducto, int cantidad);
        void DeleteById(int idPresupuesto);
        void ModificarById(Presupuestos presupuesto);
    }
}
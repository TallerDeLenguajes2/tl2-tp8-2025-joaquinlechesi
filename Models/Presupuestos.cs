namespace MiWebAPI
{
    public class Presupuestos
    {
        public int IdPresupuestos { get; set; }
        public string NombreDestinatario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<PresupuestosDetalle> Detalle;
        //Metodos
        public double MontoPresupuesto()
        {
            double resutado = 0;
            foreach (var detalle in Detalle)
            {
                resutado += detalle.Cantidad * detalle.productos.Precio;
            }
            return resutado;
        }
        public double MontoPresupuestoConIva()
        {
            return MontoPresupuesto() * 1.21;
        }
        public int CantidadProductos()
        {
            int cantidad = 0;
            foreach (var detalle in Detalle)
            {
                cantidad += detalle.Cantidad;
            }
            return cantidad;
        }
    }
}
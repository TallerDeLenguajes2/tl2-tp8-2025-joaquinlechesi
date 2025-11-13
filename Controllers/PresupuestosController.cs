using Microsoft.AspNetCore.Mvc;

public class PresupuestosController : Controller
{
    private PresupuestoRepository _presupuestoRepository;
    public PresupuestosController()
    {
        _presupuestoRepository = new PresupuestoRepository();
    }
    [HttpPost("postPresupuesto")]
    public IActionResult AltaPresupuesto(Presupuestos nuevoPresupuestos)
    {
        _presupuestoRepository.AltaPresupuesto(nuevoPresupuestos);
        return Ok(nuevoPresupuestos);
    }
    [HttpPost("postAgregarPresupuestoAPresupuestosDetalles")]
    public IActionResult AgregarPresupuestoAPresupuestosDetalles(int idPresupuesto, int idProducto, int cantidad)
    {
        _presupuestoRepository.agregarAPresupuesto(idPresupuesto, idProducto, cantidad);
        //verificar si exite el producto
        return Ok();
    }
    [HttpGet("getPresupuesto")]
    public IActionResult GetByid(int id)
    {
        _presupuestoRepository.GetById(id);
        return Ok();
    }
    [HttpGet("getPresupuestos")]
    public IActionResult GetAll()
    {
        var listaPresupuestos = _presupuestoRepository.GetAll();
        return Ok(listaPresupuestos);
    }
    [HttpGet("deletePresupuesto")]
    public IActionResult borrarPresuouesto(int id)
    {
        _presupuestoRepository.DeleteById(id);
        return Ok();
    }
    public IActionResult Index()
    {
        List<Presupuestos> presupuestos = _presupuestoRepository.GetAll();
        return View(presupuestos);
    }
}
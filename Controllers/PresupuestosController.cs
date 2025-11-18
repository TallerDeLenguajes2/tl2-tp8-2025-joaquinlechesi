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
        var _productoRepository = new ProductoRepository();
        var producto = _productoRepository.GetById(idProducto);
        var presupuesto = _presupuestoRepository.GetById(idProducto);
        if (presupuesto is null || producto is null) return Index(); //deberia impedir dar de alta un registro en PresupuestosDetalles si no existe el Presupuesto o el Producto
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
    [HttpGet]
    public ActionResult Index()
    {
        List<Presupuestos> presupuestos = _presupuestoRepository.GetAll();
        return View(presupuestos);
    }
    [HttpGet]
    public IActionResult Create()
    {
        var presupuestos = new Presupuestos();
        return View(presupuestos); //Funcionando
    }
    [HttpPost]
    public IActionResult Create(Presupuestos nuevoPresupuesto)
    {
        // var presupuestos = new Presupuestos
        // {
        //     NombreDestinatario = nuevoPresupuesto.NombreDestinatario,
        //     FechaCreacion = nuevoPresupuesto.FechaCreacion.Date
        // };
        //return View(presupuestos);
        _presupuestoRepository.AltaPresupuesto(nuevoPresupuesto);
        return RedirectToAction("Index"); //Funcionando
    }
    [HttpGet]
    public IActionResult Details(int id)
    {
        var presupuesto = _presupuestoRepository.GetDetallesById(id);
        if (presupuesto is null)
        {
            return RedirectToAction("Home");
        }
        return View(presupuesto);
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var presupuestoEditar = _presupuestoRepository.GetById(id);
        return View(presupuestoEditar);
    } //Funciona
    [HttpPost]
    public IActionResult Edit(Presupuestos presupuesto)
    {
        _presupuestoRepository.ModificarById(presupuesto);
        return RedirectToAction("Index"); // Funcionando
    }
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var presupuesto = _presupuestoRepository.GetById(id);
        if (presupuesto is null) return RedirectToAction("Index"); //Por precausion
        return View(presupuesto);
    }
    [HttpPost]
    public IActionResult Delete(Presupuestos presupuesto)
    {
        _presupuestoRepository.DeleteById(presupuesto.IdPresupuestos);
        return RedirectToAction("Presupuestos");
        //return View();
    }
}
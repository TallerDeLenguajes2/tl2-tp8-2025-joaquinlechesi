using Microsoft.AspNetCore.Mvc;

//[ApiController]
//[Route("[controller]")]
public class ProductosController : Controller
{
    private readonly ILogger<ProductosController> _logger;
    private ProductoRepository _productoRepository;
    public ProductosController()
    {
        _productoRepository = new ProductoRepository();
    }
    [HttpPost("postAgregarProducto")]
    public IActionResult PostAgregarPedido([FromBody] Productos NuevoProducto)
    {
        //cadeteria.AgregarPedido(NuevoPedido);
        //ADPedidos.Guardar(cadeteria.ListadoPedidos); //funciona
        _productoRepository.nuevoProducto(NuevoProducto);
        return Ok(NuevoProducto); //retorna el objeto NuevoPedido
    }
    [HttpPut("putModificarProductos")]
    public IActionResult PutProducto(int id, [FromBody] Productos NuevoProducto)
    {
        _productoRepository.modificarProducto(id, NuevoProducto);
        return Ok(NuevoProducto);
    }
    [HttpGet("getProductos")]
    public IActionResult GetAll()
    {
        var listaProductos = _productoRepository.GetAll();
        return Ok(listaProductos);
    }
    [HttpGet("getProducto")]
    public IActionResult GetById(int id)
    {
        var producto = _productoRepository.GetById(id);
        return Ok(producto);
    }
    [HttpGet("deleteBorrarProducto")]
    public IActionResult DeleteById(int id)
    {
        _productoRepository.DeleteById(id);
        return Ok();
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<Productos> productos = _productoRepository.GetAll();
        return View(productos);
    }
    [HttpGet]
    public IActionResult Create()
    {
        var producto = new Productos();
        return View(producto);
    }
    [HttpPost]
    public IActionResult Create(Productos nuevoProducto)
    {
        _productoRepository.nuevoProducto(nuevoProducto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var producto = _productoRepository.GetById(id);
        if (producto is null) RedirectToAction("Index");
        return View(producto); //Funciona
    }
    [HttpPost]
    public IActionResult Edit(Productos productoEditado)
    {
        _productoRepository.modificarProducto(productoEditado.IdProducto, productoEditado);
        return RedirectToAction("Index"); //Dirige a index //Funciona
    }
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var producto = _productoRepository.GetById(id);
        if (producto is null) return RedirectToAction("Index");
        return View();
    }
}
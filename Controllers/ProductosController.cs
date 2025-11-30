using MiWebAPI.Models;
using MiWebAPI.Interfaces;
using MiWebAPI.Repository.ProductoRepository;
using MiWebAPI.ViewModels.ProductoViewModel;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.ViewModels.AgregarProductoViewModel;

namespace MiWebAPI.Controllers;

//[ApiController]
//[Route("[controller]")]
public class ProductosController : Controller
{
    private readonly ILogger<ProductosController> _logger;
    private readonly IProductoRepository _productoRepository;
    private readonly IAuthenticationService _authService;
    public ProductosController(IProductoRepository productoRepository, IAuthenticationService authService)
    {
        _productoRepository = new ProductoRepository();
        _productoRepository = productoRepository;
        _authService = authService;
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
    private IActionResult CheckAdminPermissions() // Permite acceder solo si esta logueado y es administrador
    { // 1. No logueado? -> vuelve al login
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        } // 2. No es Administrador? -> Da Error
        if (!_authService.HasAccessLevel("Administrador"))
        { 
            // Llamamos a AccesoDenegado (llama a la vista correspondiente de Productos)
            return RedirectToAction("AccesoDenegado");
        }
        return null; // Permiso concedido
    }
    [HttpGet]
    public IActionResult Index()
    {
        // Aplicamos el chequeo de seguridad
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        List<Productos> productos = _productoRepository.GetAll();
        return View(productos);
    }
    [HttpGet]
    public IActionResult Create()
    {
        // Aplicamos el chequeo de seguridad
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        //var producto = new Productos();
        var producto = new CrearProductoViewModel();
        return View(producto); //Funcionando
    }
    [HttpPost]
    public IActionResult Create(CrearProductoViewModel nuevoProductoVM)
    {
        // Aplicamos el chequeo de seguridad
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        //transformar ese vm -> model
        var nuevoProducto = new Productos(nuevoProductoVM);
        if (nuevoProductoVM.Description is null) //Logica de negocio?
        {
            nuevoProducto.Description = "";
        }
        _productoRepository.nuevoProducto(nuevoProducto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        // Aplicamos el chequeo de seguridad
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        
        var producto = _productoRepository.GetById(id);
        if (producto is null) RedirectToAction("Index");
        var productoVM = new EditarProductoViewModel(producto);
        return View(productoVM); //Funciona
    }
    [HttpPost]
    public IActionResult Edit(EditarProductoViewModel productoEditadoVM)
    {
        // Aplicamos el chequeo de seguridad
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        
        if (!ModelState.IsValid) //Chequeo de validez
        {
            return RedirectToAction("Index"); //Falta retornar con el View(ViewModel)
        }
        //transformar ese vm -> model
        var productoEditado = new Productos(productoEditadoVM);
        _productoRepository.modificarProducto(productoEditado.IdProducto, productoEditado);
        return RedirectToAction("Index"); //Dirige a index //Funciona
    }
    [HttpGet]
    public IActionResult Delete(int id)
    {
        // Aplicamos el chequeo de seguridad
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        
        var producto = _productoRepository.GetById(id);
        if (producto is null) return RedirectToAction("Index");
        return View(producto); //Funcionando
    }
    [HttpPost]
    public IActionResult Delete(Productos producto)
    {
        // Aplicamos el chequeo de seguridad
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        
        _productoRepository.DeleteById(producto.IdProducto);
        //if (producto is null) return RedirectToAction("Index");
        //return View();
        return RedirectToAction("Index"); //Funcionando
    }
    // public IActionResult AccesoDenegado()
    // {
    //     return View();
    // }
}
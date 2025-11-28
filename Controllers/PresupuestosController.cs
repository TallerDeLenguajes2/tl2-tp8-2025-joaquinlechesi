using MiWebAPI.Models;
using MiWebAPI.Interfaces;
using MiWebAPI.Repository.PresupuestoRepository;
using MiWebAPI.Repository.ProductoRepository;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.ViewModels.PresupuestosViewModel;
using MiWebAPI.ViewModels.AgregarProductoViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiWebAPI.Controllers;
public class PresupuestosController : Controller
{
    private readonly IPresupuestoRepository _presupuestoRepository;
    private readonly IProductoRepository _productoRepository;
    private readonly IAuthenticationService _service;
    public PresupuestosController(IPresupuestoRepository presupuestoRepository, IProductoRepository productoRepository, IAuthenticationService service)
    {
        _presupuestoRepository = presupuestoRepository;
        _productoRepository = productoRepository;
        _service = service;
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
        var presupuestos = new CrearPresupuestosViewModel();
        return View(presupuestos); //Funcionando
    }
    [HttpPost]
    public IActionResult Create(CrearPresupuestosViewModel nuevoPresupuestoVM)
    {
        // var presupuestos = new Presupuestos
        // {
        //     NombreDestinatario = nuevoPresupuesto.NombreDestinatario,
        //     FechaCreacion = nuevoPresupuesto.FechaCreacion.Date
        // };
        //return View(presupuestos);
        var nuevoPresupuesto = new Presupuestos();
        if (nuevoPresupuestoVM.Correo.ToString() == "")
        {
            nuevoPresupuesto.NombreDestinatario = nuevoPresupuestoVM.NombreDestinatario;
        }else
        {
            nuevoPresupuesto.NombreDestinatario = nuevoPresupuestoVM.Correo.ToString();
        }
        nuevoPresupuesto.FechaCreacion = nuevoPresupuestoVM.FechaCreacion;
        _presupuestoRepository.AltaPresupuesto(nuevoPresupuesto);
        return RedirectToAction("Index"); //Funcionando
    }
    [HttpGet]
    public IActionResult Details(int id)
    {
        var presupuesto = _presupuestoRepository.GetDetallesById(id);
        if (presupuesto is null)
        {
            return RedirectToAction("Index"); //Cuando el PresupuestoDetalle no posee nada
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
        return View(presupuesto); //Funciona
    }
    [HttpPost]
    public IActionResult Delete(Presupuestos presupuesto)
    {
        _presupuestoRepository.DeleteById(presupuesto.IdPresupuestos);
        return RedirectToAction("Index"); //Funciona
        //return View();
    }
    [HttpGet]
    public IActionResult AgregarProducto(int id)
    {
        var productos = _productoRepository.GetAll();

        var agregarProducto = new AgregarProductoViewModel
        {
            IdPresupuestos = id,
            ListaProductos = new SelectList(productos, "IdProducto", "Description")
        };
        return View(agregarProducto);
    }
    [HttpPost]
    public IActionResult AgregarProducto(AgregarProductoViewModel productoVM)
    {
        if (!ModelState.IsValid)
        {
            var productos = _productoRepository.GetAll();
            productoVM.ListaProductos = new SelectList(productos, "IdProducto", "Description");
            return View(productoVM);
        }
        _presupuestoRepository.agregarAPresupuesto(productoVM.IdPresupuestos, productoVM.IdProducto, productoVM.Cantidad);
        return RedirectToAction(nameof(Details), new{ id = productoVM.IdPresupuestos});
    }

}
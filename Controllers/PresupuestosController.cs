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
    private readonly IAuthenticationService _authService;
    public PresupuestosController(IPresupuestoRepository presupuestoRepository, IProductoRepository productoRepository, IAuthenticationService authService)
    {
        _presupuestoRepository = presupuestoRepository;
        _productoRepository = productoRepository;
        _authService = authService;
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
        // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso que necesite validar
        if (_authService.HasAccessLevel("Administrador") || _authService.HasAccessLevel("Cliente") )
        {
            //si es es valido entra sino vuelve a login
            List<Presupuestos> presupuestos = _presupuestoRepository.GetAll();
            return View(presupuestos);
        } else {
            return RedirectToAction("Index", "Login");
        }
    }
    [HttpGet]
    public IActionResult Create()
    {
        // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction("AccesoDenegado");
        } else
        {
            var presupuestos = new CrearPresupuestosViewModel();
            return View(presupuestos); //Funcionando
        }
    }
    [HttpPost]
    public IActionResult Create(CrearPresupuestosViewModel nuevoPresupuestoVM)
    {
        // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction("AccesoDenegado");
        } else {    
            var nuevoPresupuesto = new Presupuestos();
            if (nuevoPresupuestoVM.Correo.ToString() == "")
            {
                nuevoPresupuesto.NombreDestinatario = nuevoPresupuestoVM.NombreDestinatario;
            } else {
                nuevoPresupuesto.NombreDestinatario = nuevoPresupuestoVM.Correo.ToString();
            }
            nuevoPresupuesto.FechaCreacion = nuevoPresupuestoVM.FechaCreacion;
            _presupuestoRepository.AltaPresupuesto(nuevoPresupuesto);
            return RedirectToAction("Index"); //Funcionando
        }
    }
    [HttpGet]
    public IActionResult Details(int id)
    {
        // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction("AccesoDenegado");
        } else
        {
            var presupuesto = _presupuestoRepository.GetDetallesById(id);
            if (presupuesto is null)
            {
                return RedirectToAction("Index"); //Cuando el PresupuestoDetalle no posee nada
            }
            return View(presupuesto);
        }
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction("AccesoDenegado");
        } else
        {
            var presupuestoEditar = _presupuestoRepository.GetById(id);
            return View(presupuestoEditar);
        }
    } //Funciona
    [HttpPost]
    public IActionResult Edit(Presupuestos presupuesto)
    {
        // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction("AccesoDenegado");
        } else
        {
            _presupuestoRepository.ModificarById(presupuesto);
            return RedirectToAction("Index"); // Funcionando
        }
    }
    [HttpGet]
    public IActionResult Delete(int id)
    {
        // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction("AccesoDenegado");
        } else
        {
            var presupuesto = _presupuestoRepository.GetById(id);
            if (presupuesto is null) return RedirectToAction("Index"); //Por precausion
            return View(presupuesto); //Funciona
        }
    }
    [HttpPost]
    public IActionResult Delete(Presupuestos presupuesto)
    {
        // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction("AccesoDenegado");
        } else
        {
            _presupuestoRepository.DeleteById(presupuesto.IdPresupuestos);
            return RedirectToAction("Index"); //Funciona
            //return View();
        }
    }
    [HttpGet]
    public IActionResult AgregarProducto(int id)
    {
        // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction("AccesoDenegado");
        } else
        {
            var productos = _productoRepository.GetAll();

            var agregarProducto = new AgregarProductoViewModel
            {
                IdPresupuestos = id,
                ListaProductos = new SelectList(productos, "IdProducto", "Description")
            };
            return View(agregarProducto);
        }
    }
    [HttpPost]
    public IActionResult AgregarProducto(AgregarProductoViewModel productoVM)
    {
        // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction("AccesoDenegado");
        } else
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
    public IActionResult AccesoDenegado()
    {
        return View();
    }
}
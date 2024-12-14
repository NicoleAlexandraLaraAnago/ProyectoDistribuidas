using System;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Login.Models;  // Usamos el modelo de Login
using BLL;           // Capa de negocio
using Entities;      // Entidades del proyecto
using SLC;

namespace Login.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        // Constructor que inicializa la capa de servicio de usuario
        public UserController()
        {
            _userService = new UserService(); // Instancia el servicio de usuario
        }

        // Método para convertir la contraseña en un hash
        private byte[] HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // GET: User/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        public ActionResult Register(User newUser) // Usamos el modelo User de Login.Models
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Mapeo de Login.Models.User a Entities.Users
                    var entityUser = new Entities.Users
                    {
                        Username = newUser.Username,
                        // Convertimos la contraseña en texto plano a un hash
                        PasswordHash = HashPassword(newUser.PasswordHash)
                    };

                    // Se llama al servicio para crear el nuevo usuario utilizando el modelo Entities.Users
                    _userService.CreateUser(entityUser);
                    return RedirectToAction("Login"); // Redirigir al login después del registro
                }
            }
            catch (Exception ex)
            {
                // En caso de error, se agrega un mensaje al estado del modelo
                ModelState.AddModelError("", ex.Message);
            }

            return View(newUser); // Si hay errores, regresa a la vista con el modelo
        }

        // GET: User/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        public async Task<ActionResult> Login(User user) // Recibe el modelo completo User
        {
            try
            {
                // Llamas al servicio para autenticar al usuario, pasando el usuario y la contraseña
                bool isAuthenticated = await _userService.AuthenticateUser(user.Username, user.PasswordHash); // Usa el modelo completo

                if (isAuthenticated)
                {
                    return RedirectToAction("Dashboard"); // Redirige a la página de inicio o dashboard
                }
                else
                {
                    // Si la autenticación falla, muestra un error
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message); // Manejo de errores
            }

            return View(user); // Si la autenticación falla, regresa al formulario con el modelo
        }

        // Acción para mostrar el dashboard o página de inicio después de login
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}

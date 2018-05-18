using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using System;
using System.Collections.Generic;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                         + "," + RolesUsuarios.UR_INOCUIDAD
                         + "," + RolesUsuarios.UR_MOVILIZACION
                         + "," + RolesUsuarios.UR_ANIMAL
                         + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.UR_VEGETAL
                         + "," + RolesUsuarios.UR_UPV
                         + "," + RolesUsuarios.IE_INOCUIDAD
                         + "," + RolesUsuarios.IE_MOVILIZACION
                         + "," + RolesUsuarios.IE_ANIMAL
                         + "," + RolesUsuarios.IE_VEGETAL
                         + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.PUESTO_AUXILIAR_ADMINISTRATIVO
                         + "," + RolesUsuarios.PUESTO_AUXILIAR_CAMPO
                         + "," + RolesUsuarios.PUESTO_AUXILIAR_INFORMATICA
                         + "," + RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                         + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                         + "," + RolesUsuarios.PUESTO_GERENTE
                         + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                         + "," + RolesUsuarios.PUESTO_PROF_ADMINISTRATIVO
                         + "," + RolesUsuarios.PUESTO_PROF_PROYECTO
                         + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                         + "," + RolesUsuarios.PUESTO_PROF_TECNICO_CALIDAD_MEJORA_PROCESOS
                         + "," + RolesUsuarios.PUESTO_PROF_TEC_CAPACITACION_DIVULGACION
                         + "," + RolesUsuarios.PUESTO_SECRETARIA
                         + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                         + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                         + "," + RolesUsuarios.TECNICO_OPERATIVO
                         + "," + RolesUsuarios.SUP_ESTATAL
                         + "," + RolesUsuarios.SUP_REGIONAL
                         + "," + RolesUsuarios.ESTATAL_PRES
                         + "," + RolesUsuarios.SUP_NACIONAL
                         + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
                         + "," + RolesUsuarios.IE_PROGRAMAS_U
                         + "," + RolesUsuarios.SUP_AUDITOR
        )]
    public class AccountController : Controller
    {
        ApplicationDbContext context;
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
            context = new ApplicationDbContext();
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    int Anio = DateTime.Today.Year;

                    UserData DataUsuario = UtilityClass.SetUserFields(user, UserManager);
                    Session["DataUsuario"] = DataUsuario;
                    Session["IdAnio"] = db.Anios.Where(item => item.Anio1 == Anio).Select(item => item.Pk_IdAnio).First();

                    //Obtiene el nombre de la persona logeada desde la tabla SIS.Persona
                    string nombreUsuario = "";

                    var _queryNombre = db.Personas.Where(item => item.Pk_IdPersona == DataUsuario.FK_IdPersona__SIS)
                                            .Select(item => item.NombreCompleto);

                    if(_queryNombre.Count() != 0) nombreUsuario = _queryNombre.First();

                    //Obtiene el nombre del Estado a la cual pertenece de la persona logeada desde la tabla SIS.Estado
                    string estado = "";

                    var _queryEstado = db.Estadoes.Where(item => item.Pk_IdEstado == DataUsuario.FK_IdEstado__SIS)
                                                    .Select(item => item.Nombre);

                    if (_queryEstado.Count() != 0) estado = _queryEstado.First();

                    //Obtiene el nombre del Comité a la cual pertenece de la persona logeada desde la tabla UE.UnidadEjecutora
                    string InstanciaEjecutora = "";

                    var _queryIE = db.UnidadEjecutoras.Where(item => item.Pk_IdUnidadEjecutora == DataUsuario.FK_IdUnidadEjecutora__UE)
                                                        .Select(item => item.Nombre);

                    if (_queryIE.Count() != 0) InstanciaEjecutora = _queryIE.First();

                    var modelUser = db.CurrentUsers;

                    CurrentUser currentUser = new CurrentUser()
                    {
                        IdSession = DataUsuario.IdSession,
                        IdUser = DataUsuario.Id,
                        Rol = "",
                        DateLogin = DateTime.Now,
                        UserName = DataUsuario.UserName,
                        Nombre = nombreUsuario,
                        Estado = estado,
                        InstanciaEjecutora = InstanciaEjecutora
                    };

                    try
                    {
                        modelUser.Add(currentUser);
                        db.SaveChanges();
                    }
                    catch (Exception e) { }

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Nombre de usuario y/o contraseña inválidos.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult PideDatosRecuperacionContrasenia()
        {
            return View("_PideDatosRecuperacionContrasenia");
        }

        /// <summary>
        /// Manda un correo al usuario con su nueva contraseña
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecuperaContrasenia(string usuarioProporcionado, string emailProporcionado)
        {
            //no se permitirá resetear la contraseña del Admin1
            if (usuarioProporcionado == "Admin1")
            {
                ViewBag.mensaje = "Error al querere restaurar la contraseña.";
                return View("RecuperaContrasenia");
            }

            var usuarioEncontrado = await UserManager.FindByNameAsync(usuarioProporcionado);

            //no se encuentra el usuario en el sistema
            if(usuarioEncontrado == null )
            {
                ViewBag.mensaje = "El usuario " + usuarioProporcionado + " no está dado de alta en el sistema.";
                return View("RecuperaContrasenia");
            }

            string _email = "";

            //Aquí identificamos si el usuario es de Instancia Ejecutora
            if(usuarioProporcionado.Length == 10 && usuarioProporcionado.Substring(0, 3) == "AIE")
            {
                _email = (from unidadEjecutora in db.UnidadEjecutoras
                          where unidadEjecutora.Pk_IdUnidadEjecutora == usuarioEncontrado.FK_IdUnidadEjecutora__UE
                          select unidadEjecutora.CorreoElectronico).First();
            }
            else
            {
                _email = (from personas in db.Personas
                          where personas.Pk_IdPersona == usuarioEncontrado.FK_IdPersona__SIS
                                && personas.esActivo == true
                          select personas.Email).First();
            }

            if(_email == "")
            {
                ViewBag.mensaje = "El usuario " + usuarioProporcionado + " no tiene asociado un correo electrónico. Favor de comunicarte con tu Instancia Ejecutora o Unidad Responsable correspondiente.";
                return View("RecuperaContrasenia");
            }

            if(_email != emailProporcionado)
            {
                ViewBag.mensaje = "El correo electrónico asociado a este usuario no coincide con el correo electrónico proporcionado.";
                return View("RecuperaContrasenia");
            }
            else
            {
                string psw = GeneraPsw();

                IdentityResult result1 = await UserManager.RemovePasswordAsync(usuarioEncontrado.Id);
                IdentityResult result2 = await UserManager.AddPasswordAsync(usuarioEncontrado.Id, psw);

                if (result1.Succeeded && result2.Succeeded)
                {
                    Correo _correo = new Correo(_email, "[SENASICA] Recuperación de contraseña", "Tu nueva contraseña para el usuario " + usuarioProporcionado + " es " + psw);
                    ViewBag.mensaje = await _correo.Enviar() ? "Se ha enviado la nueva contraseña a la cuenta de correo electrónico asociada a esta cuenta."
                                                        : "No se pudo enviar el correo." + _correo.Error();
                }
            }
            return View("RecuperaContrasenia");
        }

        /// <summary>
        /// Genera una nueva contraseña
        /// </summary>
        /// <returns></returns>
        private string GeneraPsw()
        {
            Random _random = new Random();
            string _contrasenia = "";

            for (int i = 0; i < 10; i++)
            {
                int aux = _random.Next(20);

                if (aux >= 0 && aux < 10) _contrasenia += (char)_random.Next(47, 57);
                if (aux >= 10 && aux < 15) _contrasenia += (char)_random.Next(64, 90);
                if (aux >= 15 && aux < 21) _contrasenia += (char)_random.Next(97, 122);
            }

            return _contrasenia;
        }

        //
        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        //-----------------------------------------------------------------------

        #region Genera nuevos usuarios con psw aleatorios
        //[AllowAnonymous]

        /// <summary>
        /// Este método se podrá llamar siempre que se deseen crear una lista de usuarios nuevos al sistema
        /// 
        /// La lista de usuarios que se crearán estarán guardados en List<string> listaUsuarios y el rol al cual pertenecerá estará
        /// guardado en string Rol
        /// 
        /// El método devolverá un json con dicha información
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        public JsonResult GeneraNuevosUsuarios()
        {
            string Rol = "Superusuario";

            List<string> listaUsuarios = new List<string>
            {
                "uce_gadai01"
            };

            Dictionary<string, string> listaUsuarioPsw = new Dictionary<string, string>();

            for (int i = 1; i <= listaUsuarios.Count(); i++)
            {
                string psw = GeneraPsw();

                bool result = CreaUsuario(listaUsuarios[i-1], i, Rol, psw);

                if (result) listaUsuarioPsw.Add(listaUsuarios[i-1], psw);
            }

            return Json(listaUsuarioPsw, JsonRequestBehavior.AllowGet);
        }

        private bool CreaUsuario(string usuario, int IdEstado, string rol, string psw)
        {
            var user = new ApplicationUser()
            {
                UserName = usuario,
                FK_IdEstado__SIS = IdEstado
            };

            var result = UserManager.Create(user, psw);

            if(result.Succeeded)
            {
                UserManager.AddToRole(user.Id, rol);

                return true;
            }

            return false;
        }
        #endregion

        public JsonResult GetEstadoByUnidadEjecutora()
        {
            string rolUsuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            int? IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);

            var _query = db.Estadoes.Where(e => e.Pk_IdEstado == IdEstado)
                                    .Select(e => new { e.Pk_IdEstado, e.Nombre });

            return Json(_query, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnidadEjecutora()
        {
            string rolUsuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            int? IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);

            var _query = db.UnidadEjecutoras.Where(u => u.Pk_IdUnidadEjecutora == IdUnidadEjecutora)
                                            .Select(u => new { u.Pk_IdUnidadEjecutora, u.Nombre });

            return Json(_query, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPersonasByUnidadEjecutora()
        {
            int? IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);

            var _query = db.Personas.Where(p => p.esActivo == true
                                                && p.Fk_IdUnidadEjecutora__UE == IdUnidadEjecutora) 
                                    .Select(p => new { p.Pk_IdPersona, p.NombreCompleto })
                                    .OrderBy (u => u.NombreCompleto);

            return Json(_query, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPuestoByPersona(int IdPersona)
        {
            var _query = db.Personas.Where(p => p.esActivo == true
                                                && p.Pk_IdPersona == IdPersona)
                                    .Select(p => new { p.Fk_IdCargo, p.Cargo.Nombre });

            return Json(_query, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { 
                                                    UserName = model.UserName
                                                    , FK_IdEstado__SIS = model.dropDownEstado
                                                    , FK_IdUnidadResponsable__UE = model.dropDownUnidadResponsable
                                                    , FK_IdUnidadEjecutora__UE = model.dropDownInstanciaEjecutora
                                                    , FK_IdCampania__UE = 0
                                                    , FK_IdAccion__UE = model.dropDownAccion
                                                    , FK_IdActividad__UE = model.Actividad
                                                    , FK_IdPersona__SIS = model.dropDownPersona
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Assign Role to user Here 
                    await UserManager.AddToRoleAsync(user.Id, model.dropDownRoles);
                    //Ends Here

                    await SignInAsync(user, isPersistent: false);
                    UserData DataUsuario = UtilityClass.SetUserFields(user, UserManager);
                    this.Session["DataUsuario"] = DataUsuario;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Su contraseña ha cambiado."
                : message == ManageMessageId.SetPasswordSuccess ? "Se ha establecido su password."
                : message == ManageMessageId.RemoveLoginSuccess ? "Se eliminó el loggeo externo."
                : message == ManageMessageId.Error ? "Ha ocurrido un error."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                UserData DataUsuario = UtilityClass.SetUserFields(user, UserManager);
                this.Session["DataUsuario"] = DataUsuario;
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        UserData DataUsuario = UtilityClass.SetUserFields(user, UserManager);
                        this.Session["DataUsuario"] = DataUsuario;
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/LogOff
        
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();

            string IdSession = FuncionesAuxiliares.GetCurrent_IdSession();

            var model = db.CurrentUsers;

            try
            {
                var item = model.FirstOrDefault(it => it.IdSession == IdSession);
                if (item != null)
                    model.Remove(item);

                db.SaveChanges();
            }
            catch (Exception e) { }

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
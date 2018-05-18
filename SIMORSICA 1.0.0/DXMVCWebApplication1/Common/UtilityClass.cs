using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DXMVCWebApplication1.Models;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace DXMVCWebApplication1.Common
{
    public static class UtilityClass
    {
        public static UserData SetUserFields(ApplicationUser user, UserManager<ApplicationUser> UserManager)
        {
            bool IsAdmin = UserManager.IsInRole(user.Id, "Admin");

            IList<string> RolesUsuario = UserManager.GetRoles(user.Id);

           
            UserData DataUser = new UserData(Guid.NewGuid().ToString()
                                                , user.FK_IdEstado__SIS
                                                , user.FK_IdUnidadEjecutora__UE
                                                , user.FK_IdUnidadResponsable__UE
                                                , user.FK_IdCampania__UE
                                                , user.FK_IdAccion__UE
                                                , user.FK_IdActividad__UE
                                                , user.FK_IdPersona__SIS
                                                , user.Id
                                                , user.UserName
                                                , IsAdmin
                                                , RolesUsuario
                );

            return DataUser;
        }

    }
    public class UserData
    {
        public string IdSession { get; set; }
        public int FK_IdEstado__SIS { get; set; }
        public int FK_IdUnidadEjecutora__UE { get; set; }
        public int FK_IdUnidadResponsable__UE { get; set; }
        public int FK_IdCampania__UE { get; set; }
        public int FK_IdAccion__UE { get; set; }
        public int FK_IdActividad__UE { get; set; }
        public int FK_IdPersona__SIS { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public IList<string> RolesUsuario { get; set; }


        public UserData() { }
        
        public UserData(string idSession
            , int fK_IdEstado__SIS
            , int fK_IdUnidadEjecutora__UE
            , int fK_IdUnidadResponsable__UE
            , int fK_IdCampania__UE
            , int fK_IdAlcance__UE
            , int fK_IdActividad__UE
            , int fK_IdPersona__SIS
            , string id
            , string userName
            , bool isAdmin
            , IList<string> rolesUsuario)
        {
            IdSession = idSession;
            FK_IdEstado__SIS = fK_IdEstado__SIS;
            FK_IdUnidadEjecutora__UE = fK_IdUnidadEjecutora__UE;
            FK_IdUnidadResponsable__UE = fK_IdUnidadResponsable__UE;
            FK_IdCampania__UE = fK_IdCampania__UE;
            FK_IdAccion__UE = fK_IdAlcance__UE;
            FK_IdActividad__UE = fK_IdActividad__UE;
            FK_IdPersona__SIS = fK_IdPersona__SIS;
            Id = id;
            UserName = userName;
            IsAdmin = isAdmin;
            RolesUsuario = rolesUsuario;
        }

        public int? setFilter(int? IdForFilter)
        {
            if (IsAdmin)
            {
                return null;
            }
            else
            {
                return IdForFilter;
            }
        }
    }
}
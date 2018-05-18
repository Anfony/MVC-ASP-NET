using DXMVCWebApplication1.Models;
using System.Collections;
using System.Linq;
using System.Web.UI;

namespace DXMVCWebApplication1.Common
{
    public abstract class ItemsData : IHierarchicalEnumerable, IEnumerable
    {
        public ItemsData() { }

        public abstract IEnumerable Data { get; }

        public IEnumerator GetEnumerator()
        { return Data.GetEnumerator(); }

        public IHierarchyData GetHierarchyData(object enumeratedItem)
        { return (IHierarchyData)enumeratedItem; }
    }

    public class ItemData : IHierarchyData
    {
        public string Text { get; protected set; }

        public string NavigateUrl { get; protected set; }

        public int Orden { get; set; }

        public ItemData(string text, string navigateUrl, int orden)
        {
            Text = text;
            NavigateUrl = navigateUrl;
            Orden = orden;
        }

        // IHierarchyData
        bool IHierarchyData.HasChildren
        {
            get { return HasChildren(); }
        }

        object IHierarchyData.Item
        {
            get { return this; }
        }

        string IHierarchyData.Path
        {
            get { return NavigateUrl; }
        }

        string IHierarchyData.Type
        {
            get { return GetType().ToString(); }
        }

        IHierarchicalEnumerable IHierarchyData.GetChildren() { return CreateChildren(); }

        IHierarchyData IHierarchyData.GetParent() { return null; }

        protected virtual bool HasChildren() { return false; }

        protected virtual IHierarchicalEnumerable CreateChildren() { return null; }
    }

    public class CreaMenu : ItemsData
    {
        public override IEnumerable Data
        {
            get
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();

                // La siguiente consulta es para saber cuáles de los menús principales tiene acceso el usuario de acuerdo 
                // a sus privilegios de acceso conforme a su rol
                var _result = db.Menus.ToList().Join(db.Pantallas, m => m.Fk_IdPantallas__SIS, p => p.Pk_IdPantallas, (m, p) => new { m, p })
                                    .Join(db.PrivilegiosXRols, pp => pp.p.Pk_IdPantallas, pr => pr.Fk_IdPantalla__SIS, (pp, pr) => new { pp, pr })
                                    .Where(a => a.pp.m.Fk_IdMenu__SIS == null && a.pr.Rol.Nombre == Senasica.GetRolUsuarioLogeado())
                                    .Select(m => new MenuData(m.pp.m))
                                    .OrderBy(m => m.Menu.Orden);

                return _result;
            }
        }
    }

    public class MenuData : ItemData
    {
        public Menu Menu { get; protected set; }

        public MenuData(Menu menu) : base(menu.Pantalla.Nombre, "", menu.Orden)
        { Menu = menu; }

        protected override bool HasChildren() { return true; }

        protected override IHierarchicalEnumerable CreateChildren()
        {
            return new SubMenu(Menu.Pk_IdMenu);
        }
    }

    public class SubMenu : ItemsData
    {
        public int Pk_IdMenu { get; protected set; }

        public SubMenu(int Pk_IdMenu) : base()
        { this.Pk_IdMenu = Pk_IdMenu; }

        public override IEnumerable Data
        {
            get
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();

                // La siguiente consulta es para saber cuáles de los submenús tiene acceso el usuario de acuerdo 
                // a sus privilegios de acceso conforme a su rol
                var _result = db.Menus.ToList().Join(db.Pantallas, m => m.Fk_IdPantallas__SIS, p => p.Pk_IdPantallas, (m, p) => new { m, p })
                                            .Join(db.PrivilegiosXRols, pp => pp.p.Pk_IdPantallas, pr => pr.Fk_IdPantalla__SIS, (pp, pr) => new { pp, pr })
                                            .Where(a => a.pp.m.Fk_IdMenu__SIS == Pk_IdMenu && a.pr.Rol.Nombre == Senasica.GetRolUsuarioLogeado())
                                            .Select(m => new SubMenuData(m.pp.m))
                                            .OrderBy(m => m.Orden);

                return _result;
            }
        }
    }

    public class SubMenuData : ItemData
    {
        public SubMenuData(Menu menu) : base(menu.Pantalla.Nombre, "~/" + menu.Pantalla.Controlador, menu.Orden) { }
    }
}
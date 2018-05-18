using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.UR_ANIMAL
                  + "," + RolesUsuarios.UR_VEGETAL
                  + "," + RolesUsuarios.UR_INOCUIDAD
                  + "," + RolesUsuarios.UR_MOVILIZACION
    )]
    public class AsignacionPresupuestoXEstadoController : Controller
    {
        private static int IdAnio = 3;

        public ActionResult Index()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _query = db.PorcentajeXDireccionGenerals.Where(p => p.FK_IdAnio == IdAnio && p.PresupuestoCerrado == true);

            ViewData["PresupuestoYaOtorgado"] = _query.Count() == 0 ? false : true;
            ViewData["esRecursoCerrado"] = recursosCerrados();
            ViewData["recursosYaDistribuidos"] = recursosYaDistribuidos();

            return View();
        }

        public ActionResult DGPresupuestoXEstado()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdURLogeado = FuncionesAuxiliares.getIdUnidadResponsable(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString());
            int IdPorcentajeXDireccionGeneral = getIdPorcentajeXDireccionGeneral();

            var _presupuestoAsignado = db.PorcentajeXDireccionGenerals.Where(p => p.FK_IdAnio == IdAnio && p.FK_IdUnidadResponsable == IdURLogeado).Select(p => p.PresupuestoAsignado);
            var _data = db.PresupuestoXEstadoes.ToList().Where(p => p.FK_IdPorcentajeXDireccionGeneral == IdPorcentajeXDireccionGeneral);

            ViewData["PresupuestoTotalAsignado"] = _presupuestoAsignado.Count() == 0 ? 0 : _presupuestoAsignado.First();

            LlenaViewData();

            return PartialView("_DGPresupuestoXEstado", _data);
        }

        private void LlenaViewData()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdPorcentajeXDireccionGeneral = getIdPorcentajeXDireccionGeneral();

            decimal?[] _cantidades = new decimal?[33];

            for (int i = 1; i < 33; i++)
            {
                var _query = db.PresupuestoXEstadoes.Where(p => p.FK_IdPorcentajeXDireccionGeneral == IdPorcentajeXDireccionGeneral 
                                                                && p.FK_IdEstado == i)
                                                        .Select(p => p.CantidadSolicitada);
                _cantidades[i] = _query.Count() == 0 ? 0 : _query.First();
            }
            ViewData["Aguascalientes"] = _cantidades[1];
            ViewData["BajaCalifornia"] = _cantidades[2];
            ViewData["BajaCaliforniaSur"] = _cantidades[3];
            ViewData["Campeche"] = _cantidades[4];
            ViewData["Coahuila"] = _cantidades[5];
            ViewData["Colima"] = _cantidades[6];
            ViewData["Chiapas"] = _cantidades[7];
            ViewData["Chihuahua"] = _cantidades[8];
            ViewData["CDMX"] = _cantidades[9];
            ViewData["Durango"] = _cantidades[10];
            ViewData["Guanajuato"] = _cantidades[11];
            ViewData["Guerrero"] = _cantidades[12];
            ViewData["Hidalgo"] = _cantidades[13];
            ViewData["Jalisco"] = _cantidades[14];
            ViewData["Mexico"] = _cantidades[15];
            ViewData["MichoacanOcampo"] = _cantidades[16];
            ViewData["Morelos"] = _cantidades[17];
            ViewData["Nayarit"] = _cantidades[18];
            ViewData["NuevoLeon"] = _cantidades[19];
            ViewData["Oaxaca"] = _cantidades[20];
            ViewData["Puebla"] = _cantidades[21];
            ViewData["Queretaro"] = _cantidades[22];
            ViewData["QuintanaRoo"] = _cantidades[23];
            ViewData["SanLuisPotosi"] = _cantidades[24];
            ViewData["Sinaloa"] = _cantidades[25];
            ViewData["Sonora"] = _cantidades[26];
            ViewData["Tabasco"] = _cantidades[27];
            ViewData["Tamaulipas"] = _cantidades[28];
            ViewData["Tlaxcala"] = _cantidades[29];
            ViewData["Veracruz"] = _cantidades[30];
            ViewData["Yucatan"] = _cantidades[31];
            ViewData["Zacatecas"] = _cantidades[32];
        }

        public ActionResult GuardaRecursosXEstado(string Aguascalientes, string BajaCalifornia, string BajaCaliforniaSur, string Campeche, string Coahuila, string Colima, string Chiapas, string Chihuahua,
                                                    string CDMX, string Durango, string Guanajuato, string Guerrero, string Hidalgo, string Jalisco, string Mexico, string MichoacanOcampo,
                                                    string Morelos, string Nayarit, string NuevoLeon, string Oaxaca, string Puebla, string Queretaro, string QuintanaRoo, string SanLuisPotosi,
                                                    string Sinaloa, string Sonora, string Tabasco, string Tamaulipas, string Tlaxcala, string Veracruz, string Yucatan, string Zacatecas)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            double presupuestoAguascalientes = Aguascalientes == "" ? 0 : Convert.ToDouble(Aguascalientes);
            double presupuestoBajaCalifornia = BajaCalifornia == "" ? 0 : Convert.ToDouble(BajaCalifornia);
            double presupuestoBajaCaliforniaSur = BajaCaliforniaSur == "" ? 0 : Convert.ToDouble(BajaCaliforniaSur);
            double presupuestoCampeche = Campeche == "" ? 0 : Convert.ToDouble(Campeche);
            double presupuestoCoahuila = Coahuila == "" ? 0 : Convert.ToDouble(Coahuila);
            double presupuestoColima = Colima == "" ? 0 : Convert.ToDouble(Colima);
            double presupuestoChiapas = Chiapas == "" ? 0 : Convert.ToDouble(Chiapas);
            double presupuestoChihuahua = Chihuahua == "" ? 0 : Convert.ToDouble(Chihuahua);
            double presupuestoCDMX = CDMX == "" ? 0 : Convert.ToDouble(CDMX);
            double presupuestoDurango = Durango == "" ? 0 : Convert.ToDouble(Durango);
            double presupuestoGuanajuato = Guanajuato == "" ? 0 : Convert.ToDouble(Guanajuato);
            double presupuestoGuerrero = Guerrero == "" ? 0 : Convert.ToDouble(Guerrero);
            double presupuestoHidalgo = Hidalgo == "" ? 0 : Convert.ToDouble(Hidalgo);
            double presupuestoJalisco = Jalisco == "" ? 0 : Convert.ToDouble(Jalisco);
            double presupuestoMexico = Mexico == "" ? 0 : Convert.ToDouble(Mexico);
            double presupuestoMichoacanOcampo = MichoacanOcampo == "" ? 0 : Convert.ToDouble(MichoacanOcampo);
            double presupuestoMorelos = Morelos == "" ? 0 : Convert.ToDouble(Morelos);
            double presupuestoNayarit = Nayarit == "" ? 0 : Convert.ToDouble(Nayarit);
            double presupuestoNuevoLeon = NuevoLeon == "" ? 0 : Convert.ToDouble(NuevoLeon);
            double presupuestoOaxaca = Oaxaca == "" ? 0 : Convert.ToDouble(Oaxaca);
            double presupuestoPuebla = Puebla == "" ? 0 : Convert.ToDouble(Puebla);
            double presupuestoQueretaro = Queretaro == "" ? 0 : Convert.ToDouble(Queretaro);
            double presupuestoQuintanaRoo = QuintanaRoo == "" ? 0 : Convert.ToDouble(QuintanaRoo);
            double presupuestoSanLuisPotosi = SanLuisPotosi == "" ? 0 : Convert.ToDouble(SanLuisPotosi);
            double presupuestoSinaloa = Sinaloa == "" ? 0 : Convert.ToDouble(Sinaloa);
            double presupuestoSonora = Sonora == "" ? 0 : Convert.ToDouble(Sonora);
            double presupuestoTabasco = Tabasco == "" ? 0 : Convert.ToDouble(Tabasco);
            double presupuestoTamaulipas = Tamaulipas == "" ? 0 : Convert.ToDouble(Tamaulipas);
            double presupuestoTlaxcala = Tlaxcala == "" ? 0 : Convert.ToDouble(Tlaxcala);
            double presupuestoVeracruz = Veracruz == "" ? 0 : Convert.ToDouble(Veracruz);
            double presupuestoYucatan = Yucatan == "" ? 0 : Convert.ToDouble(Yucatan);
            double presupuestoZacatecas = Zacatecas == "" ? 0 : Convert.ToDouble(Zacatecas);

            if (!esValidaDistribucion(presupuestoAguascalientes, presupuestoBajaCalifornia, presupuestoBajaCaliforniaSur, presupuestoCampeche, presupuestoCoahuila, presupuestoColima, presupuestoChiapas, presupuestoChihuahua,
                                        presupuestoCDMX, presupuestoDurango, presupuestoGuanajuato, presupuestoGuerrero, presupuestoHidalgo, presupuestoJalisco, presupuestoMexico, presupuestoMichoacanOcampo,
                                        presupuestoMorelos, presupuestoNayarit, presupuestoNuevoLeon, presupuestoOaxaca, presupuestoPuebla, presupuestoQueretaro, presupuestoQuintanaRoo, presupuestoSanLuisPotosi,
                                        presupuestoSinaloa, presupuestoSonora, presupuestoTabasco, presupuestoTamaulipas, presupuestoTlaxcala, presupuestoVeracruz, presupuestoYucatan, presupuestoZacatecas)) return PartialView("_errorDistribucion");

            try
            {
                int IdPorcentajeXDireccionGeneral = getIdPorcentajeXDireccionGeneral();

                if (recursosYaDistribuidos())
                {
                    PresupuestoXEstado[] _elementos = new PresupuestoXEstado[33];

                    for (int i = 1; i < 33; i++)
                    {
                        _elementos[i] = db.PresupuestoXEstadoes.FirstOrDefault(elemento => elemento.FK_IdPorcentajeXDireccionGeneral == IdPorcentajeXDireccionGeneral && elemento.FK_IdEstado == i);
                    }

                    if (_elementos[1] != null) _elementos[1].CantidadSolicitada = Convert.ToDecimal(presupuestoAguascalientes);
                    if (_elementos[2] != null) _elementos[2].CantidadSolicitada = Convert.ToDecimal(presupuestoBajaCalifornia);
                    if (_elementos[3] != null) _elementos[3].CantidadSolicitada = Convert.ToDecimal(presupuestoBajaCaliforniaSur);
                    if (_elementos[4] != null) _elementos[4].CantidadSolicitada = Convert.ToDecimal(presupuestoCampeche);
                    if (_elementos[5] != null) _elementos[5].CantidadSolicitada = Convert.ToDecimal(presupuestoCoahuila);
                    if (_elementos[6] != null) _elementos[6].CantidadSolicitada = Convert.ToDecimal(presupuestoColima);
                    if (_elementos[7] != null) _elementos[7].CantidadSolicitada = Convert.ToDecimal(presupuestoChiapas);
                    if (_elementos[8] != null) _elementos[8].CantidadSolicitada = Convert.ToDecimal(presupuestoChihuahua);
                    if (_elementos[9] != null) _elementos[9].CantidadSolicitada = Convert.ToDecimal(presupuestoCDMX);
                    if (_elementos[10] != null) _elementos[10].CantidadSolicitada = Convert.ToDecimal(presupuestoDurango);
                    if (_elementos[11] != null) _elementos[11].CantidadSolicitada = Convert.ToDecimal(presupuestoGuanajuato);
                    if (_elementos[12] != null) _elementos[12].CantidadSolicitada = Convert.ToDecimal(presupuestoGuerrero);
                    if (_elementos[13] != null) _elementos[13].CantidadSolicitada = Convert.ToDecimal(presupuestoHidalgo);
                    if (_elementos[14] != null) _elementos[14].CantidadSolicitada = Convert.ToDecimal(presupuestoJalisco);
                    if (_elementos[15] != null) _elementos[15].CantidadSolicitada = Convert.ToDecimal(presupuestoMexico);
                    if (_elementos[16] != null) _elementos[16].CantidadSolicitada = Convert.ToDecimal(presupuestoMichoacanOcampo);
                    if (_elementos[17] != null) _elementos[17].CantidadSolicitada = Convert.ToDecimal(presupuestoMorelos);
                    if (_elementos[18] != null) _elementos[18].CantidadSolicitada = Convert.ToDecimal(presupuestoNayarit);
                    if (_elementos[19] != null) _elementos[19].CantidadSolicitada = Convert.ToDecimal(presupuestoNuevoLeon);
                    if (_elementos[20] != null) _elementos[20].CantidadSolicitada = Convert.ToDecimal(presupuestoOaxaca);
                    if (_elementos[21] != null) _elementos[21].CantidadSolicitada = Convert.ToDecimal(presupuestoPuebla);
                    if (_elementos[22] != null) _elementos[22].CantidadSolicitada = Convert.ToDecimal(presupuestoQueretaro);
                    if (_elementos[23] != null) _elementos[23].CantidadSolicitada = Convert.ToDecimal(presupuestoQuintanaRoo);
                    if (_elementos[24] != null) _elementos[24].CantidadSolicitada = Convert.ToDecimal(presupuestoSanLuisPotosi);
                    if (_elementos[25] != null) _elementos[25].CantidadSolicitada = Convert.ToDecimal(presupuestoSinaloa);
                    if (_elementos[26] != null) _elementos[26].CantidadSolicitada = Convert.ToDecimal(presupuestoSonora);
                    if (_elementos[27] != null) _elementos[27].CantidadSolicitada = Convert.ToDecimal(presupuestoTabasco);
                    if (_elementos[28] != null) _elementos[28].CantidadSolicitada = Convert.ToDecimal(presupuestoTamaulipas);
                    if (_elementos[29] != null) _elementos[29].CantidadSolicitada = Convert.ToDecimal(presupuestoTlaxcala);
                    if (_elementos[30] != null) _elementos[30].CantidadSolicitada = Convert.ToDecimal(presupuestoVeracruz);
                    if (_elementos[31] != null) _elementos[31].CantidadSolicitada = Convert.ToDecimal(presupuestoYucatan);
                    if (_elementos[32] != null) _elementos[32].CantidadSolicitada = Convert.ToDecimal(presupuestoZacatecas);
                }
                else
                {
                    PresupuestoXEstado[] _elementos =
                    {
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 1, CantidadSolicitada = Convert.ToDecimal(presupuestoAguascalientes) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 2, CantidadSolicitada = Convert.ToDecimal(presupuestoBajaCalifornia) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 3, CantidadSolicitada = Convert.ToDecimal(presupuestoBajaCaliforniaSur) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 4, CantidadSolicitada = Convert.ToDecimal(presupuestoCampeche) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 5, CantidadSolicitada = Convert.ToDecimal(presupuestoCoahuila) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 6, CantidadSolicitada = Convert.ToDecimal(presupuestoColima) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 7, CantidadSolicitada = Convert.ToDecimal(presupuestoChiapas) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 8, CantidadSolicitada = Convert.ToDecimal(presupuestoChihuahua) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 9, CantidadSolicitada = Convert.ToDecimal(presupuestoCDMX) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 10, CantidadSolicitada = Convert.ToDecimal(presupuestoDurango) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 11, CantidadSolicitada = Convert.ToDecimal(presupuestoGuanajuato) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 12, CantidadSolicitada = Convert.ToDecimal(presupuestoGuerrero) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 13, CantidadSolicitada = Convert.ToDecimal(presupuestoHidalgo) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 14, CantidadSolicitada = Convert.ToDecimal(presupuestoJalisco) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 15, CantidadSolicitada = Convert.ToDecimal(presupuestoMexico) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 16, CantidadSolicitada = Convert.ToDecimal(presupuestoMichoacanOcampo) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 17, CantidadSolicitada = Convert.ToDecimal(presupuestoMorelos) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 18, CantidadSolicitada = Convert.ToDecimal(presupuestoNayarit) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 19, CantidadSolicitada = Convert.ToDecimal(presupuestoNuevoLeon) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 20, CantidadSolicitada = Convert.ToDecimal(presupuestoOaxaca) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 21, CantidadSolicitada = Convert.ToDecimal(presupuestoPuebla) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 22, CantidadSolicitada = Convert.ToDecimal(presupuestoQueretaro) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 23, CantidadSolicitada = Convert.ToDecimal(presupuestoQuintanaRoo) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 24, CantidadSolicitada = Convert.ToDecimal(presupuestoSanLuisPotosi) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 25, CantidadSolicitada = Convert.ToDecimal(presupuestoSinaloa) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 26, CantidadSolicitada = Convert.ToDecimal(presupuestoSonora) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 27, CantidadSolicitada = Convert.ToDecimal(presupuestoTabasco) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 28, CantidadSolicitada = Convert.ToDecimal(presupuestoTamaulipas) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 29, CantidadSolicitada = Convert.ToDecimal(presupuestoTlaxcala) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 30, CantidadSolicitada = Convert.ToDecimal(presupuestoVeracruz) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 31, CantidadSolicitada = Convert.ToDecimal(presupuestoYucatan) },
                        new PresupuestoXEstado { FK_IdPorcentajeXDireccionGeneral = IdPorcentajeXDireccionGeneral, FK_IdEstado = 32, CantidadSolicitada = Convert.ToDecimal(presupuestoZacatecas) }
                    };

                    foreach (var item in _elementos) db.PresupuestoXEstadoes.Add(item);
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Verifica que todo el recurso otorgado se haya distribuido en todos los estados
        /// </summary>
        /// <returns></returns>
        private bool esValidaDistribucion(double aguascalientes, double bajaCalifornia, double bajaCaliforniaSur, double campeche, double coahuila, double colima, double chiapas, double chihuahua, 
                                          double cDMX, double durango, double guanajuato, double guerrero, double hidalgo, double jalisco, double mexico, double michoacanOcampo, 
                                          double morelos, double nayarit, double nuevoLeon, double oaxaca, double puebla, double queretaro, double quintanaRoo, double sanLuisPotosi, 
                                          double sinaloa, double sonora, double tabasco, double tamaulipas, double tlaxcala, double veracruz, double yucatan, double zacatecas)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdURLogeado = FuncionesAuxiliares.getIdUnidadResponsable(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString());

            var _query = db.PorcentajeXDireccionGenerals.Where(p => p.FK_IdAnio == IdAnio && p.FK_IdUnidadResponsable == IdURLogeado).Select(p => p.PresupuestoAsignado);

            var _presupuestoTotal = _query.Count() == 0 ? 0 : _query.First();

            var _sumaEstados = aguascalientes + bajaCalifornia + bajaCaliforniaSur + campeche + coahuila + colima + chiapas + chihuahua + cDMX + durango + guanajuato + guerrero + hidalgo + jalisco + mexico + michoacanOcampo
                             + morelos + nayarit + nuevoLeon + oaxaca + puebla + queretaro + quintanaRoo + sanLuisPotosi + sinaloa + sonora + tabasco + tamaulipas + tlaxcala + veracruz + yucatan + zacatecas;

            return _sumaEstados == _presupuestoTotal ? true : false;
        }

        /// <summary>
        /// Realiza el cirre de la distribución del recurso.
        /// 
        /// El recurso ya no podrá ser modificado
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CierraDistribucionRecurso()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            try
            {
                int IdPorcentajeXDireccionGeneral = getIdPorcentajeXDireccionGeneral();

                var _elementos = db.PresupuestoXEstadoes.Where(p => p.PorcentajeXDireccionGeneral.Pk_IdPorcentajeXDireccionGeneral == IdPorcentajeXDireccionGeneral);

                foreach (var item in _elementos) item.PresupuestoCerrado = true;

                db.SaveChanges();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Verifica si los recursos ingresados están ya cerrados (que ya no se pueden editar)
        /// </summary>
        /// <returns></returns>
        private bool recursosCerrados()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdPorcentajeXDireccionGeneral = getIdPorcentajeXDireccionGeneral();

            return db.PresupuestoXEstadoes.Where(p => p.PorcentajeXDireccionGeneral.Pk_IdPorcentajeXDireccionGeneral == IdPorcentajeXDireccionGeneral
                                                    && p.PresupuestoCerrado == true).Count() != 0 ? true : false;
        }

        /// <summary>
        /// Verifica que ya se haya hecho la distribución de los recursos en las distintas Direcciones Generales
        /// </summary>
        /// <returns></returns>
        private bool recursosYaDistribuidos()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdPorcentajeXDireccionGeneral = getIdPorcentajeXDireccionGeneral();

            return db.PresupuestoXEstadoes.Where(p => p.PorcentajeXDireccionGeneral.Pk_IdPorcentajeXDireccionGeneral == IdPorcentajeXDireccionGeneral).Count() != 0 ? true : false;
        }

        /// <summary>
        /// Obtiene el ID a la tabla PRES.PorcentajeXDireccionGeneral en la cual
        /// indicaría el techo presupuestal para el usuario logeado
        /// </summary>
        /// <returns></returns>
        private int getIdPorcentajeXDireccionGeneral()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdURLogeado = FuncionesAuxiliares.getIdUnidadResponsable(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString());

            int IdPorcentajeXDireccionGeneral = db.PorcentajeXDireccionGenerals.Where(p => p.FK_IdUnidadResponsable == IdURLogeado && p.FK_IdAnio == IdAnio)
                                                                                .Select(p => p.Pk_IdPorcentajeXDireccionGeneral).First();

            return IdPorcentajeXDireccionGeneral;
        }
    }
}

using FoodGood.Carrito.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace FoodGood.Utilities
{
    /// <summary>
    /// Summary description for PedidoUtilities
    /// </summary>
    public class PedidoUtilities
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public PedidoUtilities()
        { }

        public static string SetupShoppingCart()
        {
            return SetupShoppingCart(HttpContext.Current);
        }

        public static string SetupShoppingCart(HttpContext context)
        {
            string cookieName = "FoodGoodCartId";

            string userName = context.User.Identity.Name;
            string guid = "";
            //if (string.IsNullOrEmpty(userName))
            //{
            HttpCookie cookie = context.Request.Cookies[cookieName];
            if (cookie == null)
            {
                guid = Guid.NewGuid().ToString();
                cookie = new HttpCookie(cookieName, guid);
                cookie.Expires = DateTime.Now.AddDays(365);
                context.Response.Cookies.Add(cookie);
                try
                {
                    FoodGood.Carrito.Carrito obj = new FoodGood.Carrito.Carrito();
                    obj.CarritoId = guid;
                    //obj.UsuarioId = 
                    obj.Contenido = "{}";
                    obj.Fecha = DateTime.Now;
                    CarritoBLL.InsertCarrito(obj);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                guid = cookie.Value;
            //}
            //else
            //{
            //    //TODO: Obenter el GUID del carrito para el usuario logueado
            //    guid = "";
            //    HttpCookie cookie = context.Request.Cookies[cookieName];

            //    if (string.IsNullOrEmpty(guid))
            //    {
            //        guid = new Guid().ToString();
            //    }
            //    else
            //    {
            //        string oldGuid = "";
            //        if (cookie != null)
            //            oldGuid = cookie.Value;

            //        if (!string.IsNullOrEmpty(oldGuid))
            //        {
            //            //TODO: Eliminar el carrito asignado a este usua
            //        }

            //    }

            //    if (cookie != null)
            //    {
            //        context.Response.Cookies.Remove(cookieName);                                       
            //    }
            //    cookie = new HttpCookie(cookieName, guid);
            //    cookie.Expires = DateTime.Now.AddDays(365);
            //    context.Response.Cookies.Add(cookie);
            //}

            return guid;
        }

        public static string obtenerIdCarrito()
        {
            return obtenerIdCarrito(HttpContext.Current);
        }

        public static string obtenerIdCarrito(HttpContext context)
        {
            try
            {
                string cookieName = "FoodGoodCartId";
                if (context.Request.Cookies[cookieName] != null)
                {
                    HttpCookie cookie = context.Request.Cookies[cookieName];
                    return cookie.Value;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener cookie FoodGoodCartId", ex);

            }
            return null;
        }

        public static Dictionary<string, DatorProductoCarrito> GetCarrito()
        {
            try
            {
                string cartId = obtenerIdCarrito();
                string cartJson = "{}";
                //TODO: Obtener carrito en JSON de la base de datos
                if (!string.IsNullOrEmpty(cartId))
                    cartJson = CarritoBLL.GetCarritoById(cartId).Contenido;

                JavaScriptSerializer js = new JavaScriptSerializer();
                Dictionary<string, DatorProductoCarrito> carrito = js.Deserialize<Dictionary<string, DatorProductoCarrito>>(cartJson);
                return carrito;
            }
            catch (Exception ex)
            { log.Error("Error al obtener el carrito de la base de datos", ex); return null; }
        }

        public static bool GetCarritoIdForUsuarioIniciado(int userId)
        {
            return GetCarritoIdForUsuarioIniciado(userId, HttpContext.Current);
        }

        public static bool GetCarritoIdForUsuarioIniciado(int userId, HttpContext context)
        {
            try
            {
                bool existeCarrito = false;
                string carritoId = "";
                FoodGood.Carrito.Carrito objCarrito = CarritoBLL.GetCarritoByIdUsurio(userId);
                if (objCarrito != null)
                {
                    carritoId = objCarrito.CarritoId;
                    string cookieName = "FoodGoodCartId";
                    HttpCookie cookie = context.Request.Cookies[cookieName];
                    if (cookie == null)
                    {
                        cookie = new HttpCookie(cookieName, carritoId);
                        cookie.Expires = DateTime.Now.AddDays(365);
                        context.Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        try
                        {
                            string valorLogin = LoginUtilities.ObtenerLoginCookies();
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            Dictionary<string, DatorProductoCarrito> carritoNuevo = PedidoUtilities.GetCarrito();
                            Dictionary<string, DatorProductoCarrito> carritoAntiguo = js.Deserialize<Dictionary<string, DatorProductoCarrito>>(objCarrito.Contenido);
                            foreach (KeyValuePair<string, DatorProductoCarrito> recorrido in carritoAntiguo)
                            {
                                if (!carritoNuevo.ContainsKey(recorrido.Key))
                                {
                                    carritoNuevo.Add(recorrido.Key, recorrido.Value);
                                    UpdateCarrito(carritoNuevo);
                                }
                            }
                            CarritoBLL.DeleteCarrito(objCarrito.CarritoId);
                        }
                        catch (Exception ex)
                        {
                            //error fucionar las lista de articulos de carrito
                            throw ex;
                        }
                    }
                    //string cookieEmailName = "KomodoSuscription";
                    //HttpCookie cookieEmail = context.Request.Cookies[cookieEmailName];
                    //if (cookieEmail == null)
                    //{
                    //    cookie = new HttpCookie(cookieEmailName, cookieEmailName);
                    //    cookie.Expires = DateTime.Now.AddDays(365);
                    //    context.Response.Cookies.Add(cookie);
                    //}
                    existeCarrito = true;
                }
                return existeCarrito;
            }
            catch (Exception ex)
            { throw ex; }
        }

        //public static bool GetCarritoEnCuotas()
        //{
        //    try
        //    {
        //        string cartId = obtenerIdCarrito();
        //        bool value = false;
        //        //TODO: Obtener valor de la base de datos
        //        if (!string.IsNullOrEmpty(cartId))
        //            value = CarritoBLL.GetCarritoById(cartId).EnCuotas;
        //        //
        //        return value;
        //    }
        //    catch (Exception ex)
        //    { log.Error("Error al obtener el estado del carrito", ex); return false; }
        //}

        public static void SearchAndUpdateCarrito(string carritoId)
        {
            try
            {
                string cookieName = "FoodGoodCartId";
                bool cookieExists = HttpContext.Current.Request.Cookies[cookieName] != null;
                if (!cookieExists)
                {
                    PedidoUtilities.SetupShoppingCart();
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Dictionary<string, DatorProductoCarrito> carrito = js.Deserialize<Dictionary<string, DatorProductoCarrito>>(CarritoBLL.GetCarritoById(carritoId).Contenido);
                UpdateCarrito(carrito);
            }
            catch (Exception ex)
            { log.Error("Error en SearchAndUpdateCarrito, busqueda y actualizacion de carrito", ex); }
        }

        public static void UpdateCarrito(Dictionary<string, DatorProductoCarrito> carrito)
        {
            UpdateCarrito(carrito, HttpContext.Current);
        }

        public static void UpdateCarrito(Dictionary<string, DatorProductoCarrito> carrito, HttpContext context)
        {
            try
            {
                string cartId = obtenerIdCarrito(context);

                //FoodGood.Carrito.Carrito cartTemp = CarritoBLL.GetCarritoById(cartId);
                //if (cartTemp.conservarCarrito)
                //{
                //    CloneAndRemoveOldCart(cartTemp.Email, cartTemp.UserId);
                //    return;
                //}

                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonCart = js.Serialize(carrito);

                //TODO: Actualizar carrito en la base de datos

                //string email = null;
                int? usuarioId = null;

                //if (!string.IsNullOrEmpty(SuscripcionUtilities.GetCorreoSuscripto()))
                //{
                //    email = SuscripcionUtilities.GetCorreoSuscripto();
                //}
                //if (context.User.Identity.IsAuthenticated)
                //{
                //    usuarioId = UserBLL.GetUserByUsername(context.User.Identity.Name).UserId;
                //    email = UserBLL.GetUserByUsername(context.User.Identity.Name).Email;
                //}

                string clave = LoginUtilities.ObtenerLoginCookies();
                FoodGood.Usuario.Usuario user = LoginUtilities.LoginUser(clave);
                if (user != null)
                {
                    usuarioId = user.UsuarioId;
                }

                FoodGood.Carrito.Carrito obj = new FoodGood.Carrito.Carrito();
                obj.CarritoId = cartId;
                obj.UsuarioId = usuarioId;
                obj.Contenido = jsonCart;
                obj.Fecha = DateTime.Now;

                CarritoBLL.UpdateCarrtio(obj);
            }
            catch (Exception ex)
            { log.Error("Error al actualizar el carrito en la base de datos", ex); }
        }

        //public static void CloneAndRemoveOldCart(string email, int? userId)
        //{
        //    try
        //    {
        //        string oldGuid = SetupShoppingCart(HttpContext.Current);
        //        string cookieName = "KomodoCartId";

        //        string newGuid = SetupShoppingCart(HttpContext.Current);
        //        HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];

        //        try
        //        {
        //            newGuid = Guid.NewGuid().ToString();
        //            cookie = new HttpCookie(cookieName, newGuid);
        //            cookie.Expires = DateTime.Now.AddDays(365);
        //            HttpContext.Current.Response.Cookies.Add(cookie);

        //            Cart obj = new Cart();
        //            obj.CarritoId = newGuid;
        //            obj.Contenido = "{}";
        //            obj.UltimaActualizacion = DateTime.Now;
        //            Artexacta.App.Pedido.BLL.CarritoBLL.InsertCarrito(obj);
        //        }
        //        catch (Exception)
        //        { }

        //        Cart oldCartObj = Artexacta.App.Pedido.BLL.CarritoBLL.GetCarritoById(oldGuid);
        //        Cart cartToUpdate = Artexacta.App.Pedido.BLL.CarritoBLL.GetCarritoById(newGuid);
        //        if (string.IsNullOrEmpty(email))
        //            cartToUpdate.Email = null;
        //        else
        //            cartToUpdate.Email = email;
        //        if (userId <= 0 || userId == null)
        //            cartToUpdate.UserId = null;
        //        else
        //            cartToUpdate.UserId = userId;

        //        cartToUpdate.Contenido = oldCartObj.Contenido;
        //        cartToUpdate.EnCuotas = oldCartObj.EnCuotas;
        //        cartToUpdate.UltimaActualizacion = DateTime.Now;

        //        Artexacta.App.Pedido.BLL.CarritoBLL.UpdateCarrito(cartToUpdate);
        //    }
        //    catch (Exception ex)
        //    { log.Error("Error al remover viejo y crear nuevo carrito", ex); }
        //}

        //public static void SendNotifications(int pedidoId)
        //{
        //    try
        //    {
        //        Artexacta.App.Pedido.Pedido objPedido = Artexacta.App.Pedido.BLL.PedidoBLL.GetPedidoById(pedidoId);
        //        Artexacta.App.Cliente.Cliente objCliente = Artexacta.App.Cliente.BLL.ClienteBLL.GetClienteById(objPedido.ClienteId);
        //        Artexacta.App.User.User objUser = UserBLL.GetUserByAsistexaClienteId(objPedido.ClienteId);

        //        string text = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/DataFiles/Emails/ClientOrderCreated.html"));
        //        StringBuilder message = new StringBuilder(text);
        //        message.Replace("<%Name%>", objCliente.RazonSocial);

        //        string root = HttpContext.Current.Request.Url.Scheme + "://" +
        //        HttpContext.Current.Request.Url.Authority +
        //        HttpContext.Current.Request.ApplicationPath;

        //        string link1 = root + "/Account/OrderDetails.aspx?oId=" + objPedido.PedidoId;
        //        message.Replace("<%Link%>", link1);

        //        //Notificar al cliente
        //        EmailUtilities.SendEmail(objUser.Email, "Komodo - Pedido Creado", message.ToString());

        //        string managerEmail = Artexacta.App.Configuration.Configuration.GetStoreManagerEmail();
        //        if (string.IsNullOrEmpty(managerEmail))
        //            return;

        //        text = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/DataFiles/Emails/ManagerOrderCreated.html"));
        //        message = new StringBuilder(text);
        //        message.Replace("<%Name%>", objCliente.RazonSocial);

        //        string link2 = root + "/OpenPage.aspx?type=Pedido&oId=" + objPedido.PedidoId;
        //        message.Replace("<%Link%>", link2);

        //        //Notificar al Usuario responsable y al cliente
        //        EmailUtilities.SendEmail(managerEmail, "Administracion Komodo - Pedido Creado en la Tienda Virtual", message.ToString());
        //    }
        //    catch (Exception ex)
        //    { log.Error("Error sending email to client", ex); }
        //}

    }
}
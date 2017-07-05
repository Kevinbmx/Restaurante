using FoodGood.Usuario;
using FoodGood.Usuario.BLL;
using FoodGood.Utilities;
using FoodGood.Utilities.Security;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoginUtilities
/// </summary>
public class LoginUtilities
{
    //public static string LoginCookieName { get { return "login"; } }
    //public static string NombreUsuario { get { return "usuario"; } }

    public LoginUtilities()
    { }

    public static void ActualizarLoginCookies(string texto)
    {
        ActualizarLoginCookies(texto, HttpContext.Current);
        //ActualizarUsuarioCookies(texto, HttpContext.Current);
    }

    //public static void ActualizarUsuarioCookies(string email, HttpContext context)
    //{
    //    HttpCookie loginUsuarioCookie = context.Request.Cookies[NombreUsuario];
    //    bool itsNuevo = loginUsuarioCookie == null;
    //    loginUsuarioCookie = new HttpCookie(NombreUsuario);
    //    loginUsuarioCookie.Expires = DateTime.Now.AddDays(1d);
    //    loginUsuarioCookie.Value = email.ToString().ToLower();

    //    if (!itsNuevo)
    //        context.Response.Cookies.Remove(NombreUsuario);
    //    context.Response.Cookies.Add(loginUsuarioCookie);
    //}

    public static void ActualizarLoginCookies(string email, HttpContext context)
    {
        try
        {
            string LoginCookieName = "login";
            HttpCookie loginCookie = context.Request.Cookies[LoginCookieName];

            bool itsNew = loginCookie == null;


            loginCookie = new HttpCookie(LoginCookieName);
            loginCookie.Expires = DateTime.Now.AddDays(1d);
            loginCookie.Value = email.ToString().ToLower();

            if (!itsNew)
                context.Response.Cookies.Remove(LoginCookieName);
            context.Response.Cookies.Add(loginCookie);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string ObtenerLoginCookies()
    {
        return ObtenerLoginCookies(HttpContext.Current);
    }

    public static string ObtenerLoginCookies(HttpContext context)
    {
        try
        {
            string valor = "";
            string LoginCookieName = "login";
            if (context.Request.Cookies[LoginCookieName] != null)
            {
                HttpCookie cookie = context.Request.Cookies[LoginCookieName];
                valor = cookie.Value;
                return valor;
            }
            else
            {
                HttpCookie loginCookie = new HttpCookie(LoginCookieName, "");
                loginCookie.Expires = DateTime.Now.AddDays(1d);
                HttpContext.Current.Response.Cookies.Add(loginCookie);
                return valor;
            }
            //return null;






            //HttpCookie loginCookie = HttpContext.Current.Request.Cookies[LoginCookieName];
            //if (loginCookie == null)
            //{
            //    loginCookie = new HttpCookie(LoginCookieName, "");
            //    loginCookie.Expires = DateTime.Now.AddDays(1d);
            //    HttpContext.Current.Response.Cookies.Add(loginCookie);
            //}
            //string value = "";
            //value = loginCookie.Value;
            //return value;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool IsUserAuthenticated()
    {
        return HttpContext.Current.User.Identity.IsAuthenticated;
    }


    public static Usuario GetUserLogged()
    {
        string usuarioEmail = ObtenerLoginCookies();
        if (!string.IsNullOrEmpty(usuarioEmail))
        {
            return LoginUser(usuarioEmail);
        }
        return null;
    }

    public static Searcher consultaSqlUsuario(string query)
    {
        Searcher searcher = new Searcher(new BusquedaUsuaio());
        searcher.Query = query;
        return searcher;
    }

    public static Usuario LoginUser(string EmailPassword)
    {
        try
        {
            string armadoDeQuery = "@tipousuarioId IN(1,3)";
            string query = consultaSqlUsuario(armadoDeQuery).SqlQuery();
            List<Usuario> lista = UsuariosBLL.GetUsuarioListForSearch(query);
            string usuario = "";
            if (lista != null)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    usuario = CryptographyFunctions.SHA1HashTheString(lista[i].Email + lista[i].Password);
                    if (usuario.ToLower().Equals(EmailPassword.ToLower()))
                    {
                        ActualizarLoginCookies(EmailPassword);
                        return lista[i];
                    }
                }
            }

            ActualizarLoginCookies("");
            return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }



    public static void CloseSesion()
    {
        CloseSesion(HttpContext.Current);
        //ActualizarUsuarioCookies(texto, HttpContext.Current);
    }


    public static void CloseSesion(HttpContext context)
    {
        string LoginCookieName = "login";
        HttpCookie myCookie = new HttpCookie(LoginCookieName);
        myCookie.Expires = DateTime.Now.AddDays(-1d);
        context.Response.Cookies.Add(myCookie);

        PedidoUtilities.borrarCarritoIdCookie();
        //string cookieName = "FoodGoodCartId";

        //myCookie = new HttpCookie(cookieName);
        //myCookie.Expires = DateTime.Now.AddDays(-1d);
        //context.Response.Cookies.Add(myCookie);

        //ActualizarLoginCookies("");
        return;
    }

}
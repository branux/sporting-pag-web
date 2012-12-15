using System;
using System.Data;
using System.Collections.Generic;

public class GestorNoticias
{
    public static List<Noticia> getNoticiasPrincipales()
    {
        return NoticiaDAL.getNoticiasPrincipales();
    }

    /// <summary>
    /// Ordeno por el id de mayor a menor para tener las mas nuevas al comienzo y validando
    /// que no sean principales devuelvo dos noticias.
    /// </summary>
    /// <returns></returns>
    public static List<Noticia> getNoticiasLaterales()
    {
        List<Noticia> noticias = NoticiaDAL.getNoticias();
        List<Noticia> noticiasLaterales = new List<Noticia>();
        noticias.Sort();
        int cantNoticias = 2; //cant de noticias a mostrar en la barra lateral
        for (int i=0;i<noticias.Count;i++)
        {
            if (noticiasLaterales.Count == cantNoticias)
            {
                break;
            }
            if (!noticias[i].Principal1 && !noticias[i].Principal2)
            {
                //si no es una noticia principal la agrego
                noticiasLaterales.Add(noticias[i]);
            }
        }
        return noticiasLaterales;
    }
}
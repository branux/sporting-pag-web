﻿using System.Data.Odbc;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.SqlClient;

public class NoticiaDAL
{
    //Obtengo una noticia de base de datos por su id
    public static Noticia getNoticiaById(int id)
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        OdbcCommand cmd = null;
        Noticia noticia = null;
        try
        {
            cmd = new OdbcCommand(selectNoticia + "WHERE n.id=" + id.ToString(), con);

            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                noticia = new Noticia();
                noticia.IdNoticia = dr.GetInt32(0);
                noticia.Titulo = dr.GetString(1);
                noticia.Descripcion = dr.GetString(2);
                setImagenes(con,noticia);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un error al intentar obtener la noticia "+id+" de la base de datos. "+e.Message);
        }
        finally
        {
            cmd.Connection.Close();
        }
        return noticia;
    }

    /// <summary>
    /// Retorna la lista de todos las noticias
    /// </summary>
    /// <returns></returns>
    public static List<Noticia> getNoticias()
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        DataSet ds = new DataSet();
        List<Noticia> listaNoticias = new List<Noticia>();
        try
        {
            OdbcCommand cmd = new OdbcCommand(selectNoticia, con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Noticia noticia = new Noticia();
                noticia.IdNoticia = dr.GetInt32(0);
                noticia.Titulo = dr.GetString(1);
                noticia.Descripcion = dr.GetString(2);
                noticia.Principal = dr.GetBoolean(3);
                setImagenes(con,noticia);
                listaNoticias.Add(noticia);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener todas las noticias. " + e.Message);
        }
        finally
        {
            con.Close();
        }
        return listaNoticias;
    }

    /// <summary>
    /// Retorna un datatable con todas las noticias.
    /// Cada noticia con el pathmedium de su imagen de portada.
    /// </summary>
    /// <returns></returns>
    public static DataTable getDataTableNoticias()
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        DataTable dataTable = new DataTable();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT n.id, n.titulo, n.descripcion, n.principal, i.pathMedium "+
                "FROM noticia n, imagen_X_noticia ixn, imagen i "+
                "WHERE ixn.idImagen = i.id AND ixn.idNoticia = n.id AND i.portada = 1", con);
            cmd.CommandType = CommandType.Text;
            dataTable = new DataTable();
            OdbcDataAdapter adapter = new OdbcDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener todas las noticias. " + e.Message);
        }
        finally
        {
            con.Close();
        }
        return dataTable;
    }

    public static List<Noticia> getNoticiasPrincipales()
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        DataSet ds = new DataSet();
        List<Noticia> listaNoticias = new List<Noticia>();
        try
        {
            OdbcCommand cmd = new OdbcCommand(selectNoticia + "WHERE principal = 1", con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Noticia noticia = new Noticia();
                noticia.IdNoticia = dr.GetInt32(0);
                noticia.Titulo = dr.GetString(1);
                noticia.Descripcion = dr.GetString(2);
                noticia.Principal = dr.GetBoolean(3);
                setImagenes(con,noticia);
                listaNoticias.Add(noticia);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener las noticias principales. " + e.Message);
        }
        finally
        {
            con.Close();
        }
        return listaNoticias;
    }

    /// <summary>
    /// Setea la lista de imagenes de una noticia
    /// </summary>
    /// <returns></returns>
    public static void setImagenes(OdbcConnection con,Noticia noticia)
    {
        DataSet ds = new DataSet();
        List<Imagen> listaImagenes = new List<Imagen>();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT i.id, i.pathBig, i.pathSmall, i.portada FROM imagen i, imagen_x_noticia n WHERE i.id=n.idImagen AND n.idNoticia=" + noticia.IdNoticia, con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Imagen imagen = new Imagen();
                imagen.IdImagen = dr.GetInt32(0);
                imagen.PathBig = dr.GetString(1);
                imagen.PathSmall = dr.GetString(2);
                imagen.Portada = dr.GetBoolean(3);
                listaImagenes.Add(imagen);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener las imagenes de las noticias. " + e.Message);
        }
        noticia.Imagenes = listaImagenes;
    }

    public static DataTable getDataTableImagenes(int id)
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        DataSet ds = new DataSet();
        List<Noticia> listaNoticias = new List<Noticia>();
        DataTable dataTable = null;
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT i.pathBig, i.pathSmall FROM imagen_x_noticia ixn, imagen i" +
            " WHERE ixn.idNoticia = "+id+" AND i.id = ixn.idImagen", con);
            cmd.CommandType = CommandType.Text;

            dataTable = new DataTable();
            OdbcDataAdapter adapter = new OdbcDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener las noticias principales. " + e.Message);
        }
        finally
        {
            con.Close();
        }
        return dataTable;
    }

    private static String selectNoticia = "SELECT n.id, n.titulo, n.descripcion, n.principal FROM noticia n ";
}
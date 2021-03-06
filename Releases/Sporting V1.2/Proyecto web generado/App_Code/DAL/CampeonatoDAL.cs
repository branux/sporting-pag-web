﻿using System.Data.Odbc;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.SqlClient;

public class CampeonatoDAL
{
    /// <summary>
    /// Obtiene el campeonato actual.
    /// Se resuelve obteniendo el ultimo registro de la tabla campeonato.
    /// </summary>
    /// <returns></returns>
    public static CampeonatoLiga getCampeonatoActual()
    {
        CampeonatoLiga camp = new CampeonatoLiga();
        OdbcDataReader dr = null;
        String selectCampActual = "SELECT c.id, c.nombre, c.anio FROM campeonato c "+
                "WHERE c.id = (SELECT MAX(id) FROM campeonato)";

        using (OdbcConnection con = new OdbcConnection(Constantes.CONNECTION_STRING))
        {
            using (OdbcCommand cmd = new OdbcCommand(selectCampActual, con))
            {
                try
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        camp.IdCampeonato = dr.GetInt32(dr.GetOrdinal("id"));
                        camp.Nombre = dr.GetString(dr.GetOrdinal("nombre"));
                        camp.Anio = dr.GetInt32(dr.GetOrdinal("anio"));
                        camp.ListaFechas = getFechasDeCampeonato(camp, con);
                    }
                }
                catch (Exception e)
                {
                    throw new SportingException("Ocurrio un problema al intentar obtener el campeonato actual. " + e.Message);
                }
            }
        }
        
        return camp;
    }

    public static List<FechaCampeonato> getFechasDeCampeonato(CampeonatoLiga camp)
    {
        List<FechaCampeonato> fechas = new List<FechaCampeonato>();
        using (OdbcConnection con = new OdbcConnection(Constantes.CONNECTION_STRING))
        {
            con.Open();
            fechas = getFechasDeCampeonato(camp, con);
        }
        return fechas;
    }

    /// <summary>
    /// Obtiene la lista con las fechas de un campeonato especifico
    /// </summary>
    /// <param name="camp"></param>
    /// <returns></returns>
    public static List<FechaCampeonato> getFechasDeCampeonato(CampeonatoLiga camp, OdbcConnection con)
    {
        List<FechaCampeonato> fechas = new List<FechaCampeonato>();
        String getFechas = "SELECT f.id, f.numero, f.descripcion FROM fecha_campeonato f" +
                " WHERE f.idCampeonato = " + camp.IdCampeonato + " ORDER BY f.numero";
        OdbcDataReader dr = null;

        try
        {
            OdbcCommand cmd = new OdbcCommand(getFechas, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                FechaCampeonato f = new FechaCampeonato();
                f.IdFecha = dr.GetInt32(0);
                f.Numero = dr.GetInt32(1);
                f.Descripcion = dr.GetString(2);
                f.Resultados = getResultadosFecha(f, con);
                fechas.Add(f);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener las fechas del campeonato '" + camp.Nombre + "'. " + e.Message);
        }

        return fechas;
    }

    /// <summary>
    /// Obtiene la lista de resultados de una fecha especifica.
    /// </summary>
    /// <param name="fecha"></param>
    /// <returns></returns>
    public static List<Resultado> getResultadosFecha(FechaCampeonato fecha, OdbcConnection con)
    {
        List<Resultado> resultados = new List<Resultado>();
        String getResultadosFecha = "SELECT r.id, r.idEquipoLocal, r.localPuntos, r.idEquipoVisitante, r.visitantePuntos, r.jugado, r.fechaPartido "+
                "FROM resultado_partido r " +
                "WHERE r.idFecha = " + fecha.IdFecha;
        OdbcDataReader dr = null;

        try
        {
            OdbcCommand cmd = new OdbcCommand(getResultadosFecha, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Resultado r = new Resultado();
                r.IdResultado = dr.GetInt32(0);
                r.EquipoLocal = getEquipoById(dr.GetInt32(1), con);
                r.EquipoLocalPuntos = dr.GetInt32(2);
                r.EquipoVisitante = getEquipoById(dr.GetInt32(3), con);
                r.EquipoVisitantePuntos = dr.GetInt32(4);
                r.Jugado = dr.GetBoolean(5);
                r.FechaPartido = dr.GetDate(dr.GetOrdinal("fechaPartido"));
                resultados.Add(r);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener los resultados de la fecha '" + fecha.Numero + "'. " + e.Message);
        }
        return resultados;
    }

    /// <summary>
    /// Obtiene un equipo especifico
    /// </summary>
    /// <returns></returns>
    public static EquipoCampeonato getEquipoById(int id, OdbcConnection con)
    {
        EquipoCampeonato equipo = null;
        String selectEquipo = "SELECT e.id, e.nombre, e.localidad FROM equipo e " +
                "WHERE e.id = "+id;
        OdbcDataReader dr = null;

        try
        {
            OdbcCommand cmd = new OdbcCommand(selectEquipo, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                equipo = new EquipoCampeonato();
                equipo.IdEquipo = dr.GetInt32(0);
                equipo.Nombre = dr.GetString(1);
                equipo.Localidad = dr.GetString(2);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener el equipo con id '" + id + "'. " + e.Message);
        }
        if (equipo == null)
        {
            throw new SportingException("El equipo con id '" + id + "' no existe en la base de datos. ");
        }
        return equipo;
    }

    /// <summary>
    /// Devuelve la lista con todos los resultados del campeonato actual
    /// Con el boolean 'soloJugados' se decide si solo traer los ya jugados (true) o traer todos (false)
    /// </summary>
    /// <param name="camp"></param>
    /// <returns></returns>
    public static List<Resultado> getResultadosCampeonato(CampeonatoLiga camp, Boolean soloJugados)
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        List<Resultado> resultados = null;
        try
        {
            String query = "SELECT r.id, r.idEquipoLocal, r.localPuntos, r.idEquipoVisitante, r.visitantePuntos " +
                "FROM resultado_partido r";
            if (soloJugados)
            {
                query += " WHERE r.jugado <> 0";
            }

            OdbcCommand cmd = new OdbcCommand(query, con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();
            resultados = new List<Resultado>();
            while (dr.Read())
            {       
                Resultado resultado = new Resultado();

                int idEquipoLocal = dr.GetInt32(dr.GetOrdinal("idEquipoLocal"));
                EquipoCampeonato equipoLocal = getEquipoById(idEquipoLocal, con);
                resultado.EquipoLocal = equipoLocal;
                resultado.EquipoLocalPuntos = dr.GetInt32(dr.GetOrdinal("localPuntos"));

                int idEquipoVisitante = dr.GetInt32(dr.GetOrdinal("idEquipoVisitante"));
                EquipoCampeonato equipoVisitante = getEquipoById(idEquipoVisitante, con);
                resultado.EquipoVisitante = equipoVisitante;
                resultado.EquipoVisitantePuntos = dr.GetInt32(dr.GetOrdinal("visitantePuntos"));

                resultado.Jugado = true; //porque la query trae solo los partidos jugados
                
                resultados.Add(resultado);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener la tabla de posiciones. " + e.Message);
        }
        return resultados;
    }

    public static List<CampeonatoLiga> getCampeonatos()
    {
        List<CampeonatoLiga> campeonatos = new List<CampeonatoLiga>();
        OdbcDataReader dr = null;
        String query = "SELECT c.id, c.nombre, c.anio FROM campeonato c " +
                "Order by c.anio desc";

        using (OdbcConnection con = new OdbcConnection(Constantes.CONNECTION_STRING))
        {
            using (OdbcCommand cmd = new OdbcCommand(query, con))
            {
                try
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    dr = cmd.ExecuteReader();

                    CampeonatoLiga camp;
                    while (dr.Read())
                    {
                        camp = new CampeonatoLiga();
                        camp.IdCampeonato = dr.GetInt32(dr.GetOrdinal("id"));
                        camp.Nombre = dr.GetString(dr.GetOrdinal("nombre"));
                        camp.Anio = dr.GetInt32(dr.GetOrdinal("anio"));
                        camp.ListaFechas = getFechasDeCampeonato(camp, con);

                        campeonatos.Add(camp);
                    }
                }
                catch (Exception e)
                {
                    throw new SportingException("Ocurrio un problema al intentar obtener los campeonatos. " + e.Message);
                }
            }
        }
        return campeonatos;
    }

    public static void insertarCampeonato(CampeonatoLiga camp)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            if (camp == null)
            {
                throw new SportingException("Error al registrar nuevo campeonato. Campeonato sin información.");
            }
            conexion = ConexionBD.ObtenerConexion();

            //Guardo los datos del campeonato
            String insertarCamperonato = " INSERT INTO campeonato (nombre, anio)" +
                                         " VALUES ('" + camp.Nombre + "', " + camp.Anio + ")";
            cmd = new OdbcCommand(insertarCamperonato, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static void updateCampeonato(CampeonatoLiga camp)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            if (camp == null)
            {
                throw new SportingException("Error al actualizar el campeonato. Campeonato sin información.");
            }
            conexion = ConexionBD.ObtenerConexion();

            //Actualizo los datos del campeonato
            String updateCamp = "UPDATE campeonato set nombre='" + camp.Nombre +
                                    "', anio = " + camp.Anio + " WHERE id = " +
                                    camp.IdCampeonato.ToString();
            cmd = new OdbcCommand(updateCamp, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static void deleteCampeonato(string idCamp)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            conexion = ConexionBD.ObtenerConexion();

            String deleteCampeonato = "DELETE FROM campeonato WHERE id = " + idCamp.ToString();

            cmd = new OdbcCommand(deleteCampeonato, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un error al intentar borrar el campeonato. " + e.Message);
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static void insertarFecha(FechaCampeonato fechaCamp)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            if (fechaCamp == null)
            {
                throw new SportingException("Error al registrar nueva fecha de campeonato. Fecha sin información.");
            }
            conexion = ConexionBD.ObtenerConexion();

            //Guardo los datos de la fecha
            String insertarFecha = " INSERT INTO fecha_campeonato (numero, descripcion, idCampeonato)" +
                                         " VALUES (" + fechaCamp.Numero + ", '" + fechaCamp.Descripcion +
                                         "' , " + fechaCamp.IdCampeonato + ")";
            cmd = new OdbcCommand(insertarFecha, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static void updateFecha(FechaCampeonato fechaCamp)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            if (fechaCamp == null)
            {
                throw new SportingException("Error al actualizar la fecha de campeonato. Fecha sin información.");
            }
            conexion = ConexionBD.ObtenerConexion();

            //Actualizo los datos de la fecha de campeonato
            String updateFecha = "UPDATE fecha_campeonato set numero=" + fechaCamp.Numero +
                                    ", descripcion = '" + fechaCamp.Descripcion +
                                    "', idCampeonato = "+ fechaCamp.IdCampeonato +" WHERE id = " +
                                    fechaCamp.IdFecha.ToString();
            cmd = new OdbcCommand(updateFecha, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static void deleteFecha(string idFecha)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            conexion = ConexionBD.ObtenerConexion();

            String deleteFecha = "DELETE FROM fecha_campeonato WHERE id = " + idFecha.ToString();

            cmd = new OdbcCommand(deleteFecha, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un error al intentar borrar la fecha de campeonato. " + e.Message);
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static List<EquipoCampeonato> getEquipos()
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        List<EquipoCampeonato> equipos = new List<EquipoCampeonato>();
        try
        {
            conexion = ConexionBD.ObtenerConexion();
            String getEquipos = "SELECT id, nombre, localidad FROM equipo ";

            cmd = new OdbcCommand(getEquipos, conexion);

            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            EquipoCampeonato equipo;
            foreach (DataRow row in dt.Rows)
            {
                equipo = new EquipoCampeonato();
                equipo.IdEquipo = Convert.ToInt32(row["id"].ToString());
                equipo.Nombre = row["nombre"].ToString();
                equipo.Localidad = row["localidad"].ToString();
                equipos.Add(equipo);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un error al intentar obtener los equipos. " + e.Message);
        }
        finally
        {
            cmd.Connection.Close();
        }
        return equipos;
    }

    public static void insertarEquipo(EquipoCampeonato equipo)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            if (equipo == null)
            {
                throw new SportingException("Error al registrar nuevo equipo. Equipo sin información.");
            }
            conexion = ConexionBD.ObtenerConexion();

            //Guardo los datos del equipo
            String insertarEquipo = " INSERT INTO equipo (nombre, localidad)" +
                                         " VALUES ('" + equipo.Nombre + "', '" + equipo.Localidad + "')";
            cmd = new OdbcCommand(insertarEquipo, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static void updateEquipo(EquipoCampeonato equipo)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            if (equipo == null)
            {
                throw new SportingException("Error al actualizar el equipo. Equipo sin información.");
            }
            conexion = ConexionBD.ObtenerConexion();

            //Actualizo los datos del equipo
            String updateEquipo = "UPDATE equipo set nombre='" + equipo.Nombre +
                                    "', localidad = '" + equipo.Localidad + "' WHERE id = " +
                                    equipo.IdEquipo.ToString();
            cmd = new OdbcCommand(updateEquipo, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static void deleteEquipo(string idEquipo)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            conexion = ConexionBD.ObtenerConexion();

            String deleteEquipo = "DELETE FROM equipo WHERE id = " + idEquipo.ToString();

            cmd = new OdbcCommand(deleteEquipo, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un error al intentar borrar el equipo. " + e.Message);
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static List<FechaCampeonato> getFixtureCampeonato_porFecha(CampeonatoLiga camp, int fecha)
    {
        List<FechaCampeonato> fechasCamp = new List<FechaCampeonato>();
        try
        {
            OdbcConnection con = ConexionBD.ObtenerConexion();
            String getFixture = " SELECT f.id, f.numero, f.descripcion FROM fecha_campeonato f" +
                                " WHERE f.idCampeonato = " + camp.IdCampeonato + " AND f.id = " + fecha;
            OdbcCommand cmd = new OdbcCommand(getFixture, con);
            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            FechaCampeonato f;
            foreach (DataRow row in dt.Rows)
            {
                f = new FechaCampeonato();
                f.IdFecha = Convert.ToInt32(row["id"].ToString());
                f.Numero = Convert.ToInt32(row["numero"].ToString());
                f.Descripcion = row["descripcion"].ToString();
                f.Resultados = getResultadosFecha(f, con);
                fechasCamp.Add(f);
            }
        }
        catch (SportingException sEx)
        {
            throw sEx;
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener las fechas del campeonato. " + e.Message);
        }
        return fechasCamp;
    }

    public static void insertarPartidoFixture(FechaCampeonato partidoFixture)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            if (partidoFixture == null)
            {
                throw new SportingException("Error al registrar nuevo partido en el fixture. Partido sin información.");
            }
            conexion = ConexionBD.ObtenerConexion();

            //Guardo los datos del partido
            String insertarPartidoFixture = " INSERT INTO resultado_partido (idFecha, idEquipoLocal, localPuntos, " +
                                            " idEquipoVisitante, visitantePuntos, jugado, fechaPartido) " +
                                            " VALUES (" + partidoFixture.IdFecha + ", " + partidoFixture.Resultados[0].EquipoLocal.IdEquipo + ", " +
                                            partidoFixture.Resultados[0].EquipoLocalPuntos +", "+ partidoFixture.Resultados[0].EquipoVisitante.IdEquipo + ", " +
                                            partidoFixture.Resultados[0].EquipoVisitantePuntos +", "+ partidoFixture.Resultados[0].Jugado+ ", '" +
                                            partidoFixture.Resultados[0].FechaPartido.ToString("yyyy/MM/dd") + "')";

            cmd = new OdbcCommand(insertarPartidoFixture, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static void updatePartidoFixture(FechaCampeonato partidoFixture)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            if (partidoFixture == null)
            {
                throw new SportingException("Error al modificar partido en el fixture. Partido sin información.");
            }
            conexion = ConexionBD.ObtenerConexion();

            //Actualizo los datos del partido
            String updatePartidoFixture = "UPDATE resultado_partido set idFecha = " + partidoFixture.IdFecha + ", "+
                                             " idEquipoLocal = " + partidoFixture.Resultados[0].EquipoLocal.IdEquipo + ", "+
                                             " localPuntos = " + partidoFixture.Resultados[0].EquipoLocalPuntos + ", "+
                                             " idEquipoVisitante = " + partidoFixture.Resultados[0].EquipoVisitante.IdEquipo + ", "+
                                             " visitantePuntos = " + partidoFixture.Resultados[0].EquipoVisitantePuntos + ", "+
                                             " jugado = " + partidoFixture.Resultados[0].Jugado + ", "+
                                             " fechaPartido = '" + partidoFixture.Resultados[0].FechaPartido.ToString("yyyy/MM/dd") + "' "+
                                          "WHERE id = " +partidoFixture.Resultados[0].IdResultado.ToString();


            cmd = new OdbcCommand(updatePartidoFixture, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static void deletePartidoFixture(string idResultadoPartido)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            conexion = ConexionBD.ObtenerConexion();

            String deletePartidoFixture = "DELETE FROM resultado_partido WHERE id = " + idResultadoPartido.ToString();

            cmd = new OdbcCommand(deletePartidoFixture, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un error al intentar borrar el partido del fixture. " + e.Message);
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
}
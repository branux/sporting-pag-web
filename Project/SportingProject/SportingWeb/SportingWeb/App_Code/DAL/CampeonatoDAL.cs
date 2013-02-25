using System.Data.Odbc;
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
        OdbcConnection con = ConexionBD.ObtenerConexion();
        DataSet ds = new DataSet();
        CampeonatoLiga camp = new CampeonatoLiga();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT c.id, c.nombre, c.anio FROM campeonato c "+
                "WHERE c.id = (SELECT MAX(id) FROM campeonato)", con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

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
        return camp;
    }

    /// <summary>
    /// Obtiene la lista con las fechas de un campeonato especifico
    /// </summary>
    /// <param name="camp"></param>
    /// <returns></returns>
    public static List<FechaCampeonato> getFechasDeCampeonato(CampeonatoLiga camp, OdbcConnection con)
    {
        DataSet ds = new DataSet();
        List<FechaCampeonato> fechas = new List<FechaCampeonato>();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT f.id, f.numero, f.fecha FROM fecha_campeonato f" +
                " WHERE f.idCampeonato = " + camp.IdCampeonato, con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                FechaCampeonato f = new FechaCampeonato();
                f.IdFecha = dr.GetInt32(0);
                f.Numero = dr.GetInt32(1);
                f.Fecha = dr.GetDateTime(2);
                f.Resultados = getResultadosFecha(f, con);
                fechas.Add(f);                
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener las fechas del campeonato '"+camp.Nombre+"'. " + e.Message);
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
        DataSet ds = new DataSet();
        List<Resultado> resultados = new List<Resultado>();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT r.id, r.idEquipoLocal, r.localPuntos, r.idEquipoVisitante, r.visitantePuntos "+
                "FROM resultado_partido r " +
                "WHERE r.idFecha = " + fecha.IdFecha, con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Resultado r = new Resultado();
                r.IdResultado = dr.GetInt32(0);
                r.EquipoLocal = getEquipoById(dr.GetInt32(1), con);
                r.EquipoLocalPuntos = dr.GetInt32(2);
                r.EquipoVisitante = getEquipoById(dr.GetInt32(3), con);
                r.EquipoVisitantePuntos = dr.GetInt32(4);
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
        DataSet ds = new DataSet();
        EquipoCampeonato equipo = null;
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT e.id, e.nombre, e.localidad FROM equipo e " +
                "WHERE e.id = "+id, con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

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
            throw new SportingException("Ocurrio un problema al intentar obtener el equipo con id '"+id+"'. " + e.Message);
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
        DataSet ds = new DataSet();
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
}
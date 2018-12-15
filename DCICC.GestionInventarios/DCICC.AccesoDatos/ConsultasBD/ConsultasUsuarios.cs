using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using DCICC.Seguridad.Encryption;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasUsuarios
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasUsuarios()
        {
            conn_BD=ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los Usuarios de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultausuarios o usuarioshabilitados</param>
        /// <returns></returns>
        public MensajesUsuarios ObtenerUsuarios(string nombreFuncion)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            List<Usuarios> lstUsuarios = new List<Usuarios>();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        if(nombreFuncion=="consultausuarios")
                        {
                            while (dr.Read())
                            {
                                Usuarios objUsuarios = new Usuarios
                                {
                                    IdUsuario = (int)dr[0],
                                    IdRol = (int)dr[1],
                                    NombresUsuario = dr[2].ToString().Trim(),
                                    NickUsuario = dr[3].ToString().Trim(),
                                    PasswordUsuario = ConfigEncryption.DesencriptarValor(dr[4].ToString().Trim()),
                                    CorreoUsuario = dr[5].ToString().Trim(),
                                    TelefonoUsuario = dr[6].ToString().Trim(),
                                    TelefonoCelUsuario = dr[7].ToString().Trim(),
                                    DireccionUsuario = dr[8].ToString().Trim(),
                                    HabilitadoUsuario = (bool)dr[9],
                                };
                                lstUsuarios.Add(objUsuarios);
                            }
                        }
                        else
                        {
                            while (dr.Read())
                            {
                                Usuarios objUsuarios = new Usuarios
                                {
                                    IdUsuario = (int)dr[0],
                                    IdRol = (int)dr[1],
                                    NombresUsuario = dr[2].ToString().Trim(),
                                    NickUsuario = dr[3].ToString().Trim(),
                                    PasswordUsuario = ConfigEncryption.DesencriptarValor(dr[4].ToString().Trim()),
                                    CorreoUsuario = dr[5].ToString().Trim(),
                                    TelefonoUsuario = dr[6].ToString().Trim(),
                                    TelefonoCelUsuario = dr[7].ToString().Trim(),
                                    DireccionUsuario = dr[8].ToString().Trim(),
                                    HabilitadoUsuario = (bool)dr[9],
                                    NombreRol = dr[10].ToString().Trim()
                                };
                                lstUsuarios.Add(objUsuarios);
                            }
                        }
                        conn_BD.Close();
                        msjUsuarios.ListaObjetoInventarios = lstUsuarios;
                        msjUsuarios.OperacionExitosa = true;
                    }
                }                
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
                msjUsuarios.ListaObjetoInventarios = null;
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para obtener un Usuario en específico de la base de datos por su ID.
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios ObtenerUsuarioPorId(int IdUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                MensajesUsuarios msjUsuariosConsulta = objConsultasUsuariosBD.ObtenerUsuarios("consultausuarios");
                Usuarios infoUsuarioBD = msjUsuariosConsulta.ListaObjetoInventarios.Find(x => x.IdUsuario == IdUsuario);
                if (infoUsuarioBD != null)
                {
                    msjUsuarios.ObjetoInventarios = infoUsuarioBD;
                    msjUsuarios.OperacionExitosa = true;
                }
                else
                {
                    msjUsuarios.ObjetoInventarios = null;
                    msjUsuarios.OperacionExitosa = true;
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
                msjUsuarios.ListaObjetoInventarios = null;
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para obtener un Usuario en específico de la base de datos por su Nick
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios ObtenerUsuarioPorNick(string nickUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                MensajesUsuarios msjUsuariosConsulta = objConsultasUsuariosBD.ObtenerUsuarios("consultausuarios");
                Usuarios infoUsuarioBD = msjUsuariosConsulta.ListaObjetoInventarios.Find(x => x.NickUsuario == nickUsuario);
                if (infoUsuarioBD != null)
                {
                    msjUsuarios.ObjetoInventarios = infoUsuarioBD;
                    msjUsuarios.OperacionExitosa = true;
                }else
                {
                    msjUsuarios.ObjetoInventarios = null;
                    msjUsuarios.OperacionExitosa = true;
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
                msjUsuarios.ListaObjetoInventarios = null;
            }
            return msjUsuarios;
        }
    }
}
